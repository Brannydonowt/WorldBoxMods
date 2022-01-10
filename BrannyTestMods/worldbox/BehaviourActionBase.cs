using System;
using ai.behaviours;

// Token: 0x02000156 RID: 342
public class BehaviourActionBase<T> : Asset
{
	// Token: 0x060007D9 RID: 2009 RVA: 0x00056881 File Offset: 0x00054A81
	public BehaviourActionBase()
	{
		this.create();
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x0005688F File Offset: 0x00054A8F
	public new virtual void create()
	{
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x00056891 File Offset: 0x00054A91
	public virtual BehResult startExecute(T pObject)
	{
		BehaviourActionBase<T>.world = MapBox.instance;
		if (this.errorsFound(pObject))
		{
			return BehResult.Stop;
		}
		return this.execute(pObject);
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x000568AF File Offset: 0x00054AAF
	public virtual BehResult execute(T pObject)
	{
		return BehResult.Continue;
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x000568B2 File Offset: 0x00054AB2
	public virtual bool errorsFound(T pObject)
	{
		return false;
	}

	// Token: 0x04000A49 RID: 2633
	protected static MapBox world;
}
