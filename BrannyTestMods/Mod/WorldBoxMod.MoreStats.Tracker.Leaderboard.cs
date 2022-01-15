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

		public Dictionary<string, List<LeaderboardEntry>> leaderboards = new Dictionary<string, List<LeaderboardEntry>>();

		public int GetPositionOnLeaderboard(string type, int numStat) 
		{
			List<LeaderboardEntry> targetLeaderboard = GetLeaderboardFromType(type);

			// if there are none in this leaderboard
			// TO-DO add new leaderboard creation
			if (targetLeaderboard.Count <= 0)
			{
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
							bestIndex = targetLeaderboard.IndexOf(e);
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

		public List<LeaderboardEntry> GetLeaderboardFromType(string type) 
		{
			if (leaderboards.ContainsKey(type))
			{
				List<LeaderboardEntry> result;
				leaderboards.TryGetValue(type, out result);
				return result;
			}
			else 
			{
				return CreateNewLeaderboard(type);
			}
		}

		public List<LeaderboardEntry> CreateNewLeaderboard(string type) 
		{
			List <LeaderboardEntry> l = new List<LeaderboardEntry>();
			leaderboards.Add(type, l);
			Debug.Log("Created new leaderboard: " + type);
			return l;
		}

		public bool tryAddToLeaderboard(string type, string actorId, int numStat) 
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
				if (leaderboard.Count > 10)
					leaderboard.RemoveAt(leaderboard.Count - 1);

				// Check to see if our new actor is already on the leaderboard

				LeaderboardEntry newEntry = new LeaderboardEntry(_id, pos, numStat);

				if (CheckActorOnLeaderboard(newEntry, leaderboard))
				{
					RemoveActorFromLeaderboard(newEntry.actorId, leaderboard);
				}

				leaderboard.Insert(pos, newEntry);

				foreach (LeaderboardEntry e in leaderboard)
				{
					e.UpdatePosition(leaderboard.IndexOf(e));
				}

				leaderboards.Remove(type);
				leaderboards.Add(type, leaderboard);

				UpdateStatLeaderboard(type, leaderboard);

				return true;
			}

			return false;
		}

		// Compares a stat value against an existing single stat (non-leaderboard entry)
		// TODO - Break pieces of code from this method and tryAddStatToLeaderbaord into more reusable methods //less dupe code por favor
		public bool TryAddStat(string type, string actorId, int numStat) 
		{
			if (CompareStatToLeaderboards(type, numStat)) 
			{
				int pos = GetPositionOnLeaderboard(type, numStat);

				Debug.Log("Stat pos = " + pos);

				if (pos == 999) 
				{
					return false;
				}

				Actor mActor = MapBox.instance.getActorByID(actorId);

				string _id = BrannyActorManager.RememberActor(mActor);

				List<LeaderboardEntry> leaderboard = GetLeaderboardFromType(type);

				LeaderboardEntry newEntry = new LeaderboardEntry(_id, pos, numStat);

				if (CheckActorOnLeaderboard(newEntry, leaderboard))
				{
					RemoveActorFromLeaderboard(newEntry.actorId, leaderboard);
				}

				leaderboard.Insert(0, newEntry);

				foreach (LeaderboardEntry e in leaderboard)
				{
					e.UpdatePosition(leaderboard.IndexOf(e));
				}

				leaderboards.Remove(type);
				leaderboards.Add(type, leaderboard);

				UpdateStatLeaderboard(type, leaderboard);
				return true;
			}

			return false;
		}

		public bool CheckActorOnLeaderboard(LeaderboardEntry entry, List<LeaderboardEntry> leaderboard) 
		{
			bool result = false;

			foreach (LeaderboardEntry e in leaderboard) 
			{
				if (e.actorId == entry.actorId)
					result = true;
			}

			return result;
		}

		public void RemoveActorFromLeaderboard(string actorId, List<LeaderboardEntry> leaderboard)
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
		public bool CompareStatToLeaderboards(string type, int numStat) 
		{
			List<LeaderboardEntry> targetLeaderboard = GetLeaderboardFromType(type);

			// Are there any entries yet?
			if (targetLeaderboard.Count <= 0)
			{
				return true;
			}

			if (targetLeaderboard.Count >= 10)
			{
				// If our value is higher than the current heighest
				if (numStat > targetLeaderboard[targetLeaderboard.Count - 1].statValue)
				{
					return true;
				}
			}
			else 
			{
				return true;
			}

			return false;
		}
	}
}
