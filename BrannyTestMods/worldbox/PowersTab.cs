using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020001C6 RID: 454
public class PowersTab : MonoBehaviour
{
	// Token: 0x06000A46 RID: 2630 RVA: 0x00068644 File Offset: 0x00066844
	private void Start()
	{
		this.parentObj = base.transform.parent.parent.gameObject;
		if (this.mainTab == this)
		{
			this.setActive(null);
			PowersTab.mainTabb = this.mainTab;
		}
		else
		{
			this.hideTab();
		}
		if (this.powerButton != null)
		{
			this.powerButton.OnHover(new UnityAction(this.showHoverTooltip));
			this.powerButton.OnHoverOut(new UnityAction(Tooltip.hideTooltip));
		}
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x000686D0 File Offset: 0x000668D0
	public static void showTabFromButton(Button pButtonTab)
	{
		pButtonTab.onClick.Invoke();
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x000686DD File Offset: 0x000668DD
	public static bool isTabSelected()
	{
		return PowersTab.currentTab != null;
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x000686EA File Offset: 0x000668EA
	public static void unselect()
	{
		PowersTab.currentTab.hideTab();
		PowersTab.mainTabb.setActive(null);
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x00068704 File Offset: 0x00066904
	public void showTab(Button pTabButton)
	{
		bool flag = false;
		if (PowersTab.currentTab != null)
		{
			if (PowersTab.currentTab == this)
			{
				flag = true;
			}
			PowersTab.currentTab.hideTab();
			PowersTab.currentTab = null;
		}
		if (flag)
		{
			PowersTab.mainTabb.setActive(null);
			return;
		}
		this.mainTab.hideTab();
		PowersTab.currentTab = this;
		PowersTab.currentTabButton = pTabButton;
		this.setActive(pTabButton);
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x0006876C File Offset: 0x0006696C
	private void setActive(Button pTabButton = null)
	{
		if (pTabButton != null)
		{
			pTabButton.image.sprite = this.image_selected;
		}
		base.gameObject.SetActive(true);
		base.gameObject.transform.localPosition = new Vector3(0f, -16f);
		this.setNewWidth();
		base.gameObject.transform.localScale = new Vector3(0.2f, 0.9f, 0.9f);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.gameObject.transform, 1f, 0.2f), 9);
		if (this != this.mainTab)
		{
			WorldTip.instance.showToolbarText(LocalizedTextManager.getText(this.tipKey, null));
		}
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x00068830 File Offset: 0x00066A30
	private void setNewWidth()
	{
		int childCount = base.transform.childCount;
		float num = 0f;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = base.transform.GetChild(i).gameObject;
			float num2 = gameObject.transform.localPosition.x + gameObject.GetComponent<RectTransform>().sizeDelta.x;
			if (num2 > num)
			{
				num = num2;
			}
		}
		RectTransform component = this.parentObj.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(num, component.sizeDelta.y);
		PowerTabController.instance.recalc(num);
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x000688D4 File Offset: 0x00066AD4
	private void showHoverTooltip()
	{
		if (!Config.tooltipsActive)
		{
			return;
		}
		Tooltip.info_tip = this.tipKey;
		Tooltip.instance.show(this.powerButton.gameObject, "tip", null, null);
		this.powerButton.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
		ShortcutExtensions.DOKill(this.powerButton.transform, false);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.powerButton.transform, 1f, 0.1f), 26);
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x00068968 File Offset: 0x00066B68
	public void hideTab()
	{
		this.completeHide();
		PowersTab.currentTab = null;
		if (PowersTab.currentTabButton != null)
		{
			PowersTab.currentTabButton.image.sprite = this.image_normal;
			PowersTab.currentTabButton = null;
		}
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x0006899E File Offset: 0x00066B9E
	private void completeHide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000CD8 RID: 3288
	public PowersTab mainTab;

	// Token: 0x04000CD9 RID: 3289
	private static PowersTab currentTab;

	// Token: 0x04000CDA RID: 3290
	private static Button currentTabButton;

	// Token: 0x04000CDB RID: 3291
	private static PowersTab mainTabb;

	// Token: 0x04000CDC RID: 3292
	private GameObject parentObj;

	// Token: 0x04000CDD RID: 3293
	public string tipKey;

	// Token: 0x04000CDE RID: 3294
	public Sprite image_normal;

	// Token: 0x04000CDF RID: 3295
	public Sprite image_selected;

	// Token: 0x04000CE0 RID: 3296
	public Button powerButton;
}
