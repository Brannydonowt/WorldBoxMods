using System;
using System.Collections.Generic;
using Beebyte.Obfuscator;
using UnityEngine;

// Token: 0x02000013 RID: 19
[ObfuscateLiterals]
[Serializable]
public class AchievementLibrary : AssetLibrary<Achievement>
{
	// Token: 0x06000065 RID: 101 RVA: 0x00005B04 File Offset: 0x00003D04
	public override void init()
	{
		base.init();
		Debug.Log("Init Achievements");
		AchievementLibrary.achievementLavaStrike = this.add(new Achievement
		{
			playStoreID = "CgkIia6M98wfEAIQAg",
			id = "achievementLavaStrike",
			hidden = true,
			check = new AchievementCheck(AchievementLibrary.checkLavaStrike),
			icon = "iconLightning",
			group = "destruction"
		});
		AchievementLibrary.achievementBabyTornado = this.add(new Achievement
		{
			playStoreID = "CgkIia6M98wfEAIQAw",
			id = "achievementBabyTornado",
			hidden = true,
			check = new AchievementCheck(AchievementLibrary.checkBabyTornado),
			icon = "iconTornado",
			group = "nature"
		});
		AchievementLibrary.achievement10000Creatures = this.add(new Achievement
		{
			playStoreID = "CgkIia6M98wfEAIQBA",
			id = "achievement10000Creatures",
			hidden = true,
			check = new AchievementCheck(AchievementLibrary.check10000Creatures),
			icon = "icon1000Creatures",
			group = "creation"
		});
		AchievementLibrary.achievementManyBombs = this.add(new Achievement
		{
			playStoreID = "",
			id = "achievementManyBombs",
			check = new AchievementCheck(AchievementLibrary.checkManyBombs),
			icon = "iconBomb",
			group = "destruction"
		});
		AchievementLibrary.achievementMegapolis = this.add(new Achievement
		{
			playStoreID = "CgkIia6M98wfEAIQBg",
			hidden = true,
			id = "achievementMegapolis",
			check_city = new AchievementCheckCity(AchievementLibrary.checkMegapolis),
			icon = "iconMegapolis",
			group = "civilizations"
		});
		AchievementLibrary.achievementMakeWilhelmScream = this.add(new Achievement
		{
			playStoreID = "CgkIia6M98wfEAIQBw",
			id = "achievementMakeWilhelmScream",
			hidden = true,
			check = new AchievementCheck(AchievementLibrary.checkMakeWilhelmScream),
			icon = "iconHumans",
			group = "miscellaneous"
		});
		AchievementLibrary.achievementBurger = this.add(new Achievement
		{
			playStoreID = "?",
			id = "achievementBurger",
			hidden = true,
			icon = "iconBurger",
			check = new AchievementCheck(AchievementLibrary.checkBurger),
			group = "miscellaneous"
		});
		AchievementLibrary.achievementBurger = this.add(new Achievement
		{
			playStoreID = "?",
			id = "achievementPie",
			hidden = true,
			icon = "iconResPie",
			group = "miscellaneous"
		});
		AchievementLibrary.achievementMayday = this.add(new Achievement
		{
			playStoreID = "?",
			id = "achievementMayday",
			hidden = true,
			check = new AchievementCheck(AchievementLibrary.checkMayday),
			icon = "iconSanta",
			group = "destruction"
		});
		AchievementLibrary.achievementDestroyWorldBox = this.add(new Achievement
		{
			playStoreID = "?",
			id = "achievementDestroyWorldBox",
			hidden = true,
			icon = "iconBrowse2",
			check = new AchievementCheck(AchievementLibrary.checkDestroyWorldBox),
			group = "miscellaneous"
		});
		AchievementLibrary.achievementCustomWorld = this.add(new Achievement
		{
			playStoreID = "?",
			id = "achievementCustomWorld",
			hidden = true,
			icon = "iconTileSoil",
			check = new AchievementCheck(AchievementLibrary.checkCustomWorld),
			group = "creation"
		});
		AchievementLibrary.achievement4RaceCities = this.add(new Achievement
		{
			playStoreID = "?",
			id = "achievement4RaceCities",
			hidden = true,
			icon = "icon4Races",
			check = new AchievementCheck(AchievementLibrary.check4RaceCities),
			group = "civilizations"
		});
		AchievementLibrary.achievementPiranhaLand = this.add(new Achievement
		{
			playStoreID = "?",
			id = "achievementPiranhaLand",
			hidden = true,
			icon = "iconPiranha",
			check_actor = new AchievementCheckActor(AchievementLibrary.checkPiranhaLand),
			group = "creatures"
		});
		AchievementLibrary.achievementPrintHeart = this.add(new Achievement
		{
			playStoreID = "?",
			id = "achievementPrintHeart",
			hidden = true,
			check_godPower = new AchievementCheckGodPower(AchievementLibrary.checkPrintHeart),
			icon = "iconPrintHeart",
			group = "creation"
		});
		AchievementLibrary.achievementSacrifice = this.add(new Achievement
		{
			playStoreID = "?",
			id = "achievementSacrifice",
			hidden = true,
			icon = "iconSheep",
			check = new AchievementCheck(AchievementLibrary.checkSacrifice),
			group = "creatures"
		});
		AchievementLibrary.achievementAntWorld = this.add(new Achievement
		{
			playStoreID = "?",
			id = "achievementAntWorld",
			hidden = true,
			icon = "iconAntBlack",
			check = new AchievementCheck(AchievementLibrary.checkAntWorld),
			group = "creatures"
		});
		AchievementLibrary.achievementFinalResolution = this.add(new Achievement
		{
			playStoreID = "?",
			id = "achievementFinalResolution",
			hidden = true,
			icon = "iconGreygoo",
			check = new AchievementCheck(AchievementLibrary.checkFinalResolution),
			group = "destruction"
		});
		AchievementLibrary.achievementTntAndHeat = this.add(new Achievement
		{
			playStoreID = "?",
			id = "achievementTntAndHeat",
			hidden = true,
			icon = "iconTnt",
			check = new AchievementCheck(AchievementLibrary.checkTntAndHeat),
			group = "destruction"
		});
		AchievementLibrary.achievementGodFingerLightning = this.add(new Achievement
		{
			playStoreID = "?",
			id = "achievementGodFingerLightning",
			hidden = true,
			check = new AchievementCheck(AchievementLibrary.checkGodFingerLightning),
			icon = "iconGodFinger",
			group = "destruction"
		});
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00006157 File Offset: 0x00004357
	public static void unlock(string pID)
	{
		AchievementLibrary.unlock(AssetManager.achievements.dict[pID]);
	}

	// Token: 0x06000067 RID: 103 RVA: 0x0000616E File Offset: 0x0000436E
	public static void unlock(Achievement pAchievement)
	{
		if (AchievementLibrary.isUnlocked(pAchievement))
		{
			return;
		}
		if (PlayerConfig.unlockAchievement(pAchievement.id))
		{
			AchievementPopup.show(pAchievement);
		}
		Analytics.LogEvent("Achievement", "id", pAchievement.id);
		MapBox.aye();
	}

	// Token: 0x06000068 RID: 104 RVA: 0x000061A6 File Offset: 0x000043A6
	public static bool isUnlocked(Achievement pAchievement)
	{
		return PlayerConfig.isAchievementUnlocked(pAchievement.id);
	}

	// Token: 0x06000069 RID: 105 RVA: 0x000061B3 File Offset: 0x000043B3
	public static bool isUnlocked(string pID)
	{
		return PlayerConfig.isAchievementUnlocked(pID);
	}

	// Token: 0x0600006A RID: 106 RVA: 0x000061BC File Offset: 0x000043BC
	private static void checkAntWorld()
	{
		if (AchievementLibrary.isUnlocked(AchievementLibrary.achievementAntWorld))
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		List<Actor> simpleList = MapBox.instance.units.getSimpleList();
		for (int i = 0; i < simpleList.Count; i++)
		{
			Actor actor = simpleList[i];
			if (actor.stats.id == "antBlack")
			{
				num++;
			}
			if (actor.stats.id == "antBlue")
			{
				num4++;
			}
			if (actor.stats.id == "antRed")
			{
				num3++;
			}
			if (actor.stats.id == "antGreen")
			{
				num2++;
			}
		}
		if (num4 >= 10 && num2 >= 10 && num3 >= 10 && num >= 10)
		{
			AchievementLibrary.unlock(AchievementLibrary.achievementAntWorld);
		}
	}

	// Token: 0x0600006B RID: 107 RVA: 0x0000629A File Offset: 0x0000449A
	private static void check10000Creatures()
	{
		if (AchievementLibrary.isUnlocked(AchievementLibrary.achievement10000Creatures))
		{
			return;
		}
		if (MapBox.instance.gameStats.data.creaturesCreated >= 10000)
		{
			AchievementLibrary.unlock(AchievementLibrary.achievement10000Creatures);
		}
	}

	// Token: 0x0600006C RID: 108 RVA: 0x000062CE File Offset: 0x000044CE
	private static void checkLavaStrike()
	{
		if (AchievementLibrary.isUnlocked(AchievementLibrary.achievementLavaStrike))
		{
			return;
		}
		AchievementLibrary.unlock(AchievementLibrary.achievementLavaStrike);
	}

	// Token: 0x0600006D RID: 109 RVA: 0x000062E7 File Offset: 0x000044E7
	private static void checkBabyTornado()
	{
		if (AchievementLibrary.isUnlocked(AchievementLibrary.achievementBabyTornado))
		{
			return;
		}
		AchievementLibrary.unlock(AchievementLibrary.achievementBabyTornado);
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00006300 File Offset: 0x00004500
	private static void checkManyBombs()
	{
		if (AchievementLibrary.isUnlocked(AchievementLibrary.achievementManyBombs))
		{
			return;
		}
		if (MapBox.instance.gameStats.data.bombsDropped >= 1000)
		{
			AchievementLibrary.unlock(AchievementLibrary.achievementManyBombs);
		}
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00006334 File Offset: 0x00004534
	private static void checkMegapolis(City pTarget)
	{
		if (AchievementLibrary.isUnlocked(AchievementLibrary.achievementMegapolis))
		{
			return;
		}
		if (pTarget.race.id == "human" && pTarget.getPopulationTotal() >= 200)
		{
			AchievementLibrary.unlock(AchievementLibrary.achievementMegapolis);
		}
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00006371 File Offset: 0x00004571
	private static void checkMakeWilhelmScream()
	{
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00006373 File Offset: 0x00004573
	private static void checkBurger()
	{
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00006375 File Offset: 0x00004575
	private static void checkMayday()
	{
		if (AchievementLibrary.isUnlocked(AchievementLibrary.achievementMayday))
		{
			return;
		}
		AchievementLibrary.unlock(AchievementLibrary.achievementMayday);
	}

	// Token: 0x06000073 RID: 115 RVA: 0x0000638E File Offset: 0x0000458E
	private static void checkDestroyWorldBox()
	{
		if (AchievementLibrary.isUnlocked(AchievementLibrary.achievementDestroyWorldBox))
		{
			return;
		}
		AchievementLibrary.unlock(AchievementLibrary.achievementDestroyWorldBox);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x000063A7 File Offset: 0x000045A7
	private static void checkCustomWorld()
	{
		if (AchievementLibrary.isUnlocked(AchievementLibrary.achievementCustomWorld))
		{
			return;
		}
		AchievementLibrary.unlock(AchievementLibrary.achievementCustomWorld);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x000063C0 File Offset: 0x000045C0
	private static void check4RaceCities()
	{
		if (AchievementLibrary.isUnlocked(AchievementLibrary.achievement4RaceCities))
		{
			return;
		}
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		for (int i = 0; i < MapBox.instance.citiesList.Count; i++)
		{
			City city = MapBox.instance.citiesList[i];
			if (city.race.id == "human")
			{
				flag = true;
			}
			if (city.race.id == "orc")
			{
				flag2 = true;
			}
			if (city.race.id == "elf")
			{
				flag3 = true;
			}
			if (city.race.id == "dwarf")
			{
				flag4 = true;
			}
		}
		if (flag && flag2 && flag3 && flag4)
		{
			AchievementLibrary.unlock(AchievementLibrary.achievement4RaceCities);
		}
	}

	// Token: 0x06000076 RID: 118 RVA: 0x0000648C File Offset: 0x0000468C
	private static void checkPiranhaLand(Actor pTarget)
	{
		if (pTarget.stats.dieOnGround && !pTarget.currentTile.Type.liquid)
		{
			if (pTarget.stats.id.Contains("piranha") && !AchievementLibrary.isUnlocked(AchievementLibrary.achievementPiranhaLand))
			{
				AchievementLibrary.unlock(AchievementLibrary.achievementPiranhaLand);
			}
			pTarget.killHimself(false, AttackType.Other, true, true);
		}
	}

	// Token: 0x06000077 RID: 119 RVA: 0x000064EF File Offset: 0x000046EF
	private static void checkPrintHeart(GodPower pTarget)
	{
		if (AchievementLibrary.isUnlocked(AchievementLibrary.achievementPrintHeart))
		{
			return;
		}
		if (pTarget.printersPrint == "heart")
		{
			AchievementLibrary.unlock(AchievementLibrary.achievementPrintHeart);
		}
	}

	// Token: 0x06000078 RID: 120 RVA: 0x0000651A File Offset: 0x0000471A
	private static void checkSacrifice()
	{
		if (AchievementLibrary.isUnlocked(AchievementLibrary.achievementSacrifice))
		{
			return;
		}
		AchievementLibrary.unlock(AchievementLibrary.achievementSacrifice);
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00006533 File Offset: 0x00004733
	private static void checkFinalResolution()
	{
		if (AchievementLibrary.isUnlocked(AchievementLibrary.achievementFinalResolution))
		{
			return;
		}
		AchievementLibrary.unlock(AchievementLibrary.achievementFinalResolution);
	}

	// Token: 0x0600007A RID: 122 RVA: 0x0000654C File Offset: 0x0000474C
	private static void checkTntAndHeat()
	{
		if (AchievementLibrary.isUnlocked(AchievementLibrary.achievementTntAndHeat))
		{
			return;
		}
		AchievementLibrary.unlock(AchievementLibrary.achievementTntAndHeat);
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00006565 File Offset: 0x00004765
	private static void checkGodFingerLightning()
	{
		if (AchievementLibrary.isUnlocked(AchievementLibrary.achievementGodFingerLightning))
		{
			return;
		}
		AchievementLibrary.unlock(AchievementLibrary.achievementGodFingerLightning);
	}

	// Token: 0x0600007C RID: 124 RVA: 0x0000657E File Offset: 0x0000477E
	public static void login()
	{
	}

	// Token: 0x0400004C RID: 76
	public static Achievement achievementLavaStrike;

	// Token: 0x0400004D RID: 77
	public static Achievement achievementBabyTornado;

	// Token: 0x0400004E RID: 78
	public static Achievement achievementManyBombs;

	// Token: 0x0400004F RID: 79
	public static Achievement achievementMegapolis;

	// Token: 0x04000050 RID: 80
	public static Achievement achievementMakeWilhelmScream;

	// Token: 0x04000051 RID: 81
	public static Achievement achievementBurger;

	// Token: 0x04000052 RID: 82
	public static Achievement achievementMayday;

	// Token: 0x04000053 RID: 83
	public static Achievement achievementDestroyWorldBox;

	// Token: 0x04000054 RID: 84
	public static Achievement achievementCustomWorld;

	// Token: 0x04000055 RID: 85
	public static Achievement achievement4RaceCities;

	// Token: 0x04000056 RID: 86
	public static Achievement achievementPiranhaLand;

	// Token: 0x04000057 RID: 87
	public static Achievement achievementPrintHeart;

	// Token: 0x04000058 RID: 88
	public static Achievement achievementSacrifice;

	// Token: 0x04000059 RID: 89
	public static Achievement achievementFinalResolution;

	// Token: 0x0400005A RID: 90
	public static Achievement achievementTntAndHeat;

	// Token: 0x0400005B RID: 91
	public static Achievement achievementGodFingerLightning;

	// Token: 0x0400005C RID: 92
	public static Achievement achievement10000Creatures;

	// Token: 0x0400005D RID: 93
	public static Achievement achievementAntWorld;
}
