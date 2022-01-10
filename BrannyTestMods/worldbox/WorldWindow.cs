using System;
using UnityEngine;

// Token: 0x02000278 RID: 632
public class WorldWindow : MonoBehaviour
{
	// Token: 0x06000DF8 RID: 3576 RVA: 0x00083AC6 File Offset: 0x00081CC6
	private void OnDisable()
	{
		MapBox.instance.mapStats.name = this.nameInput.textField.text;
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x00083AE7 File Offset: 0x00081CE7
	private void OnEnable()
	{
		if (MapBox.instance == null)
		{
			return;
		}
		if (MapBox.instance.mapStats == null)
		{
			return;
		}
		this.nameInput.setText(MapBox.instance.mapStats.name);
	}

	// Token: 0x040010C9 RID: 4297
	public NameInput nameInput;
}
