using System;
using System.Collections.Generic;
using System.Text;

namespace Console_Text_RPG_Team
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            Player player = new Player();
            SceneShop sceneShop = new SceneShop();
            sceneShop.Start(player);
        }
    }
}
