using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000208 RID: 520
public class DebugLayerCursor : MapLayer
{
	// Token: 0x06000BA0 RID: 2976 RVA: 0x000704C8 File Offset: 0x0006E6C8
	internal override void create()
	{
		base.create();
		this.color_highlight_white = Toolbox.makeColor("#FFFFFF77");
		this.color_main = new Color(0f, 1f, 0f, 0.1f);
		this.color_neighbour = new Color(1f, 0f, 1f, 0.5f);
		this.color_edges = new Color(1f, 0f, 0f, 0.5f);
		this.color_edges_blink = new Color(1f, 0f, 0f, 0.8f);
		this.color_region = new Color(0f, 0f, 1f, 0.8f);
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x00070588 File Offset: 0x0006E788
	protected override void UpdateDirty(float pElapsed)
	{
		if (ScrollWindow.isWindowActive())
		{
			return;
		}
		if (!Config.isEditor || !DebugConfig.instance.debugButton.gameObject.activeSelf)
		{
			this.clear();
			return;
		}
		if (this.timerBlink > 0f)
		{
			this.timerBlink -= pElapsed;
		}
		else
		{
			this.timerBlink = 0.2f;
			this.blink = !this.blink;
		}
		if (this.timerRecalc <= 0f)
		{
			this.timerRecalc = 0.1f;
			return;
		}
		this.timerRecalc -= pElapsed;
		this.clear();
		WorldTile mouseTilePos = this.world.getMouseTilePos();
		if (mouseTilePos == null)
		{
			return;
		}
		this.lastChunk = mouseTilePos.chunk;
		MapChunk chunk = mouseTilePos.chunk;
		MapChunk mapChunk = this.lastChunk;
		if (DebugConfig.isOn(DebugOption.Islands))
		{
			bool flag;
			if (mouseTilePos == null)
			{
				flag = (null != null);
			}
			else
			{
				MapRegion region = mouseTilePos.region;
				flag = (((region != null) ? region.island : null) != null);
			}
			if (flag)
			{
				this.drawIsland(mouseTilePos.region.island);
			}
		}
		if (DebugConfig.isOn(DebugOption.CursorChunk))
		{
			this.fill(this.lastChunk.tiles, this.color_highlight_white, false);
		}
		if (DebugConfig.isOn(DebugOption.Region) && mouseTilePos.region != null)
		{
			this.fill(mouseTilePos.region.tiles, this.color_region, false);
		}
		if (DebugConfig.isOn(DebugOption.RenderConnectedIslands))
		{
			bool flag2;
			if (mouseTilePos == null)
			{
				flag2 = (null != null);
			}
			else
			{
				MapRegion region2 = mouseTilePos.region;
				flag2 = (((region2 != null) ? region2.island : null) != null);
			}
			if (flag2)
			{
				TileIsland island = mouseTilePos.region.island;
				foreach (TileIsland tileIsland in this.world.islandsCalculator.islands)
				{
					if (island != tileIsland && island.connectedWith(tileIsland))
					{
						foreach (MapRegion mapRegion in tileIsland.regions)
						{
							this.fill(mapRegion.tiles, Color.blue, false);
						}
					}
				}
			}
		}
		if (DebugConfig.isOn(DebugOption.RenderIslandsTileCorners))
		{
			bool flag3;
			if (mouseTilePos == null)
			{
				flag3 = (null != null);
			}
			else
			{
				MapRegion region3 = mouseTilePos.region;
				flag3 = (((region3 != null) ? region3.island : null) != null);
			}
			if (flag3)
			{
				foreach (MapRegion mapRegion2 in mouseTilePos.region.island.regionsCorners)
				{
					this.fill(mapRegion2.getTileCorners(), Color.red, false);
				}
			}
		}
		if (DebugConfig.isOn(DebugOption.RenderIslandCenterRegions))
		{
			bool flag4;
			if (mouseTilePos == null)
			{
				flag4 = (null != null);
			}
			else
			{
				MapRegion region4 = mouseTilePos.region;
				flag4 = (((region4 != null) ? region4.island : null) != null);
			}
			if (flag4)
			{
				foreach (MapRegion mapRegion3 in mouseTilePos.region.island.regions)
				{
					if (!mapRegion3.centerRegion)
					{
						this.fill(mapRegion3.tiles, Color.red, false);
					}
				}
			}
		}
		if (DebugConfig.isOn(DebugOption.RenderMapRegionEdges) && mouseTilePos.region != null)
		{
			this.fill(mouseTilePos.region.getTileCorners(), Color.red, false);
		}
		if (DebugConfig.isOn(DebugOption.RegionNeighbours) && mouseTilePos.region != null)
		{
			foreach (MapRegion mapRegion4 in mouseTilePos.region.neighbours)
			{
				this.fill(mapRegion4.tiles, this.color_neighbour, false);
			}
		}
		if (DebugConfig.isOn(DebugOption.ChunkEdges) && mouseTilePos.chunk != null)
		{
			this.fill(mouseTilePos.chunk.edges_up, this.color_edges, false);
			this.fill(mouseTilePos.chunk.edges_right, this.color_edges, false);
			this.fill(mouseTilePos.chunk.edges_left, this.color_edges, false);
			this.fill(mouseTilePos.chunk.edges_down, this.color_edges, false);
		}
		if (DebugConfig.isOn(DebugOption.Connections) && mouseTilePos.region != null)
		{
			this.drawConnections(mouseTilePos);
		}
		base.updatePixels();
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x000709C4 File Offset: 0x0006EBC4
	private void drawIsland(TileIsland pIsland)
	{
		Color32 color = Color.red;
		foreach (MapRegion mapRegion in pIsland.regions)
		{
			this.tiles.AddRange(mapRegion.tiles);
			foreach (WorldTile worldTile in mapRegion.tiles)
			{
				this.pixels[worldTile.data.tile_id] = color;
			}
		}
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x00070A7C File Offset: 0x0006EC7C
	private void drawConnections(WorldTile pTile)
	{
		if (this.blink && pTile.region.edges_up != null)
		{
			this.fill(pTile.region.edges_up, this.color_edges_blink, true);
			this.fill(pTile.region.edges_down, this.color_edges_blink, true);
			this.fill(pTile.region.edges_left, this.color_edges_blink, true);
			this.fill(pTile.region.edges_right, this.color_edges_blink, true);
		}
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x00070B00 File Offset: 0x0006ED00
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

	// Token: 0x06000BA5 RID: 2981 RVA: 0x00070B7C File Offset: 0x0006ED7C
	internal override void clear()
	{
		if (this.tiles.Count == 0)
		{
			return;
		}
		foreach (WorldTile worldTile in this.tiles)
		{
			this.pixels[worldTile.data.tile_id] = Color.clear;
		}
		this.tiles.Clear();
	}

	// Token: 0x04000DC6 RID: 3526
	private Color color_highlight_white;

	// Token: 0x04000DC7 RID: 3527
	private Color color_main;

	// Token: 0x04000DC8 RID: 3528
	private Color color_neighbour;

	// Token: 0x04000DC9 RID: 3529
	private Color color_region;

	// Token: 0x04000DCA RID: 3530
	private Color color_edges;

	// Token: 0x04000DCB RID: 3531
	private Color color_edges_blink;

	// Token: 0x04000DCC RID: 3532
	private List<WorldTile> tiles = new List<WorldTile>();

	// Token: 0x04000DCD RID: 3533
	private bool blink = true;

	// Token: 0x04000DCE RID: 3534
	private float timerBlink = 0.2f;

	// Token: 0x04000DCF RID: 3535
	private float timerRecalc = 0.1f;

	// Token: 0x04000DD0 RID: 3536
	private MapChunk lastChunk;
}
