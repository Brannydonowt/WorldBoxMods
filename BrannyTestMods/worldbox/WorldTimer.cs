using System;
using UnityEngine;

// Token: 0x020001DC RID: 476
public class WorldTimer
{
	// Token: 0x06000ABE RID: 2750 RVA: 0x0006B75C File Offset: 0x0006995C
	public WorldTimer(float pInterval, Action pCallback)
	{
		this.interval = pInterval;
		this.callback = pCallback;
		this.timer = this.interval;
	}

	// Token: 0x06000ABF RID: 2751 RVA: 0x0006B77E File Offset: 0x0006997E
	public void setTime(float pNewTime)
	{
		this.timer = pNewTime;
	}

	// Token: 0x06000AC0 RID: 2752 RVA: 0x0006B787 File Offset: 0x00069987
	internal void setInterval(float pInterval)
	{
		this.interval = pInterval;
	}

	// Token: 0x06000AC1 RID: 2753 RVA: 0x0006B790 File Offset: 0x00069990
	public WorldTimer(float pInterval, bool pStopWatch)
	{
		this.isStopWatch = pStopWatch;
		this.interval = pInterval;
		this.timer = 0f;
		this.isActive = false;
	}

	// Token: 0x06000AC2 RID: 2754 RVA: 0x0006B7B8 File Offset: 0x000699B8
	public void startTimer(float pRate = -1f)
	{
		if (pRate != -1f)
		{
			this.interval = pRate;
		}
		this.timer = this.interval;
	}

	// Token: 0x06000AC3 RID: 2755 RVA: 0x0006B7D5 File Offset: 0x000699D5
	public void stop()
	{
		this.isActive = false;
	}

	// Token: 0x06000AC4 RID: 2756 RVA: 0x0006B7E0 File Offset: 0x000699E0
	public void update(float pElapsed = -1f)
	{
		if (pElapsed == -1f)
		{
			pElapsed = Time.deltaTime;
		}
		if (this.isStopWatch)
		{
			if (this.timer > 0f)
			{
				this.timer -= pElapsed;
				this.isActive = true;
				return;
			}
			this.isActive = false;
			return;
		}
		else
		{
			if (this.timer > 0f)
			{
				this.timer -= pElapsed;
				return;
			}
			this.timer = this.interval;
			this.callback();
			return;
		}
	}

	// Token: 0x04000D45 RID: 3397
	public bool isActive;

	// Token: 0x04000D46 RID: 3398
	private bool isStopWatch;

	// Token: 0x04000D47 RID: 3399
	private Action callback;

	// Token: 0x04000D48 RID: 3400
	private float interval;

	// Token: 0x04000D49 RID: 3401
	internal float timer;
}
