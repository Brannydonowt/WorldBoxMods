using System;
using System.IO;
using System.Security.Cryptography;
using Beebyte.Obfuscator;

// Token: 0x02000248 RID: 584
[ObfuscateLiterals]
public class RequestHelper
{
	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0007B31C File Offset: 0x0007951C
	public static string salt
	{
		get
		{
			if (RequestHelper._salt == "")
			{
				try
				{
					RequestHelper._salt = RequestHelper.SHA256CheckSum(typeof(RequestHelper).Assembly.Location);
				}
				catch (Exception)
				{
					RequestHelper._salt = "err";
				}
			}
			return RequestHelper._salt;
		}
	}

	// Token: 0x06000CB9 RID: 3257 RVA: 0x0007B37C File Offset: 0x0007957C
	public static string SHA256CheckSum(string filePath)
	{
		string result;
		using (SHA256 sha = SHA256.Create())
		{
			using (BufferedStream bufferedStream = new BufferedStream(File.OpenRead(filePath), 1200000))
			{
				result = BitConverter.ToString(sha.ComputeHash(bufferedStream)).Replace("-", string.Empty).ToLower();
			}
		}
		return result;
	}

	// Token: 0x04000FA9 RID: 4009
	private static string _salt = "";
}
