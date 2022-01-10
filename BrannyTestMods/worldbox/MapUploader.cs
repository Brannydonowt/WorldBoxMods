using System;
using RSG;

// Token: 0x0200021B RID: 539
public class MapUploader
{
	// Token: 0x06000C00 RID: 3072 RVA: 0x00076CE4 File Offset: 0x00074EE4
	public static Promise<string> uploadMap()
	{
		string text = DateTime.UtcNow.ToString("yyyyMMdd");
		return S3Manager.instance.UploadFileToAWS3(string.Concat(new string[]
		{
			"wbox/",
			text.ToString(),
			"/",
			Auth.userId,
			"_",
			Guid.NewGuid().ToString(),
			".wbox"
		}), MapUploader.getMapData());
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x00076D65 File Offset: 0x00074F65
	private static byte[] getMapData()
	{
		return SaveManager.getMapFromPath(SaveManager.currentSavePath, false).toZip();
	}
}
