using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000289 RID: 649
public class HistoryHudItem : MonoBehaviour
{
	// Token: 0x06000E55 RID: 3669 RVA: 0x00085A04 File Offset: 0x00083C04
	private void Start()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.canvasGroup.alpha = 0f;
		this.button = base.GetComponent<Button>();
		this.rectTransform = base.GetComponent<RectTransform>();
		this.txtRect = this.textField.GetComponent<RectTransform>();
		this.button.onClick.AddListener(delegate()
		{
			if (MapBox.controlsLocked())
			{
				return;
			}
			if (MapBox.instance.selectedButtons.isPowerSelected())
			{
				return;
			}
			this.remove_timer = 0f;
			ref this.message.jumpToLocation();
		});
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x00085A72 File Offset: 0x00083C72
	private void OnEnable()
	{
		this.active = true;
		this.creating = true;
		this.remove_timer = 8f;
		this.removing = false;
		base.GetComponent<CanvasGroup>().alpha = 0f;
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x00085AA4 File Offset: 0x00083CA4
	public void SetMessage(ref WorldLogMessage pMessage)
	{
		this.textField.text = ref pMessage.getFormatedText(this.textField, false, true);
		this.textField.GetComponent<LocalizedText>().checkTextFont();
		this.textField.GetComponent<LocalizedText>().checkSpecialLanguages();
		if (pMessage.icon != "")
		{
			if (!HistoryHudItem.spriteList.ContainsKey(pMessage.icon))
			{
				HistoryHudItem.spriteList[pMessage.icon] = (Sprite)Resources.Load("ui/Icons/" + pMessage.icon, typeof(Sprite));
			}
			this.icon.sprite = HistoryHudItem.spriteList[pMessage.icon];
		}
		else
		{
			this.icon.gameObject.SetActive(false);
		}
		this.message = pMessage;
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x00085B7C File Offset: 0x00083D7C
	public void moveTo(float newBottom)
	{
		this.timeLimit = 0f;
		this.targetBottom = newBottom;
	}

	// Token: 0x06000E59 RID: 3673 RVA: 0x00085B90 File Offset: 0x00083D90
	public void moveToAndDestroy(float newBottom)
	{
		this.timeLimit = 0f;
		this.targetBottom = newBottom;
		this.remove_timer = 0.5f;
		this.removing = true;
	}

	// Token: 0x06000E5A RID: 3674 RVA: 0x00085BB8 File Offset: 0x00083DB8
	private void Update()
	{
		if (!this.active)
		{
			return;
		}
		this.background.raycastTarget = HistoryHud.raycastOn;
		this.rectTransform.sizeDelta = new Vector2(this.rectTransform.sizeDelta.x, 10f);
		if (this.creating)
		{
			if (this.canvasGroup.alpha < 1f)
			{
				this.canvasGroup.alpha += Time.deltaTime * Config.timeScale * 2f;
				return;
			}
			this.creating = false;
			return;
		}
		else
		{
			if (Config.paused || ScrollWindow.isWindowActive() || RewardedAds.isShowing())
			{
				return;
			}
			if (this.timeLimit <= 2f)
			{
				this.timeLimit += Time.deltaTime;
				this.rectTransform.SetTop(-Mathf.Lerp(this.rectTransform.offsetMax.y, -this.targetBottom, this.timeLimit / 2f));
			}
			if (this.removing && this.rectTransform.offsetMax.y > 10f)
			{
				HistoryHud.makeInactive(this);
				return;
			}
			this.remove_timer -= Time.deltaTime;
			if (this.remove_timer <= 0f)
			{
				this.canvasGroup.alpha -= Time.deltaTime * 2f;
				if (this.canvasGroup.alpha <= 0f)
				{
					HistoryHud.makeInactive(this);
				}
			}
			return;
		}
	}

	// Token: 0x04001128 RID: 4392
	public bool active;

	// Token: 0x04001129 RID: 4393
	private bool creating = true;

	// Token: 0x0400112A RID: 4394
	private float remove_timer = 8f;

	// Token: 0x0400112B RID: 4395
	private CanvasGroup canvasGroup;

	// Token: 0x0400112C RID: 4396
	private Button button;

	// Token: 0x0400112D RID: 4397
	private WorldLogMessage message;

	// Token: 0x0400112E RID: 4398
	public Text textField;

	// Token: 0x0400112F RID: 4399
	public Image icon;

	// Token: 0x04001130 RID: 4400
	private RectTransform rectTransform;

	// Token: 0x04001131 RID: 4401
	public Image background;

	// Token: 0x04001132 RID: 4402
	private RectTransform txtRect;

	// Token: 0x04001133 RID: 4403
	private static Dictionary<string, Sprite> spriteList = new Dictionary<string, Sprite>();

	// Token: 0x04001134 RID: 4404
	public bool removing;

	// Token: 0x04001135 RID: 4405
	private float timeLimit;

	// Token: 0x04001136 RID: 4406
	public float targetBottom;
}
