using System;
using Beebyte.Obfuscator;

// Token: 0x02000084 RID: 132
[ObfuscateLiterals]
public class MapSizePresset
{
	// Token: 0x060002D3 RID: 723 RVA: 0x000315F8 File Offset: 0x0002F7F8
	public static int getSize(string pMapSizePresset)
	{
		if (pMapSizePresset != null)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(pMapSizePresset);
			if (num <= 1620760713U)
			{
				if (num <= 1271934388U)
				{
					if (num != 63526728U)
					{
						if (num == 1271934388U)
						{
							if (pMapSizePresset == "large")
							{
								return 5;
							}
						}
					}
					else if (pMapSizePresset == "standard")
					{
						return 4;
					}
				}
				else if (num != 1338940139U)
				{
					if (num == 1620760713U)
					{
						if (pMapSizePresset == "titanic")
						{
							return 8;
						}
					}
				}
				else if (pMapSizePresset == "gigantic")
				{
					return 7;
				}
			}
			else if (num <= 1737402210U)
			{
				if (num != 1630922347U)
				{
					if (num == 1737402210U)
					{
						if (pMapSizePresset == "huge")
						{
							return 6;
						}
					}
				}
				else if (pMapSizePresset == "tiny")
				{
					return 2;
				}
			}
			else if (num != 2638759970U)
			{
				if (num == 2730816652U)
				{
					if (pMapSizePresset == "small")
					{
						return 3;
					}
				}
			}
			else if (pMapSizePresset == "iceberg")
			{
				return 9;
			}
		}
		return 2;
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x00031710 File Offset: 0x0002F910
	public static string getPreset(int pMapSize)
	{
		switch (pMapSize)
		{
		case 2:
			return "tiny";
		case 3:
			return "small";
		case 4:
			return "standard";
		case 5:
			return "large";
		case 6:
			return "huge";
		case 7:
			return "gigantic";
		case 8:
			return "titanic";
		case 9:
			return "iceberg";
		default:
			return null;
		}
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x00031778 File Offset: 0x0002F978
	public static bool isSizeValid(int pMapSize)
	{
		return !string.IsNullOrEmpty(MapSizePresset.getPreset(pMapSize)) && pMapSize <= MapSizePresset.getSize(Config.maxMapSize);
	}

	// Token: 0x04000424 RID: 1060
	public const string Tiny = "tiny";

	// Token: 0x04000425 RID: 1061
	public const string Small = "small";

	// Token: 0x04000426 RID: 1062
	public const string Standard = "standard";

	// Token: 0x04000427 RID: 1063
	public const string Large = "large";

	// Token: 0x04000428 RID: 1064
	public const string Huge = "huge";

	// Token: 0x04000429 RID: 1065
	public const string Gigantic = "gigantic";

	// Token: 0x0400042A RID: 1066
	public const string Titanic = "titanic";

	// Token: 0x0400042B RID: 1067
	public const string Iceberg = "iceberg";
}
