using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D3 RID: 723
public class MarkContainer
{
	// Token: 0x06000FA0 RID: 4000 RVA: 0x0008B635 File Offset: 0x00089835
	public MarkContainer(string pPath)
	{
		this.prefab = Resources.Load<MapMark>("civ/" + pPath);
	}

	// Token: 0x06000FA1 RID: 4001 RVA: 0x0008B66C File Offset: 0x0008986C
	public MapMark get()
	{
		MapMark mapMark;
		if (this.stack.Count == 0)
		{
			mapMark = Object.Instantiate<MapMark>(this.prefab, MapBox.instance.mapMarksArrows);
		}
		else
		{
			mapMark = this.stack.Pop();
		}
		this.active.Add(mapMark);
		return mapMark;
	}

	// Token: 0x06000FA2 RID: 4002 RVA: 0x0008B6B8 File Offset: 0x000898B8
	public void clear()
	{
		for (int i = 0; i < this.active.Count; i++)
		{
			MapMark mapMark = this.active[i];
			mapMark.gameObject.SetActive(false);
			mapMark.transform.position = Vector3.zero;
			this.stack.Push(mapMark);
		}
		this.active.Clear();
	}

	// Token: 0x040012CF RID: 4815
	private Stack<MapMark> stack = new Stack<MapMark>();

	// Token: 0x040012D0 RID: 4816
	private List<MapMark> active = new List<MapMark>();

	// Token: 0x040012D1 RID: 4817
	private MapMark prefab;
}
