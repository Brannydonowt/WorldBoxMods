using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000106 RID: 262
public class EffectInfinityCoin : BaseEffect
{
	// Token: 0x060005D3 RID: 1491 RVA: 0x00046778 File Offset: 0x00044978
	internal override void create()
	{
		base.create();
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x00046780 File Offset: 0x00044980
	internal override void prepare(Vector3 pVector, float pScale = 1f)
	{
		base.prepare(pVector, pScale);
		Vector3 localPosition = base.transform.localPosition;
		localPosition.z = localPosition.y - 2f;
		base.transform.localPosition = localPosition;
		this.used = false;
		this.world.startShake(0.1f, 0.02f, 3f, true, true);
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x000467E4 File Offset: 0x000449E4
	private void Update()
	{
		if (this.spriteAnimation.currentFrameIndex >= 32 && !this.used)
		{
			this.world.startShake(0.2f, 0.01f, 3f, false, true);
			this.used = true;
			Vector3 localPosition = base.transform.localPosition;
			localPosition.y += 2f;
			BaseEffect baseEffect = this.world.stackEffects.get("boulderImpact").spawnAt(localPosition, base.transform.localScale.x);
			if (baseEffect != null)
			{
				baseEffect.autoYZ = false;
				localPosition = baseEffect.transform.localPosition;
				localPosition.z = base.transform.localPosition.z - 10f;
				baseEffect.transform.localPosition = localPosition;
			}
			((ExplosionFlash)this.world.stackEffects.get("explosionWave").spawnNew()).startFlash(localPosition, 5, 1f);
			this.doAction();
		}
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x000468F0 File Offset: 0x00044AF0
	private void doAction()
	{
		int num = 0;
		int num2 = 0;
		foreach (Actor actor in this.world.units)
		{
			if (actor.data.alive && !actor.data.favorite && !actor.stats.ignoredByInfinityCoin)
			{
				num++;
			}
		}
		if (num % 2 == 0)
		{
			num2 = num / 2;
		}
		else
		{
			num2 = num / 2 + 1;
		}
		foreach (City city in this.world.citiesList)
		{
			city.killHalfPopPoints();
		}
		EffectInfinityCoin.temp_list.Clear();
		EffectInfinityCoin.temp_list.AddRange(this.world.units);
		EffectInfinityCoin.temp_list.Shuffle<Actor>();
		int num3 = 0;
		foreach (Actor actor2 in EffectInfinityCoin.temp_list)
		{
			if (num2 == 0)
			{
				break;
			}
			if (actor2.data.alive && !actor2.data.favorite && !actor2.stats.ignoredByInfinityCoin)
			{
				num3++;
				num2--;
				actor2.getHit((float)(actor2.data.health * 1000 + 1), true, AttackType.Other, null, false);
			}
		}
		WorldTip.showNow(LocalizedTextManager.getText("Infinity Coin Used", null) + " " + num3.ToString(), false, "top", 3f);
	}

	// Token: 0x040007C0 RID: 1984
	public static List<Actor> temp_list = new List<Actor>();

	// Token: 0x040007C1 RID: 1985
	private bool used;
}
