using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000209 RID: 521
public class DebugLogs : MonoBehaviour
{
	// Token: 0x06000BA7 RID: 2983 RVA: 0x00070C34 File Offset: 0x0006EE34
	public void showLogs()
	{
		this.text.text = LogHandler.log;
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x00070C46 File Offset: 0x0006EE46
	private void OnEnable()
	{
		this.showLogs();
	}

	// Token: 0x04000DD1 RID: 3537
	public Text text;
}
