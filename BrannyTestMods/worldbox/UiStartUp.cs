using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000274 RID: 628
public class UiStartUp : MonoBehaviour
{
	// Token: 0x06000DE0 RID: 3552 RVA: 0x00083333 File Offset: 0x00081533
	private void Start()
	{
		this.goUp();
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x0008333C File Offset: 0x0008153C
	private void goDown()
	{
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			new Vector3(0f, -7f, base.gameObject.transform.localPosition.z),
			"easeType",
			"easeInOutQuad",
			"time",
			3f,
			"oncomplete",
			"goUp",
			"islocal",
			true
		}));
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x000833DC File Offset: 0x000815DC
	private void goUp()
	{
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			new Vector3(0f, 7f, base.gameObject.transform.localPosition.z),
			"easeType",
			"easeInOutQuad",
			"time",
			3f,
			"oncomplete",
			"goDown",
			"islocal",
			true
		}));
	}

	// Token: 0x06000DE3 RID: 3555 RVA: 0x0008347B File Offset: 0x0008167B
	private void Update()
	{
		if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
		{
			MapBox.instance.startTheGame();
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x040010B7 RID: 4279
	public Image logo;
}
