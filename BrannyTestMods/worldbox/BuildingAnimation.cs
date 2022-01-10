using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C0 RID: 192
public class BuildingAnimation
{
	// Token: 0x06000426 RID: 1062 RVA: 0x0003C097 File Offset: 0x0003A297
	public void createSpriteArray()
	{
		this.frames = this.list.ToArray();
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x0003C0AA File Offset: 0x0003A2AA
	public int sorter(Sprite p1, Sprite p2)
	{
		return p2.name.CompareTo(p1.name);
	}

	// Token: 0x0400064C RID: 1612
	internal Sprite[] frames;

	// Token: 0x0400064D RID: 1613
	internal List<Sprite> list = new List<Sprite>();

	// Token: 0x0400064E RID: 1614
	internal float timeBetweenFrames = 0.5f;
}
