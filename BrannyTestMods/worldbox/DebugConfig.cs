using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200016E RID: 366
public class DebugConfig : MonoBehaviour
{
	// Token: 0x0600083B RID: 2107 RVA: 0x00059D6E File Offset: 0x00057F6E
	private void Start()
	{
		DebugConfig.instance = this;
		DebugConfig.pos_x = 80;
		DebugConfig.pos_y = -10;
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x00059D84 File Offset: 0x00057F84
	public static void init()
	{
		DebugConfig.dictionary = new Dictionary<DebugOption, bool>();
		foreach (object obj in Enum.GetValues(typeof(DebugOption)))
		{
			DebugConfig.setOption((DebugOption)obj, false);
		}
		DebugConfig.setOption(DebugOption.UseGlobalPathLock, true);
		DebugConfig.setOption(DebugOption.SystemBuildTick, true);
		DebugConfig.setOption(DebugOption.SystemCityPlaceFinder, true);
		DebugConfig.setOption(DebugOption.SystemProduceNewCitizens, true);
		DebugConfig.setOption(DebugOption.SystemUnitPathfinding, true);
		DebugConfig.setOption(DebugOption.SystemWorldBehaviours, true);
		DebugConfig.setOption(DebugOption.SystemZoneGrowth, true);
		DebugConfig.setOption(DebugOption.SystemCheckUnitAction, true);
		DebugConfig.setOption(DebugOption.SystemUpdateUnitAnimation, true);
		DebugConfig.setOption(DebugOption.SystemUpdateBuildings, true);
		DebugConfig.setOption(DebugOption.SystemUpdateUnits, true);
		DebugConfig.setOption(DebugOption.SystemUpdateCities, true);
		DebugConfig.setOption(DebugOption.SystemRedrawMap, true);
		DebugConfig.setOption(DebugOption.SystemCalculateIslands, true);
		DebugConfig.setOption(DebugOption.SystemUpdateDirtyChunks, true);
		DebugConfig.setOption(DebugOption.SystemCheckGoodForBoat, false);
		DebugConfig.setOption(DebugOption.SystemUseCitizensDict, true);
		DebugConfig.setOption(DebugOption.SystemCityTasks, true);
		DebugConfig.setOption(DebugOption.SystemMusic, false);
		DebugConfig.setOption(DebugOption.Greg, false);
		DebugConfig.setOption(DebugOption.MakeUnitsFollowCursor, false);
		DebugConfig.setOption(DebugOption.SystemSplitAstar, true);
		DebugConfig.setOption(DebugOption.UseCacheForRegionPath, true);
		if (Config.isEditor)
		{
			Config.customMapSizeDefault = "standard";
			Config.ignoreStartupWindow = false;
			Config.loadSaveOnStart = true;
			Config.loadSaveOnStartSlot = 3;
			DebugConfig.setOption(DebugOption.Graphy, true);
			DebugConfig.setOption(DebugOption.SonicSpeed, true);
		}
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x00059EDC File Offset: 0x000580DC
	public static bool isOn(DebugOption pOption)
	{
		return DebugConfig.dictionary[pOption];
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x00059EE9 File Offset: 0x000580E9
	public static void switchOption(DebugOption pOption)
	{
		DebugConfig.setOption(pOption, !DebugConfig.isOn(pOption));
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x00059EFC File Offset: 0x000580FC
	public static void pressButton(DebugOption pOption)
	{
		if (pOption == DebugOption.NewDebugWindow)
		{
			DebugConfig.createTool("Game Info", 80, -10);
		}
		if (pOption == DebugOption.TabMain)
		{
			ScrollWindow.get("debug").GetComponent<UiDebugWindow>().setWindow(DebugOption.TabMain);
		}
		if (pOption == DebugOption.TabUnits)
		{
			ScrollWindow.get("debug").GetComponent<UiDebugWindow>().setWindow(DebugOption.TabUnits);
		}
		if (pOption == DebugOption.TabSystem)
		{
			ScrollWindow.get("debug").GetComponent<UiDebugWindow>().setWindow(DebugOption.TabSystem);
		}
		if (pOption == DebugOption.TabsLogs)
		{
			ScrollWindow.get("debug").GetComponent<UiDebugWindow>().setWindow(DebugOption.TabsLogs);
		}
		if (pOption == DebugOption.Console && Config.gameLoaded)
		{
			MapBox.instance.console.Show();
		}
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x00059FA3 File Offset: 0x000581A3
	public static void setOption(DebugOption pOption, bool pVal)
	{
		DebugConfig.dictionary[pOption] = pVal;
		if (pOption == DebugOption.Graphy)
		{
			DebugConfig.checkGraphy();
		}
		if (pOption == DebugOption.SonicSpeed)
		{
			if (pVal)
			{
				Config.timeScale = 40f;
				return;
			}
			Config.timeScale = 1f;
		}
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x00059FD8 File Offset: 0x000581D8
	public static void checkGraphy()
	{
		if (PrefabLibrary.instance != null)
		{
			PrefabLibrary.instance.graphy.gameObject.SetActive(DebugConfig.isOn(DebugOption.Graphy));
		}
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x0005A004 File Offset: 0x00058204
	public static void createTool(string pID, int pX = 80, int pY = -10)
	{
		DebugTool debugTool = Object.Instantiate<DebugTool>(PrefabLibrary.instance.debugTool, DebugConfig.instance.transform);
		for (int i = 0; i < debugTool.dropdown.options.Count; i++)
		{
			if (debugTool.dropdown.options[i].text == pID)
			{
				debugTool.dropdown.value = i;
				debugTool.dropdown.captionText.text = pID;
				break;
			}
		}
		debugTool.transform.localPosition = new Vector3((float)pX, (float)pY);
		DebugConfig.pos_x += 90;
	}

	// Token: 0x04000AAA RID: 2730
	public GameObject debugButton;

	// Token: 0x04000AAB RID: 2731
	public static DebugConfig instance;

	// Token: 0x04000AAC RID: 2732
	private static Dictionary<DebugOption, bool> dictionary;

	// Token: 0x04000AAD RID: 2733
	private static int pos_x;

	// Token: 0x04000AAE RID: 2734
	private static int pos_y;
}
