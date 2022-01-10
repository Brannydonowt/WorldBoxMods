using System;
using UnityEngine;

// Token: 0x02000086 RID: 134
public class Drop : BaseMapObject
{
	// Token: 0x060002EA RID: 746 RVA: 0x00031EFD File Offset: 0x000300FD
	private void Awake()
	{
		this.rnd = base.gameObject.GetComponent<SpriteRenderer>();
		this.sprite_animation = base.gameObject.GetComponent<SpriteAnimation>();
	}

	// Token: 0x060002EB RID: 747 RVA: 0x00031F24 File Offset: 0x00030124
	internal void launch(WorldTile pTile, DropAsset pAsset, float zDropHeight = -1f)
	{
		if (!this.created)
		{
			this.create();
		}
		if (this.soundOn && pAsset.sound_launch != string.Empty)
		{
			Sfx.play(pAsset.sound_launch, true, -1f, -1f);
		}
		this.soundOn = false;
		this.asset = pAsset;
		if (this.asset.action_launch != null)
		{
			this.asset.action_launch(null, null);
		}
		this.falling_speed = this.asset.fallingSpeed + Toolbox.randomFloat(0f, this.asset.fallingSpeedRandom);
		if (this.asset.spriteList == null || this.asset.spriteList.Length == 0)
		{
			this.asset.spriteList = Resources.LoadAll<Sprite>(this.asset.texture);
		}
		this.sprite_animation.setFrames(this.asset.spriteList);
		if (this.asset.random_flip)
		{
			this.rnd.flipX = Toolbox.randomBool();
		}
		if (this.asset.animated)
		{
			this.sprite_animation.isOn = true;
			this.sprite_animation.timeBetweenFrames = this.asset.animation_speed + Toolbox.randomFloat(0f, this.asset.animation_speed_random);
		}
		else
		{
			this.sprite_animation.isOn = false;
		}
		if (this.asset.random_frame)
		{
			this.sprite_animation.setRandomFrame();
		}
		this.sprite_animation.forceUpdateFrame();
		this.forceVector.Set(0f, 0f, 0f);
		this.currentTile = pTile;
		if (zDropHeight != -1f)
		{
			this.zPosition.z = zDropHeight;
		}
		else
		{
			this.zPosition.z = (float)((int)Random.Range(pAsset.fallingHeight.x, pAsset.fallingHeight.y));
		}
		this.currentPosition = new Vector3(pTile.posV3.x, pTile.posV3.y, 0f);
		this.updatePosition();
	}

	// Token: 0x060002EC RID: 748 RVA: 0x00032138 File Offset: 0x00030338
	public void setScale(Vector3 pVec)
	{
		this.m_transform.localScale = pVec;
		this._scale = pVec.x;
	}

	// Token: 0x060002ED RID: 749 RVA: 0x00032154 File Offset: 0x00030354
	private void updateFall(float pElapsed)
	{
		float num = 50f * pElapsed;
		if (this._scale < 1f)
		{
			num *= this.falling_speed / (this._scale * 2f);
		}
		else
		{
			num *= this.falling_speed;
		}
		if (this.zPosition.z < 0f)
		{
			num = 0f;
		}
		this.zPosition.z = this.zPosition.z - num * this._scale;
		if (num > 0f && this.asset.fallingRandomXMove && (double)Random.value > 0.5)
		{
			if ((double)Random.value > 0.5)
			{
				this.currentPosition.x = this.currentPosition.x - 1f * this._scale;
			}
			else
			{
				this.currentPosition.x = this.currentPosition.x + 1f * this._scale;
			}
		}
		this.updatePosition();
		if (this.zPosition.z <= 0f || (this.zPosition.z <= 0f && this.forceVector.z <= 0f))
		{
			this.zPosition.z = 0f;
			if (this.forceVector.z == 0f)
			{
				this.currentTile = this.world.GetTile((int)this.currentPosition.x, (int)this.currentPosition.y);
			}
			else
			{
				Vector3 localPosition = base.transform.localPosition;
				this.currentTile = this.world.GetTile((int)localPosition.x, (int)localPosition.y);
			}
			if (this.currentTile == null || this.currentTile.Type.liquid || !this.bounce)
			{
				this.land();
				return;
			}
			if (Mathf.Abs(this.forceVector.z) > 0.1f)
			{
				this.forceVector.z = -this.forceVector.z * 0.5f;
				this.forceVector.x = this.forceVector.x * 0.7f;
				this.forceVector.y = this.forceVector.y * 0.7f;
				this.initHeight *= 0.7f;
				this.forceVector.z = this.initHeight;
				return;
			}
			this.land();
		}
	}

	// Token: 0x060002EE RID: 750 RVA: 0x000323B0 File Offset: 0x000305B0
	private void land()
	{
		if (this.asset.action_landed != null && this.currentTile != null)
		{
			this.asset.action_landed(this.currentTile, this.asset.id);
		}
		if (this.asset.sound_drop != string.Empty && !this.burst)
		{
			Sfx.play(this.asset.sound_drop, true, -1f, -1f);
		}
		this.controller.landPixel(this);
	}

	// Token: 0x060002EF RID: 751 RVA: 0x0003243C File Offset: 0x0003063C
	protected void updateForce(float pElapsed)
	{
		if (this.forceVector.x == 0f && this.forceVector.y == 0f && this.forceVector.z == 0f)
		{
			return;
		}
		float num = pElapsed * 40f;
		float num2 = this.forceVector.z * num;
		if (this.forceVector.x != 0f)
		{
			this.currentPosition.x = this.currentPosition.x + this.forceVector.x * num;
		}
		if (this.forceVector.y != 0f)
		{
			this.currentPosition.y = this.currentPosition.y + this.forceVector.y * num;
		}
		this.zPosition.z = this.zPosition.z + num2;
		this.forceVector.z = this.forceVector.z - pElapsed * 5f;
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x00032518 File Offset: 0x00030718
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		this.sprite_animation.update(pElapsed);
		pElapsed *= 0.3f;
		this.updateForce(pElapsed);
		this.updateFall(pElapsed);
		this.updatePosition();
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x0003254A File Offset: 0x0003074A
	public void updatePosition()
	{
		this.m_transform.position = new Vector3(this.currentPosition.x, this.currentPosition.y + this.zPosition.z, 0f);
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x00032584 File Offset: 0x00030784
	public void addForce(float pX, float pY, float pZ)
	{
		this.forceVector.x = pX * 0.6f;
		this.forceVector.y = pY * 0.6f;
		this.forceVector.z = pZ * 2f;
		this.initHeight = this.forceVector.z;
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x000325D8 File Offset: 0x000307D8
	protected void findCurrentTile()
	{
		int num = Mathf.FloorToInt(base.transform.localPosition.x);
		int num2 = Mathf.FloorToInt(base.transform.localPosition.y);
		if (num >= MapBox.width)
		{
			num = MapBox.width - 1;
		}
		if (num2 >= MapBox.height)
		{
			num2 = MapBox.height - 1;
		}
		if (num < 0)
		{
			num = 0;
		}
		if (num2 < 0)
		{
			num2 = 0;
		}
		this.currentTile = this.world.GetTile(num, num2);
	}

	// Token: 0x0400048A RID: 1162
	public int drop_index;

	// Token: 0x0400048B RID: 1163
	public bool active;

	// Token: 0x0400048C RID: 1164
	internal DropManager controller;

	// Token: 0x0400048D RID: 1165
	public SpriteRenderer rnd;

	// Token: 0x0400048E RID: 1166
	public SpriteAnimation sprite_animation;

	// Token: 0x0400048F RID: 1167
	public Vector3 forceVector;

	// Token: 0x04000490 RID: 1168
	private float initHeight;

	// Token: 0x04000491 RID: 1169
	public TileType forcedTileType;

	// Token: 0x04000492 RID: 1170
	public DropAsset asset;

	// Token: 0x04000493 RID: 1171
	internal bool soundOn;

	// Token: 0x04000494 RID: 1172
	internal bool createGround;

	// Token: 0x04000495 RID: 1173
	internal bool bounce;

	// Token: 0x04000496 RID: 1174
	internal bool burst;

	// Token: 0x04000497 RID: 1175
	internal float falling_speed;

	// Token: 0x04000498 RID: 1176
	private float _scale = 1f;

	// Token: 0x04000499 RID: 1177
	private Vector3 prevPos = Vector3.zero;
}
