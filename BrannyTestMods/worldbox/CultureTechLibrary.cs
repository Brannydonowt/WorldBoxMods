using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000024 RID: 36
public class CultureTechLibrary : AssetLibrary<CultureTechAsset>
{
	// Token: 0x060000EA RID: 234 RVA: 0x00012CAC File Offset: 0x00010EAC
	public override void init()
	{
		base.init();
		this.addCulture();
		this.addLegendary();
		this.addEpic();
		this.addCiv();
		this.addWar();
		this.addBuildings();
		this.addProduction();
		this.addTrading();
		this.addTechBoats();
		this.addTechItems();
		this.addTechMaterials();
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00012D04 File Offset: 0x00010F04
	private void logTechNames()
	{
		string text = "";
		foreach (CultureTechAsset cultureTechAsset in this.list)
		{
			text += cultureTechAsset.id;
			text += "\n";
		}
		Debug.Log(text);
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00012D78 File Offset: 0x00010F78
	private void addCulture()
	{
		this.add(new CultureTechAsset
		{
			id = "culture_convert_chance_1",
			icon = "tech/icon_tech_culture_convert_chance_1"
		});
		this.t.stats.culture_spread_convert_chance.add(0.05f);
		this.add(new CultureTechAsset
		{
			id = "culture_convert_chance_2",
			icon = "tech/icon_tech_culture_convert_chance_2"
		});
		this.addRequirement("culture_convert_chance_1");
		this.t.stats.culture_spread_convert_chance.add(0.05f);
		this.t.required_level = 10;
		this.add(new CultureTechAsset
		{
			id = "culture_convert_chance_3",
			icon = "tech/icon_tech_culture_convert_chance_3"
		});
		this.addRequirement("culture_convert_chance_2");
		this.t.stats.culture_spread_convert_chance.add(0.05f);
		this.t.required_level = 20;
		this.add(new CultureTechAsset
		{
			id = "culture_spread_speed_1",
			icon = "tech/icon_tech_culture_spread_speed_1"
		});
		this.t.stats.culture_spread_speed.add(-0.5f);
		this.add(new CultureTechAsset
		{
			id = "culture_spread_speed_2",
			icon = "tech/icon_tech_culture_spread_speed_2"
		});
		this.addRequirement("culture_spread_speed_1");
		this.t.stats.culture_spread_speed.add(-0.5f);
		this.t.required_level = 10;
		this.add(new CultureTechAsset
		{
			id = "culture_spread_speed_3",
			icon = "tech/icon_tech_culture_spread_speed_3"
		});
		this.addRequirement("culture_spread_speed_2");
		this.t.stats.culture_spread_speed.add(-0.5f);
		this.t.required_level = 20;
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00012F4D File Offset: 0x0001114D
	private void addLegendary()
	{
		this.add(new CultureTechAsset
		{
			id = "nature_lovers",
			icon = "tech/icon_tech_nature_lovers",
			type = TechType.Rare,
			required_level = 20,
			enabled = false
		});
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00012F88 File Offset: 0x00011188
	private void addEpic()
	{
		this.add(new CultureTechAsset
		{
			id = "ancestors_knowledge",
			icon = "tech/icon_tech_ancestors_knowledge",
			type = TechType.Rare,
			required_level = 20
		});
		this.t.stats.bonus_born_level.value = 1f;
		this.add(new CultureTechAsset
		{
			id = "way_of_live",
			icon = "tech/icon_tech_way_of_life",
			type = TechType.Rare,
			required_level = 20
		});
		this.add(new CultureTechAsset
		{
			id = "heroes",
			icon = "tech/icon_tech_heroes",
			type = TechType.Rare,
			required_level = 20
		});
		this.t.stats.bonus_max_unit_level.add(2f);
		this.add(new CultureTechAsset
		{
			id = "steal_resources",
			icon = "tech/icon_tech_steal_resources",
			type = TechType.Rare,
			required_level = 20,
			enabled = false
		});
		this.add(new CultureTechAsset
		{
			id = "steal_kids",
			icon = "tech/icon_tech_steal_kids",
			type = TechType.Rare,
			required_level = 20,
			enabled = false
		});
		this.add(new CultureTechAsset
		{
			id = "gems_water",
			icon = "tech/icon_tech_gems_water",
			type = TechType.Rare,
			required_level = 20,
			enabled = false
		});
	}

	// Token: 0x060000EF RID: 239 RVA: 0x00013104 File Offset: 0x00011304
	private void addCiv()
	{
		this.add(new CultureTechAsset
		{
			id = "housing_1",
			icon = "tech/icon_tech_housing_1",
			required_level = 5
		});
		this.addRequirement("house_tier_2");
		this.t.stats.housing.add(1f);
		this.add(new CultureTechAsset
		{
			id = "housing_2",
			icon = "tech/icon_tech_housing_2",
			required_level = 10
		});
		this.addRequirement("house_tier_4");
		this.addRequirement("housing_1");
		this.t.stats.housing.add(1f);
		this.add(new CultureTechAsset
		{
			id = "housing_3",
			icon = "tech/icon_tech_housing_3",
			required_level = 15
		});
		this.addRequirement("house_tier_5");
		this.addRequirement("housing_2");
		this.t.stats.housing.add(1f);
		this.add(new CultureTechAsset
		{
			id = "governance_1",
			icon = "tech/icon_tech_governance_1",
			required_level = 10
		});
		this.t.stats.bonus_max_cities.add(1f);
		this.add(new CultureTechAsset
		{
			id = "governance_2",
			icon = "tech/icon_tech_governance_2"
		});
		this.addRequirement("governance_1");
		this.t.stats.bonus_max_cities.add(1f);
		this.add(new CultureTechAsset
		{
			id = "governance_3",
			icon = "tech/icon_tech_governance_3",
			type = TechType.Rare,
			required_level = 20
		});
		this.addRequirement("governance_2");
		this.t.stats.bonus_max_cities.add(1f);
		this.add(new CultureTechAsset
		{
			id = "knowledge_gain_1",
			icon = "tech/icon_tech_knowledge_gain1",
			required_level = 5
		});
		this.t.stats.knowledge_gain.add(0.5f);
		this.add(new CultureTechAsset
		{
			id = "knowledge_gain_2",
			icon = "tech/icon_tech_knowledge_gain2",
			required_level = 10
		});
		this.addRequirement("knowledge_gain_1");
		this.t.stats.knowledge_gain.add(0.5f);
		this.add(new CultureTechAsset
		{
			id = "knowledge_gain_3",
			icon = "tech/icon_tech_knowledge_gain3",
			required_level = 15
		});
		this.addRequirement("knowledge_gain_2");
		this.t.stats.knowledge_gain.add(0.5f);
		this.add(new CultureTechAsset
		{
			id = "army_training_1",
			icon = "tech/icon_tech_army_training_1",
			required_level = 5
		});
		this.addRequirement("building_barracks");
		this.t.stats.bonus_max_army.add(5f);
		this.add(new CultureTechAsset
		{
			id = "army_training_2",
			icon = "tech/icon_tech_army_training_2",
			required_level = 5
		});
		this.addRequirement("army_training_1");
		this.t.stats.bonus_max_army.add(5f);
		this.add(new CultureTechAsset
		{
			id = "army_training_3",
			icon = "tech/icon_tech_army_training_3",
			required_level = 5
		});
		this.addRequirement("army_training_2");
		this.t.stats.bonus_max_army.add(5f);
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x000134C0 File Offset: 0x000116C0
	private void addWar()
	{
		this.add(new CultureTechAsset
		{
			id = "military_strategy",
			icon = "tech/icon_tech_military_strategy",
			required_level = 20,
			type = TechType.Rare
		});
		this.addRequirement("weapon_production");
		this.addRequirement("armor_production");
		this.t.stats.bonus_damage.add(0.05f);
		this.add(new CultureTechAsset
		{
			id = "defense_strategy",
			icon = "tech/icon_tech_defense_strategy",
			required_level = 20,
			type = TechType.Rare
		});
		this.addRequirement("armor_production");
		this.t.stats.bonus_armor.add(0.05f);
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00013584 File Offset: 0x00011784
	private void addBuildings()
	{
		this.add(new CultureTechAsset
		{
			id = "house_tier_0",
			icon = "tech/icon_tech_house_tier_0",
			priority = true
		});
		this.add(new CultureTechAsset
		{
			id = "house_tier_1",
			icon = "tech/icon_tech_house_tier_1",
			required_level = 5,
			priority = true
		});
		this.addRequirement("house_tier_0");
		this.add(new CultureTechAsset
		{
			id = "house_tier_2",
			icon = "tech/icon_tech_house_tier_2",
			required_level = 8,
			priority = true
		});
		this.addRequirement("house_tier_1");
		this.add(new CultureTechAsset
		{
			id = "house_tier_3",
			icon = "tech/icon_tech_house_tier_3",
			required_level = 12
		});
		this.addRequirement("house_tier_2");
		this.add(new CultureTechAsset
		{
			id = "house_tier_4",
			icon = "tech/icon_tech_house_tier_4",
			required_level = 15
		});
		this.addRequirement("house_tier_3");
		this.add(new CultureTechAsset
		{
			id = "house_tier_5",
			icon = "tech/icon_tech_house_tier_5",
			required_level = 20
		});
		this.addRequirement("house_tier_4");
		this.add(new CultureTechAsset
		{
			id = "building_docks",
			icon = "tech/icon_tech_building_docks",
			priority = true
		});
		this.addRequirement("house_tier_2");
		this.add(new CultureTechAsset
		{
			id = "building_roads",
			icon = "tech/icon_tech_building_roads",
			required_level = 5
		});
		this.add(new CultureTechAsset
		{
			id = "building_well",
			icon = "tech/icon_tech_well",
			required_level = 12
		});
		this.addRequirement("house_tier_2");
		this.add(new CultureTechAsset
		{
			id = "building_watch_tower",
			icon = "tech/icon_tech_building_watch_tower",
			required_level = 5
		});
		this.addRequirement("house_tier_3");
		this.addRequirement("weapon_bow");
		this.add(new CultureTechAsset
		{
			id = "building_watch_tower_bonus",
			icon = "tech/icon_tech_watch_tower_bonus",
			required_level = 20,
			type = TechType.Rare
		});
		this.t.stats.bonus_watch_towers.value = 1f;
		this.addRequirement("building_watch_tower");
		this.add(new CultureTechAsset
		{
			id = "building_statues",
			icon = "tech/icon_tech_building_statues",
			required_level = 5
		});
		this.addRequirement("house_tier_3");
		this.add(new CultureTechAsset
		{
			id = "building_mine",
			icon = "tech/icon_tech_building_mine",
			required_level = 5
		});
		this.addRequirement("house_tier_1");
		this.add(new CultureTechAsset
		{
			id = "building_barracks",
			icon = "tech/icon_tech_barracks",
			required_level = 5
		});
		this.addRequirement("house_tier_2");
		this.add(new CultureTechAsset
		{
			id = "building_windmill",
			icon = "tech/icon_tech_windmill",
			required_level = 5
		});
		this.addRequirement("house_tier_2");
		this.add(new CultureTechAsset
		{
			id = "building_temple",
			icon = "tech/icon_tech_temple",
			required_level = 10
		});
		this.addRequirement("house_tier_3");
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x000138FC File Offset: 0x00011AFC
	private void addProduction()
	{
		this.add(new CultureTechAsset
		{
			id = "mining_efficiency",
			icon = "tech/icon_tech_mining_efficiency",
			required_level = 3
		});
		this.t.stats.bonus_res_chance_ores.add(0.2f);
		this.t.stats.bonus_res_ore_amount.add(1f);
		this.add(new CultureTechAsset
		{
			id = "sharp_axes",
			icon = "tech/icon_tech_sharp_axes",
			required_level = 2
		});
		this.t.stats.bonus_res_chance_wood.add(0.2f);
		this.t.stats.bonus_res_wood_amount.add(1f);
		this.add(new CultureTechAsset
		{
			id = "weaponsmith",
			icon = "tech/icon_tech_weaponsmith",
			required_level = 20,
			type = TechType.Rare
		});
		this.addRequirement("weapon_production");
		this.t.stats.item_production_tries_weapons.add(2f);
		this.add(new CultureTechAsset
		{
			id = "armorsmith",
			icon = "tech/icon_tech_armorsmith",
			required_level = 20,
			type = TechType.Rare
		});
		this.addRequirement("armor_production");
		this.t.stats.item_production_tries_armor.add(2f);
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00013A70 File Offset: 0x00011C70
	private void addTrading()
	{
		this.add(new CultureTechAsset
		{
			id = "trading",
			icon = "tech/icon_tech_trading",
			required_level = 7
		});
		this.add(new CultureTechAsset
		{
			id = "trading_efficiency",
			icon = "tech/icon_tech_trading-_efficiency",
			required_level = 8
		});
		this.addRequirement("trading");
		this.t.stats.mod_trading_bonus.add(0.15f);
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x00013AF4 File Offset: 0x00011CF4
	private void addTechBoats()
	{
		this.add(new CultureTechAsset
		{
			id = "boats_trading",
			icon = "tech/icon_tech_boats_trading",
			required_level = 4
		});
		this.addRequirement("building_docks");
		this.add(new CultureTechAsset
		{
			id = "boats_transport",
			icon = "tech/icon_tech_boats_transport",
			required_level = 8,
			priority = true
		});
		this.addRequirement("building_docks");
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x00013B70 File Offset: 0x00011D70
	private void addTechItems()
	{
		this.add(new CultureTechAsset
		{
			id = "weapon_sword",
			icon = "tech/icon_tech_weapon_sword",
			required_level = 3,
			priority = true
		});
		this.addRequirement("weapon_production");
		this.add(new CultureTechAsset
		{
			id = "weapon_axe",
			icon = "tech/icon_tech_weapon_axe",
			required_level = 3
		});
		this.addRequirement("weapon_production");
		this.add(new CultureTechAsset
		{
			id = "weapon_hammer",
			icon = "tech/icon_tech_weapon_hammer",
			required_level = 3
		});
		this.addRequirement("weapon_production");
		this.add(new CultureTechAsset
		{
			id = "weapon_spear",
			icon = "tech/icon_tech_weapon_spear",
			required_level = 3
		});
		this.addRequirement("weapon_production");
		this.add(new CultureTechAsset
		{
			id = "armor_production",
			icon = "tech/icon_tech_armor_production",
			required_level = 3
		});
		this.addRequirement("weapon_production");
		this.add(new CultureTechAsset
		{
			id = "weapon_production",
			icon = "tech/icon_tech_weapon_production",
			required_level = 3,
			priority = true
		});
		this.add(new CultureTechAsset
		{
			id = "weapon_bow",
			icon = "tech/icon_tech_weapon_bow",
			required_level = 3
		});
		this.addRequirement("weapon_production");
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00013CEC File Offset: 0x00011EEC
	private void addTechMaterials()
	{
		this.add(new CultureTechAsset
		{
			id = "material_copper",
			icon = "tech/icon_tech_material_copper",
			required_level = 4
		});
		this.add(new CultureTechAsset
		{
			id = "material_bronze",
			icon = "tech/icon_tech_material_bronze",
			required_level = 6
		});
		this.addRequirement("material_copper");
		this.add(new CultureTechAsset
		{
			id = "material_silver",
			icon = "tech/icon_tech_material_silver",
			required_level = 10
		});
		this.addRequirement("material_bronze");
		this.add(new CultureTechAsset
		{
			id = "material_iron",
			icon = "tech/icon_tech_material_iron",
			required_level = 20
		});
		this.addRequirement("material_silver");
		this.add(new CultureTechAsset
		{
			id = "material_steel",
			icon = "tech/icon_tech_material_steel",
			required_level = 30
		});
		this.addRequirement("material_iron");
		this.add(new CultureTechAsset
		{
			id = "material_mythril",
			icon = "tech/icon_tech_material_mythril",
			required_level = 40
		});
		this.addRequirement("material_steel");
		this.add(new CultureTechAsset
		{
			id = "material_adamantine",
			icon = "tech/icon_tech_material_adamantine",
			required_level = 50
		});
		this.addRequirement("material_mythril");
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00013E5F File Offset: 0x0001205F
	private void addRequirement(string pRequirement)
	{
		if (this.t.requirements == null)
		{
			this.t.requirements = new List<string>();
		}
		this.t.requirements.Add(pRequirement);
	}
}
