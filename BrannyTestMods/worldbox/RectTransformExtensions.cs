using System;
using UnityEngine;

// Token: 0x020001C9 RID: 457
public static class RectTransformExtensions
{
	// Token: 0x06000A51 RID: 2641 RVA: 0x000689B4 File Offset: 0x00066BB4
	public static void SetLeft(this RectTransform rt, float left)
	{
		rt.offsetMin = new Vector2(left, rt.offsetMin.y);
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x000689CD File Offset: 0x00066BCD
	public static void SetRight(this RectTransform rt, float right)
	{
		rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x000689E7 File Offset: 0x00066BE7
	public static void SetTop(this RectTransform rt, float top)
	{
		rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x00068A01 File Offset: 0x00066C01
	public static void SetBottom(this RectTransform rt, float bottom)
	{
		rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x00068A1C File Offset: 0x00066C1C
	public static void SetAnchor(this RectTransform source, AnchorPresets allign, float offsetX = 0f, float offsetY = 0f)
	{
		source.anchoredPosition = new Vector3(offsetX, offsetY, 0f);
		switch (allign)
		{
		case AnchorPresets.TopLeft:
			source.anchorMin = new Vector2(0f, 1f);
			source.anchorMax = new Vector2(0f, 1f);
			return;
		case AnchorPresets.TopCenter:
			source.anchorMin = new Vector2(0.5f, 1f);
			source.anchorMax = new Vector2(0.5f, 1f);
			return;
		case AnchorPresets.TopRight:
			source.anchorMin = new Vector2(1f, 1f);
			source.anchorMax = new Vector2(1f, 1f);
			return;
		case AnchorPresets.MiddleLeft:
			source.anchorMin = new Vector2(0f, 0.5f);
			source.anchorMax = new Vector2(0f, 0.5f);
			return;
		case AnchorPresets.MiddleCenter:
			source.anchorMin = new Vector2(0.5f, 0.5f);
			source.anchorMax = new Vector2(0.5f, 0.5f);
			return;
		case AnchorPresets.MiddleRight:
			source.anchorMin = new Vector2(1f, 0.5f);
			source.anchorMax = new Vector2(1f, 0.5f);
			return;
		case AnchorPresets.BottomLeft:
			source.anchorMin = new Vector2(0f, 0f);
			source.anchorMax = new Vector2(0f, 0f);
			return;
		case AnchorPresets.BottonCenter:
			source.anchorMin = new Vector2(0.5f, 0f);
			source.anchorMax = new Vector2(0.5f, 0f);
			return;
		case AnchorPresets.BottomRight:
			source.anchorMin = new Vector2(1f, 0f);
			source.anchorMax = new Vector2(1f, 0f);
			return;
		case AnchorPresets.BottomStretch:
			break;
		case AnchorPresets.VertStretchLeft:
			source.anchorMin = new Vector2(0f, 0f);
			source.anchorMax = new Vector2(0f, 1f);
			return;
		case AnchorPresets.VertStretchRight:
			source.anchorMin = new Vector2(1f, 0f);
			source.anchorMax = new Vector2(1f, 1f);
			return;
		case AnchorPresets.VertStretchCenter:
			source.anchorMin = new Vector2(0.5f, 0f);
			source.anchorMax = new Vector2(0.5f, 1f);
			return;
		case AnchorPresets.HorStretchTop:
			source.anchorMin = new Vector2(0f, 1f);
			source.anchorMax = new Vector2(1f, 1f);
			return;
		case AnchorPresets.HorStretchMiddle:
			source.anchorMin = new Vector2(0f, 0.5f);
			source.anchorMax = new Vector2(1f, 0.5f);
			return;
		case AnchorPresets.HorStretchBottom:
			source.anchorMin = new Vector2(0f, 0f);
			source.anchorMax = new Vector2(1f, 0f);
			return;
		case AnchorPresets.StretchAll:
			source.anchorMin = new Vector2(0f, 0f);
			source.anchorMax = new Vector2(1f, 1f);
			break;
		default:
			return;
		}
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x00068D3C File Offset: 0x00066F3C
	public static void SetPivot(this RectTransform source, PivotPresets preset)
	{
		switch (preset)
		{
		case PivotPresets.TopLeft:
			source.pivot = new Vector2(0f, 1f);
			return;
		case PivotPresets.TopCenter:
			source.pivot = new Vector2(0.5f, 1f);
			return;
		case PivotPresets.TopRight:
			source.pivot = new Vector2(1f, 1f);
			return;
		case PivotPresets.MiddleLeft:
			source.pivot = new Vector2(0f, 0.5f);
			return;
		case PivotPresets.MiddleCenter:
			source.pivot = new Vector2(0.5f, 0.5f);
			return;
		case PivotPresets.MiddleRight:
			source.pivot = new Vector2(1f, 0.5f);
			return;
		case PivotPresets.BottomLeft:
			source.pivot = new Vector2(0f, 0f);
			return;
		case PivotPresets.BottomCenter:
			source.pivot = new Vector2(0.5f, 0f);
			return;
		case PivotPresets.BottomRight:
			source.pivot = new Vector2(1f, 0f);
			return;
		default:
			return;
		}
	}
}
