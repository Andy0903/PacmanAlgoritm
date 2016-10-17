using Microsoft.Xna.Framework;

namespace Pacman
{
    class VioletGhost : Ghost
    {
        #region Properties
        private bool MovingVertically
        {
            get { return (Direction == Direction.Up || Direction == Direction.Down) ? true : false; }
        }
        #endregion

        #region Constructors
        public VioletGhost(Player aPlayer, GameBoard aGameBoard, Vector2 aPosition)
            : base(aPlayer, aGameBoard, aPosition, 1)
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
        private void PathFinding()
        {
            Vector2? upTarget = myGameBoard.ValidTargetPosition(Row - 1, Column);
            Vector2? leftTarget = myGameBoard.ValidTargetPosition(Row, Column - 1);
            Vector2? rightTarget = myGameBoard.ValidTargetPosition(Row, Column + 1);
            Vector2? downTarget = myGameBoard.ValidTargetPosition(Row + 1, Column);
            int randomNumber = GameBoard.myRandom.Next(0, 2);

            if ((MovingVertically) && (FoundHorizontalRoad(leftTarget, rightTarget)))
            {
                if (FoundTwoHorizontalRoads(leftTarget, rightTarget))
                {
                    RandomizeLeftOrRight(randomNumber);
                }
                else
                {
                    TurnToValidHorzontalRoad(leftTarget);
                }
            }
            else if (((MovingVertically) == false) && (FoundVerticalRoad(upTarget, downTarget)))
            {
                if (FoundTwoVerticalRoads(upTarget, downTarget))
                {
                    RandomizeUpOrDown(randomNumber);
                }
                else
                {
                    TurnToValidVerticalRoad(upTarget);
                }
            }
            else if (FoundDeadEnd(upTarget, leftTarget, downTarget, rightTarget))
            {
                ReverseDirection();
            }
        }

        private Vector2? Movement()
        {
            Vector2? target = null;

            PathFinding();
          
            target = NextTarget(Direction);

            return target;
        }

        private void RandomizeLeftOrRight(int aNumber)
        {
            Direction = aNumber == 0 ? Direction.Left : Direction.Right;
        }

        private void RandomizeUpOrDown(int aNumber)
        {
            Direction = aNumber == 0 ? Direction.Up : Direction.Down;
        }

        private bool FoundHorizontalRoad(Vector2? aLeftTarget, Vector2? aRightTarget)
        {
            return (aLeftTarget != null || aRightTarget != null) ? true : false;
        }

        private bool FoundVerticalRoad(Vector2? aUpTarget, Vector2? aDownTarget)
        {
            return (aUpTarget != null || aDownTarget != null) ? true : false;
        }

        private bool FoundTwoHorizontalRoads(Vector2? aLeftTarget, Vector2? aRightTarget)
        {
            if (aLeftTarget != null && aRightTarget != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool FoundTwoVerticalRoads(Vector2? aUpTarget, Vector2? aDownTarget)
        {
            if (aUpTarget != null && aDownTarget != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void TurnToValidHorzontalRoad(Vector2? aLeftTarget)
        {
            Direction = aLeftTarget != null ? Direction.Left : Direction.Right;
        }

        private void TurnToValidVerticalRoad(Vector2? aUpTarget)
        {
            Direction = aUpTarget != null ? Direction.Up : Direction.Down;
        }

        private bool FoundDeadEnd(Vector2? aUpTarget, Vector2? aLeftTarget, Vector2? aDownTarget, Vector2? aRightTarget)
        {
            if ((Direction == Direction.Up && aUpTarget == null)
                || (Direction == Direction.Left && aLeftTarget == null)
                || (Direction == Direction.Down && aDownTarget == null)
                || (Direction == Direction.Right && aRightTarget == null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ReverseDirection()
        {
            switch (Direction)
            {
                case Direction.Up:
                    Direction = Direction.Down;
                    break;
                case Direction.Left:
                    Direction = Direction.Right;
                    break;
                case Direction.Down:
                    Direction = Direction.Up;
                    break;
                case Direction.Right:
                    Direction = Direction.Left;
                    break;
            }
        }
        #endregion
    }
}

