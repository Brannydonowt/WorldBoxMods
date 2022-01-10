using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200019F RID: 415
public class Sfx : MonoBehaviour
{
	// Token: 0x06000983 RID: 2435 RVA: 0x000644C4 File Offset: 0x000626C4
	public static void timeout(string pName)
	{
		if (!PlayerConfig.dict["sound"].boolVal)
		{
			return;
		}
		List<SoundController> list = Sfx.dict[pName];
		list[0].timeout = list[0].timeoutInterval;
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x0006450C File Offset: 0x0006270C
	public static void play(string pName, bool pRestart = true, float pX = -1f, float pY = -1f)
	{
		if (!PlayerConfig.dict["sound"].boolVal)
		{
			return;
		}
		if (!Sfx.dict.ContainsKey(pName))
		{
			return;
		}
		List<SoundController> list = Sfx.dict[pName];
		if (list[0].timeout > 0f)
		{
			return;
		}
		if (list[0].ambientSound && !PlayerConfig.dict["sound_ambient"].boolVal)
		{
			return;
		}
		list[0].timeout = list[0].timeoutInterval;
		foreach (SoundController soundController in list)
		{
			if (!soundController.s.isPlaying)
			{
				soundController.play(pX, pY);
				return;
			}
		}
		if (list[0].curCopies < list[0].copies)
		{
			list[0].curCopies++;
			SoundController soundController2 = Object.Instantiate<SoundController>(list[0]);
			Sfx.listAll.Add(soundController2);
			list.Add(soundController2);
			soundController2.transform.parent = list[0].transform.parent;
			soundController2.play(pX, pY);
		}
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x00064660 File Offset: 0x00062860
	public static void fadeOut(string pName)
	{
		bool boolVal = PlayerConfig.dict["sound"].boolVal;
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x00064678 File Offset: 0x00062878
	private void Start()
	{
		Sfx.dict = new Dictionary<string, List<SoundController>>();
		Sfx.listAll = new List<SoundController>();
		for (int i = 0; i < base.transform.childCount; i++)
		{
			List<SoundController> list = new List<SoundController>();
			SoundController component = base.transform.GetChild(i).GetComponent<SoundController>();
			if (component.soundEnabled)
			{
				list.Add(component);
				Sfx.dict.Add(component.transform.name, list);
				Sfx.listAll.Add(component);
			}
		}
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x000646F8 File Offset: 0x000628F8
	private void Update()
	{
		float deltaTime = Time.deltaTime;
		foreach (SoundController soundController in Sfx.listAll)
		{
			soundController.update(deltaTime);
			if (soundController.s != null && !soundController.s.isPlaying)
			{
				soundController.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x04000C30 RID: 3120
	private static Dictionary<string, List<SoundController>> dict;

	// Token: 0x04000C31 RID: 3121
	private static List<SoundController> listAll;
}
