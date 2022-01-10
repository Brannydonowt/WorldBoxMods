using System;

// Token: 0x02000042 RID: 66
public class ProjectileLibrary : AssetLibrary<ProjectileAsset>
{
	// Token: 0x060001B4 RID: 436 RVA: 0x0002230C File Offset: 0x0002050C
	public override void init()
	{
		base.init();
		this.add(new ProjectileAsset
		{
			id = "arrow",
			speed = 18f,
			texture = "arrow",
			parabolic = true,
			texture_shadow = "shadow_arrow"
		});
		this.clone("snowball", "arrow");
		this.t.texture = "snowball";
		this.t.texture_shadow = "shadow_snowball";
		this.t.speed = 15f;
		this.t.hitFreeze = true;
		this.add(new ProjectileAsset
		{
			id = "firebomb",
			speed = 5f,
			texture = "firebomb",
			parabolic = true,
			texture_shadow = "shadow_ball",
			terraformOption = "demon_fireball",
			endEffect = "fireballExplosion",
			hitShake = true,
			startScale = 0.075f,
			targetScale = 0.075f,
			playImpactSound = true,
			impactSoundID = "explosion medium",
			terraformRange = 2
		});
		this.add(new ProjectileAsset
		{
			id = "torch",
			speed = 5f,
			texture = "pr_torch",
			parabolic = true,
			texture_shadow = "shadow_ball",
			terraformOption = "torch",
			startScale = 0.075f,
			targetScale = 0.075f,
			terraformRange = 1,
			playImpactSound = false
		});
		this.add(new ProjectileAsset
		{
			id = "dark_orb",
			speed = 10f,
			texture = "dark_orb",
			texture_shadow = "shadow_ball",
			parabolic = true,
			startScale = 0.035f,
			targetScale = 0.1f
		});
		this.add(new ProjectileAsset
		{
			id = "red_orb",
			speed = 12f,
			speed_random = 5f,
			texture = "pr_red_orb",
			startScale = 0.035f,
			targetScale = 0.2f
		});
		ProjectileAsset t = this.t;
		t.world_actions = (WorldAction)Delegate.Combine(t.world_actions, new WorldAction(ActionLibrary.action_fire));
		this.add(new ProjectileAsset
		{
			id = "freeze_orb",
			speed = 12f,
			texture = "pr_freeze_orb",
			startScale = 0.035f,
			targetScale = 0.2f
		});
		this.add(new ProjectileAsset
		{
			id = "green_orb",
			speed = 20f,
			texture = "pr_green_orb",
			startScale = 0.035f,
			targetScale = 0.2f
		});
		this.add(new ProjectileAsset
		{
			id = "blue_orb_small",
			speed = 6f,
			texture = "pr_freeze_orb",
			startScale = 0.035f,
			targetScale = 0.1f,
			terraformOption = "demon_fireball"
		});
		this.add(new ProjectileAsset
		{
			id = "bone",
			speed = 10f,
			texture = "pr_bone",
			texture_shadow = "shadow_ball",
			parabolic = true,
			startScale = 0.035f,
			targetScale = 0.05f
		});
		this.add(new ProjectileAsset
		{
			id = "plasma_ball",
			speed = 20f,
			texture = "pr_plasma_ball",
			trailEffect_enabled = true,
			trailEffect_texture = "fx_plasma_trail",
			trailEffect_scale = 0.1f,
			trailEffect_timer = 0.1f,
			look_at_target = true,
			terraformOption = "plasma_ball",
			endEffect = "fireballExplosion",
			terraformRange = 2,
			startScale = 0.035f,
			targetScale = 0.2f
		});
		this.add(new ProjectileAsset
		{
			id = "fireball",
			speed = 15f,
			texture = "fireball",
			trailEffect_enabled = true,
			texture_shadow = "shadow_ball",
			terraformOption = "demon_fireball",
			endEffect = "fireballExplosion",
			terraformRange = 3,
			startScale = 0.035f,
			targetScale = 0.2f
		});
		this.add(new ProjectileAsset
		{
			id = "madness",
			speed = 30f,
			texture = "madness_ball",
			terraformOption = "madness_ball",
			texture_shadow = "shadow_ball",
			terraformRange = 3,
			startScale = 0.035f,
			targetScale = 0.2f
		});
		this.clone("rock", "arrow");
		this.t.texture = "rock";
		this.t.texture_shadow = "shadow_snowball";
		this.t.startScale = 0.05f;
		this.t.targetScale = 0.09f;
		this.t.parabolic = true;
		this.t.speed = 15f;
		this.add(new ProjectileAsset
		{
			id = "shotgun_bullet",
			speed = 20f,
			speed_random = 7f,
			texture = "shotgun_bullet",
			look_at_target = true,
			looped = false,
			animation_speed = 0.1f,
			terraformOption = "assimilator_bullet",
			startScale = 0.035f,
			targetScale = 0.08f
		});
	}
}
