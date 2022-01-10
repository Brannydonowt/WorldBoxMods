using System;
using System.ComponentModel;

// Token: 0x020001C3 RID: 451
[Serializable]
public class PlayerOptionData
{
	// Token: 0x06000A40 RID: 2624 RVA: 0x000681E9 File Offset: 0x000663E9
	public PlayerOptionData(string pName)
	{
		this.name = pName;
	}

	// Token: 0x04000CBE RID: 3262
	public string name = "OPTION";

	// Token: 0x04000CBF RID: 3263
	[DefaultValue(true)]
	public bool boolVal = true;

	// Token: 0x04000CC0 RID: 3264
	[DefaultValue("")]
	public string stringVal = string.Empty;

	// Token: 0x04000CC1 RID: 3265
	[DefaultValue(0)]
	public int intVal;
}
