using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200016B RID: 363
public class ConwayLife : MapLayer
{
	// Token: 0x06000817 RID: 2071 RVA: 0x00058F72 File Offset: 0x00057172
	internal override void create()
	{
		base.create();
		ConwayLife.colorCreator = Toolbox.makeColor("#3BCC55");
		this.tileDictionary = new TileDictionary();
		this.newList = new TileDictionary();
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x00058FA4 File Offset: 0x000571A4
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x00058FB0 File Offset: 0x000571B0
	protected override void UpdateDirty(float pElapsed)
	{
		this.UpdateVisual();
		if (this.world._isPaused)
		{
			return;
		}
		if (this.nextTickTimer > 0f)
		{
			this.nextTickTimer -= pElapsed;
			return;
		}
		this.nextTickTimer = this.nextTickInterval;
		int num = 0;
		while ((float)num < Config.timeScale)
		{
			this.updateTick();
			num++;
		}
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x00059010 File Offset: 0x00057210
	private void UpdateVisual()
	{
		if (this.pixels_to_update.Count() == 0)
		{
			return;
		}
		foreach (WorldTile worldTile in this.pixels_to_update.dict.Keys)
		{
			if (this.tileDictionary.contains(worldTile))
			{
				if (worldTile.data.conwayType == ConwayType.Eater)
				{
					this.pixels[worldTile.data.tile_id] = ConwayLife.colorEater;
				}
				else if (worldTile.data.conwayType == ConwayType.Creator)
				{
					this.pixels[worldTile.data.tile_id] = ConwayLife.colorCreator;
				}
				else
				{
					this.pixels[worldTile.data.tile_id] = Toolbox.clear;
				}
			}
			else
			{
				worldTile.data.conwayType = ConwayType.None;
				this.pixels[worldTile.data.tile_id] = Toolbox.clear;
			}
		}
		this.pixels_to_update.clear();
		base.updatePixels();
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x00059134 File Offset: 0x00057334
	public void remove(WorldTile pTile)
	{
		this.tileDictionary.remove(pTile, true);
		this.pixels_to_update.add(pTile);
		pTile.data.conwayType = ConwayType.None;
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x0005915C File Offset: 0x0005735C
	public void add(WorldTile pTile, string pType)
	{
		if (pType == "conway")
		{
			pTile.data.conwayType = ConwayType.Eater;
		}
		else
		{
			pTile.data.conwayType = ConwayType.Creator;
		}
		this.tileDictionary.add(pTile);
		this.pixels_to_update.add(pTile);
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x000591A8 File Offset: 0x000573A8
	private void updateTick()
	{
		int num = this.decreaseTick;
		this.decreaseTick = num - 1;
		if (num <= 0)
		{
			this.decreaseTick = 5;
		}
		if (this.tileDictionary.Count() <= 0 && this.newList.Count() <= 0)
		{
			return;
		}
		this.newList.clear();
		foreach (WorldTile worldTile in this.tileDictionary.dict.Keys)
		{
			this.checkCell(worldTile);
			foreach (WorldTile pCell in worldTile.neighboursAll)
			{
				this.checkCell(pCell);
			}
		}
		TileDictionary tileDictionary = this.tileDictionary;
		this.tileDictionary = this.newList;
		this.newList = tileDictionary;
		this.UpdateVisual();
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x000592B0 File Offset: 0x000574B0
	private void makeAlive(WorldTile pCell)
	{
		if (this.decreaseTick == 5)
		{
			if (pCell.data.conwayType == ConwayType.Eater)
			{
				MapAction.decreaseTile(pCell, "destroy_no_flash");
			}
			else
			{
				MapAction.increaseTile(pCell, "destroy_no_flash");
			}
		}
		this.newList.add(pCell);
		if (this.makeFlash)
		{
			this.makeFlashh(pCell, 25);
		}
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x00059308 File Offset: 0x00057508
	internal void makeFlashh(WorldTile pCell, int pAmount)
	{
		if (pCell.data.conwayType == ConwayType.None)
		{
			return;
		}
		ConwayType conwayType = pCell.data.conwayType;
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x00059325 File Offset: 0x00057525
	internal override void clear()
	{
		base.clear();
		this.newList.clear();
		this.tileDictionary.clear();
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x00059344 File Offset: 0x00057544
	private void checkCell(WorldTile pCell)
	{
		if (this.pixels_to_update.contains(pCell))
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		this.pixels_to_update.add(pCell);
		if (pCell.data.conwayType == ConwayType.Eater)
		{
			num2++;
		}
		if (pCell.data.conwayType == ConwayType.Creator)
		{
			num3++;
		}
		if (this.tileDictionary.contains(pCell))
		{
			foreach (WorldTile worldTile in pCell.neighboursAll)
			{
				if (this.tileDictionary.contains(worldTile))
				{
					num++;
					if (worldTile.data.conwayType == ConwayType.Creator)
					{
						num3++;
					}
					else if (worldTile.data.conwayType == ConwayType.Eater)
					{
						num2++;
					}
				}
				if (num >= 4)
				{
					if (this.makeFlash)
					{
						this.makeFlashh(pCell, 15);
					}
					pCell.data.conwayType = ConwayType.None;
					return;
				}
			}
			if (num == 2 || num == 3)
			{
				if (pCell.data.conwayType == ConwayType.None && (num2 != 0 || num3 != 0))
				{
					if (num2 >= num3)
					{
						pCell.data.conwayType = ConwayType.Eater;
					}
					else
					{
						pCell.data.conwayType = ConwayType.Creator;
					}
				}
				this.makeAlive(pCell);
				return;
			}
			pCell.data.conwayType = ConwayType.None;
			return;
		}
		else
		{
			foreach (WorldTile worldTile2 in pCell.neighboursAll)
			{
				if (this.tileDictionary.contains(worldTile2))
				{
					num++;
				}
				if (worldTile2.data.conwayType == ConwayType.Eater)
				{
					num2++;
				}
				if (worldTile2.data.conwayType == ConwayType.Creator)
				{
					num3++;
				}
			}
			if (num == 3)
			{
				if (pCell.data.conwayType == ConwayType.None && (num2 != 0 || num3 != 0))
				{
					if (num2 >= num3)
					{
						pCell.data.conwayType = ConwayType.Eater;
					}
					else
					{
						pCell.data.conwayType = ConwayType.Creator;
					}
				}
				this.makeAlive(pCell);
			}
		}
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x00059548 File Offset: 0x00057748
	internal void checkKillRange(Vector2Int pPos, int pRad)
	{
		if (this.tileDictionary.dict.Count == 0)
		{
			return;
		}
		this.toRemove.Clear();
		foreach (WorldTile worldTile in this.tileDictionary.dict.Keys)
		{
			if (Toolbox.DistVec2(worldTile.pos, pPos) <= (float)pRad)
			{
				worldTile.data.conwayType = ConwayType.None;
				this.toRemove.Add(worldTile);
			}
		}
		foreach (WorldTile pTile in this.toRemove)
		{
			this.remove(pTile);
		}
	}

	// Token: 0x04000A98 RID: 2712
	public static Color32 colorEater = new Color(1f, 0.2f, 1f);

	// Token: 0x04000A99 RID: 2713
	public static Color32 colorCreator;

	// Token: 0x04000A9A RID: 2714
	public bool makeFlash = true;

	// Token: 0x04000A9B RID: 2715
	private TileDictionary newList;

	// Token: 0x04000A9C RID: 2716
	private float nextTickTimer;

	// Token: 0x04000A9D RID: 2717
	private float nextTickInterval = 0.05f;

	// Token: 0x04000A9E RID: 2718
	private int decreaseTick;

	// Token: 0x04000A9F RID: 2719
	private List<WorldTile> toRemove = new List<WorldTile>();
}
