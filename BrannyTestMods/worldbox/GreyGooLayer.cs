using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000176 RID: 374
public class GreyGooLayer : BaseModule
{
	// Token: 0x0600086B RID: 2155 RVA: 0x0005B482 File Offset: 0x00059682
	internal override void create()
	{
		base.create();
		this.tileDictionary = new TileDictionary();
		this.tilesToAdd = new List<WorldTile>();
		this.tilesToRemove = new List<WorldTile>();
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x0005B4AC File Offset: 0x000596AC
	internal void checkKillRange(Vector2Int pPos, int pRad)
	{
		this.toRemove.Clear();
		foreach (WorldTile worldTile in this.tileDictionary.dict.Keys)
		{
			if (Toolbox.DistVec2(worldTile.pos, pPos) <= (float)pRad)
			{
				this.toRemove.Add(worldTile);
			}
		}
		foreach (WorldTile pTile in this.toRemove)
		{
			this.remove(pTile);
		}
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x0005B56C File Offset: 0x0005976C
	internal override void clear()
	{
		base.clear();
		this.tileDictionary.clear();
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x0005B580 File Offset: 0x00059780
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this.tileDictionary.Count() == 0)
		{
			return;
		}
		if (this.world.isPaused())
		{
			return;
		}
		if (this.timer > 0f)
		{
			this.timer -= pElapsed;
			return;
		}
		this.timer = 0.04f;
		this.tilesToRemove.Clear();
		this.tilesToAdd.Clear();
		foreach (WorldTile worldTile in this.tileDictionary.dict.Keys)
		{
			if (worldTile.Type.greyGoo)
			{
				if (Toolbox.randomBool())
				{
					worldTile.health--;
				}
				if (worldTile.health == 5)
				{
					this.checkAroundTiles(worldTile);
				}
				if (worldTile.health <= 0)
				{
					if (worldTile.building != null)
					{
						worldTile.building.startDestroyBuilding(true);
					}
					if (Toolbox.randomBool())
					{
						this.terraform(worldTile);
						this.tilesToRemove.Add(worldTile);
					}
				}
			}
			else
			{
				this.tilesToRemove.Add(worldTile);
			}
		}
		foreach (WorldTile pTile in this.tilesToRemove)
		{
			this.remove(pTile);
		}
		foreach (WorldTile pTile2 in this.tilesToAdd)
		{
			this.add(pTile2);
		}
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x0005B740 File Offset: 0x00059940
	private void checkAroundTiles(WorldTile pTile)
	{
		foreach (WorldTile worldTile in pTile.neighbours)
		{
			if (!worldTile.Type.IsType("pit_deep_ocean") && (!worldTile.Type.IsType("deep_ocean") || !(worldTile.building == null)) && !worldTile.Type.greyGoo)
			{
				this.makeGoo(worldTile);
				this.tilesToAdd.Add(worldTile);
			}
		}
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x0005B7E0 File Offset: 0x000599E0
	private void makeGoo(WorldTile pTile)
	{
		MapAction.terraformMain(pTile, TileLibrary.grey_goo);
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x0005B7ED File Offset: 0x000599ED
	private void terraform(WorldTile pTile)
	{
		MapAction.terraformMain(pTile, TileLibrary.pit_deep_ocean, TerraformLibrary.grey_goo);
		Sfx.play("goo1", true, -1f, -1f);
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x0005B814 File Offset: 0x00059A14
	public void remove(WorldTile pTile)
	{
		this.tileDictionary.remove(pTile, true);
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x0005B824 File Offset: 0x00059A24
	public void add(WorldTile pTile)
	{
		if (pTile.Type.IsType("pit_deep_ocean"))
		{
			return;
		}
		if (pTile.Type.IsType("deep_ocean"))
		{
			return;
		}
		if (this.tileDictionary.contains(pTile))
		{
			return;
		}
		this.tileDictionary.add(pTile);
		this.makeGoo(pTile);
	}

	// Token: 0x04000AE9 RID: 2793
	private List<WorldTile> tilesToRemove;

	// Token: 0x04000AEA RID: 2794
	private List<WorldTile> tilesToAdd;

	// Token: 0x04000AEB RID: 2795
	private List<WorldTile> toRemove = new List<WorldTile>();
}
