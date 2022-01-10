using System;
using UnityEngine;

// Token: 0x0200003B RID: 59
public class PersonalityAsset : Asset
{
	// Token: 0x06000175 RID: 373 RVA: 0x0001B5E0 File Offset: 0x000197E0
	public Sprite getSprite()
	{
		if (this.sprite == null)
		{
			this.sprite = (Sprite)Resources.Load("ui/Icons/" + this.icon, typeof(Sprite));
		}
		return this.sprite;
	}

	// Token: 0x04000166 RID: 358
	public BaseStats baseStats = new BaseStats();

	// Token: 0x04000167 RID: 359
	public string icon;

	// Token: 0x04000168 RID: 360
	public Sprite sprite;
}
