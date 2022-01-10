using System;
using UnityEngine;

// Token: 0x02000044 RID: 68
public class ResourceAsset : Asset
{
	// Token: 0x060001B6 RID: 438 RVA: 0x000228DF File Offset: 0x00020ADF
	public Sprite getSprite()
	{
		if (this.sprite == null)
		{
			this.sprite = (Sprite)Resources.Load("ui/Icons/" + this.icon, typeof(Sprite));
		}
		return this.sprite;
	}

	// Token: 0x04000195 RID: 405
	public ResType type = ResType.Ingredient;

	// Token: 0x04000196 RID: 406
	public string icon;

	// Token: 0x04000197 RID: 407
	public int mineRate;

	// Token: 0x04000198 RID: 408
	public int maximum = 999;

	// Token: 0x04000199 RID: 409
	public Sprite sprite;

	// Token: 0x0400019A RID: 410
	public int restoreHunger;

	// Token: 0x0400019B RID: 411
	public float restoreHealth;

	// Token: 0x0400019C RID: 412
	public int ingredientsAmount = 1;

	// Token: 0x0400019D RID: 413
	public string[] ingredients;

	// Token: 0x0400019E RID: 414
	public int supplyBoundGive = 30;

	// Token: 0x0400019F RID: 415
	public int supplyBoundTake = 10;

	// Token: 0x040001A0 RID: 416
	public int supplyGive = 10;

	// Token: 0x040001A1 RID: 417
	public int tradeBound = 40;

	// Token: 0x040001A2 RID: 418
	public int tradeGive = 10;

	// Token: 0x040001A3 RID: 419
	public int tradeCost = 1;
}
