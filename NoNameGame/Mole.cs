using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NoNameGame
{
    class  Mole
    {
        public int x = 190;
        public int y = 150;

        public Bitmap mole = new Bitmap(NoNameGame.Properties.Resources.mole);

        public Rectangle rect;

        public Boolean hit = true;

        public Mole()
        {
            rect = new Rectangle(x + 30, y + 30, 40, 50);

        }

        
    }




}
