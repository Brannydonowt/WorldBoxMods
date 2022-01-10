using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001DB RID: 475
internal class WindowKeyboardHighlighter : MonoBehaviour
{
	// Token: 0x06000AB6 RID: 2742 RVA: 0x0006B543 File Offset: 0x00069743
	private void OnEnable()
	{
		if (!TouchScreenKeyboard.isSupported)
		{
			Object.Destroy(base.GetComponent<WindowKeyboardHighlighter>());
			return;
		}
		this.findInputFields();
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x0006B560 File Offset: 0x00069760
	private void findInputFields()
	{
		this.noInputs = 0;
		this.rescan = false;
		WindowKeyboardHighlighter.inputFields.Clear();
		foreach (Transform transform in base.transform.GetComponentsInChildren<Transform>())
		{
			if (transform.gameObject.GetComponent<InputField>() != null)
			{
				WindowKeyboardHighlighter.inputFields.Add(transform.gameObject.GetComponent<InputField>());
			}
		}
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x0006B5CB File Offset: 0x000697CB
	private void OnDisable()
	{
		WindowKeyboardHighlighter.inputFields.Clear();
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x0006B5D8 File Offset: 0x000697D8
	private void up(InputField pInput)
	{
		int num = Screen.height / 4 * 3;
		if (pInput.transform.position.y >= (float)num)
		{
			return;
		}
		Vector3 localPosition = base.gameObject.transform.localPosition;
		localPosition.y += 10f;
		base.transform.localPosition = localPosition;
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x0006B634 File Offset: 0x00069834
	private void down()
	{
		if (base.gameObject.transform.localPosition.y <= 10f)
		{
			return;
		}
		Vector3 localPosition = base.gameObject.transform.localPosition;
		localPosition.y -= 5f;
		base.transform.localPosition = localPosition;
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x0006B68C File Offset: 0x0006988C
	private void Update()
	{
		this.anyFocused = false;
		if (WindowKeyboardHighlighter.inputFields.Count == 0)
		{
			this.noInputs++;
		}
		foreach (InputField inputField in WindowKeyboardHighlighter.inputFields)
		{
			if (!inputField.gameObject.activeInHierarchy)
			{
				this.rescan = true;
			}
			if (inputField.isFocused)
			{
				this.up(inputField);
				this.anyFocused = true;
			}
		}
		if (!this.anyFocused)
		{
			this.down();
		}
		if (this.rescan || this.noInputs > 60)
		{
			this.findInputFields();
		}
	}

	// Token: 0x04000D41 RID: 3393
	private static List<InputField> inputFields = new List<InputField>();

	// Token: 0x04000D42 RID: 3394
	private bool rescan;

	// Token: 0x04000D43 RID: 3395
	private int noInputs;

	// Token: 0x04000D44 RID: 3396
	private bool anyFocused;
}
