using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BaseLibrary
{
    [Serializable]
    public class Payment
    {
        public string Name { get; }
        public string Description { get; }
        public float Charge { get; }
        public bool IsActive { get; }

        public Payment(string name, string description = null, float charge = 0, bool isActive = true)
        {
            Name = name;
            Description = description;
            Charge = charge;
            IsActive = isActive;
        }

        public decimal Apply(decimal amount)
        {
            if (Charge <= 0)
                return amount;

            return amount * (decimal)(1f + (Charge / 100f));
        }
    }
}
