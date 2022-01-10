using System;

// Token: 0x020001CA RID: 458
internal sealed class RewardTester
{
	// Token: 0x06000A57 RID: 2647 RVA: 0x00068E3C File Offset: 0x0006703C
	internal bool haveRew(string pPowID)
	{
		RewardedPower result = null;
		foreach (RewardedPower rewardedPower in PlayerConfig.instance.data.rewardedPowers)
		{
			if (rewardedPower.name == pPowID)
			{
				result = rewardedPower;
				break;
			}
		}
		return result != null;
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x00068EAC File Offset: 0x000670AC
	internal bool checkRew()
	{
		if (PlayerConfig.instance.data.rewardedPowers.Count == 0)
		{
			return false;
		}
		double num = Epoch.Current();
		int num2 = 1860;
		bool flag = false;
		int i = 0;
		while (i < PlayerConfig.instance.data.rewardedPowers.Count)
		{
			RewardedPower rewardedPower = PlayerConfig.instance.data.rewardedPowers[i];
			bool flag2 = false;
			if (rewardedPower.timeStamp > num)
			{
				flag2 = true;
			}
			if (num - rewardedPower.timeStamp > (double)num2)
			{
				flag2 = true;
			}
			if (flag2)
			{
				PlayerConfig.instance.data.rewardedPowers.RemoveAt(i);
				flag = true;
			}
			else
			{
				i++;
			}
		}
		if (flag)
		{
			PlayerConfig.saveData();
			return true;
		}
		return false;
	}
}
