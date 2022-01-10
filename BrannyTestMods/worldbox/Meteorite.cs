using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200010F RID: 271
public class Meteorite : BaseMapObject
{
	// Token: 0x0600060C RID: 1548 RVA: 0x0004818E File Offset: 0x0004638E
	private void Awake()
	{
		this.shadowRenderer = this.shadow.GetComponent<SpriteRenderer>();
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x000481A1 File Offset: 0x000463A1
	internal override void create()
	{
		base.create();
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x000481AC File Offset: 0x000463AC
	public void spawnOn(WorldTile pTile)
	{
		base.setWorld();
		this.tile = pTile;
		this.radius = 20;
		base.transform.position = new Vector3(pTile.posV3.x, pTile.posV3.y, (float)pTile.y);
		Vector3 end = new Vector3(0f, 0f);
		base.StartCoroutine(this.SmoothMovement(end));
		this.setAlpha(0f);
		Sfx.play("meteorite fall", true, -1f, -1f);
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x0004823A File Offset: 0x0004643A
	protected IEnumerator SmoothMovement(Vector3 end)
	{
		Vector3 position = base.transform.position;
		position.z = 0f;
		float sqrRemainingDistance = (position - end).sqrMagnitude;
		while (sqrRemainingDistance > 1E-45f)
		{
			Vector3 vector = Vector3.MoveTowards(this.mainSprite.transform.localPosition, end, this.fallingSpeed * this.world.elapsed);
			this.mainSprite.transform.localPosition = vector;
			this.shadow.transform.localPosition = new Vector3(vector.x, this.shadow.transform.localPosition.y);
			sqrRemainingDistance = (this.mainSprite.transform.localPosition - end).sqrMagnitude;
			yield return null;
		}
		this.explode();
		yield break;
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x00048250 File Offset: 0x00046450
	private void Update()
	{
		this.alpha += this.world.getCurElapsed() * 0.2f;
		this.setAlpha(this.alpha);
		this.mainSprite.transform.Rotate(this.rotationSpeed * this.world.elapsed);
		this.shadow.transform.Rotate(this.rotationSpeed * this.world.elapsed);
		if (this.timerSmoke > 0f)
		{
			this.timerSmoke -= this.world.getCurElapsed();
			return;
		}
		this.world.stackEffects.get("fireSmoke").spawnAt(this.mainSprite.transform.position, 1f);
		this.timerSmoke = 0.05f;
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x00048334 File Offset: 0x00046534
	protected void setAlpha(float pVal)
	{
		this.alpha = pVal;
		if (this.alpha < 0f)
		{
			this.alpha = 0f;
		}
		Color color = this.shadowRenderer.color;
		color.a = this.alpha;
		this.shadowRenderer.color = color;
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x00048388 File Offset: 0x00046588
	private void explode()
	{
		this.world.gameStats.data.meteoritesLaunched++;
		MapAction.damageWorld(this.tile, this.radius, AssetManager.terraform.get("bomb"));
		Object.Destroy(base.gameObject);
		this.world.spawnFlash(this.tile, this.radius);
		Vector3 pVector = new Vector3((float)this.tile.pos.x, (float)(this.tile.pos.y - 2));
		float pScale = Toolbox.randomFloat(0.8f, 0.9f);
		this.world.stackEffects.get("explosionSmall").spawnAt(pVector, pScale);
		Sfx.play("explosion meteorite", true, -1f, -1f);
	}

	// Token: 0x040007E8 RID: 2024
	public SpriteRenderer mainRenderer;

	// Token: 0x040007E9 RID: 2025
	private SpriteRenderer shadowRenderer;

	// Token: 0x040007EA RID: 2026
	public Vector3 rotationSpeed = new Vector3(0f, 0f, 50f);

	// Token: 0x040007EB RID: 2027
	private float fallingSpeed = 200f;

	// Token: 0x040007EC RID: 2028
	public GameObject mainSprite;

	// Token: 0x040007ED RID: 2029
	public GameObject shadow;

	// Token: 0x040007EE RID: 2030
	private WorldTile tile;

	// Token: 0x040007EF RID: 2031
	private int radius;

	// Token: 0x040007F0 RID: 2032
	private float alpha;

	// Token: 0x040007F1 RID: 2033
	private float timerSmoke = 0.01f;
}
