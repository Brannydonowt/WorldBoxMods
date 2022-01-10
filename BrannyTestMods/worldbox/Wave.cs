using System;

// Token: 0x020002F1 RID: 753
public class Wave : BaseEffect
{
	// Token: 0x06001111 RID: 4369 RVA: 0x0009598C File Offset: 0x00093B8C
	public override void update(float pElapsed)
	{
		if (this.timer > 0f)
		{
			this.timer -= pElapsed;
			return;
		}
		this.timer = 0.6f;
		if (this.tile != null)
		{
			this.tile = this.tile.tile_up;
		}
	}

	// Token: 0x06001112 RID: 4370 RVA: 0x000959D9 File Offset: 0x00093BD9
	public void waveEnd()
	{
		this.controller.killObject(this);
	}

	// Token: 0x04001443 RID: 5187
	private float timer;
}
