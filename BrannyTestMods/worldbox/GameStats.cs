using System;
using System.IO;
using UnityEngine;

// Token: 0x0200008C RID: 140
public class GameStats : MonoBehaviour
{
	// Token: 0x06000312 RID: 786 RVA: 0x000334C4 File Offset: 0x000316C4
	private void Start()
	{
		this.dataPath = Application.persistentDataPath + "/stats.json";
		this.loadData();
		if (this.data == null)
		{
			this.data = new GameStatsData();
		}
		this.saveTimer = new WorldTimer(30f, new Action(this.saveData));
		this.data.gameLaunches++;
	}

	// Token: 0x06000313 RID: 787 RVA: 0x0003352E File Offset: 0x0003172E
	internal bool goodForAds()
	{
		return true;
	}

	// Token: 0x06000314 RID: 788 RVA: 0x00033534 File Offset: 0x00031734
	private void saveData()
	{
		string text = JsonUtility.ToJson(this.data);
		File.WriteAllText(this.dataPath, text);
		int num = (int)TimeSpan.FromSeconds(this.data.gameTime).TotalHours;
		string str;
		if (num < 1)
		{
			str = "0+";
		}
		else if (num < 2)
		{
			str = "1+";
		}
		else if (num < 3)
		{
			str = "3+";
		}
		else if (num < 10)
		{
			str = "5+";
		}
		else if (num < 20)
		{
			str = "10+";
		}
		else if (num < 40)
		{
			str = "20+";
		}
		else if (num < 50)
		{
			str = "40+";
		}
		else if (num < 100)
		{
			str = "50+";
		}
		else if (num < 200)
		{
			str = "100+";
		}
		else if (num < 500)
		{
			str = "200+";
		}
		else
		{
			str = "500+";
		}
		PlayerConfig.setFirebaseProp("game_launches", this.data.gameLaunches.ToString() ?? "");
		PlayerConfig.setFirebaseProp("time_spent_hours", "h_" + str);
	}

	// Token: 0x06000315 RID: 789 RVA: 0x0003363C File Offset: 0x0003183C
	private void loadData()
	{
		if (!File.Exists(this.dataPath))
		{
			return;
		}
		string text = File.ReadAllText(this.dataPath);
		try
		{
			this.data = JsonUtility.FromJson<GameStatsData>(text);
		}
		catch (Exception)
		{
			this.data = new GameStatsData();
		}
	}

	// Token: 0x06000316 RID: 790 RVA: 0x00033690 File Offset: 0x00031890
	public void updateStats(float pTime)
	{
		this.data.gameTime = this.data.gameTime + (double)pTime;
		this.saveTimer.update(-1f);
	}

	// Token: 0x040004C6 RID: 1222
	internal GameStatsData data;

	// Token: 0x040004C7 RID: 1223
	private string dataPath;

	// Token: 0x040004C8 RID: 1224
	private WorldTimer saveTimer;
}
