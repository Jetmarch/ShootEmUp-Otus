using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Hero
    {
        public int Damage { get; set; }
        public int Mana { get; set; }
        public Equipment Equipment { get; }

        public Hero()
        {
            Equipment = new Equipment();
        }
    }
}