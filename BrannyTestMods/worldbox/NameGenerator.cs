using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x02000185 RID: 389
public class NameGenerator
{
	// Token: 0x060008EB RID: 2283 RVA: 0x0005F2DA File Offset: 0x0005D4DA
	public static void init()
	{
		if (NameGenerator.initiated)
		{
			return;
		}
		NameGenerator.initiated = true;
		NameGenerator.loadProfanityFilter();
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x0005F2F0 File Offset: 0x0005D4F0
	private static void loadProfanityFilter()
	{
		if (NameGenerator.profanity != null && NameGenerator.profanity.Count > 0)
		{
			return;
		}
		try
		{
			NameGenerator.profanity = new Dictionary<char, List<string>>();
			string[] array = Regex.Split((Resources.Load("blacklisted_names") as TextAsset).text, "\r\n?|\n", 16);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].ToLower();
				foreach (char c2 in (from c in text.Distinct<char>()
				where char.IsLetter(c)
				select c).ToArray<char>())
				{
					if (!NameGenerator.profanity.ContainsKey(c2))
					{
						NameGenerator.profanity[c2] = new List<string>();
					}
					NameGenerator.profanity[c2].Add(text);
				}
			}
		}
		catch (Exception)
		{
			Debug.LogError("Error when loading blacklist");
		}
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x0005F3F4 File Offset: 0x0005D5F4
	public static string getName(string pAssetID, ActorGender pGender = ActorGender.Male)
	{
		NameGenerator.init();
		NameGeneratorAsset nameGeneratorAsset = AssetManager.nameGenerator.get(pAssetID);
		NameGenerator.curConsonants = 0;
		NameGenerator.curVowels = 0;
		string text = NameGenerator.generateNameFromTemplate(nameGeneratorAsset);
		if (pGender == ActorGender.Female)
		{
			string strB = text.Substring(text.Length - 1, 1);
			bool flag = false;
			string[] vowels = nameGeneratorAsset.vowels;
			for (int i = 0; i < vowels.Length; i++)
			{
				if (vowels[i].CompareTo(strB) == 0)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				text += Toolbox.getRandom<string>(nameGeneratorAsset.vowels);
			}
		}
		return text;
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x0005F480 File Offset: 0x0005D680
	private static bool checkBlackList(string pName)
	{
		pName = pName.ToLower();
		foreach (char c2 in (from c in pName.Distinct<char>()
		where char.IsLetter(c)
		select c).ToArray<char>())
		{
			if (NameGenerator.profanity.ContainsKey(c2))
			{
				for (int j = 0; j < NameGenerator.profanity[c2].Count; j++)
				{
					if (pName.Contains(NameGenerator.profanity[c2][j]))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x0005F51B File Offset: 0x0005D71B
	private static string firstToUpper(string pString)
	{
		string str = pString.Substring(0, 1).ToUpper();
		pString = pString.Substring(1, pString.Length - 1);
		return str + pString;
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x0005F541 File Offset: 0x0005D741
	private static string addVowel(string[] pList, bool pUppercase = false)
	{
		NameGenerator.curConsonants = 0;
		NameGenerator.curVowels++;
		if (pUppercase)
		{
			return NameGenerator.firstToUpper(Toolbox.getRandom<string>(pList));
		}
		return Toolbox.getRandom<string>(pList);
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x0005F56C File Offset: 0x0005D76C
	private static string addEnding(NameGeneratorAsset pTemplate, string pName)
	{
		string text = Toolbox.getRandom<string>(pTemplate.parts);
		if (NameGenerator.isConsonant(text.Substring(0, 1)) && NameGenerator.isConsonant(pName.Substring(pName.Length - 1, 1)))
		{
			text = NameGenerator.addVowel(pTemplate.vowels, false) + text;
		}
		else if (!NameGenerator.isConsonant(text.Substring(0, 1)) && !NameGenerator.isConsonant(pName.Substring(pName.Length - 1, 1)))
		{
			text = NameGenerator.addConsonant(pTemplate.consonants, false) + text;
		}
		return text;
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x0005F5F8 File Offset: 0x0005D7F8
	private static string addConsonant(string[] pList, bool pUppercase = false)
	{
		NameGenerator.curConsonants++;
		NameGenerator.curVowels = 0;
		if (pUppercase)
		{
			return NameGenerator.firstToUpper(Toolbox.getRandom<string>(pList));
		}
		return Toolbox.getRandom<string>(pList);
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x0005F624 File Offset: 0x0005D824
	private static string addPart(string[] pArray, bool pUppercase = false)
	{
		string text = Toolbox.getRandom<string>(pArray);
		if (NameGenerator.isConsonant(text.Substring(text.Length - 1, 1)))
		{
			NameGenerator.curConsonants++;
			NameGenerator.curVowels = 0;
		}
		else
		{
			NameGenerator.curConsonants = 0;
			NameGenerator.curVowels++;
		}
		if (pUppercase)
		{
			text = NameGenerator.firstToUpper(text);
		}
		return text;
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x0005F67F File Offset: 0x0005D87F
	private static bool isConsonant(string pString)
	{
		return NameGenerator.consonants_all.Contains(pString);
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x0005F68C File Offset: 0x0005D88C
	public static string generateNameFromTemplate(NameGeneratorAsset pAsset)
	{
		NameGenerator.dict_splitted_items.Clear();
		NameGenerator.curConsonants = 0;
		NameGenerator.curVowels = 0;
		string text = "";
		string[] array = pAsset.templates.GetRandom<string>().Split(new char[]
		{
			','
		});
		bool flag = false;
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string[] array3 = array2[i].Split(new char[]
			{
				'#'
			});
			string text2 = array3[0];
			if (pAsset.motto_generator)
			{
				if (text2 == "comma")
				{
					text += ", ";
				}
				else
				{
					if (text2.Contains(";"))
					{
						text2 = text2.Split(new char[]
						{
							';'
						}).GetRandom<string>();
					}
					if (!NameGenerator.dict_splitted_items.ContainsKey(text2))
					{
						List<string> list = Enumerable.ToList<string>(pAsset.motto_parts[text2].Split(new char[]
						{
							','
						}));
						NameGenerator.dict_splitted_items.Add(text2, list);
					}
					NameGenerator.dict_splitted_items[text2].ShuffleOne<string>();
					string str = NameGenerator.dict_splitted_items[text2][0];
					NameGenerator.dict_splitted_items[text2].RemoveAt(0);
					text += str;
				}
			}
			else if (text2 != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num <= 2267396087U)
				{
					if (num <= 717708808U)
					{
						if (num <= 51335718U)
						{
							if (num != 34558099U)
							{
								if (num != 51335718U)
								{
									goto IL_93E;
								}
								if (!(text2 == "special2"))
								{
									goto IL_93E;
								}
								text += pAsset.special2.GetRandom<string>();
								goto IL_93E;
							}
							else
							{
								if (!(text2 == "special1"))
								{
									goto IL_93E;
								}
								text += pAsset.special1.GetRandom<string>();
								goto IL_93E;
							}
						}
						else if (num != 455613546U)
						{
							if (num != 467038368U)
							{
								if (num != 717708808U)
								{
									goto IL_93E;
								}
								if (!(text2 == "Part_group"))
								{
									goto IL_93E;
								}
							}
							else
							{
								if (!(text2 == "number"))
								{
									goto IL_93E;
								}
								text += Toolbox.randomInt(0, 10).ToString();
								goto IL_93E;
							}
						}
						else
						{
							if (!(text2 == "CONSONANT"))
							{
								goto IL_93E;
							}
							text += NameGenerator.addConsonant(pAsset.consonants, true);
							goto IL_93E;
						}
					}
					else if (num <= 1417423114U)
					{
						if (num != 737585510U)
						{
							if (num != 894689925U)
							{
								if (num != 1417423114U)
								{
									goto IL_93E;
								}
								if (!(text2 == "Letters"))
								{
									goto IL_93E;
								}
								string[] array4 = array3[1].Split(new char[]
								{
									'-'
								});
								text += NameGenerator.addWord(pAsset, int.Parse(array4[0]), int.Parse(array4[1]), true);
								goto IL_93E;
							}
							else
							{
								if (!(text2 == "space"))
								{
									goto IL_93E;
								}
								text += " ";
								goto IL_93E;
							}
						}
						else
						{
							if (!(text2 == "vowel"))
							{
								goto IL_93E;
							}
							text += NameGenerator.addVowel(pAsset.vowels, false);
							goto IL_93E;
						}
					}
					else if (num != 1431138378U)
					{
						if (num != 2088252948U)
						{
							if (num != 2267396087U)
							{
								goto IL_93E;
							}
							if (!(text2 == "removalchance"))
							{
								goto IL_93E;
							}
							if (Toolbox.randomBool())
							{
								text.Remove(text.Length - 1);
								goto IL_93E;
							}
							goto IL_93E;
						}
						else
						{
							if (!(text2 == "part"))
							{
								goto IL_93E;
							}
							text += pAsset.parts.GetRandom<string>();
							goto IL_93E;
						}
					}
					else
					{
						if (!(text2 == "consonant"))
						{
							goto IL_93E;
						}
						text += NameGenerator.addConsonant(pAsset.consonants, false);
						goto IL_93E;
					}
				}
				else
				{
					if (num <= 2524959326U)
					{
						if (num <= 2312965761U)
						{
							if (num != 2287153481U)
							{
								if (num != 2296188142U)
								{
									if (num != 2312965761U)
									{
										goto IL_93E;
									}
									if (!(text2 == "part_group3"))
									{
										goto IL_93E;
									}
									goto IL_7A8;
								}
								else if (!(text2 == "part_group2"))
								{
									goto IL_93E;
								}
							}
							else
							{
								if (!(text2 == "addition_ending"))
								{
									goto IL_93E;
								}
								if (!flag && Toolbox.randomChance(pAsset.add_addition_chance))
								{
									text = text + " " + pAsset.addition_ending.GetRandom<string>();
									flag = true;
									goto IL_93E;
								}
								goto IL_93E;
							}
						}
						else if (num != 2446939470U)
						{
							if (num != 2463717089U)
							{
								if (num != 2524959326U)
								{
									goto IL_93E;
								}
								if (!(text2 == "vowelchance"))
								{
									goto IL_93E;
								}
								if (Toolbox.randomBool())
								{
									text += NameGenerator.addVowel(pAsset.vowels, false);
									goto IL_93E;
								}
								goto IL_93E;
							}
							else
							{
								if (!(text2 == "Part_group3"))
								{
									goto IL_93E;
								}
								goto IL_8D3;
							}
						}
						else
						{
							if (!(text2 == "Part_group2"))
							{
								goto IL_93E;
							}
							goto IL_868;
						}
					}
					else if (num <= 3814285364U)
					{
						if (num != 3159231528U)
						{
							if (num != 3552634630U)
							{
								if (num != 3814285364U)
								{
									goto IL_93E;
								}
								if (!(text2 == "Part"))
								{
									goto IL_93E;
								}
								string text3 = pAsset.parts.GetRandom<string>();
								text3 = NameGenerator.firstToUpper(text3);
								text += text3;
								goto IL_93E;
							}
							else
							{
								if (!(text2 == "VOWEL"))
								{
									goto IL_93E;
								}
								text += NameGenerator.addVowel(pAsset.vowels, true);
								goto IL_93E;
							}
						}
						else
						{
							if (!(text2 == "part_group"))
							{
								goto IL_93E;
							}
							using (List<string>.Enumerator enumerator = pAsset.part_groups.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									string text4 = enumerator.Current;
									string[] list2 = text4.Split(new char[]
									{
										','
									});
									text += list2.GetRandom<string>();
								}
								goto IL_93E;
							}
						}
					}
					else if (num != 3890148115U)
					{
						if (num != 4018923290U)
						{
							if (num != 4153687146U)
							{
								goto IL_93E;
							}
							if (!(text2 == "letters"))
							{
								goto IL_93E;
							}
							string[] array5 = array3[1].Split(new char[]
							{
								'-'
							});
							text += NameGenerator.addWord(pAsset, int.Parse(array5[0]), int.Parse(array5[1]), false);
							goto IL_93E;
						}
						else
						{
							if (!(text2 == "addition_start"))
							{
								goto IL_93E;
							}
							if (!flag && Toolbox.randomChance(pAsset.add_addition_chance))
							{
								text = text + pAsset.addition_start.GetRandom<string>() + " ";
								flag = true;
								goto IL_93E;
							}
							goto IL_93E;
						}
					}
					else
					{
						if (!(text2 == "RANDOM_LETTER"))
						{
							goto IL_93E;
						}
						if (Toolbox.randomBool())
						{
							text += NameGenerator.addVowel(pAsset.vowels, true);
							goto IL_93E;
						}
						text += NameGenerator.addConsonant(pAsset.consonants, true);
						goto IL_93E;
					}
					using (List<string>.Enumerator enumerator = pAsset.part_groups2.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text5 = enumerator.Current;
							string[] list3 = text5.Split(new char[]
							{
								','
							});
							text += list3.GetRandom<string>();
						}
						goto IL_93E;
					}
					IL_7A8:
					using (List<string>.Enumerator enumerator = pAsset.part_groups3.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text6 = enumerator.Current;
							string[] list4 = text6.Split(new char[]
							{
								','
							});
							text += list4.GetRandom<string>();
						}
						goto IL_93E;
					}
				}
				bool flag2 = true;
				using (List<string>.Enumerator enumerator = pAsset.part_groups.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text7 = enumerator.Current;
						string[] list5 = text7.Split(new char[]
						{
							','
						});
						if (flag2)
						{
							text += NameGenerator.firstToUpper(list5.GetRandom<string>());
							flag2 = false;
						}
						else
						{
							text += list5.GetRandom<string>();
						}
					}
					goto IL_93E;
				}
				IL_868:
				flag2 = true;
				using (List<string>.Enumerator enumerator = pAsset.part_groups2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text8 = enumerator.Current;
						string[] list6 = text8.Split(new char[]
						{
							','
						});
						if (flag2)
						{
							text += NameGenerator.firstToUpper(list6.GetRandom<string>());
							flag2 = false;
						}
						else
						{
							text += list6.GetRandom<string>();
						}
					}
					goto IL_93E;
				}
				IL_8D3:
				flag2 = true;
				foreach (string text9 in pAsset.part_groups3)
				{
					string[] list7 = text9.Split(new char[]
					{
						','
					});
					if (flag2)
					{
						text += NameGenerator.firstToUpper(list7.GetRandom<string>());
						flag2 = false;
					}
					else
					{
						text += list7.GetRandom<string>();
					}
				}
			}
			IL_93E:;
		}
		if (NameGenerator.checkBlackList(text))
		{
			text = NameGenerator.generateNameFromTemplate(pAsset);
		}
		text = NameGenerator.firstToUpper(text);
		return text;
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x0006004C File Offset: 0x0005E24C
	private static string addWord(NameGeneratorAsset pAsset, int pMin, int tMax, bool pToUpperFirst = false)
	{
		string text = "";
		int num = Toolbox.randomInt(pMin, tMax);
		for (int i = 0; i < num; i++)
		{
			if (NameGenerator.curConsonants >= pAsset.max_consonanats_in_row)
			{
				text += NameGenerator.addVowel(pAsset.vowels, pToUpperFirst);
				pToUpperFirst = false;
			}
			else if (NameGenerator.curVowels >= pAsset.max_vowels_in_row)
			{
				text += NameGenerator.addConsonant(pAsset.consonants, pToUpperFirst);
				pToUpperFirst = false;
			}
			else if (Toolbox.randomBool())
			{
				text += NameGenerator.addVowel(pAsset.vowels, pToUpperFirst);
				pToUpperFirst = false;
			}
			else
			{
				text += NameGenerator.addConsonant(pAsset.consonants, pToUpperFirst);
				pToUpperFirst = false;
			}
		}
		return text;
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x00060104 File Offset: 0x0005E304
	// Note: this type is marked as 'beforefieldinit'.
	static NameGenerator()
	{
		List<string> list = new List<string>();
		list.Add("a");
		list.Add("e");
		list.Add("i");
		list.Add("o");
		list.Add("u");
		list.Add("y");
		NameGenerator.vowels_all = list;
		List<string> list2 = new List<string>();
		list2.Add("b");
		list2.Add("c");
		list2.Add("d");
		list2.Add("f");
		list2.Add("g");
		list2.Add("h");
		list2.Add("j");
		list2.Add("k");
		list2.Add("l");
		list2.Add("m");
		list2.Add("n");
		list2.Add("p");
		list2.Add("q");
		list2.Add("r");
		list2.Add("s");
		list2.Add("t");
		list2.Add("v");
		list2.Add("w");
		list2.Add("x");
		list2.Add("z");
		NameGenerator.consonants_all = list2;
		NameGenerator.initiated = false;
		NameGenerator.dict_splitted_items = new Dictionary<string, List<string>>();
	}

	// Token: 0x04000B77 RID: 2935
	private static int curConsonants = 0;

	// Token: 0x04000B78 RID: 2936
	private static int curVowels = 0;

	// Token: 0x04000B79 RID: 2937
	private static List<string> vowels_all;

	// Token: 0x04000B7A RID: 2938
	private static List<string> consonants_all;

	// Token: 0x04000B7B RID: 2939
	private static Dictionary<char, List<string>> profanity;

	// Token: 0x04000B7C RID: 2940
	private static bool initiated;

	// Token: 0x04000B7D RID: 2941
	private static Dictionary<string, List<string>> dict_splitted_items;
}
