using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000AE RID: 174
public static class WorldLogMessageExtensions
{
	// Token: 0x06000389 RID: 905 RVA: 0x00037A71 File Offset: 0x00035C71
	public static void add(this WorldLogMessage pMessage)
	{
		WorldLog.instance.truncateList();
		WorldLog.instance.list.Insert(0, pMessage);
		HistoryHud.instance.newHistory(ref pMessage);
	}

	// Token: 0x0600038A RID: 906 RVA: 0x00037AA0 File Offset: 0x00035CA0
	private static string coloredText(this WorldLogMessage pMessage, string pText, bool pColorTags, int pColorId = -1)
	{
		Color color = Color.clear;
		if (pColorId == 1)
		{
			color = pMessage.color_special1;
		}
		else if (pColorId == 2)
		{
			color = pMessage.color_special2;
		}
		else if (pColorId == 3)
		{
			color = pMessage.color_special3;
		}
		if (color == Color.clear)
		{
			pColorTags = false;
		}
		if (pColorTags)
		{
			string text = Toolbox.colorToHex(color, true);
			return string.Concat(new string[]
			{
				"<color=",
				text,
				">",
				pText,
				"</color>"
			});
		}
		return pText;
	}

	// Token: 0x0600038B RID: 907 RVA: 0x00037B28 File Offset: 0x00035D28
	public static string getFormatedText(this WorldLogMessage pMessage, Text pTextField, bool pColorField, bool pColorTags)
	{
		string text = LocalizedTextManager.getText(pMessage.text, null);
		Color color = Toolbox.color_log_neutral;
		string text2 = pMessage.text;
		if (text2 != null)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
			if (num > 2212907174U)
			{
				if (num <= 2828091924U)
				{
					if (num <= 2607736614U)
					{
						if (num != 2240594631U)
						{
							if (num != 2607736614U)
							{
								goto IL_6DB;
							}
							if (!(text2 == "city_destroyed"))
							{
								goto IL_6DB;
							}
							color = Toolbox.color_log_warning;
							text = text.Replace("$name$", ref pMessage.coloredText(pMessage.special1, pColorTags, 1));
							pMessage.icon = "iconPyromaniac";
							goto IL_6DB;
						}
						else
						{
							if (!(text2 == "city_new"))
							{
								goto IL_6DB;
							}
							color = Toolbox.color_log_neutral;
							text = text.Replace("$name$", ref pMessage.coloredText(pMessage.special1, pColorTags, 1));
							pMessage.icon = "iconCitySelect";
							goto IL_6DB;
						}
					}
					else if (num != 2636985467U)
					{
						if (num != 2811345726U)
						{
							if (num != 2828091924U)
							{
								goto IL_6DB;
							}
							if (!(text2 == "king_killed_3"))
							{
								goto IL_6DB;
							}
						}
						else
						{
							if (!(text2 == "diplomacy_war_started"))
							{
								goto IL_6DB;
							}
							color = Toolbox.color_log_warning;
							text = text.Replace("$kingdom1$", ref pMessage.coloredText(pMessage.special1, pColorTags, 1));
							text = text.Replace("$kingdom2$", ref pMessage.coloredText(pMessage.special2, pColorTags, 2));
							pMessage.icon = "iconSpite";
							goto IL_6DB;
						}
					}
					else
					{
						if (!(text2 == "king_new"))
						{
							goto IL_6DB;
						}
						color = Toolbox.color_log_neutral;
						text = text.Replace("$kingdom$", ref pMessage.coloredText(pMessage.special1, pColorTags, 1));
						text = text.Replace("$king$", ref pMessage.coloredText(pMessage.special2, pColorTags, 2));
						pMessage.icon = "iconKings";
						goto IL_6DB;
					}
				}
				else if (num <= 2987610217U)
				{
					if (num != 2844869543U)
					{
						if (num != 2861647162U)
						{
							if (num != 2987610217U)
							{
								goto IL_6DB;
							}
							if (!(text2 == "log_city_revolted"))
							{
								goto IL_6DB;
							}
							color = Toolbox.color_log_good;
							text = text.Replace("$name$", ref pMessage.coloredText(pMessage.special1, pColorTags, -1));
							text = text.Replace("$kingdom$", ref pMessage.coloredText(pMessage.special2, pColorTags, 2));
							pMessage.icon = "iconInspiration";
							goto IL_6DB;
						}
						else if (!(text2 == "king_killed_1"))
						{
							goto IL_6DB;
						}
					}
					else if (!(text2 == "king_killed_2"))
					{
						goto IL_6DB;
					}
				}
				else if (num != 3050995753U)
				{
					if (num != 3204354119U)
					{
						if (num != 4035398513U)
						{
							goto IL_6DB;
						}
						if (!(text2 == "worldlog_disaster_underground_necromancer"))
						{
							goto IL_6DB;
						}
						goto IL_69E;
					}
					else
					{
						if (!(text2 == "kingdom_new"))
						{
							goto IL_6DB;
						}
						color = Toolbox.color_log_neutral;
						text = text.Replace("$name$", ref pMessage.coloredText(pMessage.special1, pColorTags, 1));
						pMessage.icon = "iconKingdom";
						goto IL_6DB;
					}
				}
				else
				{
					if (!(text2 == "worldlog_disaster_evil_mage"))
					{
						goto IL_6DB;
					}
					goto IL_69E;
				}
				color = Toolbox.color_log_warning;
				text = text.Replace("$kingdom$", ref pMessage.coloredText(pMessage.special1, pColorTags, 1));
				text = text.Replace("$king$", ref pMessage.coloredText(pMessage.special2, pColorTags, 2));
				text = text.Replace("$name$", ref pMessage.coloredText(pMessage.special3, pColorTags, 3));
				pMessage.icon = "iconKingslayer";
				goto IL_6DB;
			}
			if (num <= 1138845820U)
			{
				if (num <= 650767114U)
				{
					if (num != 633989495U)
					{
						if (num != 650767114U)
						{
							goto IL_6DB;
						}
						if (!(text2 == "favorite_killed_2"))
						{
							goto IL_6DB;
						}
					}
					else if (!(text2 == "favorite_killed_1"))
					{
						goto IL_6DB;
					}
				}
				else if (num != 667544733U)
				{
					if (num != 1053981597U)
					{
						if (num != 1138845820U)
						{
							goto IL_6DB;
						}
						if (!(text2 == "diplomacy_peace"))
						{
							goto IL_6DB;
						}
						color = Toolbox.color_log_good;
						text = text.Replace("$kingdom1$", ref pMessage.coloredText(pMessage.special1, pColorTags, 1));
						text = text.Replace("$kingdom2$", ref pMessage.coloredText(pMessage.special2, pColorTags, 2));
						pMessage.icon = "iconPacifist";
						goto IL_6DB;
					}
					else
					{
						if (!(text2 == "king_dead"))
						{
							goto IL_6DB;
						}
						color = Toolbox.color_log_warning;
						text = text.Replace("$kingdom$", ref pMessage.coloredText(pMessage.special1, pColorTags, 1));
						text = text.Replace("$king$", ref pMessage.coloredText(pMessage.special2, pColorTags, -1));
						pMessage.icon = "iconDead";
						goto IL_6DB;
					}
				}
				else if (!(text2 == "favorite_killed_3"))
				{
					goto IL_6DB;
				}
				color = Toolbox.color_log_warning;
				text = text.Replace("$name$", ref pMessage.coloredText(pMessage.special1, pColorTags, 1));
				text = text.Replace("$killer$", ref pMessage.coloredText(pMessage.special2, pColorTags, 2));
				pMessage.icon = "iconFavoriteKilled";
				goto IL_6DB;
			}
			if (num <= 1980831014U)
			{
				if (num != 1782552946U)
				{
					if (num != 1947275776U)
					{
						if (num != 1980831014U)
						{
							goto IL_6DB;
						}
						if (!(text2 == "favorite_dead_3"))
						{
							goto IL_6DB;
						}
					}
					else if (!(text2 == "favorite_dead_1"))
					{
						goto IL_6DB;
					}
				}
				else
				{
					if (!(text2 == "king_left"))
					{
						goto IL_6DB;
					}
					color = Toolbox.color_log_warning;
					text = text.Replace("$kingdom$", ref pMessage.coloredText(pMessage.special1, pColorTags, 1));
					text = text.Replace("$king$", ref pMessage.coloredText(pMessage.special2, pColorTags, -1));
					pMessage.icon = "iconStupid";
					goto IL_6DB;
				}
			}
			else if (num != 1997608633U)
			{
				if (num != 2050910677U)
				{
					if (num != 2212907174U)
					{
						goto IL_6DB;
					}
					if (!(text2 == "kingdom_destroyed"))
					{
						goto IL_6DB;
					}
					color = Toolbox.color_log_warning;
					text = text.Replace("$name$", ref pMessage.coloredText(pMessage.special1, pColorTags, 1));
					pMessage.icon = "iconPyromaniac";
					goto IL_6DB;
				}
				else
				{
					if (!(text2 == "worldlog_disaster_mad_thoughts"))
					{
						goto IL_6DB;
					}
					goto IL_69E;
				}
			}
			else if (!(text2 == "favorite_dead_2"))
			{
				goto IL_6DB;
			}
			color = Toolbox.color_log_warning;
			text = text.Replace("$name$", ref pMessage.coloredText(pMessage.special1, pColorTags, 1));
			pMessage.icon = "iconFavoriteKilled";
			goto IL_6DB;
			IL_69E:
			pColorField = true;
			color = Toolbox.color_log_warning;
			text = text.Replace("$name$", ref pMessage.coloredText(pMessage.special1, pColorTags, 1));
			text = text.Replace("$city$", ref pMessage.coloredText(pMessage.special2, pColorTags, 1));
		}
		IL_6DB:
		if (pColorField)
		{
			pTextField.color = color;
		}
		return text;
	}

	// Token: 0x0600038C RID: 908 RVA: 0x0003821B File Offset: 0x0003641B
	public static bool followLocation(this WorldLogMessage pMessage)
	{
		if (ref pMessage.hasFollowLocation())
		{
			WorldLog.locationFollow(pMessage.unit);
			return true;
		}
		return false;
	}

	// Token: 0x0600038D RID: 909 RVA: 0x00038234 File Offset: 0x00036434
	public static void jumpToLocation(this WorldLogMessage pMessage)
	{
		if (ref pMessage.followLocation())
		{
			return;
		}
		Vector3 location = ref pMessage.getLocation();
		if (location != Vector3.zero && Toolbox.inMapBorder(location))
		{
			WorldLog.locationJump(location);
		}
	}

	// Token: 0x0600038E RID: 910 RVA: 0x0003826C File Offset: 0x0003646C
	public static bool hasLocation(this WorldLogMessage pMessage)
	{
		return ref pMessage.getLocation() != Vector3.zero;
	}

	// Token: 0x0600038F RID: 911 RVA: 0x0003827E File Offset: 0x0003647E
	public static bool hasFollowLocation(this WorldLogMessage pMessage)
	{
		return pMessage.unit != null && pMessage.unit.data.alive;
	}

	// Token: 0x06000390 RID: 912 RVA: 0x000382A4 File Offset: 0x000364A4
	public static Vector3 getLocation(this WorldLogMessage pMessage)
	{
		if (pMessage.unit != null && pMessage.unit.data.alive)
		{
			return pMessage.unit.currentPosition;
		}
		if (pMessage.location != Vector3.zero && Toolbox.inMapBorder(pMessage.location))
		{
			return pMessage.location;
		}
		if (pMessage.kingdom != null && pMessage.kingdom.alive && pMessage.kingdom.capital != null)
		{
			if (Toolbox.inMapBorder(pMessage.kingdom.capital.lastCityCenter))
			{
				return pMessage.kingdom.capital.lastCityCenter;
			}
			if (Toolbox.inMapBorder(pMessage.kingdom.capital.cityCenter))
			{
				return pMessage.kingdom.capital.cityCenter;
			}
		}
		if (pMessage.city != null && pMessage.city.alive)
		{
			if (Toolbox.inMapBorder(pMessage.city.lastCityCenter))
			{
				return pMessage.city.lastCityCenter;
			}
			if (Toolbox.inMapBorder(pMessage.city.cityCenter))
			{
				return pMessage.city.cityCenter;
			}
		}
		return Vector3.zero;
	}
}
