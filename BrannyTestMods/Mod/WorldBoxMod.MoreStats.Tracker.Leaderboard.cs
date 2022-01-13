//extern alias ncms;
//using NCMS = ncms.NCMS;
using NCMS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HarmonyLib;

namespace BrannyTestMods
{
	public partial class WorldBoxMod
	{
		public ActorStatus killLeader;

		public struct LeaderboardEntry
		{
			public string actorId;
			public int position;
			public int statValue;

			public LeaderboardEntry(string id, int pos, int statValue)
			{
				this.actorId = id;
				this.position = pos;
				this.statValue = statValue;
			}

			public void UpdatePosition(int pos) 
			{
				position = pos;
			}
		}

		public static List<LeaderboardEntry> killLeaderboard = new List<LeaderboardEntry>();
		public static List<LeaderboardEntry> childrenLeaderboard = new List<LeaderboardEntry>();

		public static int GetPositionOnLeaderboard(string type, int numStat) 
		{
			List<LeaderboardEntry> targetLeaderboard = GetLeaderboardFromType(type);

			// if there are none in this leaderboard
			// TO-DO add new leaderboard creation
			if (targetLeaderboard.Count <= 0)
			{
				Debug.Log("Leaderboard doesn't exist, so pos = 0");
				return 0;
			}

			int bestIndex = 999;

			if (targetLeaderboard.Count < 10)
				bestIndex = targetLeaderboard.Count;

			int lastEntry = targetLeaderboard[targetLeaderboard.Count - 1].statValue;
			// if our stat is higher than that of the last entry on the leaderboard
			if (numStat > lastEntry)
			{
				foreach (LeaderboardEntry e in targetLeaderboard)
				{
					// The new entry belongs higher
					if (numStat > e.statValue)
					{
						if (targetLeaderboard.IndexOf(e) < bestIndex)
						{
							Debug.Log("our stat " + numStat + " is higher than current " + e.statValue + ", best index = " + bestIndex);
							bestIndex = targetLeaderboard.IndexOf(e);
							Debug.Log("We have a new best index of: " + bestIndex);
						}
					}
				}

				if (bestIndex == 999)
					Debug.Log("No Best Index found, does not belong");

				return bestIndex;
			}
			else 
			{
				return bestIndex;
			}
		}

		public static List<LeaderboardEntry> GetLeaderboardFromType(string type) 
		{
			List<LeaderboardEntry> targetLeaderboard;

			switch (type)
			{
				case "Kills":
					targetLeaderboard = killLeaderboard;
					break;
				case "Children":
					targetLeaderboard = childrenLeaderboard;
					break;
				default:
					targetLeaderboard = childrenLeaderboard;
					break;
			}

			return targetLeaderboard;
		}

		public static bool tryAddToLeaderboard(string type, string actorId, int numStat) 
		{
			if (CompareStatToLeaderboards(type, numStat)) 
			{
				int pos = GetPositionOnLeaderboard(type, numStat);

				if (pos == 999)
				{
					Debug.Log("Position on leaderboard returned 999");
					return false;
				}

				Actor mActor = MapBox.instance.getActorByID(actorId);

				string _id = BrannyActorManager.RememberActor(mActor);

				List<LeaderboardEntry> leaderboard = GetLeaderboardFromType(type);
				
				// If more than 10 entries, chop the last
				if (leaderboard.Count >= 10)
					leaderboard.RemoveAt(leaderboard.Count - 1);

				// Check to see if our new actor is already on the leaderboard

				LeaderboardEntry newEntry = new LeaderboardEntry(_id, pos, numStat);

				if (CheckActorOnLeaderboard(newEntry, leaderboard))
				{
					Debug.Log("Removing actor from leaderboard");
					RemoveActorFromLeaderboard(newEntry.actorId, leaderboard);
				}

				leaderboard.Insert(pos, newEntry);

				foreach (LeaderboardEntry e in leaderboard)
				{
					e.UpdatePosition(leaderboard.IndexOf(e));
				}

				if (pos == 0)
				{
					// Do something special
					Debug.Log("Brand new leader");
				}

				UpdateStatLeaderboard(type, leaderboard);

				return true;
			}

			Debug.Log("Reached end of tryAdd method");
			return false;
		}

		public static bool CheckActorOnLeaderboard(LeaderboardEntry entry, List<LeaderboardEntry> leaderboard) 
		{
			bool result = false;

			foreach (LeaderboardEntry e in leaderboard) 
			{
				if (e.actorId == entry.actorId)
					result = true;
			}

			return result;
		}

		public static void RemoveActorFromLeaderboard(string actorId, List<LeaderboardEntry> leaderboard)
		{
			int targetIndex = 999;

			foreach (LeaderboardEntry e in leaderboard) 
			{
				if (e.actorId == actorId) 
				{
					targetIndex = leaderboard.IndexOf(e);
				}
			}

			if (targetIndex != 999)
				leaderboard.RemoveAt(targetIndex);
		}

		// Returns true if the stat is higher than in the leaderboard
		public static bool CompareStatToLeaderboards(string type, int numStat) 
		{
			List<LeaderboardEntry> targetLeaderboard = GetLeaderboardFromType(type);

			// Are there any entries yet?
			if (targetLeaderboard.Count <= 0)
			{
				Debug.Log("Leaderboard does not exist, creating a new one and adding num stat");
				return true;
			}

			if (targetLeaderboard.Count >= 10)
			{
				// If our value is higher than the current heightest
				if (numStat > targetLeaderboard[targetLeaderboard.Count - 1].statValue)
				{
					Debug.Log("New Leaderboard entry " + numStat + " belongs on the leaderboard");
					return true;
				}
			}
			else 
			{
				return true;
			}

			Debug.Log("statvalue: " + numStat + " is not high enough to get onto the leaderboard");
			return false;
		}
	}
}
