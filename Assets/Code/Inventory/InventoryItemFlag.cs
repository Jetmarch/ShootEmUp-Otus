using System;

namespace Game
{
    [Flags]
    public enum InventoryItemFlag
    {
        NONE = 0,
        STACKABLE = 1,
        CONSUMABLE = 2,
        EQUIPPABLE = 4,
        EFFECTIBLE = 8
    }
}