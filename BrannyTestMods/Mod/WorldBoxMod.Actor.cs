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
	// I need a class that can track any actor that may have existed at one point
	public static class BrannyActorManager 
	{
		static Dictionary<string, BrannyActor> memorableActors;

		public static void RememberActor(Actor toRemember) 
		{
			BrannyActor savedActor = new BrannyActor(toRemember);
			memorableActors.Add(savedActor.getActorID(), savedActor);
		}

		public static bool DoesActorExist(Actor a) 
		{
			if ()
		}
	}

	// Memorable Actors will be cached as a BrannyActor and saved as a reference for future use.
	// TODO - I'll need some serialization on this.
	public class BrannyActor : Actor 
	{
		public BrannyActor(Actor inActor) 
		{
			
		}

		public ActorStatus getActorStatus() 
		{
			return Helper.Reflection.GetActorData(this);
		}

		public ActorStats getActorStats() 
		{
			return Helper.Reflection.GetActorStats(this);
		}

		public string getActorID() 
		{
			return getActorStatus().actorID;
		}

	}
	
	public partial class WorldBoxMod
	{
		
	}
}
