using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Lib
{
    public abstract class SolverBase
    {
        protected char[] sudoku;
        protected int sideLength;
        public SolverBase(string sInput)
        {
            string[] input = sInput.Split(',');

            if (Math.Sqrt(input.Length) % 1 != 0 || Math.Sqrt(sideLength) % 1 != 0)
                throw new ArgumentException("Invalid sudoku size");
            else
                sideLength = (int)Math.Sqrt(input.Length);

            //ew yuck
            sudoku = sInput.Split(",").Select(x => Convert.ToChar(int.Parse(x[0].ToString()))).ToArray();

            //sorts numbers into groups by their numeric value, and saves a count.
            //var res =                                                   //i got this piece of code from stackoverflow
            //        from item in sudokuInput                            //after which i translated it to the method syntax
            //        group item by item into g
            //        orderby g.Count() descending
            //        select new { Item = g.Key, Count = g.Count() };
        }

        public char[] SudokuInput { get { return sudoku; } }

        public abstract char[] Solve();
        
        public IEnumerable<int> GetIndexesInSquareFromIndex(int idx)
        {
            int squaresize = (int)Math.Sqrt(sideLength);

            var coord = GetCoordinateFromIndex(idx);

            int band = GetBand(coord.row);
            int stack = GetStack(coord.col);

            for (int r = 0; r < squaresize; r++)
            {
                for (int c = 0; c < squaresize; c++)
                {
                    int rPos = (squaresize * band) + r;
                    int cPos = (squaresize * stack) + c;

                    yield return GetIndex(rPos, cPos);
                }
            }
        }
        public IEnumerable<char> GetSquareFromIndex(int index)
        {
            var coord = GetCoordinateFromIndex(index);
            return GetSquareFromCoordinate(coord.row, coord.col);
        }
        /// <summary>
        /// Gets the square in which the given field is
        /// </summary>
        /// <param name="row">row position of the field</param>
        /// <param name="col">column position of the field</param>
        /// <returns></returns>
        public IEnumerable<char> GetSquareFromCoordinate(int row, int col) //ex row 4, col 8
        {
            var idxs = GetIndexesInSquareFromIndex(GetIndex(row, col));

            foreach (var num in idxs)
                yield return sudoku[num];
        }
        public IEnumerable<char> GetRow(int row)
        {
            for (int col = 0; col < sideLength; col++)
                yield return GetValue(row, col);
        }
        public IEnumerable<char> GetRowFromIndex(int idx)
        {
            return GetRow(idx / sideLength);
        }
        public IEnumerable<int> GetRowIndexesFromIndex(int idx)
        {
            for (int i = 0; i < sideLength; i++)
                yield return idx / sideLength * sideLength + i;
        }
        public IEnumerable<char> GetColumn(int col)
        {
            for (int row = 0; row < sideLength; row++)
                yield return GetValue(row, col);
        }
        public IEnumerable<char> GetColumFromIndex(int idx)
        {
            return GetColumn(idx % sideLength);
        }
        public IEnumerable<int> GetColumnIndexesFromIndex(int idx)
        {
            for (int i = 0; i < sideLength; i++)
                yield return (idx % sideLength)+(i*sideLength);
        }
        public (int row, int col) GetCoordinateFromIndex(int idx) { return (idx / sideLength, idx % sideLength); }
        public int GetIndex(int row, int col) { return row * sideLength + col; }
        public char GetValue(int row, int col) { return sudoku[GetIndex(row, col)]; }
        public int GetBand(int row) { return (int)(row / Math.Sqrt(sideLength)); }
        public int GetStack(int column) { return GetBand(column); }
    }
}
