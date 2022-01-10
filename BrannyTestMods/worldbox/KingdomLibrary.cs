using System;

// Token: 0x02000091 RID: 145
public class KingdomLibrary : AssetLibrary<KingdomAsset>
{
	// Token: 0x06000323 RID: 803 RVA: 0x0003388C File Offset: 0x00031A8C
	public override void init()
	{
		base.init();
		this.add(new KingdomAsset
		{
			id = "human",
			civ = true
		});
		this.t.addTag("civ");
		this.t.addTag("human");
		this.t.addFriendlyTag("human");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("good");
		this.t.addEnemyTag("bandits");
		this.add(new KingdomAsset
		{
			id = "elf",
			civ = true
		});
		this.t.addTag("civ");
		this.t.addTag("elf");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("elf");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("good");
		this.t.addEnemyTag("bandits");
		this.add(new KingdomAsset
		{
			id = "dwarf",
			civ = true
		});
		this.t.addTag("civ");
		this.t.addTag("dwarf");
		this.t.addFriendlyTag("dwarf");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("good");
		this.t.addEnemyTag("bandits");
		this.add(new KingdomAsset
		{
			id = "orc",
			civ = true
		});
		this.t.addTag("orc");
		this.add(new KingdomAsset
		{
			id = "nomads_human",
			nomads = true,
			mobs = true
		});
		this.t.default_kingdom_color = new KingdomColor("#BACADD", "#BACADD", "#BACADD");
		this.t.addTag("civ");
		this.t.addTag("human");
		this.t.addFriendlyTag("human");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("good");
		this.t.addEnemyTag("bandits");
		this.add(new KingdomAsset
		{
			id = "nomads_elf",
			nomads = true,
			mobs = true
		});
		this.t.default_kingdom_color = new KingdomColor("#98DB8C", "#98DB8C", "#98DB8C");
		this.t.addTag("civ");
		this.t.addTag("elf");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("elf");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("good");
		this.t.addEnemyTag("bandits");
		this.add(new KingdomAsset
		{
			id = "nomads_orc",
			nomads = true,
			mobs = true
		});
		this.t.default_kingdom_color = new KingdomColor("#FFCD70", "#FFCD70", "#FFCD70");
		this.t.addTag("civ");
		this.t.addTag("orc");
		this.t.addFriendlyTag("orc");
		this.t.addEnemyTag("bandits");
		this.add(new KingdomAsset
		{
			id = "nomads_dwarf",
			nomads = true,
			mobs = true
		});
		this.t.default_kingdom_color = new KingdomColor("#B1A0FF", "#B1A0FF", "#B1A0FF");
		this.t.addTag("civ");
		this.t.addTag("dwarf");
		this.t.addFriendlyTag("dwarf");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("good");
		this.t.addEnemyTag("bandits");
		this.add(new KingdomAsset
		{
			id = "nature",
			nature = true,
			mobs = true
		});
		this.t.default_kingdom_color = new KingdomColor("#888888", "#888888", "#888888");
		this.add(new KingdomAsset
		{
			id = "ruins",
			nature = true,
			mobs = true
		});
		this.t.default_kingdom_color = new KingdomColor("#444444", "#444444", "#444444");
		this.add(new KingdomAsset
		{
			id = "abandoned",
			nature = true,
			mobs = true
		});
		this.t.default_kingdom_color = new KingdomColor("#888888", "#888888", "#888888");
		this.add(new KingdomAsset
		{
			id = "goldenBrain",
			mobs = true,
			brain = true
		});
		this.t.addTag("neutral");
		this.t.addFriendlyTag("bandits");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("civ");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("living_houses");
		this.t.addFriendlyTag("snowman");
		this.add(new KingdomAsset
		{
			id = "corruptedBrain",
			mobs = true,
			brain = true
		});
		this.t.addTag("mad");
		this.add(new KingdomAsset
		{
			id = "tumor",
			mobs = true
		});
		this.add(new KingdomAsset
		{
			id = "biomass",
			mobs = true
		});
		this.add(new KingdomAsset
		{
			id = "assimilators",
			mobs = true
		});
		this.add(new KingdomAsset
		{
			id = "super_pumpkins",
			mobs = true
		});
		this.t.addFriendlyTag("druid");
		this.add(new KingdomAsset
		{
			id = "neutral_animals",
			mobs = true
		});
		this.t.addTag("neutral");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("good");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("living_houses");
		this.t.addFriendlyTag("snowman");
		this.t.addFriendlyTag("civ");
		this.add(new KingdomAsset
		{
			id = "cats",
			mobs = true
		});
		this.t.addTag("cats");
		this.t.addTag("neutral");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("living_houses");
		this.t.addFriendlyTag("snowman");
		this.t.addFriendlyTag("civ");
		this.t.addEnemyTag("rats");
		this.t.addEnemyTag("ratKings");
		this.add(new KingdomAsset
		{
			id = "wolves",
			mobs = true
		});
		this.t.addTag("wolves");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("wolves");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("living_houses");
		this.add(new KingdomAsset
		{
			id = "hyenas",
			mobs = true
		});
		this.t.addTag("hyenas");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("hyenas");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("living_houses");
		this.add(new KingdomAsset
		{
			id = "crocodiles",
			mobs = true
		});
		this.t.addTag("crocodiles");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("crocodiles");
		this.t.addFriendlyTag("nature_creature");
		this.add(new KingdomAsset
		{
			id = "snakes",
			mobs = true
		});
		this.t.addTag("snakes");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("snakes");
		this.t.addFriendlyTag("nature_creature");
		this.t.addEnemyTag("civ");
		this.add(new KingdomAsset
		{
			id = "rhinos",
			mobs = true
		});
		this.t.addTag("rhinos");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("rhinos");
		this.t.addFriendlyTag("nature_creature");
		this.t.addEnemyTag("hyenas");
		this.t.addEnemyTag("snakes");
		this.t.addEnemyTag("bears");
		this.t.addEnemyTag("wolves");
		this.t.addEnemyTag("rats");
		this.add(new KingdomAsset
		{
			id = "bears",
			mobs = true
		});
		this.t.addTag("bears");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("bears");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("living_houses");
		this.t.addEnemyTag("rhinos");
		this.add(new KingdomAsset
		{
			id = "acids",
			mobs = true
		});
		this.t.addTag("acids");
		this.t.addFriendlyTag("acids");
		this.t.addFriendlyTag("rats");
		this.add(new KingdomAsset
		{
			id = "demons",
			mobs = true
		});
		this.add(new KingdomAsset
		{
			id = "walkers",
			mobs = true
		});
		this.t.addTag("snow");
		this.t.addFriendlyTag("snow");
		this.add(new KingdomAsset
		{
			id = "dragons",
			mobs = true
		});
		this.add(new KingdomAsset
		{
			id = "aliens",
			mobs = true
		});
		this.add(new KingdomAsset
		{
			id = "piranhas",
			mobs = true
		});
		this.add(new KingdomAsset
		{
			id = "bandits",
			mobs = true
		});
		this.t.addTag("bandits");
		this.t.addTag("neutral");
		this.t.addFriendlyTag("neutral");
		this.t.addEnemyTag("civ");
		this.add(new KingdomAsset
		{
			id = "undead",
			mobs = true
		});
		this.add(new KingdomAsset
		{
			id = "living_plants",
			mobs = true
		});
		this.t.addTag("nature_creature");
		this.t.addTag("living_plants");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("good");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("mush");
		this.add(new KingdomAsset
		{
			id = "living_houses",
			mobs = true
		});
		this.t.addTag("living_houses");
		this.t.addFriendlyTag("living_houses");
		this.add(new KingdomAsset
		{
			id = "ants",
			mobs = true
		});
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("good");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("living_houses");
		this.add(new KingdomAsset
		{
			id = "santa",
			mobs = true
		});
		this.t.addTag("santa");
		this.t.addFriendlyTag("snowman");
		this.add(new KingdomAsset
		{
			id = "rats",
			mobs = true
		});
		this.t.addTag("rats");
		this.t.addTag("neutral");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("acids");
		this.t.addFriendlyTag("good");
		this.t.addFriendlyTag("rats");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("civ");
		this.t.addFriendlyTag("nature_creature");
		this.t.addEnemyTag("cats");
		this.add(new KingdomAsset
		{
			id = "ratKings",
			attack_each_other = true,
			mobs = true
		});
		this.t.addTag("rats");
		this.t.addTag("neutral");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("acids");
		this.t.addFriendlyTag("good");
		this.t.addFriendlyTag("rats");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("civ");
		this.t.addFriendlyTag("nature_creature");
		this.t.addEnemyTag("cats");
		this.add(new KingdomAsset
		{
			id = "crab",
			mobs = true
		});
		this.t.addTag("crab");
		this.t.addTag("neutral");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("good");
		this.t.addFriendlyTag("civ");
		this.t.addFriendlyTag("crab");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("living_houses");
		this.t.addFriendlyTag("snowman");
		this.add(new KingdomAsset
		{
			id = "crabzilla",
			mobs = true
		});
		this.t.addTag("crab");
		this.t.addFriendlyTag("crab");
		this.add(new KingdomAsset
		{
			id = "snowman",
			mobs = true
		});
		this.t.addTag("snow");
		this.t.addFriendlyTag("good");
		this.t.addFriendlyTag("snow");
		this.t.addFriendlyTag("santa");
		this.add(new KingdomAsset
		{
			id = "evil",
			mobs = true
		});
		this.t.addTag("evil");
		this.t.addFriendlyTag("evil");
		this.add(new KingdomAsset
		{
			id = "druid",
			mobs = true
		});
		this.t.addTag("good");
		this.t.addTag("nature_creature");
		this.t.addFriendlyTag("good");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("civ");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("super_pumpkins");
		this.add(new KingdomAsset
		{
			id = "good",
			mobs = true
		});
		this.t.addTag("good");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("civ");
		this.t.addFriendlyTag("neutral");
		this.t.addFriendlyTag("nature_creature");
		this.t.addFriendlyTag("living_houses");
		this.t.addFriendlyTag("snowman");
		this.t.addEnemyTag("wolves");
		this.t.addEnemyTag("bears");
		this.t.addEnemyTag("orc");
		this.t.addEnemyTag("bandits");
		this.add(new KingdomAsset
		{
			id = "mad",
			attack_each_other = true,
			mobs = true,
			mad = true
		});
		this.t.default_kingdom_color = new KingdomColor("#E53B3B", "#E53B3B", "#E53B3B");
		this.t.addTag("mad");
		this.t.addFriendlyTag("mad");
		this.add(new KingdomAsset
		{
			id = "greg",
			mobs = true
		});
		this.t.addTag("greg");
		this.add(new KingdomAsset
		{
			id = "mush",
			mobs = true
		});
		this.t.addTag("mush");
		this.t.addFriendlyTag("living_plants");
		this.add(new KingdomAsset
		{
			id = "godfinger",
			nature = true
		});
	}
}
