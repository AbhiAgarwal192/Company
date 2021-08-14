using AjiraTech.Entities;
using System;
using System.Collections.Generic;

namespace AjiraTech
{
    public class Rules
    {
        private List<int>[] adj;
        private readonly int numberOfSoliderClasses = 6;
        private readonly int militia = 0;
        private readonly int spearmen = 1;
        private readonly int lightCavalry = 2;
        private readonly int heavyCavalry = 3;
        private readonly int footArcher = 4;
        private readonly int cavalryArcher = 5;
        private readonly int advantageModifier = 2;
        private Graph graph;
        public void Initialize()
        {
            adj = new List<int>[numberOfSoliderClasses];
            for (int i=0;i<numberOfSoliderClasses;i++)
            {
                adj[i] = new List<int>();
            }

            adj[militia].Add(spearmen);
            adj[militia].Add(lightCavalry);

            adj[spearmen].Add(lightCavalry);
            adj[spearmen].Add(heavyCavalry);

            adj[lightCavalry].Add(footArcher);
            adj[lightCavalry].Add(cavalryArcher);

            adj[heavyCavalry].Add(militia);
            adj[heavyCavalry].Add(footArcher);
            adj[heavyCavalry].Add(lightCavalry);

            adj[cavalryArcher].Add(spearmen);
            adj[cavalryArcher].Add(heavyCavalry);

            adj[footArcher].Add(militia);
            adj[footArcher].Add(cavalryArcher);

            graph = new Graph();
        }
        public string DeterminePlatoonForWinning(Platoon myPlatoon, Platoon opponentPlatoon)
        {
            var mySet = new HashSet<SoldierType>();
            var opponentSet = new HashSet<SoldierType>();
            int wins = Combinations(myPlatoon,opponentPlatoon, 0, mySet, opponentSet);
            if (wins < 3)
            {
                return Constants.NoChanceOfWinning;
            }
            return ParseResult(myPlatoon, mySet);
        }
        public int Combinations(Platoon myPlatoon, Platoon opponentPlatoon, int wins, HashSet<SoldierType> mySet, HashSet<SoldierType> opponentSet)
        {
            foreach (var mysoldier in myPlatoon.Soldiers)
            {
                if (mySet.Contains(mysoldier.SoldierType))
                {
                    continue;
                }

                mySet.Add(mysoldier.SoldierType);

                foreach (var opponentSoldier in opponentPlatoon.Soldiers)
                {
                    if (opponentSet.Contains(opponentSoldier.SoldierType))
                    {
                        continue;
                    }

                    //It is a connected graph so we dont have to check for path to exist.
                    int level = graph.NumberOfEdges(Convert.ToInt32(mysoldier.SoldierType), Convert.ToInt32(opponentSoldier.SoldierType), adj, numberOfSoliderClasses);

                    int pathModifier = Convert.ToInt32(Math.Pow(advantageModifier, level));
                    
                    opponentSet.Add(opponentSoldier.SoldierType);
                    
                    if (mysoldier.Count * pathModifier > opponentSoldier.Count)
                    {
                        wins++;
                    }

                    wins = Combinations(myPlatoon, opponentPlatoon, wins , mySet, opponentSet);
                    
                    if (wins >= 3 && mySet.Count == myPlatoon.Soldiers.Count && opponentSet.Count == opponentPlatoon.Soldiers.Count)
                    {
                        return wins;
                    }

                    opponentSet.Remove(opponentSoldier.SoldierType);
                }

                mySet.Remove(mysoldier.SoldierType);
            }
            return wins;
        }
        private string ParseResult(Platoon myPlatoon , HashSet<SoldierType> mySet)
        {
            string result = string.Empty;
            foreach (SoldierType type in mySet)
            {
                int index = Convert.ToInt32(type);
                if (string.IsNullOrEmpty(result))
                {
                    result = $"{type}#{myPlatoon.Soldiers[index].Count}";
                }
                else
                {
                    result = $"{result};{type}#{myPlatoon.Soldiers[index].Count}";
                }
            }
            return result;
        }
    }
}
