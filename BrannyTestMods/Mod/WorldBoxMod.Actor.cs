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
		static Dictionary<string, BrannyActor> memorableActors = new Dictionary<string, BrannyActor>();

		public static void RememberActor(Actor toRemember) 
		{			
			BrannyActor savedActor = new BrannyActor(toRemember);

			// We want to replace any saved data
			if (memorableActors.ContainsKey(savedActor.actorID))
				memorableActors.Remove(savedActor.actorID);

			Debug.Log("Made Actor");
			memorableActors.Add(savedActor.actorID, savedActor);
			Debug.Log("Adding to memorable actors");
		}

		public static bool DoesActorExist(string id) 
		{
			// Have we saved this actor id before?
			if (memorableActors.ContainsKey(id))
				return true;

			return false;
		}

		public static Actor GetRememberedActor(string id) 
		{
			if (DoesActorExist(id)) 
			{
				BrannyActor result;
                _ = memorableActors.TryGetValue(id, out result);
				return result;
			}

			Debug.Log("We don't seem to have saved that requested actor");
			return null;
		}
	}

	// Memorable Actors will be cached as a BrannyActor and saved as a reference for future use.
	// TODO - I'll need some serialization on this.
	public class BrannyActor : Actor 
	{
		public Actor basedOn;
		public string actorID;

		public BrannyActor(Actor inActor) : base() 
		{
			Debug.Log("Branny actor constructor");
			basedOn = inActor;
			Debug.Log("Set based on");
			actorID = Helper.Reflection.GetActorData(inActor).actorID;
			Debug.Log("ID = : " + actorID);
		}

		public ActorStatus getActorStatus() 
		{
			Debug.Log("Getting Status Object");
			return Helper.Reflection.GetActorData(basedOn);
		}

		public ActorStats getActorStats() 
		{
			return Helper.Reflection.GetActorStats(basedOn);
		}

		public string getActorID()
		{
			Debug.Log("Getting actor ID");
			return getActorStatus().actorID;
		}
	}
	
	public partial class WorldBoxMod
	{
		
	}
}
