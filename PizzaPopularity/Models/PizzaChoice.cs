using System;
using System.Linq;

namespace PizzaPopularity.Models
{
    /// <summary>
    /// Pizza topping choice
    /// </summary>
    public class PizzaChoice : IEquatable<PizzaChoice>
    {
        /// <summary>
        /// toppings array
        /// </summary>
        public string[] toppings;

        /// <summary>
        /// equality of toppings type
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if equal - false if not</returns>
        public bool Equals(PizzaChoice other)
        {
            bool ret = false;

            // check the count
            if (this.toppings.Count() == other.toppings.Count())
            {
                // see if the array contains the same toppings
                if (this.toppings.ToList().OrderBy(b => b).SequenceEqual(other.toppings.ToList().OrderBy(o => o)))
                {
                    ret = true;
                }
            }

            return ret;
        }

        /// <summary>
        /// override for IEquatable
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>true if equal - false if not</returns>
        public override bool Equals(object obj)
        {
            // Call the implementation from IEquatable
            return Equals((PizzaChoice)obj);
        }

        /// <summary>
        /// get the hash code for PizzaChoice object
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            int ret = 0;
            this.toppings.ToList().OrderBy(t => t).ToList().ForEach(o => ret += o.GetHashCode());

            return ret;
        }

        /// <summary>
        /// Return the object how we want it to look
        /// </summary>
        /// <returns>string representation</returns>
        public override string ToString()
        {
            // String representation.
            string sRet = "\r\n";
            this.toppings.ToList().ForEach(b => sRet += "\t" + b.ToString() + "\r\n");
            return sRet;
        }
    }
}
