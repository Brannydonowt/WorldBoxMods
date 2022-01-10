using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200018B RID: 395
public class TileZone
{
	// Token: 0x0600091A RID: 2330 RVA: 0x00060B9C File Offset: 0x0005ED9C
	public TileZone()
	{
		this.buildingLists.Add(this.buildings_all);
		this.buildingLists.Add(this.buildings);
		this.buildingLists.Add(this.abandoned);
		this.buildingLists.Add(this.plants);
		this.buildingLists.Add(this.trees);
		this.buildingLists.Add(this.ruins);
		this.buildingLists.Add(this.stone);
		this.buildingLists.Add(this.gold);
		this.buildingLists.Add(this.ore);
		this.buildingLists.Add(this.food);
		this.buildingLists.Add(this.wheat);
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x00060D10 File Offset: 0x0005EF10
	public bool haveBuildingOf(City pCity)
	{
		foreach (Building building in this.buildings)
		{
			if (!building.isNonUsable() && building.city == pCity)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x00060D7C File Offset: 0x0005EF7C
	public void addTileType(TileTypeBase pType, WorldTile pTile)
	{
		HashSetWorldTile hashSetWorldTile = null;
		this.tileTypes.TryGetValue(pType, ref hashSetWorldTile);
		if (hashSetWorldTile == null)
		{
			this.tileTypes.Add(pType, hashSetWorldTile = new HashSetWorldTile());
			if (pType.canBeFarmField)
			{
				this.canBeFarms.Add(pType);
			}
		}
		hashSetWorldTile.Add(pTile);
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x00060DCC File Offset: 0x0005EFCC
	public void removeTileType(TileTypeBase pType, WorldTile pTile)
	{
		HashSetWorldTile hashSetWorldTile = null;
		this.tileTypes.TryGetValue(pType, ref hashSetWorldTile);
		if (hashSetWorldTile == null)
		{
			return;
		}
		hashSetWorldTile.Remove(pTile);
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x00060DF6 File Offset: 0x0005EFF6
	public HashSetWorldTile getTilesOfType(TileTypeBase pType)
	{
		if (!this.tileTypes.ContainsKey(pType))
		{
			return null;
		}
		return this.tileTypes[pType];
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x00060E14 File Offset: 0x0005F014
	public bool haveTiles(TileTypeBase pType)
	{
		return this.tileTypes.ContainsKey(pType) && this.tileTypes[pType].Count > 0;
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x00060E3C File Offset: 0x0005F03C
	internal int countCanBeFarms()
	{
		int num = 0;
		for (int i = 0; i < this.canBeFarms.Count; i++)
		{
			TileTypeBase tileTypeBase = this.canBeFarms[i];
			num += this.tileTypes[tileTypeBase].Count;
		}
		return num;
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x00060E84 File Offset: 0x0005F084
	internal void addTile(WorldTile pTile, int pX, int pY)
	{
		this.tiles.Add(pTile);
		if (pX == 0)
		{
			pTile.worldTileZoneBorder.borderLeft = true;
			pTile.worldTileZoneBorder.border = true;
		}
		else if (pX == 7)
		{
			pTile.worldTileZoneBorder.borderRight = true;
			pTile.worldTileZoneBorder.border = true;
		}
		if (pY == 0)
		{
			pTile.worldTileZoneBorder.borderDown = true;
			pTile.worldTileZoneBorder.border = true;
			return;
		}
		if (pY == 7)
		{
			pTile.worldTileZoneBorder.border = true;
			pTile.worldTileZoneBorder.borderUp = true;
		}
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x00060F0E File Offset: 0x0005F10E
	internal void setCity(City pCity)
	{
		this.city = pCity;
		if (this.city == null && this.culture != null)
		{
			this.culture.removeZone(this);
		}
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x00060F39 File Offset: 0x0005F139
	public void setCulture(Culture pCulture)
	{
		this.culture = pCulture;
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x00060F42 File Offset: 0x0005F142
	public void removeCulture()
	{
		this.culture = null;
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x00060F4C File Offset: 0x0005F14C
	internal void addBuilding(Building pBuilding)
	{
		HashSet<Building> hashSet;
		if (pBuilding.isRuin())
		{
			hashSet = this.ruins;
		}
		else if (pBuilding.data.state == BuildingState.CivAbandoned)
		{
			hashSet = this.abandoned;
		}
		else
		{
			switch (pBuilding.stats.buildingType)
			{
			case BuildingType.Tree:
				hashSet = this.trees;
				break;
			case BuildingType.Stone:
				hashSet = this.stone;
				break;
			case BuildingType.Ore:
				hashSet = this.ore;
				break;
			case BuildingType.Gold:
				hashSet = this.gold;
				break;
			case BuildingType.Fruits:
				hashSet = this.food;
				break;
			case BuildingType.Wheat:
				hashSet = this.wheat;
				break;
			case BuildingType.Plant:
				hashSet = this.plants;
				break;
			default:
				if (!pBuilding.isRuin())
				{
					hashSet = this.buildings;
				}
				else
				{
					hashSet = this.ruins;
				}
				break;
			}
		}
		hashSet.Add(pBuilding);
		this.buildings_all.Add(pBuilding);
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x00061028 File Offset: 0x0005F228
	internal void removeBuilding(Building pBuilding)
	{
		this.buildings_all.Add(pBuilding);
		this.abandoned.Remove(pBuilding);
		this.buildings.Remove(pBuilding);
		this.ruins.Remove(pBuilding);
		if (pBuilding.stats.buildingType != BuildingType.None)
		{
			switch (pBuilding.stats.buildingType)
			{
			case BuildingType.Tree:
				this.trees.Remove(pBuilding);
				return;
			case BuildingType.Stone:
				this.stone.Remove(pBuilding);
				return;
			case BuildingType.Ore:
				this.ore.Remove(pBuilding);
				return;
			case BuildingType.Gold:
				this.gold.Remove(pBuilding);
				return;
			case BuildingType.Fruits:
				this.food.Remove(pBuilding);
				return;
			case BuildingType.Wheat:
				this.wheat.Remove(pBuilding);
				break;
			case BuildingType.Plant:
				this.plants.Remove(pBuilding);
				return;
			default:
				return;
			}
		}
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x0006110C File Offset: 0x0005F30C
	internal void AddNeighbour(TileZone pNeighbour, TileDirection pDirection, bool pDiagonal = false)
	{
		if (pNeighbour == null)
		{
			this.world_edge = true;
			return;
		}
		if (!pDiagonal)
		{
			this.neighbours.Add(pNeighbour);
		}
		this.neighboursAll.Add(pNeighbour);
		switch (pDirection)
		{
		case TileDirection.Left:
			this.zone_left = pNeighbour;
			return;
		case TileDirection.Right:
			this.zone_right = pNeighbour;
			return;
		case TileDirection.Up:
			this.zone_up = pNeighbour;
			return;
		case TileDirection.Down:
			this.zone_down = pNeighbour;
			return;
		default:
			return;
		}
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x00061175 File Offset: 0x0005F375
	public bool canPlaceCityHere()
	{
		return !(this.city != null) && this.tilesWithGround >= 64;
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x00061194 File Offset: 0x0005F394
	public bool goodForExpansion()
	{
		return !this.haveTiles(TileLibrary.lava0) && !this.haveTiles(TileLibrary.lava1) && !this.haveTiles(TileLibrary.lava2) && !this.haveTiles(TileLibrary.lava3);
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x000611D4 File Offset: 0x0005F3D4
	public bool isInLimitFor(WorldTile pTile, BuildingAsset pAsset, int pLimit)
	{
		BuildingType buildingType = pAsset.buildingType;
		if (buildingType != BuildingType.Tree)
		{
			if (buildingType != BuildingType.Fruits)
			{
				if (buildingType == BuildingType.Plant)
				{
					if (this.plants.Count >= pLimit)
					{
						return true;
					}
				}
			}
			else if (this.food.Count >= pLimit)
			{
				return true;
			}
		}
		else
		{
			if (pTile.main_type.rankType == TileRank.Low)
			{
				pLimit = 1;
			}
			if (this.trees.Count >= pLimit)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x00061238 File Offset: 0x0005F438
	public void clear()
	{
		foreach (TileTypeBase tileTypeBase in this.tileTypes.Keys)
		{
			this.tileTypes[tileTypeBase].Clear();
		}
		foreach (HashSetBuilding hashSetBuilding in this.buildingLists)
		{
			hashSetBuilding.Clear();
		}
		this.city = null;
		this.culture = null;
		this.goodForNewCity = false;
		this.last_drawn_id = 0;
		this.last_drawn_hashcode = 0;
	}

	// Token: 0x04000B99 RID: 2969
	public int last_drawn_id;

	// Token: 0x04000B9A RID: 2970
	public int last_drawn_hashcode;

	// Token: 0x04000B9B RID: 2971
	public bool visible;

	// Token: 0x04000B9C RID: 2972
	public InfoText infoText;

	// Token: 0x04000B9D RID: 2973
	public int x;

	// Token: 0x04000B9E RID: 2974
	public int y;

	// Token: 0x04000B9F RID: 2975
	public int id;

	// Token: 0x04000BA0 RID: 2976
	public List<WorldTile> tiles = new List<WorldTile>(8);

	// Token: 0x04000BA1 RID: 2977
	public Color debug_zone_color;

	// Token: 0x04000BA2 RID: 2978
	public City city;

	// Token: 0x04000BA3 RID: 2979
	public MapChunk chunk;

	// Token: 0x04000BA4 RID: 2980
	public List<TileZone> neighbours;

	// Token: 0x04000BA5 RID: 2981
	public List<TileZone> neighboursAll;

	// Token: 0x04000BA6 RID: 2982
	public bool world_edge;

	// Token: 0x04000BA7 RID: 2983
	public WorldTile centerTile;

	// Token: 0x04000BA8 RID: 2984
	public int tilesOnFire;

	// Token: 0x04000BA9 RID: 2985
	public int tilesWithLiquid;

	// Token: 0x04000BAA RID: 2986
	public int tilesWithSoil;

	// Token: 0x04000BAB RID: 2987
	public int tilesWithGround;

	// Token: 0x04000BAC RID: 2988
	public HashSetBuilding buildings_all = new HashSetBuilding();

	// Token: 0x04000BAD RID: 2989
	public HashSetBuilding buildings = new HashSetBuilding();

	// Token: 0x04000BAE RID: 2990
	public HashSetBuilding abandoned = new HashSetBuilding();

	// Token: 0x04000BAF RID: 2991
	public HashSetBuilding trees = new HashSetBuilding();

	// Token: 0x04000BB0 RID: 2992
	public HashSetBuilding ruins = new HashSetBuilding();

	// Token: 0x04000BB1 RID: 2993
	public HashSetBuilding stone = new HashSetBuilding();

	// Token: 0x04000BB2 RID: 2994
	public HashSetBuilding gold = new HashSetBuilding();

	// Token: 0x04000BB3 RID: 2995
	public HashSetBuilding ore = new HashSetBuilding();

	// Token: 0x04000BB4 RID: 2996
	public HashSetBuilding food = new HashSetBuilding();

	// Token: 0x04000BB5 RID: 2997
	public HashSetBuilding wheat = new HashSetBuilding();

	// Token: 0x04000BB6 RID: 2998
	public HashSetBuilding plants = new HashSetBuilding();

	// Token: 0x04000BB7 RID: 2999
	private List<HashSetBuilding> buildingLists = new List<HashSetBuilding>();

	// Token: 0x04000BB8 RID: 3000
	internal TileZone zone_up;

	// Token: 0x04000BB9 RID: 3001
	internal TileZone zone_down;

	// Token: 0x04000BBA RID: 3002
	internal TileZone zone_left;

	// Token: 0x04000BBB RID: 3003
	internal TileZone zone_right;

	// Token: 0x04000BBC RID: 3004
	private Dictionary<TileTypeBase, HashSetWorldTile> tileTypes = new Dictionary<TileTypeBase, HashSetWorldTile>();

	// Token: 0x04000BBD RID: 3005
	public List<TileTypeBase> canBeFarms = new List<TileTypeBase>();

	// Token: 0x04000BBE RID: 3006
	public bool goodForNewCity;

	// Token: 0x04000BBF RID: 3007
	internal bool zoneChecked;

	// Token: 0x04000BC0 RID: 3008
	public Culture culture;
}
