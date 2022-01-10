using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000216 RID: 534
public class UnitSpiteConstructorDebug : MonoBehaviour
{
	// Token: 0x06000BE3 RID: 3043 RVA: 0x000760A4 File Offset: 0x000742A4
	private void Update()
	{
		if (UnitSpriteConstructor._debug_id != 0L)
		{
			Sprite sprite = UnitSpriteConstructor.getSprite(UnitSpriteConstructor._debug_id);
			if (sprite != null)
			{
				base.GetComponent<Image>().sprite = sprite;
			}
		}
	}
}
