using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000259 RID: 601
public class HoveringBgIconManager : MonoBehaviour
{
	// Token: 0x06000D0F RID: 3343 RVA: 0x0007D228 File Offset: 0x0007B428
	private void Awake()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			this.places.Add(child);
			child.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0007D270 File Offset: 0x0007B470
	private void OnEnable()
	{
		foreach (Transform transform in this.icons)
		{
			Object.Destroy(transform.gameObject);
		}
		this.icons.Clear();
		float num = Toolbox.randomFloat(0f, 360f);
		string[] list = this.texture_sprite.Split(new char[]
		{
			','
		});
		string random = list.GetRandom<string>();
		foreach (Transform transform2 in this.places)
		{
			random = list.GetRandom<string>();
			HoveringIcon hoveringIcon = Object.Instantiate<HoveringIcon>(this.prefab, transform2.position, Quaternion.identity, base.transform);
			hoveringIcon.transform.localPosition = transform2.transform.localPosition;
			hoveringIcon.image.sprite = (Sprite)Resources.Load("ui/Icons/" + random, typeof(Sprite));
			float num2 = Toolbox.randomFloat(0.4f, 1f);
			hoveringIcon.image.color = new Color(num2, num2, num2, 1f);
			hoveringIcon.transform.localScale = new Vector3(num2, num2, num2);
			this.icons.Add(hoveringIcon.transform);
			num += Toolbox.randomFloat(20f, 130f);
			hoveringIcon.transform.eulerAngles = new Vector3(0f, 0f, num);
		}
	}

	// Token: 0x04000FE9 RID: 4073
	public HoveringIcon prefab;

	// Token: 0x04000FEA RID: 4074
	private List<Transform> places = new List<Transform>();

	// Token: 0x04000FEB RID: 4075
	private List<Transform> icons = new List<Transform>();

	// Token: 0x04000FEC RID: 4076
	public string texture_sprite;
}
