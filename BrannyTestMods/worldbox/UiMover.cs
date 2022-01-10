using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000272 RID: 626
public class UiMover : MonoBehaviour
{
	// Token: 0x06000DD8 RID: 3544 RVA: 0x00082CD1 File Offset: 0x00080ED1
	private void Awake()
	{
		if (this.initInitPos)
		{
			this.initPos = base.gameObject.transform.localPosition;
		}
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x00082CF4 File Offset: 0x00080EF4
	public void setVisible(bool pVisible, bool pNow = false)
	{
		this.visible = pVisible;
		if (!pNow)
		{
			if (this.visible)
			{
				if (!this.onVisible)
				{
					this.onVisible = true;
					base.StartCoroutine(this.moveTween(this.initPos, "finishTween"));
					return;
				}
			}
			else if (this.onVisible)
			{
				this.onVisible = false;
				base.StartCoroutine(this.moveTween(this.hidePos, "finishTween"));
			}
			return;
		}
		if (pVisible)
		{
			base.gameObject.transform.localPosition = this.initPos;
			return;
		}
		base.gameObject.transform.localPosition = this.hidePos;
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x00082D93 File Offset: 0x00080F93
	protected IEnumerator moveTween(Vector3 pVecPos, string pCompleteCallback = "finishTween")
	{
		yield return new WaitForSeconds(0.02f);
		float num = 0.35f;
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			pVecPos,
			"easeType",
			"easeInOutCubic",
			"time",
			num,
			"islocal",
			true,
			"oncomplete",
			pCompleteCallback
		}));
		yield break;
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x00082DB0 File Offset: 0x00080FB0
	public void finishTween()
	{
	}

	// Token: 0x0400109D RID: 4253
	public bool onVisible;

	// Token: 0x0400109E RID: 4254
	public Vector3 initPos;

	// Token: 0x0400109F RID: 4255
	public Vector3 hidePos;

	// Token: 0x040010A0 RID: 4256
	public bool visible;

	// Token: 0x040010A1 RID: 4257
	public bool initInitPos = true;
}
