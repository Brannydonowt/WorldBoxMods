using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002E7 RID: 743
public class ScrollWindow : MonoBehaviour
{
	// Token: 0x0600101F RID: 4127 RVA: 0x0008DF57 File Offset: 0x0008C157
	public static bool isWindowActive()
	{
		return ScrollWindow.currentWindows.Count > 0;
	}

	// Token: 0x06001020 RID: 4128 RVA: 0x0008DF68 File Offset: 0x0008C168
	private void Awake()
	{
		this.canvasGroup = base.gameObject.GetComponent<CanvasGroup>();
		this.bgRect = base.gameObject.transform.GetChild(0).GetComponent<RectTransform>();
		if (this.destroyOnAwake != null)
		{
			GameObject[] array = this.destroyOnAwake;
			for (int i = 0; i < array.Length; i++)
			{
				Object.Destroy(array[i]);
			}
		}
	}

	// Token: 0x06001021 RID: 4129 RVA: 0x0008DFC7 File Offset: 0x0008C1C7
	private void Start()
	{
		if (this.canvas == null)
		{
			this.create(true);
		}
	}

	// Token: 0x06001022 RID: 4130 RVA: 0x0008DFE0 File Offset: 0x0008C1E0
	internal void create(bool pHide = false)
	{
		this.canvas = CanvasMain.instance.canvas_windows;
		if (this.historyActionEnabled)
		{
			this.backButton.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(new UnityAction(WindowHistory.clickBack));
			this.backButton.transform.SetAsLastSibling();
		}
		else
		{
			this.backButton.gameObject.SetActive(false);
		}
		if (pHide)
		{
			this.hide("right");
			this.finishTween();
		}
		this.checkGradient();
	}

	// Token: 0x06001023 RID: 4131 RVA: 0x0008E070 File Offset: 0x0008C270
	private void checkGradient()
	{
		if (this.scrollRect.gameObject.activeSelf && this.scrollRect.enabled && this.transform_scrollRect.sizeDelta.y < this.transform_content.sizeDelta.y)
		{
			this.scrollingGradient.gameObject.SetActive(true);
			return;
		}
		this.scrollingGradient.gameObject.SetActive(false);
	}

	// Token: 0x06001024 RID: 4132 RVA: 0x0008E0E1 File Offset: 0x0008C2E1
	public void clickBack()
	{
		WindowHistory.clickBack();
	}

	// Token: 0x06001025 RID: 4133 RVA: 0x0008E0E8 File Offset: 0x0008C2E8
	private static void addCurrentWindow(ScrollWindow pWindow)
	{
		ScrollWindow.currentWindows.Add(pWindow);
	}

	// Token: 0x06001026 RID: 4134 RVA: 0x0008E0F5 File Offset: 0x0008C2F5
	public void switchActive()
	{
		this.setActive(!this.currentWindow, "right", null, "right", false);
	}

	// Token: 0x06001027 RID: 4135 RVA: 0x0008E112 File Offset: 0x0008C312
	public static void queueWindow(string pWindowID)
	{
		ScrollWindow.queuedWindow = pWindowID;
	}

	// Token: 0x06001028 RID: 4136 RVA: 0x0008E11A File Offset: 0x0008C31A
	public static void clearQueue()
	{
		if (ScrollWindow.queuedWindow != "")
		{
			string pWindowID = ScrollWindow.queuedWindow;
			ScrollWindow.queuedWindow = "";
			ScrollWindow.hideAllEvent(false);
			ScrollWindow.showWindow(pWindowID);
		}
	}

	// Token: 0x06001029 RID: 4137 RVA: 0x0008E147 File Offset: 0x0008C347
	public static void showWindow(string pWindowID)
	{
		if (ScrollWindow.animationActive)
		{
			return;
		}
		ScrollWindow.checkWindowExist(pWindowID);
		ScrollWindow.allWindows[pWindowID].clickShow();
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x0008E168 File Offset: 0x0008C368
	public static bool windowLoaded(string pWindowID)
	{
		string text = pWindowID;
		if (text.StartsWith("worldnet", StringComparison.Ordinal))
		{
			text = "not_found";
		}
		return ScrollWindow.allWindows.ContainsKey(text);
	}

	// Token: 0x0600102B RID: 4139 RVA: 0x0008E198 File Offset: 0x0008C398
	private static void checkWindowExist(string pWindowID)
	{
		string text = pWindowID;
		if (text.StartsWith("worldnet", StringComparison.Ordinal))
		{
			text = "not_found";
		}
		if (!ScrollWindow.allWindows.ContainsKey(pWindowID))
		{
			ScrollWindow scrollWindow = (ScrollWindow)Resources.Load("windows/" + text, typeof(ScrollWindow));
			if (scrollWindow == null)
			{
				Debug.LogError("Window with id " + text + " not found!");
				scrollWindow = (ScrollWindow)Resources.Load("windows/not_found", typeof(ScrollWindow));
			}
			ScrollWindow scrollWindow2 = Object.Instantiate<ScrollWindow>(scrollWindow, CanvasMain.instance.transformWindows);
			scrollWindow2.screen_id = pWindowID;
			scrollWindow2.name = pWindowID;
			scrollWindow2.create(false);
			if (!ScrollWindow.allWindows.ContainsKey(pWindowID))
			{
				ScrollWindow.allWindows.Add(pWindowID, scrollWindow2);
			}
		}
	}

	// Token: 0x0600102C RID: 4140 RVA: 0x0008E262 File Offset: 0x0008C462
	public static ScrollWindow get(string pWindowID)
	{
		ScrollWindow.checkWindowExist(pWindowID);
		return ScrollWindow.allWindows[pWindowID];
	}

	// Token: 0x0600102D RID: 4141 RVA: 0x0008E278 File Offset: 0x0008C478
	public void clickShow()
	{
		if (ScrollWindow.animationActive)
		{
			return;
		}
		LogText.log("Window Opened", this.screen_id, "");
		if (ScrollWindow.currentWindows.Contains(this) && ScrollWindow.waiting.Count == 0)
		{
			this.showSameWindow();
			return;
		}
		if (ScrollWindow.waiting.Contains(this))
		{
			ScrollWindow.moveAllToLeftAndRemove(true);
			return;
		}
		ScrollWindow.moveAllToLeftAndRemove(true);
		this.show(null, "right", "right", false);
	}

	// Token: 0x0600102E RID: 4142 RVA: 0x0008E2F0 File Offset: 0x0008C4F0
	public void clickShowLeft()
	{
		if (ScrollWindow.animationActive)
		{
			return;
		}
		LogText.log("Window Opened", this.screen_id, "");
		if (ScrollWindow.currentWindows.Contains(this) && ScrollWindow.waiting.Count == 0)
		{
			this.showSameWindow();
			return;
		}
		if (ScrollWindow.waiting.Contains(this))
		{
			ScrollWindow.moveAllToRightAndRemove(true);
			return;
		}
		ScrollWindow.moveAllToRightAndRemove(true);
		this.show(null, "left", "left", false);
	}

	// Token: 0x0600102F RID: 4143 RVA: 0x0008E366 File Offset: 0x0008C566
	public void forceShow()
	{
		this.show(null, "right", "right", true);
	}

	// Token: 0x06001030 RID: 4144 RVA: 0x0008E37C File Offset: 0x0008C57C
	public void show(ScrollWindow pWindowFrom = null, string pDistPosition = "right", string pStartPosition = "right", bool pSkipAnimation = false)
	{
		this.setActive(true, pDistPosition, pWindowFrom, pStartPosition, pSkipAnimation);
		if (this.screen_id == "PremiumPurchaseError")
		{
			Analytics.LogEvent("purchase_premium_error", true, true);
		}
		Analytics.trackWindow(this.screen_id);
		this.historyAction();
		this.specialWindowAction();
		DiscordTracker.trackWindow(this.screen_id);
		this.checkGradient();
	}

	// Token: 0x06001031 RID: 4145 RVA: 0x0008E3DB File Offset: 0x0008C5DB
	private void historyAction()
	{
		if (!this.historyActionEnabled)
		{
			return;
		}
		if (WindowHistory.list.Count >= 1)
		{
			this.backButton.SetActive(true);
		}
		else
		{
			this.backButton.SetActive(false);
		}
		WindowHistory.addIntoHistory(this);
	}

	// Token: 0x06001032 RID: 4146 RVA: 0x0008E414 File Offset: 0x0008C614
	public static void moveAllToLeftAndRemove(bool pWithAnimation = true)
	{
		foreach (ScrollWindow scrollWindow in ScrollWindow.waiting)
		{
			if (pWithAnimation)
			{
				scrollWindow.moveToLeft(true);
			}
			else
			{
				scrollWindow.activeToFalse();
			}
		}
		foreach (ScrollWindow scrollWindow2 in ScrollWindow.currentWindows)
		{
			if (pWithAnimation)
			{
				scrollWindow2.moveToLeft(true);
			}
			else
			{
				scrollWindow2.activeToFalse();
			}
		}
		ScrollWindow.waiting.Clear();
		ScrollWindow.currentWindows.Clear();
	}

	// Token: 0x06001033 RID: 4147 RVA: 0x0008E4D4 File Offset: 0x0008C6D4
	public static bool currentWindowsContains(string pWindowID)
	{
		using (List<ScrollWindow>.Enumerator enumerator = ScrollWindow.currentWindows.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.screen_id == pWindowID)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001034 RID: 4148 RVA: 0x0008E534 File Offset: 0x0008C734
	public static void moveAllToRightAndRemove(bool pWithAnimation = true)
	{
		foreach (ScrollWindow scrollWindow in ScrollWindow.waiting)
		{
			if (pWithAnimation)
			{
				scrollWindow.moveToRight(true);
			}
			else
			{
				scrollWindow.activeToFalse();
			}
		}
		foreach (ScrollWindow scrollWindow2 in ScrollWindow.currentWindows)
		{
			if (pWithAnimation)
			{
				scrollWindow2.moveToRight(true);
			}
			else
			{
				scrollWindow2.activeToFalse();
			}
		}
		ScrollWindow.waiting.Clear();
		ScrollWindow.currentWindows.Clear();
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x0008E5F4 File Offset: 0x0008C7F4
	private static void addToWaitingList(ScrollWindow pWindow)
	{
		ScrollWindow.waiting.Add(pWindow);
		foreach (ScrollWindow scrollWindow in ScrollWindow.waiting)
		{
			scrollWindow.moveToLeft(false);
		}
	}

	// Token: 0x06001036 RID: 4150 RVA: 0x0008E650 File Offset: 0x0008C850
	private static void removeFromWaitingList()
	{
		foreach (ScrollWindow scrollWindow in ScrollWindow.waiting)
		{
			scrollWindow.moveToRight(false);
		}
		ScrollWindow.setCanvasGroupEnabled(ScrollWindow.waiting[ScrollWindow.waiting.Count - 1].canvasGroup, true);
		ScrollWindow.waiting.RemoveAt(ScrollWindow.waiting.Count - 1);
	}

	// Token: 0x06001037 RID: 4151 RVA: 0x0008E6D8 File Offset: 0x0008C8D8
	public void moveToLeft(bool pRemove = false)
	{
		ScrollWindow.setCanvasGroupEnabled(this.canvasGroup, false);
		float pToX;
		if (pRemove)
		{
			pToX = base.transform.localPosition.x + this.getHidePosLeft();
			base.StartCoroutine(this.moveTween(pToX, "activeToFalse"));
			return;
		}
		pToX = base.transform.localPosition.x + this.getHidePosLeftHalf();
		base.StartCoroutine(this.moveTween(pToX, "waitingListCheck"));
	}

	// Token: 0x06001038 RID: 4152 RVA: 0x0008E74C File Offset: 0x0008C94C
	public void moveToRight(bool pRemove = false)
	{
		ScrollWindow.setCanvasGroupEnabled(this.canvasGroup, false);
		float pToX;
		if (pRemove)
		{
			pToX = base.transform.localPosition.x - this.getHidePosLeft();
			base.StartCoroutine(this.moveTween(pToX, "activeToFalse"));
			return;
		}
		pToX = base.transform.localPosition.x - this.getHidePosLeftHalf();
		base.StartCoroutine(this.moveTween(pToX, "waitingListCheck"));
	}

	// Token: 0x06001039 RID: 4153 RVA: 0x0008E7BF File Offset: 0x0008C9BF
	private void doElementsAction()
	{
	}

	// Token: 0x0600103A RID: 4154 RVA: 0x0008E7C1 File Offset: 0x0008C9C1
	public static void setCanvasGroupEnabled(CanvasGroup pGroup, bool pValue)
	{
		pGroup.interactable = pValue;
		pGroup.blocksRaycasts = pValue;
	}

	// Token: 0x0600103B RID: 4155 RVA: 0x0008E7D1 File Offset: 0x0008C9D1
	public void showSameWindow()
	{
		base.StartCoroutine(this.sameWindowTween(0f, "finishTween"));
		this.historyAction();
		this.specialWindowAction();
	}

	// Token: 0x0600103C RID: 4156 RVA: 0x0008E7F6 File Offset: 0x0008C9F6
	private void specialWindowAction()
	{
		if (this.screen_id == "kingdom")
		{
			base.GetComponent<KingdomWindow>().showInfo();
		}
	}

	// Token: 0x0600103D RID: 4157 RVA: 0x0008E818 File Offset: 0x0008CA18
	public void setActive(bool pActive, string pDistPosition = "right", ScrollWindow pWindowFrom = null, string pStartPosition = "right", bool pSkipAnimation = false)
	{
		WorldTip.hideNow();
		Tooltip.hideTooltip();
		this.currentWindow = pActive;
		if (this.unselectPower && MapBox.instance.selectedButtons.selectedButton != null && MapBox.instance.selectedButtons.selectedButton.godPower.unselectWhenWindow)
		{
			MapBox.instance.selectedButtons.unselectAll();
		}
		if (this.currentWindow)
		{
			ScrollWindow.setCanvasGroupEnabled(this.canvasGroup, true);
			base.gameObject.SetActive(true);
			if (pWindowFrom != null)
			{
				ScrollWindow.addToWaitingList(pWindowFrom);
			}
			ScrollWindow.addCurrentWindow(this);
			if (pStartPosition == "right")
			{
				base.transform.localPosition = new Vector3(this.getHidePosRight(), 10f, base.transform.localPosition.z);
			}
			if (pStartPosition == "left")
			{
				base.transform.localPosition = new Vector3(this.getHidePosLeft(), 10f, base.transform.localPosition.z);
			}
			if (pSkipAnimation)
			{
				this.finishTween();
				base.transform.localPosition = new Vector3(0f, 10f, 0f);
				return;
			}
			base.StartCoroutine(this.moveTween(0f, "finishTween"));
			return;
		}
		else
		{
			ScrollWindow.currentWindows.Remove(this);
			if (pDistPosition == "left")
			{
				float hidePosLeft = this.getHidePosLeft();
				base.StartCoroutine(this.moveTween(hidePosLeft, "activeToFalse"));
				return;
			}
			base.StartCoroutine(this.moveTween(this.getHidePosRight(), "activeToFalse"));
			return;
		}
	}

	// Token: 0x0600103E RID: 4158 RVA: 0x0008E9B7 File Offset: 0x0008CBB7
	protected IEnumerator moveTween(float pToX = 0f, string pCompleteCallback = "finishTween")
	{
		yield return new WaitForSeconds(0.02f);
		ScrollWindow.animationActive = true;
		float num = 0.35f;
		string text = "easeOutBack";
		if (pCompleteCallback == "activeToFalse")
		{
			num = 0.1f;
			text = "linear";
		}
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			new Vector3(pToX, 10f, base.gameObject.transform.localPosition.z),
			"easeType",
			text,
			"time",
			num,
			"islocal",
			true,
			"oncomplete",
			pCompleteCallback
		}));
		yield break;
	}

	// Token: 0x0600103F RID: 4159 RVA: 0x0008E9D4 File Offset: 0x0008CBD4
	protected IEnumerator sameWindowTween(float pToX = 0f, string pCompleteCallback = "finishTween")
	{
		yield return new WaitForSeconds(0.02f);
		ScrollWindow.animationActive = true;
		float num = 0.09f;
		base.transform.localPosition = new Vector3(0f, 16f, 0f);
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			new Vector3(0f, 10f, base.gameObject.transform.localPosition.z),
			"easeType",
			"easeOutBack",
			"time",
			num,
			"islocal",
			true,
			"oncomplete",
			pCompleteCallback
		}));
		yield break;
	}

	// Token: 0x06001040 RID: 4160 RVA: 0x0008E9EA File Offset: 0x0008CBEA
	public static void hideAllEvent(bool pWithAnimation = true)
	{
		ScrollWindow.moveAllToLeftAndRemove(pWithAnimation);
		WindowHistory.clear();
		MapBox.instance.controlsLockTimer = 0.1f;
	}

	// Token: 0x06001041 RID: 4161 RVA: 0x0008EA08 File Offset: 0x0008CC08
	public void clickHide(string pDirection = "right")
	{
		if (this.currentWindow != this)
		{
			return;
		}
		if (!ScrollWindow.canClickHide())
		{
			return;
		}
		this.hide(pDirection);
		MapBox.instance.selectedButtons.gameObject.SetActive(true);
		MapBox.instance.controlsLockTimer = 0.3f;
		WindowHistory.clear();
	}

	// Token: 0x06001042 RID: 4162 RVA: 0x0008EA5C File Offset: 0x0008CC5C
	public static bool canClickHide()
	{
		return !WorkshopUploadingWorldWindow.uploading;
	}

	// Token: 0x06001043 RID: 4163 RVA: 0x0008EA68 File Offset: 0x0008CC68
	public void hide(string pDirection = "right")
	{
		LogText.log("Window Hide", this.screen_id, "");
		this.setActive(false, pDirection, null, "right", false);
		ScrollWindow.setCanvasGroupEnabled(this.canvasGroup, false);
		if (ScrollWindow.waiting.Count > 0)
		{
			ScrollWindow.removeFromWaitingList();
		}
		this.openedFrom = null;
		Analytics.hideWindow();
	}

	// Token: 0x06001044 RID: 4164 RVA: 0x0008EAC3 File Offset: 0x0008CCC3
	public void waitingListCheck()
	{
		this.finishTween();
	}

	// Token: 0x06001045 RID: 4165 RVA: 0x0008EACB File Offset: 0x0008CCCB
	public void finishTween()
	{
		ScrollWindow.animationActive = false;
	}

	// Token: 0x06001046 RID: 4166 RVA: 0x0008EAD3 File Offset: 0x0008CCD3
	public void activeToFalse()
	{
		base.gameObject.SetActive(false);
		ScrollWindow.animationActive = false;
	}

	// Token: 0x06001047 RID: 4167 RVA: 0x0008EAE7 File Offset: 0x0008CCE7
	private void setCanvasAlpha(float pVal)
	{
		this.alpha = pVal;
		this.canvasGroup.alpha = pVal;
	}

	// Token: 0x06001048 RID: 4168 RVA: 0x0008EAFC File Offset: 0x0008CCFC
	public float getHidePosRight()
	{
		return this.canvas.GetComponent<RectTransform>().sizeDelta.x / 2f + this.bgRect.sizeDelta.x / 2f + this.bgRect.sizeDelta.x * 0.2f;
	}

	// Token: 0x06001049 RID: 4169 RVA: 0x0008EB54 File Offset: 0x0008CD54
	public float getHidePosLeft()
	{
		return -(this.canvas.GetComponent<RectTransform>().sizeDelta.x / 2f + this.bgRect.sizeDelta.x / 2f + this.bgRect.sizeDelta.x * 0.2f);
	}

	// Token: 0x0600104A RID: 4170 RVA: 0x0008EBAC File Offset: 0x0008CDAC
	public float getHidePosLeftHalf()
	{
		float num = (this.canvas.GetComponent<RectTransform>().sizeDelta.x - this.bgRect.sizeDelta.x) / 2f;
		return this.getHidePosLeft() + num / 2f;
	}

	// Token: 0x0600104B RID: 4171 RVA: 0x0008EBF4 File Offset: 0x0008CDF4
	public float getDistBetweenWindows()
	{
		return this.getHidePosLeftHalf();
	}

	// Token: 0x0600104C RID: 4172 RVA: 0x0008EBFC File Offset: 0x0008CDFC
	public void openConsole()
	{
		MapBox.instance.console.Show();
	}

	// Token: 0x04001346 RID: 4934
	public static bool animationActive = false;

	// Token: 0x04001347 RID: 4935
	public static List<ScrollWindow> currentWindows = new List<ScrollWindow>();

	// Token: 0x04001348 RID: 4936
	public static List<ScrollWindow> waiting = new List<ScrollWindow>();

	// Token: 0x04001349 RID: 4937
	private static Dictionary<string, ScrollWindow> allWindows = new Dictionary<string, ScrollWindow>();

	// Token: 0x0400134A RID: 4938
	public string screen_id = "screen";

	// Token: 0x0400134B RID: 4939
	public bool unselectPower = true;

	// Token: 0x0400134C RID: 4940
	internal Canvas canvas;

	// Token: 0x0400134D RID: 4941
	private float alpha;

	// Token: 0x0400134E RID: 4942
	private CanvasGroup canvasGroup;

	// Token: 0x0400134F RID: 4943
	internal bool currentWindow;

	// Token: 0x04001350 RID: 4944
	private RectTransform bgRect;

	// Token: 0x04001351 RID: 4945
	private ScrollWindow openedFrom;

	// Token: 0x04001352 RID: 4946
	public Text titleText;

	// Token: 0x04001353 RID: 4947
	public GameObject backButton;

	// Token: 0x04001354 RID: 4948
	public static string queuedWindow = "";

	// Token: 0x04001355 RID: 4949
	public ScrollRect scrollRect;

	// Token: 0x04001356 RID: 4950
	public Image scrollingGradient;

	// Token: 0x04001357 RID: 4951
	public RectTransform transform_content;

	// Token: 0x04001358 RID: 4952
	public RectTransform transform_viewport;

	// Token: 0x04001359 RID: 4953
	public RectTransform transform_scrollRect;

	// Token: 0x0400135A RID: 4954
	public GameObject[] destroyOnAwake;

	// Token: 0x0400135B RID: 4955
	public bool historyActionEnabled = true;
}
