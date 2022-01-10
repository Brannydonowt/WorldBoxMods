using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000251 RID: 593
public class CancelButton : MonoBehaviour
{
	// Token: 0x06000CE3 RID: 3299 RVA: 0x0007BC4E File Offset: 0x00079E4E
	private void Awake()
	{
		this.rect = base.GetComponent<RectTransform>();
	}

	// Token: 0x06000CE4 RID: 3300 RVA: 0x0007BC5C File Offset: 0x00079E5C
	public void setIconFrom(PowerButton pButton)
	{
		if (pButton.godPower == null)
		{
			return;
		}
		if (pButton.icon == null)
		{
			return;
		}
		this.powerIcon.sprite = pButton.icon.sprite;
	}

	// Token: 0x06000CE5 RID: 3301 RVA: 0x0007BC8C File Offset: 0x00079E8C
	private void Update()
	{
		if (CancelButton.goDown != this._goDown)
		{
			this._goDown = CancelButton.goDown;
			this.timer = 0f;
			if (CancelButton.goDown)
			{
				this.timer = 0.95f;
			}
		}
		if (CancelButton.goUp != this._goUp)
		{
			this._goUp = CancelButton.goUp;
			this.timer = -1f;
		}
		if (this.timer < 1f)
		{
			this.timer += Time.deltaTime / 2f;
			this.timer = Mathf.Clamp(this.timer, 0f, 1f);
			float y;
			if (this._goDown)
			{
				y = iTween.easeInOutCirc(0f, -90f, this.timer);
			}
			else if (this._goUp)
			{
				y = iTween.easeInQuart(0f, this.topTarget, this.timer);
			}
			else
			{
				y = iTween.easeInOutCirc(this.rect.anchoredPosition.y, 0f, this.timer);
			}
			this.rect.anchoredPosition = new Vector3(this.rect.anchoredPosition.x, y);
		}
	}

	// Token: 0x04000FBA RID: 4026
	public Image powerIcon;

	// Token: 0x04000FBB RID: 4027
	public static bool goUp;

	// Token: 0x04000FBC RID: 4028
	public static bool goDown;

	// Token: 0x04000FBD RID: 4029
	private bool _goDown;

	// Token: 0x04000FBE RID: 4030
	private bool _goUp;

	// Token: 0x04000FBF RID: 4031
	private RectTransform rect;

	// Token: 0x04000FC0 RID: 4032
	private float timer;

	// Token: 0x04000FC1 RID: 4033
	private float topTarget = 90f;
}
