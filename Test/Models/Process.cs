using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class Process : IComparable<Process>
    {
        public int arrival_time { get; set; }

        public int length { get; set; }

        public int priority { get; set; }

        public bool added { get; set; }

        public int done { get; set; }
        public char name { get; set; }

        public Process(int arrival, int length, char name)
        {
            this.arrival_time = arrival;
            this.length = length;
            this.name = name;
            this.done = 1;
            this.priority = 1;
            this.added = false;
        }

        public List<Process> Info { get; set; }

        public int CompareTo(Process other)
        {
            return this.arrival_time.CompareTo(other.arrival_time);
        }

        public string print()
        {
            return this.length + "\n";
        }
    }

   
}
