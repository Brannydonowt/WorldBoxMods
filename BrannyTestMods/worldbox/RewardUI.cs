using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E8 RID: 488
public class RewardUI : MonoBehaviour
{
	// Token: 0x06000B1F RID: 2847 RVA: 0x0006CFF3 File Offset: 0x0006B1F3
	internal void setRewardInfo(List<PowerButton> pButtons)
	{
		this.rewardPowers = pButtons;
		this.nextReward();
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x0006D002 File Offset: 0x0006B202
	internal bool hasRewards()
	{
		return this.rewardPowers != null && this.rewardPowers.Count > 0;
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x0006D01C File Offset: 0x0006B21C
	internal PowerButton popLowestReward()
	{
		int num = 10000;
		int num2 = 0;
		int num3 = 0;
		foreach (PowerButton powerButton in this.rewardPowers)
		{
			if (powerButton.godPower.rank < (PowerRank)num)
			{
				num2 = num3;
				num = (int)powerButton.godPower.rank;
			}
			num3++;
		}
		PowerButton result = this.rewardPowers[num2];
		this.rewardPowers.RemoveAt(num2);
		return result;
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x0006D0B0 File Offset: 0x0006B2B0
	internal void nextReward()
	{
		if (!this.hasRewards())
		{
			return;
		}
		PowerButton powerButton = this.popLowestReward();
		this.powerSprite.sprite = powerButton.icon.sprite;
		this.text.GetComponent<LocalizedText>().key = powerButton.godPower.name;
		this.text.GetComponent<LocalizedText>().updateText(true);
		this.text_description.gameObject.SetActive(true);
		this.text_description.GetComponent<LocalizedText>().key = powerButton.godPower.name + " Description";
		this.text_description.GetComponent<LocalizedText>().updateText(true);
		if (powerButton.godPower.id == "clock")
		{
			this.window_title.GetComponent<LocalizedText>().key = "free_hourglass_title";
			this.free_power_unlocked.GetComponent<LocalizedText>().key = "free_hourglass_unlocked";
			this.rewardAnimation.quickReward = true;
		}
		else
		{
			this.window_title.GetComponent<LocalizedText>().key = "free_power";
			this.free_power_unlocked.GetComponent<LocalizedText>().key = "free_power_unlocked";
			this.rewardAnimation.quickReward = false;
		}
		PlayerConfig.instance.data.lastReward = powerButton.godPower.id;
		this.window_title.GetComponent<LocalizedText>().updateText(true);
		this.free_power_unlocked.GetComponent<LocalizedText>().updateText(true);
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x0006D218 File Offset: 0x0006B418
	public void bottomButtonClick()
	{
		if (this.rewardAnimation.state != RewardAnimationState.Open)
		{
			if (this.rewardAnimation.state == RewardAnimationState.Idle)
			{
				this.rewardAnimation.clickAnimation();
			}
			return;
		}
		if (this.hasRewards())
		{
			this.rewardAnimation.resetAnim();
			this.nextReward();
			return;
		}
		base.GetComponent<ButtonEvent>().hideRewardWindowAndHighlightPower();
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x0006D274 File Offset: 0x0006B474
	internal void setRewardInfo(string pSprite, string pText)
	{
		this.powerSprite.sprite = (Sprite)Resources.Load("ui/Icons/" + pSprite, typeof(Sprite));
		this.text.GetComponent<LocalizedText>().key = pText;
		this.text.GetComponent<LocalizedText>().updateText(true);
		this.text_description.gameObject.SetActive(false);
		this.window_title.GetComponent<LocalizedText>().key = "free_saveslots_title";
		this.window_title.GetComponent<LocalizedText>().updateText(true);
		this.free_power_unlocked.GetComponent<LocalizedText>().key = "free_saveslots_unlocked";
		this.free_power_unlocked.GetComponent<LocalizedText>().updateText(true);
		this.rewardAnimation.quickReward = true;
	}

	// Token: 0x04000D79 RID: 3449
	public Image powerSprite;

	// Token: 0x04000D7A RID: 3450
	public Text text;

	// Token: 0x04000D7B RID: 3451
	public Text text_description;

	// Token: 0x04000D7C RID: 3452
	public Text window_title;

	// Token: 0x04000D7D RID: 3453
	public Text free_power_unlocked;

	// Token: 0x04000D7E RID: 3454
	public List<PowerButton> rewardPowers;

	// Token: 0x04000D7F RID: 3455
	public RewardAnimation rewardAnimation;
}
