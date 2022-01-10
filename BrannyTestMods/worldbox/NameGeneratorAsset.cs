using System;
using System.Collections.Generic;

// Token: 0x02000039 RID: 57
public class NameGeneratorAsset : Asset
{
	// Token: 0x04000154 RID: 340
	public static string[] vowels_all = new string[]
	{
		"a",
		"e",
		"i",
		"o",
		"u",
		"y"
	};

	// Token: 0x04000155 RID: 341
	public static string[] consonants_all = new string[]
	{
		"b",
		"c",
		"d",
		"f",
		"g",
		"h",
		"j",
		"k",
		"l",
		"m",
		"n",
		"p",
		"q",
		"r",
		"s",
		"t",
		"v",
		"w",
		"x",
		"z"
	};

	// Token: 0x04000156 RID: 342
	public string[] special1;

	// Token: 0x04000157 RID: 343
	public string[] special2;

	// Token: 0x04000158 RID: 344
	public string[] vowels = NameGeneratorAsset.vowels_all;

	// Token: 0x04000159 RID: 345
	public string[] consonants = new string[]
	{
		"b",
		"c",
		"d",
		"f",
		"g",
		"h",
		"ph",
		"ch",
		"k",
		"l",
		"m",
		"n",
		"p",
		"r",
		"s",
		"t",
		"v",
		"w",
		"sh"
	};

	// Token: 0x0400015A RID: 346
	public string[] parts;

	// Token: 0x0400015B RID: 347
	public string[] addition_start;

	// Token: 0x0400015C RID: 348
	public string[] addition_ending;

	// Token: 0x0400015D RID: 349
	public List<string> part_groups = new List<string>();

	// Token: 0x0400015E RID: 350
	public List<string> part_groups2 = new List<string>();

	// Token: 0x0400015F RID: 351
	public List<string> part_groups3 = new List<string>();

	// Token: 0x04000160 RID: 352
	public Dictionary<string, string> motto_parts = new Dictionary<string, string>();

	// Token: 0x04000161 RID: 353
	public bool motto_generator;

	// Token: 0x04000162 RID: 354
	[NonSerialized]
	public List<string> templates = new List<string>();

	// Token: 0x04000163 RID: 355
	public int max_vowels_in_row = 1;

	// Token: 0x04000164 RID: 356
	public int max_consonanats_in_row = 1;

	// Token: 0x04000165 RID: 357
	public float add_addition_chance = 0.5f;
}
