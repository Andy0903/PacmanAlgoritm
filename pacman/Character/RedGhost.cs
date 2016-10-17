using Microsoft.Xna.Framework;

namespace Pacman
{
    class RedGhost : Ghost
    {
        #region Constructors
        public RedGhost(Player aPlayer, GameBoard aGameBoard, Vector2 aPosition)
            : base(aPlayer, aGameBoard, aPosition, 0)
        {
        }
        #endregion

        #region Protected methods
        override protected Vector2? AliveMovement()
        {
            return GoToPositionEfficiently(Player.Column, Player.Row);
        }
        #endregion
    }
}
