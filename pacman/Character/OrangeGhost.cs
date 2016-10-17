using Microsoft.Xna.Framework;

namespace Pacman
{
    class OrangeGhost : Ghost
    {
        #region Constructors
        public OrangeGhost(Player aPlayer, GameBoard aGameBoard, Vector2 aPosition)
            : base(aPlayer, aGameBoard, aPosition, 3)
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
        private Vector2? Movement()
        {
            Vector2? target = NextTarget(Direction);

            if (target == null)
            {
                CollisionWithWall(target);

            }
            return target;
        }

        private void CollisionWithWall(Vector2? aTarget)
        {
            int randomNumber = GameBoard.myRandom.Next(0, 2);

            if (Direction == Direction.Up || Direction == Direction.Down)
            {
                aTarget = RandomizeLeftOrRight(randomNumber);
            }
            else
            {
                aTarget = RandomizeUpOrDown(randomNumber);
            }
        }

        private Vector2? RandomizeLeftOrRight(int aNumber)
        {
            switch (aNumber)
            {
                case 0:
                    Direction = Direction.Left;
                    return myGameBoard.ValidTargetPosition(Row, Column - 1);
                case 1:
                    Direction = Direction.Right;
                    return myGameBoard.ValidTargetPosition(Row, Column + 1);
            }
            return null;
        }

        private Vector2? RandomizeUpOrDown(int aNumber)
        {
            switch (aNumber)
            {
                case 0:
                    Direction = Direction.Up;
                    return myGameBoard.ValidTargetPosition(Row - 1, Column);
                case 1:
                    Direction = Direction.Down;
                    return myGameBoard.ValidTargetPosition(Row + 1, Column);
            }
            return null;
        }
        #endregion
    }
}
