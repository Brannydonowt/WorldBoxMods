using System;

// Token: 0x02000157 RID: 343
public abstract class BehaviourBaseCondition<T>
{
	// Token: 0x060007DE RID: 2014 RVA: 0x000568B5 File Offset: 0x00054AB5
	public virtual bool check(T pObject)
	{
		return true;
	}
}
