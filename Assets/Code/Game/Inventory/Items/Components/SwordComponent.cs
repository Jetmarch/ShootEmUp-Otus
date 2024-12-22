using System;

namespace Game
{
    [Serializable]
    public class SwordComponent : EquipmentComponent
    {
        public int Damage;

        public IItemComponent Clone()
        {
            return new SwordComponent
            {
                Damage = Damage,
                EquipmentType = EquipmentType
            };
        }
    }
}