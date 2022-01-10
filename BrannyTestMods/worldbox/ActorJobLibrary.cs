using System;
using ai.behaviours.conditions;
using Beebyte.Obfuscator;

// Token: 0x0200015C RID: 348
[ObfuscateLiterals]
public class ActorJobLibrary : AssetLibrary<ActorJob>
{
	// Token: 0x060007EA RID: 2026 RVA: 0x00056989 File Offset: 0x00054B89
	public override void init()
	{
		base.init();
		this.initJobsCivs();
		this.initJobsMobs();
		this.initJobsBoats();
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x000569A4 File Offset: 0x00054BA4
	private void initJobsBoats()
	{
		this.add(new ActorJob
		{
			id = "boat_fishing"
		});
		this.t.addTask("boat_danger_check");
		this.t.addTask("boat_fishing");
		this.t.addTask("boat_idle");
		this.add(new ActorJob
		{
			id = "boat_trading"
		});
		this.t.addTask("boat_danger_check");
		this.t.addTask("boat_trading");
		this.t.addTask("boat_idle");
		this.add(new ActorJob
		{
			id = "boat_transport"
		});
		this.t.addTask("boat_danger_check");
		this.t.addTask("boat_transport_check");
		this.t.addTask("boat_idle");
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x00056A88 File Offset: 0x00054C88
	private void initJobsCivs()
	{
		this.add(new ActorJob
		{
			id = "unit"
		});
		this.t.addTask("check_join_city");
		this.t.addTask("build_city_here");
		this.t.addTask("random_move");
		this.t.addTask("check_join_empty_nearby_city");
		this.t.addTask("nomad_try_build_city");
		this.t.addTask("check_if_stuck_on_small_land");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "baby"
		});
		this.t.addTask("random_move");
		this.t.addTask("city_idle_walking");
		this.t.addTask("stay_in_house");
		this.t.addTask("check_if_stuck_on_small_land");
		this.add(new ActorJob
		{
			id = "citizen"
		});
		this.t.addTask("random_move");
		this.t.addTask("city_idle_walking");
		this.t.addTask("find_city_job");
		this.t.addTask("stay_in_house");
		this.t.addTask("try_to_return_home");
		this.t.addTask("check_if_stuck_on_small_land");
		this.add(new ActorJob
		{
			id = "fireman",
			cityJob = true
		});
		this.t.addTask("put_out_fire");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "hunter",
			cityJob = true
		});
		this.t.addTask("random_move");
		this.t.addTask("look_for_animals");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "builder",
			cityJob = true
		});
		this.t.addTask("build_new_building");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "settler",
			cityJob = true,
			cancel_when_embarking = false
		});
		this.t.addTask("city_idle_walking");
		this.t.addTask("settler_check_transport");
		this.t.addTask("settler_same_island");
		this.t.addTask("build_city_here");
		this.t.addTask("check_join_city");
		this.t.addTask("try_to_return_home");
		this.t.addTask("check_if_stuck_on_small_land");
		this.add(new ActorJob
		{
			id = "cleaner",
			cityJob = true
		});
		this.t.addTask("cleaning");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "road_builder",
			cityJob = true
		});
		this.t.addTask("build_road");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "woodcutter",
			cityJob = true
		});
		this.t.addTask("chop_trees");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "miner",
			cityJob = true
		});
		this.t.addTask("mine");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "miner_deposit",
			cityJob = true
		});
		this.t.addTask("mine_deposit");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "metalworker",
			cityJob = true
		});
		this.t.addTask("convert_ore");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "blacksmith",
			cityJob = true
		});
		this.t.addTask("make_items");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "gatherer",
			cityJob = true
		});
		this.t.addTask("collect_fruits");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "farmer_plower",
			cityJob = true
		});
		this.t.addTask("farming_make_field");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "farmer",
			cityJob = true
		});
		this.t.addTask("plant_crops");
		this.t.addTask("harvest");
		this.t.addTask("end_job");
		this.add(new ActorJob
		{
			id = "attacker",
			cityJob = true
		});
		this.t.addTask("warrior_check_city_army_group");
		this.t.addTask("city_idle_walking");
		this.t.addCondition(new CondGroupLeader());
		this.t.addCondition(new CondHaveUnitGroup());
		this.t.addCondition(new CondNoAttackTarget());
		this.t.addTask("warrior_army_leader_move_random");
		this.t.addCondition(new CondGroupLeader());
		this.t.addCondition(new CondHaveUnitGroup());
		this.t.addTask("warrior_army_leader_move_to_attack_target");
		this.t.addCondition(new CondGroupLeader());
		this.t.addCondition(new CondHaveUnitGroup());
		this.t.addCondition(new CondHaveAttackTarget());
		this.t.addTask("warrior_army_follow_leader");
		this.t.addCondition(new CondHaveUnitGroup());
		this.t.addCondition(new CondNotGroupLeader());
		this.t.addTask("city_idle_walking");
		this.t.addCondition(new CondNoUnitGroup());
		this.t.addTask("random_move");
		this.t.addCondition(new CondNoUnitGroup());
		this.t.addTask("check_warrior_transport");
		this.t.addTask("wait");
		this.t.addTask("try_to_return_home");
		this.t.addTask("check_if_stuck_on_small_land");
		this.add(new ActorJob
		{
			id = "defender",
			cityJob = true
		});
		this.t.addTask("check_warrior_defender");
		this.t.addTask("wait");
		this.t.addTask("try_to_return_home");
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x000571A4 File Offset: 0x000553A4
	private void initJobsMobs()
	{
		this.add(new ActorJob
		{
			id = "rat"
		});
		this.t.addTask("rat");
		this.t.addTask("check_hunger_animal");
		this.t.addTask("try_babymaking");
		this.add(new ActorJob
		{
			id = "crab"
		});
		this.t.addTask("swim_to_island");
		this.t.addTask("crab_danger_check");
		this.t.addTask("random_move");
		this.t.addTask("crab_danger_check");
		this.t.addTask("try_babymaking");
		this.t.addTask("crab");
		this.t.addTask("crab_danger_check");
		this.add(new ActorJob
		{
			id = "crab_burrow"
		});
		this.t.addTask("crab_burrow");
		this.add(new ActorJob
		{
			id = "animal_water_eater"
		});
		this.t.addTask("swim_to_island");
		this.t.addTask("wait10");
		this.t.addTask("random_move");
		this.t.addTask("try_babymaking");
		this.t.addTask("water_feeding");
		this.t.addTask("wait10");
		this.add(new ActorJob
		{
			id = "animal"
		});
		this.t.addTask("random_animal_move");
		this.t.addTask("check_hunger_animal");
		this.t.addTask("try_babymaking");
		this.t.addTask("wait10");
		this.add(new ActorJob
		{
			id = "bee"
		});
		this.t.addTask("pollinate");
		this.t.addTask("random_move");
		this.add(new ActorJob
		{
			id = "animal_herd"
		});
		this.t.addTask("random_move");
		this.t.addTask("check_hunger_animal");
		this.t.addTask("try_babymaking");
		this.t.addTask("follow_same_race");
		this.t.addTask("wait5");
		this.add(new ActorJob
		{
			id = "random_move"
		});
		this.t.addTask("random_move");
		this.add(new ActorJob
		{
			id = "random_swim"
		});
		this.t.addTask("random_swim");
		this.add(new ActorJob
		{
			id = "move_mob"
		});
		this.t.addTask("random_move");
		this.t.addTask("attack_golden_brain");
		this.add(new ActorJob
		{
			id = "skeleton_job"
		});
		this.t.addTask("skeleton_move");
		this.t.addTask("attack_golden_brain");
		this.add(new ActorJob
		{
			id = "necromancer"
		});
		this.t.addTask("random_move");
		this.t.addTask("wait");
		this.t.addTask("check_heal");
		this.t.addTask("make_skeleton");
		this.t.addTask("attack_golden_brain");
		this.add(new ActorJob
		{
			id = "evil_mage"
		});
		this.t.addTask("random_move");
		this.t.addTask("check_heal");
		this.t.addTask("random_teleport");
		this.t.addTask("wait");
		this.add(new ActorJob
		{
			id = "white_mage"
		});
		this.t.addTask("random_move");
		this.t.addTask("check_heal");
		this.t.addTask("random_teleport");
		this.t.addTask("wait");
		this.add(new ActorJob
		{
			id = "druid"
		});
		this.t.addTask("random_move");
		this.t.addTask("check_heal");
		this.t.addTask("spawn_grass_seeds");
		this.t.addTask("wait");
		this.add(new ActorJob
		{
			id = "plague_doctor"
		});
		this.t.addTask("check_cure");
		this.t.addTask("burn_tumors");
		this.t.addTask("random_move_towards_civ_building");
		this.t.addTask("check_heal");
		this.t.addTask("wait");
		this.add(new ActorJob
		{
			id = "mush"
		});
		this.t.addTask("random_move");
		this.t.addTask("wait");
		this.add(new ActorJob
		{
			id = "tumor_monster"
		});
		this.t.addTask("random_move");
		this.t.addTask("wait");
		this.t.addTask("attack_golden_brain");
		this.add(new ActorJob
		{
			id = "unit_on_fire"
		});
		this.t.addTask("short_move");
		this.t.addTask("short_move");
		this.t.addTask("short_move");
		this.t.addTask("run_to_water");
		this.t.addTask("end_job");
	}
}
