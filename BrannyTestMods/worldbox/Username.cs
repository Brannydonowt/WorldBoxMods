using System;
using System.Threading.Tasks;

// Token: 0x02000249 RID: 585
public class Username
{
	// Token: 0x06000CBC RID: 3260 RVA: 0x0007B408 File Offset: 0x00079608
	public static bool isValid(string strToCheck)
	{
		return false;
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x0007B40C File Offset: 0x0007960C
	public static async Task<bool> isTaken(string pUsername)
	{
		Username.isValid(pUsername);
		return false;
	}
}
