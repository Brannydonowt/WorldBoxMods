using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000200 RID: 512
public static class Benchmark
{
	// Token: 0x06000B6E RID: 2926 RVA: 0x0006EACC File Offset: 0x0006CCCC
	public static void testVirtual()
	{
		int num = 1000;
		BenchTest1 benchTest = new BenchTest1();
		BenchTest2 benchTest2 = new BenchTest2();
		Toolbox.bench("BenchTest - normal");
		for (int i = 0; i < num; i++)
		{
			benchTest.test();
		}
		Toolbox.benchEnd("BenchTest - normal");
		Toolbox.bench("BenchTest - virtual");
		for (int j = 0; j < num; j++)
		{
			benchTest2.testVirtual();
		}
		Toolbox.benchEnd("BenchTest - virtual");
		Debug.Log("Benchmark:");
		Debug.Log("- BenchTest - normal:" + Toolbox.getBenchResult("BenchTest - normal", true));
		Debug.Log("- BenchTest - virtual:" + Toolbox.getBenchResult("BenchTest - virtual", true));
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x0006EB80 File Offset: 0x0006CD80
	public static void testQueue()
	{
		int num = 10000;
		List<TileType> list = new List<TileType>();
		Queue<TileType> queue = new Queue<TileType>();
		LinkedList<TileType> linkedList = new LinkedList<TileType>();
		for (int i = 0; i < num; i++)
		{
			list.Add(new TileType());
			queue.Enqueue(new TileType());
			linkedList.AddLast(new TileType());
		}
		Toolbox.bench("list");
		for (int j = 0; j < list.Count; j++)
		{
			TileType tileType = list[0];
			list.RemoveAt(0);
		}
		Toolbox.benchEnd("list");
		Toolbox.bench("queue");
		for (int k = 0; k < queue.Count; k++)
		{
			queue.Dequeue();
		}
		Toolbox.benchEnd("queue");
		Toolbox.bench("linked");
		for (int l = 0; l < linkedList.Count; l++)
		{
			LinkedListNode<TileType> first = linkedList.First;
			linkedList.RemoveFirst();
		}
		Toolbox.benchEnd("linked");
		Debug.Log("!!!BENCH REMOVE AT 0 " + num.ToString());
		Toolbox.printBenchResult("list", true);
		Toolbox.printBenchResult("queue", true);
		Toolbox.printBenchResult("linked", true);
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x0006ECB4 File Offset: 0x0006CEB4
	public static void testRemoveStructs()
	{
		int num = 100;
		int num2 = 500;
		List<Vector3> list = new List<Vector3>();
		List<Vector3> list2 = new List<Vector3>();
		List<Vector3> list3 = new List<Vector3>();
		List<Vector3> list4 = new List<Vector3>();
		HashSet<Vector3> hashSet = new HashSet<Vector3>();
		for (int i = 0; i < num2; i++)
		{
			list.Add(new Vector3
			{
				x = (float)Toolbox.randomInt(0, 1000),
				y = (float)Toolbox.randomInt(0, 1000),
				z = (float)Toolbox.randomInt(0, 1000)
			});
		}
		list.Shuffle<Vector3>();
		for (int j = 0; j < num; j++)
		{
			list2.Add(list.GetRandom<Vector3>());
		}
		Toolbox.bench("remove");
		foreach (Vector3 vector in list)
		{
			list3.Add(vector);
		}
		for (int k = 0; k < num; k++)
		{
			list3.Remove(list2[k]);
		}
		Toolbox.benchEnd("remove");
		Toolbox.bench("RemoveAtSwapBack");
		foreach (Vector3 vector2 in list)
		{
			list4.Add(vector2);
		}
		for (int l = 0; l < num; l++)
		{
			list4.RemoveAtSwapBack(list2[l]);
		}
		Toolbox.benchEnd("RemoveAtSwapBack");
		Toolbox.benchEnd("remove_native");
		Toolbox.bench("remove_hashset");
		foreach (Vector3 vector3 in list)
		{
			hashSet.Add(vector3);
		}
		for (int m = 0; m < num; m++)
		{
			hashSet.Remove(list2[m]);
		}
		Toolbox.benchEnd("remove_hashset");
		Debug.Log("Benchmark:");
		Debug.Log("- built-in remove:" + Toolbox.getBenchResult("remove", true));
		Debug.Log("- own RemoveAtSwapBack: " + Toolbox.getBenchResult("RemoveAtSwapBack", true));
		Debug.Log("- native RemoveAtSwapBack: " + Toolbox.getBenchResult("remove_native", true));
		Debug.Log("- remove hashset: " + Toolbox.getBenchResult("remove_hashset", true));
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x0006EF4C File Offset: 0x0006D14C
	public static void testRemove()
	{
		int num = 1000;
		List<ActorAsset> list = new List<ActorAsset>();
		List<ActorAsset> list2 = new List<ActorAsset>();
		List<ActorAsset> list3 = new List<ActorAsset>();
		List<ActorAsset> list4 = new List<ActorAsset>();
		for (int i = 0; i < 1000; i++)
		{
			ActorAsset actorAsset = new ActorAsset();
			list.Add(actorAsset);
		}
		list.Shuffle<ActorAsset>();
		list3.AddRange(list);
		list4.AddRange(list);
		for (int j = 0; j < num; j++)
		{
			list2.Add(list.GetRandom<ActorAsset>());
		}
		Toolbox.bench("removeAt");
		for (int k = 0; k < num; k++)
		{
			list3.Remove(list2[k]);
		}
		Toolbox.benchEnd("removeAt");
		Toolbox.bench("RemoveAtSwapBack");
		for (int l = 0; l < num; l++)
		{
			list4.RemoveAtSwapBack(list2[l]);
		}
		Toolbox.benchEnd("RemoveAtSwapBack");
		Debug.Log("Benchmark:");
		Debug.Log("- built-in removeAt:" + Toolbox.getBenchResult("removeAt", true));
		Debug.Log("- own RemoveAtSwapBack: " + Toolbox.getBenchResult("RemoveAtSwapBack", true));
	}
}
