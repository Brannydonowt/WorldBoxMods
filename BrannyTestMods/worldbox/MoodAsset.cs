using System;
using Beebyte.Obfuscator;
using UnityEngine;

// Token: 0x02000037 RID: 55
[ObfuscateLiterals]
public class MoodAsset : Asset
{
	// Token: 0x06000166 RID: 358 RVA: 0x00017B34 File Offset: 0x00015D34
	public Sprite getSprite()
	{
		if (this.sprite == null)
		{
			this.sprite = (Sprite)Resources.Load("ui/Icons/" + this.icon, typeof(Sprite));
		}
		return this.sprite;
	}

	// Token: 0x04000151 RID: 337
	public BaseStats baseStats = new BaseStats();

	// Token: 0x04000152 RID: 338
	public string icon;

	// Token: 0x04000153 RID: 339
	public Sprite sprite;
}
