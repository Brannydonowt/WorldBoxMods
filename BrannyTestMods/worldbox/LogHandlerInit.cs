using System;
using UnityEngine;

// Token: 0x02000211 RID: 529
public class LogHandlerInit : MonoBehaviour
{
	// Token: 0x06000BD1 RID: 3025 RVA: 0x00075DD4 File Offset: 0x00073FD4
	private void Awake()
	{
		try
		{
			LogHandler.init();
		}
		catch (Exception)
		{
		}
	}
}
