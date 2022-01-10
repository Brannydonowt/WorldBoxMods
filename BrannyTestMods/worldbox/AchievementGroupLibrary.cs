using System;
using Beebyte.Obfuscator;

// Token: 0x0200000D RID: 13
[ObfuscateLiterals]
[Serializable]
public class AchievementGroupLibrary : AssetLibrary<AchievementGroupAsset>
{
	// Token: 0x06000051 RID: 81 RVA: 0x000059C0 File Offset: 0x00003BC0
	public override void init()
	{
		base.init();
		AchievementGroupLibrary.creation = this.add(new AchievementGroupAsset
		{
			id = "creation"
		});
		AchievementGroupLibrary.time = this.add(new AchievementGroupAsset
		{
			id = "civilizations"
		});
		AchievementGroupLibrary.creatures = this.add(new AchievementGroupAsset
		{
			id = "creatures"
		});
		AchievementGroupLibrary.destruction = this.add(new AchievementGroupAsset
		{
			id = "destruction"
		});
		AchievementGroupLibrary.nature = this.add(new AchievementGroupAsset
		{
			id = "nature"
		});
		AchievementGroupLibrary.miscellaneous = this.add(new AchievementGroupAsset
		{
			id = "miscellaneous"
		});
		this.LoadAchievements();
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00005A7C File Offset: 0x00003C7C
	public void LoadAchievements()
	{
		foreach (Achievement achievement in AssetManager.achievements.list)
		{
			this.dict[achievement.group].achievementList.Add(achievement);
		}
	}

	// Token: 0x0400003D RID: 61
	public static AchievementGroupAsset creation;

	// Token: 0x0400003E RID: 62
	public static AchievementGroupAsset destruction;

	// Token: 0x0400003F RID: 63
	public static AchievementGroupAsset nature;

	// Token: 0x04000040 RID: 64
	public static AchievementGroupAsset time;

	// Token: 0x04000041 RID: 65
	public static AchievementGroupAsset miscellaneous;

	// Token: 0x04000042 RID: 66
	public static AchievementGroupAsset civilizations;

	// Token: 0x04000043 RID: 67
	public static AchievementGroupAsset creatures;
}
