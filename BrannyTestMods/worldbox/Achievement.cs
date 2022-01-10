using System;

// Token: 0x02000012 RID: 18
public class Achievement : Asset
{
	// Token: 0x04000044 RID: 68
	internal string playStoreID;

	// Token: 0x04000045 RID: 69
	internal bool hidden;

	// Token: 0x04000046 RID: 70
	internal string group = "misc";

	// Token: 0x04000047 RID: 71
	internal string icon;

	// Token: 0x04000048 RID: 72
	internal AchievementCheck check;

	// Token: 0x04000049 RID: 73
	internal AchievementCheckCity check_city;

	// Token: 0x0400004A RID: 74
	internal AchievementCheckActor check_actor;

	// Token: 0x0400004B RID: 75
	internal AchievementCheckGodPower check_godPower;
}
