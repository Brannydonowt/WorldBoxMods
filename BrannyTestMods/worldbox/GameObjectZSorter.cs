using System;
using UnityEngine;

// Token: 0x0200008A RID: 138
public class GameObjectZSorter : MonoBehaviour
{
	// Token: 0x0600030E RID: 782 RVA: 0x000333E1 File Offset: 0x000315E1
	private void Start()
	{
		this.z_order_offset = -(float)base.GetComponent<SpriteRenderer>().sortingOrder / 10f;
		base.GetComponent<SpriteRenderer>().sortingOrder = 0;
	}

	// Token: 0x0600030F RID: 783 RVA: 0x00033408 File Offset: 0x00031608
	private void Update()
	{
		Vector3 position = this.zParent.transform.position;
		if (this.start_time > 0f)
		{
			this.start_time -= Time.deltaTime;
		}
		if (position.z != this.lastZ || this.start_time > 0f)
		{
			this.lastZ = position.z;
			this.pos = base.transform.position;
			this.pos.z = this.lastZ + this.z_order_offset;
			base.transform.position = this.pos;
		}
	}

	// Token: 0x040004B8 RID: 1208
	public GameObject zParent;

	// Token: 0x040004B9 RID: 1209
	public float z_order_offset;

	// Token: 0x040004BA RID: 1210
	private Vector3 pos;

	// Token: 0x040004BB RID: 1211
	private float lastZ;

	// Token: 0x040004BC RID: 1212
	private float start_time = 0.1f;
}
