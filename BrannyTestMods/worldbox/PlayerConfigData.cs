using System;
using System.Collections.Generic;

// Token: 0x020001C5 RID: 453
[Serializable]
public class PlayerConfigData
{
	// Token: 0x06000A43 RID: 2627 RVA: 0x000682B8 File Offset: 0x000664B8
	public void initData()
	{
		PlayerConfig.dict.Clear();
		this.add(new PlayerOptionData("map_names")
		{
			boolVal = false
		});
		this.add(new PlayerOptionData("map_city_zones")
		{
			boolVal = false
		});
		this.add(new PlayerOptionData("map_culture_zones")
		{
			boolVal = false
		});
		this.add(new PlayerOptionData("map_kingdom_zones")
		{
			boolVal = false
		});
		this.add(new PlayerOptionData("map_kings_leaders")
		{
			boolVal = false
		});
		this.add(new PlayerOptionData("history_log")
		{
			boolVal = false
		});
		this.add(new PlayerOptionData("marks_boats")
		{
			boolVal = false
		});
		this.add(new PlayerOptionData("portrait")
		{
			boolVal = true
		});
		this.add(new PlayerOptionData("sound")
		{
			boolVal = true
		});
		this.add(new PlayerOptionData("sound_ambient")
		{
			boolVal = true
		});
		this.add(new PlayerOptionData("vignette")
		{
			boolVal = true
		});
		this.add(new PlayerOptionData("bloom")
		{
			boolVal = true
		});
		this.add(new PlayerOptionData("fire")
		{
			boolVal = true
		});
		this.add(new PlayerOptionData("smoke")
		{
			boolVal = true
		});
		this.add(new PlayerOptionData("vsync")
		{
			boolVal = false
		});
		this.add(new PlayerOptionData("experimental")
		{
			boolVal = false
		});
		this.add(new PlayerOptionData("fullscreen")
		{
			boolVal = true
		});
		this.add(new PlayerOptionData("language")
		{
			stringVal = "en",
			boolVal = false
		});
		this.add(new PlayerOptionData("username")
		{
			stringVal = "",
			boolVal = false
		});
		this.add(new PlayerOptionData("wbb_confirmed")
		{
			boolVal = false
		});
		this.add(new PlayerOptionData("ui_size")
		{
			stringVal = "100%"
		});
		if (Config.isMobile)
		{
			this.add(new PlayerOptionData("shadows")
			{
				boolVal = false
			});
			this.add(new PlayerOptionData("tooltips")
			{
				boolVal = false
			});
			return;
		}
		this.add(new PlayerOptionData("shadows")
		{
			boolVal = true
		});
		this.add(new PlayerOptionData("tooltips")
		{
			boolVal = true
		});
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x0006854C File Offset: 0x0006674C
	public PlayerOptionData get(string pKey)
	{
		foreach (PlayerOptionData playerOptionData in this.list)
		{
			if (string.Equals(pKey, playerOptionData.name))
			{
				return playerOptionData;
			}
		}
		return null;
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x000685B0 File Offset: 0x000667B0
	public PlayerOptionData add(PlayerOptionData pData)
	{
		foreach (PlayerOptionData playerOptionData in this.list)
		{
			if (string.Equals(pData.name, playerOptionData.name))
			{
				PlayerConfig.dict.Add(playerOptionData.name, playerOptionData);
				return playerOptionData;
			}
		}
		this.list.Add(pData);
		PlayerConfig.dict.Add(pData.name, pData);
		return pData;
	}

	// Token: 0x04000CC4 RID: 3268
	public int nextReward = 5;

	// Token: 0x04000CC5 RID: 3269
	public string powerReward = string.Empty;

	// Token: 0x04000CC6 RID: 3270
	public string lastReward = string.Empty;

	// Token: 0x04000CC7 RID: 3271
	public double nextAdTimestamp = -1.0;

	// Token: 0x04000CC8 RID: 3272
	public List<RewardedPower> rewardedPowers = new List<RewardedPower>();

	// Token: 0x04000CC9 RID: 3273
	public List<PlayerOptionData> list = new List<PlayerOptionData>();

	// Token: 0x04000CCA RID: 3274
	public List<string> achievements = new List<string>();

	// Token: 0x04000CCB RID: 3275
	[NonSerialized]
	public HashSet<string> achievements_hashset = new HashSet<string>();

	// Token: 0x04000CCC RID: 3276
	internal string worldnet = "";

	// Token: 0x04000CCD RID: 3277
	public bool premium;

	// Token: 0x04000CCE RID: 3278
	public bool valCheck;

	// Token: 0x04000CCF RID: 3279
	public bool magicCheck;

	// Token: 0x04000CD0 RID: 3280
	public bool magicCheck0133;

	// Token: 0x04000CD1 RID: 3281
	public bool fireworksCheck;

	// Token: 0x04000CD2 RID: 3282
	public bool fireworksCheck0133;

	// Token: 0x04000CD3 RID: 3283
	public int saveVersion = 1;

	// Token: 0x04000CD4 RID: 3284
	public int lastRateID;

	// Token: 0x04000CD5 RID: 3285
	public bool tutorialFinished;

	// Token: 0x04000CD6 RID: 3286
	public bool pPossible = true;

	// Token: 0x04000CD7 RID: 3287
	public bool pPossible0133 = true;
}
