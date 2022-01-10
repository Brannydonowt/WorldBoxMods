using System;
using System.IO;
using UnityEngine;

// Token: 0x0200001E RID: 30
public abstract class BaseAssetLibrary
{
	// Token: 0x060000DC RID: 220 RVA: 0x00012401 File Offset: 0x00010601
	public virtual void init()
	{
		string version = Application.version;
	}

	// Token: 0x060000DD RID: 221 RVA: 0x0001240C File Offset: 0x0001060C
	public void saveAssets()
	{
		string text = Application.streamingAssetsPath + "/modules/core/" + this.id + ".json";
		string text2 = JsonUtility.ToJson(this);
		File.WriteAllText(text, text2);
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00012440 File Offset: 0x00010640
	public virtual void checkCache()
	{
	}

	// Token: 0x040000A2 RID: 162
	public string id = "ASSET_LIBRARY";
}
