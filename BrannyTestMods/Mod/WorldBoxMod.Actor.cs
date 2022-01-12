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
		public static void Branny_Actor_Patch(Harmony harmony) 
		{
			Helper.Utils.HarmonyPatching(harmony, "prefix", AccessTools.Method(typeof(Actor), "killHimself"), AccessTools.Method(typeof(BrannyActorManager), "killHimself_prefix"));
			Debug.Log("Prefix Actor_killHimself DONE");
		}

		static Dictionary<string, BrannyActor> memorableActors = new Dictionary<string, BrannyActor>();

		static Dictionary<string, string> trackedLiveActors = new Dictionary<string, string>();

		static List<string> trackedIds = new List<string>();

		public static string RememberActor(Actor toRemember) 
		{
			BrannyActor savedActor;
			ActorStatus stat = Helper.Reflection.GetActorData(toRemember);

			if (trackedLiveActors.ContainsKey(stat.actorID))
			{
				// We are already tracking this actor, maybe update the information?
				string t = "";
				trackedLiveActors.TryGetValue(stat.actorID, out t);
				memorableActors.TryGetValue(t, out savedActor);
				// TODO - This might not be necessary
				savedActor = new BrannyActor(toRemember);
				savedActor._id = t;
				memorableActors[savedActor._id] = savedActor;
			}
			else
			{
				savedActor = new BrannyActor(toRemember);
				trackedLiveActors.Add(stat.actorID, savedActor._id);
				memorableActors.Add(savedActor._id, savedActor);
			}

			return savedActor._id;
		}

		public static bool DoesActorExist(string id)
		{
			// Have we saved this actor id before?
			if (memorableActors.ContainsKey(id))
				return true;

			return false;
		}

		public static bool HasID(string id) 
		{
			if (trackedIds.Contains(id))
				return true;
			else
				return false;
		}

		public static void AddTrackedID(string id) 
		{
			trackedIds.Add(id);
		}

		public static BrannyActor GetRememberedActor(string id)
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

		public static void KillRememberedActor(string id) 
		{
			BrannyActor toKill = GetRememberedActor(id);

			trackedLiveActors.Remove(toKill.actorID);

			// I may have to place the data back inside of the dictionary, we will see.
			toKill.alive = false;
			toKill.yod = MapBox.instance.mapStats.year;
		}

		public static void killHimself_prefix(Actor __instance) 
		{
			ActorStatus data = Helper.Reflection.GetActorData(__instance);

			// Did somebody we were tracking just die?
			if (trackedLiveActors.ContainsKey(data.actorID)) 
			{
				string bId = "";
				trackedLiveActors.TryGetValue(data.actorID, out bId);
				KillRememberedActor(bId);
			}
		}
	}

	// Memorable Actors will be cached as a BrannyActor and saved as a reference for future use.
	// TODO - I'll need some serialization on this.
	public class BrannyActor
	{
		public string _id;

		public bool alive;
		public string actorID;

		public string firstName;
		public string kingdom;

		// Misc stats
		public int kills;
		public int age;
		public int born;
		public int yod;
		public int children;

		public string faveFood;

		ActorGender gender;

		private ActorStatus status;

		public BrannyActor(Actor inActor)
		{
			ActorStatus stat = Helper.Reflection.GetActorData(inActor);
			actorID = stat.actorID;

			// We want to copy over any important stats that we want to track.
			kills = stat.kills;
			age = stat.age;
			born = stat.bornTime;
			
			firstName = stat.firstName;
			kingdom = inActor.kingdom.name;
			children = stat.children;
			faveFood = stat.favoriteFood;
			gender = stat.gender;

			alive = true;

			status = getActorStatus();
			_id = GenerateID();
			BrannyActorManager.AddTrackedID(_id);
		}

		string GenerateID() 
		{
			string i = Toolbox.randomInt(1000, 10000).ToString();

			if (BrannyActorManager.HasID(i))
				return GenerateID();
			else
				return i;
		}

		public Actor getActor()
		{
			if (alive)
			{
				return MapBox.instance.getActorByID(actorID);
			}
			else 
			{
				return null;
			}
		}

		public ActorStatus getActorStatus() 
		{
			ActorStatus result = new ActorStatus();
			result.alive = alive;
			result.actorID = actorID;
			result.firstName = firstName;
			result.kills = kills;
			result.age = age;
			result.bornTime = born;

			return result;
		}
	}
	
	public partial class WorldBoxMod
	{
		
	}
}
