using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E6 RID: 486
public class RewardAnimation : MonoBehaviour
{
	// Token: 0x06000B11 RID: 2833 RVA: 0x0006C9F4 File Offset: 0x0006ABF4
	private void Awake()
	{
		this._rotationAnimation = this.boxSprite.GetComponent<IconRotationAnimation>();
		this.spriteAnimation = this.boxSprite.GetComponent<SpriteAnimation>();
		this.spriteAnimation.Awake();
		if (this.originalPos == Vector3.zero)
		{
			this.originalPos = this.rewardedPowerIcon.transform.localPosition;
		}
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x0006CA58 File Offset: 0x0006AC58
	public void OnEnable()
	{
		if (this.originalPos == Vector3.zero)
		{
			this.originalPos = this.rewardedPowerIcon.transform.localPosition;
		}
		this.bottomButtonText.key = "free_power_button_open_in";
		this.bottomButtonText.updateText(true);
		this.resetAnim();
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x0006CAAF File Offset: 0x0006ACAF
	private void OnDisable()
	{
		ShortcutExtensions.DOKill(this.rewardedPowerIcon.transform, false);
		ShortcutExtensions.DOKill(this.rewardTexts.transform, false);
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x0006CAD8 File Offset: 0x0006ACD8
	public void resetAnim()
	{
		this.state = RewardAnimationState.Idle;
		this.spriteAnimation.resetAnim(3);
		this._rotationAnimation.enabled = true;
		ShortcutExtensions.DOKill(this.rewardedPowerIcon.transform, false);
		this.rewardedPowerIcon.SetActive(false);
		this.rewardTexts.SetActive(false);
		this.Text_free_power_unlocked.gameObject.SetActive(false);
		this.Text_free_power_tap_to_unlock.gameObject.SetActive(true);
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x0006CB50 File Offset: 0x0006AD50
	private void Update()
	{
		if (this.quickReward && this.spriteAnimation.currentFrameIndex < 7)
		{
			this.spriteAnimation.currentFrameIndex = 7;
			this.showRewards(false);
			this.moveStageThree();
		}
		if (this.state == RewardAnimationState.Play || this.state == RewardAnimationState.Open)
		{
			this.spriteAnimation.update(Time.deltaTime);
			if (this.spriteAnimation.currentFrameIndex > 6 && this.state != RewardAnimationState.Open)
			{
				this.showRewards(true);
			}
		}
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x0006CBCC File Offset: 0x0006ADCC
	private void showRewards(bool pStart = true)
	{
		this.state = RewardAnimationState.Open;
		this.rewardedPowerIcon.SetActive(true);
		ShortcutExtensions.DOKill(this.rewardTexts.transform, false);
		this.rewardTexts.gameObject.transform.localScale = new Vector3(0.5f, 0.5f);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.rewardTexts.transform, new Vector3(1f, 1f, 1f), 0.3f), 27);
		this.rewardTexts.gameObject.SetActive(true);
		this.Text_free_power_unlocked.gameObject.SetActive(true);
		this.Text_free_power_tap_to_unlock.gameObject.SetActive(false);
		this.bottomButtonText.key = "get_it";
		this.bottomButtonText.updateText(true);
		ShortcutExtensions.DOKill(this.rewardedPowerIcon.transform, false);
		this.rewardedPowerIcon.transform.localPosition = this.originalPos;
		this.rewardedPowerIcon.transform.localScale = new Vector3(0.02f, 0.1f, 1f);
		if (pStart)
		{
			Vector3 vector = new Vector3(this.originalPos.x, this.originalPos.y, 0f);
			vector.y += 22f;
			TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMove(this.rewardedPowerIcon.transform, vector, this.moveTime1, false), 21).onComplete = new TweenCallback(this.moveStageTwo);
		}
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(this.rewardedPowerIcon.transform, new Vector3(0.75f, 0.75f, 1f), this.rewardedPowerScaleTime), 32).onComplete = new TweenCallback(this.scaleStageTwo);
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x0006CD95 File Offset: 0x0006AF95
	private void moveStageTwo()
	{
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMove(this.rewardedPowerIcon.transform, this.originalPos, this.moveTime2, false), 7).onComplete = new TweenCallback(this.moveStageThree);
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x0006CDCC File Offset: 0x0006AFCC
	private void moveStageThree()
	{
		Vector3 vector = new Vector3(this.originalPos.x, this.originalPos.y, 1f);
		vector.y += 3f;
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMove(this.rewardedPowerIcon.transform, vector, this.moveTime3, false), 7).onComplete = new TweenCallback(this.moveStageFour);
	}

	// Token: 0x06000B19 RID: 2841 RVA: 0x0006CE3A File Offset: 0x0006B03A
	private void moveStageFour()
	{
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOLocalMove(this.rewardedPowerIcon.transform, this.originalPos, this.moveTime4, false), 7).onComplete = new TweenCallback(this.moveStageThree);
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x0006CE70 File Offset: 0x0006B070
	private void scaleStageTwo()
	{
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x0006CE74 File Offset: 0x0006B074
	public void clickAnimation()
	{
		if (this.spriteAnimation.currentFrameIndex > 5)
		{
			return;
		}
		this.spriteAnimation.resetAnim(0);
		this._rotationAnimation.enabled = false;
		iTween.Stop(this._rotationAnimation.gameObject);
		this._rotationAnimation.transform.localScale = new Vector3(1f, 1f, 1f);
		if (this.state != RewardAnimationState.Idle)
		{
			this.resetAnim();
		}
		this.state = RewardAnimationState.Play;
	}

	// Token: 0x04000D67 RID: 3431
	public Image boxSprite;

	// Token: 0x04000D68 RID: 3432
	public GameObject rewardTexts;

	// Token: 0x04000D69 RID: 3433
	public Text Text_free_power_unlocked;

	// Token: 0x04000D6A RID: 3434
	public Text Text_free_power_tap_to_unlock;

	// Token: 0x04000D6B RID: 3435
	private IconRotationAnimation _rotationAnimation;

	// Token: 0x04000D6C RID: 3436
	public GameObject rewardedPowerIcon;

	// Token: 0x04000D6D RID: 3437
	private SpriteAnimation spriteAnimation;

	// Token: 0x04000D6E RID: 3438
	internal RewardAnimationState state;

	// Token: 0x04000D6F RID: 3439
	public LocalizedText bottomButtonText;

	// Token: 0x04000D70 RID: 3440
	private Vector3 originalPos = Vector3.zero;

	// Token: 0x04000D71 RID: 3441
	public bool quickReward;

	// Token: 0x04000D72 RID: 3442
	public float rewardedPowerScaleTime = 0.45f;

	// Token: 0x04000D73 RID: 3443
	public float moveTime1 = 0.25f;

	// Token: 0x04000D74 RID: 3444
	public float moveTime2 = 0.25f;

	// Token: 0x04000D75 RID: 3445
	public float moveTime3 = 1.5f;

	// Token: 0x04000D76 RID: 3446
	public float moveTime4 = 1.5f;
}
