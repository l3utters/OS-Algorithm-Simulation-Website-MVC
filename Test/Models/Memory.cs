using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class Memory
    {
        public decimal Total_RAM { get; set; }

        public decimal Reserved_RAM { get; set; }

        public decimal Frame_Size { get; set; }

        public decimal Usable_RAM { get; set; }

        public decimal Total_Frames { get; set; }

        public Memory(decimal Total, decimal Reserved, decimal frame_size)
        {
            Total_RAM = Total;
            Reserved_RAM = Reserved;
            Frame_Size = frame_size;

            Usable_RAM = Total_RAM - Reserved_RAM;

            Total_Frames = Math.Floor((Usable_RAM * 1024) / Frame_Size);
        }
    }

}
