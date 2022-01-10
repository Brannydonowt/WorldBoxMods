using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Token: 0x020000A9 RID: 169
public class TilemapExtended : MonoBehaviour
{
	// Token: 0x06000368 RID: 872 RVA: 0x00036E24 File Offset: 0x00035024
	internal void create()
	{
		this.tilemap = base.GetComponent<Tilemap>();
		this._vec = new List<Vector3Int>();
		this._tiles = new List<Tile>();
	}

	// Token: 0x06000369 RID: 873 RVA: 0x00036E48 File Offset: 0x00035048
	internal void prepareDraw()
	{
		this._vec.Clear();
		this._tiles.Clear();
	}

	// Token: 0x0600036A RID: 874 RVA: 0x00036E60 File Offset: 0x00035060
	internal void setTile(WorldTile pWorldTile, Vector3Int pVec, Tile pGraphic)
	{
		pVec.z = 0;
		if (pWorldTile.curGraphics == pGraphic && pGraphic != null)
		{
			return;
		}
		pWorldTile.curGraphics = pGraphic;
		this._vec.Add(pVec);
		this._tiles.Add(pGraphic);
	}

	// Token: 0x0600036B RID: 875 RVA: 0x00036EAC File Offset: 0x000350AC
	internal void clear()
	{
		this.tilemap.ClearAllTiles();
	}

	// Token: 0x0600036C RID: 876 RVA: 0x00036EBC File Offset: 0x000350BC
	internal void redraw()
	{
		if (this._vec.Count == 0)
		{
			return;
		}
		Tilemap tilemap = this.tilemap;
		Vector3Int[] array = this._vec.ToArray();
		TileBase[] array2 = this._tiles.ToArray();
		tilemap.SetTiles(array, array2);
	}

	// Token: 0x040005C2 RID: 1474
	internal TileTypeBase tileType;

	// Token: 0x040005C3 RID: 1475
	public int z;

	// Token: 0x040005C4 RID: 1476
	internal Tilemap tilemap;

	// Token: 0x040005C5 RID: 1477
	private List<Vector3Int> _vec;

	// Token: 0x040005C6 RID: 1478
	private List<Tile> _tiles;
}
