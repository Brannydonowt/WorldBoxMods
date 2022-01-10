using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D5 RID: 213
public class SpriteGroupSystem<T> : MonoBehaviour where T : GroupSpriteObject
{
	// Token: 0x0600046F RID: 1135 RVA: 0x0003D792 File Offset: 0x0003B992
	public virtual void create()
	{
		this.list_to_actors = new List<Actor>();
		this.list_to_buildings = new List<Building>();
		base.transform.name = "GroupSpriteController";
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x0003D7BA File Offset: 0x0003B9BA
	public virtual void update(float pElapsed)
	{
		this.finale();
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x0003D7C4 File Offset: 0x0003B9C4
	protected void checkRotation(GroupSpriteObject pObject, Actor pActor)
	{
		ref Vector3 ptr = ref pActor.curAngle;
		if (ptr.y != 0f || ptr.z != 0f)
		{
			this.t_pivot.Set(pActor.curTransformPosition.x, pActor.curTransformPosition.y, 0f);
			this.t_pos = Toolbox.RotatePointAroundPivot(ref this.t_pos, ref this.t_pivot, ref ptr);
		}
		pObject.setRotation(pActor.curAngle);
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x0003D83C File Offset: 0x0003BA3C
	private void finale()
	{
		this.clearLast();
		this.count_active = this.active.Count;
		this.count_stack = this.stack.Count;
		this.count_total = this.stack.Count + this.active.Count;
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x0003D890 File Offset: 0x0003BA90
	public void clearFull()
	{
		if (this.active.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this.active.Count; i++)
		{
			T t = this.active[i];
			t.gameObject.SetActive(false);
			this.stack.Push(t);
		}
		this.active.Clear();
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x0003D8F8 File Offset: 0x0003BAF8
	public void clearLast()
	{
		int i = this.active.Count - this.usedIndex;
		if (i > 0)
		{
			while (i > 0)
			{
				int num = this.active.Count - 1;
				T t = this.active[num];
				t.gameObject.SetActive(false);
				this.active.RemoveAt(num);
				this.stack.Push(t);
				i--;
			}
		}
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x0003D96C File Offset: 0x0003BB6C
	public T get()
	{
		T t;
		if (this.stack.Count == 0)
		{
			t = this.createNew();
		}
		else
		{
			t = this.stack.Pop();
			t.gameObject.SetActive(true);
		}
		this.active.Add(t);
		return t;
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x0003D9B9 File Offset: 0x0003BBB9
	protected virtual T createNew()
	{
		return Object.Instantiate<T>(this.prefab, base.gameObject.transform);
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x0003D9D1 File Offset: 0x0003BBD1
	public void clearList()
	{
		this.list_to_actors.Clear();
		this.list_to_buildings.Clear();
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x0003D9E9 File Offset: 0x0003BBE9
	public void add(Actor pActor)
	{
		this.list_to_actors.Add(pActor);
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x0003D9F7 File Offset: 0x0003BBF7
	public void add(Building pBuilding)
	{
		this.list_to_buildings.Add(pBuilding);
	}

	// Token: 0x04000699 RID: 1689
	protected float z_pos = 0.05f;

	// Token: 0x0400069A RID: 1690
	protected Stack<T> stack = new Stack<T>();

	// Token: 0x0400069B RID: 1691
	protected List<T> active = new List<T>();

	// Token: 0x0400069C RID: 1692
	internal T prefab;

	// Token: 0x0400069D RID: 1693
	public int count_active;

	// Token: 0x0400069E RID: 1694
	public int count_stack;

	// Token: 0x0400069F RID: 1695
	public int count_total;

	// Token: 0x040006A0 RID: 1696
	public int usedIndex;

	// Token: 0x040006A1 RID: 1697
	internal List<Actor> list_to_actors;

	// Token: 0x040006A2 RID: 1698
	internal List<Building> list_to_buildings;

	// Token: 0x040006A3 RID: 1699
	protected Vector3 t_pos;

	// Token: 0x040006A4 RID: 1700
	protected Vector3 t_pivot;
}
