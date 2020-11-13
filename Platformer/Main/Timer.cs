using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Main
{
    public class Timer
    {
        public int Time { get; private set; }

        public Action action;

        public Timer(int Time, Action action)
        {
            this.Time = Time;

            this.action = action;
        }

        public void Update()
        {

        }
    }
}
