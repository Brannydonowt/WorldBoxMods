using System;
using System.Collections.Generic;

// Token: 0x02000192 RID: 402
public class WorldNameGenerator
{
	// Token: 0x0600093A RID: 2362 RVA: 0x00061878 File Offset: 0x0005FA78
	internal static string generateName()
	{
		if (Toolbox.randomBool())
		{
			return WorldNameGenerator.first.GetRandom<string>() + " " + WorldNameGenerator.second.GetRandom<string>();
		}
		return WorldNameGenerator.second.GetRandom<string>() + " of " + WorldNameGenerator.ofWords.GetRandom<string>();
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x000618D4 File Offset: 0x0005FAD4
	// Note: this type is marked as 'beforefieldinit'.
	static WorldNameGenerator()
	{
		List<string> list = new List<string>();
		list.Add("Forgotten");
		list.Add("New");
		list.Add("Red");
		list.Add("Blue");
		list.Add("Never");
		list.Add("Green");
		list.Add("Mirage");
		list.Add("Ever");
		list.Add("Hollow");
		list.Add("Infinite");
		list.Add("Empty");
		list.Add("Ghost");
		list.Add("Perfected");
		list.Add("God's");
		list.Add("Hidden");
		list.Add("Stolen");
		list.Add("Broken");
		list.Add("Nightmare");
		list.Add("Secret");
		list.Add("Sacred");
		list.Add("Cruel");
		list.Add("Phantom");
		list.Add("Free");
		list.Add("Dragon");
		list.Add("Demon");
		list.Add("Thunder");
		list.Add("Silent");
		list.Add("Old");
		list.Add("Ancient");
		list.Add("Eternity");
		list.Add("Flat");
		list.Add("Dream");
		WorldNameGenerator.first = list;
		List<string> list2 = new List<string>();
		list2.Add("Isles");
		list2.Add("Lands");
		list2.Add("Land");
		list2.Add("Realm");
		list2.Add("Earth");
		list2.Add("Sanctum");
		list2.Add("World");
		list2.Add("Planet");
		list2.Add("Archipelago");
		list2.Add("Territories");
		list2.Add("Acres");
		WorldNameGenerator.second = list2;
		List<string> list3 = new List<string>();
		list3.Add("Souls");
		list3.Add("Life");
		list3.Add("Sun");
		list3.Add("Moon");
		list3.Add("God");
		list3.Add("Blood");
		list3.Add("Dragons");
		list3.Add("Man");
		list3.Add("Greatness");
		list3.Add("Misery");
		list3.Add("Happiness");
		list3.Add("Death");
		list3.Add("Clouds");
		list3.Add("Rain");
		list3.Add("War");
		WorldNameGenerator.ofWords = list3;
	}

	// Token: 0x04000BD5 RID: 3029
	private static List<string> first;

	// Token: 0x04000BD6 RID: 3030
	private static List<string> second;

	// Token: 0x04000BD7 RID: 3031
	private static List<string> ofWords;
}
