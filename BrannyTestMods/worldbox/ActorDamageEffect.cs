using System;
using UnityEngine;

// Token: 0x020000FB RID: 251
public class ActorDamageEffect : BaseMapObject
{
	// Token: 0x0600058D RID: 1421 RVA: 0x000451EE File Offset: 0x000433EE
	private void Awake()
	{
		this.sprRnd = base.gameObject.GetComponent<SpriteRenderer>();
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x00045204 File Offset: 0x00043404
	private void Start()
	{
		this.spriteAnimation = base.gameObject.transform.parent.GetComponent<SpriteAnimation>();
		this.actor = base.gameObject.transform.parent.GetComponent<Actor>();
		this.sprRnd.sortingLayerID = this.actor.spriteRenderer.sortingLayerID;
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x0004526E File Offset: 0x0004346E
	public void start()
	{
		base.gameObject.SetActive(true);
		this.alpha = 1f;
		this.sprRnd.color = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x000452AC File Offset: 0x000434AC
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this.spriteAnimation == null)
		{
			return;
		}
		this.sprRnd.sprite = this.spriteAnimation.getCurrentGraphics();
		this.sprRnd.sortingOrder = this.actor.spriteRenderer.sortingOrder + 1;
		this.sprRnd.flipX = this.actor.spriteRenderer.flipX;
		this.alpha -= pElapsed * 2f;
		if (this.alpha < 0f)
		{
			this.alpha = 0f;
		}
		this.sprRnd.color = new Color(1f, 1f, 1f, this.alpha);
		if (this.alpha < 0f)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x04000782 RID: 1922
	private float alpha = 1f;

	// Token: 0x04000783 RID: 1923
	protected SpriteRenderer sprRnd;

	// Token: 0x04000784 RID: 1924
	protected Texture2D texture;

	// Token: 0x04000785 RID: 1925
	private SpriteAnimation spriteAnimation;

	// Token: 0x04000786 RID: 1926
	private Actor actor;
}
