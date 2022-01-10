using System;
using UnityEngine;

// Token: 0x020002D2 RID: 722
public class MapMark : MonoBehaviour
{
	// Token: 0x06000F9C RID: 3996 RVA: 0x0008B5F9 File Offset: 0x000897F9
	private void Awake()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		Transform transform = base.transform.Find("sail");
		this.spriteRendererSail = ((transform != null) ? transform.GetComponent<SpriteRenderer>() : null);
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x0008B629 File Offset: 0x00089829
	public void show()
	{
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x0008B62B File Offset: 0x0008982B
	public void clear()
	{
	}

	// Token: 0x040012CD RID: 4813
	public SpriteRenderer spriteRenderer;

	// Token: 0x040012CE RID: 4814
	public SpriteRenderer spriteRendererSail;
}
