using System;
using System.ComponentModel;

// Token: 0x020000B2 RID: 178
[Serializable]
public class ActorBag
{
	// Token: 0x0600039F RID: 927 RVA: 0x00038A58 File Offset: 0x00036C58
	public void add(string pRes, int pAmount)
	{
		this.resource = pRes;
		this.amount += pAmount;
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00038A6F File Offset: 0x00036C6F
	public void empty()
	{
		this.resource = string.Empty;
		this.amount = 0;
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00038A83 File Offset: 0x00036C83
	public int getResource(string pRes)
	{
		if (this.resource != pRes)
		{
			return 0;
		}
		return this.amount;
	}

	// Token: 0x040005ED RID: 1517
	[DefaultValue("")]
	public string resource = string.Empty;

	// Token: 0x040005EE RID: 1518
	[DefaultValue(0)]
	public int amount;
}
