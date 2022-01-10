using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200024A RID: 586
public class BottomBarArrow : MonoBehaviour
{
	// Token: 0x06000CBF RID: 3263 RVA: 0x0007B459 File Offset: 0x00079659
	private void Start()
	{
		this.shownPos = new Vector3(0f, 0f);
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x0007B470 File Offset: 0x00079670
	private void Update()
	{
		if (this.isLeft)
		{
			if (this.scrollRect.horizontalNormalizedPosition > 0.1f)
			{
				this.timer -= Time.deltaTime * 2f;
			}
			else
			{
				this.timer += Time.deltaTime * 2f;
			}
		}
		else if (this.scrollRect.horizontalNormalizedPosition == 0f || this.scrollRect.horizontalNormalizedPosition == 1f)
		{
			this.timer += Time.deltaTime * 2f;
		}
		else if (this.scrollRect.horizontalNormalizedPosition < 0.98f)
		{
			this.timer -= Time.deltaTime * 2f;
		}
		else
		{
			this.timer += Time.deltaTime * 2f;
		}
		this.timer = Mathf.Clamp(this.timer, 0f, 1f);
		float num = iTween.easeInOutCirc(0f, this.hidPos.x, this.timer);
		if (this.arrow.transform.localPosition.x == num)
		{
			return;
		}
		this.arrow.transform.localPosition = new Vector3(num, 0f);
	}

	// Token: 0x04000FAA RID: 4010
	public Image arrow;

	// Token: 0x04000FAB RID: 4011
	private Vector3 shownPos;

	// Token: 0x04000FAC RID: 4012
	public Vector3 hidPos;

	// Token: 0x04000FAD RID: 4013
	public bool isLeft = true;

	// Token: 0x04000FAE RID: 4014
	public ScrollRect scrollRect;

	// Token: 0x04000FAF RID: 4015
	private float timer;

	// Token: 0x04000FB0 RID: 4016
	private bool shown;

	// Token: 0x04000FB1 RID: 4017
	internal float maxPos;
}
