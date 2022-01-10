using System;
using System.Collections.Generic;

// Token: 0x0200002D RID: 45
public class GenericTest
{
	// Token: 0x06000142 RID: 322 RVA: 0x00016F42 File Offset: 0x00015142
	public T get<T>(int pI) where T : class
	{
		return this.list[pI] as T;
	}

	// Token: 0x06000143 RID: 323 RVA: 0x00016F5A File Offset: 0x0001515A
	public void Add(object pObject)
	{
		this.list.Add(pObject);
	}

	// Token: 0x04000105 RID: 261
	private List<object> list = new List<object>();
}
