using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000179 RID: 377
public class ItemsController : MonoBehaviour
{
	// Token: 0x06000885 RID: 2181 RVA: 0x0005C276 File Offset: 0x0005A476
	private void Awake()
	{
		this.active = new List<Item>();
		this.pool = new List<Item>();
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x0005C28E File Offset: 0x0005A48E
	public void returnToPool(Item pObject)
	{
		pObject.status = ItemStatus.Removed;
		pObject.gameObject.SetActive(false);
		this.pool.Add(pObject);
		this.active.Remove(pObject);
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x0005C2BC File Offset: 0x0005A4BC
	public void spawnMeat(WorldTile pTile)
	{
		Item item = this.getItem("meat");
		this.spawnItem(item, pTile);
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x0005C2E0 File Offset: 0x0005A4E0
	public void spawnBurger(WorldTile pTile)
	{
		Item item = this.getItem("burger");
		this.spawnItem(item, pTile);
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x0005C304 File Offset: 0x0005A504
	public void spawnItem(Item pItem, WorldTile pTile)
	{
		pItem.tile = pTile;
		pItem.transform.localPosition = new Vector3((float)pTile.pos.x + 0.5f, (float)pTile.pos.y + 0.5f);
		this.active.Add(pItem);
		pItem.transform.parent = base.transform;
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x0005C370 File Offset: 0x0005A570
	private Item getItem(string pID)
	{
		Item item = null;
		for (int i = 0; i < this.pool.Count; i++)
		{
			Item item2 = this.pool[i];
			if (!(item2.id != pID))
			{
				item = item2;
				break;
			}
		}
		if (item != null)
		{
			this.pool.Remove(item);
		}
		if (item == null)
		{
			for (int j = 0; j < this.prefabs.Count; j++)
			{
				Item item3 = this.prefabs[j];
				if (!(item3.id != pID))
				{
					item = Object.Instantiate<Item>(item3);
					break;
				}
			}
		}
		item.gameObject.SetActive(true);
		item.controller = this;
		item.status = ItemStatus.OnGround;
		return item;
	}

	// Token: 0x04000AF9 RID: 2809
	public List<Item> prefabs;

	// Token: 0x04000AFA RID: 2810
	private List<Item> active;

	// Token: 0x04000AFB RID: 2811
	private List<Item> pool;
}
