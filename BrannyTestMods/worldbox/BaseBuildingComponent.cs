using System;

// Token: 0x020000BC RID: 188
public class BaseBuildingComponent : BaseWorldObject
{
	// Token: 0x060003C6 RID: 966 RVA: 0x000395E7 File Offset: 0x000377E7
	internal override void create()
	{
		base.create();
		this.building = base.GetComponent<Building>();
	}

	// Token: 0x04000621 RID: 1569
	internal Building building;
}
