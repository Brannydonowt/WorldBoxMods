using System;
using System.Collections.Generic;

// Token: 0x02000158 RID: 344
public class BehaviourTaskBase<T> : Asset
{
	// Token: 0x060007E0 RID: 2016 RVA: 0x000568C0 File Offset: 0x00054AC0
	public BehaviourTaskBase()
	{
		this.create();
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x000568D9 File Offset: 0x00054AD9
	public new virtual void create()
	{
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x000568DB File Offset: 0x00054ADB
	public T get(int pIndex)
	{
		return this.list[pIndex];
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x000568E9 File Offset: 0x00054AE9
	public void addBeh(T pAction)
	{
		this.list.Add(pAction);
	}

	// Token: 0x04000A4A RID: 2634
	public List<T> list = new List<T>();
}
