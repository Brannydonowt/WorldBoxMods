using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000097 RID: 151
public class ObjectContainer<T> : IEnumerable<T>, IEnumerable
{
	// Token: 0x0600032A RID: 810 RVA: 0x00034BC9 File Offset: 0x00032DC9
	public IEnumerator<T> GetEnumerator()
	{
		return this._hashSet.GetEnumerator();
	}

	// Token: 0x0600032B RID: 811 RVA: 0x00034BDB File Offset: 0x00032DDB
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x0600032C RID: 812 RVA: 0x00034BE3 File Offset: 0x00032DE3
	public bool Contains(T pObject)
	{
		return this._hashSet.Contains(pObject);
	}

	// Token: 0x0600032D RID: 813 RVA: 0x00034BF1 File Offset: 0x00032DF1
	public void Clear()
	{
		this._hashSet.Clear();
		this._to_add.Clear();
		this._to_remove.Clear();
		if (this._simpleList != null)
		{
			this._simpleList.Clear();
		}
		this._simpleListDirty = false;
	}

	// Token: 0x0600032E RID: 814 RVA: 0x00034C2E File Offset: 0x00032E2E
	public void doChecks()
	{
		this.checkAddRemove();
		this.checkSimpleListDirty();
	}

	// Token: 0x0600032F RID: 815 RVA: 0x00034C3C File Offset: 0x00032E3C
	public void checkAddRemove()
	{
		if (this._to_add.Count > 0)
		{
			this._simpleListDirty = true;
			for (int i = 0; i < this._to_add.Count; i++)
			{
				T t = this._to_add[i];
				this._hashSet.Add(t);
			}
			this._to_add.Clear();
		}
		if (this._to_remove.Count > 0)
		{
			this._simpleListDirty = true;
			for (int j = 0; j < this._to_remove.Count; j++)
			{
				T t2 = this._to_remove[j];
				this._hashSet.Remove(t2);
			}
			this._to_remove.Clear();
		}
	}

	// Token: 0x06000330 RID: 816 RVA: 0x00034CE9 File Offset: 0x00032EE9
	public List<T> getSimpleList()
	{
		if (this._simpleList == null)
		{
			this._simpleList = new List<T>();
		}
		this.checkSimpleListDirty();
		return this._simpleList;
	}

	// Token: 0x06000331 RID: 817 RVA: 0x00034D0C File Offset: 0x00032F0C
	private void checkSimpleListDirty()
	{
		if (this._simpleListDirty)
		{
			if (this._simpleList == null)
			{
				this._simpleList = new List<T>();
			}
			this._simpleList.Clear();
			foreach (T t in this._hashSet)
			{
				this._simpleList.Add(t);
			}
			this._simpleListDirty = false;
		}
	}

	// Token: 0x06000332 RID: 818 RVA: 0x00034D94 File Offset: 0x00032F94
	public T GetRandom()
	{
		this.checkSimpleListDirty();
		if (this._simpleList == null || this._simpleList.Count == 0)
		{
			return default(T);
		}
		return this._simpleList.GetRandom<T>();
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000333 RID: 819 RVA: 0x00034DD1 File Offset: 0x00032FD1
	public int Count
	{
		get
		{
			return this._hashSet.Count;
		}
	}

	// Token: 0x06000334 RID: 820 RVA: 0x00034DE0 File Offset: 0x00032FE0
	public string debug()
	{
		return string.Concat(new string[]
		{
			this._hashSet.Count.ToString(),
			"/",
			this._to_add.Count.ToString(),
			"/",
			this._to_remove.Count.ToString()
		});
	}

	// Token: 0x06000335 RID: 821 RVA: 0x00034E4A File Offset: 0x0003304A
	public void Add(T pObject)
	{
		this._to_add.Add(pObject);
	}

	// Token: 0x06000336 RID: 822 RVA: 0x00034E58 File Offset: 0x00033058
	public void Remove(T pObject)
	{
		this._to_remove.Add(pObject);
	}

	// Token: 0x06000337 RID: 823 RVA: 0x00034E68 File Offset: 0x00033068
	public void putInSimpleList(List<T> pList)
	{
		foreach (T t in this._hashSet)
		{
			pList.Add(t);
		}
	}

	// Token: 0x0400052B RID: 1323
	private List<T> _to_remove = new List<T>();

	// Token: 0x0400052C RID: 1324
	private List<T> _to_add = new List<T>();

	// Token: 0x0400052D RID: 1325
	private HashSet<T> _hashSet = new HashSet<T>();

	// Token: 0x0400052E RID: 1326
	private bool _simpleListDirty;

	// Token: 0x0400052F RID: 1327
	private List<T> _simpleList;
}
