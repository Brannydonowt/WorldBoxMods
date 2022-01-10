using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002DE RID: 734
public class CityInfoElement : MonoBehaviour
{
	// Token: 0x06000FC4 RID: 4036 RVA: 0x0008C0FD File Offset: 0x0008A2FD
	private void Awake()
	{
		this.rect = base.GetComponent<RectTransform>();
	}

	// Token: 0x06000FC5 RID: 4037 RVA: 0x0008C10B File Offset: 0x0008A30B
	public void clickLeader()
	{
		Config.selectedCity = this.city;
		Config.selectedUnit = this.city.leader;
		if (Config.selectedUnit == null)
		{
			return;
		}
		ScrollWindow.showWindow("inspect_unit");
	}

	// Token: 0x06000FC6 RID: 4038 RVA: 0x0008C140 File Offset: 0x0008A340
	public void show(City pCity)
	{
		this.city = pCity;
		if (this.city.isCapitalCity())
		{
			base.GetComponent<Image>().color = CityInfoElement.capitalColor;
		}
		else
		{
			base.GetComponent<Image>().color = Color.white;
		}
		this.buttonLeader.gameObject.SetActive(pCity.leader != null);
		this.cityName.text = pCity.data.cityName;
		this.population.text = (pCity.getPopulationTotal().ToString() ?? "");
		this.capitalIcon.gameObject.SetActive(this.city.kingdom.capital == this.city);
		int relationRating = this.city.getRelationRating();
		this.diplomacy.text = (relationRating.ToString() ?? "");
		if (relationRating > 0)
		{
			this.diplomacy.color = Color.green;
		}
		else
		{
			this.diplomacy.color = Color.red;
		}
		this.tipOpinion.city = this.city;
	}

	// Token: 0x06000FC7 RID: 4039 RVA: 0x0008C260 File Offset: 0x0008A460
	public void clickCity()
	{
		Config.selectedCity = this.city;
		ScrollWindow.showWindow("village");
	}

	// Token: 0x040012D8 RID: 4824
	private static Color capitalColor = new Color(0.7f, 0.7f, 0.7f);

	// Token: 0x040012D9 RID: 4825
	internal City city;

	// Token: 0x040012DA RID: 4826
	public Text cityName;

	// Token: 0x040012DB RID: 4827
	public Text population;

	// Token: 0x040012DC RID: 4828
	public Text diplomacy;

	// Token: 0x040012DD RID: 4829
	public GameObject buttonLeader;

	// Token: 0x040012DE RID: 4830
	public GameObject capitalIcon;

	// Token: 0x040012DF RID: 4831
	public RectTransform rect;

	// Token: 0x040012E0 RID: 4832
	public TipButton tipOpinion;
}
