using System;
using ai.behaviours;

// Token: 0x02000196 RID: 406
public class SimpleMod
{
	// Token: 0x0600095A RID: 2394 RVA: 0x00063324 File Offset: 0x00061524
	public SimpleMod()
	{
		ProjectileAsset projectileAsset = AssetManager.projectiles.clone("skeleton_arrow", "arrow");
		projectileAsset.trailEffect_enabled = true;
		projectileAsset.texture = "fireball";
		projectileAsset.targetScale = 0.5f;
		ItemAsset itemAsset = AssetManager.items.clone("skeleton_bow", "_range");
		itemAsset.baseStats.range = 22f;
		itemAsset.baseStats.crit = 10f;
		itemAsset.baseStats.damageCritMod = 0.5f;
		itemAsset.projectile = "skeleton_arrow";
		ActorStats actorStats = AssetManager.unitStats.clone("super_skeleton", "skeleton");
		actorStats.defaultAttack = "skeleton_bow";
		actorStats.defaultWeapons = null;
		actorStats.baseStats.health = 100000;
		actorStats.baseStats.damage = 500;
		actorStats.baseStats.speed = 500f;
		actorStats.job = "super_skeleton_job";
		AssetManager.unitStats.addTrait("regeneration");
		AssetManager.unitStats.addTrait("immortal");
		ActorJob actorJob = AssetManager.job_actor.add(new ActorJob
		{
			id = "super_skeleton_job"
		});
		actorJob.addTask("mod_destroy_trees");
		actorJob.addTask("random_move");
		actorJob.addTask("wait");
		actorJob.addTask("follow_same_race");
		actorJob.addTask("attack_golden_brain");
		BehaviourTaskActor behaviourTaskActor = AssetManager.tasks_actor.add(new BehaviourTaskActor
		{
			id = "mod_destroy_trees"
		});
		behaviourTaskActor.addBeh(new BehFindBuilding("trees"));
		behaviourTaskActor.addBeh(new BehGoToBuildingTarget(false));
		behaviourTaskActor.addBeh(new BehResourceGatheringAnimation(1f));
		behaviourTaskActor.addBeh(new BehResourceGatheringAnimation(1f));
		behaviourTaskActor.addBeh(new BehResourceGatheringAnimation(1f));
		behaviourTaskActor.addBeh(new BehResourceGatheringAnimation(1f));
		behaviourTaskActor.addBeh(new BehExtractResourcesFromBuilding());
		behaviourTaskActor.addBeh(new BehRandomWait(1f, 2f));
	}
}
