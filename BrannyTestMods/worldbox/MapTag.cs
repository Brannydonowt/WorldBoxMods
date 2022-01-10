using System;
using UnityEngine;

// Token: 0x02000244 RID: 580
public class MapTag : MonoBehaviour
{
	// Token: 0x06000CAF RID: 3247 RVA: 0x0007B28A File Offset: 0x0007948A
	private void Start()
	{
		this.updateSprite();
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x0007B292 File Offset: 0x00079492
	public void clickButton()
	{
	}

	// Token: 0x06000CB1 RID: 3249 RVA: 0x0007B294 File Offset: 0x00079494
	public void clickListWorldsButton()
	{
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x0007B296 File Offset: 0x00079496
	private void updateSprite()
	{
	}

	// Token: 0x04000F94 RID: 3988
	public bool tagEnabled;

	// Token: 0x04000F95 RID: 3989
	public MapTagType tagType;

	// Token: 0x04000F96 RID: 3990
	public Sprite buttonOn;

	// Token: 0x04000F97 RID: 3991
	public Sprite buttonOff;

	// Token: 0x04000F98 RID: 3992
	public string icon;

	// Token: 0x04000F99 RID: 3993
	public CanvasGroup tagGroup;
}
