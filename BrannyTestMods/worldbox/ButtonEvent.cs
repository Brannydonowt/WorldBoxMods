using System;
using UnityEngine;

// Token: 0x0200024D RID: 589
public class ButtonEvent : MonoBehaviour
{
	// Token: 0x06000CC7 RID: 3271 RVA: 0x0007B60A File Offset: 0x0007980A
	public void clickGenerateMap(string pValue)
	{
		MapBox.instance.clickGenerateNewMap(pValue);
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x0007B617 File Offset: 0x00079817
	public void clickPremiumButton()
	{
		ScrollWindow.showWindow("steam");
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x0007B623 File Offset: 0x00079823
	public void clickRewardAds()
	{
		if (ScrollWindow.currentWindows.Count > 0 && ScrollWindow.currentWindows[0].name == "reward_ads")
		{
			return;
		}
		ScrollWindow.showWindow("reward_ads");
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x0007B659 File Offset: 0x00079859
	public void showWindow(string pID)
	{
		ScrollWindow.showWindow(pID);
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x0007B661 File Offset: 0x00079861
	public void locateSelectedVillage()
	{
		MapBox.instance.locateSelectedVillage();
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x0007B66D File Offset: 0x0007986D
	public void locateSelectedUnit()
	{
		MapBox.instance.locateSelectedUnit();
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x0007B679 File Offset: 0x00079879
	public void startLoadSaveSlot()
	{
		AutoSaveManager.autoSave(true);
		MapBox.instance.saveManager.startLoadSlot();
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x0007B690 File Offset: 0x00079890
	public void clickSaveSlot()
	{
		AutoSaveManager.autoSave(false);
		MapBox.instance.saveManager.clickSaveSlot();
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x0007B6A7 File Offset: 0x000798A7
	public void confirmDeleteWorld()
	{
		SaveManager.deleteCurrentSave();
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x0007B6AE File Offset: 0x000798AE
	public void startTutorialBear()
	{
		MapBox.instance.tutorial.startTutorial();
	}

	// Token: 0x06000CD1 RID: 3281 RVA: 0x0007B6BF File Offset: 0x000798BF
	public void showRewardedAd()
	{
		PlayerConfig.instance.data.powerReward = string.Empty;
		if (!Config.isMobile && !Config.isEditor)
		{
			return;
		}
		RewardedAds.instance.ShowRewardedAd("gift");
	}

	// Token: 0x06000CD2 RID: 3282 RVA: 0x0007B6F3 File Offset: 0x000798F3
	public void showRewardedPowerAd()
	{
		PlayerConfig.instance.data.powerReward = Config.powerToUnlock.id;
		if (!Config.isMobile && !Config.isEditor)
		{
			return;
		}
		RewardedAds.instance.ShowRewardedAd("power");
	}

	// Token: 0x06000CD3 RID: 3283 RVA: 0x0007B72C File Offset: 0x0007992C
	public void showRewardedClockAd()
	{
		PlayerConfig.instance.data.powerReward = "clock";
		if (!Config.isMobile && !Config.isEditor)
		{
			return;
		}
		RewardedAds.instance.ShowRewardedAd("clock");
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x0007B760 File Offset: 0x00079960
	public void showRewardedSaveSlotAd()
	{
		PlayerConfig.instance.data.powerReward = "saveslots";
		if (!Config.isMobile && !Config.isEditor)
		{
			return;
		}
		RewardedAds.instance.ShowRewardedAd("save_slot");
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x0007B794 File Offset: 0x00079994
	public void hideRewardWindowAndHighlightPower()
	{
		if (ScrollWindow.get("reward_ads_received").currentWindow)
		{
			ScrollWindow.get("reward_ads_received").clickHide("right");
			if (PlayerConfig.instance.data.lastReward != string.Empty)
			{
				if (PlayerConfig.instance.data.lastReward.StartsWith("saveslots", StringComparison.Ordinal))
				{
					ScrollWindow.showWindow("saves_list");
				}
				else
				{
					PowerButton powerButton = PowerButton.get(PlayerConfig.instance.data.lastReward);
					if (powerButton == null)
					{
						return;
					}
					powerButton.selectPowerTab();
				}
				PlayerConfig.instance.data.lastReward = string.Empty;
			}
		}
	}

	// Token: 0x06000CD6 RID: 3286 RVA: 0x0007B845 File Offset: 0x00079A45
	public void restorePurchasesIOS()
	{
		InAppManager.instance.RestorePurchases();
	}
}
