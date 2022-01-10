using System;
using System.Collections.Generic;
using System.ComponentModel;

// Token: 0x020000B3 RID: 179
[Serializable]
public class ActorData
{
	// Token: 0x040005EF RID: 1519
	public List<ItemData> items;

	// Token: 0x040005F0 RID: 1520
	public ActorBag inventory;

	// Token: 0x040005F1 RID: 1521
	public ActorStatus status;

	// Token: 0x040005F2 RID: 1522
	[DefaultValue("")]
	public string cityID = "";

	// Token: 0x040005F3 RID: 1523
	public int x;

	// Token: 0x040005F4 RID: 1524
	public int y;
}
