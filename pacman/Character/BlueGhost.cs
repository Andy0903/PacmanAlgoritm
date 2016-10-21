using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class BlueGhost : Ghost
    {
        #region Constructors
        public BlueGhost(Player aPlayer, GameBoard aGameBoard, Vector2 aPosition)
            : base(aPlayer, aGameBoard, aPosition, 2)
        {
        }
        #endregion

        #region Protected methods
        protected override List<Tile> FindPath(Graph aGraph, Tile aStart, Tile aGoal)
        {
            return null;
        }
        #endregion

        #region Private methods
        #endregion

    }
}
