using System;
using UnityEngine;

// Token: 0x02000092 RID: 146
public class LibraryMaterials : MonoBehaviour
{
	// Token: 0x06000325 RID: 805 RVA: 0x00034BA1 File Offset: 0x00032DA1
	private void Awake()
	{
		LibraryMaterials.instance = this;
	}

	// Token: 0x04000523 RID: 1315
	public static LibraryMaterials instance;

	// Token: 0x04000524 RID: 1316
	public Material matDamaged;

	// Token: 0x04000525 RID: 1317
	public Material matHighLighted;

	// Token: 0x04000526 RID: 1318
	public Material matWorldObjects;
}
