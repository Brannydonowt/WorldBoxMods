using System;

// Token: 0x020000BD RID: 189
public class Beehive : BaseBuildingComponent
{
	// Token: 0x060003C8 RID: 968 RVA: 0x00039603 File Offset: 0x00037803
	public void addHoney()
	{
		if (this.honey >= 10)
		{
			return;
		}
		this.honey++;
	}

	// Token: 0x04000622 RID: 1570
	public int honey;
}
