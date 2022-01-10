using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Token: 0x020000A7 RID: 167
public class TileSprites
{
	// Token: 0x06000364 RID: 868 RVA: 0x00036DD0 File Offset: 0x00034FD0
	public void addVariation(Sprite pSprite)
	{
		Tile tile = ScriptableObject.CreateInstance<Tile>();
		tile.sprite = pSprite;
		this._tiles.Add(tile);
	}

	// Token: 0x06000365 RID: 869 RVA: 0x00036DF6 File Offset: 0x00034FF6
	public Tile getRandom()
	{
		return this._tiles.GetRandom<Tile>();
	}

	// Token: 0x06000366 RID: 870 RVA: 0x00036E03 File Offset: 0x00035003
	public Tile getVariation(int pID)
	{
		return this._tiles[pID];
	}

	// Token: 0x040005BF RID: 1471
	private List<Tile> _tiles = new List<Tile>();
}
