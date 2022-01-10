using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

// Token: 0x020001AC RID: 428
public class Encryption
{
	// Token: 0x060009B4 RID: 2484 RVA: 0x00065504 File Offset: 0x00063704
	public static string EncryptString(string plainText, string passPhrase)
	{
		byte[] bytes = Encoding.UTF8.GetBytes("sayHiIfUReadThis");
		byte[] bytes2 = Encoding.UTF8.GetBytes(plainText);
		byte[] bytes3 = new PasswordDeriveBytes(passPhrase, null).GetBytes(32);
		ICryptoTransform cryptoTransform = new RijndaelManaged
		{
			Mode = 1
		}.CreateEncryptor(bytes3, bytes);
		MemoryStream memoryStream = new MemoryStream();
		CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, 1);
		cryptoStream.Write(bytes2, 0, bytes2.Length);
		cryptoStream.FlushFinalBlock();
		byte[] inArray = memoryStream.ToArray();
		memoryStream.Close();
		cryptoStream.Close();
		return Convert.ToBase64String(inArray);
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x00065590 File Offset: 0x00063790
	public static string DecryptString(string cipherText, string passPhrase)
	{
		byte[] bytes = Encoding.UTF8.GetBytes("sayHiIfUReadThis");
		byte[] array = Convert.FromBase64String(cipherText);
		byte[] bytes2 = new PasswordDeriveBytes(passPhrase, null).GetBytes(32);
		ICryptoTransform cryptoTransform = new RijndaelManaged
		{
			Mode = 1
		}.CreateDecryptor(bytes2, bytes);
		MemoryStream memoryStream = new MemoryStream(array);
		CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, 0);
		byte[] array2 = new byte[array.Length];
		int count = cryptoStream.Read(array2, 0, array2.Length);
		memoryStream.Close();
		cryptoStream.Close();
		return Encoding.UTF8.GetString(array2, 0, count);
	}

	// Token: 0x04000C5F RID: 3167
	private const string initVector = "sayHiIfUReadThis";

	// Token: 0x04000C60 RID: 3168
	private const int keysize = 256;
}
