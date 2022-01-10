using System;
using System.Collections.Generic;

// Token: 0x0200015A RID: 346
public class TaskContainer<T>
{
	// Token: 0x060007E5 RID: 2021 RVA: 0x00056906 File Offset: 0x00054B06
	public void addCondition(T pCondition)
	{
		if (this.conditions == null)
		{
			this.conditions = new List<T>();
		}
		this.conditions.Add(pCondition);
	}

	// Token: 0x04000A4D RID: 2637
	public string id;

	// Token: 0x04000A4E RID: 2638
	public List<T> conditions;
}
