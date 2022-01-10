using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000201 RID: 513
public class ToolBenchmark
{
	// Token: 0x06000B72 RID: 2930 RVA: 0x0006F074 File Offset: 0x0006D274
	private static ToolBenchmarkData get(string pID, bool pNew = true)
	{
		ToolBenchmarkData toolBenchmarkData;
		ToolBenchmark.benchDict.TryGetValue(pID, ref toolBenchmarkData);
		if (toolBenchmarkData == null && pNew)
		{
			ToolBenchmark.benchDict.Add(pID, toolBenchmarkData = new ToolBenchmarkData());
		}
		return toolBenchmarkData;
	}

	// Token: 0x06000B73 RID: 2931 RVA: 0x0006F0AC File Offset: 0x0006D2AC
	public static float bench(string pID)
	{
		ToolBenchmarkData toolBenchmarkData = ToolBenchmark.get(pID, true);
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		toolBenchmarkData.put(realtimeSinceStartup);
		return realtimeSinceStartup;
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x0006F0CD File Offset: 0x0006D2CD
	public static void benchSet(string pID, float pVal)
	{
		ToolBenchmark.get(pID, true).set(pVal);
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x0006F0DC File Offset: 0x0006D2DC
	public static int getBenchCounter(string pID)
	{
		return (int)ToolBenchmark.get(pID, true).latest;
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x0006F0EC File Offset: 0x0006D2EC
	public static float benchEnd(string pID)
	{
		ToolBenchmarkData toolBenchmarkData = ToolBenchmark.get(pID, true);
		float num = Time.realtimeSinceStartup - toolBenchmarkData.latest;
		toolBenchmarkData.end(num);
		return num;
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x0006F118 File Offset: 0x0006D318
	public static string getBenchResult(string pID, bool pAverage = true)
	{
		return ToolBenchmark.getBenchResultFloat(pID, pAverage).ToString() ?? "";
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x0006F140 File Offset: 0x0006D340
	public static float getBenchResultFloat(string pID, bool pAverage = true)
	{
		ToolBenchmarkData toolBenchmarkData = ToolBenchmark.get(pID, false);
		if (toolBenchmarkData == null)
		{
			return -1f;
		}
		if (pAverage)
		{
			return toolBenchmarkData.getAverage();
		}
		return toolBenchmarkData.latestResult;
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x0006F170 File Offset: 0x0006D370
	public static void printBenchResult(string pID, bool pAverage)
	{
		string str = ToolBenchmark.getBenchResultFloat(pID, pAverage).ToString("0.##########");
		Debug.Log("#benchmark: " + pID + ": " + str);
	}

	// Token: 0x04000D9D RID: 3485
	private static Dictionary<string, ToolBenchmarkData> benchDict = new Dictionary<string, ToolBenchmarkData>();

	// Token: 0x04000D9E RID: 3486
	private static Dictionary<string, ToolBenchmarkData> benchDictLatest = new Dictionary<string, ToolBenchmarkData>();
}
