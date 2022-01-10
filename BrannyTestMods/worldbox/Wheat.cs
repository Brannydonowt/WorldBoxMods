using System;

// Token: 0x020000D4 RID: 212
public class Wheat : BaseBuildingComponent
{
	// Token: 0x0600046B RID: 1131 RVA: 0x0003D72E File Offset: 0x0003B92E
	internal override void create()
	{
		base.create();
		this.timerGrow = this.GROW_INTERVAL;
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x0003D742 File Offset: 0x0003B942
	public override void update(float pElapsed)
	{
		if (this.timerGrow > 0f)
		{
			this.timerGrow -= pElapsed;
			return;
		}
		this.growWheat();
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x0003D766 File Offset: 0x0003B966
	internal void growWheat()
	{
		this.building.upgradeBulding();
		this.timerGrow = this.GROW_INTERVAL;
	}

	// Token: 0x04000697 RID: 1687
	private float timerGrow;

	// Token: 0x04000698 RID: 1688
	private float GROW_INTERVAL = 30f;
}
