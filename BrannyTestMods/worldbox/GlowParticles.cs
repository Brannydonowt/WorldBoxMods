using System;
using UnityEngine;

// Token: 0x0200010A RID: 266
public class GlowParticles : MonoBehaviour
{
	// Token: 0x060005ED RID: 1517 RVA: 0x00047668 File Offset: 0x00045868
	private void Awake()
	{
		this.particles = base.GetComponent<ParticleSystem>();
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x00047676 File Offset: 0x00045876
	private void Update()
	{
		if (this.cooldown > 0f)
		{
			this.cooldown -= Time.deltaTime;
		}
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x00047698 File Offset: 0x00045898
	public void spawn(float pX, float pY, bool pRemoveCooldown = false)
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.particles.particleCount > 50)
		{
			return;
		}
		if (pRemoveCooldown)
		{
			this.cooldown = 0f;
		}
		if (this.cooldown > 0f)
		{
			return;
		}
		this.cooldown = 0.2f + Toolbox.randomFloat(0f, 0.3f);
		ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
		emitParams.position = new Vector3(pX, pY);
		this.particles.Emit(emitParams, 1);
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x00047718 File Offset: 0x00045918
	public void spawn(Vector3 pPos)
	{
		this.spawn(pPos.x, pPos.y, false);
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x0004772D File Offset: 0x0004592D
	public void clear()
	{
		this.particles.Clear();
	}

	// Token: 0x040007CF RID: 1999
	private float cooldown;

	// Token: 0x040007D0 RID: 2000
	public ParticleSystem particles;
}
