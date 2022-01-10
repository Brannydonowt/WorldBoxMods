using System;
using System.Collections.Generic;
using Beebyte.Obfuscator;

// Token: 0x02000069 RID: 105
[ObfuscateLiterals]
public class ItemLibrary : AssetLibrary<ItemAsset>
{
	// Token: 0x0600022B RID: 555 RVA: 0x00027E00 File Offset: 0x00026000
	public override void init()
	{
		base.init();
		ItemAsset itemAsset = new ItemAsset();
		itemAsset.id = "_equipment";
		ItemAsset pAsset = itemAsset;
		this.t = itemAsset;
		this.add(pAsset);
		this.t.pool = "equipment";
		this.t.materials = List.Of<string>(new string[]
		{
			"leather",
			"copper",
			"bronze",
			"silver",
			"iron",
			"steel",
			"mythril",
			"adamantine"
		});
		this.clone("armor", "_equipment");
		this.clone("boots", "_equipment");
		this.clone("helmet", "_equipment");
		ItemAsset itemAsset2 = new ItemAsset();
		itemAsset2.id = "_accessory";
		pAsset = itemAsset2;
		this.t = itemAsset2;
		this.add(pAsset);
		this.t.pool = "equipment";
		this.t.materials = List.Of<string>(new string[]
		{
			"bone",
			"copper",
			"bronze",
			"silver",
			"iron",
			"steel",
			"mythril",
			"adamantine"
		});
		this.clone("ring", "_accessory");
		this.clone("amulet", "_accessory");
		this.clone("_weapon", "_equipment");
		this.t.slash = "base";
		this.clone("_melee", "_weapon");
		this.t.pool = "melee";
		this.clone("_range", "_weapon");
		this.t.pool = "range";
		this.t.attackType = WeaponType.Range;
		this.t.baseStats.projectiles = 1;
		this.clone("base", "_melee");
		this.t.slash = "base";
		this.clone("hands", "_melee");
		this.t.slash = "punch";
		this.clone("jaws", "_melee");
		this.t.slash = "jaws";
		this.clone("claws", "_melee");
		this.t.slash = "claws";
		this.clone("stick", "_melee");
		this.t.slash = "punch";
		this.t.suffixes = List.Of<string>(new string[]
		{
			"0"
		});
		this.t.materials = List.Of<string>(new string[]
		{
			"wood"
		});
		this.t.baseStats.damage = 1;
		this.t.baseStats.attackSpeed = 20f;
		this.t.baseStats.armor = 1;
		this.clone("snowball", "_range");
		this.t.slash = "punch";
		this.t.baseStats.range = 22f;
		this.t.baseStats.crit = 10f;
		this.t.baseStats.damageCritMod = 0.5f;
		this.t.projectile = "snowball";
		this.t.baseStats.projectiles = 1;
		ItemAsset t = this.t;
		t.attackAction = (WorldAction)Delegate.Combine(t.attackAction, new WorldAction(ActionLibrary.addFrozenEffectOnTarget20));
		this.clone("bow", "_range");
		this.t.tech_needed = "weapon_bow";
		this.t.projectile = "arrow";
		this.t.slash = "bow";
		this.t.materials = List.Of<string>(new string[]
		{
			"wood",
			"copper",
			"bronze",
			"silver",
			"iron",
			"steel",
			"mythril",
			"adamantine"
		});
		this.t.baseStats.range = 22f;
		this.t.baseStats.crit = 10f;
		this.t.baseStats.damageCritMod = 0.5f;
		this.clone("flame_sword", "_weapon");
		this.t.materials = List.Of<string>(new string[]
		{
			"base"
		});
		this.t.suffixes = List.Of<string>(new string[]
		{
			"0",
			"power",
			"doom",
			"terror"
		});
		this.t.slash = "base";
		this.t.baseStats.damage = 30;
		this.t.baseStats.attackSpeed = 120f;
		this.t.baseStats.targets = 2;
		this.t.baseStats.damageCritMod = 0.1f;
		this.t.equipment_value = 50;
		ItemAsset t2 = this.t;
		t2.attackAction = (WorldAction)Delegate.Combine(t2.attackAction, new WorldAction(ActionLibrary.addBurningEffectOnTarget));
		this.clone("necromancer_staff", "_range");
		this.t.materials = List.Of<string>(new string[]
		{
			"base"
		});
		this.t.suffixes = List.Of<string>(new string[]
		{
			"0",
			"power",
			"doom",
			"terror"
		});
		this.t.projectile = "bone";
		this.t.slash = "punch";
		this.t.baseStats.attackSpeed = 20f;
		this.t.baseStats.range = 22f;
		this.t.baseStats.crit = 10f;
		this.t.baseStats.targets = 1;
		this.t.baseStats.damageCritMod = 0.5f;
		this.t.equipment_value = 50;
		this.t.baseStats.damage = 30;
		this.clone("alien_blaster", "_range");
		this.t.materials = List.Of<string>(new string[]
		{
			"base"
		});
		this.t.suffixes = List.Of<string>(new string[]
		{
			"0"
		});
		this.t.projectile = "plasma_ball";
		this.t.slash = "punch";
		this.t.baseStats.attackSpeed = 20f;
		this.t.baseStats.range = 22f;
		this.t.baseStats.crit = 10f;
		this.t.baseStats.targets = 1;
		this.t.baseStats.damageCritMod = 0.5f;
		this.t.equipment_value = 50;
		this.t.baseStats.damage = 30;
		this.clone("evil_staff", "_range");
		this.t.materials = List.Of<string>(new string[]
		{
			"base"
		});
		this.t.suffixes = List.Of<string>(new string[]
		{
			"0",
			"power",
			"doom",
			"terror"
		});
		this.t.projectile = "red_orb";
		this.t.slash = "punch";
		this.t.baseStats.attackSpeed = 20f;
		this.t.baseStats.range = 22f;
		this.t.baseStats.crit = 10f;
		this.t.baseStats.targets = 1;
		this.t.baseStats.damageCritMod = 0.5f;
		this.t.equipment_value = 50;
		this.t.baseStats.projectiles = 20;
		this.t.baseStats.damage = 10;
		this.clone("white_staff", "_range");
		this.t.materials = List.Of<string>(new string[]
		{
			"base"
		});
		this.t.suffixes = List.Of<string>(new string[]
		{
			"0",
			"power",
			"doom",
			"terror"
		});
		this.t.projectile = "freeze_orb";
		this.t.slash = "punch";
		this.t.baseStats.attackSpeed = 20f;
		this.t.baseStats.range = 22f;
		this.t.baseStats.crit = 10f;
		this.t.baseStats.targets = 1;
		this.t.baseStats.damageCritMod = 0.5f;
		this.t.baseStats.damage = 35;
		this.t.equipment_value = 50;
		ItemAsset t3 = this.t;
		t3.attackAction = (WorldAction)Delegate.Combine(t3.attackAction, new WorldAction(ActionLibrary.addFrozenEffectOnTarget20));
		this.clone("plague_doctor_staff", "_weapon");
		this.t.materials = List.Of<string>(new string[]
		{
			"base"
		});
		this.t.suffixes = List.Of<string>(new string[]
		{
			"0",
			"power",
			"health",
			"protection",
			"justice"
		});
		this.t.slash = "punch";
		this.t.baseStats.attackSpeed = 20f;
		this.t.baseStats.crit = 10f;
		this.t.baseStats.targets = 3;
		this.t.baseStats.damageCritMod = 0.5f;
		this.t.baseStats.damage = 35;
		this.t.equipment_value = 15;
		this.clone("druid_staff", "_range");
		this.t.materials = List.Of<string>(new string[]
		{
			"base"
		});
		this.t.suffixes = List.Of<string>(new string[]
		{
			"0",
			"power",
			"doom",
			"terror"
		});
		this.t.projectile = "green_orb";
		this.t.slash = "punch";
		this.t.baseStats.attackSpeed = 35f;
		this.t.baseStats.range = 22f;
		this.t.baseStats.crit = 10f;
		this.t.baseStats.targets = 1;
		this.t.baseStats.damageCritMod = 0.3f;
		this.t.baseStats.damage = 12;
		this.t.equipment_value = 50;
		this.t.baseStats.projectiles = 2;
		ItemAsset t4 = this.t;
		t4.attackAction = (WorldAction)Delegate.Combine(t4.attackAction, new WorldAction(ActionLibrary.addSlowEffectOnTarget20));
		this.clone("sword", "_melee");
		this.t.tech_needed = "weapon_sword";
		this.t.slash = "sword";
		this.t.materials = List.Of<string>(new string[]
		{
			"wood",
			"stone",
			"copper",
			"bronze",
			"silver",
			"iron",
			"steel",
			"mythril",
			"adamantine"
		});
		this.t.baseStats.damage = 1;
		this.t.baseStats.attackSpeed = 40f;
		this.clone("axe", "_melee");
		this.t.tech_needed = "weapon_axe";
		this.t.slash = "axe";
		this.t.materials = List.Of<string>(new string[]
		{
			"wood",
			"stone",
			"copper",
			"bronze",
			"silver",
			"iron",
			"steel",
			"mythril",
			"adamantine"
		});
		this.t.baseStats.attackSpeed = 20f;
		this.clone("hammer", "_melee");
		this.t.tech_needed = "weapon_hammer";
		this.t.slash = "hammer";
		this.t.materials = List.Of<string>(new string[]
		{
			"wood",
			"stone",
			"copper",
			"bronze",
			"silver",
			"iron",
			"steel",
			"mythril",
			"adamantine"
		});
		this.t.baseStats.attackSpeed = -10f;
		this.t.baseStats.targets = 1;
		this.clone("spear", "_melee");
		this.t.tech_needed = "weapon_spear";
		this.t.slash = "spear";
		this.t.materials = List.Of<string>(new string[]
		{
			"wood",
			"stone",
			"copper",
			"bronze",
			"silver",
			"iron",
			"steel",
			"mythril",
			"adamantine"
		});
		this.t.baseStats.range = 1f;
		this.t.baseStats.attackSpeed = 15f;
		this.weapons_id_melee = new List<string>();
		this.weapons_id_melee.Add("sword");
		this.weapons_id_melee.Add("axe");
		this.weapons_id_melee.Add("spear");
		this.weapons_id_melee.Add("hammer");
		this.weapons_id_range = new List<string>();
		this.weapons_id_range.Add("bow");
		this.clone("venomous_bite", "_melee");
		this.t.slash = "jaws";
		this.t.attackType = WeaponType.Melee;
		ItemAsset t5 = this.t;
		t5.attackAction = (WorldAction)Delegate.Combine(t5.attackAction, new WorldAction(ActionLibrary.addPoisonedEffectOnTarget));
		this.clone("rocks", "_range");
		this.t.projectile = "rock";
		this.t.slash = "punch";
		this.t.baseStats.range = 15f;
		this.t.baseStats.crit = 10f;
		this.t.baseStats.projectiles = 1;
		this.t.baseStats.damageCritMod = 0.5f;
		this.clone("shotgun", "_range");
		this.t.materials = List.Of<string>(new string[]
		{
			"base"
		});
		this.t.suffixes = List.Of<string>(new string[]
		{
			"0"
		});
		this.t.projectile = "shotgun_bullet";
		this.t.slash = "punch";
		this.t.baseStats.projectiles = 12;
		this.t.baseStats.attackSpeed = 10f;
		this.t.baseStats.range = 22f;
		this.t.baseStats.targets = 1;
		this.t.baseStats.damage = 4;
	}

	// Token: 0x0600022C RID: 556 RVA: 0x00028EC4 File Offset: 0x000270C4
	public string getEquipmentID(EquipmentType pType)
	{
		switch (pType)
		{
		case EquipmentType.Weapon:
			return "weapon";
		case EquipmentType.Helmet:
			return "helmet";
		case EquipmentType.Armor:
			return "armor";
		case EquipmentType.Boots:
			return "boots";
		case EquipmentType.Ring:
			return "ring";
		case EquipmentType.Amulet:
			return "amulet";
		default:
			return null;
		}
	}

	// Token: 0x040002F6 RID: 758
	public List<string> weapons_id_melee;

	// Token: 0x040002F7 RID: 759
	public List<string> weapons_id_range;
}
