using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000213 RID: 531
public class UiDebugButton : MonoBehaviour
{
	// Token: 0x06000BD7 RID: 3031 RVA: 0x00075E42 File Offset: 0x00074042
	public void Awake()
	{
		this.button.onClick.AddListener(new UnityAction(this.click));
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x00075E60 File Offset: 0x00074060
	public void click()
	{
		if (this.toggleButton)
		{
			DebugConfig.switchOption(this.debugOption);
			this.iconOn.gameObject.SetActive(DebugConfig.isOn(this.debugOption));
			return;
		}
		DebugConfig.pressButton(this.debugOption);
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x00075E9C File Offset: 0x0007409C
	public void Start()
	{
		base.gameObject.transform.name = this.debugOption.ToString();
		this.text.text = this.debugOption.ToString();
		if (this.toggleButton)
		{
			this.iconOn.gameObject.SetActive(DebugConfig.isOn(this.debugOption));
		}
	}

	// Token: 0x04000E4F RID: 3663
	private string option;

	// Token: 0x04000E50 RID: 3664
	public Text text;

	// Token: 0x04000E51 RID: 3665
	public Image iconOn;

	// Token: 0x04000E52 RID: 3666
	public Button button;

	// Token: 0x04000E53 RID: 3667
	public DebugOption debugOption;

	// Token: 0x04000E54 RID: 3668
	public bool toggleButton = true;
}
