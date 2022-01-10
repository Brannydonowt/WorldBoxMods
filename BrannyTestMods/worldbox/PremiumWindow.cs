using System;
using UnityEngine;

// Token: 0x020002EC RID: 748
public class PremiumWindow : MonoBehaviour
{
	// Token: 0x0600106A RID: 4202 RVA: 0x0008FF7F File Offset: 0x0008E17F
	public void Awake()
	{
		this.clearButtons();
		this.addButtons();
	}

	// Token: 0x0600106B RID: 4203 RVA: 0x0008FF90 File Offset: 0x0008E190
	private void addButtons()
	{
		foreach (PowerButton powerButton in GodPower.premiumButtons)
		{
			powerButton.gameObject.SetActive(false);
			PowerButton powerButton2 = Object.Instantiate<PowerButton>(powerButton, this.buttons_transform);
			powerButton2.transform.name = powerButton.transform.name;
			powerButton2.type = PowerButtonType.Shop;
			powerButton2.destroyLockIcon();
			IconRotationAnimation iconRotationAnimation = powerButton2.gameObject.AddComponent<IconRotationAnimation>();
			iconRotationAnimation.delay = Toolbox.randomFloat(1f, 10f);
			iconRotationAnimation.randomDelay = true;
			powerButton.gameObject.SetActive(true);
			powerButton2.gameObject.SetActive(true);
		}
	}

	// Token: 0x0600106C RID: 4204 RVA: 0x0009005C File Offset: 0x0008E25C
	private void clearButtons()
	{
		while (this.buttons_transform.childCount > 0)
		{
			GameObject gameObject = this.buttons_transform.GetChild(0).gameObject;
			gameObject.transform.SetParent(null);
			Object.Destroy(gameObject);
		}
	}

	// Token: 0x04001383 RID: 4995
	public Transform buttons_transform;
}
