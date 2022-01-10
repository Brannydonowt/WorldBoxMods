using System;
using UnityEngine;

// Token: 0x020002D0 RID: 720
public class ViewRainfall : MapLayer
{
	// Token: 0x06000F97 RID: 3991 RVA: 0x0008B58C File Offset: 0x0008978C
	internal override void create()
	{
		this.colorValues = new Color(0f, 0f, 1f);
		this.colors_amount = 10;
		base.create();
		this.sprRnd.color = new Color(1f, 1f, 1f, 0.6f);
	}

	// Token: 0x06000F98 RID: 3992 RVA: 0x0008B5E5 File Offset: 0x000897E5
	public void setTileDirty(WorldTile pTile)
	{
	}

	// Token: 0x06000F99 RID: 3993 RVA: 0x0008B5E7 File Offset: 0x000897E7
	protected override void UpdateDirty(float pElapsed)
	{
	}
}
