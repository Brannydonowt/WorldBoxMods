using System;
using System.Collections.Generic;
using sfx;

// Token: 0x020000AA RID: 170
public class UnitZones
{
	// Token: 0x0600036E RID: 878 RVA: 0x00036F02 File Offset: 0x00035102
	public UnitZones(MapBox pWorld)
	{
		this.world = pWorld;
	}

	// Token: 0x0600036F RID: 879 RVA: 0x00036F32 File Offset: 0x00035132
	internal void update()
	{
		if (this.timer > 0f)
		{
			this.timer -= this.world.deltaTime;
			return;
		}
		this.timer = this.interval;
		this.recalc();
	}

	// Token: 0x06000370 RID: 880 RVA: 0x00036F6C File Offset: 0x0003516C
	internal void clear()
	{
		MusicMan.clear();
		for (int i = 0; i < this.world.citiesList.Count; i++)
		{
			this.world.citiesList[i].clearCurrentCaptureAmounts();
		}
		for (int j = 0; j < this.tiles.Count; j++)
		{
			this.tiles[j].clearUnits();
		}
		for (int k = 0; k < this.world.islandsCalculator.islands.Count; k++)
		{
			this.world.islandsCalculator.islands[k].docks.Clear();
		}
		for (int l = 0; l < this.chunks.Count; l++)
		{
			this.chunks[l].clearObjects();
		}
		this.chunks.Clear();
		this.tiles.Clear();
	}

	// Token: 0x06000371 RID: 881 RVA: 0x00037054 File Offset: 0x00035254
	private void addUnit(Actor pActor, WorldTile pTile)
	{
		if (pTile.units.Count == 0)
		{
			this.tiles.Add(pTile);
		}
		pTile.units.Add(pActor);
		if (pActor.professionAsset.can_capture && pTile.zone.city != null && !pActor.isInsideSomething())
		{
			pTile.zone.city.updateConquest(pActor);
		}
	}

	// Token: 0x06000372 RID: 882 RVA: 0x000370C0 File Offset: 0x000352C0
	private void recalc()
	{
		this.clear();
		List<Actor> simpleList = this.world.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Actor actor = simpleList[i];
			if (!(actor == null) && actor.base_data.alive && actor.kingdom != null)
			{
				MapChunk chunk = actor.currentTile.chunk;
				if (chunk.k_list_objects.Count == 0)
				{
					this.chunks.Add(chunk);
				}
				this.addUnit(actor, actor.currentTile);
				chunk.addObject(actor);
			}
		}
		List<Building> simpleList2 = this.world.buildings.getSimpleList();
		for (int j = 0; j < simpleList2.Count; j++)
		{
			Building building = simpleList2[j];
			if (!(building == null) && building.base_data.alive)
			{
				if (building._is_visible)
				{
					MusicMan.count(building);
				}
				if (building.stats.docks && building.GetComponent<Docks>().tiles_ocean.Count > 0)
				{
					building.GetComponent<Docks>().tiles_ocean[0].region.island.docks.Add(building.GetComponent<Docks>());
				}
				MapChunk chunk2 = building.currentTile.chunk;
				if (chunk2.k_list_objects.Count == 0)
				{
					this.chunks.Add(chunk2);
				}
				chunk2.addObject(building);
				if (building.stats.tower && building.city != null)
				{
					building.city.addCapturePoints(building, 10);
				}
			}
		}
		MusicMan.finishCount();
	}

	// Token: 0x040005C7 RID: 1479
	private float timer;

	// Token: 0x040005C8 RID: 1480
	private float interval = 1f;

	// Token: 0x040005C9 RID: 1481
	private MapBox world;

	// Token: 0x040005CA RID: 1482
	private List<MapChunk> chunks = new List<MapChunk>();

	// Token: 0x040005CB RID: 1483
	private List<WorldTile> tiles = new List<WorldTile>();
}
