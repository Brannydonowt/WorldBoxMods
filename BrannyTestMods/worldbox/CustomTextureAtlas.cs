using System;

// Token: 0x020001A9 RID: 425
public class CustomTextureAtlas
{
	// Token: 0x060009AA RID: 2474 RVA: 0x00065240 File Offset: 0x00063440
	public static bool filesExists()
	{
		return true;
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x00065250 File Offset: 0x00063450
	public static void createUnityBin()
	{
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x00065260 File Offset: 0x00063460
	private static void save(string pData)
	{
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x0006526D File Offset: 0x0006346D
	internal static void delete(string pTexture)
	{
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x00065270 File Offset: 0x00063470
	public static string createTextureID(string pString)
	{
		string pID = CustomTextureAtlas.width.ToString() + CustomTextureAtlas.height.ToString();
		return Toolbox.textureID(pString, pID);
	}

	// Token: 0x04000C51 RID: 3153
	private static int _save_counter = 0;

	// Token: 0x04000C52 RID: 3154
	private static bool create_atlases_editor = false;

	// Token: 0x04000C53 RID: 3155
	private static int _f_index = 0;

	// Token: 0x04000C54 RID: 3156
	private static int _total_files = 4;

	// Token: 0x04000C55 RID: 3157
	public static string name = "texture_atlas";

	// Token: 0x04000C56 RID: 3158
	public static int TOTAL_DATAS = 0;

	// Token: 0x04000C57 RID: 3159
	private static bool done = false;

	// Token: 0x04000C58 RID: 3160
	private static int width = 1202;

	// Token: 0x04000C59 RID: 3161
	private static int height = 2021;
}
