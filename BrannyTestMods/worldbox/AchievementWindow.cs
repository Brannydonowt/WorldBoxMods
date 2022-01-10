using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200027B RID: 635
public class AchievementWindow : MonoBehaviour
{
	// Token: 0x06000E02 RID: 3586 RVA: 0x00083E2F File Offset: 0x0008202F
	private void OnEnable()
	{
		this.showList();
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x00083E38 File Offset: 0x00082038
	internal void showList()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		for (int i = 0; i < this.elements.Count; i++)
		{
			Object.Destroy(this.elements[i].gameObject);
		}
		this.elements.Clear();
		foreach (AchievementGroupAsset pAchievementGroup in AssetManager.achievementGroups.list)
		{
			this.showElement(pAchievementGroup);
		}
		this.updateTotalBar();
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x00083ED4 File Offset: 0x000820D4
	private void updateTotalBar()
	{
		int count = AssetManager.achievements.list.Count;
		int num = 0;
		using (List<Achievement>.Enumerator enumerator = AssetManager.achievements.list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (AchievementLibrary.isUnlocked(enumerator.Current.id))
				{
					num++;
				}
			}
		}
		this.achievementBar.setBar((float)num, (float)count, num.ToString() + "/" + count.ToString());
	}

	// Token: 0x06000E05 RID: 3589 RVA: 0x00083F6C File Offset: 0x0008216C
	private void showElement(AchievementGroupAsset pAchievementGroup)
	{
		AchievementGroup achievementGroup = Object.Instantiate<AchievementGroup>(this.achievementGroupPrefab, this.transformContent);
		achievementGroup.LoadGroup(pAchievementGroup);
		this.elements.Add(achievementGroup);
	}

	// Token: 0x040010D3 RID: 4307
	public AchievementGroup achievementGroupPrefab;

	// Token: 0x040010D4 RID: 4308
	private List<AchievementGroup> elements = new List<AchievementGroup>();

	// Token: 0x040010D5 RID: 4309
	public Transform transformContent;

	// Token: 0x040010D6 RID: 4310
	public StatBar achievementBar;
}
