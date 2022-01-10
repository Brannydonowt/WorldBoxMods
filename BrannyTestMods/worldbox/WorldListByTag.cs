using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002CC RID: 716
public class WorldListByTag : MonoBehaviour
{
	// Token: 0x040012AD RID: 4781
	public MapTagButton mapTagButtonPrefab;

	// Token: 0x040012AE RID: 4782
	public Transform transformContent;

	// Token: 0x040012AF RID: 4783
	public Transform tagContent;

	// Token: 0x040012B0 RID: 4784
	public Transform listContent;

	// Token: 0x040012B1 RID: 4785
	private bool loaded;

	// Token: 0x040012B2 RID: 4786
	private List<MapTagButton> elements = new List<MapTagButton>();
}
