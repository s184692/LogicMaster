using LogicMaster.gui.pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicMaster.gameplay
{
    public class GameManager
    {
        private Game game { get; set; }

        public GameManager(Game game)
        {
            this.game = game;
        }
    }
}
