using System;

// Token: 0x020000E3 RID: 227
public class JobManagerBuildings : JobManagerBase<BatchBuildings, Building>
{
	// Token: 0x060004D6 RID: 1238 RVA: 0x0003FFED File Offset: 0x0003E1ED
	internal override void removeObject(Building pObject)
	{
		BatchBuildings batch = pObject.batch;
		pObject.batch.remove(pObject);
		this.batches_all_dirty = true;
	}
}
