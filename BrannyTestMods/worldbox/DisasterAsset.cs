using System;

// Token: 0x02000027 RID: 39
public class DisasterAsset : Asset
{
	// Token: 0x040000E6 RID: 230
	public DisasterAction action;

	// Token: 0x040000E7 RID: 231
	public int rate = 1;

	// Token: 0x040000E8 RID: 232
	public float chance = 1f;

	// Token: 0x040000E9 RID: 233
	public string world_log;

	// Token: 0x040000EA RID: 234
	public string world_log_icon;

	// Token: 0x040000EB RID: 235
	public int min_world_population;

	// Token: 0x040000EC RID: 236
	public bool premium_only;

	// Token: 0x040000ED RID: 237
	public DisasterType type = DisasterType.Other;
}
