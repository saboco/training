using System.Collections.Generic;
using System.Linq;
using Training.DataStructures;

namespace Training.HackerRank.DataStructures
{
    public class ConnectedCellsInAGrid
    {
        private readonly Graph<string> _regions = new Graph<string>();
        private readonly HashSet<string> _edgesAdded = new HashSet<string>();
        private readonly Grid _grid;

        public ConnectedCellsInAGrid(int[][] grid)
        {
            _grid = new Grid(grid);
            MapCellsToGraph();
        }

        private void MapCellsToGraph()
        {
            foreach (var kvCell in _grid.Cells.Where(c => c.Value.IsFilled))
            {
                _regions.AddNode(kvCell.Key);
                foreach (var adjacentFilledCell in _grid.GetAdjacentFilledCells(kvCell.Value))
                {
                    if (_edgesAdded.Contains(GetEdgeId(kvCell.Value, adjacentFilledCell))) continue;

                    _regions.AddEdge(kvCell.Key, adjacentFilledCell.Id);
                    _edgesAdded.Add(GetEdgeId(kvCell.Value, adjacentFilledCell));
                }
            }
        }

        private string GetEdgeId(Cell source, Cell destination)
        {
            return source.Id + "<|>" + destination.Id;
        }

        public int GetGreatesRegion()
        {
            return _regions.GetGreatesRegion();
        }

        private class Cell
        {
            public static readonly NullCell Null = new NullCell();

            public class NullCell : Cell
            {
                public NullCell() : this(-1, -1, false)
                {
                }

                private NullCell(int i, int j, bool isFilled) : base(i, j, isFilled)
                {
                }
            }

            public class Coordinates
            {
                public int X { get; }
                public int Y { get; }

                public Coordinates(int x, int y)
                {
                    X = x;
                    Y = y;
                }
            }

            public Coordinates Coords { get; }
            public string Id { get; }
            public bool IsFilled { get; }

            public Cell(int i, int j, bool isFilled)
            {
                Coords = new Coordinates(i, j);
                Id = GenerateId(Coords);
                IsFilled = isFilled;
            }

            public static string GenerateId(int x, int y)
            {
                return x + "¤" + y;
            }

            private static string GenerateId(Coordinates coordinates)
            {
                return GenerateId(coordinates.X, coordinates.Y);
            }
        }

        private class Grid
        {
            private readonly int[][] _grid;
            public Dictionary<string, Cell> Cells { get; }

            public Grid(int[][] grid)
            {
                _grid = grid;
                Cells = new Dictionary<string, Cell>();
                AddCells();
            }

            public IEnumerable<Cell> GetAdjacentFilledCells(Cell cell)
            {
                var upperCell = GetUpperCell(cell);
                var upperLeftCell = GetUpperLeftCell(cell);
                var upperRightCell = GetUpperRightCell(cell);

                var leftCell = GetLeftCell(cell);
                var rightCell = GetRightCell(cell);

                var downCell = GetDownCell(cell);
                var downLeftCell = GetDownLeftCell(cell);
                var downRightCell = GetDownRightCell(cell);

                var filledCells = new List<Cell>();

                if (upperCell.IsFilled) filledCells.Add(upperCell);
                if (upperLeftCell.IsFilled) filledCells.Add(upperLeftCell);
                if (upperRightCell.IsFilled) filledCells.Add(upperRightCell);
                if (leftCell.IsFilled) filledCells.Add(leftCell);
                if (rightCell.IsFilled) filledCells.Add(rightCell);
                if (downCell.IsFilled) filledCells.Add(downCell);
                if (downLeftCell.IsFilled) filledCells.Add(downLeftCell);
                if (downRightCell.IsFilled) filledCells.Add(downRightCell);

                return filledCells;
            }

            private void AddCells()
            {
                for (var i = 0; i < _grid.Length; i++)
                {
                    for (var j = 0; j < _grid[i].Length; j++)
                    {
                        var cell = new Cell(i, j, CellIsFilled(i, j));
                        Cells.Add(cell.Id, cell);
                    }
                }
            }

            private bool CellIsFilled(int i, int j)
            {
                return _grid[i][j] == 1;
            }

            private Cell GetUpperCell(Cell cell)
            {
                return GetCell(cell.Coords.X - 1, cell.Coords.Y);
            }

            private Cell GetUpperRightCell(Cell cell)
            {
                return GetCell(cell.Coords.X - 1, cell.Coords.Y - 1);
            }

            private Cell GetUpperLeftCell(Cell cell)
            {
                return GetCell(cell.Coords.X - 0, cell.Coords.Y + 1);
            }

            private Cell GetRightCell(Cell cell)
            {
                return GetCell(cell.Coords.X, cell.Coords.Y - 1);
            }

            private Cell GetLeftCell(Cell cell)
            {
                return GetCell(cell.Coords.X, cell.Coords.Y + 1);
            }

            private Cell GetDownCell(Cell cell)
            {
                return GetCell(cell.Coords.X + 1, cell.Coords.Y);
            }

            private Cell GetDownLeftCell(Cell cell)
            {
                return GetCell(cell.Coords.X + 1, cell.Coords.Y - 1);
            }

            private Cell GetDownRightCell(Cell cell)
            {
                return GetCell(cell.Coords.X + 1, cell.Coords.Y + 1);
            }

            private Cell GetCell(int i, int j)
            {
                var id = Cell.GenerateId(i, j);
                return Cells.ContainsKey(id) ? Cells[id] : Cell.Null;
            }
        }
    }
}