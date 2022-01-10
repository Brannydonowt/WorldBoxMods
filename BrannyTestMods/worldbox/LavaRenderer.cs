using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x0200010D RID: 269
public class LavaRenderer : MonoBehaviour
{
	// Token: 0x06000602 RID: 1538 RVA: 0x00047C40 File Offset: 0x00045E40
	private void Start()
	{
		this.renderTexture = new RenderTexture(Screen.width, Screen.height, 8, RenderTextureFormat.ARGB32);
		this.renderTexture.dimension = TextureDimension.Tex2D;
		this.renderTexture.antiAliasing = 1;
		this.renderTexture.anisoLevel = 0;
		this.renderTexture.filterMode = FilterMode.Point;
		this.renderTexture.Create();
		this.curCamera.targetTexture = this.renderTexture;
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x00047CB1 File Offset: 0x00045EB1
	private void OnPreRender()
	{
		this.targetCamera.targetTexture = this.renderTexture;
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x00047CC4 File Offset: 0x00045EC4
	private void OnPostRender()
	{
		this.targetCamera.targetTexture = null;
		Graphics.DrawTexture(new Rect(0f, 0f, (float)(Screen.width / 2), (float)(Screen.height / 2)), this.renderTexture, null);
	}

	// Token: 0x040007E0 RID: 2016
	public Camera curCamera;

	// Token: 0x040007E1 RID: 2017
	public Camera targetCamera;

	// Token: 0x040007E2 RID: 2018
	private RenderTexture renderTexture;
}
