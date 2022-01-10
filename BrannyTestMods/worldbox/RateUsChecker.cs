using System;
using UnityEngine;

// Token: 0x02000267 RID: 615
public class RateUsChecker : MonoBehaviour
{
	// Token: 0x06000D45 RID: 3397 RVA: 0x0007E51C File Offset: 0x0007C71C
	private void OnEnable()
	{
		if (!Config.gameLoaded)
		{
			return;
		}
		if (this.rateUs != null && this.rateUs.gameObject != null)
		{
			GameObject gameObject = this.rateUs.gameObject;
			if (gameObject == null)
			{
				return;
			}
			gameObject.SetActive(false);
		}
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x0007E568 File Offset: 0x0007C768
	private void Update()
	{
		if (VersionCheck.isOutdated())
		{
			if (this.rateUs != null && this.rateUs.gameObject != null)
			{
				this.rateUs.gameObject.SetActive(false);
			}
			if (this.updateAvailable != null && this.updateAvailable.gameObject != null)
			{
				this.updateAvailable.gameObject.SetActive(true);
				return;
			}
		}
		else if (this.updateAvailable != null && this.updateAvailable.gameObject != null)
		{
			this.updateAvailable.gameObject.SetActive(false);
		}
	}

	// Token: 0x04001031 RID: 4145
	public GameObject rateUs;

	// Token: 0x04001032 RID: 4146
	public GameObject updateAvailable;
}
