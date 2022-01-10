using System;
using UnityEngine;

// Token: 0x02000214 RID: 532
public class UiDebugWindow : MonoBehaviour
{
	// Token: 0x06000BDB RID: 3035 RVA: 0x00075F18 File Offset: 0x00074118
	private void OnEnable()
	{
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x00075F1C File Offset: 0x0007411C
	public void setWindow(DebugOption pOption)
	{
		this.page_toolbar.SetActive(false);
		this.page_jobs.SetActive(false);
		this.page_system.SetActive(false);
		this.page_logs.gameObject.SetActive(false);
		if (pOption == DebugOption.TabsLogs)
		{
			this.page_logs.gameObject.SetActive(true);
			this.page_logs.showLogs();
		}
		if (pOption == DebugOption.TabUnits)
		{
			this.page_jobs.SetActive(true);
		}
		if (pOption == DebugOption.TabMain)
		{
			this.page_toolbar.SetActive(true);
		}
		if (pOption == DebugOption.TabSystem)
		{
			this.page_system.SetActive(true);
		}
	}

	// Token: 0x04000E55 RID: 3669
	public GameObject page_toolbar;

	// Token: 0x04000E56 RID: 3670
	public GameObject page_jobs;

	// Token: 0x04000E57 RID: 3671
	public GameObject page_system;

	// Token: 0x04000E58 RID: 3672
	public DebugLogs page_logs;
}
