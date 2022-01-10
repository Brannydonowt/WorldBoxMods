using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002B7 RID: 695
public class NameInput : MonoBehaviour
{
	// Token: 0x06000F46 RID: 3910 RVA: 0x0008A3FA File Offset: 0x000885FA
	private void Start()
	{
		this.textField.horizontalOverflow = 0;
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x0008A408 File Offset: 0x00088608
	private void OnEnable()
	{
		this.inputField.onEndEdit.AddListener(delegate(string <p0>)
		{
			this.CheckInput(this.inputField);
		});
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x0008A426 File Offset: 0x00088626
	private void OnDisable()
	{
		this.inputField.onEndEdit.RemoveAllListeners();
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x0008A438 File Offset: 0x00088638
	private void CheckInput(InputField input)
	{
		if (string.IsNullOrEmpty(input.text))
		{
			Debug.Log("Text Empty");
			input.text = this.LastValue;
			return;
		}
		if (input.text.Length > 0)
		{
			Debug.Log("Text has been entered");
		}
		this.LastValue = input.text;
	}

	// Token: 0x06000F4A RID: 3914 RVA: 0x0008A48D File Offset: 0x0008868D
	public void setText(string ptext)
	{
		this.textField.text = ptext;
		this.inputField.text = ptext;
		this.LastValue = ptext;
	}

	// Token: 0x0400122F RID: 4655
	public InputField inputField;

	// Token: 0x04001230 RID: 4656
	public Text textField;

	// Token: 0x04001231 RID: 4657
	private string LastValue;
}
