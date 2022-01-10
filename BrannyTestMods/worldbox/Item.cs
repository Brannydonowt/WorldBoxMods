using System;
using UnityEngine;

// Token: 0x0200008F RID: 143
public class Item : MonoBehaviour
{
	// Token: 0x0600031A RID: 794 RVA: 0x00033701 File Offset: 0x00031901
	private void Start()
	{
		this.status = ItemStatus.OnGround;
	}

	// Token: 0x0600031B RID: 795 RVA: 0x0003370A File Offset: 0x0003190A
	public void remove()
	{
		this.controller.returnToPool(this);
	}

	// Token: 0x04000512 RID: 1298
	internal ItemsController controller;

	// Token: 0x04000513 RID: 1299
	public WorldTile tile;

	// Token: 0x04000514 RID: 1300
	public string id;

	// Token: 0x04000515 RID: 1301
	public ItemStatus status;
}
