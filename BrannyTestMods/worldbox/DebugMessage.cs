using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200020A RID: 522
public class DebugMessage : MonoBehaviour
{
	// Token: 0x06000BAA RID: 2986 RVA: 0x00070C56 File Offset: 0x0006EE56
	private void Start()
	{
		DebugMessage.instance = this;
		this.list = new List<DebugMessageFly>();
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x00070C6C File Offset: 0x0006EE6C
	public void moveAll(DebugMessageFly pMessage)
	{
		this.messagesToMove.Clear();
		foreach (DebugMessageFly debugMessageFly in this.list)
		{
			if (!(debugMessageFly == pMessage) && Toolbox.Dist(0f, debugMessageFly.transform.localPosition.y, 0f, pMessage.transform.localPosition.y) < 1f)
			{
				this.messagesToMove.Add(debugMessageFly);
			}
		}
		foreach (DebugMessageFly debugMessageFly2 in this.messagesToMove)
		{
			debugMessageFly2.moveUp();
		}
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x00070D50 File Offset: 0x0006EF50
	public DebugMessageFly getOldMessage(Transform pTransform)
	{
		foreach (DebugMessageFly debugMessageFly in this.list)
		{
			if (debugMessageFly.originTransform == pTransform)
			{
				return debugMessageFly;
			}
		}
		return null;
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x00070DB4 File Offset: 0x0006EFB4
	public static void log(Transform pTransofrm, string pMessage)
	{
		if (!Debug.isDebugBuild)
		{
			return;
		}
		if (!DebugMessage.log_enabled)
		{
			return;
		}
		DebugMessageFly oldMessage = DebugMessage.instance.getOldMessage(pTransofrm);
		if (oldMessage != null)
		{
			oldMessage.addString(pMessage);
			return;
		}
		TextMesh component = Object.Instantiate<GameObject>(DebugMessage.instance.prefab).gameObject.GetComponent<TextMesh>();
		component.gameObject.GetComponent<MeshRenderer>().sortingOrder = 100;
		component.transform.parent = DebugMessage.instance.transform;
		DebugMessageFly component2 = component.GetComponent<DebugMessageFly>();
		component2.originTransform = pTransofrm;
		component2.addString(pMessage);
		DebugMessage.instance.list.Add(component2);
	}

	// Token: 0x04000DD2 RID: 3538
	public GameObject prefab;

	// Token: 0x04000DD3 RID: 3539
	public static bool log_enabled;

	// Token: 0x04000DD4 RID: 3540
	public static DebugMessage instance;

	// Token: 0x04000DD5 RID: 3541
	public List<DebugMessageFly> list;

	// Token: 0x04000DD6 RID: 3542
	private List<DebugMessageFly> messagesToMove = new List<DebugMessageFly>();
}
