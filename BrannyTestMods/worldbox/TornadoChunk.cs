using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x0200011E RID: 286
public class TornadoChunk : MonoBehaviour
{
	// Token: 0x0600066E RID: 1646 RVA: 0x0004AF69 File Offset: 0x00049169
	private void Start()
	{
		TweenSettingsExtensions.SetLoops<Tweener>(ShortcutExtensions.DOShakePosition(base.transform, this.shake, 1f, 10, 90f, false, true), -1);
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x0004AF91 File Offset: 0x00049191
	private void Update()
	{
	}

	// Token: 0x04000858 RID: 2136
	public float shake;
}
