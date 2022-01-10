using System;
using System.ComponentModel;

// Token: 0x02000077 RID: 119
[Serializable]
public class BaseObjectData
{
	// Token: 0x04000389 RID: 905
	[DefaultValue(true)]
	public bool alive = true;

	// Token: 0x0400038A RID: 906
	[DefaultValue(100)]
	public int health = 100;
}
