using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002A1 RID: 673
public class UiSizeDropdown : MonoBehaviour
{
	// Token: 0x06000ED1 RID: 3793 RVA: 0x00089365 File Offset: 0x00087565
	private void Start()
	{
		this.createDropdownOptions();
		this.renderDropdownValue(this.dropdown);
		this.dropdown.onValueChanged.AddListener(delegate(int <p0>)
		{
			this.DropdownValueChanged(this.dropdown);
		});
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x00089398 File Offset: 0x00087598
	private void createDropdownOptions()
	{
		this.dropdown = base.GetComponent<Dropdown>();
		this.dropdown.ClearOptions();
		this.options.Clear();
		foreach (string text in CanvasMain.instance.scale_sizes.Keys)
		{
			this.options.Add(text);
		}
		this.dropdown.AddOptions(this.options);
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x0008942C File Offset: 0x0008762C
	private void OnEnable()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		this.dropdown = base.GetComponent<Dropdown>();
		this.renderDropdownValue(this.dropdown);
	}

	// Token: 0x06000ED4 RID: 3796 RVA: 0x0008944E File Offset: 0x0008764E
	private void DropdownValueChanged(Dropdown change)
	{
		PlayerConfig.dict["ui_size"].stringVal = this.options[change.value];
		PlayerConfig.saveData();
		this.renderDropdownValue(change);
		CanvasMain.instance.resizeUI();
	}

	// Token: 0x06000ED5 RID: 3797 RVA: 0x0008948C File Offset: 0x0008768C
	private void renderDropdownValue(Dropdown dropdown)
	{
		string stringVal = PlayerConfig.dict["ui_size"].stringVal;
		dropdown.value = this.options.IndexOf(stringVal);
		dropdown.RefreshShownValue();
	}

	// Token: 0x040011D0 RID: 4560
	private Button button;

	// Token: 0x040011D1 RID: 4561
	private Dropdown dropdown;

	// Token: 0x040011D2 RID: 4562
	private List<string> options = new List<string>();
}
