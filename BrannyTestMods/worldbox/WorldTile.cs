using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Token: 0x020002F0 RID: 752
public class WorldTile : Atom
{
	// Token: 0x060010F1 RID: 4337 RVA: 0x00094F40 File Offset: 0x00093140
	public WorldTile(int pX, int pY, int pTileID, MapBox pMapBox)
	{
		this.lastTile = WorldTilemap.lastTileEmpty;
		this.world = pMapBox;
		this.data.tile_id = pTileID;
		this.neighbours = new List<WorldTile>(4);
		this.neighboursAll = new List<WorldTile>(8);
		this.pos = new Vector2Int(pX, pY);
		this.posV3 = new Vector3((float)pX, (float)pY);
		this.posV3.x = this.posV3.x + ActorBase.spriteOffset.x;
		this.posV3.y = this.posV3.y + ActorBase.spriteOffset.y;
		this.x = pX;
		this.y = pY;
	}

	// Token: 0x060010F2 RID: 4338 RVA: 0x0009502C File Offset: 0x0009322C
	public void pollinate()
	{
		this.pollinated++;
		if (this.pollinated > 5)
		{
			this.growFlowers();
			this.pollinated = 0;
		}
	}

	// Token: 0x060010F3 RID: 4339 RVA: 0x00095054 File Offset: 0x00093254
	private void growFlowers()
	{
		WorldTile random = Toolbox.getRandomChunkFromTile(this).tiles.GetRandom<WorldTile>();
		if (random.Type.grow_type_selector_plants != null)
		{
			MapBox.instance.tryGrowVegetationRandom(random, VegetationType.Plants, false, true);
		}
	}

	// Token: 0x060010F4 RID: 4340 RVA: 0x00095090 File Offset: 0x00093290
	public bool canBuildOn(BuildingAsset pNewTemplate)
	{
		return (!this.Type.liquid || pNewTemplate.canBePlacedOnLiquid) && (!pNewTemplate.burnable || !this.data.fire) && (!pNewTemplate.affectedByLava || !this.Type.lava) && (pNewTemplate.canBePlacedOnBlocks || !this.Type.block) && ((pNewTemplate.buildingType == BuildingType.Tree && this.building != null && this.building.stats.buildingType == BuildingType.Plant) || ((pNewTemplate.ignoreBuildings || !(this.building != null) || this.building.isNonUsable() || !(this.building.city == null) || this.building.stats.ignoredByCities) && (pNewTemplate.ignoreBuildings || !(this.building != null) || !this.building.stats.cityBuilding || this.building.isNonUsable() || this.building.stats.priority < pNewTemplate.priority)));
	}

	// Token: 0x060010F5 RID: 4341 RVA: 0x000951BF File Offset: 0x000933BF
	public void setRoad()
	{
		this.world.roadsCalculator.setDirty(this);
	}

	// Token: 0x060010F6 RID: 4342 RVA: 0x000951D2 File Offset: 0x000933D2
	public bool isSameIsland(WorldTile pTile)
	{
		if (pTile.region == null || this.region == null)
		{
			return pTile.region == this.region;
		}
		return pTile.region.island == this.region.island;
	}

	// Token: 0x060010F7 RID: 4343 RVA: 0x0009520E File Offset: 0x0009340E
	public Color32 getColor()
	{
		return this.Type.color;
	}

	// Token: 0x060010F8 RID: 4344 RVA: 0x0009521C File Offset: 0x0009341C
	internal void AddNeighbour(WorldTile pNeighbour, TileDirection pDirection, bool pDiagonal = false)
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
			this.tile_left = pNeighbour;
			return;
		case TileDirection.Right:
			this.tile_right = pNeighbour;
			return;
		case TileDirection.Up:
			this.tile_up = pNeighbour;
			return;
		case TileDirection.Down:
			this.tile_down = pNeighbour;
			return;
		default:
			return;
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x060010F9 RID: 4345 RVA: 0x00095285 File Offset: 0x00093485
	public TileTypeBase Type
	{
		get
		{
			return this.cur_tile_type;
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x060010FA RID: 4346 RVA: 0x0009528D File Offset: 0x0009348D
	// (set) Token: 0x060010FB RID: 4347 RVA: 0x0009529C File Offset: 0x0009349C
	public int Height
	{
		get
		{
			return this.data.height;
		}
		set
		{
			this.data.height = value;
			if (this.data.height < 0)
			{
				this.data.height = 0;
				return;
			}
			if (this.data.height > 255)
			{
				this.data.height = 255;
			}
		}
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x000952F4 File Offset: 0x000934F4
	internal bool IsOceanAround()
	{
		for (int i = 0; i < this.neighbours.Count; i++)
		{
			if (this.neighbours[i].Type.layerType == TileLayerType.Ocean)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x00095333 File Offset: 0x00093533
	internal bool isGoodForBoat()
	{
		return this.Type.layerType == TileLayerType.Ocean;
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x00095344 File Offset: 0x00093544
	internal bool IsTypeAround(TileTypeBase pType)
	{
		for (int i = 0; i < this.neighbours.Count; i++)
		{
			if (this.neighbours[i].Type == pType)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060010FF RID: 4351 RVA: 0x00095380 File Offset: 0x00093580
	internal bool setFire(bool pForce = false)
	{
		if (this.Type.explodable)
		{
			this.world.explosionLayer.explodeBomb(this, false);
		}
		if (!pForce && (!this.Type.burnable || this.data.fire))
		{
			return false;
		}
		if (this.Type.liquid)
		{
			return false;
		}
		bool flag = false;
		if (this.building != null && this.building.isBurnable())
		{
			ActionLibrary.addBurningEffectOnTarget(this.building, null);
			flag = true;
		}
		if (this.Type.burnable || flag || pForce)
		{
			flag = true;
			if (this.Type.IsType("fireworks"))
			{
				this.world.stackEffects.spawnFireworks(this, 0.05f);
			}
			this.data.fire_stage = Random.Range(0, 5);
			if (!this.data.fire)
			{
				this.zone.tilesOnFire++;
			}
			if (this.Type.burnable)
			{
				MapAction.decreaseTile(this, "flash");
			}
			this.data.fire = true;
			this.world.setTileDirty(this, false);
			this.world.fireLayer.setTileDirty(this);
		}
		return flag;
	}

	// Token: 0x06001100 RID: 4352 RVA: 0x000954B9 File Offset: 0x000936B9
	public void updateStats()
	{
		if (this.top_type != null)
		{
			this.cur_tile_type = this.top_type;
			return;
		}
		this.cur_tile_type = this.main_type;
	}

	// Token: 0x06001101 RID: 4353 RVA: 0x000954DC File Offset: 0x000936DC
	public void setTopTileType(TopTileType pTopTile)
	{
		if (this.top_type != pTopTile)
		{
			if (this.top_type != null)
			{
				this.zone.removeTileType(this.top_type, this);
			}
			if (pTopTile != null)
			{
				this.zone.addTileType(pTopTile, this);
			}
		}
		if (this.top_type != null)
		{
			this.top_type.hashsetRemove(this);
		}
		this.top_type = pTopTile;
		if (this.top_type != null)
		{
			this.top_type.hashsetAdd(this);
		}
		this.world.setTileDirty(this, false);
		this.updateStats();
	}

	// Token: 0x06001102 RID: 4354 RVA: 0x00095560 File Offset: 0x00093760
	public void setTileType(TileType pType)
	{
		if (this.main_type != pType)
		{
			if (this.main_type != null)
			{
				this.zone.removeTileType(this.main_type, this);
			}
			this.zone.addTileType(pType, this);
		}
		this.health = WorldTile.DEFAULT_HEALTH;
		if (this.zone != null)
		{
			if (this.main_type == null)
			{
				if (pType.liquid)
				{
					this.zone.tilesWithLiquid++;
				}
				if (pType.ground)
				{
					this.zone.tilesWithGround++;
				}
			}
			else
			{
				if (!this.main_type.liquid && pType.liquid)
				{
					this.zone.tilesWithLiquid++;
				}
				else if (this.main_type.liquid && !pType.liquid)
				{
					this.zone.tilesWithLiquid--;
				}
				if (!this.main_type.ground && pType.ground)
				{
					this.zone.tilesWithGround++;
				}
				else if (this.main_type.ground && !pType.ground)
				{
					this.zone.tilesWithGround--;
				}
			}
		}
		if (this.main_type != null)
		{
			this.main_type.hashsetRemove(this);
		}
		this.main_type = pType;
		this.main_type.hashsetAdd(this);
		this.world.setTileDirty(this, false);
		this.updateStats();
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x000956D8 File Offset: 0x000938D8
	public void setTileType(string pType)
	{
		TileType tileType = AssetManager.tiles.get(pType);
		if (tileType == null)
		{
			tileType = TileLibrary.soil_low;
		}
		this.setTileType(tileType);
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x00095701 File Offset: 0x00093901
	public void setBurned(int pForceVal = -1)
	{
		if (!this.Type.canBeSetOnFire)
		{
			return;
		}
		if (pForceVal == -1)
		{
			this.burned_stages = 15 - Random.Range(0, 10);
		}
		else
		{
			this.burned_stages = pForceVal;
		}
		this.world.burnedLayer.setTileDirty(this);
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x00095740 File Offset: 0x00093940
	public void removeBurn()
	{
		if (this.burned_stages == 0)
		{
			return;
		}
		this.burned_stages = 0;
		this.world.burnedLayer.setTileDirty(this);
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x00095764 File Offset: 0x00093964
	internal void stopFire(bool pByItself = false)
	{
		if (!this.data.fire)
		{
			return;
		}
		if (this.data.fire)
		{
			this.zone.tilesOnFire--;
		}
		this.data.fire = false;
		this.data.fire_stage = 0;
		this.world.setTileDirty(this, false);
		this.setBurned(-1);
		this.world.fireLayer.setTileDirty(this);
		if (pByItself)
		{
			this.grassTicksBeforeGrowth = 5;
		}
	}

	// Token: 0x06001107 RID: 4359 RVA: 0x000957E6 File Offset: 0x000939E6
	internal bool canGrow()
	{
		return !this.data.fire && this.burned_stages == 0;
	}

	// Token: 0x06001108 RID: 4360 RVA: 0x00095800 File Offset: 0x00093A00
	public void removeTrees(bool pFlash = true)
	{
		if (pFlash)
		{
			this.world.flashEffects.flashPixel(this, 20, ColorType.White);
		}
		this.world.setTileDirty(this, false);
	}

	// Token: 0x06001109 RID: 4361 RVA: 0x00095826 File Offset: 0x00093A26
	public void removeGrass(bool pFlash = true)
	{
		if (pFlash)
		{
			this.world.flashEffects.flashPixel(this, 20, ColorType.White);
		}
		MapAction.removeGreens(this);
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x00095845 File Offset: 0x00093A45
	public void eatGreens(int pTicks = 5)
	{
		this.removeGrass(true);
		this.grassTicksBeforeGrowth = pTicks;
	}

	// Token: 0x0600110B RID: 4363 RVA: 0x00095855 File Offset: 0x00093A55
	internal void clearUnits()
	{
		this.units.Clear();
	}

	// Token: 0x0600110C RID: 4364 RVA: 0x00095864 File Offset: 0x00093A64
	internal void clear()
	{
		this.buildingX = 0;
		this.buildingY = 0;
		this.units.Clear();
		this.pollinated = 0;
		this.setTileType(TileLibrary.deep_ocean);
		this.setTopTileType(null);
		this.dirty = false;
		this.needRedraw = false;
		this.delayedTimerBomb = -1f;
		this.Height = 0;
		this.curGraphics = null;
		this.heat = 0;
		this.lavaTick = 0;
		this.flash_state = 0;
		this.burned_stages = 0;
		this.building = null;
		this.data.conwayType = ConwayType.None;
		this.data.fire = false;
		this.data.fire_stage = 0;
		this.explosionFxStage = 0;
		this.region = null;
		this.edge_region_right = null;
		this.edge_region_up = null;
		this.lastTile = WorldTilemap.lastTileEmpty;
		this.lastBorderTile = WorldTilemap.lastTileEmpty;
		this.worldTileZoneBorder.reset();
	}

	// Token: 0x0600110D RID: 4365 RVA: 0x0009594E File Offset: 0x00093B4E
	public override int GetHashCode()
	{
		return this.data.tile_id;
	}

	// Token: 0x0600110E RID: 4366 RVA: 0x0009595B File Offset: 0x00093B5B
	public override bool Equals(object obj)
	{
		return this.Equals(obj as WorldTile);
	}

	// Token: 0x0600110F RID: 4367 RVA: 0x00095969 File Offset: 0x00093B69
	public bool Equals(WorldTile pTile)
	{
		return this.data.tile_id == pTile.data.tile_id;
	}

	// Token: 0x0400140C RID: 5132
	public TopTileType top_type;

	// Token: 0x0400140D RID: 5133
	public TileType main_type;

	// Token: 0x0400140E RID: 5134
	private TileTypeBase cur_tile_type;

	// Token: 0x0400140F RID: 5135
	public bool clicked;

	// Token: 0x04001410 RID: 5136
	internal Tile curGraphics;

	// Token: 0x04001411 RID: 5137
	public int burned_stages;

	// Token: 0x04001412 RID: 5138
	internal WorldTileZoneBorder worldTileZoneBorder = new WorldTileZoneBorder();

	// Token: 0x04001413 RID: 5139
	public static int DEFAULT_HEALTH = 10;

	// Token: 0x04001414 RID: 5140
	public int health = 10;

	// Token: 0x04001415 RID: 5141
	public Vector3Int lastBorderTile;

	// Token: 0x04001416 RID: 5142
	public Vector3Int lastBorderOcean;

	// Token: 0x04001417 RID: 5143
	public Vector3Int lastTile;

	// Token: 0x04001418 RID: 5144
	public TileTypeBase lastDrawnType;

	// Token: 0x04001419 RID: 5145
	public float delayedTimerBomb;

	// Token: 0x0400141A RID: 5146
	public string delayedBombType = "";

	// Token: 0x0400141B RID: 5147
	public int lavaTick;

	// Token: 0x0400141C RID: 5148
	public WorldTileData data = new WorldTileData();

	// Token: 0x0400141D RID: 5149
	public int heat;

	// Token: 0x0400141E RID: 5150
	public int explosionWave;

	// Token: 0x0400141F RID: 5151
	public int explosionPower;

	// Token: 0x04001420 RID: 5152
	public int grassTicksBeforeGrowth;

	// Token: 0x04001421 RID: 5153
	public ActorBase targetedBy;

	// Token: 0x04001422 RID: 5154
	public bool world_edge;

	// Token: 0x04001423 RID: 5155
	public WorldTile tile_up;

	// Token: 0x04001424 RID: 5156
	public WorldTile tile_down;

	// Token: 0x04001425 RID: 5157
	public WorldTile tile_left;

	// Token: 0x04001426 RID: 5158
	public WorldTile tile_right;

	// Token: 0x04001427 RID: 5159
	public List<WorldTile> neighbours;

	// Token: 0x04001428 RID: 5160
	public List<WorldTile> neighboursAll;

	// Token: 0x04001429 RID: 5161
	public TileIsland roadIsland;

	// Token: 0x0400142A RID: 5162
	public int pollinated;

	// Token: 0x0400142B RID: 5163
	internal bool dirty;

	// Token: 0x0400142C RID: 5164
	internal bool needRedraw;

	// Token: 0x0400142D RID: 5165
	public readonly int x;

	// Token: 0x0400142E RID: 5166
	public readonly int y;

	// Token: 0x0400142F RID: 5167
	public readonly Vector2Int pos;

	// Token: 0x04001430 RID: 5168
	public readonly Vector3 posV3;

	// Token: 0x04001431 RID: 5169
	internal int buildingX;

	// Token: 0x04001432 RID: 5170
	internal int buildingY;

	// Token: 0x04001433 RID: 5171
	internal int flash_state;

	// Token: 0x04001434 RID: 5172
	internal ColorArray colorArray;

	// Token: 0x04001435 RID: 5173
	internal MapRegion edge_region_right;

	// Token: 0x04001436 RID: 5174
	internal MapRegion edge_region_up;

	// Token: 0x04001437 RID: 5175
	public MapRegion region;

	// Token: 0x04001438 RID: 5176
	public TileZone zone;

	// Token: 0x04001439 RID: 5177
	public MapChunk chunk;

	// Token: 0x0400143A RID: 5178
	public bool chunkEdge;

	// Token: 0x0400143B RID: 5179
	public Building building;

	// Token: 0x0400143C RID: 5180
	public List<Actor> units = new List<Actor>();

	// Token: 0x0400143D RID: 5181
	private readonly MapBox world;

	// Token: 0x0400143E RID: 5182
	internal int explosionFxStage;

	// Token: 0x0400143F RID: 5183
	internal bool dirtyIslandTile;

	// Token: 0x04001440 RID: 5184
	internal bool checkedTile;

	// Token: 0x04001441 RID: 5185
	internal int score = -1;

	// Token: 0x04001442 RID: 5186
	internal readonly Color debugColor = Color.clear;
}
