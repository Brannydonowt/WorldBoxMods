using System;
using UnityEngine;

// Token: 0x020002FE RID: 766
public class WorldButton : MonoBehaviour
{
	// Token: 0x060011AB RID: 4523 RVA: 0x00099D7F File Offset: 0x00097F7F
	private void Start()
	{
		this.initial_pos = base.transform.localPosition;
		if (this.mainButtonObject != null)
		{
			this.hide();
		}
	}

	// Token: 0x060011AC RID: 4524 RVA: 0x00099DA8 File Offset: 0x00097FA8
	public void onClickMain()
	{
		if (WorldButton.active_buttons != null && WorldButton.active_buttons != this)
		{
			WorldButton.active_buttons.hideChildren();
			WorldButton.active_buttons = null;
		}
		if (!this.lesser_buttons[0].gameObject.activeSelf)
		{
			WorldButton[] array = this.lesser_buttons;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].activate();
			}
			WorldButton.active_buttons = this;
			return;
		}
		this.hideChildren();
	}

	// Token: 0x060011AD RID: 4525 RVA: 0x00099E20 File Offset: 0x00098020
	public void hideChildren()
	{
		WorldButton[] array = this.lesser_buttons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].hide();
		}
	}

	// Token: 0x060011AE RID: 4526 RVA: 0x00099E4A File Offset: 0x0009804A
	public void hide()
	{
		base.gameObject.SetActive(false);
		base.transform.localPosition = this.mainButtonObject.transform.position;
	}

	// Token: 0x060011AF RID: 4527 RVA: 0x00099E73 File Offset: 0x00098073
	public void activate()
	{
		base.gameObject.SetActive(true);
		base.transform.localPosition = this.initial_pos;
	}

	// Token: 0x040014B1 RID: 5297
	public static WorldButton active_buttons;

	// Token: 0x040014B2 RID: 5298
	public WorldButton mainButtonObject;

	// Token: 0x040014B3 RID: 5299
	public WorldButton[] lesser_buttons;

	// Token: 0x040014B4 RID: 5300
	private Vector3 initial_pos;
}
