using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000101 RID: 257
public class Cloud : BaseEffect
{
	// Token: 0x060005BC RID: 1468 RVA: 0x00045CD4 File Offset: 0x00043ED4
	internal override void create()
	{
		base.create();
		this.timerFallingPixels = new WorldTimer(0.05f, new Action(this.cloudAction));
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x00045CF8 File Offset: 0x00043EF8
	internal override void prepare()
	{
		this.type = CloudType.normal;
		this.sprRenderer.sprite = Toolbox.getRandom<Sprite>(this.cloudSprites);
		this.sprRenderer.flipX = Toolbox.randomBool();
		this.speed = Toolbox.randomFloat(1f, this.speedMax);
		base.prepare();
		base.setAlpha(0f);
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x00045D5C File Offset: 0x00043F5C
	private void cloudAction()
	{
		if (this.type == CloudType.normal)
		{
			return;
		}
		if (this.alpha < 0.1f)
		{
			return;
		}
		float num = this.sprRenderer.sprite.textureRect.width * 0.08f;
		float num2 = this.sprRenderer.sprite.textureRect.height * 0.04f;
		int num3 = (int)base.transform.localPosition.x;
		int num4 = (int)base.transform.localPosition.y;
		num3 += (int)Toolbox.randomFloat(-num, num);
		num4 += (int)Toolbox.randomFloat(-num2 + this.spriteShadow.offset.y, num2 + this.spriteShadow.offset.y);
		WorldTile tile = this.world.GetTile(num3, num4);
		if (tile == null)
		{
			return;
		}
		this.world.dropManager.spawn(tile, this.dropID, 10f, -1f);
		if (this.type == CloudType.rain && Toolbox.randomChance(0.01f))
		{
			num3 += (int)Toolbox.randomFloat(num * 0.5f, num);
			num4 += (int)Toolbox.randomFloat(-num2 + this.spriteShadow.offset.y, num2 + this.spriteShadow.offset.y);
			tile = this.world.GetTile(num3, num4);
			if (tile != null)
			{
				MapBox.spawnLightning(tile, 0.15f);
			}
		}
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x00045EC8 File Offset: 0x000440C8
	internal void setType(string pType)
	{
		if (pType == "cloudRain")
		{
			Sfx.play(pType, true, -1f, -1f);
			this.type = CloudType.rain;
			this.sprRenderer.color = this.colorRain;
			this.maxAlpha = 0.8f;
			this.dropID = "rain";
			return;
		}
		if (pType == "cloudAcid")
		{
			Sfx.play(pType, true, -1f, -1f);
			this.type = CloudType.acid;
			this.sprRenderer.color = this.colorAcid;
			this.maxAlpha = 0.8f;
			this.dropID = "acid";
			return;
		}
		if (pType == "cloudLava")
		{
			Sfx.play(pType, true, -1f, -1f);
			this.type = CloudType.lava;
			this.sprRenderer.color = this.colorLava;
			this.maxAlpha = 0.8f;
			this.dropID = "lava";
			return;
		}
		if (pType == "cloudSnow")
		{
			Sfx.play(pType, true, -1f, -1f);
			this.type = CloudType.snow;
			this.sprRenderer.color = this.colorSnow;
			this.maxAlpha = 0.8f;
			this.dropID = "snow";
			return;
		}
		this.type = CloudType.normal;
		this.sprRenderer.color = this.colorNormal;
		this.maxAlpha = 0.5f;
		this.dropID = null;
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x00046033 File Offset: 0x00044233
	internal void prepare(Vector3 pVec, string pType)
	{
		this.prepare();
		pVec.y -= this.spriteShadow.offset.y;
		this.setType(pType);
		base.transform.localPosition = pVec;
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x0004606C File Offset: 0x0004426C
	internal override void prepare(WorldTile tile, float pScale = 0.5f)
	{
		this.prepare();
		base.transform.localPosition = new Vector3(tile.posV3.x, tile.posV3.y);
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x0004609C File Offset: 0x0004429C
	public override void update(float pElapsed)
	{
		if (Config.timeScale > 10f)
		{
			this.fadeMultiplier = 0.05f;
		}
		else
		{
			this.fadeMultiplier = 0.2f;
		}
		if (!this.world.isPaused())
		{
			base.transform.Translate(this.speed * pElapsed, 0f, 0f);
			this.timerFallingPixels.update(pElapsed);
		}
		if (base.transform.position.x > (float)MapBox.width)
		{
			base.startToDie();
		}
		float num = this.maxAlpha;
		if (Camera.main != null && Camera.main.orthographicSize > 0f)
		{
			num *= Camera.main.orthographicSize / 100f;
			if (num > this.maxAlpha)
			{
				num = this.maxAlpha;
			}
		}
		int state = this.state;
		if (state == 1)
		{
			if (this.alpha < num)
			{
				this.alpha += pElapsed * this.fadeMultiplier;
				if (this.alpha >= num)
				{
					this.alpha = num;
				}
			}
			else if (this.alpha > num)
			{
				this.alpha -= pElapsed * this.fadeMultiplier;
				if (this.alpha <= num)
				{
					this.alpha = num;
				}
			}
			else
			{
				this.alpha = num;
			}
			base.setAlpha(this.alpha);
			return;
		}
		if (state != 2)
		{
			return;
		}
		if (this.alpha > 0f)
		{
			this.alpha -= pElapsed * this.fadeMultiplier;
		}
		else
		{
			this.alpha = 0f;
			this.controller.killObject(this);
		}
		base.setAlpha(this.alpha);
	}

	// Token: 0x040007A6 RID: 1958
	public Color colorNormal;

	// Token: 0x040007A7 RID: 1959
	public Color colorRain;

	// Token: 0x040007A8 RID: 1960
	public Color colorLava;

	// Token: 0x040007A9 RID: 1961
	public Color colorAcid;

	// Token: 0x040007AA RID: 1962
	public Color colorSnow;

	// Token: 0x040007AB RID: 1963
	public List<Sprite> cloudSprites;

	// Token: 0x040007AC RID: 1964
	private float maxAlpha = 0.3f;

	// Token: 0x040007AD RID: 1965
	private float speedMax = 6f;

	// Token: 0x040007AE RID: 1966
	private float speed = 1f;

	// Token: 0x040007AF RID: 1967
	public SpriteShadow spriteShadow;

	// Token: 0x040007B0 RID: 1968
	private CloudType type;

	// Token: 0x040007B1 RID: 1969
	private WorldTimer timerFallingPixels;

	// Token: 0x040007B2 RID: 1970
	private string dropID;

	// Token: 0x040007B3 RID: 1971
	private float fadeMultiplier = 0.2f;
}
