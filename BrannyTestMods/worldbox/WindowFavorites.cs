using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000285 RID: 645
public class WindowFavorites : MonoBehaviour
{
	// Token: 0x06000E33 RID: 3635 RVA: 0x000850AC File Offset: 0x000832AC
	private void OnEnable()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		this.clear();
		foreach (Actor actor in MapBox.instance.units)
		{
			if (actor.data.alive && actor.data.favorite)
			{
				this.actors.Add(actor);
			}
		}
		int num = 0;
		foreach (Actor pActor in this.actors)
		{
			this.showElement(pActor, num);
			num++;
		}
		this.transformContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, (float)this.elements.Count * this.offset + 20f);
	}

	// Token: 0x06000E34 RID: 3636 RVA: 0x000851A8 File Offset: 0x000833A8
	private void clear()
	{
		this.actors.Clear();
		while (this.elements.Count > 0)
		{
			Component component = this.elements[this.elements.Count - 1];
			this.elements.RemoveAt(this.elements.Count - 1);
			Object.Destroy(component.gameObject);
		}
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x0008520C File Offset: 0x0008340C
	private void showElement(Actor pActor, int pIndex)
	{
		WindowElementFavoriteUnit windowElementFavoriteUnit = Object.Instantiate<WindowElementFavoriteUnit>(this.elementPrefab, this.transformContent);
		this.elements.Add(windowElementFavoriteUnit);
		windowElementFavoriteUnit.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, -((float)this.elements.Count * this.offset - 20f));
		windowElementFavoriteUnit.show(pActor);
	}

	// Token: 0x0400110D RID: 4365
	private List<WindowElementFavoriteUnit> elements = new List<WindowElementFavoriteUnit>();

	// Token: 0x0400110E RID: 4366
	public WindowElementFavoriteUnit elementPrefab;

	// Token: 0x0400110F RID: 4367
	public Transform transformContent;

	// Token: 0x04001110 RID: 4368
	private List<Actor> actors = new List<Actor>();

	// Token: 0x04001111 RID: 4369
	private float offset = 44f;
}
