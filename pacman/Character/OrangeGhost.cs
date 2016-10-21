using System;
using System.Collections.Generic;
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
        protected override List<Tile> FindPath(Graph aGraph, Tile aStart, Tile aGoal)
        {
            return null; // DepthFirstSearch(aGraph, aStart, aGoal);
        }
        #endregion

        #region Private methods
        private List<Tile> DepthFirstSearch(Graph aGraph, Tile aStart, Tile aGoal) //Inte lämplig för endamålet (SKRIV I RAPORT).
        {
            Dictionary<Tile, Tile> visisted = new Dictionary<Tile, Tile>(); //value is the tile from which we reached key.
            Stack<Tile> stack = new Stack<Tile>();
            stack.Push(aStart);

            while (stack.Count > 0)
            {
                Tile tile = stack.Pop();

                if (tile == aGoal)
                {
                    return GetPathList(tile, aStart, visisted);
                }

                foreach (Tile neighbour in aGraph.GetNeighbours(tile))
                {
                    if (visisted.ContainsKey(neighbour) == false)
                    {
                        stack.Push(neighbour);
                        visisted.Add(neighbour, tile);
                    }
                }
            }

            return null;
        }
        #endregion
    }
}
