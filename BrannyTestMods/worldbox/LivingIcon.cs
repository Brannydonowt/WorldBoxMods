using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000287 RID: 647
public class LivingIcon : MonoBehaviour
{
	// Token: 0x06000E3D RID: 3645 RVA: 0x00085388 File Offset: 0x00083588
	private void Awake()
	{
		this.init_position = base.transform.position;
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x0008539B File Offset: 0x0008359B
	public void kill()
	{
		LivingIcon.killed_mod++;
		base.enabled = false;
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x000853B0 File Offset: 0x000835B0
	public void Update()
	{
		Vector3 mousePosition = Input.mousePosition;
		float num = Vector2.Distance(base.transform.position, mousePosition);
		float num2 = (float)(80 + LivingIcon.killed_mod * 10);
		if (num < num2)
		{
			if (this.speed_away == 0f && LivingIcon.killed_mod > 6)
			{
				this.speed_away = (float)(LivingIcon.killed_mod * 10);
			}
			this.speed_away += 200f * Time.deltaTime * (float)LivingIcon.killed_mod;
		}
		else if (this.speed_away > 0f)
		{
			this.speed_away -= 500f * Time.deltaTime;
			if (this.speed_away < 0f)
			{
				this.speed_away = 0f;
			}
		}
		if (this.speed_away > 0f)
		{
			base.transform.position = Vector2.MoveTowards(base.transform.position, mousePosition, -1f * this.speed_away * Time.deltaTime);
			this.return_timer = 1f;
			this.speed_back = 0f;
			this.<Update>g__rotate|10_0();
			return;
		}
		if (this.return_timer > 0f)
		{
			this.return_timer -= Time.deltaTime;
			return;
		}
		if (Vector2.Distance(base.transform.position, this.init_position) > 1f)
		{
			this.speed_back += Time.deltaTime * 400f;
			base.transform.position = Vector2.MoveTowards(base.transform.position, this.init_position, Time.deltaTime * this.speed_back);
			return;
		}
		this.speed_back = 0f;
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x00085590 File Offset: 0x00083790
	[CompilerGenerated]
	private void <Update>g__rotate|10_0()
	{
		Vector3 eulerAngles = base.transform.eulerAngles;
		eulerAngles.z += 10f;
		base.transform.eulerAngles = eulerAngles;
	}

	// Token: 0x04001115 RID: 4373
	private Vector3 init_position;

	// Token: 0x04001116 RID: 4374
	private float speed_back;

	// Token: 0x04001117 RID: 4375
	private float speed_away;

	// Token: 0x04001118 RID: 4376
	private float return_timer;

	// Token: 0x04001119 RID: 4377
	private float timer_wait;

	// Token: 0x0400111A RID: 4378
	private float angle;

	// Token: 0x0400111B RID: 4379
	public float speed;

	// Token: 0x0400111C RID: 4380
	public static int killed_mod = 1;
}
