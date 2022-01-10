using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000264 RID: 612
public class PowerTabController : MonoBehaviour
{
	// Token: 0x06000D39 RID: 3385 RVA: 0x0007E02B File Offset: 0x0007C22B
	private void Awake()
	{
		PowerTabController.instance = this;
	}

	// Token: 0x06000D3A RID: 3386 RVA: 0x0007E033 File Offset: 0x0007C233
	internal void recalc(float pMax)
	{
		this.scrollRect.horizontalNormalizedPosition = 0f;
		this.arrowRight.maxPos = pMax;
	}

	// Token: 0x06000D3B RID: 3387 RVA: 0x0007E054 File Offset: 0x0007C254
	public Button getTabForTabGroup(string pGroupName)
	{
		List<Button> list = new List<Button>();
		list.Add(this.t_drawing);
		list.Add(this.t_kingdoms);
		list.Add(this.t_creatures);
		list.Add(this.t_bombs);
		list.Add(this.t_nature);
		list.Add(this.t_other);
		foreach (Button button in list)
		{
			if (button.GetComponent<Button>().onClick.GetPersistentTarget(0).name == pGroupName)
			{
				return button;
			}
		}
		return null;
	}

	// Token: 0x04001011 RID: 4113
	public Transform copyTarget;

	// Token: 0x04001012 RID: 4114
	public Button t_main;

	// Token: 0x04001013 RID: 4115
	public Button t_drawing;

	// Token: 0x04001014 RID: 4116
	public Button t_kingdoms;

	// Token: 0x04001015 RID: 4117
	public Button t_creatures;

	// Token: 0x04001016 RID: 4118
	public Button t_bombs;

	// Token: 0x04001017 RID: 4119
	public Button t_nature;

	// Token: 0x04001018 RID: 4120
	public Button t_other;

	// Token: 0x04001019 RID: 4121
	internal static PowerTabController instance;

	// Token: 0x0400101A RID: 4122
	public BottomBarArrow arrowLeft;

	// Token: 0x0400101B RID: 4123
	public BottomBarArrow arrowRight;

	// Token: 0x0400101C RID: 4124
	public RectTransform rect;

	// Token: 0x0400101D RID: 4125
	public ScrollRectExtended scrollRect;
}
