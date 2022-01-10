using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000275 RID: 629
public class VersionTextUpdater : MonoBehaviour
{
	// Token: 0x06000DE5 RID: 3557 RVA: 0x000834AC File Offset: 0x000816AC
	private void Start()
	{
		if (this.addText)
		{
			this.text.text = "version: " + Application.version + "-" + Config.versionCodeText;
			return;
		}
		string text = Application.platform.ToString().ToLower();
		text = text.Replace("player", "");
		this.text.text = string.Concat(new string[]
		{
			text,
			" ",
			Application.version,
			"-",
			Config.versionCodeText
		});
		try
		{
			if (!string.IsNullOrEmpty(RequestHelper.salt) && RequestHelper.salt != "err")
			{
				Text text2 = this.text;
				text2.text = text2.text + " (" + RequestHelper.salt.Substring(0, 2) + ")";
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.ToString());
		}
	}

	// Token: 0x040010B8 RID: 4280
	public bool addText = true;

	// Token: 0x040010B9 RID: 4281
	public Text text;
}
