using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NoNameGame
{
    class Player
    {

        public int mx = 480;
        public int my = 320;
        public int bx;
        public int by;
        public Boolean flag = false;
        public Bitmap aim = new Bitmap(NoNameGame.Properties.Resources.aim);
        public Bitmap blood = new Bitmap(NoNameGame.Properties.Resources.blood);
        public String name = "Player";

    }
}
