using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000138 RID: 312
public class DragonAsset : ScriptableObject
{
	// Token: 0x0600074B RID: 1867 RVA: 0x0005316C File Offset: 0x0005136C
	public DragonAssetContainer getAsset(DragonState pState)
	{
		if (this.dict == null)
		{
			this.dict = new Dictionary<DragonState, DragonAssetContainer>();
			foreach (DragonAssetContainer dragonAssetContainer in this.list)
			{
				this.dict.Add(dragonAssetContainer.id, dragonAssetContainer);
			}
		}
		return this.dict[pState];
	}

	// Token: 0x040009A9 RID: 2473
	public Dictionary<DragonState, DragonAssetContainer> dict;

	// Token: 0x040009AA RID: 2474
	public DragonAssetContainer[] list;
}
