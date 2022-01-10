using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001CE RID: 462
public class SpriteAnimation : MonoBehaviour
{
	// Token: 0x06000A86 RID: 2694 RVA: 0x0006973E File Offset: 0x0006793E
	public void Awake()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		if (this.spriteRenderer == null)
		{
			this.image = base.GetComponent<Image>();
			this.useOnSpriteRenderer = false;
		}
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x0006976D File Offset: 0x0006796D
	public virtual void create()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.nextFrameTime = this.timeBetweenFrames;
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x00069787 File Offset: 0x00067987
	public void stopAnimations()
	{
		this.isOn = false;
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x00069790 File Offset: 0x00067990
	internal void setFrames(Sprite[] pFrames)
	{
		if (this.frames == pFrames)
		{
			return;
		}
		this.frames = pFrames;
		if (this.currentFrameIndex >= this.frames.Length)
		{
			this.currentFrameIndex = 0;
		}
		this.updateFrame();
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x000697C0 File Offset: 0x000679C0
	public bool isLastFrame()
	{
		return this.currentFrameIndex >= this.frames.Length - 1;
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x000697D7 File Offset: 0x000679D7
	public bool isFirstFrame()
	{
		return this.currentFrameIndex == 0;
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x000697E4 File Offset: 0x000679E4
	internal virtual void update(float pElapsed)
	{
		if (this.useNormalDeltaTime)
		{
			pElapsed = Time.deltaTime;
		}
		if (this.dirty)
		{
			this.dirty = false;
			this.forceUpdateFrame();
			return;
		}
		if (!this.isOn)
		{
			if (this.stopFrameTrigger)
			{
				this.stopFrameTrigger = false;
				this.updateFrame();
			}
			return;
		}
		if (MapBox.instance.isPaused() && !this.ignorePause)
		{
			return;
		}
		if (this.nextFrameTime > 0f)
		{
			this.nextFrameTime -= pElapsed;
			return;
		}
		this.nextFrameTime = this.timeBetweenFrames;
		if (this.playType == AnimPlayType.Forward)
		{
			if (this.currentFrameIndex + 1 >= this.frames.Length)
			{
				if (this.returnToPool)
				{
					base.GetComponent<BaseEffect>().kill();
					return;
				}
				if (!this.looped)
				{
					return;
				}
				this.currentFrameIndex = 0;
			}
			else
			{
				this.currentFrameIndex++;
			}
		}
		else if (this.currentFrameIndex - 1 < 0)
		{
			if (this.returnToPool)
			{
				base.GetComponent<BaseEffect>().kill();
				return;
			}
			if (!this.looped)
			{
				return;
			}
			this.currentFrameIndex = this.frames.Length - 1;
		}
		else
		{
			this.currentFrameIndex--;
		}
		this.updateFrame();
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x0006990F File Offset: 0x00067B0F
	public void stopAt(int pFrameId = 0, bool pNow = false)
	{
		this.isOn = false;
		this.currentFrameIndex = pFrameId;
		if (pNow)
		{
			this.updateFrame();
			return;
		}
		this.stopFrameTrigger = true;
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x00069930 File Offset: 0x00067B30
	public virtual void forceUpdateFrame()
	{
		if (this.frames.Length == 0)
		{
			return;
		}
		this.currentSpriteGraphic = this.frames[this.currentFrameIndex];
		this.applyCurrentSpriteGraphics(this.currentSpriteGraphic);
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x0006995B File Offset: 0x00067B5B
	public void setRandomFrame()
	{
		this.currentFrameIndex = Toolbox.randomInt(0, this.frames.Length);
		this.updateFrame();
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x00069977 File Offset: 0x00067B77
	public void setFrameIndex(int pFrame)
	{
		this.currentFrameIndex = pFrame;
		this.updateFrame();
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x00069988 File Offset: 0x00067B88
	protected virtual void updateFrame()
	{
		if (this.frames.Length == 0 || this.currentFrameIndex >= this.frames.Length || this.currentSpriteGraphic == this.frames[this.currentFrameIndex])
		{
			return;
		}
		this.currentSpriteGraphic = this.frames[this.currentFrameIndex];
		this.applyCurrentSpriteGraphics(this.currentSpriteGraphic);
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x000699E8 File Offset: 0x00067BE8
	internal virtual void applyCurrentSpriteGraphics(Sprite pSprite)
	{
		if (this.useOnSpriteRenderer)
		{
			this.spriteRenderer.sprite = pSprite;
			return;
		}
		this.image.sprite = pSprite;
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x00069A0B File Offset: 0x00067C0B
	public Sprite getCurrentGraphics()
	{
		return this.currentSpriteGraphic;
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x00069A13 File Offset: 0x00067C13
	public void resetAnim(int pFrameIndex = 0)
	{
		this.nextFrameTime = this.timeBetweenFrames;
		this.currentFrameIndex = pFrameIndex;
		this.updateFrame();
	}

	// Token: 0x04000D08 RID: 3336
	public bool ignorePause = true;

	// Token: 0x04000D09 RID: 3337
	public bool isOn = true;

	// Token: 0x04000D0A RID: 3338
	public float timeBetweenFrames = 0.1f;

	// Token: 0x04000D0B RID: 3339
	public float nextFrameTime;

	// Token: 0x04000D0C RID: 3340
	public bool useNormalDeltaTime;

	// Token: 0x04000D0D RID: 3341
	public AnimPlayType playType;

	// Token: 0x04000D0E RID: 3342
	public int currentFrameIndex;

	// Token: 0x04000D0F RID: 3343
	public bool looped = true;

	// Token: 0x04000D10 RID: 3344
	public bool returnToPool;

	// Token: 0x04000D11 RID: 3345
	public Sprite[] frames;

	// Token: 0x04000D12 RID: 3346
	public bool dirty;

	// Token: 0x04000D13 RID: 3347
	internal SpriteRenderer spriteRenderer;

	// Token: 0x04000D14 RID: 3348
	internal Image image;

	// Token: 0x04000D15 RID: 3349
	internal bool useOnSpriteRenderer = true;

	// Token: 0x04000D16 RID: 3350
	private bool stopFrameTrigger;

	// Token: 0x04000D17 RID: 3351
	internal Sprite currentSpriteGraphic;
}
