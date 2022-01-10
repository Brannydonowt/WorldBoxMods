using System;

// Token: 0x0200015D RID: 349
public class BehaviourActionCity : BehaviourActionBase<City>
{
	// Token: 0x060007EF RID: 2031 RVA: 0x0005778E File Offset: 0x0005598E
	public override bool errorsFound(City pCity)
	{
		return pCity.zones.Count == 0 || pCity.getPopulationTotal() == 0 || base.errorsFound(pCity);
	}
}
