using System;
using System.IO;
using RSG;

// Token: 0x0200021C RID: 540
public class PreviewUploader
{
	// Token: 0x06000C03 RID: 3075 RVA: 0x00076D80 File Offset: 0x00074F80
	public static Promise<string> uploadImagePreview()
	{
		string text = DateTime.UtcNow.ToString("yyyyMMdd");
		return S3Manager.instance.UploadFileToAWS3(string.Concat(new string[]
		{
			"png/",
			text.ToString(),
			"/",
			Auth.userId,
			"_",
			Guid.NewGuid().ToString(),
			".png"
		}), PreviewUploader.getImagePreview());
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x00076E01 File Offset: 0x00075001
	private static byte[] getImagePreview()
	{
		return File.ReadAllBytes(SaveManager.getPngSlotPath(-1));
	}
}
