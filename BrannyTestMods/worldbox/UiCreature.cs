using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x0200026F RID: 623
public class UiCreature : MonoBehaviour
{
	// Token: 0x06000DC5 RID: 3525 RVA: 0x000825D7 File Offset: 0x000807D7
	private void Awake()
	{
		this.initScale = base.transform.localScale.x;
		this.initialPos = base.transform.localPosition;
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x00082600 File Offset: 0x00080800
	internal void resetPosition()
	{
		if (this.tweener_scale != null && this.tweener_scale.active)
		{
			TweenExtensions.Kill(this.tweener_scale, false);
		}
		if (this.tweener_fall != null && this.tweener_fall.active)
		{
			TweenExtensions.Kill(this.tweener_fall, false);
		}
		if (this.tweener_rotation != null && this.tweener_rotation.active)
		{
			TweenExtensions.Kill(this.tweener_rotation, false);
		}
		if (this.tweener_move != null && this.tweener_move.active)
		{
			TweenExtensions.Kill(this.tweener_move, false);
		}
		this.dropped = false;
		base.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
		base.transform.localPosition = this.initialPos;
		base.transform.localScale = new Vector3(this.initScale, this.initScale, 1f);
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x000826FC File Offset: 0x000808FC
	public void click()
	{
		DOTween.Kill(this, false);
		if (base.GetComponent<HoveringIcon>() != null)
		{
			base.GetComponent<HoveringIcon>().enabled = false;
		}
		if (base.GetComponent<LivingIcon>() != null)
		{
			base.GetComponent<LivingIcon>().kill();
		}
		if (!string.IsNullOrEmpty(this.achievement))
		{
			AchievementLibrary.unlock(this.achievement);
		}
		if (this.dropped)
		{
			return;
		}
		if (this.doPlayPunch)
		{
			Sfx.play("punch", true, -1f, -1f);
		}
		if (this.doSfx != "none" && this.doSfx != "" && this.doSfx != string.Empty)
		{
			Sfx.play(this.doSfx, true, -1f, -1f);
		}
		if (this.doScale)
		{
			if (this.tweener_scale != null && this.tweener_scale.active)
			{
				TweenExtensions.Kill(this.tweener_scale, false);
			}
			float num = this.initScale * 1.2f;
			base.transform.localScale = new Vector3(num, num, num);
			this.tweener_scale = TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.transform, new Vector3(this.initScale, this.initScale, this.initScale), 0.3f), 27);
		}
		if (this.doFall)
		{
			this.fall();
		}
		if (this.doFly)
		{
			this.flyAway();
		}
		if (this.doRotate)
		{
			if (Toolbox.randomBool())
			{
				this.tweener_rotation = TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(ShortcutExtensions.DORotate(base.transform, new Vector3(0f, 0f, Toolbox.randomFloat(90f, 180f)), 1f, 0), 9);
				return;
			}
			this.tweener_rotation = TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(ShortcutExtensions.DORotate(base.transform, new Vector3(0f, 0f, Toolbox.randomFloat(-180f, -90f)), 1f, 0), 9);
		}
	}

	// Token: 0x06000DC8 RID: 3528 RVA: 0x000828F4 File Offset: 0x00080AF4
	private void flyAway()
	{
		this.dropped = true;
		if (this.changeParent)
		{
			base.transform.parent = CanvasMain.instance.canvas_tooltip.transform;
		}
		Vector3 vector = new Vector3(base.transform.position.x + Toolbox.randomFloat(-200f, 200f), 1000f, 0f);
		this.tweener_move = TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOMove(base.transform, vector, 0.6f, false), 5), new TweenCallback(this.completeFly));
	}

	// Token: 0x06000DC9 RID: 3529 RVA: 0x0008298C File Offset: 0x00080B8C
	private void fall()
	{
		this.dropped = true;
		if (this.changeParent)
		{
			base.transform.SetParent(CanvasMain.instance.canvas_tooltip.transform);
		}
		Vector3 vector = new Vector3(base.transform.position.x + Toolbox.randomFloat(-4f, 4f), -base.GetComponent<RectTransform>().sizeDelta.y, 0f);
		this.tweener_move = TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOMove(base.transform, vector, 0.6f, false), 5), new TweenCallback(this.completeFall));
	}

	// Token: 0x06000DCA RID: 3530 RVA: 0x00082A2E File Offset: 0x00080C2E
	private void completeFly()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x00082A3C File Offset: 0x00080C3C
	private void completeFall()
	{
		Sfx.play("fallingSand", true, -1f, -1f);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001086 RID: 4230
	public bool doFall;

	// Token: 0x04001087 RID: 4231
	public bool doRotate;

	// Token: 0x04001088 RID: 4232
	public bool doScale = true;

	// Token: 0x04001089 RID: 4233
	public bool doFly;

	// Token: 0x0400108A RID: 4234
	public bool doPlayPunch;

	// Token: 0x0400108B RID: 4235
	public bool changeParent = true;

	// Token: 0x0400108C RID: 4236
	public string doSfx = "none";

	// Token: 0x0400108D RID: 4237
	private Tweener tweener_scale;

	// Token: 0x0400108E RID: 4238
	private Tweener tweener_fall;

	// Token: 0x0400108F RID: 4239
	private Tweener tweener_rotation;

	// Token: 0x04001090 RID: 4240
	private Tweener tweener_move;

	// Token: 0x04001091 RID: 4241
	private float initScale = 1f;

	// Token: 0x04001092 RID: 4242
	internal bool dropped;

	// Token: 0x04001093 RID: 4243
	public string achievement = "";

	// Token: 0x04001094 RID: 4244
	internal Vector3 initialPos;
}
