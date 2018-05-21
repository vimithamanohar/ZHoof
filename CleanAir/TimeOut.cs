using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Deformable_Terrain
{
    public class TimeOut
    {
        static int timelimit = 600;
        int timecount;
        public TimeOut()
        {
                   
         timecount = 0;
        }
        public bool CheckTime()
        {
            timecount++;
            if (timecount > timelimit)
                return false;
            else
                return true;
        }

    }
}
