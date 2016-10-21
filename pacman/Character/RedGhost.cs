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
        protected override List<Tile> FindPath(Graph aGraph, Tile aStart, Tile aGoal)
        {
            return null;//BreadthFirstSearch(aGraph, aStart, aGoal);
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
                    return GetPathList(tile, aStart, visisted);
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
