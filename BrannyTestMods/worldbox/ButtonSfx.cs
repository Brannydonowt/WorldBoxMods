using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200019C RID: 412
public class ButtonSfx : MonoBehaviour
{
	// Token: 0x06000970 RID: 2416 RVA: 0x00063E49 File Offset: 0x00062049
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.playSound));
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x00063E67 File Offset: 0x00062067
	private void playSound()
	{
		Sfx.play(this.soundID, true, -1f, -1f);
	}

	// Token: 0x04000C11 RID: 3089
	public string soundID;
}
