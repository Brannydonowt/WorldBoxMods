using System;
using System.Collections.Generic;
using System.IO;
using Proyecto26;
using UnityEngine;

// Token: 0x02000089 RID: 137
public class GameLoadedEvent : BaseMapObject
{
	// Token: 0x06000303 RID: 771 RVA: 0x00032B68 File Offset: 0x00030D68
	private void Awake()
	{
		LogText.log("GameLoadedEvent", "Awake", "st");
		Application.targetFrameRate = 60;
		if (Config.isComputer)
		{
			PlayerConfig.checkVsync();
		}
		LogText.log("GameLoadedEvent", "Awake", "en");
		this.setVersionData();
	}

	// Token: 0x06000304 RID: 772 RVA: 0x00032BB8 File Offset: 0x00030DB8
	private void setVersionData()
	{
		TextAsset textAsset = Resources.Load("texts/build_info") as TextAsset;
		try
		{
			Config.versionCodeText = textAsset.text.Split(new char[]
			{
				'$'
			})[0];
			Config.versionCodeDate = textAsset.text.Split(new char[]
			{
				'$'
			})[1];
		}
		catch (Exception)
		{
			if (textAsset != null)
			{
				Config.versionCodeText = textAsset.text;
				Config.versionCodeDate = "";
			}
		}
		try
		{
			RestClient.DefaultRequestHeaders["wb-build"] = (Config.versionCodeText ?? "na");
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000305 RID: 773 RVA: 0x00032C74 File Offset: 0x00030E74
	private void testId()
	{
		int num = 0;
		HashSet<string> hashSet = new HashSet<string>();
		for (int i = 0; i < 100000000; i++)
		{
			string text = Toolbox.generateID_old();
			if (hashSet.Contains(text))
			{
				num++;
			}
			hashSet.Add(text);
		}
		Debug.Log("SAME: " + num.ToString());
	}

	// Token: 0x06000306 RID: 774 RVA: 0x00032CCC File Offset: 0x00030ECC
	private void testBox()
	{
		List<int> list = new List<int>();
		list.Add(5000);
		List<int> pList = list;
		int num = 1000;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < num; i++)
		{
			int random = Toolbox.getRandom<int>(pList);
			int num4;
			if (Toolbox.randomBool())
			{
				num4 = random * 2;
			}
			else
			{
				num4 = random / 2;
			}
			num2 += random;
			num3 += num4;
		}
		num2 /= num;
		num3 /= num;
		Debug.Log(string.Concat(new string[]
		{
			"Average after ",
			num.ToString(),
			"tests :",
			num2.ToString(),
			" ",
			num3.ToString()
		}));
	}

	// Token: 0x06000307 RID: 775 RVA: 0x00032D7C File Offset: 0x00030F7C
	private void testShuffle()
	{
		for (int i = 0; i < 20; i++)
		{
			List<int> list = new List<int>();
			list.Add(0);
			list.Add(1);
			list.Add(2);
			list.Add(3);
			List<int> list2 = list;
			list2.Shuffle<int>();
			this.testString = this.testString + "f" + i.ToString() + ": ";
			for (int j = 0; j < list2.Count; j++)
			{
				this.testString = this.testString + list2[j].ToString() + " ";
			}
			this.testString += "\n";
		}
		this.testString += "\n";
		for (int k = 0; k < 20; k++)
		{
			List<int> list3 = new List<int>();
			list3.Add(0);
			list3.Add(1);
			list3.Add(2);
			list3.Add(3);
			List<int> list2 = list3;
			list2.ShuffleHalf<int>();
			this.testString = this.testString + "h" + k.ToString() + ": ";
			for (int l = 0; l < list2.Count; l++)
			{
				this.testString = this.testString + list2[l].ToString() + " ";
			}
			this.testString += "\n";
		}
		Debug.Log("SHUFFLE \n" + this.testString);
	}

	// Token: 0x06000308 RID: 776 RVA: 0x00032F10 File Offset: 0x00031110
	private void testEquals()
	{
		int num = 400000;
		ItemAsset itemAsset = new ItemAsset
		{
			id = Toolbox.generateID_old()
		};
		List<ItemAsset> list = new List<ItemAsset>();
		for (int i = 0; i < num; i++)
		{
			list.Add(new ItemAsset
			{
				id = Toolbox.generateID_old()
			});
		}
		list.Add(itemAsset);
		list.Shuffle<ItemAsset>();
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		int num2 = 0;
		for (int j = 0; j < list.Count; j++)
		{
			if (itemAsset == list[j])
			{
				num2++;
			}
		}
		Debug.Log("test 1: " + (Time.realtimeSinceStartup - realtimeSinceStartup).ToString() + " | " + num2.ToString());
		realtimeSinceStartup = Time.realtimeSinceStartup;
		num2 = 0;
		for (int k = 0; k < list.Count; k++)
		{
			if (itemAsset.id.Equals(list[k].id))
			{
				num2++;
			}
		}
		Debug.Log("test 2: " + (Time.realtimeSinceStartup - realtimeSinceStartup).ToString() + " | " + num2.ToString());
		realtimeSinceStartup = Time.realtimeSinceStartup;
		num2 = 0;
		for (int l = 0; l < list.Count; l++)
		{
			if (itemAsset.id == list[l].id)
			{
				num2++;
			}
		}
		Debug.Log("test 3: " + (Time.realtimeSinceStartup - realtimeSinceStartup).ToString() + " | " + num2.ToString());
		realtimeSinceStartup = Time.realtimeSinceStartup;
		num2 = 0;
		for (int m = 0; m < list.Count; m++)
		{
			if (itemAsset.id.CompareTo(list[m].id) == 0)
			{
				num2++;
			}
		}
		Debug.Log("test 4: " + (Time.realtimeSinceStartup - realtimeSinceStartup).ToString() + " | " + num2.ToString());
	}

	// Token: 0x06000309 RID: 777 RVA: 0x00033102 File Offset: 0x00031302
	private void testStreamingAssets()
	{
		File.OpenText(Application.streamingAssetsPath + "/modules/readme.txt").ReadToEnd();
	}

	// Token: 0x0600030A RID: 778 RVA: 0x00033120 File Offset: 0x00031320
	private void testTransform()
	{
		int num = 200000;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num2 = 0f;
		for (int i = 0; i < num; i++)
		{
			num2 += (float)base.gameObject.transform.GetHashCode();
		}
		Debug.Log("test 1... " + (Time.realtimeSinceStartup - realtimeSinceStartup).ToString());
		Debug.Log("test 1...res " + num2.ToString());
		realtimeSinceStartup = Time.realtimeSinceStartup;
		num2 = 0f;
		Transform transform = base.gameObject.transform;
		for (int j = 0; j < num; j++)
		{
			num2 += (float)transform.GetHashCode();
		}
		Debug.Log("test 2... " + (Time.realtimeSinceStartup - realtimeSinceStartup).ToString());
		Debug.Log("test 2...res " + num2.ToString());
	}

	// Token: 0x0600030B RID: 779 RVA: 0x00033200 File Offset: 0x00031400
	private void testSwitch()
	{
		int num = 200000;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		int num2 = 0;
		for (int i = 0; i < num; i++)
		{
			string text;
			if (Toolbox.randomBool())
			{
				text = "val0";
			}
			else
			{
				text = "val1";
			}
			if (text != null)
			{
				if (!(text == "val0"))
				{
					if (!(text == "val1"))
					{
					}
				}
				else
				{
					num2++;
				}
			}
		}
		Debug.Log("test 2... " + (Time.realtimeSinceStartup - realtimeSinceStartup).ToString());
		Debug.Log("test 2...suc " + num2.ToString());
		realtimeSinceStartup = Time.realtimeSinceStartup;
		num2 = 0;
		for (int j = 0; j < num; j++)
		{
			TestEnum testEnum;
			if (Toolbox.randomBool())
			{
				testEnum = TestEnum.Val0;
			}
			else
			{
				testEnum = TestEnum.Val1;
			}
			if (testEnum != TestEnum.Val0)
			{
				if (testEnum != TestEnum.Val1)
				{
				}
			}
			else
			{
				num2++;
			}
		}
		Debug.Log("test 1... " + (Time.realtimeSinceStartup - realtimeSinceStartup).ToString());
		Debug.Log("test 1...suc " + num2.ToString());
	}

	// Token: 0x0600030C RID: 780 RVA: 0x00033308 File Offset: 0x00031508
	internal override void create()
	{
		base.create();
		Config.LOAD_TIME_INIT = Time.realtimeSinceStartup;
		LogText.log("GameLoadedEvent", "create", "");
		LocalizedTextManager.instance.setLanguage(PlayerConfig.dict["language"].stringVal);
		this.world.startTheGame();
		GodPower.diagnostic();
		PlayerConfig.checkSettings();
		Config.updateCrashMetadata();
		MonoBehaviour.print("LOAD TIME INIT: " + Config.LOAD_TIME_INIT.ToString());
		MonoBehaviour.print("LOAD TIME CREATE: " + (Config.LOAD_TIME_CREATE - Config.LOAD_TIME_INIT).ToString());
		MonoBehaviour.print("LOAD TIME GENERATE: " + (Config.LOAD_TIME_GENERATE - Config.LOAD_TIME_CREATE).ToString());
	}

	// Token: 0x040004B7 RID: 1207
	private string testString = "";
}
