using System;
using System.Collections.Generic;
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

        protected override List<Tile> FindPath(Graph aGraph, Tile aStart, Tile aGoal)
        {
            return BreadthFirstSearch(aGraph, aStart, aGoal);
        }
        #endregion

        #region Private methods
        private List<Tile> BreadthFirstSearch(Graph aGraph, Tile aStart, Tile aGoal)
        {
            Queue<Tile> queue = new Queue<Tile>();
            Dictionary<Tile, Tile> visisted = new Dictionary<Tile, Tile>(); //value is the tile from which we reached key.
            queue.Enqueue(aStart);

            while (queue.Count > 0)
            {
                Tile tile = queue.Dequeue();

                if (tile == aGoal)
                {
                    List<Tile> path = GetPathList(tile, aStart, visisted);
                    return path;
                }

                foreach (Tile neighbour in aGraph.GetNeighbours(tile))
                {
                    if (visisted.ContainsKey(neighbour) == false)
                    {
                        queue.Enqueue(neighbour);
                        visisted.Add(neighbour, tile);
                    }
                }
            }

            return null;
        }
        #endregion
    }
}
