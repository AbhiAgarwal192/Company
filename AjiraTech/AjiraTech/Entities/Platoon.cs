using System.Collections.Generic;

namespace AjiraTech.Entities
{
    public class Platoon
    {
        public List<Soldier> Soldiers;

        public Platoon()
        {
            Soldiers = new List<Soldier>();
        }
        public void AddSoldier(SoldierType soldierType, int count)
        {
            var soldier = new Soldier(soldierType,count);
            Soldiers.Add(soldier);
        }
    }
}
