using System;
using System.Collections.Generic;

// Token: 0x02000189 RID: 393
public class RoadsCalculator : BaseModule
{
	// Token: 0x0600090B RID: 2315 RVA: 0x000606FA File Offset: 0x0005E8FA
	internal override void create()
	{
		base.create();
		this.islands = new List<TileIsland>();
		this.tileDictionary = new TileDictionary();
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x00060718 File Offset: 0x0005E918
	public void setDirty(WorldTile pTile)
	{
		if (pTile.roadIsland != null)
		{
			pTile.roadIsland.dirty = true;
		}
		if (pTile.Type.road)
		{
			this.tileDictionary.add(pTile);
		}
		else
		{
			this.tileDictionary.remove(pTile, true);
			pTile.roadIsland = null;
		}
		this.dirty = true;
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x0006076F File Offset: 0x0005E96F
	internal override void clear()
	{
		base.clear();
		this.tileDictionary.clear();
		this.islands.Clear();
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x0006078D File Offset: 0x0005E98D
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (!this.dirty)
		{
			return;
		}
		this.dirty = false;
		this.RecalculateIslands();
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x000607AC File Offset: 0x0005E9AC
	private void RecalculateIslands()
	{
		for (int i = 0; i < this.islands.Count; i++)
		{
			TileIsland tileIsland = this.islands[i];
			if (tileIsland.dirty)
			{
				this.islands.RemoveAt(i);
				using (Dictionary<WorldTile, bool>.KeyCollection.Enumerator enumerator = tileIsland.tiles_roads.dict.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						WorldTile worldTile = enumerator.Current;
						worldTile.roadIsland = null;
					}
					continue;
				}
			}
		}
		foreach (WorldTile worldTile2 in this.tileDictionary.dict.Keys)
		{
			if (worldTile2.roadIsland == null)
			{
				int num = this.current_island;
				this.current_island = num + 1;
				TileIsland tileIsland = new TileIsland(num);
				this.islands.Add(tileIsland);
				this.CalcIsland(worldTile2, tileIsland);
			}
		}
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x000608BC File Offset: 0x0005EABC
	private void CalcIsland(WorldTile pTile, TileIsland pIsland)
	{
		List<WorldTile> list = new List<WorldTile>();
		list.Add(pTile);
		List<WorldTile> list2 = list;
		while (list2.Count > 0)
		{
			WorldTile worldTile = list2[0];
			list2.RemoveAt(0);
			worldTile.roadIsland = pIsland;
			pIsland.tiles_roads.add(worldTile);
			for (int i = 0; i < worldTile.neighboursAll.Count; i++)
			{
				WorldTile worldTile2 = worldTile.neighboursAll[i];
				if (worldTile2.roadIsland == null && worldTile2.Type.road)
				{
					worldTile2.roadIsland = pIsland;
					list2.Add(worldTile2);
				}
			}
		}
	}

	// Token: 0x04000B87 RID: 2951
	public List<TileIsland> islands;

	// Token: 0x04000B88 RID: 2952
	private int current_island;

	// Token: 0x04000B89 RID: 2953
	private bool dirty = true;
}
