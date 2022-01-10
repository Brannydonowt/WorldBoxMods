using System;
using UnityEngine;

// Token: 0x0200011A RID: 282
public class SpriteShadow : MonoBehaviour
{
	// Token: 0x0600063D RID: 1597 RVA: 0x000498B0 File Offset: 0x00047AB0
	private void Start()
	{
		this.baseMapObject = base.GetComponent<BaseMapObject>();
		this.transCaster = base.transform;
		this.transShadow = new GameObject().transform;
		this.transShadow.parent = this.transCaster;
		this.transShadow.gameObject.name = "Shadow";
		this.transShadow.localRotation = Quaternion.identity;
		this.transShadow.localScale = new Vector3(1f, 0.5f);
		this.sprRndCaster = base.GetComponent<SpriteRenderer>();
		this.sprRndShadow = this.transShadow.gameObject.AddComponent<SpriteRenderer>();
		this.sprRndShadow.sharedMaterial = LibraryMaterials.instance.matWorldObjects;
		this.sprRndShadow.color = this.shadowColor;
		this.sprRndShadow.sortingLayerName = this.sprRndCaster.sortingLayerName;
		this.sprRndShadow.sortingOrder = this.sprRndCaster.sortingOrder - 1;
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x000499AC File Offset: 0x00047BAC
	private void LateUpdate()
	{
		this.transShadow.position = new Vector2(this.transCaster.position.x + this.offset.x, this.transCaster.position.y + this.offset.y);
		Color color = this.shadowColor;
		color.a = this.sprRndCaster.color.a * 0.5f;
		this.sprRndShadow.color = color;
		this.sprRndShadow.sprite = this.sprRndCaster.sprite;
		this.sprRndShadow.flipX = this.sprRndCaster.flipX;
	}

	// Token: 0x04000819 RID: 2073
	public Vector2 offset = new Vector2(-3f, 3f);

	// Token: 0x0400081A RID: 2074
	internal int z_height;

	// Token: 0x0400081B RID: 2075
	private SpriteRenderer sprRndCaster;

	// Token: 0x0400081C RID: 2076
	private SpriteRenderer sprRndShadow;

	// Token: 0x0400081D RID: 2077
	private Transform transCaster;

	// Token: 0x0400081E RID: 2078
	private Transform transShadow;

	// Token: 0x0400081F RID: 2079
	public Color shadowColor;

	// Token: 0x04000820 RID: 2080
	private BaseMapObject baseMapObject;
}
