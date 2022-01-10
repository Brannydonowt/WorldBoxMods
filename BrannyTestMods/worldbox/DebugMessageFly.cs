using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200020B RID: 523
public class DebugMessageFly : MonoBehaviour
{
	// Token: 0x06000BB0 RID: 2992 RVA: 0x00070E67 File Offset: 0x0006F067
	private void Awake()
	{
		this.textMesh = base.GetComponent<TextMesh>();
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x00070E78 File Offset: 0x0006F078
	public void addString(string pText)
	{
		if (this.textMesh.color.a < 0.3f)
		{
			this.listString.Clear();
		}
		else if (this.listString.Count > 20)
		{
			this.listString.RemoveAt(0);
		}
		this.listString.Add(pText);
		Vector3 localPosition = new Vector3(this.originTransform.localPosition.x, this.originTransform.localPosition.y);
		base.transform.localPosition = localPosition;
		string text = "";
		foreach (string str in this.listString)
		{
			text = text + str + "\n";
		}
		this.textMesh.text = text;
		Color color = this.textMesh.color;
		color.a = 1f;
		this.textMesh.color = color;
	}

	// Token: 0x06000BB2 RID: 2994 RVA: 0x00070F88 File Offset: 0x0006F188
	public void moveUp()
	{
		Vector3 localPosition = base.transform.localPosition;
		localPosition.y += 3f;
		base.transform.localPosition = localPosition;
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x00070FC0 File Offset: 0x0006F1C0
	private void Update()
	{
		Vector3 localScale = base.transform.localScale;
		localScale.x += 2f * Time.deltaTime;
		if (localScale.x > 1f)
		{
			localScale.x = 1f;
		}
		base.transform.localScale = localScale;
		Vector3 localPosition = base.transform.localPosition;
		localPosition.y += 0.5f * Time.deltaTime;
		base.transform.localPosition = localPosition;
		Color color = this.textMesh.color;
		color.a -= 0.3f * Time.deltaTime;
		this.textMesh.color = color;
		if (color.a <= 0f)
		{
			Object.Destroy(base.gameObject);
			DebugMessage.instance.list.Remove(this);
		}
	}

	// Token: 0x04000DD7 RID: 3543
	private List<string> listString = new List<string>();

	// Token: 0x04000DD8 RID: 3544
	public Transform originTransform;

	// Token: 0x04000DD9 RID: 3545
	private TextMesh textMesh;
}
