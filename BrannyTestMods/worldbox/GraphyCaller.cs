using System;
using UnityEngine;

// Token: 0x0200020F RID: 527
public class GraphyCaller : MonoBehaviour
{
	// Token: 0x06000BC6 RID: 3014 RVA: 0x0007587D File Offset: 0x00073A7D
	public void click()
	{
		this.clicked++;
		if (this.clicked > 10)
		{
			DebugConfig.instance.debugButton.SetActive(!DebugConfig.instance.debugButton.activeSelf);
		}
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x000758B8 File Offset: 0x00073AB8
	public void clickConsole()
	{
		this.clicked++;
		if (this.clicked > 10)
		{
			MapBox.instance.console.Show();
		}
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x000758E1 File Offset: 0x00073AE1
	private void OnEnable()
	{
		this.clicked = 0;
	}

	// Token: 0x04000E42 RID: 3650
	public GameObject debugButton;

	// Token: 0x04000E43 RID: 3651
	private int clicked;
}
