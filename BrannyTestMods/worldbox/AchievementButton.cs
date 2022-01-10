using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000279 RID: 633
public class AchievementButton : MonoBehaviour
{
	// Token: 0x06000DFB RID: 3579 RVA: 0x00083B28 File Offset: 0x00081D28
	public void Load(Achievement pAchievement)
	{
		this.achievement = pAchievement;
		Sprite sprite = (Sprite)Resources.Load("ui/Icons/achievements/" + this.achievement.icon, typeof(Sprite));
		if (sprite == null)
		{
			sprite = (Sprite)Resources.Load("ui/Icons/" + this.achievement.icon, typeof(Sprite));
		}
		if (sprite != null)
		{
			this.achievementIcon.sprite = sprite;
			if (!AchievementLibrary.isUnlocked(this.achievement))
			{
				this.achievementIcon.color = Color.black;
				this.achievementBackDefault.SetActive(true);
				this.medalIcon.SetActive(false);
			}
		}
	}

	// Token: 0x06000DFC RID: 3580 RVA: 0x00083BE4 File Offset: 0x00081DE4
	private void Start()
	{
		base.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
		Button component = base.GetComponent<Button>();
		component.onClick.AddListener(new UnityAction(this.showTooltip));
		component.OnHover(new UnityAction(this.showHoverTooltip));
		component.OnHoverOut(new UnityAction(Tooltip.hideTooltip));
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x00083C50 File Offset: 0x00081E50
	private void showHoverTooltip()
	{
		bool tooltipsActive = Config.tooltipsActive;
		this.showTooltip();
	}

	// Token: 0x06000DFE RID: 3582 RVA: 0x00083C60 File Offset: 0x00081E60
	private void showTooltip()
	{
		bool flag = AchievementLibrary.isUnlocked(this.achievement.id);
		string id = this.achievement.id;
		string pDescription = string.Empty;
		if (this.achievement.hidden && !flag)
		{
			pDescription = "achievement_tip_hidden";
		}
		else
		{
			pDescription = this.achievement.id + " Description";
		}
		Tooltip.instance.show(base.gameObject, "normal", id, pDescription);
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		ShortcutExtensions.DOKill(base.transform, false);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.transform, 0.8f, 0.1f), 26);
	}

	// Token: 0x040010CA RID: 4298
	internal Achievement achievement;

	// Token: 0x040010CB RID: 4299
	public Image achievementIcon;

	// Token: 0x040010CC RID: 4300
	public GameObject achievementBackDefault;

	// Token: 0x040010CD RID: 4301
	public GameObject medalIcon;
}
