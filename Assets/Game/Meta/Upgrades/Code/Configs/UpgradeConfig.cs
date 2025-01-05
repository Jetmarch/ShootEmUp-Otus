using UnityEngine;

namespace Game.Meta.Upgrades
{
    public abstract class UpgradeConfig : ScriptableObject
    {
        public string Id => _id;
        public string DisplayName => _displayName;
        public int MaxLevel => _maxLevel;
        public TableValue PriceTableValue => _priceTableValue;
        
        [SerializeField] protected string _id;
        [SerializeField] protected string _displayName;
        [SerializeField] protected int _maxLevel;
        [SerializeField] protected TableValue _priceTableValue;
        public abstract Upgrade CreateUpgrade();

        protected void Validate()
        {
            _priceTableValue.FillLevelPrices(_maxLevel);
        }
    }
}