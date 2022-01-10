using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x02000260 RID: 608
public class LogoButton : MonoBehaviour
{
	// Token: 0x06000D2B RID: 3371 RVA: 0x0007DCA6 File Offset: 0x0007BEA6
	private void Awake()
	{
		this.initScale = base.transform.localScale.x;
		this.loadLetters();
	}

	// Token: 0x06000D2C RID: 3372 RVA: 0x0007DCC4 File Offset: 0x0007BEC4
	private void loadLetters()
	{
		this.listLetters = new List<UiCreature>();
		Transform transform = base.transform.Find("Letters").transform;
		int childCount = transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			UiCreature component = transform.GetChild(i).GetComponent<UiCreature>();
			if (component.dropped)
			{
				component.resetPosition();
			}
			this.listLetters.Add(component);
		}
	}

	// Token: 0x06000D2D RID: 3373 RVA: 0x0007DD2C File Offset: 0x0007BF2C
	private void letterFall()
	{
		if (this.listLetters.Count == 0)
		{
			this.loadLetters();
			AchievementLibrary.achievementDestroyWorldBox.check();
			return;
		}
		this.listLetters.ShuffleOne<UiCreature>();
		UiCreature uiCreature = this.listLetters[0];
		this.listLetters.RemoveAt(0);
		uiCreature.click();
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x0007DD84 File Offset: 0x0007BF84
	public void clickLogo()
	{
		Sfx.play("explosion big", true, -1f, -1f);
		if (this.tweener != null && this.tweener.active)
		{
			TweenExtensions.Kill(this.tweener, false);
		}
		float num = this.initScale * 1.2f;
		if (this.listLetters.Count == 0)
		{
			num = 1.6f;
			base.transform.localScale = new Vector3(num, num, num);
			this.tweener = TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.transform, new Vector3(this.initScale, this.initScale, this.initScale), 0.3f), 27);
		}
		else
		{
			base.transform.localScale = new Vector3(num, num, num);
			this.tweener = TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.transform, new Vector3(this.initScale, this.initScale, this.initScale), 0.3f), 27);
		}
		this.letterFall();
	}

	// Token: 0x04001008 RID: 4104
	private List<UiCreature> listLetters;

	// Token: 0x04001009 RID: 4105
	private float initScale = 1f;

	// Token: 0x0400100A RID: 4106
	private Tweener tweener;
}
