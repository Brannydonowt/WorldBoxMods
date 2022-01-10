using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x02000294 RID: 660
public class TopPremiumButtonMover : MonoBehaviour
{
	// Token: 0x06000E91 RID: 3729 RVA: 0x0008783C File Offset: 0x00085A3C
	private void Update()
	{
		if (this.shouldShow())
		{
			if (this.target_pos != this.pos_show)
			{
				this.target_pos = this.pos_show;
				this.updateRandomText();
				base.transform.GetComponentInChildren<LocalizedTextPrice>().updateText(true);
				ShortcutExtensions.DOLocalMoveY(base.transform, this.target_pos, 0.5f, false);
				return;
			}
		}
		else if (this.target_pos != this.pos_hide)
		{
			this.target_pos = this.pos_hide;
			ShortcutExtensions.DOLocalMoveY(base.transform, this.target_pos, 0.5f, false);
		}
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x000878D0 File Offset: 0x00085AD0
	private void updateRandomText()
	{
		int num = Toolbox.randomInt(1, 5);
		if (num > 1)
		{
			this.button_text.key = "premium_get_it_" + num.ToString();
		}
		this.button_text.updateText(true);
	}

	// Token: 0x06000E93 RID: 3731 RVA: 0x00087914 File Offset: 0x00085B14
	private bool shouldShow()
	{
		bool result = false;
		if (Config.havePremium)
		{
			return false;
		}
		string selectedPower = MapBox.instance.getSelectedPower();
		if (!string.IsNullOrEmpty(selectedPower) && AssetManager.powers.get(selectedPower).requiresPremium)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x0400116C RID: 4460
	public LocalizedText button_text;

	// Token: 0x0400116D RID: 4461
	private float target_pos = -1f;

	// Token: 0x0400116E RID: 4462
	private float pos_hide;

	// Token: 0x0400116F RID: 4463
	private float pos_show = -45f;

	// Token: 0x04001170 RID: 4464
	private DOTween _tween;
}
