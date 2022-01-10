using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020000BB RID: 187
public static class AutoSaveManager
{
	// Token: 0x060003BE RID: 958 RVA: 0x0003932C File Offset: 0x0003752C
	public static void update()
	{
		if (ScrollWindow.isWindowActive() || Config.controllableUnit != null)
		{
			return;
		}
		if (AutoSaveManager._time > 0f)
		{
			AutoSaveManager._time -= Time.deltaTime;
			return;
		}
		AutoSaveManager._time = AutoSaveManager._interval;
		AutoSaveManager.autoSave(false);
	}

	// Token: 0x060003BF RID: 959 RVA: 0x0003937C File Offset: 0x0003757C
	public static void autoSave(bool pSkipDelete = false)
	{
		try
		{
			AutoSaveManager.getAutoSaves();
			SaveManager.saveWorldToDirectory(SaveManager.generateAutosavesPath(Math.Truncate(Epoch.Current()).ToString()), false, true);
			if (!pSkipDelete)
			{
				AutoSaveManager.checkClearSaves();
			}
		}
		catch (Exception message)
		{
			Debug.Log("error while auto saving");
			Debug.LogError(message);
		}
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x000393DC File Offset: 0x000375DC
	private static void checkClearSaves()
	{
		List<AutoSaveData> autoSaves = AutoSaveManager.getAutoSaves();
		foreach (List<AutoSaveData> list in AutoSaveManager.getAutoSavesPerMap(autoSaves).Values)
		{
			if (list.Count > 5)
			{
				for (int i = list.Count; i > 5; i--)
				{
					SaveManager.deleteSavePath(list[i - 1].path);
				}
			}
		}
		autoSaves = AutoSaveManager.getAutoSaves();
		if (autoSaves.Count > 30)
		{
			for (int j = 30; j < autoSaves.Count; j++)
			{
				SaveManager.deleteSavePath(autoSaves[j].path);
			}
		}
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00039498 File Offset: 0x00037698
	public static void reset()
	{
		AutoSaveManager._time = AutoSaveManager._interval;
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x000394A4 File Offset: 0x000376A4
	public static List<AutoSaveData> getAutoSaves()
	{
		string text = SaveManager.generateAutosavesPath("");
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		List<string> directories = Toolbox.getDirectories(text);
		List<AutoSaveData> list = new List<AutoSaveData>();
		foreach (string text2 in directories)
		{
			MapMetaData metaFor = SaveManager.getMetaFor(text2);
			if (metaFor != null)
			{
				list.Add(new AutoSaveData
				{
					name = metaFor.mapStats.name,
					path = text2,
					timestamp = metaFor.timestamp
				});
			}
		}
		list.Sort(new Comparison<AutoSaveData>(AutoSaveManager.sorter));
		return list;
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x00039560 File Offset: 0x00037760
	public static Dictionary<string, List<AutoSaveData>> getAutoSavesPerMap(List<AutoSaveData> pDatas)
	{
		Dictionary<string, List<AutoSaveData>> dictionary = new Dictionary<string, List<AutoSaveData>>();
		for (int i = 0; i < pDatas.Count; i++)
		{
			AutoSaveData autoSaveData = pDatas[i];
			if (!dictionary.ContainsKey(autoSaveData.name))
			{
				dictionary[autoSaveData.name] = new List<AutoSaveData>();
			}
			dictionary[autoSaveData.name].Add(autoSaveData);
		}
		return dictionary;
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x000395BE File Offset: 0x000377BE
	public static int sorter(AutoSaveData o1, AutoSaveData o2)
	{
		return o2.timestamp.CompareTo(o1.timestamp);
	}

	// Token: 0x0400061F RID: 1567
	private static float _time = 300f;

	// Token: 0x04000620 RID: 1568
	private static float _interval = 300f;
}
