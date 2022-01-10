using System;

// Token: 0x0200013F RID: 319
[Serializable]
public class KingdomStatVal
{
	// Token: 0x06000783 RID: 1923 RVA: 0x000549A7 File Offset: 0x00052BA7
	public KingdomStatVal(string pID)
	{
		this.id = pID;
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x000549B6 File Offset: 0x00052BB6
	public void add(float pVal)
	{
		this.value += pVal;
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x000549C6 File Offset: 0x00052BC6
	public void set(float pVal)
	{
		this.value = pVal;
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x000549CF File Offset: 0x00052BCF
	public void clear()
	{
		this.value = 0f;
	}

	// Token: 0x040009F6 RID: 2550
	public float value;

	// Token: 0x040009F7 RID: 2551
	public string id;
}
