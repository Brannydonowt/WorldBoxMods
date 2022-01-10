using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000293 RID: 659
public class WorldLogWindow : MonoBehaviour
{
	// Token: 0x06000E8B RID: 3723 RVA: 0x00087590 File Offset: 0x00085790
	private void OnEnable()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		if (WorldLogWindow.spriteList == null)
		{
			WorldLogWindow.spriteList = new Dictionary<string, Sprite>();
		}
		this.Clear();
		for (int i = 0; i < WorldLog.instance.list.Count; i++)
		{
			this.showElement(i);
		}
		this.transformContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, (float)(this.elements.Count * 24 + 10));
	}

	// Token: 0x06000E8C RID: 3724 RVA: 0x00087609 File Offset: 0x00085809
	private void OnDisable()
	{
		this.Clear();
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x00087614 File Offset: 0x00085814
	private void Clear()
	{
		while (this.elements.Count > 0)
		{
			Component component = this.elements[this.elements.Count - 1];
			this.elements.RemoveAt(this.elements.Count - 1);
			Object.Destroy(component.gameObject);
		}
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x0008766C File Offset: 0x0008586C
	private void showElement(int pIndex)
	{
		WorldLogElement worldLogElement = Object.Instantiate<WorldLogElement>(this.elementPrefab, this.transformContent);
		this.elements.Add(worldLogElement);
		worldLogElement.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, (float)(-(float)(this.elements.Count * 24 - 4)));
		WorldLogMessage worldLogMessage = WorldLog.instance.list[pIndex];
		worldLogElement.date.text = worldLogMessage.date;
		string formatedText = ref worldLogMessage.getFormatedText(worldLogElement.description, false, true);
		bool active = ref worldLogMessage.hasLocation();
		bool flag = ref worldLogMessage.hasFollowLocation();
		worldLogElement.message = worldLogMessage;
		if (flag)
		{
			worldLogElement.follow.SetActive(flag);
			worldLogElement.locate.SetActive(!flag);
		}
		else
		{
			worldLogElement.follow.SetActive(flag);
			worldLogElement.locate.SetActive(active);
		}
		worldLogElement.description.text = (formatedText ?? "");
		worldLogElement.description.GetComponent<LocalizedText>().checkTextFont();
		if (worldLogMessage.icon != "")
		{
			if (!WorldLogWindow.spriteList.ContainsKey(worldLogMessage.icon))
			{
				WorldLogWindow.spriteList[worldLogMessage.icon] = (Sprite)Resources.Load("ui/Icons/" + worldLogMessage.icon, typeof(Sprite));
			}
			worldLogElement.icon.sprite = WorldLogWindow.spriteList[worldLogMessage.icon];
		}
		else
		{
			worldLogElement.icon.gameObject.SetActive(false);
		}
		worldLogElement.description.GetComponent<LocalizedText>().checkSpecialLanguages();
		if (LocalizedTextManager.isRTLLang())
		{
			worldLogElement.description.alignment = 5;
			return;
		}
		worldLogElement.description.alignment = 3;
	}

	// Token: 0x04001168 RID: 4456
	private List<WorldLogElement> elements = new List<WorldLogElement>();

	// Token: 0x04001169 RID: 4457
	public WorldLogElement elementPrefab;

	// Token: 0x0400116A RID: 4458
	public Transform transformContent;

	// Token: 0x0400116B RID: 4459
	private static Dictionary<string, Sprite> spriteList;
}
