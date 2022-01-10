using System;
using UnityEngine;

// Token: 0x0200027C RID: 636
public class BrushWindow : MonoBehaviour
{
	// Token: 0x06000E07 RID: 3591 RVA: 0x00083FB4 File Offset: 0x000821B4
	private void OnEnable()
	{
		foreach (object obj in this.brushes.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.name == Config.currentBrush)
			{
				this.updateSelectedGraphics(transform.GetComponent<PowerButton>());
				break;
			}
		}
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x0008402C File Offset: 0x0008222C
	public void selectBrush(GameObject pObject)
	{
		Config.currentBrush = pObject.transform.name;
		Config.currentBrushData = Brush.get(Config.currentBrush);
		this.updateSelectedGraphics(pObject.GetComponent<PowerButton>());
		base.GetComponent<ScrollWindow>().clickHide("right");
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x00084069 File Offset: 0x00082269
	private void updateSelectedGraphics(PowerButton pButton)
	{
		this.brushButton.icon.sprite = pButton.icon.sprite;
	}

	// Token: 0x040010D7 RID: 4311
	public PowerButton brushButton;

	// Token: 0x040010D8 RID: 4312
	public GameObject brushes;
}
