using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020001A7 RID: 423
public static class ButtonExtensions
{
	// Token: 0x060009A4 RID: 2468 RVA: 0x00065118 File Offset: 0x00063318
	public static void OnHover(this Button button, UnityAction call)
	{
		if (!Input.mousePresent)
		{
			return;
		}
		EventTrigger eventTrigger = button.gameObject.AddComponent<EventTrigger>();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = 0;
		entry.callback.AddListener(delegate(BaseEventData e)
		{
			call();
		});
		eventTrigger.triggers.Add(entry);
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x00065174 File Offset: 0x00063374
	public static void OnHoverOut(this Button button, UnityAction call)
	{
		if (!Input.mousePresent)
		{
			return;
		}
		EventTrigger eventTrigger = button.gameObject.AddComponent<EventTrigger>();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = 1;
		entry.callback.AddListener(delegate(BaseEventData e)
		{
			call();
		});
		eventTrigger.triggers.Add(entry);
	}
}
