using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000252 RID: 594
[RequireComponent(typeof(RectTransform))]
public class CanvasNotch : MonoBehaviour
{
	// Token: 0x06000CE8 RID: 3304 RVA: 0x0007BDD4 File Offset: 0x00079FD4
	private void Awake()
	{
		this._canvas = base.gameObject.transform.GetComponentInParent<Canvas>();
		this.safeAreaTransform = base.GetComponent<RectTransform>();
		if (!this.screenChangeVarsInitialized)
		{
			this.lastOrientation = Screen.orientation;
			this.lastResolution.x = (float)Screen.width;
			this.lastResolution.y = (float)Screen.height;
			this.lastSafeArea = Screen.safeArea;
			this.screenChangeVarsInitialized = true;
		}
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x0007BE4A File Offset: 0x0007A04A
	private void Start()
	{
		this.ApplySafeArea();
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x0007BE54 File Offset: 0x0007A054
	private void Update()
	{
		if (Application.isMobilePlatform && Screen.orientation != this.lastOrientation)
		{
			this.OrientationChanged();
		}
		if (Screen.safeArea != this.lastSafeArea)
		{
			this.SafeAreaChanged();
		}
		if (this._canvas != null && this._canvas.pixelRect != this.lastCanvasRect)
		{
			this.CanvasChanged();
		}
		if ((float)Screen.width != this.lastResolution.x || (float)Screen.height != this.lastResolution.y)
		{
			this.ResolutionChanged();
		}
		if (!this.ranFirstTime)
		{
			this.ApplySafeArea();
		}
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x0007BEFC File Offset: 0x0007A0FC
	private void ApplySafeArea()
	{
		if (this._canvas == null)
		{
			return;
		}
		if (this.safeAreaTransform == null)
		{
			return;
		}
		this.ranFirstTime = true;
		Rect safeArea = Screen.safeArea;
		Vector2 position = safeArea.position;
		Vector2 anchorMax = safeArea.position + safeArea.size;
		position.x /= this._canvas.pixelRect.width;
		position.y /= this._canvas.pixelRect.height;
		anchorMax.x /= this._canvas.pixelRect.width;
		anchorMax.y /= this._canvas.pixelRect.height;
		this.safeAreaTransform.anchorMin = position;
		this.safeAreaTransform.anchorMax = anchorMax;
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x0007BFE0 File Offset: 0x0007A1E0
	private void OrientationChanged()
	{
		this.lastOrientation = Screen.orientation;
		this.lastResolution.x = (float)Screen.width;
		this.lastResolution.y = (float)Screen.height;
		this.ApplySafeArea();
	}

	// Token: 0x06000CED RID: 3309 RVA: 0x0007C015 File Offset: 0x0007A215
	private void ResolutionChanged()
	{
		this.lastResolution.x = (float)Screen.width;
		this.lastResolution.y = (float)Screen.height;
		this.ApplySafeArea();
	}

	// Token: 0x06000CEE RID: 3310 RVA: 0x0007C03F File Offset: 0x0007A23F
	private void SafeAreaChanged()
	{
		this.lastSafeArea = Screen.safeArea;
		this.ApplySafeArea();
	}

	// Token: 0x06000CEF RID: 3311 RVA: 0x0007C052 File Offset: 0x0007A252
	private void CanvasChanged()
	{
		this.lastCanvasRect = this._canvas.pixelRect;
		this.ApplySafeArea();
	}

	// Token: 0x06000CF0 RID: 3312 RVA: 0x0007C06C File Offset: 0x0007A26C
	private void debugConsole()
	{
		Dictionary<string, Rect> dictionary = new Dictionary<string, Rect>();
		Debug.Log("amount of cutouts: " + Screen.cutouts.Length.ToString());
		dictionary["screen"] = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
		dictionary["safearea"] = Screen.safeArea;
		foreach (string text in dictionary.Keys)
		{
			Debug.Log(string.Concat(new string[]
			{
				"[o] ",
				text,
				": x:",
				dictionary[text].x.ToString(),
				", y:",
				dictionary[text].y.ToString(),
				", w:",
				dictionary[text].width.ToString(),
				", h:",
				dictionary[text].height.ToString()
			}));
		}
		if (this._canvas == null)
		{
			Debug.Log("canvas not ready");
			return;
		}
		foreach (string text2 in dictionary.Keys)
		{
			Debug.Log(string.Concat(new string[]
			{
				"[c] ",
				text2,
				": x:",
				(dictionary[text2].x / this._canvas.scaleFactor).ToString(),
				", y:",
				(dictionary[text2].y / this._canvas.scaleFactor).ToString(),
				", w:",
				(dictionary[text2].width / this._canvas.scaleFactor).ToString(),
				", h:",
				(dictionary[text2].height / this._canvas.scaleFactor).ToString()
			}));
		}
	}

	// Token: 0x04000FC2 RID: 4034
	private bool screenChangeVarsInitialized;

	// Token: 0x04000FC3 RID: 4035
	private bool ranFirstTime;

	// Token: 0x04000FC4 RID: 4036
	private ScreenOrientation lastOrientation = ScreenOrientation.AutoRotation;

	// Token: 0x04000FC5 RID: 4037
	private Vector2 lastResolution = Vector2.zero;

	// Token: 0x04000FC6 RID: 4038
	private Rect lastSafeArea = Rect.zero;

	// Token: 0x04000FC7 RID: 4039
	private Rect lastCanvasRect = Rect.zero;

	// Token: 0x04000FC8 RID: 4040
	private RectTransform safeAreaTransform;

	// Token: 0x04000FC9 RID: 4041
	private Canvas _canvas;
}
