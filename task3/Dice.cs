using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    public class Dice:IEnumerable<int>
    {
        public List<int> Values { get; private set; }

        public int Count => Values.Count;

        public int Modulo => Values.Count;

        public Dice(List<int> values)
        {
            Values = values;
        }

        public Dice()
        {
            Values = new();
        }

        public override string ToString()
        {
            return string.Join(",", Values);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Dice dice && dice.Values.Count==this.Values.Count)
            {
                for (int i = 0; i < dice.Count; i++)
                {
                    if (this.Values[i] != dice.Values[i])
                        return false;                    
                }
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.ToString());
        }

        public IEnumerator<int> GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Values.GetEnumerator();
        }       
    }
}
