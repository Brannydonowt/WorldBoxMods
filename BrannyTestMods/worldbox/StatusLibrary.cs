using System;
using Beebyte.Obfuscator;
using UnityEngine;

// Token: 0x0200004B RID: 75
[ObfuscateLiterals]
public class StatusLibrary : AssetLibrary<StatusEffect>
{
	// Token: 0x060001BE RID: 446 RVA: 0x00023408 File Offset: 0x00021608
	public override void init()
	{
		base.init();
		this.add(new StatusEffect
		{
			id = "powerup",
			duration = 300f,
			animated = true
		});
		this.t.baseStats.size = 3f;
		this.t.baseStats.armor = 5;
		this.t.baseStats.damage = 5;
		this.t.baseStats.attackSpeed = 5f;
		this.t.baseStats.mod_damage = 50f;
		this.t.baseStats.mod_armor = 50f;
		this.t.baseStats.mod_attackSpeed = 50f;
		this.t.baseStats.mod_crit = 50f;
		this.t.baseStats.scale = 0.1f;
		this.t.baseStats.size = 2f;
		this.add(new StatusEffect
		{
			id = "slowness",
			texture = "fx_status_slowness_t",
			duration = 30f,
			animated = true
		});
		this.t.baseStats.speed -= 100f;
		this.t.baseStats.attackSpeed = -50f;
		this.t.removeStatus.Add("caffeinated");
		this.add(new StatusEffect
		{
			id = "caffeinated",
			texture = "fx_status_caffeinated_t",
			duration = 60f,
			animated = true
		});
		this.t.baseStats.intelligence = 222;
		this.t.baseStats.speed = 200f;
		this.t.baseStats.mod_attackSpeed = 569f;
		this.t.removeStatus.Add("frozen");
		this.add(new StatusEffect
		{
			id = "frozen",
			texture = "fx_status_frozen_t",
			duration = 7f,
			random_frame = true
		});
		this.t.baseStats.knockbackReduction = 100f;
		this.t.baseStats.armor = -20;
		this.t.baseStats.speed = -10000000f;
		this.t.baseStats.attackSpeed = -10000000f;
		this.t.removeStatus.Add("burning");
		this.t.oppositeStatus.Add("shield");
		this.t.oppositeTraits.Add("freeze_proof");
		this.add(new StatusEffect
		{
			id = "shield",
			texture = "fx_status_shield_t",
			duration = 60f,
			animated = true
		});
		this.t.baseStats.knockbackReduction = 100f;
		this.t.baseStats.armor = 90;
		this.t.removeStatus.Add("burning");
		this.t.oppositeStatus.Add("frozen");
		StatusEffect t = this.t;
		t.actionOnHit = (WorldAction)Delegate.Combine(t.actionOnHit, new WorldAction(StatusLibrary.spawnShieldHitEffect));
		this.add(new StatusEffect
		{
			id = "burning",
			texture = "fx_status_burning_t",
			duration = 60f,
			action = new WorldAction(StatusLibrary.burningEffect),
			actionInterval = 2f,
			animated = true,
			animation_speed = 0.1f,
			animation_speed_random = 0.08f,
			random_frame = true,
			random_flip = true,
			cancelActorJob = true
		});
		this.t.oppositeStatus.Add("shield");
		this.t.oppositeTraits.Add("fire_proof");
		this.t.removeStatus.Add("frozen");
		this.add(new StatusEffect
		{
			id = "poisoned",
			duration = 90f,
			action = new WorldAction(StatusLibrary.poisonedEffect),
			actionInterval = 1f
		});
		this.t.oppositeTraits.Add("immune_poison");
	}

	// Token: 0x060001BF RID: 447 RVA: 0x00023890 File Offset: 0x00021A90
	public static bool burningEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (pTarget.isActor() && pTarget.a.stats.have_skin && Toolbox.randomBool())
		{
			pTarget.a.addTrait("skin_burns");
		}
		int num = (int)((float)pTarget.curStats.health * 0.1f) + 1;
		pTarget.getHit((float)num, true, AttackType.Fire, null, true);
		if (!MapBox.instance.qualityChanger.lowRes && Toolbox.randomChance(0.1f))
		{
			MapBox.instance.particlesFire.spawn(pTarget.currentPosition.x, pTarget.currentPosition.y, true);
		}
		return true;
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x00023938 File Offset: 0x00021B38
	public static bool spawnShieldHitEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		if (MapBox.instance.qualityChanger.lowRes)
		{
			return false;
		}
		BaseEffect baseEffect = MapBox.instance.stackEffects.get("fx_shield_hit").spawnAt(pTarget.currentPosition, pTarget.curStats.scale);
		if (baseEffect == null)
		{
			return false;
		}
		baseEffect.transform.SetParent(pTarget.a.transform, true);
		baseEffect.transform.localPosition = Vector3.zero;
		return true;
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x000239BC File Offset: 0x00021BBC
	public static bool poisonedEffect(BaseSimObject pTarget, WorldTile pTile = null)
	{
		int num = 1;
		if (Toolbox.randomBool() && pTarget.a.data.health > 1)
		{
			pTarget.getHit((float)num, true, AttackType.Poison, null, true);
		}
		pTarget.a.spawnParticle(Toolbox.color_infected);
		pTarget.a.startShake(0.4f, 0.2f, true, false);
		return true;
	}
}
