using System;
using UnityEngine;

// Token: 0x02000079 RID: 121
public class BaseWorldObject : MonoBehaviour
{
	// Token: 0x060002C4 RID: 708 RVA: 0x0002E39B File Offset: 0x0002C59B
	private void Start()
	{
		this.onStart();
		if (!this.created)
		{
			this.create();
		}
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x0002E3B1 File Offset: 0x0002C5B1
	public virtual void update(float pElapsed)
	{
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x0002E3B3 File Offset: 0x0002C5B3
	internal virtual void create()
	{
		this.created = true;
		this.m_gameObject = base.gameObject;
		this.m_transform = this.m_gameObject.transform;
		this.setWorld();
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x0002E3DF File Offset: 0x0002C5DF
	protected virtual void onStart()
	{
		this.setWorld();
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x0002E3E7 File Offset: 0x0002C5E7
	public void setWorld()
	{
		this.world = MapBox.instance;
	}

	// Token: 0x0400039F RID: 927
	internal bool created;

	// Token: 0x040003A0 RID: 928
	protected MapBox world;

	// Token: 0x040003A1 RID: 929
	internal Transform m_transform;

	// Token: 0x040003A2 RID: 930
	internal GameObject m_gameObject;
}
