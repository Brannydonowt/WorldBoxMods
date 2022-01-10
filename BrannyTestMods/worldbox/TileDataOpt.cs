using System;
using System.Collections.Generic;

// Token: 0x02000228 RID: 552
[Serializable]
public class TileDataOpt
{
	// Token: 0x06000C62 RID: 3170 RVA: 0x00079EE4 File Offset: 0x000780E4
	internal void add(string pVal)
	{
		this.v.Add(pVal);
	}

	// Token: 0x04000EF8 RID: 3832
	public int x;

	// Token: 0x04000EF9 RID: 3833
	public int y;

	// Token: 0x04000EFA RID: 3834
	public string type;

	// Token: 0x04000EFB RID: 3835
	public List<string> v = new List<string>();
}
