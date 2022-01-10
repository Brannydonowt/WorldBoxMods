using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200000B RID: 11
public class AchievementPopup : MonoBehaviour
{
	// Token: 0x06000048 RID: 72 RVA: 0x00005768 File Offset: 0x00003968
	internal static void show(Achievement pAchiev)
	{
		AchievementPopup.instance.show(pAchiev.id);
	}

	// Token: 0x06000049 RID: 73 RVA: 0x0000577A File Offset: 0x0000397A
	private void Awake()
	{
		AchievementPopup.instance = this;
		this.hide();
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00005788 File Offset: 0x00003988
	private void Update()
	{
		MapBox.instance.spawnCongratulationFireworks();
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00005794 File Offset: 0x00003994
	internal void show(string pID)
	{
		if (this.tween != null && this.tween.active)
		{
			return;
		}
		base.gameObject.SetActive(true);
		Achievement achievement = AssetManager.achievements.get(pID);
		Sprite sprite = (Sprite)Resources.Load("ui/Icons/achievements/" + achievement.icon, typeof(Sprite));
		if (sprite == null)
		{
			sprite = (Sprite)Resources.Load("ui/Icons/" + achievement.icon, typeof(Sprite));
		}
		if (sprite != null)
		{
			this.iconLeft.sprite = sprite;
			this.iconRight.sprite = sprite;
		}
		this.popupText.GetComponent<LocalizedText>().key = achievement.id;
		this.popupDescription.GetComponent<LocalizedText>().key = achievement.id + " Description";
		this.popupText.GetComponent<LocalizedText>().updateText(true);
		this.popupDescription.GetComponent<LocalizedText>().updateText(true);
		float num = ((float)Screen.height - Screen.safeArea.height) / CanvasMain.instance.canvas_ui.scaleFactor;
		this.tween = TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetDelay<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveY(base.transform, 0f - num, 1f, false), 27), 0.2f), new TweenCallback(this.tweenHide));
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00005900 File Offset: 0x00003B00
	public void forceHide()
	{
		if (this.tween != null)
		{
			TweenExtensions.Kill(this.tween, false);
		}
		this.tween = TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveY(base.transform, 100f, 0.5f, false), 27), new TweenCallback(this.hide));
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00005955 File Offset: 0x00003B55
	private void tweenHide()
	{
		this.tween = TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetDelay<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveY(base.transform, 100f, 1f, false), 4f), 27), new TweenCallback(this.hide));
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00005995 File Offset: 0x00003B95
	private void hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000035 RID: 53
	public static AchievementPopup instance;

	// Token: 0x04000036 RID: 54
	public Image background;

	// Token: 0x04000037 RID: 55
	public Image iconLeft;

	// Token: 0x04000038 RID: 56
	public Image iconRight;

	// Token: 0x04000039 RID: 57
	public Text popupText;

	// Token: 0x0400003A RID: 58
	public Text popupDescription;

	// Token: 0x0400003B RID: 59
	private Tweener tween;
}
