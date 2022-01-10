using System;
using UnityEngine;

// Token: 0x020000FD RID: 253
public class BaseEffect : BaseAnimatedObject
{
	// Token: 0x06000598 RID: 1432 RVA: 0x0004549F File Offset: 0x0004369F
	private new void Awake()
	{
		this.sprRenderer = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x000454AD File Offset: 0x000436AD
	internal void makeParentController()
	{
		base.transform.SetParent(this.controller.transform, true);
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x000454C8 File Offset: 0x000436C8
	internal virtual void prepare(WorldTile tile, float pScale = 0.5f)
	{
		this.state = 1;
		float z = 0f;
		if (this.autoYZ)
		{
			z = (float)tile.pos.y;
		}
		base.transform.localEulerAngles = Vector3.zero;
		base.transform.localPosition = new Vector3((float)tile.pos.x + 0.5f, (float)tile.pos.y, z);
		this.setScale(pScale);
		this.setAlpha(1f);
		if (base.GetComponent<SpriteAnimation>() != null)
		{
			base.GetComponent<SpriteAnimation>().resetAnim(0);
		}
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x0004556B File Offset: 0x0004376B
	public void setScale(float pScale)
	{
		this.scale = pScale;
		if (this.scale < 0f)
		{
			this.scale = 0f;
		}
		base.transform.localScale = new Vector3(pScale, pScale);
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x000455A0 File Offset: 0x000437A0
	internal virtual void prepare(Vector3 pVector, float pScale = 1f)
	{
		this.state = 1;
		float z = 0f;
		if (this.autoYZ)
		{
			z = pVector.y;
		}
		base.transform.rotation = Quaternion.identity;
		base.transform.localPosition = new Vector3(pVector.x, pVector.y, z);
		this.setScale(pScale);
		this.setAlpha(1f);
		if (base.GetComponent<SpriteAnimation>() != null)
		{
			base.GetComponent<SpriteAnimation>().resetAnim(0);
		}
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x00045624 File Offset: 0x00043824
	protected void setAlpha(float pVal)
	{
		this.alpha = pVal;
		if (this.sprRenderer == null)
		{
			this.sprRenderer = base.GetComponent<SpriteRenderer>();
		}
		Color color = this.sprRenderer.color;
		color.a = this.alpha;
		this.sprRenderer.color = color;
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x00045677 File Offset: 0x00043877
	internal virtual void prepare()
	{
		base.transform.position = new Vector3((float)Random.Range(-50, 30), (float)Random.Range(0, MapBox.height));
		this.state = 1;
		this.setAlpha(0f);
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x000456B1 File Offset: 0x000438B1
	internal void setTile(WorldTile pTile)
	{
		this.tile = pTile;
		base.transform.localPosition = new Vector3(pTile.posV3.x, pTile.posV3.y);
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x000456E0 File Offset: 0x000438E0
	internal void startToDie()
	{
		this.state = 2;
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x000456E9 File Offset: 0x000438E9
	public void kill()
	{
		this.state = 3;
		this.controller.killObject(this);
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x000456FE File Offset: 0x000438FE
	public void clear()
	{
		this.callback = null;
		this.callbackOnFrame = -1;
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x0004570E File Offset: 0x0004390E
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		if (this.callbackOnFrame != -1 && this.spriteAnimation.currentFrameIndex == this.callbackOnFrame)
		{
			this.callback();
			this.clear();
		}
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x00045744 File Offset: 0x00043944
	public void setCallback(int pFrame, BaseCallback pCallback)
	{
		this.callbackOnFrame = pFrame;
		this.callback = pCallback;
	}

	// Token: 0x04000788 RID: 1928
	internal bool active;

	// Token: 0x04000789 RID: 1929
	internal int effectIndex;

	// Token: 0x0400078A RID: 1930
	public const int STATE_START = 1;

	// Token: 0x0400078B RID: 1931
	public const int STATE_ON_DEATH = 2;

	// Token: 0x0400078C RID: 1932
	public const int STATE_KILLED = 3;

	// Token: 0x0400078D RID: 1933
	protected float scale;

	// Token: 0x0400078E RID: 1934
	protected float alpha;

	// Token: 0x0400078F RID: 1935
	public WorldTile tile;

	// Token: 0x04000790 RID: 1936
	internal BaseEffectController controller;

	// Token: 0x04000791 RID: 1937
	internal int state;

	// Token: 0x04000792 RID: 1938
	public SpriteRenderer sprRenderer;

	// Token: 0x04000793 RID: 1939
	public bool autoYZ;

	// Token: 0x04000794 RID: 1940
	internal BaseCallback callback;

	// Token: 0x04000795 RID: 1941
	internal int callbackOnFrame = -1;
}
