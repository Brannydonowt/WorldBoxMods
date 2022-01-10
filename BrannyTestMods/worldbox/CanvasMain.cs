using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002FD RID: 765
public class CanvasMain : MonoBehaviour
{
	// Token: 0x060011A4 RID: 4516 RVA: 0x00099AC8 File Offset: 0x00097CC8
	private void Awake()
	{
		CanvasMain.instance = this;
		this.canvasScaler = this.canvas_ui.GetComponent<CanvasScaler>();
		this.scale_sizes.Add("50%", 0.5f);
		this.scale_sizes.Add("60%", 0.6f);
		this.scale_sizes.Add("70%", 0.7f);
		this.scale_sizes.Add("80%", 0.85f);
		this.scale_sizes.Add("90%", 0.9f);
		this.scale_sizes.Add("100%", 1f);
		this.scale_sizes.Add("110%", 1.1f);
		this.scale_sizes.Add("120%", 1.2f);
		this.scale_sizes.Add("130%", 1.3f);
		this.scale_sizes.Add("140%", 1.4f);
		this.scale_sizes.Add("150%", 1.5f);
		this.resizeUI();
	}

	// Token: 0x060011A5 RID: 4517 RVA: 0x00099BDC File Offset: 0x00097DDC
	public void resizeUI()
	{
		this.lastWidth = (float)Screen.width;
		this.lastHeight = (float)Screen.height;
		this.screenOrientation = Screen.orientation;
		this.canvasScaler.uiScaleMode = 1;
		string stringVal = PlayerConfig.dict["ui_size"].stringVal;
		float num = 1f;
		if (this.scale_sizes.ContainsKey(stringVal))
		{
			num = this.scale_sizes[stringVal];
		}
		float num2 = 2f - num;
		float num3;
		if (Screen.height > Screen.width)
		{
			num3 = 360f;
		}
		else
		{
			num3 = 500f;
		}
		this.canvasScaler.referenceResolution = new Vector2(270f, num3 * num2);
	}

	// Token: 0x060011A6 RID: 4518 RVA: 0x00099C88 File Offset: 0x00097E88
	private void Start()
	{
		this.screenOrientation = Screen.orientation;
	}

	// Token: 0x060011A7 RID: 4519 RVA: 0x00099C98 File Offset: 0x00097E98
	private void Update()
	{
		if (CanvasMain.tooltip_show_timeout > 0f)
		{
			CanvasMain.tooltip_show_timeout -= Time.deltaTime;
		}
		if ((float)Screen.width != this.lastWidth || (float)Screen.height != this.lastHeight)
		{
			this.resizeUI();
		}
		if (this.screenOrientation != Screen.orientation)
		{
			this.screenOrientation = Screen.orientation;
			if (ScrollWindow.currentWindows.Count > 1)
			{
				ScrollWindow.hideAllEvent(true);
			}
		}
		if (!Config.lockGameControls)
		{
			MapBox mapBox = MapBox.instance;
			if (!(((mapBox != null) ? mapBox.stackEffects : null) != null) || !MapBox.instance.stackEffects.isLocked())
			{
				this.blocker.gameObject.SetActive(false);
				return;
			}
		}
		this.blocker.gameObject.SetActive(true);
	}

	// Token: 0x060011A8 RID: 4520 RVA: 0x00099D62 File Offset: 0x00097F62
	public static void addTooltipShowTimeout(float pTime)
	{
		CanvasMain.tooltip_show_timeout = pTime;
	}

	// Token: 0x040014A4 RID: 5284
	public static CanvasMain instance;

	// Token: 0x040014A5 RID: 5285
	public Canvas canvas_ui;

	// Token: 0x040014A6 RID: 5286
	public Canvas canvas_windows;

	// Token: 0x040014A7 RID: 5287
	public Canvas canvas_map_names;

	// Token: 0x040014A8 RID: 5288
	public Canvas canvas_tooltip;

	// Token: 0x040014A9 RID: 5289
	public Image blocker;

	// Token: 0x040014AA RID: 5290
	private ScreenOrientation screenOrientation;

	// Token: 0x040014AB RID: 5291
	private CanvasScaler canvasScaler;

	// Token: 0x040014AC RID: 5292
	public Transform transformWindows;

	// Token: 0x040014AD RID: 5293
	private float lastWidth;

	// Token: 0x040014AE RID: 5294
	private float lastHeight;

	// Token: 0x040014AF RID: 5295
	public Dictionary<string, float> scale_sizes = new Dictionary<string, float>();

	// Token: 0x040014B0 RID: 5296
	public static float tooltip_show_timeout;
}
