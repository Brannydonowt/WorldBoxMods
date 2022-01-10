using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200027A RID: 634
public class AchievementGroup : MonoBehaviour
{
	// Token: 0x06000E00 RID: 3584 RVA: 0x00083D28 File Offset: 0x00081F28
	public void LoadGroup(AchievementGroupAsset pAchievementGroup)
	{
		this.title.GetComponent<LocalizedText>().key = "achievement_group_" + pAchievementGroup.id;
		this.title.GetComponent<LocalizedText>().updateText(true);
		int num = 0;
		if (pAchievementGroup.achievementList.Count > 0)
		{
			foreach (Achievement pAchievement in pAchievementGroup.achievementList)
			{
				AchievementButton achievementButton = Object.Instantiate<AchievementButton>(this.achievementButtonPrefab, this.transformContent);
				achievementButton.Load(pAchievement);
				if (AchievementLibrary.isUnlocked(pAchievement))
				{
					num++;
				}
				this.elements.Add(achievementButton);
			}
			this.counter.text = num.ToString() + " / " + pAchievementGroup.achievementList.Count.ToString();
		}
	}

	// Token: 0x040010CE RID: 4302
	public AchievementButton achievementButtonPrefab;

	// Token: 0x040010CF RID: 4303
	public List<AchievementButton> elements = new List<AchievementButton>();

	// Token: 0x040010D0 RID: 4304
	public Text title;

	// Token: 0x040010D1 RID: 4305
	public Text counter;

	// Token: 0x040010D2 RID: 4306
	public Transform transformContent;
}
