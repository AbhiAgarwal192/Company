using AjiraTech.Entities;
using System;

namespace AjiraTech
{
    public static class InputExtensions
    {
        public static Platoon ParsePlatoonString(this string str)
        {
            var platoon = new Platoon();
            var platoonUnitArray = str.Split(';');
            foreach (var unit in platoonUnitArray)
            {
                var soldier = unit.Split('#');
                if (string.Equals(soldier[0],Constants.Militia, StringComparison.InvariantCultureIgnoreCase))
                {
                    platoon.AddSoldier(SoldierType.Militia, Convert.ToInt32(soldier[1]));
                }
                else if (string.Equals(soldier[0], Constants.Spearmen, StringComparison.InvariantCultureIgnoreCase))
                {
                    platoon.AddSoldier(SoldierType.Spearmen, Convert.ToInt32(soldier[1]));
                }
                else if (string.Equals(soldier[0], Constants.LightCavalry, StringComparison.InvariantCultureIgnoreCase))
                {
                    platoon.AddSoldier(SoldierType.LightCavalry, Convert.ToInt32(soldier[1]));
                }
                else if (string.Equals(soldier[0], Constants.HeavyCavalry, StringComparison.InvariantCultureIgnoreCase))
                {
                    platoon.AddSoldier(SoldierType.HeavyCavalry, Convert.ToInt32(soldier[1]));
                }
                else if (string.Equals(soldier[0], Constants.CavalryArcher, StringComparison.InvariantCultureIgnoreCase))
                {
                    platoon.AddSoldier(SoldierType.CavalryArcher, Convert.ToInt32(soldier[1]));
                }
                else
                {
                    platoon.AddSoldier(SoldierType.FootArcher, Convert.ToInt32(soldier[1]));
                }
            }
            return platoon;
        }
    }
}
