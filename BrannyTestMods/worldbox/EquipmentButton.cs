using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200028A RID: 650
public class EquipmentButton : MonoBehaviour
{
	// Token: 0x06000E5E RID: 3678 RVA: 0x00085D84 File Offset: 0x00083F84
	private void Start()
	{
		Button component = base.GetComponent<Button>();
		component.onClick.AddListener(new UnityAction(this.showTooltip));
		component.OnHover(new UnityAction(this.showHoverTooltip));
		component.OnHoverOut(new UnityAction(Tooltip.hideTooltip));
	}

	// Token: 0x06000E5F RID: 3679 RVA: 0x00085DD4 File Offset: 0x00083FD4
	internal void load(ActorEquipmentSlot pSlot)
	{
		this.slot = pSlot;
		if (this.slot == null)
		{
			return;
		}
		string text = "ui/Icons/items/icon_" + pSlot.data.id;
		if (pSlot.data.material != "base")
		{
			text = text + "_" + pSlot.data.material;
		}
		Sprite sprite = (Sprite)Resources.Load(text, typeof(Sprite));
		base.GetComponent<Image>().sprite = sprite;
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x00085E57 File Offset: 0x00084057
	private void showHoverTooltip()
	{
		if (!Config.tooltipsActive)
		{
			return;
		}
		this.showTooltip();
	}

	// Token: 0x06000E61 RID: 3681 RVA: 0x00085E68 File Offset: 0x00084068
	private void showTooltip()
	{
		Tooltip.info_equipment_slot = this.slot;
		Tooltip.instance.show(base.gameObject, "equipment", null, null);
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		ShortcutExtensions.DOKill(base.transform, false);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ShortcutExtensions.DOScale(base.transform, 0.8f, 0.1f), 26);
	}

	// Token: 0x04001137 RID: 4407
	private ActorEquipmentSlot slot;
}
