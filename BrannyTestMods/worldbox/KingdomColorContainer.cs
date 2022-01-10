using System;
using System.Collections.Generic;

// Token: 0x02000033 RID: 51
[Serializable]
public class KingdomColorContainer
{
	// Token: 0x06000157 RID: 343 RVA: 0x000175A4 File Offset: 0x000157A4
	public void addColor(string pColor1, string pColor2, string pColor3, string pName = "")
	{
		KingdomColor kingdomColor = new KingdomColor(pColor1, pColor2, pColor3);
		kingdomColor.name = pName;
		this.list.Add(kingdomColor);
	}

	// Token: 0x06000158 RID: 344 RVA: 0x000175D0 File Offset: 0x000157D0
	internal void prepare(Kingdom pKingdom)
	{
		this.curColor = 0;
		for (int i = 0; i < this.list.Count; i++)
		{
			if (!this.haveColorInWorld(i))
			{
				this.curColor = i;
				return;
			}
		}
		this.curColor = Toolbox.randomInt(0, this.list.Count);
	}

	// Token: 0x06000159 RID: 345 RVA: 0x00017624 File Offset: 0x00015824
	internal bool haveColorInWorld(int pColor)
	{
		for (int i = 0; i < MapBox.instance.kingdoms.list_civs.Count; i++)
		{
			Kingdom kingdom = MapBox.instance.kingdoms.list_civs[i];
			if (!(kingdom.raceID != this.race) && kingdom.colorID == pColor)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x00017685 File Offset: 0x00015885
	public KingdomColor getColor(int pColor)
	{
		if (pColor > this.list.Count - 1)
		{
			pColor = 0;
		}
		return this.list[pColor];
	}

	// Token: 0x04000149 RID: 329
	public string race;

	// Token: 0x0400014A RID: 330
	public int curColor;

	// Token: 0x0400014B RID: 331
	public List<KingdomColor> list = new List<KingdomColor>();
}
