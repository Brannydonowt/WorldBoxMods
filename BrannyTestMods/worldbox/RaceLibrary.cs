using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A2 RID: 162
public class RaceLibrary : AssetLibrary<Race>
{
	// Token: 0x0600034F RID: 847 RVA: 0x00035A34 File Offset: 0x00033C34
	public override void init()
	{
		base.init();
		Race race = new Race();
		race.id = "nature";
		race.nature = true;
		Race pAsset = race;
		this.t = race;
		this.add(pAsset);
		this.add(this.t = new Race
		{
			id = "human",
			color = Toolbox.makeColor("#00C0FF"),
			color_abandoned = Toolbox.makeColor("#697382"),
			icon = "iconHumans",
			civilization = true,
			nameLocale = "Humans",
			bannerId = "human",
			production = new string[]
			{
				"metals",
				"bread",
				"pie"
			},
			civ_baseCities = 3,
			civ_baseArmy = 10,
			civ_baseZones = 5,
			name_template_city = "human_city",
			name_template_kingdom = "human_kingdom",
			name_template_culture = "human_culture"
		});
		this.t.culture_knowledge_gain_per_intelligence = 2.5f;
		this.t.culture_rate_tech_limit = 6;
		this.t.stats.culture_spread_speed.value = 5f;
		this.t.stats.culture_spread_convert_chance.value = 0.1f;
		this.t.skin_citizen_male = List.Of<string>(new string[]
		{
			"unit_male_1",
			"unit_male_2",
			"unit_male_3",
			"unit_male_4",
			"unit_male_5"
		});
		this.t.skin_citizen_female = List.Of<string>(new string[]
		{
			"unit_female_1",
			"unit_female_2",
			"unit_female_3",
			"unit_female_4",
			"unit_female_5"
		});
		this.t.skin_warrior = List.Of<string>(new string[]
		{
			"unit_warrior_1",
			"unit_warrior_2",
			"unit_warrior_3",
			"unit_warrior_4",
			"unit_warrior_5"
		});
		Race t = this.t;
		List<string> list = new List<string>();
		list.Add("cultures/culture_element_0");
		list.Add("cultures/culture_element_1");
		list.Add("cultures/culture_element_2");
		list.Add("cultures/culture_element_3");
		list.Add("cultures/culture_element_4");
		list.Add("cultures/culture_element_5");
		list.Add("cultures/culture_element_6");
		list.Add("cultures/culture_element_7");
		t.culture_elements = list;
		Race t2 = this.t;
		List<string> list2 = new List<string>();
		list2.Add("cultures/culture_decor_0");
		list2.Add("cultures/culture_decor_1");
		list2.Add("cultures/culture_decor_2");
		list2.Add("cultures/culture_decor_3");
		list2.Add("cultures/culture_decor_4");
		list2.Add("cultures/culture_decor_5");
		list2.Add("cultures/culture_decor_6");
		list2.Add("cultures/culture_decor_7");
		list2.Add("cultures/culture_decor_8");
		t2.culture_decors = list2;
		this.t.culture_colors = List.Of<string>(new string[]
		{
			"#FF695B",
			"#596CFF",
			"#AD3AFF",
			"#FF4FC4",
			"#FF423F",
			"#A6FF47",
			"#2BFFA3",
			"#28F7FF",
			"#66CEFF",
			"#1D5B44",
			"#594555",
			"#2B313D"
		});
		this.setPreferredStatPool("diplomacy#5,warfare#5,stewardship#5,intelligence#5");
		this.setPreferredFoodPool("berries#5,bread#5,fish#5,meat#2,sushi#2,jam#1,cider#1,ale#2,burger#1,pie#1,tea#2");
		this.addPreferredWeapon("stick", 5);
		this.addPreferredWeapon("sword", 5);
		this.addPreferredWeapon("axe", 2);
		this.addPreferredWeapon("spear", 2);
		this.addPreferredWeapon("bow", 5);
		this.clone("orc", "human");
		this.t.culture_rate_tech_limit = 3;
		this.t.culture_knowledge_gain_per_intelligence = 0.9f;
		this.t.civ_baseCities = 2;
		this.t.civ_baseArmy = 15;
		this.t.civ_baseZones = 7;
		this.t.color = Toolbox.makeColor("#DD2D2D");
		this.t.color_abandoned = Toolbox.makeColor("#8E7C62");
		this.t.icon = "iconOrcs";
		this.t.nameLocale = "Orcs";
		this.t.bannerId = "orc";
		this.t.hateRaces = "human,elf,dwarf";
		this.t.preferred_weapons.Clear();
		this.t.production = new string[]
		{
			"metals",
			"bread",
			"burger",
			"tea"
		};
		this.t.name_template_city = "orc_city";
		this.t.name_template_kingdom = "orc_kingdom";
		this.t.name_template_culture = "orc_culture";
		this.t.culture_forbidden_tech.Add("building_roads");
		this.setPreferredStatPool("diplomacy#2,warfare#5,stewardship#3,intelligence#2");
		this.setPreferredFoodPool("berries#2,bread#5,fish#5,meat#10,sushi#2,cider#1,ale#5,burger#5,pie#1,tea#10");
		this.addPreferredWeapon("stick", 5);
		this.addPreferredWeapon("axe", 10);
		this.addPreferredWeapon("spear", 3);
		this.addPreferredWeapon("bow", 5);
		this.clone("elf", "human");
		this.t.culture_rate_tech_limit = 5;
		this.t.culture_knowledge_gain_per_intelligence = 1.5f;
		this.t.civ_baseCities = 1;
		this.t.civ_baseArmy = 8;
		this.t.civ_baseZones = 12;
		this.t.color = Toolbox.makeColor("#94E032");
		this.t.color_abandoned = Toolbox.makeColor("#556E4F");
		this.t.icon = "iconElves";
		this.t.nameLocale = "Elves";
		this.t.bannerId = "elf";
		this.t.hateRaces = "orc,dwarf";
		this.t.preferred_weapons.Clear();
		this.t.production = new string[]
		{
			"metals",
			"bread",
			"jam",
			"sushi",
			"cider"
		};
		this.t.name_template_city = "elf_city";
		this.t.name_template_kingdom = "elf_kingdom";
		this.t.name_template_culture = "elf_culture";
		this.t.culture_forbidden_tech.Add("building_roads");
		this.setPreferredStatPool("diplomacy#5,warfare#2,stewardship#2,intelligence#6");
		this.setPreferredFoodPool("berries#5,bread#5,fish#1,sushi#10,cider#10,tea#1");
		this.addPreferredWeapon("stick", 5);
		this.addPreferredWeapon("spear", 5);
		this.addPreferredWeapon("bow", 10);
		this.addPreferredWeapon("sword", 2);
		this.clone("dwarf", "human");
		this.t.culture_rate_tech_limit = 5;
		this.t.culture_knowledge_gain_per_intelligence = 1.7f;
		this.t.civ_baseCities = 1;
		this.t.civ_baseArmy = 12;
		this.t.civ_baseZones = 3;
		this.t.color = Toolbox.makeColor("#BC00B6");
		this.t.color_abandoned = Toolbox.makeColor("#7F585A");
		this.t.icon = "iconDwarf";
		this.t.nameLocale = "Dwarves";
		this.t.bannerId = "dwarf";
		this.t.hateRaces = "orc,elf";
		this.t.preferred_weapons.Clear();
		this.t.buildingPlacements = BuildingPlacements.Center;
		this.t.production = new string[]
		{
			"metals",
			"bread",
			"ale"
		};
		this.t.name_template_city = "dwarf_city";
		this.t.name_template_kingdom = "dwarf_kingdom";
		this.t.name_template_culture = "dwarf_culture";
		this.setPreferredStatPool("diplomacy#2,warfare#3,stewardship#5,intelligence#2");
		this.setPreferredFoodPool("berries#1,bread#5,fish#5,meat#5,sushi#1,jam#1,cider#1,ale#10,burger#2,pie#2,tea#1");
		this.addPreferredWeapon("stick", 5);
		this.addPreferredWeapon("hammer", 10);
		this.addPreferredWeapon("bow", 3);
		this.addPreferredWeapon("sword", 2);
		foreach (Race race2 in this.list)
		{
			race2.colorBorder = new Color(race2.color.r, race2.color.g, race2.color.b, 0.6f);
			race2.colorBorderOut = new Color(race2.color.r, race2.color.g, race2.color.b, 1f);
		}
	}

	// Token: 0x06000350 RID: 848 RVA: 0x00036318 File Offset: 0x00034518
	private void setPreferredStatPool(string pString)
	{
		pString = pString.Replace(" ", string.Empty);
		string[] array = pString.Split(new char[]
		{
			','
		});
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[]
			{
				'#'
			});
			int num = int.Parse(array2[1]);
			string text = array2[0];
			for (int j = 0; j < num; j++)
			{
				this.t.preferred_attribute.Add(text);
			}
		}
	}

	// Token: 0x06000351 RID: 849 RVA: 0x00036398 File Offset: 0x00034598
	private void setPreferredFoodPool(string pString)
	{
		pString = pString.Replace(" ", string.Empty);
		string[] array = pString.Split(new char[]
		{
			','
		});
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[]
			{
				'#'
			});
			int num = int.Parse(array2[1]);
			string text = array2[0];
			for (int j = 0; j < num; j++)
			{
				this.t.preferred_food.Add(text);
			}
		}
	}

	// Token: 0x06000352 RID: 850 RVA: 0x00036418 File Offset: 0x00034618
	public override Race get(string pID)
	{
		if (!this.dict.ContainsKey(pID))
		{
			Race race = new Race();
			race.id = pID;
			Race pAsset = race;
			this.t = race;
			this.add(pAsset);
		}
		return base.get(pID);
	}

	// Token: 0x06000353 RID: 851 RVA: 0x00036458 File Offset: 0x00034658
	private void addPreferredWeapon(string pID, int pAmount)
	{
		for (int i = 0; i < pAmount; i++)
		{
			this.t.preferred_weapons.Add(pID);
		}
	}

	// Token: 0x06000354 RID: 852 RVA: 0x00036484 File Offset: 0x00034684
	public void update()
	{
		for (int i = 0; i < this.list.Count; i++)
		{
			this.list[i].units.checkAddRemove();
		}
	}

	// Token: 0x06000355 RID: 853 RVA: 0x000364C0 File Offset: 0x000346C0
	public void clear()
	{
		foreach (Race race in this.list)
		{
			race.units.Clear();
		}
	}
}
