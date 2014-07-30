using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    class IDGenerator
    {
        private static int _counter = -1;

        public static int GetID()
        {
            System.Threading.Interlocked.Increment(ref _counter);
            return _counter;
        }
    }
}
