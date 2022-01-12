//extern alias ncms;
//using NCMS = ncms.NCMS;
using NCMS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using UnityEngine.EventSystems;
using HarmonyLib;

namespace BrannyTestMods
{
	public class BrannyActor : Actor 
	{
		public ActorStatus getActorStatus() 
		{
			return Helper.Reflection.GetActorData(this);
		}

		public ActorStats getActorStats() 
		{
			return Helper.Reflection.GetActorStats(this);
		}

	}
	
	public partial class WorldBoxMod
	{
		
	}
}
