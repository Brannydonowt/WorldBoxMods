using System;
using UnityEngine;

// Token: 0x0200025E RID: 606
public class LoadingScreenSheepAnimation : MonoBehaviour
{
	// Token: 0x06000D25 RID: 3365 RVA: 0x0007DC23 File Offset: 0x0007BE23
	private void Update()
	{
		LoadingScreenSheepAnimation.angle += Time.deltaTime * 20f;
		base.transform.localEulerAngles = new Vector3(0f, 0f, LoadingScreenSheepAnimation.angle);
	}

	// Token: 0x04001007 RID: 4103
	internal static float angle;
}
