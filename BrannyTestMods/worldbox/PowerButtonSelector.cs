using System;
using UnityEngine;

// Token: 0x020002E5 RID: 741
public class PowerButtonSelector : MonoBehaviour
{
	// Token: 0x0600100B RID: 4107 RVA: 0x0008D9C6 File Offset: 0x0008BBC6
	private void Awake()
	{
		PowerButtonSelector.instance = this;
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x0008D9CE File Offset: 0x0008BBCE
	private void Start()
	{
		this.clockButtMover.setVisible(false, true);
		this.sizeButtMover.setVisible(false, true);
		this.cancelButtMover.setVisible(false, true);
		this.bottomElementsMover.setVisible(false, true);
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x0008DA04 File Offset: 0x0008BC04
	internal void checkToggleIcons()
	{
		this.pauseButton.GetComponent<PauseButton>().updateSprite();
		foreach (PowerButton powerButton in PowerButton.toggleButtons)
		{
			powerButton.checkToggleIcon();
		}
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x0008DA64 File Offset: 0x0008BC64
	public virtual void setSelectedPower(PowerButton pButton, GodPower pPower, bool pAnim = false)
	{
		if (this.selectedButton == null)
		{
			return;
		}
		GodPower godPower = this.selectedButton.godPower;
		this.selectedButton.setSelectedPower(pButton, false);
		this.selectedButton.newClickAnimation();
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x0008DA9C File Offset: 0x0008BC9C
	public void setPower(PowerButton pButton)
	{
		this.selectedButton = pButton;
		if (this.selectedButton != null)
		{
			MapBox.instance.specialPowerAction(this.selectedButton.godPower);
			this.cancelButton.setIconFrom(this.selectedButton);
			LogText.log("Power Selected", pButton.godPower.id, "");
		}
		else
		{
			MapBox.instance.specialPowerAction(null);
		}
		DiscordTracker.setPower((pButton != null) ? pButton.godPower : null);
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x0008DB1C File Offset: 0x0008BD1C
	public void unselectTabs()
	{
		if (PowersTab.isTabSelected())
		{
			PowersTab.unselect();
		}
	}

	// Token: 0x06001011 RID: 4113 RVA: 0x0008DB2C File Offset: 0x0008BD2C
	public void unselectAll()
	{
		if (this.selectedButton != null)
		{
			if (this.selectedButton != null)
			{
				this.selectedButton.unselectActivePower();
			}
			this.setPower(null);
			this.buttonSelectionSprite.SetActive(false);
			WorldTip.instance.startHide();
		}
		if (Config.controllableUnit != null)
		{
			Config.controllableUnit.killHimself(true, AttackType.Other, true, true);
		}
		if (MoveCamera.focusUnit != null)
		{
			MoveCamera.focusUnit = null;
		}
	}

	// Token: 0x06001012 RID: 4114 RVA: 0x0008DBAB File Offset: 0x0008BDAB
	public bool isPowerSelected(string pPowerId)
	{
		return this.isPowerSelected() && this.selectedButton.godPower.id == pPowerId;
	}

	// Token: 0x06001013 RID: 4115 RVA: 0x0008DBCD File Offset: 0x0008BDCD
	public bool isPowerSelected()
	{
		return this.selectedButton != null;
	}

	// Token: 0x06001014 RID: 4116 RVA: 0x0008DBDC File Offset: 0x0008BDDC
	public void clickPowerButton(PowerButton pButton)
	{
		if (this.selectedButton == pButton)
		{
			this.unselectAll();
			return;
		}
		if (this.selectedButton != null)
		{
			this.selectedButton.unselectActivePower();
		}
		this.setPower(pButton);
		if (this.selectedButton != null)
		{
			this.selectedButton.selectActivePower();
			this.buttonSelectionSprite.SetActive(true);
			this.buttonSelectionSprite.transform.position = this.selectedButton.transform.position;
			this.buttonSelectionSprite.transform.SetParent(this.selectedButton.transform.parent);
			if (this.selectedButton.godPower != null)
			{
				Config.logSelectedPower(this.selectedButton.godPower);
			}
		}
		WorldTip.instance.showToolbarText(pButton.godPower);
		Analytics.LogEvent("select_power", "powerID", pButton.godPower.id);
	}

	// Token: 0x06001015 RID: 4117 RVA: 0x0008DCCC File Offset: 0x0008BECC
	private void Update()
	{
		if (this.selectedButton != null)
		{
			if (ScrollWindow.isWindowActive())
			{
				this.cancelButtMover.setVisible(false, false);
			}
			else
			{
				this.cancelButtMover.setVisible(true, false);
			}
		}
		else
		{
			this.cancelButtMover.setVisible(false, false);
		}
		if (this.selectedButton == null || ScrollWindow.isWindowActive())
		{
			this.sizeButtMover.setVisible(false, false);
			this.sizeButton.hideSizes();
			this.clockButtMover.setVisible(false, false);
			this.clockButton.hideSizes();
		}
		else
		{
			this.selectedButton.animate(Time.deltaTime);
			if (this.selectedButton.godPower.showToolSizes)
			{
				this.sizeButtMover.setVisible(true, false);
			}
			else
			{
				this.sizeButtMover.setVisible(false, false);
				this.sizeButton.hideSizes();
			}
			if (this.selectedButton.godPower.id == "clock")
			{
				this.clockButtMover.setVisible(true, false);
			}
			else
			{
				this.clockButtMover.setVisible(false, false);
				this.clockButton.hideSizes();
			}
		}
		if (ScrollWindow.isWindowActive() || Config.controllingUnit || Config.spectatorMode)
		{
			if (ScrollWindow.currentWindows.Count == 1)
			{
				this.bottomElementsMover.setVisible(false, false);
			}
			else
			{
				this.bottomElementsMover.setVisible(false, false);
			}
		}
		else
		{
			this.bottomElementsMover.setVisible(true, false);
		}
		if (!this.bottomElementsMover.visible)
		{
			CancelButton.goDown = Config.spectatorMode;
			CancelButton.goUp = (Config.controllingUnit && Config.joyControls);
			return;
		}
		CancelButton.goDown = false;
		CancelButton.goUp = false;
	}

	// Token: 0x0400132B RID: 4907
	public GameObject buttonSelectionSprite;

	// Token: 0x0400132C RID: 4908
	public GameObject buttonUnlocked;

	// Token: 0x0400132D RID: 4909
	public GameObject buttonUnlockedFlash;

	// Token: 0x0400132E RID: 4910
	public GameObject buttonUnlockedFlashNew;

	// Token: 0x0400132F RID: 4911
	public UiMover cancelButtMover;

	// Token: 0x04001330 RID: 4912
	public UiMover sizeButtMover;

	// Token: 0x04001331 RID: 4913
	public UiMover clockButtMover;

	// Token: 0x04001332 RID: 4914
	public UiMover bottomElementsMover;

	// Token: 0x04001333 RID: 4915
	public PowerButton clockButton;

	// Token: 0x04001334 RID: 4916
	public PowerButton sizeButton;

	// Token: 0x04001335 RID: 4917
	public CancelButton cancelButton;

	// Token: 0x04001336 RID: 4918
	public PowerButton pauseButton;

	// Token: 0x04001337 RID: 4919
	public PowerButton cityInfo;

	// Token: 0x04001338 RID: 4920
	public PowerButton cityZones;

	// Token: 0x04001339 RID: 4921
	public PowerButton boatMarks;

	// Token: 0x0400133A RID: 4922
	public PowerButton kingsAndLeaders;

	// Token: 0x0400133B RID: 4923
	public PowerButton historyLog;

	// Token: 0x0400133C RID: 4924
	public PowerButton followUnit;

	// Token: 0x0400133D RID: 4925
	internal PowerButton selectedButton;

	// Token: 0x0400133E RID: 4926
	public GameObject buttons;

	// Token: 0x0400133F RID: 4927
	internal static PowerButtonSelector instance;

	// Token: 0x04001340 RID: 4928
	private static int rewardCountdown = 10;
}
