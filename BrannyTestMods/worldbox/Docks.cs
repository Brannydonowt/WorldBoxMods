using System;
using System.Collections.Generic;

// Token: 0x020000D1 RID: 209
public class Docks : BaseBuildingComponent
{
	// Token: 0x0600044F RID: 1103 RVA: 0x0003D034 File Offset: 0x0003B234
	internal override void create()
	{
		base.create();
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x0003D03C File Offset: 0x0003B23C
	public TileIsland getIsland()
	{
		if (!(this.building.city != null))
		{
			return null;
		}
		WorldTile tile = this.building.city.getTile();
		if (tile == null)
		{
			return null;
		}
		return tile.region.island;
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x0003D074 File Offset: 0x0003B274
	public WorldTile getOceanTileInSameOcean(WorldTile pTile)
	{
		for (int i = 0; i < this.tiles_ocean.Count; i++)
		{
			WorldTile worldTile = this.tiles_ocean[i];
			if (worldTile.isSameIsland(pTile))
			{
				return worldTile;
			}
		}
		return null;
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x0003D0B0 File Offset: 0x0003B2B0
	public bool checkOceanTiles()
	{
		this.recalculateOceanTiles();
		return this.tiles_ocean.Count != 0;
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x0003D0C8 File Offset: 0x0003B2C8
	public void recalculateOceanTiles()
	{
		this.tiles_ocean.Clear();
		WorldTile tile = this.world.GetTile(this.building.currentTile.x - 4, this.building.currentTile.y);
		if (tile != null && tile.isGoodForBoat())
		{
			this.tiles_ocean.Add(tile);
		}
		tile = this.world.GetTile(this.building.currentTile.x + 5, this.building.currentTile.y);
		if (tile != null && tile.isGoodForBoat())
		{
			this.tiles_ocean.Add(tile);
		}
		tile = this.world.GetTile(this.building.currentTile.x, this.building.currentTile.y - 4);
		if (tile != null && tile.isGoodForBoat())
		{
			this.tiles_ocean.Add(tile);
		}
		tile = this.world.GetTile(this.building.currentTile.x, this.building.currentTile.y + 7);
		if (tile != null && tile.isGoodForBoat())
		{
			this.tiles_ocean.Add(tile);
		}
		this.tiles_ocean.Shuffle<WorldTile>();
		if (this.tiles_ocean.Count == 0)
		{
			this.building.startRemove(true);
		}
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x0003D218 File Offset: 0x0003B418
	public bool isDockGood()
	{
		int num = 0;
		while (num < this.tiles_ocean.Count && this.tiles_ocean[num].Type.ocean)
		{
			num++;
		}
		return false;
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x0003D254 File Offset: 0x0003B454
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x0003D260 File Offset: 0x0003B460
	public bool haveFreeSlot(ActorStats pStats)
	{
		List<Actor> list = this.getList(pStats);
		return this.limit > list.Count;
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x0003D286 File Offset: 0x0003B486
	public bool isMoreLimit(ActorStats pStats)
	{
		return this.getList(pStats).Count > this.limit;
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x0003D2A0 File Offset: 0x0003B4A0
	public Actor buildBoatFromHere(City pCity)
	{
		Culture culture = pCity.getCulture();
		if (culture == null)
		{
			return null;
		}
		ActorStats actorStats = AssetManager.unitStats.get(Docks.boat_types.GetRandom<string>());
		if (!string.IsNullOrEmpty(actorStats.tech) && !culture.haveTech(actorStats.tech))
		{
			return null;
		}
		this.getList(actorStats);
		if (!this.haveFreeSlot(actorStats))
		{
			return null;
		}
		if (!pCity.haveEnoughResourcesFor(actorStats.cost))
		{
			return null;
		}
		if (this.tiles_ocean.Count == 0)
		{
			this.recalculateOceanTiles();
			return null;
		}
		if (this.tiles_ocean.GetRandom<WorldTile>().region.island.getTileCount() < 2500)
		{
			return null;
		}
		Actor actor = this.world.createNewUnit(actorStats.id, this.tiles_ocean.GetRandom<WorldTile>(), null, 0f, null);
		if (actor == null)
		{
			return null;
		}
		this.addBoatToDock(actor);
		pCity.spendResourcesFor(actorStats.cost);
		return actor;
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x0003D38C File Offset: 0x0003B58C
	private List<Actor> getList(ActorStats pStats)
	{
		string id = pStats.id;
		if (id != null)
		{
			if (id == "boat_transport")
			{
				return this.boats_transport;
			}
			if (id == "boat_fishing")
			{
				return this.boats_fishing;
			}
			if (id == "boat_trading")
			{
				return this.boats_trading;
			}
		}
		return null;
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x0003D3E2 File Offset: 0x0003B5E2
	public void setKingdom(Kingdom pKingdom)
	{
		this.setKingdomToBoats(pKingdom, this.boats_transport);
		this.setKingdomToBoats(pKingdom, this.boats_fishing);
		this.setKingdomToBoats(pKingdom, this.boats_trading);
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x0003D40C File Offset: 0x0003B60C
	private void setKingdomToBoats(Kingdom pKingdom, List<Actor> pList)
	{
		for (int i = 0; i < pList.Count; i++)
		{
			pList[i].setKingdom(pKingdom);
		}
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x0003D438 File Offset: 0x0003B638
	public void addBoatToDock(Actor pBoat)
	{
		pBoat.setHomeBuilding(this.building);
		this.getList(pBoat.stats).Add(pBoat);
		pBoat.setCity(pBoat.homeBuilding.city);
		pBoat.callbacks_on_death.Add(new BaseActionActor(this.callbackUnitDied));
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x0003D48B File Offset: 0x0003B68B
	public void removeBoatFromDock(Actor pActor)
	{
		this.getList(pActor.stats).Remove(pActor);
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x0003D4A0 File Offset: 0x0003B6A0
	public void callbackUnitDied(Actor pActor)
	{
		this.removeBoatFromDock(pActor);
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x0003D4E4 File Offset: 0x0003B6E4
	// Note: this type is marked as 'beforefieldinit'.
	static Docks()
	{
		List<string> list = new List<string>();
		list.Add("boat_fishing");
		list.Add("boat_trading");
		list.Add("boat_transport");
		Docks.boat_types = list;
	}

	// Token: 0x0400068A RID: 1674
	internal int limit = 1;

	// Token: 0x0400068B RID: 1675
	internal List<Actor> boats_trading = new List<Actor>();

	// Token: 0x0400068C RID: 1676
	internal List<Actor> boats_transport = new List<Actor>();

	// Token: 0x0400068D RID: 1677
	internal List<Actor> boats_fishing = new List<Actor>();

	// Token: 0x0400068E RID: 1678
	public List<WorldTile> tiles_ocean = new List<WorldTile>();

	// Token: 0x0400068F RID: 1679
	public static List<string> boat_types;
}
