using System;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class BuildingSmokeEffect : BaseBuildingComponent
{
	// Token: 0x06000447 RID: 1095 RVA: 0x0003CB84 File Offset: 0x0003AD84
	internal override void create()
	{
		base.create();
		this.centerTopVec = default(Vector3);
		this.centerTopVec.x = (float)this.building.currentTile.pos.x;
		this.centerTopVec.y = (float)this.building.currentTile.pos.y + this.building.s_main_sprite.textureRect.height * Building.defaultScale.y;
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x0003CC10 File Offset: 0x0003AE10
	public override void update(float pElapsed)
	{
		if (this.building.stats.smoke && !this.building.data.underConstruction)
		{
			if (this.smokeTimer > 0f)
			{
				this.smokeTimer -= Time.deltaTime;
				return;
			}
			this.smokeTimer = this.building.stats.smokeInterval;
			this.world.particlesSmoke.spawn(this.centerTopVec.x, this.centerTopVec.y, true);
		}
	}

	// Token: 0x04000681 RID: 1665
	private float smokeTimer;

	// Token: 0x04000682 RID: 1666
	private Vector3 centerTopVec;
}
