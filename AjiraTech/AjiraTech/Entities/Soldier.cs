namespace AjiraTech.Entities
{
    public class Soldier
    {
        public int Count;
        public SoldierType SoldierType;

        public Soldier(SoldierType soldierType, int count)
        {
            Count = count;
            SoldierType = soldierType;
        }
    }
}
