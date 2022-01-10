using System;

// Token: 0x020000CC RID: 204
public class BuildingFruitGrowth : BaseBuildingComponent
{
	// Token: 0x06000442 RID: 1090 RVA: 0x0003C8C4 File Offset: 0x0003AAC4
	public override void update(float pElapsed)
	{
		if (this.building.data.state != BuildingState.Normal)
		{
			return;
		}
		if (!this.building.haveResources)
		{
			if (this.resourceResetTime > 0f)
			{
				this.resourceResetTime -= pElapsed;
				return;
			}
			this.building.setHaveResources(true);
			this.building.setScaleTween(0.75f, 0.2f);
		}
	}

	// Token: 0x0400067A RID: 1658
	internal float resourceResetTime;
}
