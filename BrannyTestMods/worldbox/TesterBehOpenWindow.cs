using System;
using System.Collections.Generic;
using ai.behaviours;
using UnityEngine;

// Token: 0x020001F0 RID: 496
public class TesterBehOpenWindow : BehaviourActionTester
{
	// Token: 0x06000B4D RID: 2893 RVA: 0x0006DBB4 File Offset: 0x0006BDB4
	public TesterBehOpenWindow(string pType)
	{
		this.type = pType;
		Object[] array = Resources.LoadAll("windows", typeof(GameObject));
		for (int i = 0; i < array.Length; i++)
		{
			string name = ((GameObject)array[i]).transform.name;
			if (!name.Contains("upload"))
			{
				this.windows.Add(name);
			}
		}
		this.windows.Remove("register");
		this.windows.Remove("worldnet_reset_password");
		this.windows.Remove("worldnet_main");
		this.windows.Remove("kingdom_technology");
		this.windows.Remove("worldnet_login");
		this.windows.Remove("worldnet_logout");
		this.windows.Remove("moonbox_promo");
		this.windows.Remove("inspect_unit");
		this.windows.Remove("kingdom");
		this.windows.Remove("village");
		this.windows.Remove("brushes");
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x0006DCE3 File Offset: 0x0006BEE3
	public override BehResult execute(AutoTesterBot pObject)
	{
		ScrollWindow.showWindow(this.windows.GetRandom<string>());
		return base.execute(pObject);
	}

	// Token: 0x04000D8F RID: 3471
	private List<string> windows = new List<string>();

	// Token: 0x04000D90 RID: 3472
	private string type;
}
