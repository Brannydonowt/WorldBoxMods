using System;
using System.Collections.Generic;

// Token: 0x020000A6 RID: 166
public class TileDictionary
{
	// Token: 0x0600035C RID: 860 RVA: 0x00036D45 File Offset: 0x00034F45
	public TileDictionary()
	{
		this.dict = new Dictionary<WorldTile, bool>();
	}

	// Token: 0x0600035D RID: 861 RVA: 0x00036D58 File Offset: 0x00034F58
	public void clear()
	{
		this.dict.Clear();
	}

	// Token: 0x0600035E RID: 862 RVA: 0x00036D65 File Offset: 0x00034F65
	public bool contains(WorldTile pTile)
	{
		return this.dict.ContainsKey(pTile);
	}

	// Token: 0x0600035F RID: 863 RVA: 0x00036D73 File Offset: 0x00034F73
	public void add(WorldTile pTile)
	{
		this.dict[pTile] = true;
	}

	// Token: 0x06000360 RID: 864 RVA: 0x00036D82 File Offset: 0x00034F82
	public void remove(WorldTile pTile, bool pFromList = true)
	{
		this.dict.Remove(pTile);
	}

	// Token: 0x06000361 RID: 865 RVA: 0x00036D91 File Offset: 0x00034F91
	public int Count()
	{
		return this.dict.Count;
	}

	// Token: 0x06000362 RID: 866 RVA: 0x00036DA0 File Offset: 0x00034FA0
	public WorldTile getRandomTileAndRemove()
	{
		WorldTile randomDictTile = Toolbox.getRandomDictTile(this.dict);
		this.remove(randomDictTile, true);
		return randomDictTile;
	}

	// Token: 0x06000363 RID: 867 RVA: 0x00036DC2 File Offset: 0x00034FC2
	public WorldTile getRandomTile()
	{
		return Toolbox.getRandomDictTile(this.dict);
	}

	// Token: 0x040005BE RID: 1470
	public Dictionary<WorldTile, bool> dict;
}
