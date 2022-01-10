using System;
using UnityEngine;

// Token: 0x020002D6 RID: 726
public class ButtonEmail : MonoBehaviour
{
	// Token: 0x06000FAC RID: 4012 RVA: 0x0008BDF4 File Offset: 0x00089FF4
	public void SendEmail()
	{
		string text = "supworldbox@gmail.com";
		string text2 = this.convert("WorldBox Feedback ( " + Application.version + " )");
		string text3 = this.convert("Yo!\r\n");
		Application.OpenURL(string.Concat(new string[]
		{
			"mailto:",
			text,
			"?subject=",
			text2,
			"&body=",
			text3
		}));
		Analytics.LogEvent("clicked_send_email", true, true);
	}

	// Token: 0x06000FAD RID: 4013 RVA: 0x0008BE70 File Offset: 0x0008A070
	public void SendEmailLogs()
	{
		string text = "supworldbox+errors@gmail.com";
		string text2 = this.convert("WorldBox Error Logs ( " + Application.version + " )");
		string text3 = this.convert("Please take a look at this error :\r\n" + LogHandler.log.Substring(Math.Max(0, LogHandler.log.Length - 4000)));
		Application.OpenURL(string.Concat(new string[]
		{
			"mailto:",
			text,
			"?subject=",
			text2,
			"&body=",
			text3
		}));
		Analytics.LogEvent("clicked_send_error_email", true, true);
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x0008BF0F File Offset: 0x0008A10F
	private string convert(string url)
	{
		return WWW.EscapeURL(url).Replace("+", "%20");
	}
}
