using System;
using UnityEngine;

// Token: 0x0200006E RID: 110
public class ActiveStatusEffect : SpriteAnimation
{
	// Token: 0x0600023A RID: 570 RVA: 0x0002A23B File Offset: 0x0002843B
	internal void setStatus(StatusEffect pAsset, BaseSimObject pObject)
	{
		this.asset = pAsset;
		this.simObject = pObject;
		this.actionTimer = this.asset.actionInterval;
		this.timer = this.asset.duration;
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0002A270 File Offset: 0x00028470
	internal void setZFlip(float pZ)
	{
		this.curZ = pZ;
		Vector3 localPosition = base.transform.localPosition;
		localPosition.z = pZ;
		base.transform.localPosition = localPosition;
	}

	// Token: 0x0600023C RID: 572 RVA: 0x0002A2A4 File Offset: 0x000284A4
	public void updateZFlip()
	{
		if (this.simObject.isActor())
		{
			if (this.simObject.a.flip)
			{
				if (this.curZ != 0.01f)
				{
					this.setZFlip(0.01f);
					return;
				}
			}
			else if (this.curZ != -0.01f)
			{
				this.setZFlip(-0.01f);
			}
		}
	}

	// Token: 0x0600023D RID: 573 RVA: 0x0002A304 File Offset: 0x00028504
	internal override void update(float pElapsed)
	{
		base.update(pElapsed);
		this.updateZFlip();
		if (this.simObject.isActor())
		{
			this.spriteRenderer.enabled = this.simObject.a._is_visible;
		}
		else
		{
			this.spriteRenderer.enabled = this.simObject.b._is_visible;
		}
		if (!this.finished)
		{
			if (this.asset.actionInterval != 0f)
			{
				if (this.actionTimer > 0f)
				{
					this.actionTimer -= pElapsed;
				}
				else
				{
					this.actionTimer = this.asset.actionInterval;
					if (this.asset.action != null)
					{
						this.asset.action(this.simObject, this.simObject.currentTile);
					}
				}
			}
			if (this.timer > 0f)
			{
				this.timer -= pElapsed;
				return;
			}
			this.finished = true;
		}
	}

	// Token: 0x040002FB RID: 763
	public float actionTimer;

	// Token: 0x040002FC RID: 764
	public float timer;

	// Token: 0x040002FD RID: 765
	public StatusEffect asset;

	// Token: 0x040002FE RID: 766
	public bool finished;

	// Token: 0x040002FF RID: 767
	internal BaseSimObject simObject;

	// Token: 0x04000300 RID: 768
	private float curZ;
}
