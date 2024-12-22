using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Equipment 
    {
        public event Action<InventoryItem> OnItemEquipped;
        public event Action<InventoryItem> OnItemUnequipped;
        
        [SerializeField] private readonly Dictionary<EquipmentType, InventoryItem> _equipment = new();
        
        public void AddItem(EquipmentType equipmentType, InventoryItem prototypeItem, Inventory inventory)
        {
            EquipmentUseCases.AddEquipment(equipmentType, prototypeItem, inventory, this);
        }

        public void Add(EquipmentType equipmentType, InventoryItem prototypeItem)
        {
            _equipment.Add(equipmentType, prototypeItem);
        }

        public void RemoveItem(EquipmentType equipmentType, Inventory inventory)
        {
            EquipmentUseCases.RemoveEquipment(equipmentType, inventory, this);
        }

        public void Remove(EquipmentType equipmentType)
        {
            _equipment.Remove(equipmentType);
        }

        public InventoryItem Get(EquipmentType equipmentType)
        {
            return _equipment.GetValueOrDefault(equipmentType);
        }

        public void NotifyItemAdded(InventoryItem item)
        {
            OnItemEquipped?.Invoke(item);
        }

        public void NotifyItemRemoved(InventoryItem item)
        {
            OnItemUnequipped?.Invoke(item);
        }
    }
}