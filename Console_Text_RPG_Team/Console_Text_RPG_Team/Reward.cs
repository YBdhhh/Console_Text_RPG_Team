using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Console_Text_RPG_Team
{
    internal class Reward
    {
        public int gold;
        public int exp;

        public Reward(int gold, int exp)
        {
            this.gold = gold;
            this.exp = exp;
        }
    }
}
