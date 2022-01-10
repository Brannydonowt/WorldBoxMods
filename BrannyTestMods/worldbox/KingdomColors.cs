using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class KingdomColors
{
	// Token: 0x0600015C RID: 348 RVA: 0x000176BC File Offset: 0x000158BC
	public static KingdomColorsData init(string pID = null)
	{
		KingdomColorsData kingdomColorsData = JsonUtility.FromJson<KingdomColorsData>(Resources.Load<TextAsset>("colors/kingdom_colors").text);
		KingdomColors.dict = new Dictionary<string, KingdomColorContainer>();
		foreach (KingdomColorContainer kingdomColorContainer in kingdomColorsData.colors)
		{
			foreach (KingdomColor kingdomColor in kingdomColorContainer.list)
			{
				kingdomColor.initColor();
			}
			KingdomColors.dict.Add(kingdomColorContainer.race, kingdomColorContainer);
		}
		return kingdomColorsData;
	}

	// Token: 0x0600015D RID: 349 RVA: 0x00017778 File Offset: 0x00015978
	public static void exportToFile()
	{
		KingdomColorsData kingdomColorsData = default(KingdomColorsData);
		foreach (KingdomColorContainer kingdomColorContainer in KingdomColors.dict.Values)
		{
			kingdomColorsData.colors.Add(kingdomColorContainer);
		}
		string text = JsonUtility.ToJson(kingdomColorsData, true);
		File.WriteAllText(KingdomColors._path, text);
	}

	// Token: 0x0600015E RID: 350 RVA: 0x000177F4 File Offset: 0x000159F4
	public static KingdomColor getColor(string pRace, int pColorID)
	{
		return KingdomColors.getContainer(pRace).getColor(pColorID);
	}

	// Token: 0x0600015F RID: 351 RVA: 0x00017802 File Offset: 0x00015A02
	public static KingdomColorContainer getContainer(string pRace)
	{
		return KingdomColors.dict[pRace];
	}

	// Token: 0x0400014D RID: 333
	public static Dictionary<string, KingdomColorContainer> dict = new Dictionary<string, KingdomColorContainer>();

	// Token: 0x0400014E RID: 334
	public static string _path = Application.dataPath + "/Resources/colors/kingdom_colors.json";
}
