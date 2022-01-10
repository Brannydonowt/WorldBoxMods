using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000003 RID: 3
public class ChangeTextInHindi : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private void Start()
	{
		string text = base.gameObject.GetComponent<Text>().text;
		base.gameObject.GetComponent<Text>().SetHindiText(text);
	}
}
