using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

// Token: 0x020001CB RID: 459
public sealed class SemanticVersion : IComparable, IComparable<SemanticVersion>, IEquatable<SemanticVersion>
{
	// Token: 0x06000A5A RID: 2650 RVA: 0x00068F65 File Offset: 0x00067165
	public SemanticVersion(string version) : this(SemanticVersion.Parse(version))
	{
		this._originalString = version;
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x00068F7A File Offset: 0x0006717A
	public SemanticVersion(int major, int minor, int build, int revision) : this(new Version(major, minor, build, revision))
	{
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x00068F8C File Offset: 0x0006718C
	public SemanticVersion(int major, int minor, int build, string specialVersion) : this(new Version(major, minor, build), specialVersion)
	{
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x00068F9E File Offset: 0x0006719E
	public SemanticVersion(int major, int minor, int build, string specialVersion, string metadata) : this(new Version(major, minor, build), specialVersion, metadata)
	{
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x00068FB2 File Offset: 0x000671B2
	public SemanticVersion(Version version) : this(version, string.Empty)
	{
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x00068FC0 File Offset: 0x000671C0
	public SemanticVersion(Version version, string specialVersion) : this(version, specialVersion, null, null)
	{
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x00068FCC File Offset: 0x000671CC
	public SemanticVersion(Version version, string specialVersion, string metadata) : this(version, specialVersion, metadata, null)
	{
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x00068FD8 File Offset: 0x000671D8
	private SemanticVersion(Version version, string specialVersion, string metadata, string originalString)
	{
		if (version == null)
		{
			throw new ArgumentNullException("version");
		}
		this.Version = SemanticVersion.NormalizeVersionValue(version);
		this.SpecialVersion = (specialVersion ?? string.Empty);
		this.Metadata = metadata;
		this._originalString = (string.IsNullOrEmpty(originalString) ? (version.ToString() + ((!string.IsNullOrEmpty(specialVersion)) ? ("-" + specialVersion) : null) + ((!string.IsNullOrEmpty(metadata)) ? ("+" + metadata) : null)) : originalString);
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x0006906C File Offset: 0x0006726C
	internal SemanticVersion(SemanticVersion semVer)
	{
		this._originalString = semVer.ToOriginalString();
		this.Version = semVer.Version;
		this.SpecialVersion = semVer.SpecialVersion;
		this.Metadata = semVer.Metadata;
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000A63 RID: 2659 RVA: 0x000690A4 File Offset: 0x000672A4
	// (set) Token: 0x06000A64 RID: 2660 RVA: 0x000690AC File Offset: 0x000672AC
	public Version Version { get; private set; }

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000A65 RID: 2661 RVA: 0x000690B5 File Offset: 0x000672B5
	// (set) Token: 0x06000A66 RID: 2662 RVA: 0x000690BD File Offset: 0x000672BD
	public string SpecialVersion { get; private set; }

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000A67 RID: 2663 RVA: 0x000690C6 File Offset: 0x000672C6
	// (set) Token: 0x06000A68 RID: 2664 RVA: 0x000690CE File Offset: 0x000672CE
	public string Metadata { get; private set; }

	// Token: 0x06000A69 RID: 2665 RVA: 0x000690D8 File Offset: 0x000672D8
	public string[] GetOriginalVersionComponents()
	{
		if (!string.IsNullOrEmpty(this._originalString))
		{
			int num = this._originalString.IndexOfAny(new char[]
			{
				'-',
				'+'
			});
			string version;
			if (num != -1)
			{
				version = this._originalString.Substring(0, num);
			}
			else
			{
				version = this._originalString;
			}
			return SemanticVersion.SplitAndPadVersionString(version);
		}
		return SemanticVersion.SplitAndPadVersionString(this.Version.ToString());
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x00069140 File Offset: 0x00067340
	private static string[] SplitAndPadVersionString(string version)
	{
		string[] array = version.Split(new char[]
		{
			'.'
		});
		if (array.Length == 4)
		{
			return array;
		}
		string[] array2 = new string[]
		{
			"0",
			"0",
			"0",
			"0"
		};
		Array.Copy(array, 0, array2, 0, array.Length);
		return array2;
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x0006919C File Offset: 0x0006739C
	public static SemanticVersion Parse(string version)
	{
		if (string.IsNullOrEmpty(version))
		{
			throw new ArgumentException("Value cannot be null or an empty string", "version");
		}
		SemanticVersion result;
		if (!SemanticVersion.TryParse(version, out result))
		{
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "'{0}' is not a valid version string.", version), "version");
		}
		return result;
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x000691E7 File Offset: 0x000673E7
	public static bool TryParse(string version, out SemanticVersion value)
	{
		return SemanticVersion.TryParseInternal(version, SemanticVersion._semanticVersionRegex, out value);
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x000691F5 File Offset: 0x000673F5
	public static bool TryParseStrict(string version, out SemanticVersion value)
	{
		return SemanticVersion.TryParseInternal(version, SemanticVersion._strictSemanticVersionRegex, out value);
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x00069204 File Offset: 0x00067404
	private static bool TryParseInternal(string version, Regex regex, out SemanticVersion semVer)
	{
		semVer = null;
		if (string.IsNullOrEmpty(version))
		{
			return false;
		}
		Match match = regex.Match(version.Trim());
		Version version2;
		if (!match.Success || !Version.TryParse(match.Groups["Version"].Value, out version2))
		{
			return false;
		}
		semVer = new SemanticVersion(SemanticVersion.NormalizeVersionValue(version2), SemanticVersion.RemoveLeadingChar(match.Groups["Release"].Value), SemanticVersion.RemoveLeadingChar(match.Groups["Metadata"].Value), version.Replace(" ", ""));
		return true;
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x000692A5 File Offset: 0x000674A5
	private static string RemoveLeadingChar(string s)
	{
		if (s != null && s.Length > 0)
		{
			return s.Substring(1, s.Length - 1);
		}
		return s;
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x000692C4 File Offset: 0x000674C4
	public static SemanticVersion ParseOptionalVersion(string version)
	{
		SemanticVersion result;
		SemanticVersion.TryParse(version, out result);
		return result;
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x000692DB File Offset: 0x000674DB
	private static Version NormalizeVersionValue(Version version)
	{
		return new Version(version.Major, version.Minor, Math.Max(version.Build, 0), Math.Max(version.Revision, 0));
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x00069308 File Offset: 0x00067508
	public int CompareTo(object obj)
	{
		if (obj == null)
		{
			return 1;
		}
		SemanticVersion semanticVersion = obj as SemanticVersion;
		if (semanticVersion == null)
		{
			throw new ArgumentException("Type to compare must be an instance of SemanticVersion.", "obj");
		}
		return this.CompareTo(semanticVersion);
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x00069344 File Offset: 0x00067544
	public int CompareTo(SemanticVersion other)
	{
		if (other == null)
		{
			return 1;
		}
		int num = this.Version.CompareTo(other.Version);
		if (num != 0)
		{
			return num;
		}
		bool flag = string.IsNullOrEmpty(this.SpecialVersion);
		bool flag2 = string.IsNullOrEmpty(other.SpecialVersion);
		if (flag && flag2)
		{
			return 0;
		}
		if (flag)
		{
			return 1;
		}
		if (flag2)
		{
			return -1;
		}
		string[] version = this.SpecialVersion.Split(new char[]
		{
			'.'
		});
		string[] version2 = other.SpecialVersion.Split(new char[]
		{
			'.'
		});
		return SemanticVersion.CompareReleaseLabels(version, version2);
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x000693CD File Offset: 0x000675CD
	public static bool operator ==(SemanticVersion version1, SemanticVersion version2)
	{
		if (version1 == null)
		{
			return version2 == null;
		}
		return version1.Equals(version2);
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x000693DE File Offset: 0x000675DE
	public static bool operator !=(SemanticVersion version1, SemanticVersion version2)
	{
		return !(version1 == version2);
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x000693EA File Offset: 0x000675EA
	public static bool operator <(SemanticVersion version1, SemanticVersion version2)
	{
		if (version1 == null)
		{
			throw new ArgumentNullException("version1");
		}
		return version1.CompareTo(version2) < 0;
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0006940A File Offset: 0x0006760A
	public static bool operator <=(SemanticVersion version1, SemanticVersion version2)
	{
		return version1 == version2 || version1 < version2;
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x0006941E File Offset: 0x0006761E
	public static bool operator >(SemanticVersion version1, SemanticVersion version2)
	{
		if (version1 == null)
		{
			throw new ArgumentNullException("version1");
		}
		return version2 < version1;
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x0006943B File Offset: 0x0006763B
	public static bool operator >=(SemanticVersion version1, SemanticVersion version2)
	{
		return version1 == version2 || version1 > version2;
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x00069450 File Offset: 0x00067650
	public override string ToString()
	{
		if (this.IsSemVer2())
		{
			return this.ToNormalizedString();
		}
		int num = this._originalString.IndexOf('+');
		if (num > -1)
		{
			return this._originalString.Substring(0, num);
		}
		return this._originalString;
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x00069494 File Offset: 0x00067694
	public string ToNormalizedString()
	{
		if (this._normalizedVersionString == null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Version.Major).Append('.').Append(this.Version.Minor).Append('.').Append(Math.Max(0, this.Version.Build));
			if (this.Version.Revision > 0)
			{
				stringBuilder.Append('.').Append(this.Version.Revision);
			}
			if (!string.IsNullOrEmpty(this.SpecialVersion))
			{
				stringBuilder.Append('-').Append(this.SpecialVersion);
			}
			this._normalizedVersionString = stringBuilder.ToString();
		}
		return this._normalizedVersionString;
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x00069554 File Offset: 0x00067754
	public string ToFullString()
	{
		string text = this.ToNormalizedString();
		if (!string.IsNullOrEmpty(this.Metadata))
		{
			text = string.Format(CultureInfo.InvariantCulture, "{0}+{1}", text, this.Metadata);
		}
		return text;
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x0006958D File Offset: 0x0006778D
	public string ToOriginalString()
	{
		return this._originalString;
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x00069595 File Offset: 0x00067795
	public bool IsSemVer2()
	{
		return !string.IsNullOrEmpty(this.Metadata) || (!string.IsNullOrEmpty(this.SpecialVersion) && this.SpecialVersion.Contains("."));
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x000695C5 File Offset: 0x000677C5
	public bool Equals(SemanticVersion other)
	{
		return other != null && this.Version.Equals(other.Version) && this.SpecialVersion.Equals(other.SpecialVersion, StringComparison.OrdinalIgnoreCase);
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x000695F4 File Offset: 0x000677F4
	public override bool Equals(object obj)
	{
		SemanticVersion semanticVersion = obj as SemanticVersion;
		return semanticVersion != null && this.Equals(semanticVersion);
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x00069614 File Offset: 0x00067814
	public override int GetHashCode()
	{
		int num = this.Version.GetHashCode();
		if (this.SpecialVersion != null)
		{
			num = num * 4567 + this.SpecialVersion.GetHashCode();
		}
		return num;
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x0006964C File Offset: 0x0006784C
	private static int CompareReleaseLabels(IEnumerable<string> version1, IEnumerable<string> version2)
	{
		int num = 0;
		IEnumerator<string> enumerator = version1.GetEnumerator();
		IEnumerator<string> enumerator2 = version2.GetEnumerator();
		bool flag = enumerator.MoveNext();
		bool flag2 = enumerator2.MoveNext();
		while (flag || flag2)
		{
			if (!flag && flag2)
			{
				return -1;
			}
			if (flag && !flag2)
			{
				return 1;
			}
			num = SemanticVersion.CompareRelease(enumerator.Current, enumerator2.Current);
			if (num != 0)
			{
				return num;
			}
			flag = enumerator.MoveNext();
			flag2 = enumerator2.MoveNext();
		}
		return num;
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x000696BC File Offset: 0x000678BC
	private static int CompareRelease(string version1, string version2)
	{
		int num = 0;
		int value = 0;
		bool flag = int.TryParse(version1, out num);
		bool flag2 = int.TryParse(version2, out value);
		int result;
		if (flag && flag2)
		{
			result = num.CompareTo(value);
		}
		else if (flag || flag2)
		{
			if (flag)
			{
				result = -1;
			}
			else
			{
				result = 1;
			}
		}
		else
		{
			result = StringComparer.OrdinalIgnoreCase.Compare(version1, version2);
		}
		return result;
	}

	// Token: 0x04000CFD RID: 3325
	private const RegexOptions _flags = 12;

	// Token: 0x04000CFE RID: 3326
	private static readonly Regex _semanticVersionRegex = new Regex("^(?<Version>\\d+(\\s*\\.\\s*\\d+){0,3})(?<Release>-([0]\\b|[0]$|[0][0-9]*[A-Za-z-]+|[1-9A-Za-z-][0-9A-Za-z-]*)+(\\.([0]\\b|[0]$|[0][0-9]*[A-Za-z-]+|[1-9A-Za-z-][0-9A-Za-z-]*)+)*)?(?<Metadata>\\+[0-9A-Za-z-]+(\\.[0-9A-Za-z-]+)*)?$", 12);

	// Token: 0x04000CFF RID: 3327
	private static readonly Regex _strictSemanticVersionRegex = new Regex("^(?<Version>([0-9]|[1-9][0-9]*)(\\.([0-9]|[1-9][0-9]*)){2})(?<Release>-([0]\\b|[0]$|[0][0-9]*[A-Za-z-]+|[1-9A-Za-z-][0-9A-Za-z-]*)+(\\.([0]\\b|[0]$|[0][0-9]*[A-Za-z-]+|[1-9A-Za-z-][0-9A-Za-z-]*)+)*)?(?<Metadata>\\+[0-9A-Za-z-]+(\\.[0-9A-Za-z-]+)*)?$", 12);

	// Token: 0x04000D00 RID: 3328
	private readonly string _originalString;

	// Token: 0x04000D01 RID: 3329
	private string _normalizedVersionString;
}
