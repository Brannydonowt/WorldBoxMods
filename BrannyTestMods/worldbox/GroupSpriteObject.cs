using System;
using UnityEngine;

// Token: 0x0200010B RID: 267
public class GroupSpriteObject : MonoBehaviour
{
	// Token: 0x060005F3 RID: 1523 RVA: 0x00047742 File Offset: 0x00045942
	private void Awake()
	{
		this.m_transform = base.gameObject.transform;
		this.m_renderer = base.gameObject.GetComponent<SpriteRenderer>();
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x00047768 File Offset: 0x00045968
	public void setPosOnly(ref Vector3 pPosition)
	{
		if (this.lastPos.x != pPosition.x || this.lastPos.y != pPosition.y || this.lastPos.z != pPosition.z)
		{
			this.lastPos.Set(pPosition.x, pPosition.y, pPosition.y);
			this.m_transform.localPosition = pPosition;
		}
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x000477DC File Offset: 0x000459DC
	public void setRotation(Vector3 pVec)
	{
		if (this.lastAngles.y != pVec.y || this.lastAngles.z != pVec.z)
		{
			this.lastAngles = pVec;
			this.m_transform.eulerAngles = pVec;
		}
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x00047817 File Offset: 0x00045A17
	public void setSprite(Sprite pSprite)
	{
		if (this.lastSpriteHashCode != pSprite.GetHashCode())
		{
			this.m_renderer.sprite = pSprite;
			this.lastSpriteHashCode = pSprite.GetHashCode();
		}
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x0004783F File Offset: 0x00045A3F
	public void setScale(Vector3 pVec)
	{
		if (this.lastScale.y != pVec.y)
		{
			this.lastScale = pVec;
			this.m_transform.localScale = pVec;
		}
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x00047868 File Offset: 0x00045A68
	public void setColor(ref Color pColor)
	{
		if (this.lastColor.r != pColor.r || this.lastColor.g != pColor.g || this.lastColor.b != pColor.b || this.lastColor.a != pColor.a)
		{
			this.m_renderer.color = pColor;
			this.lastColor = pColor;
		}
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x000478DE File Offset: 0x00045ADE
	public void setSharedMaterial(Material pMaterial)
	{
		if (this.m_renderer.sharedMaterial != pMaterial)
		{
			this.m_renderer.sharedMaterial = pMaterial;
		}
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x00047900 File Offset: 0x00045B00
	public void set(ref Vector2 pPosition, ref Vector3 pScale)
	{
		if (this.lastPos.x != pPosition.x || this.lastPos.y != pPosition.y)
		{
			this.lastPos = pPosition;
			this.m_transform.localPosition = pPosition;
		}
		if (this.lastScale.y != pScale.y || this.lastScale.x != pScale.x)
		{
			this.lastScale = pScale;
			this.m_transform.localScale = pScale;
		}
	}

	// Token: 0x040007D1 RID: 2001
	internal Transform m_transform;

	// Token: 0x040007D2 RID: 2002
	internal SpriteRenderer m_renderer;

	// Token: 0x040007D3 RID: 2003
	internal Vector3 lastPos;

	// Token: 0x040007D4 RID: 2004
	internal Vector3 lastScale;

	// Token: 0x040007D5 RID: 2005
	internal Vector3 lastAngles;

	// Token: 0x040007D6 RID: 2006
	internal Color lastColor;

	// Token: 0x040007D7 RID: 2007
	internal string lastTexture;

	// Token: 0x040007D8 RID: 2008
	internal int lastSpriteHashCode = -1;
}
