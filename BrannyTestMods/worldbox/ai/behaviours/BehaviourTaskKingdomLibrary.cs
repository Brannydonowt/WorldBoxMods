using System;

namespace ai.behaviours
{
	// Token: 0x020003B3 RID: 947
	public class BehaviourTaskKingdomLibrary : AssetLibrary<BehaviourTaskKingdom>
	{
		// Token: 0x06001436 RID: 5174 RVA: 0x000AA690 File Offset: 0x000A8890
		public override void init()
		{
			base.init();
			BehaviourTaskKingdom behaviourTaskKingdom = new BehaviourTaskKingdom();
			behaviourTaskKingdom.id = "nothing";
			BehaviourTaskKingdom pAsset = behaviourTaskKingdom;
			this.t = behaviourTaskKingdom;
			this.add(pAsset);
			BehaviourTaskKingdom behaviourTaskKingdom2 = new BehaviourTaskKingdom();
			behaviourTaskKingdom2.id = "check_culture";
			pAsset = behaviourTaskKingdom2;
			this.t = behaviourTaskKingdom2;
			this.add(pAsset);
			this.t.addBeh(new KingdomBehCheckCulture());
			BehaviourTaskKingdom behaviourTaskKingdom3 = new BehaviourTaskKingdom();
			behaviourTaskKingdom3.id = "wait1";
			pAsset = behaviourTaskKingdom3;
			this.t = behaviourTaskKingdom3;
			this.add(pAsset);
			this.t.addBeh(new KingdomBehRandomWait(0f, 1f));
			BehaviourTaskKingdom behaviourTaskKingdom4 = new BehaviourTaskKingdom();
			behaviourTaskKingdom4.id = "wait_random";
			pAsset = behaviourTaskKingdom4;
			this.t = behaviourTaskKingdom4;
			this.add(pAsset);
			this.t.addBeh(new KingdomBehRandomWait(0f, 5f));
			BehaviourTaskKingdom behaviourTaskKingdom5 = new BehaviourTaskKingdom();
			behaviourTaskKingdom5.id = "do_checks";
			pAsset = behaviourTaskKingdom5;
			this.t = behaviourTaskKingdom5;
			this.add(pAsset);
			this.t.addBeh(new KingdomBehCheckCapital());
			this.t.addBeh(new KingdomBehCheckKing());
			this.t.addBeh(new KingdomBehRandomWait(0f, 1f));
			BehaviourTaskKingdom behaviourTaskKingdom6 = new BehaviourTaskKingdom();
			behaviourTaskKingdom6.id = "check_dead_kingdom";
			pAsset = behaviourTaskKingdom6;
			this.t = behaviourTaskKingdom6;
			this.add(pAsset);
			this.t.addBeh(new KingdomCheckDeadKingdom());
			BehaviourTaskKingdom behaviourTaskKingdom7 = new BehaviourTaskKingdom();
			behaviourTaskKingdom7.id = "check_attack_target";
			pAsset = behaviourTaskKingdom7;
			this.t = behaviourTaskKingdom7;
			this.add(pAsset);
			this.t.addBeh(new KingdomCheckAttackTarget());
			BehaviourTaskKingdom behaviourTaskKingdom8 = new BehaviourTaskKingdom();
			behaviourTaskKingdom8.id = "check_revolts";
			pAsset = behaviourTaskKingdom8;
			this.t = behaviourTaskKingdom8;
			this.add(pAsset);
			this.t.addBeh(new KingdomCheckRevolts());
			BehaviourTaskKingdom behaviourTaskKingdom9 = new BehaviourTaskKingdom();
			behaviourTaskKingdom9.id = "check_diplomacy";
			pAsset = behaviourTaskKingdom9;
			this.t = behaviourTaskKingdom9;
			this.add(pAsset);
			this.t.addBeh(new KingdomCheckDiplomacy());
		}
	}
}
