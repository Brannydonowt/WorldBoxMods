using System;
using UnityEngine;

// Token: 0x02000107 RID: 263
public class ExplosionFlash : BaseEffect
{
	// Token: 0x060005D9 RID: 1497 RVA: 0x00046AC4 File Offset: 0x00044CC4
	public void startFlash(WorldTile pTile, int pRadius)
	{
		this.startFlash(new Vector3(pTile.posV3.x, pTile.posV3.y), pRadius, 1f);
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x00046AED File Offset: 0x00044CED
	public void startFlash(Vector3 pVector, int pRadius, float pSpeed = 1f)
	{
		this.speed = pSpeed;
		base.transform.position = new Vector3(pVector.x, pVector.y);
		base.setScale(0.1f);
		base.setAlpha(1f);
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x00046B28 File Offset: 0x00044D28
	private void Update()
	{
		base.setAlpha(this.alpha - this.world.elapsed * this.speed * 0.5f);
		base.setScale(this.scale += this.world.elapsed * this.speed * 0.1f);
		if (this.alpha <= 0f)
		{
			base.kill();
		}
	}

	// Token: 0x040007C2 RID: 1986
	private float speed;
}
