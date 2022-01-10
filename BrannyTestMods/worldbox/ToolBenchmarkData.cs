using System;
using System.Collections.Generic;

// Token: 0x02000202 RID: 514
public class ToolBenchmarkData
{
	// Token: 0x06000B7C RID: 2940 RVA: 0x0006F1C6 File Offset: 0x0006D3C6
	public void put(float pTime)
	{
		this.latest = pTime;
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x0006F1CF File Offset: 0x0006D3CF
	public void end(float pTime)
	{
		this.latestResult = pTime;
		if (this.results.Count > 20)
		{
			this.results.Dequeue();
		}
		this.results.Enqueue(pTime);
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x0006F200 File Offset: 0x0006D400
	public float getAverage()
	{
		float num = 0f;
		foreach (float num2 in this.results)
		{
			num += num2;
		}
		return num / 20f;
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x0006F260 File Offset: 0x0006D460
	public void set(float pTime)
	{
		this.latest = pTime;
	}

	// Token: 0x04000D9F RID: 3487
	private const int MAXIMUM_VALUES = 20;

	// Token: 0x04000DA0 RID: 3488
	private Queue<float> results = new Queue<float>(20);

	// Token: 0x04000DA1 RID: 3489
	public float latest;

	// Token: 0x04000DA2 RID: 3490
	public float latestResult;
}
