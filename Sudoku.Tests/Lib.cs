using System;
using Xunit;
using Sudoku.Lib;
using System.Collections;

namespace Sudoku.Tests
{
    public class Lib
    {
        private const string sudoku = "0,0,5,3,6,0,0,8,4,0,0,0,8,0,0,0,1,3,8,0,0,0,2,1,0,0,0,3,0,7,5,9,0,1,0,0,5,1,0,0,0,0,0,3,9,0,0,6,0,3,4,7,0,5,0,0,0,6,8,0,0,0,1,4,5,0,0,0,9,0,0,0,6,3,0,0,5,2,8,0,0";

        [Theory]
        [InlineData(43)]
        [InlineData(2)]
        [InlineData(51)]
        [InlineData(80)]
        [InlineData(73)]
        public void GetCoordinateFromIndexTest(int idx)
        {
            //arrange
            SolverLoop s = new SolverLoop(sudoku);

            //act
            (int row, int col) input = (idx / (int)Math.Sqrt(s.SudokuInput.Length), idx % (int)Math.Sqrt(s.SudokuInput.Length));

            //assert
            Assert.Equal(input, s.GetCoordinateFromIndex(idx));
        }

        [Fact]
        public void GetRowFromIndexTest()
        {
            SolverLoop s = new SolverLoop(sudoku);

            string expected = "630052800";

            Assert.Equal(expected, string.Join("", s.GetRowFromIndex(78)));
        }

        [Fact]
        public void GetColumnFromIndexTest()
        {
            SolverLoop s = new SolverLoop(sudoku);

            string expected = "810030000";

            Assert.Equal(expected, string.Join("", s.GetColumFromIndex(43)));
        }

        [Theory]
        [InlineData("3,12,21,30,39,48,57,66,75", 39)]
        [InlineData("0,9,18,27,36,45,54,63,72", 54)]
        public void GetColumnIndexesFromIndexTest(string input, int idx)
        {
            SolverDict s = new SolverDict(sudoku);

            Assert.Equal(input, string.Join(',', s.GetColumnIndexesFromIndex(idx)));
        }

        [Theory]
        [InlineData("0,1,2,3,4,5,6,7,8", 5)]
        [InlineData("27,28,29,30,31,32,33,34,35", 30)]
        public void GetRowIndexesFromIndexTest(string input, int idx)
        {
            SolverDict s = new SolverDict(sudoku);

            Assert.Equal(input, string.Join(',', s.GetRowIndexesFromIndex(idx)));
        }
    }
}
