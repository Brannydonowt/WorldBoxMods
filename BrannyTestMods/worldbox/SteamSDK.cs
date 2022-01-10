using System;
using Proyecto26;
using RSG;
using Steamworks;
using UnityEngine;

// Token: 0x02000236 RID: 566
public class SteamSDK : MonoBehaviour
{
	// Token: 0x06000C7F RID: 3199 RVA: 0x0007A11C File Offset: 0x0007831C
	private void Start()
	{
		if (this.initiated)
		{
			return;
		}
		this.initiated = true;
		bool flag = false;
		try
		{
			SteamSDK.instance = this;
			SteamClient.Init(1206560U, true);
			RestClient.DefaultRequestHeaders["wb-stmc"] = "true";
		}
		catch (Exception message)
		{
			Debug.Log("Disabling Steam Integration");
			Debug.LogWarning(message);
			RestClient.DefaultRequestHeaders["wb-stmc"] = "na";
			flag = true;
		}
		try
		{
			string text = SteamClient.SteamId.ToString();
			if (!string.IsNullOrEmpty(text))
			{
				Config.steamId = text;
				RestClient.DefaultRequestHeaders["wb-stm"] = text;
				Debug.Log("S:" + Config.steamId);
			}
			else
			{
				Debug.Log("S:nf");
			}
		}
		catch (Exception)
		{
		}
		try
		{
			if (Config.steamLanguageAllowDetect)
			{
				Debug.Log("s:Detect - First start, Steam detecting language");
				string steamLanguage = SteamSDK.getSteamLanguage();
				if (!string.IsNullOrEmpty(steamLanguage))
				{
					string language = LocalizedTextManager.instance.language;
					if (steamLanguage == "en" && language != "en")
					{
						Debug.Log("s:Detect - Already have a language, not falling back to english");
					}
					else
					{
						LocalizedTextManager.instance.setLanguage(steamLanguage);
					}
				}
				Debug.Log("s:Detect - language " + steamLanguage);
			}
		}
		catch (Exception)
		{
		}
		try
		{
			string text2 = SteamClient.Name.ToString();
			if (!string.IsNullOrEmpty(text2))
			{
				Config.steamName = text2;
			}
		}
		catch (Exception)
		{
		}
		try
		{
			if (SteamClient.RestartAppIfNecessary(1206560U))
			{
				Debug.Log("Restart App from Steam launcher");
				SteamSDK.shouldQuit = true;
				flag = true;
			}
		}
		catch (Exception message2)
		{
			Debug.LogError(message2);
		}
		if (SteamSDK.shouldQuit)
		{
			Application.Quit();
		}
		if (flag)
		{
			Debug.Log("Steam is not available");
			SteamSDK.steamInitialized.Reject(new Exception("Steam is not available"));
			Object.Destroy(SteamSDK.instance);
			return;
		}
		SteamSDK.steamInitialized.Resolve();
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x0007A320 File Offset: 0x00078520
	private static string getSteamLanguage()
	{
		string gameLanguage = SteamApps.GameLanguage;
		if (gameLanguage != null)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(gameLanguage);
			if (num <= 2471602315U)
			{
				if (num <= 683056061U)
				{
					if (num <= 319214730U)
					{
						if (num != 308944030U)
						{
							if (num != 316123288U)
							{
								if (num == 319214730U)
								{
									if (gameLanguage == "romanian")
									{
										return "ro";
									}
								}
							}
							else if (gameLanguage == "danish")
							{
								return "da";
							}
						}
						else if (gameLanguage == "swedish")
						{
							return "sv";
						}
					}
					else if (num <= 505713757U)
					{
						if (num != 380651494U)
						{
							if (num == 505713757U)
							{
								if (gameLanguage == "brazilian")
								{
									return "br";
								}
							}
						}
						else if (gameLanguage == "russian")
						{
							return "ru";
						}
					}
					else if (num != 599131013U)
					{
						if (num == 683056061U)
						{
							if (gameLanguage == "ukrainian")
							{
								return "ua";
							}
						}
					}
					else if (gameLanguage == "french")
					{
						return "fr";
					}
				}
				else if (num <= 1544226106U)
				{
					if (num != 693158059U)
					{
						if (num != 1262725376U)
						{
							if (num == 1544226106U)
							{
								if (gameLanguage == "hungarian")
								{
									return "hu";
								}
							}
						}
						else if (gameLanguage == "latam")
						{
							return "es";
						}
					}
					else if (gameLanguage == "norwegian")
					{
						return "no";
					}
				}
				else if (num <= 1703858441U)
				{
					if (num != 1580935484U)
					{
						if (num == 1703858441U)
						{
							if (gameLanguage == "arabic")
							{
								return "ar";
							}
						}
					}
					else if (gameLanguage == "portuguese")
					{
						return "pt";
					}
				}
				else if (num != 1901528810U)
				{
					if (num == 2471602315U)
					{
						if (gameLanguage == "italian")
						{
							return "it";
						}
					}
				}
				else if (gameLanguage == "japanese")
				{
					return "ja";
				}
			}
			else if (num <= 3229236340U)
			{
				if (num > 2805355685U)
				{
					if (num <= 3210859552U)
					{
						if (num != 3180870988U)
						{
							if (num != 3210859552U)
							{
								goto IL_4FC;
							}
							if (!(gameLanguage == "koreana"))
							{
								goto IL_4FC;
							}
						}
						else
						{
							if (!(gameLanguage == "polish"))
							{
								goto IL_4FC;
							}
							return "pl";
						}
					}
					else if (num != 3222531841U)
					{
						if (num != 3229236340U)
						{
							goto IL_4FC;
						}
						if (!(gameLanguage == "finnish"))
						{
							goto IL_4FC;
						}
						return "fn";
					}
					else if (!(gameLanguage == "korean"))
					{
						goto IL_4FC;
					}
					return "ko";
				}
				if (num != 2499415067U)
				{
					if (num != 2798875500U)
					{
						if (num == 2805355685U)
						{
							if (gameLanguage == "schinese")
							{
								return "cz";
							}
						}
					}
					else if (gameLanguage == "czech")
					{
						return "cs";
					}
				}
				else if (gameLanguage == "english")
				{
					return "en";
				}
			}
			else if (num <= 3719199419U)
			{
				if (num <= 3405445907U)
				{
					if (num != 3264533134U)
					{
						if (num == 3405445907U)
						{
							if (gameLanguage == "german")
							{
								return "de";
							}
						}
					}
					else if (gameLanguage == "tchinese")
					{
						return "ch";
					}
				}
				else if (num != 3426057626U)
				{
					if (num == 3719199419U)
					{
						if (gameLanguage == "spanish")
						{
							return "es";
						}
					}
				}
				else if (gameLanguage == "vietnamese")
				{
					return "vn";
				}
			}
			else if (num <= 3759690811U)
			{
				if (num != 3739448251U)
				{
					if (num == 3759690811U)
					{
						if (gameLanguage == "thai")
						{
							return "th";
						}
					}
				}
				else if (gameLanguage == "turkish")
				{
					return "tr";
				}
			}
			else if (num != 4151292721U)
			{
				if (num == 4263372803U)
				{
					if (gameLanguage == "greek")
					{
						return "gr";
					}
				}
			}
			else if (gameLanguage == "dutch")
			{
				return "nl";
			}
		}
		IL_4FC:
		return string.Empty;
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x0007A830 File Offset: 0x00078A30
	private void OnDisable()
	{
		try
		{
			SteamClient.Shutdown();
		}
		catch (Exception message)
		{
			Debug.LogWarning(message);
			Object.Destroy(SteamSDK.instance);
		}
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x0007A868 File Offset: 0x00078A68
	private void OnDestroy()
	{
		SteamSDK.instance = null;
	}

	// Token: 0x04000F4F RID: 3919
	internal static Promise steamInitialized = new Promise();

	// Token: 0x04000F50 RID: 3920
	private bool initiated;

	// Token: 0x04000F51 RID: 3921
	internal static SteamSDK instance;

	// Token: 0x04000F52 RID: 3922
	internal static bool shouldQuit = false;

	// Token: 0x04000F53 RID: 3923
	private static string[] supportedSteamLanguages = new string[]
	{
		"ar",
		"cz",
		"ch",
		"cs",
		"da",
		"nl",
		"en",
		"fn",
		"fr",
		"de",
		"gr",
		"hu",
		"it",
		"ja",
		"ko",
		"no",
		"pl",
		"pt",
		"br",
		"ro",
		"ru",
		"es",
		"es",
		"sv",
		"th",
		"tr",
		"ua",
		"vn"
	};
}
