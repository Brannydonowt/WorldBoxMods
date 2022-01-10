using System;
using System.Collections.Generic;

// Token: 0x02000187 RID: 391
public class RegionLinkHashes
{
	// Token: 0x060008FD RID: 2301 RVA: 0x00060298 File Offset: 0x0005E498
	public static void addHash(int pHash, MapRegion pRegion)
	{
		RegionLink regionLink = null;
		RegionLinkHashes.dict.TryGetValue(pHash, ref regionLink);
		if (regionLink == null)
		{
			regionLink = new RegionLink
			{
				id = pHash
			};
			RegionLinkHashes.dict[regionLink.id] = regionLink;
		}
		if (regionLink.regions.Add(pRegion))
		{
			pRegion.links.Add(regionLink);
		}
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x000602F0 File Offset: 0x0005E4F0
	public static int getCount()
	{
		return RegionLinkHashes.dict.Count;
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x000602FC File Offset: 0x0005E4FC
	public static void clear()
	{
		RegionLinkHashes.dict.Clear();
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x00060308 File Offset: 0x0005E508
	public static RegionLink getHash(int pHash)
	{
		RegionLink result = null;
		RegionLinkHashes.dict.TryGetValue(pHash, ref result);
		return result;
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x00060326 File Offset: 0x0005E526
	public static void remove(RegionLink pLink, MapRegion pRegion)
	{
		pLink.regions.Remove(pRegion);
		if (pLink.regions.Count == 0)
		{
			RegionLinkHashes.dict.Remove(pLink.id);
		}
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x00060353 File Offset: 0x0005E553
	public static void debug(DebugTool pTool)
	{
		pTool.setText("hashes", RegionLinkHashes.dict.Count);
	}

	// Token: 0x04000B80 RID: 2944
	private static Dictionary<int, RegionLink> dict = new Dictionary<int, RegionLink>();
}
