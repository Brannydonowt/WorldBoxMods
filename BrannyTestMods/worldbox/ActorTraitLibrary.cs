using System;
using Beebyte.Obfuscator;

// Token: 0x0200001A RID: 26
[ObfuscateLiterals]
[Serializable]
public class ActorTraitLibrary : AssetLibrary<ActorTrait>
{
	// Token: 0x060000CA RID: 202 RVA: 0x0000FCEC File Offset: 0x0000DEEC
	public override void init()
	{
		base.init();
		this.add(new ActorTrait
		{
			id = "boat",
			icon = "iconBoat",
			opposite = "plague,immortal,energized,wise"
		});
		this.add(new ActorTrait
		{
			id = "energized",
			icon = "iconLightning"
		});
		this.t.baseStats.speed = 10f;
		this.add(new ActorTrait
		{
			id = "vermin",
			icon = "iconVermin"
		});
		this.add(new ActorTrait
		{
			id = "ratKing",
			icon = "iconRatKing"
		});
		this.add(new ActorTrait
		{
			id = "rat",
			icon = "iconRat"
		});
		ActorTrait t = this.t;
		t.action_special_effect = (WorldAction)Delegate.Combine(t.action_special_effect, new WorldAction(ActionLibrary.ratEffect));
		this.add(new ActorTrait
		{
			id = "healing_aura",
			icon = "iconHealingAura"
		});
		ActorTrait t2 = this.t;
		t2.action_special_effect = (WorldAction)Delegate.Combine(t2.action_special_effect, new WorldAction(ActionLibrary.healingAuraEffect));
		this.add(new ActorTrait
		{
			id = "savage",
			icon = "iconSavage",
			group = TraitGroup.Personality,
			type = TraitType.Positive,
			sameTraitMod = 5,
			birth = 0.5f
		});
		this.add(new ActorTrait
		{
			id = "miner",
			icon = "iconMiner",
			group = TraitGroup.Personality,
			type = TraitType.Positive,
			birth = 0.5f
		});
		this.add(new ActorTrait
		{
			id = "veteran",
			icon = "iconVeteran",
			group = TraitGroup.Personality,
			type = TraitType.Positive,
			sameTraitMod = 5
		});
		this.t.baseStats.warfare = 5;
		this.t.baseStats.damage = 3;
		this.t.baseStats.armor = 1;
		this.t.baseStats.dodge = 2f;
		this.t.baseStats.health = 30;
		this.add(new ActorTrait
		{
			id = "wise",
			icon = "iconWise",
			group = TraitGroup.Personality,
			type = TraitType.Positive,
			opposite = "stupid,boat"
		});
		this.t.baseStats.diplomacy = 1;
		this.t.baseStats.stewardship = 1;
		this.t.baseStats.warfare = 1;
		this.t.baseStats.intelligence = 1;
		this.add(new ActorTrait
		{
			id = "strong_minded",
			icon = "iconStrongMinded",
			opposite = "madness",
			group = TraitGroup.Personality,
			type = TraitType.Positive
		});
		this.add(new ActorTrait
		{
			id = "peaceful",
			icon = "iconPeaceful",
			group = TraitGroup.Personality,
			type = TraitType.Positive
		});
		this.add(new ActorTrait
		{
			id = "zombie",
			icon = "iconZombie"
		});
		ActorTrait t3 = this.t;
		t3.action_special_effect = (WorldAction)Delegate.Combine(t3.action_special_effect, new WorldAction(ActionLibrary.zombieEffect));
		this.add(new ActorTrait
		{
			id = "infected",
			icon = "iconInfected",
			opposite = "immune,boat",
			inherit = 100f
		});
		this.t.can_be_cured = true;
		this.t.transformationTrait = true;
		ActorTrait t4 = this.t;
		t4.action_special_effect = (WorldAction)Delegate.Combine(t4.action_special_effect, new WorldAction(ActionLibrary.infectedEffect));
		ActorTrait t5 = this.t;
		t5.action_death = (WorldAction)Delegate.Combine(t5.action_death, new WorldAction(ActionLibrary.turnIntoZombie));
		this.t.baseStats.dodge = -5f;
		this.t.baseStats.speed = 4f;
		this.t.baseStats.loyalty_traits = -15;
		this.add(new ActorTrait
		{
			id = "mushSpores",
			icon = "iconMushSpores",
			opposite = "immune,boat",
			inherit = 100f
		});
		this.t.can_be_cured = true;
		this.t.transformationTrait = true;
		ActorTrait t6 = this.t;
		t6.action_death = (WorldAction)Delegate.Combine(t6.action_death, new WorldAction(ActionLibrary.mushSporesEffect));
		ActorTrait t7 = this.t;
		t7.action_death = (WorldAction)Delegate.Combine(t7.action_death, new WorldAction(ActionLibrary.turnIntoMush));
		this.t.baseStats.dodge = -5f;
		this.t.baseStats.speed = 4f;
		this.t.baseStats.loyalty_traits = -15;
		this.add(new ActorTrait
		{
			id = "tumorInfection",
			icon = "iconTumorInfection",
			opposite = "immune,boat",
			inherit = 100f
		});
		this.t.can_be_cured = true;
		ActorTrait t8 = this.t;
		t8.action_special_effect = (WorldAction)Delegate.Combine(t8.action_special_effect, new WorldAction(ActionLibrary.tumorEffect));
		ActorTrait t9 = this.t;
		t9.action_death = (WorldAction)Delegate.Combine(t9.action_death, new WorldAction(ActionLibrary.turnIntoTumorMonster));
		this.t.baseStats.dodge = -5f;
		this.t.baseStats.speed = 4f;
		this.t.baseStats.loyalty_traits = -15;
		this.add(new ActorTrait
		{
			id = "plague",
			icon = "iconPlague",
			opposite = "immune,immortal,rat,ratKing,boat",
			inherit = 100f
		});
		this.t.can_be_cured = true;
		ActorTrait t10 = this.t;
		t10.action_special_effect = (WorldAction)Delegate.Combine(t10.action_special_effect, new WorldAction(ActionLibrary.plagueEffect));
		this.t.baseStats.speed = -10f;
		this.t.baseStats.damage = -3;
		this.t.baseStats.dodge = -10f;
		this.t.baseStats.armor = -1;
		this.t.baseStats.loyalty_traits = -15;
		this.add(new ActorTrait
		{
			id = "blessed",
			icon = "iconBlessing",
			opposite = "evil"
		});
		this.t.baseStats.mod_armor = 10f;
		this.t.baseStats.mod_damage = 50f;
		this.t.baseStats.mod_health = 50f;
		this.t.baseStats.mod_speed = 50f;
		this.t.baseStats.mod_diplomacy = 20f;
		this.t.baseStats.mod_crit = 10f;
		this.add(new ActorTrait
		{
			id = "cursed",
			icon = "iconCurse",
			opposite = "evil,boat"
		});
		this.t.can_be_cured = true;
		this.t.transformationTrait = true;
		ActorTrait t11 = this.t;
		t11.action_death = (WorldAction)Delegate.Combine(t11.action_death, new WorldAction(ActionLibrary.turnIntoSkeleton));
		this.t.baseStats.loyalty_traits = -100;
		this.t.baseStats.mod_armor = -50f;
		this.t.baseStats.mod_damage = -50f;
		this.t.baseStats.mod_health = -50f;
		this.t.baseStats.mod_speed = -20f;
		this.t.baseStats.mod_diplomacy = -90f;
		this.t.baseStats.mod_attackSpeed = -50f;
		this.add(new ActorTrait
		{
			id = "evil",
			icon = "iconEvil",
			opposite = "cursed,blessed"
		});
		this.add(new ActorTrait
		{
			id = "kingslayer",
			icon = "iconKingslayer"
		});
		this.t.baseStats.mod_supply_timer = 2f;
		this.t.baseStats.loyalty_traits = -25;
		this.t.baseStats.attackSpeed = 5f;
		this.t.baseStats.diplomacy = -5;
		this.t.baseStats.warfare = 5;
		this.add(new ActorTrait
		{
			id = "mageslayer",
			icon = "iconMageslayer"
		});
		this.t.baseStats.loyalty_traits = -10;
		this.t.baseStats.warfare = 5;
		this.t.baseStats.crit = 3f;
		this.t.baseStats.dodge = 2f;
		this.t.baseStats.accuracy = 3f;
		this.add(new ActorTrait
		{
			id = "dragonslayer",
			icon = "iconDragonslayer"
		});
		this.t.baseStats.warfare = 6;
		this.t.baseStats.crit = 4f;
		this.t.baseStats.dodge = 3f;
		this.t.baseStats.accuracy = 5f;
		this.t.baseStats.mod_diplomacy = 10f;
		this.add(new ActorTrait
		{
			id = "giant",
			icon = "iconGiant",
			group = TraitGroup.Personality,
			type = TraitType.Positive,
			opposite = "tiny",
			birth = 0.5f,
			inherit = 10f
		});
		this.t.baseStats.scale = 0.05f;
		this.t.baseStats.mod_health = 50f;
		this.t.baseStats.attackSpeed = -5f;
		this.add(new ActorTrait
		{
			id = "tiny",
			icon = "iconTiny",
			group = TraitGroup.Personality,
			type = TraitType.Negative,
			opposite = "giant",
			birth = 0.5f,
			inherit = 10f
		});
		this.t.baseStats.diplomacy = -1;
		this.t.baseStats.scale = -0.02f;
		this.t.baseStats.mod_health = -50f;
		this.t.baseStats.attackSpeed = 10f;
		this.t.baseStats.speed = 5f;
		this.add(new ActorTrait
		{
			id = "madness",
			icon = "iconMadness",
			opposite = "strong_minded"
		});
		this.t.baseStats.damage = 1;
		this.t.baseStats.speed = 5f;
		this.t.baseStats.diplomacy = -100;
		this.t.baseStats.attackSpeed = 10f;
		this.t.baseStats.loyalty_traits = -100;
		this.add(new ActorTrait
		{
			id = "immortal",
			icon = "iconImmortal",
			birth = 0.1f,
			sameTraitMod = -20,
			group = TraitGroup.Genetic,
			type = TraitType.Positive,
			opposite = "boat"
		});
		this.t.baseStats.loyalty_traits = -20;
		this.add(new ActorTrait
		{
			id = "crippled",
			icon = "iconCrippled",
			sameTraitMod = 10,
			type = TraitType.Negative
		});
		this.t.baseStats.speed = -15f;
		this.t.baseStats.diplomacy = -1;
		this.t.baseStats.attackSpeed = -15f;
		this.add(new ActorTrait
		{
			id = "golden_tooth",
			icon = "iconGoldenTooth",
			sameTraitMod = 5,
			type = TraitType.Positive
		});
		this.t.baseStats.diplomacy = 2;
		this.add(new ActorTrait
		{
			id = "eyepatch",
			icon = "iconEyePatch",
			sameTraitMod = 20,
			type = TraitType.Negative
		});
		this.t.baseStats.accuracy = -10f;
		this.t.baseStats.diplomacy = 1;
		this.t.baseStats.attackSpeed = -15f;
		this.t.baseStats.crit = -15f;
		this.add(new ActorTrait
		{
			id = "skin_burns",
			icon = "iconSkinBurns",
			sameTraitMod = 40,
			type = TraitType.Negative
		});
		this.t.baseStats.diplomacy = -1;
		this.t.baseStats.speed = -5f;
		this.t.baseStats.attackSpeed = -5f;
		this.add(new ActorTrait
		{
			id = "tough",
			icon = "iconTough",
			birth = 1.5f,
			group = TraitGroup.Genetic,
			type = TraitType.Positive,
			sameTraitMod = -5
		});
		this.t.baseStats.armor = 2;
		this.add(new ActorTrait
		{
			id = "strong",
			icon = "iconStrong",
			birth = 1.5f,
			opposite = "weak",
			oppositeTraitMod = -10,
			sameTraitMod = 5,
			group = TraitGroup.Genetic,
			type = TraitType.Positive
		});
		this.t.baseStats.damage = 3;
		this.t.baseStats.warfare = 2;
		this.add(new ActorTrait
		{
			id = "weak",
			icon = "iconWeak",
			birth = 1.5f,
			opposite = "strong",
			oppositeTraitMod = -10,
			group = TraitGroup.Genetic,
			type = TraitType.Negative
		});
		this.t.baseStats.damage = -1;
		this.t.baseStats.warfare = -2;
		this.t.baseStats.diplomacy = -2;
		this.add(new ActorTrait
		{
			id = "stupid",
			icon = "iconStupid",
			birth = 1.5f,
			opposite = "genius,wise",
			sameTraitMod = 30,
			group = TraitGroup.Genetic,
			type = TraitType.Negative
		});
		this.t.baseStats.intelligence = -5;
		this.t.baseStats.diplomacy = -2;
		this.t.baseStats.warfare = -2;
		this.t.baseStats.stewardship = -5;
		this.t.baseStats.loyalty_traits = -15;
		this.t.baseStats.personality_rationality = -0.5f;
		this.add(new ActorTrait
		{
			id = "genius",
			icon = "iconGenius",
			birth = 0.9f,
			opposite = "stupid",
			sameTraitMod = 20,
			oppositeTraitMod = -20,
			group = TraitGroup.Genetic,
			type = TraitType.Positive
		});
		this.t.baseStats.intelligence = 10;
		this.t.baseStats.diplomacy = 5;
		this.t.baseStats.warfare = 5;
		this.t.baseStats.stewardship = 7;
		this.t.baseStats.loyalty_traits = -10;
		this.add(new ActorTrait
		{
			id = "regeneration",
			icon = "iconRegeneration",
			birth = 0.5f,
			group = TraitGroup.Genetic,
			type = TraitType.Positive
		});
		ActorTrait t12 = this.t;
		t12.action_special_effect = (WorldAction)Delegate.Combine(t12.action_special_effect, new WorldAction(ActionLibrary.regenerationEffect));
		this.add(new ActorTrait
		{
			id = "ugly",
			icon = "iconUgly",
			birth = 2f,
			opposite = "attractive",
			sameTraitMod = 5,
			oppositeTraitMod = -15,
			group = TraitGroup.Genetic,
			type = TraitType.Negative
		});
		this.add(new ActorTrait
		{
			id = "fat",
			icon = "iconFat",
			birth = 2f,
			opposite = "agile,weightless",
			oppositeTraitMod = -10,
			sameTraitMod = 10,
			group = TraitGroup.Genetic,
			type = TraitType.Negative
		});
		this.t.baseStats.dodge = -10f;
		this.t.baseStats.accuracy = -5f;
		this.t.baseStats.attackSpeed = -10f;
		this.add(new ActorTrait
		{
			id = "attractive",
			icon = "iconAttractive",
			birth = 1.5f,
			opposite = "ugly",
			sameTraitMod = 10,
			group = TraitGroup.Genetic,
			type = TraitType.Positive
		});
		this.t.baseStats.diplomacy = 2;
		this.t.baseStats.stewardship = 1;
		this.t.baseStats.crit = 10f;
		this.add(new ActorTrait
		{
			id = "fast",
			icon = "iconFast",
			birth = 1f,
			opposite = "slow",
			group = TraitGroup.Genetic,
			type = TraitType.Positive
		});
		this.t.baseStats.speed = 10f;
		this.t.baseStats.attackSpeed = 5f;
		this.add(new ActorTrait
		{
			id = "slow",
			icon = "iconSlow",
			birth = 2f,
			opposite = "fast,agile",
			group = TraitGroup.Genetic,
			type = TraitType.Negative
		});
		this.t.baseStats.speed = -10f;
		this.t.baseStats.attackSpeed = -5f;
		this.add(new ActorTrait
		{
			id = "gluttonous",
			icon = "iconGluttonous",
			birth = 1f,
			inherit = 50f,
			sameTraitMod = 5,
			group = TraitGroup.Genetic,
			type = TraitType.Negative
		});
		this.add(new ActorTrait
		{
			id = "burning_feet",
			icon = "iconBurningFeet"
		});
		ActorTrait t13 = this.t;
		t13.action_special_effect = (WorldAction)Delegate.Combine(t13.action_special_effect, new WorldAction(ActionLibrary.burningFeetEffect));
		this.add(new ActorTrait
		{
			id = "flower_prints",
			icon = "iconFlowerPrints"
		});
		ActorTrait t14 = this.t;
		t14.action_special_effect = (WorldAction)Delegate.Combine(t14.action_special_effect, new WorldAction(ActionLibrary.flowerPrintsEffect));
		this.add(new ActorTrait
		{
			id = "acid_touch",
			icon = "iconAcidTouch"
		});
		ActorTrait t15 = this.t;
		t15.action_special_effect = (WorldAction)Delegate.Combine(t15.action_special_effect, new WorldAction(ActionLibrary.acidTouchEffect));
		this.add(new ActorTrait
		{
			id = "acid_blood",
			icon = "iconAcidBlood"
		});
		ActorTrait t16 = this.t;
		t16.action_death = (WorldAction)Delegate.Combine(t16.action_death, new WorldAction(ActionLibrary.acidBloodEffect));
		this.add(new ActorTrait
		{
			id = "acid_proof",
			icon = "iconAcidProof"
		});
		this.add(new ActorTrait
		{
			id = "fire_blood",
			icon = "iconFireBlood"
		});
		ActorTrait t17 = this.t;
		t17.action_death = (WorldAction)Delegate.Combine(t17.action_death, new WorldAction(ActionLibrary.fireBlood));
		this.add(new ActorTrait
		{
			id = "fire_proof",
			icon = "iconFireProof"
		});
		this.add(new ActorTrait
		{
			id = "freeze_proof",
			icon = "iconFreezeProof"
		});
		this.add(new ActorTrait
		{
			id = "cold_aura",
			icon = "iconColdAura"
		});
		ActorTrait t18 = this.t;
		t18.action_special_effect = (WorldAction)Delegate.Combine(t18.action_special_effect, new WorldAction(ActionLibrary.coldAuraEffect));
		this.add(new ActorTrait
		{
			id = "bomberman",
			icon = "iconGrenade"
		});
		ActorTrait t19 = this.t;
		t19.action_special_effect = (WorldAction)Delegate.Combine(t19.action_special_effect, new WorldAction(ActionLibrary.bombermanEffect));
		this.add(new ActorTrait
		{
			id = "pyromaniac",
			icon = "iconPyromaniac",
			birth = 1f
		});
		ActorTrait t20 = this.t;
		t20.action_special_effect = (WorldAction)Delegate.Combine(t20.action_special_effect, new WorldAction(ActionLibrary.pyromaniacEffect));
		this.t.baseStats.warfare = 3;
		this.add(new ActorTrait
		{
			id = "eagle_eyed",
			icon = "iconEagleEye",
			birth = 0.7f,
			opposite = "short_sighted",
			group = TraitGroup.Genetic,
			type = TraitType.Positive
		});
		this.t.baseStats.accuracy = 10f;
		this.t.baseStats.crit = 15f;
		this.add(new ActorTrait
		{
			id = "short_sighted",
			icon = "iconShortsighted",
			birth = 0.7f,
			opposite = "eagle_eyed",
			group = TraitGroup.Genetic,
			type = TraitType.Negative
		});
		this.t.baseStats.accuracy = -5f;
		this.t.baseStats.crit = -5f;
		this.add(new ActorTrait
		{
			id = "lucky",
			icon = "iconLucky",
			birth = 0.7f,
			opposite = "unlucky",
			group = TraitGroup.Genetic,
			type = TraitType.Positive
		});
		this.t.baseStats.dodge = 5f;
		this.t.baseStats.accuracy = 4f;
		this.t.baseStats.crit = 30f;
		this.add(new ActorTrait
		{
			id = "unlucky",
			icon = "iconUnlucky",
			birth = 0.7f,
			opposite = "lucky",
			group = TraitGroup.Genetic,
			type = TraitType.Negative
		});
		this.t.baseStats.dodge = -2f;
		this.t.baseStats.accuracy = -4f;
		this.t.baseStats.crit = -30f;
		this.add(new ActorTrait
		{
			id = "immune",
			icon = "iconImmune",
			birth = 0.25f,
			inherit = 10f,
			group = TraitGroup.Genetic,
			type = TraitType.Positive
		});
		this.t.baseStats.armor = 3;
		this.add(new ActorTrait
		{
			id = "agile",
			icon = "iconAgile",
			birth = 0.75f,
			opposite = "fat,slow",
			sameTraitMod = 5,
			group = TraitGroup.Genetic,
			type = TraitType.Positive
		});
		this.t.baseStats.dodge = 10f;
		this.t.baseStats.attackSpeed = 30f;
		this.add(new ActorTrait
		{
			id = "deceitful",
			icon = "iconDeceitful",
			birth = 1.5f,
			inherit = -1f,
			opposite = "honest",
			sameTraitMod = -15,
			oppositeTraitMod = -5,
			group = TraitGroup.Personality,
			type = TraitType.Negative
		});
		this.t.baseStats.diplomacy = 1;
		this.t.baseStats.stewardship = 4;
		this.t.baseStats.loyalty_traits = -20;
		this.add(new ActorTrait
		{
			id = "bloodlust",
			icon = "iconBloodlust",
			birth = 1.5f,
			opposite = "pacifist",
			group = TraitGroup.Personality,
			type = TraitType.Negative
		});
		this.t.baseStats.mod_supply_timer = 1.5f;
		this.t.baseStats.loyalty_traits = -20;
		this.t.baseStats.warfare = 10;
		this.t.baseStats.diplomacy = -2;
		this.add(new ActorTrait
		{
			id = "pacifist",
			icon = "iconPacifist",
			birth = 1.5f,
			opposite = "bloodlust",
			group = TraitGroup.Personality,
			type = TraitType.Positive
		});
		this.t.baseStats.mod_supply_timer = -0.1f;
		this.t.baseStats.loyalty_traits = 50;
		this.t.baseStats.diplomacy = 10;
		this.t.baseStats.warfare = -4;
		this.add(new ActorTrait
		{
			id = "ambitious",
			icon = "iconAmbitious",
			birth = 1.6f,
			opposite = "content",
			inherit = -1f,
			sameTraitMod = -10
		});
		this.t.baseStats.diplomacy = 2;
		this.t.baseStats.warfare = 4;
		this.t.baseStats.stewardship = 1;
		this.t.baseStats.damage = 1;
		this.t.baseStats.loyalty_traits = -15;
		this.add(new ActorTrait
		{
			id = "content",
			icon = "iconContent",
			birth = 2f,
			opposite = "ambitious,greedy",
			inherit = -1f,
			sameTraitMod = 15,
			group = TraitGroup.Personality,
			type = TraitType.Positive
		});
		this.t.baseStats.mod_supply_timer = -0.3f;
		this.t.baseStats.loyalty_traits = 10;
		this.t.baseStats.diplomacy = 2;
		this.t.baseStats.stewardship = 2;
		this.t.baseStats.warfare = -2;
		this.add(new ActorTrait
		{
			id = "honest",
			icon = "iconHonest",
			birth = 1.8f,
			opposite = "deceitful",
			inherit = -1f,
			sameTraitMod = 10,
			oppositeTraitMod = -10,
			group = TraitGroup.Personality,
			type = TraitType.Positive
		});
		this.t.baseStats.stewardship = 3;
		this.t.baseStats.diplomacy = 2;
		this.t.baseStats.warfare = -2;
		this.t.baseStats.loyalty_traits = 5;
		this.add(new ActorTrait
		{
			id = "paranoid",
			icon = "iconParanoid",
			birth = 1.4f,
			opposite = "honest",
			inherit = -1f,
			group = TraitGroup.Personality,
			type = TraitType.Negative
		});
		this.t.baseStats.diplomacy = -2;
		this.t.baseStats.warfare = 4;
		this.t.baseStats.mod_supply_timer = 0.5f;
		this.t.baseStats.loyalty_traits = -15;
		this.add(new ActorTrait
		{
			id = "greedy",
			icon = "iconGreedy",
			birth = 1.5f,
			inherit = -1f,
			opposite = "content",
			group = TraitGroup.Personality,
			type = TraitType.Negative
		});
		this.t.baseStats.diplomacy = -2;
		this.t.baseStats.stewardship = -3;
		this.t.baseStats.warfare = 4;
		this.t.baseStats.mod_supply_timer = 4f;
		this.t.baseStats.loyalty_traits = -5;
		this.add(new ActorTrait
		{
			id = "weightless",
			icon = "iconWeightless",
			birth = 0.8f,
			opposite = "fat"
		});
		this.add(new ActorTrait
		{
			id = "poisonous",
			icon = "iconPoisonous"
		});
		this.add(new ActorTrait
		{
			id = "venomous",
			icon = "iconVenomous"
		});
		this.add(new ActorTrait
		{
			id = "poison_immune",
			icon = "iconPoisonImmune"
		});
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00011BC2 File Offset: 0x0000FDC2
	public override ActorTrait add(ActorTrait pAsset)
	{
		base.add(pAsset);
		this.checkDefault(pAsset);
		return pAsset;
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00011BD4 File Offset: 0x0000FDD4
	private void checkDefault(ActorTrait pAsset)
	{
		if (pAsset.inherit == 0f)
		{
			pAsset.inherit = pAsset.birth * 10f;
		}
		if (pAsset.opposite != null)
		{
			pAsset.oppositeArr = pAsset.opposite.Split(new char[]
			{
				','
			});
		}
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00011C24 File Offset: 0x0000FE24
	public int checkTraitsMod(Actor pMain, Actor pTarget)
	{
		int num = 0;
		for (int i = 0; i < pMain.data.traits.Count; i++)
		{
			string text = pMain.data.traits[i];
			ActorTrait actorTrait = AssetManager.traits.get(text);
			if (actorTrait != null)
			{
				if (actorTrait.sameTraitMod != 0 && pTarget.haveTrait(text))
				{
					num += actorTrait.sameTraitMod;
				}
				if (actorTrait.oppositeTraitMod != 0)
				{
					for (int j = 0; j < actorTrait.oppositeArr.Length; j++)
					{
						string pTrait = actorTrait.oppositeArr[j];
						if (pTarget.haveTrait(pTrait))
						{
							num += actorTrait.oppositeTraitMod;
						}
					}
				}
			}
		}
		return num;
	}
}
