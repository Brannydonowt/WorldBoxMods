using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200028D RID: 653
public class UnitAvatarLoader : MonoBehaviour
{
	// Token: 0x06000E6A RID: 3690 RVA: 0x000860FC File Offset: 0x000842FC
	public void load(Actor pActor)
	{
		if (!pActor.stats.specialAnimation)
		{
			pActor.updateAnimation(0f, true);
			pActor.spriteAnimation.setFrameIndex(0);
		}
		while (base.transform.childCount > 0)
		{
			Transform child = base.transform.GetChild(0);
			child.SetParent(null);
			Object.Destroy(child.gameObject);
		}
		base.transform.localScale = new Vector3(pActor.stats.inspectAvatarScale * this.avatarSize, pActor.stats.inspectAvatarScale * this.avatarSize, pActor.stats.inspectAvatarScale);
		pActor.forceAnimation();
		pActor.checkSpriteConstructor();
		this.showSpritePart(pActor.spriteRenderer.sprite, pActor, new Vector3(0f, 0f));
		if (pActor.s_item_sprite != null)
		{
			AnimationFrameData animationFrameData = pActor.getAnimationFrameData();
			Vector3 pPos = default(Vector3);
			pPos.x = animationFrameData.posItem.x;
			pPos.y = animationFrameData.posItem.y;
			this.showSpritePart(pActor.s_item_sprite, pActor, pPos);
		}
	}

	// Token: 0x06000E6B RID: 3691 RVA: 0x0008621C File Offset: 0x0008441C
	private void showSpritePart(Sprite pSprite, Actor pActor, Vector3 pPos)
	{
		GameObject gameObject = new GameObject();
		Image image = gameObject.AddComponent<Image>();
		image.sprite = pSprite;
		image.rectTransform.sizeDelta = new Vector2(image.sprite.textureRect.width, image.sprite.textureRect.height);
		gameObject.transform.SetParent(base.transform);
		image.rectTransform.SetAnchor(AnchorPresets.BottonCenter, 0f, 0f);
		float x = image.sprite.pivot.x / image.sprite.textureRect.width;
		float y = image.sprite.pivot.y / image.sprite.textureRect.height;
		image.rectTransform.pivot = new Vector2(x, y);
		image.rectTransform.anchoredPosition = pPos;
		gameObject.transform.localScale = Vector3.one;
	}

	// Token: 0x04001139 RID: 4409
	public float avatarSize = 1f;
}
