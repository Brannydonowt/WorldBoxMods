using System;
using UnityEngine;

// Token: 0x02000193 RID: 403
public class RelationColor
{
	// Token: 0x0600093D RID: 2365 RVA: 0x00061B80 File Offset: 0x0005FD80
	public RelationColor(string pColor)
	{
		this.color = Toolbox.makeColor(pColor);
		this.border = Toolbox.makeColor(pColor);
		this.color.a = 160;
		this.id = RelationColor.last_id++;
	}

	// Token: 0x04000BD8 RID: 3032
	private static int last_id;

	// Token: 0x04000BD9 RID: 3033
	public Color32 color;

	// Token: 0x04000BDA RID: 3034
	public Color32 border;

	// Token: 0x04000BDB RID: 3035
	public int id;
}
