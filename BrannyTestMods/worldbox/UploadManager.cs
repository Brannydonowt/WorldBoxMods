using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200022A RID: 554
public class UploadManager : MonoBehaviour
{
	// Token: 0x04000F0C RID: 3852
	public static int uploadProgress = 0;

	// Token: 0x04000F0D RID: 3853
	public static int onlineProgress = 0;

	// Token: 0x04000F0E RID: 3854
	private static string mapUploadKey = "";

	// Token: 0x04000F0F RID: 3855
	private static string mapFileName = "";

	// Token: 0x04000F10 RID: 3856
	private static string mapPreviewName = "";

	// Token: 0x04000F11 RID: 3857
	public static string mapId = "";

	// Token: 0x04000F12 RID: 3858
	public static string mapName = "";

	// Token: 0x04000F13 RID: 3859
	public static string mapDescription = "";

	// Token: 0x04000F14 RID: 3860
	public static string uploadError = "";

	// Token: 0x04000F15 RID: 3861
	public static List<MapTagType> mapTags = new List<MapTagType>();

	// Token: 0x04000F16 RID: 3862
	private static Action<string> callback;

	// Token: 0x04000F17 RID: 3863
	private SavedMap data;

	// Token: 0x04000F18 RID: 3864
	private SaveManager saveManager;
}
