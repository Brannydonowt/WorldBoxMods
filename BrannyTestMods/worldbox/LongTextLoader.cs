using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200029D RID: 669
public class LongTextLoader : MonoBehaviour
{
	// Token: 0x06000EBB RID: 3771 RVA: 0x000884F4 File Offset: 0x000866F4
	private void Start()
	{
		this.m_text = base.GetComponent<Text>();
		this.create();
		this.finish();
	}

	// Token: 0x06000EBC RID: 3772 RVA: 0x00088510 File Offset: 0x00086710
	private void finish()
	{
		RectTransform component = this.m_text.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(component.sizeDelta.x, this.m_text.preferredHeight + 10f);
		RectTransform component2 = base.transform.parent.GetComponent<RectTransform>();
		component2.sizeDelta = new Vector2(component2.sizeDelta.x, component.sizeDelta.y);
		float num = -component2.transform.localPosition.y;
		component2.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, component.sizeDelta.y + 20f + num);
	}

	// Token: 0x06000EBD RID: 3773 RVA: 0x000885C0 File Offset: 0x000867C0
	public virtual void create()
	{
		try
		{
			this.m_text.text = this.textAsset.text;
		}
		catch (Exception)
		{
			Debug.LogError("LongTextLoader: Text File is too long");
		}
	}

	// Token: 0x0400119C RID: 4508
	public TextAsset textAsset;

	// Token: 0x0400119D RID: 4509
	protected Text m_text;
}
