using System;
using System.Collections.Generic;

// Token: 0x02000048 RID: 72
public class Spell : Asset
{
	// Token: 0x040001AF RID: 431
	public float healthPercent;

	// Token: 0x040001B0 RID: 432
	public float chance = 1f;

	// Token: 0x040001B1 RID: 433
	public float min_distance;

	// Token: 0x040001B2 RID: 434
	public CastTarget castTarget;

	// Token: 0x040001B3 RID: 435
	public CastEntity castEntity = CastEntity.Both;

	// Token: 0x040001B4 RID: 436
	public List<WorldAction> action = new List<WorldAction>();
}
