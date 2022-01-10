using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000263 RID: 611
public class PauseButton : MonoBehaviour
{
	// Token: 0x06000D35 RID: 3381 RVA: 0x0007DFA0 File Offset: 0x0007C1A0
	private void Start()
	{
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x0007DFA2 File Offset: 0x0007C1A2
	internal void press()
	{
		Config.paused = !Config.paused;
		if (Config.paused)
		{
			WorldTip.instance.setText("gamePaused", false);
		}
		else
		{
			WorldTip.instance.setText("gameUnpaused", false);
		}
		this.updateSprite();
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x0007DFE0 File Offset: 0x0007C1E0
	internal void updateSprite()
	{
		Image component = base.transform.Find("Icon").GetComponent<Image>();
		if (Config.paused)
		{
			component.sprite = this.play;
			return;
		}
		component.sprite = this.pause;
	}

	// Token: 0x0400100F RID: 4111
	public Sprite pause;

	// Token: 0x04001010 RID: 4112
	public Sprite play;
}
