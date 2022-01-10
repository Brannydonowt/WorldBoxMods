using System;
using UnityEngine;

// Token: 0x02000029 RID: 41
public class DropAsset : Asset
{
	// Token: 0x040000EF RID: 239
	public bool random_frame;

	// Token: 0x040000F0 RID: 240
	public bool random_flip;

	// Token: 0x040000F1 RID: 241
	public bool animated;

	// Token: 0x040000F2 RID: 242
	public float animation_speed = 0.1f;

	// Token: 0x040000F3 RID: 243
	public float animation_speed_random = 0.1f;

	// Token: 0x040000F4 RID: 244
	public string sound_drop = string.Empty;

	// Token: 0x040000F5 RID: 245
	public string sound_launch = string.Empty;

	// Token: 0x040000F6 RID: 246
	public DropsAction action_launch;

	// Token: 0x040000F7 RID: 247
	public DropsAction action_landed;

	// Token: 0x040000F8 RID: 248
	public string powerOnLanding;

	// Token: 0x040000F9 RID: 249
	public string constructionTemplate;

	// Token: 0x040000FA RID: 250
	public string constructionTemplateSound;

	// Token: 0x040000FB RID: 251
	public bool bounce;

	// Token: 0x040000FC RID: 252
	public float fallingSpeed = 3.2f;

	// Token: 0x040000FD RID: 253
	public float fallingSpeedRandom = 0.5f;

	// Token: 0x040000FE RID: 254
	public Vector3 fallingHeight = new Vector2(15f, 20f);

	// Token: 0x040000FF RID: 255
	public bool fallingRandomXMove;

	// Token: 0x04000100 RID: 256
	public float particleInterval;

	// Token: 0x04000101 RID: 257
	public string texture = "drops/drop_pixel";

	// Token: 0x04000102 RID: 258
	public float default_scale = 1f;

	// Token: 0x04000103 RID: 259
	public Sprite[] spriteList;
}
