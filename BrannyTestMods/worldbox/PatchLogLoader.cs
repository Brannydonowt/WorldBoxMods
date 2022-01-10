using System;
using UnityEngine;

// Token: 0x02000262 RID: 610
public class PatchLogLoader : LongTextLoader
{
	// Token: 0x06000D33 RID: 3379 RVA: 0x0007DF54 File Offset: 0x0007C154
	public override void create()
	{
		try
		{
			this.m_text.text = this.textAsset.text;
		}
		catch (Exception)
		{
			Debug.LogError("PatchLogLoader: Text File is too long");
		}
	}

	// Token: 0x0400100E RID: 4110
	public TextAsset textAssetCN;
}
