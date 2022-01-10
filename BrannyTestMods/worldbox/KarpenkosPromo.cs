using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B9 RID: 441
public class KarpenkosPromo : MonoBehaviour
{
	// Token: 0x060009EC RID: 2540 RVA: 0x00066328 File Offset: 0x00064528
	private void Awake()
	{
		this.maxElements = this.sprites.Count;
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x0006633C File Offset: 0x0006453C
	private void OnEnable()
	{
		this.curImageIndex = 0;
		this.timerChange = this.intervalMainImage / 2f;
		this.setImage(this.image1, this.curImageIndex);
		this.curImageIndex++;
		this.setImage(this.image2, this.curImageIndex);
		this.imageCurrent = this.image1;
		this.imageTransition = this.image2;
		this.imageCurrent.GetComponent<CanvasGroup>().alpha = 1f;
		this.imageTransition.GetComponent<CanvasGroup>().alpha = 0f;
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x000663D6 File Offset: 0x000645D6
	private void setImage(Image pImage, int pIndex)
	{
		pImage.sprite = this.sprites[pIndex];
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x000663EC File Offset: 0x000645EC
	private void Update()
	{
		if (this.timerChange > 0f)
		{
			this.timerChange -= Time.deltaTime;
			return;
		}
		if (this.imageTransition.GetComponent<CanvasGroup>().alpha < 1f)
		{
			this.imageTransition.GetComponent<CanvasGroup>().alpha += Time.deltaTime * 2f;
			if (this.imageTransition.GetComponent<CanvasGroup>().alpha >= 1f)
			{
				this.imageTransition.GetComponent<CanvasGroup>().alpha = 0f;
				this.imageCurrent.sprite = this.imageTransition.sprite;
				this.timerChange = this.intervalChange;
				if (this.curImageIndex == 0)
				{
					this.timerChange = this.intervalMainImage;
				}
				this.curImageIndex++;
				if (this.curImageIndex >= this.maxElements)
				{
					this.curImageIndex = 0;
				}
				this.setImage(this.imageTransition, this.curImageIndex);
			}
		}
	}

	// Token: 0x04000C72 RID: 3186
	public List<Sprite> sprites = new List<Sprite>();

	// Token: 0x04000C73 RID: 3187
	public Image image1;

	// Token: 0x04000C74 RID: 3188
	public Image image2;

	// Token: 0x04000C75 RID: 3189
	private float intervalChange = 1f;

	// Token: 0x04000C76 RID: 3190
	private float intervalMainImage = 1.5f;

	// Token: 0x04000C77 RID: 3191
	private int maxElements;

	// Token: 0x04000C78 RID: 3192
	private int curImageIndex;

	// Token: 0x04000C79 RID: 3193
	private float timerChange;

	// Token: 0x04000C7A RID: 3194
	private Image imageTransition;

	// Token: 0x04000C7B RID: 3195
	private Image imageCurrent;
}
