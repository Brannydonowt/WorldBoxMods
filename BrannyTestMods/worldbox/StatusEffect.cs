using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class StatusEffect : Asset
{
	// Token: 0x040001B5 RID: 437
	public WorldAction action;

	// Token: 0x040001B6 RID: 438
	public WorldAction actionOnHit;

	// Token: 0x040001B7 RID: 439
	public float actionInterval;

	// Token: 0x040001B8 RID: 440
	public float duration = 10f;

	// Token: 0x040001B9 RID: 441
	public string texture = string.Empty;

	// Token: 0x040001BA RID: 442
	public bool random_frame;

	// Token: 0x040001BB RID: 443
	public bool animated;

	// Token: 0x040001BC RID: 444
	public float animation_speed = 0.1f;

	// Token: 0x040001BD RID: 445
	public float animation_speed_random;

	// Token: 0x040001BE RID: 446
	public float startScale;

	// Token: 0x040001BF RID: 447
	public float targetScale;

	// Token: 0x040001C0 RID: 448
	public bool random_flip;

	// Token: 0x040001C1 RID: 449
	public bool cancelActorJob;

	// Token: 0x040001C2 RID: 450
	public BaseStats baseStats = new BaseStats();

	// Token: 0x040001C3 RID: 451
	internal List<string> oppositeTraits = new List<string>();

	// Token: 0x040001C4 RID: 452
	internal List<string> oppositeStatus = new List<string>();

	// Token: 0x040001C5 RID: 453
	internal List<string> removeStatus = new List<string>();

	// Token: 0x040001C6 RID: 454
	internal Sprite[] spriteList;
}
