using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200019D RID: 413
public class MapObjectShadow : MonoBehaviour
{
	// Token: 0x06000973 RID: 2419 RVA: 0x00063E87 File Offset: 0x00062087
	public void create()
	{
		this.m_gameObject = base.gameObject;
		this.m_transform = this.m_gameObject.transform;
		this.spriteRenderer = base.gameObject.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x00063EB8 File Offset: 0x000620B8
	public void setShadow(string pShadowID, bool pBuilding = false)
	{
		MapObjectShadow.createDict();
		this.spriteRenderer = base.gameObject.GetComponent<SpriteRenderer>();
		if (!MapObjectShadow.shadowSprites.ContainsKey(pShadowID))
		{
			MapObjectShadow.addSprite(pShadowID);
		}
		this.m_gameObject = base.gameObject;
		this.m_transform = this.m_gameObject.transform;
		this.m_transform.localScale = Vector3.zero;
		this.spriteRenderer.sprite = MapObjectShadow.shadowSprites[pShadowID];
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x00063F34 File Offset: 0x00062134
	private static void createDict()
	{
		if (MapObjectShadow.shadowSprites != null)
		{
			return;
		}
		MapObjectShadow.shadowSprites = new Dictionary<string, Sprite>();
		foreach (Sprite sprite in Resources.LoadAll<Sprite>("shadows/buildingShadows"))
		{
			MapObjectShadow.shadowSprites.Add(sprite.name, sprite);
		}
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x00063F84 File Offset: 0x00062184
	private static void addSprite(string pID)
	{
		Sprite sprite = Resources.Load<Sprite>("shadows/" + pID);
		if (sprite == null)
		{
			Debug.LogError("Shadow not found for " + pID);
		}
		MapObjectShadow.shadowSprites.Add(pID, sprite);
	}

	// Token: 0x04000C12 RID: 3090
	private static Dictionary<string, Sprite> shadowSprites;

	// Token: 0x04000C13 RID: 3091
	internal SpriteRenderer spriteRenderer;

	// Token: 0x04000C14 RID: 3092
	internal Transform m_transform;

	// Token: 0x04000C15 RID: 3093
	internal GameObject m_gameObject;

	// Token: 0x04000C16 RID: 3094
	public ActorBase actor;
}
