using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    class Graph
    {
        GameBoard myGameBoard;

        public Graph(GameBoard aGameBoard)
        {
            myGameBoard = aGameBoard;
        }
        
        public List<Tile> GetNeighbours(Tile aTile) //GetNeighbours. Returnerar en lista av legit neighbours
        {
            List<Tile> neighbours = new List<Tile>();
            
            neighbours.Add(myGameBoard.GetWalkableTile(aTile.Row - 1, aTile.Column));
            neighbours.Add(myGameBoard.GetWalkableTile(aTile.Row, aTile.Column - 1));
            neighbours.Add(myGameBoard.GetWalkableTile(aTile.Row + 1, aTile.Column));
            neighbours.Add(myGameBoard.GetWalkableTile(aTile.Row, aTile.Column + 1));

            neighbours = neighbours.FindAll(n => n != null);

            return neighbours;
        }
    }
}
