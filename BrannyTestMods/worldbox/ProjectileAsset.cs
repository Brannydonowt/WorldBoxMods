using System;
using UnityEngine;

// Token: 0x02000041 RID: 65
public class ProjectileAsset : Asset
{
	// Token: 0x04000176 RID: 374
	public string texture;

	// Token: 0x04000177 RID: 375
	public bool animated = true;

	// Token: 0x04000178 RID: 376
	public float animation_speed = 0.1f;

	// Token: 0x04000179 RID: 377
	public bool looped = true;

	// Token: 0x0400017A RID: 378
	public bool stayInGround;

	// Token: 0x0400017B RID: 379
	public float speed = 10f;

	// Token: 0x0400017C RID: 380
	public float speed_random;

	// Token: 0x0400017D RID: 381
	public string terraformOption = string.Empty;

	// Token: 0x0400017E RID: 382
	public int terraformRange;

	// Token: 0x0400017F RID: 383
	public string endEffect;

	// Token: 0x04000180 RID: 384
	public bool playImpactSound;

	// Token: 0x04000181 RID: 385
	public string impactSoundID;

	// Token: 0x04000182 RID: 386
	public bool rotate;

	// Token: 0x04000183 RID: 387
	public bool look_at_target;

	// Token: 0x04000184 RID: 388
	public bool trailEffect_enabled;

	// Token: 0x04000185 RID: 389
	public string trailEffect_texture = "fireSmoke";

	// Token: 0x04000186 RID: 390
	public float trailEffect_scale = 0.25f;

	// Token: 0x04000187 RID: 391
	public float trailEffect_timer = 0.2f;

	// Token: 0x04000188 RID: 392
	public bool hitFreeze;

	// Token: 0x04000189 RID: 393
	public bool hitShake;

	// Token: 0x0400018A RID: 394
	internal Sprite[] _frames;

	// Token: 0x0400018B RID: 395
	public float startScale = 0.1f;

	// Token: 0x0400018C RID: 396
	public float targetScale = 0.1f;

	// Token: 0x0400018D RID: 397
	public bool parabolic;

	// Token: 0x0400018E RID: 398
	public string texture_shadow = string.Empty;

	// Token: 0x0400018F RID: 399
	public WorldAction world_actions;
}
