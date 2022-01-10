using System;
using System.Collections.Generic;
using RSG;
using UnityEngine;

// Token: 0x020002CE RID: 718
internal class WorldLoader : MonoBehaviour
{
	// Token: 0x040012C8 RID: 4808
	public static WorldLoader instance;

	// Token: 0x040012C9 RID: 4809
	public const int worldNetTimeout = 25;

	// Token: 0x040012CA RID: 4810
	public static Dictionary<string, Map> mapCache = new Dictionary<string, Map>();

	// Token: 0x040012CB RID: 4811
	public static Dictionary<string, Promise<Dictionary<string, Map>>> listCache = new Dictionary<string, Promise<Dictionary<string, Map>>>();
}
