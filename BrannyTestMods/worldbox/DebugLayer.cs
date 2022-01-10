using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000207 RID: 519
public class DebugLayer : MapLayer
{
	// Token: 0x06000B8D RID: 2957 RVA: 0x0006F638 File Offset: 0x0006D838
	protected override void UpdateDirty(float pElapsed)
	{
		if (!DebugConfig.instance.debugButton.gameObject.activeSelf)
		{
			this.clear();
			return;
		}
		if (this.timerDirtyChunksClear > 0f)
		{
			this.timerDirtyChunksClear -= pElapsed;
		}
		else
		{
			this.timerDirtyChunksClear = 1f;
			if (DebugLayer.dirtyChunks.Count > 0)
			{
				DebugLayer.dirtyChunks.Clear();
			}
		}
		this.color_active_path = new Color(1f, 1f, 1f, 0.5f);
		this.used = false;
		this.clear();
		if (DebugConfig.isOn(DebugOption.CityZones))
		{
			this.drawZones();
		}
		else if (DebugConfig.isOn(DebugOption.Chunks))
		{
			this.drawChunks();
		}
		if (DebugConfig.isOn(DebugOption.ChunksDirty))
		{
			this.drawDirtyChunks();
		}
		if (DebugConfig.isOn(DebugOption.PathRegions))
		{
			this.drawPathRegions();
		}
		if (DebugConfig.isOn(DebugOption.ActivePaths))
		{
			this.drawActivePaths();
		}
		if (DebugConfig.isOn(DebugOption.CityPlaces))
		{
			this.drawCityPlaces();
		}
		if (DebugConfig.isOn(DebugOption.Buildings))
		{
			this.drawBuildings();
		}
		if (DebugConfig.isOn(DebugOption.ConstructionTiles))
		{
			this.drawConstructionTiles();
		}
		if (DebugConfig.isOn(DebugOption.UnitsInside))
		{
			this.drawUnitsInside();
		}
		if (DebugConfig.isOn(DebugOption.TargetedBy))
		{
			this.drawTargetedBy();
		}
		if (DebugConfig.isOn(DebugOption.UnitKingdoms))
		{
			this.drawUnitKingdoms();
		}
		if (DebugConfig.isOn(DebugOption.DisplayUnitTiles))
		{
			this.drawUnitTiles();
		}
		if (DebugConfig.isOn(DebugOption.JobCleaner))
		{
			this.drawJobs("cleaner");
		}
		if (DebugConfig.isOn(DebugOption.JobFarmer))
		{
			this.drawJobs("farmer");
		}
		if (DebugConfig.isOn(DebugOption.JobMiner))
		{
			this.drawJobs("miner");
		}
		if (DebugConfig.isOn(DebugOption.JobGatherer))
		{
			this.drawJobs("gatherer");
		}
		if (DebugConfig.isOn(DebugOption.JobHunter))
		{
			this.drawJobs("hunter");
		}
		if (DebugConfig.isOn(DebugOption.JobMetalWorker))
		{
			this.drawJobs("metalworker");
		}
		if (DebugConfig.isOn(DebugOption.JobBlacksmith))
		{
			this.drawJobs("blacksmith");
		}
		if (DebugConfig.isOn(DebugOption.JobFireman))
		{
			this.drawJobs("fireman");
		}
		if (DebugConfig.isOn(DebugOption.JobBuilder))
		{
			this.drawJobs("builder");
		}
		if (DebugConfig.isOn(DebugOption.JobAttacker))
		{
			this.drawJobs("attacker");
		}
		if (DebugConfig.isOn(DebugOption.JobSettler))
		{
			this.drawJobs("settler");
		}
		if (DebugConfig.isOn(DebugOption.JobDefender))
		{
			this.drawJobs("defender");
		}
		if (DebugConfig.isOn(DebugOption.ProKing))
		{
			this.drawProfession(UnitProfession.King);
		}
		if (DebugConfig.isOn(DebugOption.ProLeader))
		{
			this.drawProfession(UnitProfession.Leader);
		}
		if (DebugConfig.isOn(DebugOption.ProUnit))
		{
			this.drawProfession(UnitProfession.Unit);
		}
		if (DebugConfig.isOn(DebugOption.ProBaby))
		{
			this.drawProfession(UnitProfession.Baby);
		}
		if (DebugConfig.isOn(DebugOption.ProWarrior))
		{
			this.drawProfession(UnitProfession.Warrior);
		}
		if (this.used)
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			base.updatePixels();
			return;
		}
		if (base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x0006F904 File Offset: 0x0006DB04
	private void drawUnitKingdoms()
	{
		this.used = true;
		foreach (Actor actor in this.world.units)
		{
			if (actor.kingdom != null && actor.kingdom.kingdomColor != null)
			{
				Color colorBorderInside = actor.kingdom.kingdomColor.colorBorderInside;
				this.pixels[actor.currentTile.data.tile_id] = colorBorderInside;
				this.tiles.Add(actor.currentTile);
			}
		}
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x0006F9B0 File Offset: 0x0006DBB0
	private void drawUnitTiles()
	{
		this.used = true;
		foreach (WorldTile worldTile in this.world.tilesList)
		{
			if (worldTile.units.Count != 0)
			{
				this.pixels[worldTile.data.tile_id] = Color.blue;
				this.tiles.Add(worldTile);
			}
		}
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x0006FA44 File Offset: 0x0006DC44
	private void drawTargetedBy()
	{
		this.used = true;
		foreach (WorldTile worldTile in this.world.tilesList)
		{
			if (worldTile.targetedBy != null)
			{
				this.pixels[worldTile.data.tile_id] = Color.blue;
				this.tiles.Add(worldTile);
			}
		}
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x0006FAD8 File Offset: 0x0006DCD8
	private void drawProfession(UnitProfession pPro)
	{
		this.used = true;
		foreach (Actor actor in this.world.units)
		{
			if (actor.isProfession(pPro))
			{
				Color blue = Color.blue;
				this.pixels[actor.currentTile.data.tile_id] = blue;
				this.tiles.Add(actor.currentTile);
			}
		}
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x0006FB6C File Offset: 0x0006DD6C
	private void drawJobs(string pID)
	{
		this.used = true;
		foreach (Actor actor in this.world.units)
		{
			if (actor.ai.job != null && !(pID != actor.ai.job.id))
			{
				Color red = Color.red;
				this.pixels[actor.currentTile.data.tile_id] = red;
				this.tiles.Add(actor.currentTile);
			}
		}
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x0006FC1C File Offset: 0x0006DE1C
	private void drawUnitsInside()
	{
		this.used = true;
		foreach (Actor actor in this.world.units)
		{
			if (actor.insideBuilding != null)
			{
				this.pixels[actor.currentTile.data.tile_id] = Color.green;
				this.tiles.Add(actor.currentTile);
			}
		}
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x0006FCB4 File Offset: 0x0006DEB4
	private void drawConstructionTiles()
	{
		this.used = true;
		foreach (WorldTile worldTile in this.world.tilesList)
		{
			if (worldTile.building != null && worldTile.building.stats.docks)
			{
				worldTile.building.getConstructionTile();
				foreach (WorldTile worldTile2 in Toolbox.temp_list_tiles)
				{
					this.pixels[worldTile2.data.tile_id] = Color.red;
					this.tiles.Add(worldTile2);
				}
			}
		}
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x0006FDA4 File Offset: 0x0006DFA4
	private void drawBuildings()
	{
		this.used = true;
		foreach (WorldTile worldTile in this.world.tilesList)
		{
			if (worldTile.building != null)
			{
				if (worldTile.building.kingdom != null && worldTile.building.kingdom.isCiv())
				{
					this.pixels[worldTile.data.tile_id] = worldTile.building.kingdom.kingdomColor.colorBorderInside;
				}
				else
				{
					this.pixels[worldTile.data.tile_id] = Color.red;
				}
				this.tiles.Add(worldTile);
			}
		}
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x0006FE90 File Offset: 0x0006E090
	private void drawCityPlaces()
	{
		this.used = true;
		foreach (TileZone tileZone in this.world.zoneCalculator.zones)
		{
			if (tileZone.city != null)
			{
				this.fill(tileZone.tiles, Color.yellow, false);
			}
			else if (tileZone.goodForNewCity)
			{
				this.fill(tileZone.tiles, Color.blue, false);
			}
			else if (tileZone.zoneChecked)
			{
				this.fill(tileZone.tiles, Color.red, false);
			}
		}
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x0006FF48 File Offset: 0x0006E148
	private void drawActivePaths()
	{
		this.used = true;
		foreach (Actor actor in this.world.units)
		{
			if (actor.current_path_global != null)
			{
				foreach (MapRegion mapRegion in actor.current_path_global)
				{
					this.fill(mapRegion.tiles, this.color_active_path, false);
				}
				this.fill(actor.current_path, Color.blue, false);
			}
		}
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x00070004 File Offset: 0x0006E204
	private void drawPathRegions()
	{
		this.used = true;
		foreach (MapChunk mapChunk in this.world.mapChunkManager.list)
		{
			foreach (MapRegion mapRegion in mapChunk.regions)
			{
				if (mapRegion.path_wave_id != -1)
				{
					this.fill(mapRegion.tiles, new Color(1f, 1f, 0f, 0.9f), false);
				}
			}
		}
		List<MapRegion> last_globalPath = this.world.regionPathFinder.last_globalPath;
		if (last_globalPath != null && last_globalPath.Count > 0)
		{
			RegionPathFinder regionPathFinder = this.world.regionPathFinder;
			bool flag;
			if (regionPathFinder == null)
			{
				flag = (null != null);
			}
			else
			{
				WorldTile tileStart = regionPathFinder.tileStart;
				flag = (((tileStart != null) ? tileStart.region : null) != null);
			}
			if (flag)
			{
				RegionPathFinder regionPathFinder2 = this.world.regionPathFinder;
				bool flag2;
				if (regionPathFinder2 == null)
				{
					flag2 = (null != null);
				}
				else
				{
					WorldTile tileTarget = regionPathFinder2.tileTarget;
					flag2 = (((tileTarget != null) ? tileTarget.region : null) != null);
				}
				if (flag2)
				{
					foreach (MapRegion mapRegion2 in this.world.regionPathFinder.last_globalPath)
					{
						this.fill(mapRegion2.tiles, Color.blue, false);
					}
					this.fill(this.world.regionPathFinder.tileStart.region.tiles, Color.green, false);
					this.fill(this.world.regionPathFinder.tileTarget.region.tiles, new Color(1f, 0f, 0f, 0.3f), false);
				}
			}
		}
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x000701F4 File Offset: 0x0006E3F4
	private void drawDirtyChunks()
	{
		this.used = true;
		foreach (MapChunk mapChunk in DebugLayer.dirtyChunks)
		{
			this.fill(mapChunk.tiles, this.color_red, false);
		}
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x0007025C File Offset: 0x0006E45C
	private void fill(List<WorldTile> pTiles, Color pColor, bool pEdge = false)
	{
		foreach (WorldTile worldTile in pTiles)
		{
			if (!pEdge || worldTile.region != null)
			{
				this.tiles.Add(worldTile);
				this.pixels[worldTile.data.tile_id] = pColor;
			}
		}
	}

	// Token: 0x06000B9B RID: 2971 RVA: 0x000702D8 File Offset: 0x0006E4D8
	private void drawZones()
	{
		this.used = true;
		foreach (TileZone tileZone in this.world.zoneCalculator.zones)
		{
			if ((tileZone.x + tileZone.y) % 2 == 0)
			{
				tileZone.debug_zone_color = this.color1;
			}
			else
			{
				tileZone.debug_zone_color = this.color2;
			}
			this.fill(tileZone.tiles, tileZone.debug_zone_color, false);
		}
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x00070374 File Offset: 0x0006E574
	private void drawChunks()
	{
		this.used = true;
		foreach (MapChunk mapChunk in this.world.mapChunkManager.list)
		{
			this.fill(mapChunk.tiles, mapChunk.color, false);
		}
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x000703E4 File Offset: 0x0006E5E4
	internal override void clear()
	{
		if (this.tiles.Count == 0)
		{
			return;
		}
		foreach (WorldTile worldTile in this.tiles)
		{
			if (worldTile.data.tile_id <= this.pixels.Length - 1)
			{
				this.pixels[worldTile.data.tile_id] = Color.clear;
			}
		}
		this.tiles.Clear();
		base.createTextureNew();
	}

	// Token: 0x04000DBE RID: 3518
	internal static List<MapChunk> dirtyChunks = new List<MapChunk>();

	// Token: 0x04000DBF RID: 3519
	internal List<WorldTile> tiles = new List<WorldTile>();

	// Token: 0x04000DC0 RID: 3520
	public Color color1 = Color.gray;

	// Token: 0x04000DC1 RID: 3521
	public Color color2 = Color.white;

	// Token: 0x04000DC2 RID: 3522
	public Color color_red = Color.red;

	// Token: 0x04000DC3 RID: 3523
	public Color color_active_path;

	// Token: 0x04000DC4 RID: 3524
	private bool used;

	// Token: 0x04000DC5 RID: 3525
	private float timerDirtyChunksClear;
}
