using System;
using System.Collections.Generic;
using Beebyte.Obfuscator;
using UnityEngine;

// Token: 0x0200001C RID: 28
[ObfuscateLiterals]
[Serializable]
public abstract class AssetLibrary<T> : BaseAssetLibrary where T : Asset
{
	// Token: 0x060000D1 RID: 209 RVA: 0x00011CE8 File Offset: 0x0000FEE8
	public virtual T get(string pID)
	{
		T t = default(T);
		this.dict.TryGetValue(pID, ref t);
		if (t == null)
		{
			Debug.Log("asset " + pID + " not found in lib " + this.id);
			return default(T);
		}
		return t;
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00011D3C File Offset: 0x0000FF3C
	public virtual T add(T pAsset)
	{
		string id = pAsset.id;
		if (this.dict.ContainsKey(id))
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				if (!(this.list[i].id != id))
				{
					this.list.RemoveAt(i);
					break;
				}
			}
			this.dict.Remove(id);
			Debug.Log("[" + this.id + "] found new asset to replace... " + id);
		}
		this.t = pAsset;
		this.t.create();
		this.list.Add(pAsset);
		this.dict.Add(id, pAsset);
		return pAsset;
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00011E00 File Offset: 0x00010000
	public virtual T clone(string pNew, string pFrom)
	{
		T t = JsonUtility.FromJson<T>(JsonUtility.ToJson(this.dict[pFrom]));
		this.t = t;
		this.t.id = pNew;
		this.add(this.t);
		return this.t;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00011E54 File Offset: 0x00010054
	public void saveToFile(string pPath = "units.json")
	{
		Application.streamingAssetsPath + "/modules/core/" + pPath;
	}

	// Token: 0x04000077 RID: 119
	public List<T> list = new List<T>();

	// Token: 0x04000078 RID: 120
	public Dictionary<string, T> dict = new Dictionary<string, T>();

	// Token: 0x04000079 RID: 121
	protected T t;
}
