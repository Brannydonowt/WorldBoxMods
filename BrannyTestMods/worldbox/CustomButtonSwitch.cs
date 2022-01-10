using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000256 RID: 598
public class CustomButtonSwitch : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	// Token: 0x06000D03 RID: 3331 RVA: 0x0007CE60 File Offset: 0x0007B060
	private void Start()
	{
		switch (this.type)
		{
		case CustomButtonSwitchType.PerlinScale:
			this.valuesInt = new int[]
			{
				5,
				6,
				7,
				8,
				10,
				12,
				14,
				16,
				18,
				20,
				30
			};
			this.curIndex = 1;
			break;
		case CustomButtonSwitchType.RandomShapes:
			this.valuesInt = new int[]
			{
				0,
				5,
				10,
				15,
				20,
				25,
				40,
				50,
				70,
				100
			};
			this.curIndex = 1;
			break;
		case CustomButtonSwitchType.WaterLevel:
			this.valuesInt = new int[]
			{
				0,
				10,
				15,
				20,
				25,
				40,
				50,
				70,
				100
			};
			this.curIndex = 1;
			break;
		case CustomButtonSwitchType.MapSize:
			this.mapSizes = new string[]
			{
				"tiny",
				"small",
				"standard",
				"large",
				"huge",
				"gigantic",
				"titanic"
			};
			this.mapSizes = this.mapSizes.Append("iceberg").ToArray<string>();
			this.curIndex = 2;
			break;
		}
		this.initialized = true;
		this.updateVars();
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x0007CF67 File Offset: 0x0007B167
	private void OnEnable()
	{
		this.updateVars();
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x0007CF6F File Offset: 0x0007B16F
	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == 1)
		{
			this.decrease();
			return;
		}
		this.increase();
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x0007CF88 File Offset: 0x0007B188
	public void increase()
	{
		this.curIndex++;
		if (this.type == CustomButtonSwitchType.MapSize)
		{
			if (this.curIndex > this.mapSizes.Length - 1)
			{
				this.curIndex = 0;
			}
		}
		else if (this.curIndex > this.valuesInt.Length - 1)
		{
			this.curIndex = 0;
		}
		this.updateVars();
	}

	// Token: 0x06000D07 RID: 3335 RVA: 0x0007CFE8 File Offset: 0x0007B1E8
	public void decrease()
	{
		this.curIndex--;
		if (this.type == CustomButtonSwitchType.MapSize)
		{
			if (this.curIndex < 0)
			{
				this.curIndex = this.mapSizes.Length - 1;
			}
		}
		else if (this.curIndex < 0)
		{
			this.curIndex = this.valuesInt.Length - 1;
		}
		this.updateVars();
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x0007D048 File Offset: 0x0007B248
	private void updateVars()
	{
		if (!this.initialized)
		{
			return;
		}
		int num = 0;
		if (this.type != CustomButtonSwitchType.MapSize)
		{
			num = this.valuesInt[this.curIndex];
			this.text.text = (num.ToString() ?? "");
		}
		switch (this.type)
		{
		case CustomButtonSwitchType.PerlinScale:
			Config.customPerlinScale = num;
			break;
		case CustomButtonSwitchType.RandomShapes:
			Config.customRandomShapes = num;
			break;
		case CustomButtonSwitchType.WaterLevel:
			Config.customWaterLevel = num;
			break;
		case CustomButtonSwitchType.MapSize:
			Config.customMapSize = this.mapSizes[this.curIndex];
			this.text.text = LocalizedTextManager.getText("map_size_" + Config.customMapSize, null);
			break;
		}
		this.text.GetComponent<LocalizedText>().checkSpecialLanguages();
	}

	// Token: 0x04000FE2 RID: 4066
	private bool initialized;

	// Token: 0x04000FE3 RID: 4067
	private string[] mapSizes;

	// Token: 0x04000FE4 RID: 4068
	private int[] valuesInt;

	// Token: 0x04000FE5 RID: 4069
	private int curIndex;

	// Token: 0x04000FE6 RID: 4070
	public Text text;

	// Token: 0x04000FE7 RID: 4071
	public CustomButtonSwitchType type;
}
