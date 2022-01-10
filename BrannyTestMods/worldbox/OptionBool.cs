using System;
using SleekRender;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020001BF RID: 447
public class OptionBool : MonoBehaviour
{
	// Token: 0x06000A15 RID: 2581 RVA: 0x0006747C File Offset: 0x0006567C
	private void Start()
	{
		this.updateSprite();
		this.world = MapBox.instance;
		if (this.invokeCallbackOnStart)
		{
			if (this.callback != null)
			{
				this.callback.Invoke();
			}
			if (this.boolCallback != null)
			{
				this.boolCallback.Invoke(this.optionEnabled);
			}
		}
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x000674CE File Offset: 0x000656CE
	public void checkGameOption(bool pSwitch = false)
	{
		if (pSwitch)
		{
			PlayerConfig.switchOption(this.gameOptionName, this.gameOptionType);
		}
		this.optionEnabled = PlayerConfig.optionEnabled(this.gameOptionName, this.gameOptionType);
		this.updateSprite();
		this.updateStaticOptions();
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x00067507 File Offset: 0x00065707
	private void OnEnable()
	{
		this.world = MapBox.instance;
		if (this.world == null)
		{
			return;
		}
		if (this.gameOption)
		{
			this.updateSprite();
			this.checkGameOption(false);
			this.updateSprite();
		}
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x00067540 File Offset: 0x00065740
	private void updateStaticOptions()
	{
		if (this.gameOptionName == "portrait")
		{
			Config.setPortrait(this.optionEnabled);
			return;
		}
		if (this.gameOptionName == "bloom")
		{
			this.sleekRenderSettings.bloomEnabled = this.optionEnabled;
			return;
		}
		if (this.gameOptionName == "vignette")
		{
			this.sleekRenderSettings.vignetteEnabled = this.optionEnabled;
			return;
		}
		if (this.gameOptionName == "smoke")
		{
			this.world.particlesSmoke.enabled = this.optionEnabled;
			return;
		}
		if (this.gameOptionName == "vsync")
		{
			PlayerConfig.setVsync(this.optionEnabled);
			return;
		}
		if (this.gameOptionName == "fire")
		{
			this.world.particlesFire.enabled = this.optionEnabled;
			return;
		}
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x00067624 File Offset: 0x00065824
	public void clickButton()
	{
		if (this.gameOption)
		{
			this.checkGameOption(true);
			PlayerConfig.saveData();
			return;
		}
		this.optionEnabled = !this.optionEnabled;
		this.updateSprite();
		if (this.callback != null)
		{
			this.callback.Invoke();
		}
		if (this.boolCallback != null)
		{
			this.boolCallback.Invoke(this.optionEnabled);
		}
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x00067687 File Offset: 0x00065887
	private void updateSprite()
	{
		if (this.optionEnabled)
		{
			this.icon.sprite = this.spriteOn;
			return;
		}
		this.icon.sprite = this.spriteOff;
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x000676B4 File Offset: 0x000658B4
	public void optionDefault()
	{
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x000676B6 File Offset: 0x000658B6
	public void optionSpriteAnimation()
	{
		Config.spriteAnimationsOn = !Config.spriteAnimationsOn;
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x000676C5 File Offset: 0x000658C5
	public void optionShowFPS()
	{
		Config.showFPS = !Config.showFPS;
		this.world.debugText.enabled = Config.showFPS;
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x000676E9 File Offset: 0x000658E9
	public void optionShowWORLD()
	{
		this.world.gameObject.SetActive(!this.world.gameObject.activeSelf);
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x0006770E File Offset: 0x0006590E
	public void optionRemovePremuium()
	{
		Config.havePremium = false;
		PlayerConfig.instance.data.premium = false;
		PlayerConfig.saveData();
		PremiumElementsChecker.checkElements();
		if (Config.isMobile)
		{
			InAppManager.consumePremium();
		}
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x0006773C File Offset: 0x0006593C
	public void clearRewards()
	{
		PlayerConfig.instance.data.rewardedPowers.Clear();
		PlayerConfig.saveData();
		PremiumElementsChecker.checkElements();
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x0006775C File Offset: 0x0006595C
	public void optionShowCanvas()
	{
		this.world.canvas.enabled = false;
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x0006776F File Offset: 0x0006596F
	public void optionRenderer()
	{
		this.spriteRenderer.enabled = this.optionEnabled;
		this.updateSprite();
	}

	// Token: 0x04000C9F RID: 3231
	public bool optionEnabled = true;

	// Token: 0x04000CA0 RID: 3232
	public bool invokeCallbackOnStart = true;

	// Token: 0x04000CA1 RID: 3233
	public SpriteRenderer spriteRenderer;

	// Token: 0x04000CA2 RID: 3234
	public Image icon;

	// Token: 0x04000CA3 RID: 3235
	private Button button;

	// Token: 0x04000CA4 RID: 3236
	public Sprite spriteOn;

	// Token: 0x04000CA5 RID: 3237
	public Sprite spriteOff;

	// Token: 0x04000CA6 RID: 3238
	private MapBox world;

	// Token: 0x04000CA7 RID: 3239
	public UnityEvent callback;

	// Token: 0x04000CA8 RID: 3240
	public UnityEvent<bool> boolCallback;

	// Token: 0x04000CA9 RID: 3241
	public bool gameOption;

	// Token: 0x04000CAA RID: 3242
	public OptionType gameOptionType;

	// Token: 0x04000CAB RID: 3243
	public string gameOptionName = "-";

	// Token: 0x04000CAC RID: 3244
	public SleekRenderSettings sleekRenderSettings;
}
