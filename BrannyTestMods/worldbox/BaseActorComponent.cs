using System;

// Token: 0x020000B5 RID: 181
public class BaseActorComponent : BaseWorldObject
{
	// Token: 0x060003A4 RID: 932 RVA: 0x00038AC1 File Offset: 0x00036CC1
	internal override void create()
	{
		this.actor = base.gameObject.GetComponent<Actor>();
		base.create();
	}

	// Token: 0x04000601 RID: 1537
	internal Actor actor;
}
