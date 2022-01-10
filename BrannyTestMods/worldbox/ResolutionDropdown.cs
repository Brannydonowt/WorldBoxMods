using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002A0 RID: 672
public class ResolutionDropdown : MonoBehaviour
{
	// Token: 0x06000ECA RID: 3786 RVA: 0x0008911E File Offset: 0x0008731E
	private void Start()
	{
		this.dropdown = base.GetComponent<Dropdown>();
		this.PopulateDropdown(this.dropdown);
		this.dropdown.onValueChanged.AddListener(delegate(int <p0>)
		{
			this.DropdownValueChanged(this.dropdown);
		});
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x00089154 File Offset: 0x00087354
	private void OnEnable()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		this.dropdown = base.GetComponent<Dropdown>();
		this.PopulateDropdown(this.dropdown);
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x00089178 File Offset: 0x00087378
	private void DropdownValueChanged(Dropdown change)
	{
		Resolution[] resolutions = Screen.resolutions;
		if (ResolutionDropdown.options[change.value] == LocalizedTextManager.getText("windowed_mode", null))
		{
			PlayerConfig.setFullScreen(false, true);
		}
		else
		{
			foreach (Resolution resolution in resolutions)
			{
				if (resolution.ToString() == ResolutionDropdown.options[change.value])
				{
					if (!Screen.fullScreen)
					{
						PlayerConfig.setFullScreen(true, false);
					}
					Screen.SetResolution(resolution.width, resolution.height, true, resolution.refreshRate);
					break;
				}
			}
		}
		this.fullscreenOption.checkGameOption(false);
	}

	// Token: 0x06000ECD RID: 3789 RVA: 0x0008922C File Offset: 0x0008742C
	private void PopulateDropdown(Dropdown dropdown)
	{
		ResolutionDropdown.options.Clear();
		foreach (Resolution resolution in Screen.resolutions)
		{
			ResolutionDropdown.options.Add(resolution.ToString());
		}
		ResolutionDropdown.options.Add(LocalizedTextManager.getText("windowed_mode", null));
		dropdown.ClearOptions();
		ResolutionDropdown.options.Reverse();
		int num = ResolutionDropdown.options.IndexOf(Screen.currentResolution.ToString());
		if (!Screen.fullScreen)
		{
			num = ResolutionDropdown.options.IndexOf(LocalizedTextManager.getText("windowed_mode", null));
		}
		dropdown.AddOptions(ResolutionDropdown.options);
		if (num > -1)
		{
			dropdown.value = num;
		}
		else
		{
			ResolutionDropdown.options.Insert(0, Screen.currentResolution.ToString());
			dropdown.AddOptions(ResolutionDropdown.options);
			dropdown.value = ResolutionDropdown.options.IndexOf(Screen.currentResolution.ToString());
		}
		dropdown.RefreshShownValue();
	}

	// Token: 0x040011CC RID: 4556
	private Button button;

	// Token: 0x040011CD RID: 4557
	private Dropdown dropdown;

	// Token: 0x040011CE RID: 4558
	public OptionBool fullscreenOption;

	// Token: 0x040011CF RID: 4559
	private static List<string> options = new List<string>();
}
