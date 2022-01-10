using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Token: 0x020000B1 RID: 177
public class WorldTilemap : BaseMapObject
{
	// Token: 0x06000396 RID: 918 RVA: 0x00038408 File Offset: 0x00036608
	internal override void create()
	{
		base.create();
		WorldTilemap.lastTileEmpty.z = WorldTilemap.EMPTY_Z;
		this.layers = new Dictionary<int, TilemapExtended>();
		this.border_water = AssetManager.tiles.get("border_water");
		this.border_pit = AssetManager.tiles.get("border_pit");
		foreach (TileTypeBase pTileBase in AssetManager.tiles.list)
		{
			this.createTileMapFor(pTileBase);
		}
		foreach (TileTypeBase pTileBase2 in AssetManager.topTiles.list)
		{
			this.createTileMapFor(pTileBase2);
		}
	}

	// Token: 0x06000397 RID: 919 RVA: 0x000384F0 File Offset: 0x000366F0
	private void createTileMapFor(TileTypeBase pTileBase)
	{
		if (this.layers.ContainsKey(pTileBase.render_z))
		{
			return;
		}
		TilemapExtended tilemapExtended = Object.Instantiate<TilemapExtended>(this.prefabTilemapLayer, base.transform);
		tilemapExtended.tileType = pTileBase;
		tilemapExtended.z = pTileBase.render_z;
		tilemapExtended.gameObject.name = pTileBase.drawLayerName;
		tilemapExtended.GetComponent<TilemapRenderer>().sortingOrder = pTileBase.render_z;
		this.layers.Add(pTileBase.render_z, tilemapExtended);
		tilemapExtended.create();
		if (pTileBase.id == "deep_ocean")
		{
			tilemapExtended.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00038590 File Offset: 0x00036790
	internal void clear()
	{
		this.redrawTiles.clear();
		foreach (TilemapExtended tilemapExtended in this.layers.Values)
		{
			tilemapExtended.clear();
		}
	}

	// Token: 0x06000399 RID: 921 RVA: 0x000385F0 File Offset: 0x000367F0
	internal void updateTileMap()
	{
		Vector3Int vector3Int = default(Vector3Int);
		foreach (TilemapExtended tilemapExtended in this.layers.Values)
		{
			tilemapExtended.prepareDraw();
		}
		for (int i = 0; i < this.world.tilesDirty.Count; i++)
		{
			WorldTile worldTile = this.world.tilesDirty[i];
			if (worldTile.dirty)
			{
				this.world.checkBehaviours(worldTile);
				worldTile.dirty = false;
				TileTypeBase tileTypeBase = worldTile.main_type;
				if (worldTile.top_type != null)
				{
					tileTypeBase = worldTile.top_type;
				}
				int render_z = tileTypeBase.render_z;
				vector3Int.Set(worldTile.pos.x, worldTile.pos.y, render_z);
				if (vector3Int.z != worldTile.lastTile.z || worldTile.lastDrawnType != tileTypeBase)
				{
					if (worldTile.lastTile.z != WorldTilemap.lastTileEmpty.z)
					{
						this.layers[worldTile.lastTile.z].setTile(worldTile, worldTile.lastTile, null);
						worldTile.lastTile = WorldTilemap.lastTileEmpty;
					}
					worldTile.lastDrawnType = tileTypeBase;
					TilemapExtended tilemapExtended2 = this.layers[vector3Int.z];
					Tile variation = this.getVariation(worldTile, null);
					tilemapExtended2.setTile(worldTile, vector3Int, variation);
					worldTile.lastTile = vector3Int;
				}
				if (worldTile.lastBorderOcean.z != WorldTilemap.lastTileEmpty.z)
				{
					this.layers[worldTile.lastBorderOcean.z].setTile(worldTile, worldTile.lastBorderOcean, null);
					worldTile.lastBorderOcean = WorldTilemap.lastTileEmpty;
				}
				if ((worldTile.main_type.ground || worldTile.main_type.block) && !worldTile.main_type.canBeFilledWithOcean)
				{
					TileType tileType = null;
					if (worldTile.tile_down != null && worldTile.tile_down.main_type.canBeFilledWithOcean)
					{
						tileType = this.border_pit;
						render_z = tileType.render_z;
					}
					else if (worldTile.tile_down == null || worldTile.tile_down.main_type.liquid)
					{
						tileType = this.border_water;
						render_z = tileType.render_z;
					}
					if (tileType != null)
					{
						TilemapExtended tilemapExtended3 = this.layers[render_z];
						vector3Int.y = worldTile.pos.y - 1;
						vector3Int.z = render_z;
						Tile variation = tileType.sprites.getVariation(0);
						tilemapExtended3.setTile(worldTile, vector3Int, variation);
						worldTile.lastBorderOcean = vector3Int;
					}
				}
			}
		}
		foreach (TilemapExtended tilemapExtended4 in this.layers.Values)
		{
			tilemapExtended4.redraw();
		}
	}

	// Token: 0x0600039A RID: 922 RVA: 0x00038904 File Offset: 0x00036B04
	internal void enableTiles(bool pValue)
	{
		base.gameObject.SetActive(pValue);
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00038914 File Offset: 0x00036B14
	private Tile getVariation(WorldTile pTile, TileTypeBase pForcedType = null)
	{
		TileSprites sprites = pTile.main_type.sprites;
		if (pForcedType == null)
		{
			if (pTile.top_type != null)
			{
				sprites = pTile.top_type.sprites;
			}
		}
		else
		{
			sprites = pForcedType.sprites;
		}
		if (pTile.Type.force_edge_variation && pTile.tile_up != null && pTile.tile_up.Type != pTile.Type)
		{
			return pTile.main_type.sprites.getVariation(pTile.main_type.force_edge_variation_frame);
		}
		return sprites.getRandom();
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00038998 File Offset: 0x00036B98
	internal int countTiles()
	{
		int num = 0;
		foreach (TilemapExtended tilemapExtended in this.layers.Values)
		{
			BoundsInt cellBounds = tilemapExtended.tilemap.cellBounds;
			TileBase[] tilesBlock = tilemapExtended.tilemap.GetTilesBlock(cellBounds);
			for (int i = 0; i < tilesBlock.Length; i++)
			{
				if (tilesBlock[i] != null)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x040005E6 RID: 1510
	public static int EMPTY_Z = -1000;

	// Token: 0x040005E7 RID: 1511
	public static Vector3Int lastTileEmpty = new Vector3Int(-1, -1, -1000);

	// Token: 0x040005E8 RID: 1512
	private Dictionary<int, TilemapExtended> layers;

	// Token: 0x040005E9 RID: 1513
	public TilemapExtended prefabTilemapLayer;

	// Token: 0x040005EA RID: 1514
	private TileType border_water;

	// Token: 0x040005EB RID: 1515
	private TileType border_pit;

	// Token: 0x040005EC RID: 1516
	private TileDictionary redrawTiles = new TileDictionary();
}
