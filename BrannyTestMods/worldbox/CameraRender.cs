using System;
using UnityEngine;

// Token: 0x020001A8 RID: 424
public class CameraRender : MonoBehaviour
{
	// Token: 0x060009A6 RID: 2470 RVA: 0x000651D0 File Offset: 0x000633D0
	private void Start()
	{
		this.mainRenderTexture = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
		this.mainRenderTexture.Create();
		this.BackgroundCamera.targetTexture = this.mainRenderTexture;
		this.MainCamera.targetTexture = this.mainRenderTexture;
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x00065223 File Offset: 0x00063423
	private void Update()
	{
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x00065225 File Offset: 0x00063425
	private void OnPostRender()
	{
		Graphics.Blit(this.mainRenderTexture, this.PostProcessMaterial);
	}

	// Token: 0x04000C4D RID: 3149
	public Material PostProcessMaterial;

	// Token: 0x04000C4E RID: 3150
	public Camera BackgroundCamera;

	// Token: 0x04000C4F RID: 3151
	public Camera MainCamera;

	// Token: 0x04000C50 RID: 3152
	private RenderTexture mainRenderTexture;
}
