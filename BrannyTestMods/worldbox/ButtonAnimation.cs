using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x0200024B RID: 587
public class ButtonAnimation : MonoBehaviour
{
	// Token: 0x06000CC2 RID: 3266 RVA: 0x0007B5CD File Offset: 0x000797CD
	private IEnumerator newAnim()
	{
		base.gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
		yield return new WaitForSeconds(0.01f);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.gameObject.transform, 1f, 0.1f), 28);
		yield break;
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x0007B5DC File Offset: 0x000797DC
	public void clickAnimation()
	{
		if (base.gameObject.activeSelf)
		{
			base.StartCoroutine(this.newAnim());
		}
	}
}
