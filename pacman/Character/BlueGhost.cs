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
        override protected Vector2? AliveMovement()
        {
            return Movement();
        }
        #endregion

        #region Private methods
        Vector2? Movement()
        {
            int randomNumber = GameBoard.myRandom.Next(0, 4);

            Vector2? target = null;

            switch (randomNumber)
            {
                case 0:
                    Direction = Direction.Up;
                    target = myGameBoard.ValidTargetPosition(Row - 1, Column);
                    break;
                case 1:
                    Direction = Direction.Left;
                    target = myGameBoard.ValidTargetPosition(Row, Column - 1);
                    break;
                case 2:
                    Direction = Direction.Down;
                    target = myGameBoard.ValidTargetPosition(Row + 1, Column);
                    break;
                case 3:
                    Direction = Direction.Right;
                    target = myGameBoard.ValidTargetPosition(Row, Column + 1);
                    break;
            }
            return target;
        }
        #endregion
    }
}
