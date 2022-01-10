using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000288 RID: 648
public class HistoryHud : MonoBehaviour
{
	// Token: 0x1700003D RID: 61
	// (get) Token: 0x06000E43 RID: 3651 RVA: 0x000855C5 File Offset: 0x000837C5
	// (set) Token: 0x06000E44 RID: 3652 RVA: 0x00085603 File Offset: 0x00083803
	public static bool raycastOn
	{
		get
		{
			return !MoveCamera.cameraDragRun && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2) && !MapBox.controlsLocked() && !MapBox.instance.selectedButtons.isPowerSelected() && HistoryHud._raycastOn;
		}
		set
		{
			HistoryHud._raycastOn = value;
		}
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x0008560B File Offset: 0x0008380B
	private void Awake()
	{
		HistoryHud.instance = this;
		HistoryHud.contentGroup = base.transform.Find("Scroll View/Viewport/Content");
		HistoryHud.parkedGroup = base.transform.Find("Scroll View/Viewport/Parked");
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x0008563D File Offset: 0x0008383D
	private void Start()
	{
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x0008563F File Offset: 0x0008383F
	private void OnEnable()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		HistoryHud.contentGroup.gameObject.GetComponent<RectTransform>().SetTop(0f);
		HistoryHud.contentGroup.gameObject.GetComponent<RectTransform>().SetLeft(0f);
	}

	// Token: 0x06000E48 RID: 3656 RVA: 0x0008567B File Offset: 0x0008387B
	private void OnDisable()
	{
		HistoryHud.Clear();
	}

	// Token: 0x06000E49 RID: 3657 RVA: 0x00085682 File Offset: 0x00083882
	private void Update()
	{
		this.checkEnabled();
		if (this.recalc)
		{
			this.recalc = false;
			this.recalcPositions();
		}
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x000856A1 File Offset: 0x000838A1
	public static void disableRaycasts()
	{
		HistoryHud.raycastOn = false;
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x000856A9 File Offset: 0x000838A9
	public static void enableRaycasts()
	{
		HistoryHud.raycastOn = true;
	}

	// Token: 0x06000E4C RID: 3660 RVA: 0x000856B4 File Offset: 0x000838B4
	private float recalcPositions()
	{
		if (HistoryHud.historyItems.Count == 0)
		{
			return HistoryHud.startPosition;
		}
		float num = HistoryHud.startPosition;
		float num2 = HistoryHud.startPosition;
		int num3 = 0;
		if (HistoryHud.historyItems.Count > 10)
		{
			num3 = HistoryHud.historyItems.Count - 10;
		}
		int i = 0;
		while (i < HistoryHud.historyItems.Count)
		{
			if (num3 > 0)
			{
				if (HistoryHud.historyItems[i].targetBottom != (float)(num3 * -15))
				{
					HistoryHud.historyItems[i].moveToAndDestroy((float)(num3 * -15));
				}
				num3--;
				goto IL_B7;
			}
			if (!HistoryHud.historyItems[i].removing)
			{
				if (HistoryHud.historyItems[i].targetBottom != num)
				{
					HistoryHud.historyItems[i].moveTo(num);
				}
				num += 15f;
				goto IL_B7;
			}
			IL_D3:
			i++;
			continue;
			IL_B7:
			num2 = -HistoryHud.historyItems[i].GetComponent<RectTransform>().offsetMax.y;
			goto IL_D3;
		}
		if (num2 >= num)
		{
			return num2 + 15f;
		}
		return num;
	}

	// Token: 0x06000E4D RID: 3661 RVA: 0x000857B8 File Offset: 0x000839B8
	private bool checkEnabled()
	{
		if (!PlayerConfig.optionBoolEnabled("history_log"))
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			return false;
		}
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		return true;
	}

	// Token: 0x06000E4E RID: 3662 RVA: 0x00085806 File Offset: 0x00083A06
	public void newHistory(ref WorldLogMessage pMessage)
	{
		if (!this.checkEnabled())
		{
			return;
		}
		this.newText(ref pMessage);
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000E4F RID: 3663 RVA: 0x00085824 File Offset: 0x00083A24
	public static void makeInactive(HistoryHudItem historyItem)
	{
		HistoryHud.parkedItems.Add(historyItem.gameObject);
		HistoryHud.historyItems.Remove(historyItem);
		historyItem.active = false;
		historyItem.gameObject.SetActive(false);
		historyItem.transform.SetParent(HistoryHud.parkedGroup);
		HistoryHud.instance.recalc = true;
	}

	// Token: 0x06000E50 RID: 3664 RVA: 0x0008587B File Offset: 0x00083A7B
	public static void Clear()
	{
		while (HistoryHud.historyItems.Count > 0)
		{
			HistoryHud.makeInactive(HistoryHud.historyItems[0]);
		}
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x0008589C File Offset: 0x00083A9C
	public GameObject GetObject()
	{
		GameObject gameObject;
		if (HistoryHud.parkedItems.Count > 0)
		{
			gameObject = HistoryHud.parkedItems[HistoryHud.parkedItems.Count - 1];
			HistoryHud.parkedItems.Remove(gameObject);
		}
		else
		{
			gameObject = Object.Instantiate<GameObject>(this.templateObj.gameObject);
		}
		return gameObject;
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x000858F0 File Offset: 0x00083AF0
	private void newText(ref WorldLogMessage message)
	{
		GameObject @object = this.GetObject();
		@object.name = "HistoryItem " + (HistoryHud.historyItems.Count + 1).ToString();
		@object.SetActive(true);
		@object.transform.Find("CText").GetComponent<Text>();
		@object.transform.SetParent(HistoryHud.contentGroup);
		RectTransform component = @object.GetComponent<RectTransform>();
		component.localScale = Vector3.one;
		component.localPosition = Vector3.zero;
		component.SetLeft(0f);
		float num = this.recalcPositions();
		component.SetTop(num);
		component.sizeDelta = new Vector2(component.sizeDelta.x, 15f);
		@object.GetComponent<HistoryHudItem>().targetBottom = num;
		@object.GetComponent<HistoryHudItem>().SetMessage(ref message);
		HistoryHud.historyItems.Add(@object.GetComponent<HistoryHudItem>());
		this.recalc = true;
	}

	// Token: 0x0400111D RID: 4381
	public static HistoryHud instance;

	// Token: 0x0400111E RID: 4382
	public GameObject templateObj;

	// Token: 0x0400111F RID: 4383
	public static List<HistoryHudItem> historyItems = new List<HistoryHudItem>();

	// Token: 0x04001120 RID: 4384
	public static List<GameObject> parkedItems = new List<GameObject>();

	// Token: 0x04001121 RID: 4385
	public static Transform contentGroup;

	// Token: 0x04001122 RID: 4386
	public static Transform parkedGroup;

	// Token: 0x04001123 RID: 4387
	private const int HISTORY_ITEM_SIZE = 15;

	// Token: 0x04001124 RID: 4388
	private const int MAX_HISTORY_ITEMS = 10;

	// Token: 0x04001125 RID: 4389
	public bool recalc;

	// Token: 0x04001126 RID: 4390
	private static bool _raycastOn = true;

	// Token: 0x04001127 RID: 4391
	private static float startPosition = 0f;
}
