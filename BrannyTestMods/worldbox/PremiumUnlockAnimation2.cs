using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000266 RID: 614
public class PremiumUnlockAnimation2 : MonoBehaviour
{
	// Token: 0x06000D42 RID: 3394 RVA: 0x0007E230 File Offset: 0x0007C430
	private void Start()
	{
		this.canvasGroup = this.shineFX.GetComponent<CanvasGroup>();
		this.circleFX.SetActive(true);
		iTween.ScaleTo(this.circleFX, iTween.Hash(new object[]
		{
			"scale",
			new Vector3(1f, 1f, 1f),
			"time",
			1f,
			"looptype",
			"PingPong"
		}));
		iTween.ScaleTo(this.aye, iTween.Hash(new object[]
		{
			"delay",
			0.5f,
			"scale",
			new Vector3(1f, 1f, 1f),
			"time",
			1f,
			"easetype",
			"easeOutElastic"
		}));
		foreach (object obj in this.listContainer.transform)
		{
			Transform transform = (Transform)obj;
			this.buttonList.Add(transform.gameObject);
		}
		for (int i = 0; i < this.buttonList.Count; i++)
		{
			this.directionList.Add(new Vector3((float)Random.Range(-3, 3), (float)Random.Range(-3, 3), (float)Random.Range(-3, 3)));
		}
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x0007E3CC File Offset: 0x0007C5CC
	private void Update()
	{
		this.canvasGroup.alpha += Time.deltaTime / this.fadeDelay;
		this.shineFX.transform.Rotate(new Vector3(0f, 0f, 1f));
		foreach (GameObject gameObject in this.buttonList)
		{
			this.index = this.buttonList.IndexOf(gameObject);
			iTween.MoveAdd(gameObject, this.directionList[this.index], this.time);
			gameObject.transform.Rotate(this.directionList[this.index], 1f);
			iTween.ScaleAdd(gameObject, iTween.Hash(new object[]
			{
				"amount",
				this.scaleAdd,
				"easetype",
				21,
				"time",
				1
			}));
		}
	}

	// Token: 0x04001026 RID: 4134
	public List<GameObject> buttonList = new List<GameObject>();

	// Token: 0x04001027 RID: 4135
	public GameObject listContainer;

	// Token: 0x04001028 RID: 4136
	public float time;

	// Token: 0x04001029 RID: 4137
	private List<Vector3> directionList = new List<Vector3>();

	// Token: 0x0400102A RID: 4138
	public GameObject circleFX;

	// Token: 0x0400102B RID: 4139
	public GameObject shineFX;

	// Token: 0x0400102C RID: 4140
	public GameObject aye;

	// Token: 0x0400102D RID: 4141
	private CanvasGroup canvasGroup;

	// Token: 0x0400102E RID: 4142
	public float fadeDelay;

	// Token: 0x0400102F RID: 4143
	private int index;

	// Token: 0x04001030 RID: 4144
	public Vector3 scaleAdd;
}
