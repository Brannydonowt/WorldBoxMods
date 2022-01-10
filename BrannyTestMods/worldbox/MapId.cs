using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002A3 RID: 675
public class MapId : MonoBehaviour
{
	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x000894F8 File Offset: 0x000876F8
	public static string formattedMapId
	{
		get
		{
			if (!string.IsNullOrEmpty(MapId.mapId) && MapId.mapId.Length == 12)
			{
				return string.Concat(new string[]
				{
					"WB-",
					MapId.mapId.Substring(0, 4),
					"-",
					MapId.mapId.Substring(4, 4),
					"-",
					MapId.mapId.Substring(8, 4)
				});
			}
			return MapId.mapId;
		}
	}

	// Token: 0x040011D6 RID: 4566
	public Button continueButton;

	// Token: 0x040011D7 RID: 4567
	public InputField mapIdText;

	// Token: 0x040011D8 RID: 4568
	public Text statusText;

	// Token: 0x040011D9 RID: 4569
	public static string mapId;

	// Token: 0x040011DA RID: 4570
	public static Map map;

	// Token: 0x040011DB RID: 4571
	public Sprite buttonOn;

	// Token: 0x040011DC RID: 4572
	public Sprite buttonOff;
}
