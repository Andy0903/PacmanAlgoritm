using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pacman
{
    abstract class Ghost : Character
    {
        #region Member variables
        GhostHealthState myGhostHealthState;

        protected readonly int myDefaultFrameYIndex;
        #endregion

        #region Properties
        protected Player Player
        {
            get;
            private set;
        }
        #endregion

        #region Constructors
        public Ghost(Player aPlayer, GameBoard aGameBoard, Vector2 aPosition, int aDefaultFrameYIndex)
            : base("Ghosts", aPosition, 32, 8, 5, 2, 1, 100, aGameBoard, 280)
        {
            myDefaultFrameYIndex = aDefaultFrameYIndex;
            InitializeMemberVariables(aPlayer);
        }
        #endregion

        #region Public methods
        public override void Reset()
        {
            SetDefaultNonParameterMemberVariables();
            base.Reset();
        }

        public override void Update(GameTime aGameTime)
        {
            UpdateHealthState();
            Collision();
            base.Update(aGameTime);
        }
        #endregion

        #region Protected methods    
        abstract protected Vector2? AliveMovement();

        protected override Vector2? NextTarget()
        {
            switch (myGhostHealthState)
            {
                case GhostHealthState.Alive:
                    return AliveMovement();
                case GhostHealthState.Scared:
                    return AvoidPositionEfficiently(Player.Position);
                case GhostHealthState.Dead:
                    return GoToPositionEfficiently((int)SpawnPosition.X / Tile.Size, (int)SpawnPosition.Y / Tile.Size);
            }
            return null;
        }

        protected Vector2? AvoidPositionEfficiently(Vector2 aPosition)
        {
            Direction bestDirection = Direction;
            Vector2? bestTarget = null;
            float bestDistance = Vector2.Distance(Position, aPosition);

            foreach (Direction direction in Enum.GetValues(typeof(Direction)).Cast<Direction>()) //Foreach direction in enum Direction
            {
                Vector2? target = NextTarget(direction);

                if (target != null)
                {
                    float distance = Vector2.Distance((Vector2)target, aPosition);
                    if (bestDistance < distance)
                    {
                        bestDirection = direction;
                        bestTarget = target;
                        bestDistance = distance;
                    }
                }
            }

            Direction = bestDirection;
            return bestTarget;
        }

        protected Vector2? NextTarget(Direction aDirection)
        {
            switch (aDirection)
            {
                case Direction.Up:
                    return myGameBoard.ValidTargetPosition(Row - 1, Column);
                case Direction.Left:
                    return myGameBoard.ValidTargetPosition(Row, Column - 1);
                case Direction.Down:
                    return myGameBoard.ValidTargetPosition(Row + 1, Column);
                case Direction.Right:
                    return myGameBoard.ValidTargetPosition(Row, Column + 1);
            }
            return null;
        }

        protected Vector2? GoToPositionEfficiently(int aColumn, int aRow)
        {
            Tile nextTile = FindNextTile(aColumn, aRow);

            if (nextTile == null)
            {
                return null;
            }

            Vector2? bestTarget = myGameBoard.ValidTargetPosition(nextTile.Row, nextTile.Column);
            SetDirectionTowardTile(nextTile);

            return bestTarget;
        }

        override protected void CalculateSourceRectangle(GameTime aGameTime)
        {
            myFrameTimeCounterMilliseconds -= aGameTime.ElapsedGameTime.Milliseconds;

            if (myFrameTimeCounterMilliseconds <= 0)
            {
                myFrameTimeCounterMilliseconds = myTimePerFrameMilliseconds;

                switch (myGhostHealthState)
                {
                    case GhostHealthState.Alive:
                        ChangeSourceRectangleAlive();
                        break;
                    case GhostHealthState.Scared:
                        ChangeSourceRectangleScared();
                        break;
                    case GhostHealthState.Dead:
                        ChangeSourceRectangleDead();
                        break;
                }
            }
        }
        #endregion

        #region Private methdos
        private void InitializeMemberVariables(Player aPlayer)
        {
            InitializeParameterMemberVariables(aPlayer);
            SetDefaultNonParameterMemberVariables();
        }

        private void InitializeParameterMemberVariables(Player aPlayer)
        {
            Player = aPlayer;
        }

        private void SetDefaultNonParameterMemberVariables()
        {
            myGhostHealthState = GhostHealthState.Alive;
        }

        private void Collision()
        {
            if (myGhostHealthState != GhostHealthState.Dead)
            {
                if (CollisionWithPlayer())
                {
                    switch (Player.PowerUp)
                    {
                        case PowerUpType.None:
                        case PowerUpType.WallUnlocker:
                            Player.GotEaten();
                            break;
                        case PowerUpType.GhostEater:
                            GotEaten();
                            break;
                    }
                }
            }
        }

        private bool CollisionWithPlayer()
        {
            if (Player.SizeHitbox.Intersects(SizeHitbox))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UpdateHealthState()
        {
            if (myGhostHealthState != GhostHealthState.Dead)
            {
                switch (Player.PowerUp)
                {
                    case PowerUpType.None:
                    case PowerUpType.WallUnlocker:
                        myGhostHealthState = GhostHealthState.Alive;
                        break;
                    case PowerUpType.GhostEater:
                        myGhostHealthState = GhostHealthState.Scared;
                        break;
                }
            }
            else
            {
                if (Position == SpawnPosition)
                {
                    myGhostHealthState = GhostHealthState.Alive;
                }
            }
        }

        private void GotEaten()
        {
            myGhostHealthState = GhostHealthState.Dead;
            SoundEffectManager.PlayGhostSound();
        }

        private void ChangeSourceRectangleDead()
        {
            myFrameYIndex = 4;
            switch (Direction)
            {
                case Direction.Up:
                    if (myFrameXIndex != 6)
                    {
                        myFrameXIndex = 6;
                    }
                    break;
                case Direction.Left:
                    if (myFrameXIndex != 5)
                    {
                        myFrameXIndex = 5;
                    }
                    break;
                case Direction.Down:
                    if (myFrameXIndex != 7)
                    {
                        myFrameXIndex = 7;
                    }
                    break;
                case Direction.Right:
                    if (myFrameXIndex != 4)
                    {
                        myFrameXIndex = 4;
                    }
                    break;
            }
        }

        private void ChangeSourceRectangleScared()
        {
            myFrameYIndex = 4;

            switch (myFrameXIndex)
            {
                case 0:
                case 1:
                case 2:
                    myFrameXIndex++;
                    break;
                case 3:
                default:
                    myFrameXIndex = 0;
                    break;
            }
        }

        private void ChangeSourceRectangleAlive()
        {
            myFrameYIndex = myDefaultFrameYIndex;
            switch (Direction)
            {
                case Direction.Up:
                    myFrameXIndex = myFrameXIndex != 4 ? 4 : 5;
                    break;
                case Direction.Left:
                    myFrameXIndex = myFrameXIndex != 2 ? 2 : 3;
                    break;
                case Direction.Down:
                    myFrameXIndex = myFrameXIndex != 6 ? 6 : 7;
                    break;
                case Direction.Right:
                    myFrameXIndex = myFrameXIndex != 0 ? 0 : 1;
                    break;
            }
        }

        private void SetDirectionTowardTile(Tile aTile)
        {
            if (aTile.Column == Column && aTile.Row == Row - 1)
            {
                Direction = Direction.Up;
            }
            else if (aTile.Column == Column - 1 && aTile.Row == Row)
            {
                Direction = Direction.Left;
            }
            else if (aTile.Column == Column + 1 && aTile.Row == Row)
            {
                Direction = Direction.Right;
            }
            else if (aTile.Column == Column && aTile.Row == Row + 1)
            {
                Direction = Direction.Down;
            }

        }


        //Steg 1: Bygg en graf så du slipper kolla bounds etc. (Får kolla sånt medan du bygger grafen). Närhetsmatris/Närhetslista --> matris
        //Steg 2: Gör sökningen så du får fram pathen. abstract -> overrides av alla spöken        abstract SearchAlgoritm... BFS etc kallas ur overrided
        //Steg 3: Fucka hela pathen. Du får pathen i termer av grafen -> översett den till en tile. Tilen är första steget i grafen dvs steget spöket ska ta.
        private Tile FindNextTile(int aColumn, int aRow) //BFS
        {
            Queue<Tile> queue = new Queue<Tile>();
            Dictionary<Tile, Tile> parents = new Dictionary<Tile, Tile>();

            Tile currentTile = myGameBoard.GetWalkableTile(Row, Column);
            queue.Enqueue(currentTile);

            while (queue.Count != 0)
            {
                Tile tile = queue.Dequeue();

                if (aColumn == tile.Column && aRow == tile.Row)
                {
                    return WalkBackwardsFromParent(parents, tile, currentTile);
                }

                AddChildren(queue, parents, tile);
            }
            return null;
        }

        private void AddChildren(Queue<Tile> aQueue, Dictionary<Tile, Tile> aParents, Tile aTile) //GetNeighbours. Returnerar en lista av legit neighbours
        {
            Tile[] children = new Tile[]
            {
                myGameBoard.GetWalkableTile(aTile.Row - 1, aTile.Column),
                myGameBoard.GetWalkableTile(aTile.Row, aTile.Column - 1),
                myGameBoard.GetWalkableTile(aTile.Row + 1, aTile.Column),
                myGameBoard.GetWalkableTile(aTile.Row, aTile.Column + 1),
            };

            foreach (Tile child in children)
            {
                if (child != null //&& aParents.ContainsKey(child) == false)
                {
                    //terutn neighbours
                    aQueue.Enqueue(child);
                    aParents.Add(child, aTile);
                }
            }
        }

        private Tile WalkBackwardsFromParent(Dictionary<Tile, Tile> aParents, Tile aTile, Tile aCurrentTile) //Plocka ut till första. Håll koll på hela pathen -> ta ut första.
        {
            while (aTile != null)
            {
                Tile parent;
                if (aParents.TryGetValue(aTile, out parent))
                {
                    if (parent == aCurrentTile)
                    {
                        return aTile;
                    }
                }
                else
                {
                    return null;
                }

                aTile = parent;
            }
            return null;
        }
        #endregion
    }
}
