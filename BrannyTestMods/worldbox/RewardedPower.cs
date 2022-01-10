using System;

// Token: 0x020001C4 RID: 452
[Serializable]
public class RewardedPower
{
	// Token: 0x06000A41 RID: 2625 RVA: 0x00068215 File Offset: 0x00066415
	public RewardedPower(string pName, double pTimeStamp)
	{
		this.name = pName;
		this.timeStamp = pTimeStamp;
	}

	// Token: 0x04000CC2 RID: 3266
	public string name;

	// Token: 0x04000CC3 RID: 3267
	public double timeStamp;
}
