using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020002E4 RID: 740
public class PowerButton : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IInitializePotentialDragHandler, IScrollHandler
{
	// Token: 0x1700003F RID: 63
	// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x0008CD54 File Offset: 0x0008AF54
	private PowerButtonSelector selectedButtons
	{
		get
		{
			return PowerButtonSelector.instance;
		}
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x0008CD5C File Offset: 0x0008AF5C
	public static PowerButton get(string pID)
	{
		for (int i = 0; i < PowerButton.powerButtons.Count; i++)
		{
			PowerButton powerButton = PowerButton.powerButtons[i];
			if (powerButton.gameObject.name == pID)
			{
				return powerButton;
			}
		}
		return null;
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x0008CDA0 File Offset: 0x0008AFA0
	private void init()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.image = base.GetComponent<Image>();
		this.button = base.GetComponent<Button>();
		if (this.type == PowerButtonType.Special || this.type == PowerButtonType.Active)
		{
			PowerButton.powerButtons.Add(this);
			this.godPower = AssetManager.powers.dict[base.gameObject.name];
		}
		else if (this.type == PowerButtonType.Shop)
		{
			this.godPower = AssetManager.powers.dict[base.gameObject.name];
		}
		if (this.godPower != null)
		{
			if (this.type == PowerButtonType.Active)
			{
				GodPower.addPower(this.godPower, this);
			}
			if (this.godPower.toggle_action != null)
			{
				PowerButton.toggleButtons.Add(this);
			}
		}
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x0008CE70 File Offset: 0x0008B070
	private void OnEnable()
	{
		if (this.initialized)
		{
			return;
		}
		this.initialized = true;
		this.init();
		this.defaultScale = base.transform.localScale;
		this.clickedScale = this.defaultScale * 0.9f;
		if (this.type == PowerButtonType.Active && this.godPower != null)
		{
			if (base.gameObject.name.Contains("Button"))
			{
				Color color = new Color(0.5f, 0.5f, 0.5f, 1f);
				this.image.color = color;
				this.icon.color = color;
			}
			else
			{
				this.godPower.id = base.gameObject.transform.name;
			}
		}
		this.button.OnHover(new UnityAction(this.showTooltip));
		this.button.OnHoverOut(new UnityAction(Tooltip.hideTooltip));
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x0008CF60 File Offset: 0x0008B160
	public void OnPointerClick(PointerEventData eventData)
	{
		if (this.draggindBarEnabled() && ScrollRectExtended.instance.isDragged())
		{
			return;
		}
		this.newClickAnimation();
		this.playSound();
		DiscordTracker.trackPower(this.getText());
		if (this.type == PowerButtonType.Active)
		{
			this.clickActivePower();
		}
		if (this.type == PowerButtonType.BrushSizeMain)
		{
			this.clickSizeMainTool();
		}
		if (this.type == PowerButtonType.TimeScale)
		{
			this.clickTimeScaleTool();
		}
		if (this.type == PowerButtonType.Shop)
		{
			this.clickShop();
		}
		if (this.type == PowerButtonType.Special)
		{
			this.clickSpecial();
		}
		if (this.type == PowerButtonType.Window)
		{
			this.clickOpenWindow();
		}
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x0008CFF4 File Offset: 0x0008B1F4
	private void showTooltip()
	{
		if (!Config.tooltipsActive)
		{
			return;
		}
		if (this.godPower != null)
		{
			Tooltip.instance.show(base.gameObject, "normal", this.godPower.name, this.godPower.name + " Description");
		}
		else
		{
			string text = this.getText();
			string description = this.getDescription();
			if (text == "")
			{
				return;
			}
			if (description != "")
			{
				Tooltip.info_tip = text;
				Tooltip.instance.show(base.gameObject, "normal", text, description);
			}
			else
			{
				Tooltip.info_tip = text;
				Tooltip.instance.show(base.gameObject, "tip", null, null);
			}
		}
		base.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
		ShortcutExtensions.DOKill(base.transform, false);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.transform, 1f, 0.1f), 26);
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x0008D0F8 File Offset: 0x0008B2F8
	private string getText()
	{
		if (!string.IsNullOrEmpty(this.custom_tooltip_name))
		{
			return this.custom_tooltip_name;
		}
		if (this.godPower != null)
		{
			return this.godPower.name;
		}
		if (LocalizedTextManager.stringExists("Button " + this.button.name))
		{
			return "Button " + this.button.name;
		}
		if (LocalizedTextManager.stringExists(this.button.name))
		{
			return this.button.name;
		}
		return "";
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x0008D184 File Offset: 0x0008B384
	private string getDescription()
	{
		if (!string.IsNullOrEmpty(this.custom_tooltip_description))
		{
			return this.custom_tooltip_description;
		}
		if (this.godPower != null)
		{
			return this.godPower.name + " Description";
		}
		if (LocalizedTextManager.stringExists("Button " + this.button.name + " Description"))
		{
			return "Button " + this.button.name + " Description";
		}
		if (LocalizedTextManager.stringExists(this.button.name + " Description"))
		{
			return this.button.name + " Description";
		}
		return "";
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x0008D236 File Offset: 0x0008B436
	private void clickOpenWindow()
	{
		if (this.open_window_id == "steam" && (Config.isComputer || Config.isEditor))
		{
			this.open_window_id = "steam_workshop_main";
		}
		this.showWindow(this.open_window_id);
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x0008D270 File Offset: 0x0008B470
	private void clickSpecial()
	{
		Analytics.LogEvent("select_power", "powerID", this.godPower.id);
		if (this.godPower.id == "pause")
		{
			base.GetComponent<PauseButton>().press();
		}
		if (this.godPower.toggle_action != null)
		{
			this.godPower.toggle_action(this.godPower.id);
			PowerButtonSelector.instance.checkToggleIcons();
		}
	}

	// Token: 0x06000FF0 RID: 4080 RVA: 0x0008D2EC File Offset: 0x0008B4EC
	public void checkToggleIcon()
	{
		if (this.godPower == null)
		{
			return;
		}
		if (string.IsNullOrEmpty(this.godPower.toggle_name))
		{
			return;
		}
		base.transform.Find("ToggleIcon").GetComponent<ToggleIcon>().updateIcon(PlayerConfig.optionBoolEnabled(this.godPower.toggle_name));
	}

	// Token: 0x06000FF1 RID: 4081 RVA: 0x0008D33F File Offset: 0x0008B53F
	private void playSound()
	{
		Sfx.play("click", true, -1f, -1f);
	}

	// Token: 0x06000FF2 RID: 4082 RVA: 0x0008D358 File Offset: 0x0008B558
	public void checkLockIcon()
	{
		if (this.type == PowerButtonType.Shop)
		{
			return;
		}
		if (this.godPower != null && this.godPower.requiresPremium)
		{
			if (this.iconLock == null)
			{
				this.iconLock = Object.Instantiate<Image>(PrefabLibrary.instance.iconLock, base.transform);
				this.iconLock.enabled = true;
			}
			if (this.buttonUnlocked == null)
			{
				this.buttonUnlocked = Object.Instantiate<GameObject>(PowerButtonSelector.instance.buttonUnlockedFlashNew, base.transform);
				this.buttonUnlocked.transform.position = base.transform.position;
				this.buttonUnlocked.SetActive(false);
				this.buttonUnlocked.transform.SetSiblingIndex(0);
			}
			if (this.buttonUnlockedFlash == null)
			{
				this.buttonUnlockedFlash = Object.Instantiate<GameObject>(PowerButtonSelector.instance.buttonUnlockedFlash, base.transform);
				this.buttonUnlockedFlash.transform.position = base.transform.position;
				this.buttonUnlockedFlash.SetActive(false);
				this.buttonUnlockedFlash.transform.SetSiblingIndex(0);
			}
		}
		if (this.iconLock == null)
		{
			return;
		}
		if (this.godPower == null)
		{
			this.iconLock.enabled = false;
			return;
		}
		if (Config.havePremium)
		{
			this.iconLock.enabled = false;
			return;
		}
		this.iconLock.enabled = true;
	}

	// Token: 0x06000FF3 RID: 4083 RVA: 0x0008D4C5 File Offset: 0x0008B6C5
	public void showOthers()
	{
		Debug.Log("other");
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x0008D4D1 File Offset: 0x0008B6D1
	public void cancelSelection()
	{
		this.selectedButtons.unselectAll();
	}

	// Token: 0x06000FF5 RID: 4085 RVA: 0x0008D4DE File Offset: 0x0008B6DE
	public void selectActivePower()
	{
		this.selected = true;
	}

	// Token: 0x06000FF6 RID: 4086 RVA: 0x0008D4E8 File Offset: 0x0008B6E8
	public void unselectActivePower()
	{
		this.selected = false;
		if (base.gameObject.activeSelf && base.transform.parent.gameObject.activeSelf)
		{
			base.StartCoroutine(this.angleToZero());
			return;
		}
		this.icon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x0008D552 File Offset: 0x0008B752
	public void hideSizes()
	{
		this.sizeButtons.SetActive(false);
	}

	// Token: 0x06000FF8 RID: 4088 RVA: 0x0008D560 File Offset: 0x0008B760
	private void clickSizeMainTool()
	{
		this.sizeButtons.SetActive(!this.sizeButtons.activeSelf);
	}

	// Token: 0x06000FF9 RID: 4089 RVA: 0x0008D57C File Offset: 0x0008B77C
	public void clickTimeScaleTool()
	{
		this.sizeButtons.SetActive(false);
		this.mainSizeButton.icon.sprite = this.icon.sprite;
		if (base.transform.name == "x5")
		{
			Config.timeScale = 5f;
		}
		else if (base.transform.name == "x3")
		{
			Config.timeScale = 3f;
		}
		else if (base.transform.name == "x2")
		{
			Config.timeScale = 2f;
		}
		else
		{
			Config.timeScale = 1f;
		}
		this.mainSizeButton.newClickAnimation();
		MapBox.instance.inspectTimerClick = 1f;
	}

	// Token: 0x06000FFA RID: 4090 RVA: 0x0008D63D File Offset: 0x0008B83D
	private void clickActivePower()
	{
		this.selectedButtons.clickPowerButton(this);
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x0008D64C File Offset: 0x0008B84C
	private void clickShop()
	{
		WorldTip.showNow(LocalizedTextManager.getText(this.godPower.name, null) + "\n" + LocalizedTextManager.getText(this.godPower.name + " Description", null), false, "top", 3f);
	}

	// Token: 0x06000FFC RID: 4092 RVA: 0x0008D69F File Offset: 0x0008B89F
	public void setSelectedPower(PowerButton pLibraryButton, bool pAnim = false)
	{
		this.godPower = pLibraryButton.godPower;
	}

	// Token: 0x06000FFD RID: 4093 RVA: 0x0008D6AD File Offset: 0x0008B8AD
	public void newClickAnimation()
	{
		base.gameObject.transform.localScale = this.clickedScale;
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.gameObject.transform, this.defaultScale, 0.1f), 28);
	}

	// Token: 0x06000FFE RID: 4094 RVA: 0x0008D6E8 File Offset: 0x0008B8E8
	protected IEnumerator angleToZero()
	{
		while (this.icon.transform.localEulerAngles.z != 0f)
		{
			Vector3 localEulerAngles = this.icon.transform.localEulerAngles;
			localEulerAngles.z -= 100f * Time.deltaTime;
			if (localEulerAngles.z < 0f)
			{
				localEulerAngles.z = 0f;
			}
			this.icon.transform.localEulerAngles = localEulerAngles;
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x0008D6F8 File Offset: 0x0008B8F8
	public void animate(float pElapsed)
	{
		if (this.icon.transform.localEulerAngles.z < 20f)
		{
			Vector3 localEulerAngles = this.icon.transform.localEulerAngles;
			localEulerAngles.z += 100f * pElapsed;
			if (localEulerAngles.z > 20f)
			{
				localEulerAngles.z = 20f;
			}
			this.icon.transform.localEulerAngles = localEulerAngles;
		}
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x0008D770 File Offset: 0x0008B970
	public void destroyLockIcon()
	{
		if (this.buttonUnlocked != null)
		{
			Object.Destroy(this.buttonUnlocked);
		}
		if (this.buttonUnlockedFlash != null)
		{
			Object.Destroy(this.buttonUnlockedFlash);
		}
		if (this.iconLock != null)
		{
			Object.Destroy(this.iconLock.gameObject);
			return;
		}
		Transform transform = base.transform.Find("IconLock");
		if (transform != null)
		{
			Object.Destroy(transform.gameObject);
			return;
		}
		transform = base.transform.Find("IconLock(Clone)");
		if (transform != null)
		{
			Object.Destroy(transform.gameObject);
		}
	}

	// Token: 0x06001001 RID: 4097 RVA: 0x0008D81B File Offset: 0x0008BA1B
	public void showWindow(string pID)
	{
		ScrollWindow.showWindow(pID);
	}

	// Token: 0x06001002 RID: 4098 RVA: 0x0008D824 File Offset: 0x0008BA24
	public void selectPowerTab()
	{
		if (!base.transform.parent.gameObject.activeInHierarchy)
		{
			Button tabForTabGroup = PowerTabController.instance.getTabForTabGroup(base.transform.parent.name);
			if (tabForTabGroup != null)
			{
				PowersTab.showTabFromButton(tabForTabGroup);
			}
		}
		float num = (float)Screen.width / CanvasMain.instance.canvas_ui.scaleFactor;
		float num2 = -base.transform.localPosition.x + 32f + num / 2f;
		if (num2 > 0f)
		{
			num2 = 0f;
		}
		TweenSettingsExtensions.SetDelay<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMoveX(base.transform.parent.parent.parent.transform, num2, 1.5f, false), 27), 0.3f);
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x0008D8EF File Offset: 0x0008BAEF
	private bool draggindBarEnabled()
	{
		return this.drag_power_bar && Input.mousePresent;
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x0008D900 File Offset: 0x0008BB00
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!this.draggindBarEnabled())
		{
			return;
		}
		ScrollRectExtended.instance.SendMessage("OnBeginDrag", eventData);
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x0008D91B File Offset: 0x0008BB1B
	public void OnDrag(PointerEventData eventData)
	{
		if (!this.draggindBarEnabled())
		{
			return;
		}
		ScrollRectExtended.instance.SendMessage("OnDrag", eventData);
	}

	// Token: 0x06001006 RID: 4102 RVA: 0x0008D936 File Offset: 0x0008BB36
	public void OnEndDrag(PointerEventData eventData)
	{
		if (!this.draggindBarEnabled())
		{
			return;
		}
		ScrollRectExtended.instance.SendMessage("OnEndDrag", eventData);
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x0008D951 File Offset: 0x0008BB51
	public void OnInitializePotentialDrag(PointerEventData eventData)
	{
		if (!this.draggindBarEnabled())
		{
			return;
		}
		ScrollRectExtended.instance.SendMessage("OnInitializePotentialDrag", eventData);
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x0008D96C File Offset: 0x0008BB6C
	public void OnScroll(PointerEventData eventData)
	{
		if (!this.draggindBarEnabled())
		{
			return;
		}
		ScrollRectExtended.instance.SendMessage("OnScroll", eventData);
	}

	// Token: 0x04001316 RID: 4886
	public bool drag_power_bar;

	// Token: 0x04001317 RID: 4887
	public string open_window_id = string.Empty;

	// Token: 0x04001318 RID: 4888
	public string custom_tooltip_name = string.Empty;

	// Token: 0x04001319 RID: 4889
	public string custom_tooltip_description = string.Empty;

	// Token: 0x0400131A RID: 4890
	protected bool selected;

	// Token: 0x0400131B RID: 4891
	public Image icon;

	// Token: 0x0400131C RID: 4892
	private Image image;

	// Token: 0x0400131D RID: 4893
	private Button button;

	// Token: 0x0400131E RID: 4894
	public PowerButtonType type;

	// Token: 0x0400131F RID: 4895
	public GameObject sizeButtons;

	// Token: 0x04001320 RID: 4896
	public PowerButton mainSizeButton;

	// Token: 0x04001321 RID: 4897
	internal GodPower godPower;

	// Token: 0x04001322 RID: 4898
	private Image iconLock;

	// Token: 0x04001323 RID: 4899
	public GameObject buttonUnlocked;

	// Token: 0x04001324 RID: 4900
	public GameObject buttonUnlockedFlash;

	// Token: 0x04001325 RID: 4901
	public static List<PowerButton> powerButtons = new List<PowerButton>();

	// Token: 0x04001326 RID: 4902
	public static List<PowerButton> toggleButtons = new List<PowerButton>();

	// Token: 0x04001327 RID: 4903
	internal RectTransform rectTransform;

	// Token: 0x04001328 RID: 4904
	private Vector3 defaultScale;

	// Token: 0x04001329 RID: 4905
	private Vector3 clickedScale;

	// Token: 0x0400132A RID: 4906
	private bool initialized;
}
