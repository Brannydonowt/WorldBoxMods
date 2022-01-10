using System;
using System.Collections.Generic;

// Token: 0x02000023 RID: 35
public class CultureTechAsset : Asset
{
	// Token: 0x040000DB RID: 219
	public KingdomStats stats = new KingdomStats();

	// Token: 0x040000DC RID: 220
	public string icon;

	// Token: 0x040000DD RID: 221
	public float knowledge_cost = 1f;

	// Token: 0x040000DE RID: 222
	public int required_level;

	// Token: 0x040000DF RID: 223
	public TechType type;

	// Token: 0x040000E0 RID: 224
	public List<string> requirements;

	// Token: 0x040000E1 RID: 225
	public bool enabled = true;

	// Token: 0x040000E2 RID: 226
	public bool priority;
}
