using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Lib
{
    public class SolverDict : SolverBase
    {
        Dictionary<int, List<char>> sudokuHelper = new Dictionary<int, List<char>>();

        public SolverDict(string sInput) : base(sInput)
        {
            List<char> nums = new List<char>();
            for (int i = 1; i <= sideLength; i++)
            {
                nums.Add(Convert.ToChar(i));
            }

            for (int i = 0; i < sudoku.Length; i++)
            {
                char num = sudoku[i];
                if (num > Convert.ToChar(0))
                {
                    sudokuHelper.Add(i, new List<char> { num });
                }
                else
                {
                    sudokuHelper.Add(i, new List<char>(nums));
                }

            }
            /*
             I've thought of another solution, where you keep adding numbers 
             that cant be put into the field as value for the key, then the 
             remaining number that can be put in is the result
            */
        }

        public override char[] Solve()
        {
            while (sudoku.Contains(Convert.ToChar(0)))
            {
                SolveNaked();
                SolveHidden();
            }
            return sudoku;
        }

        private void SolveHidden()
        {
            for (int i = 0; i < sideLength; i++)
            {
                int squareSize = (int)Math.Sqrt(sideLength);

                int row = (i / squareSize) * squareSize;

                int col = (i % squareSize) * squareSize;

                var square = GetIndexesInSquareFromIndex(GetIndex(row, col));


            }
        }

        private void SolveNaked()
        {
            do
            {
                var temp = sudokuHelper.Where(x => x.Value.Count() == 1);

                foreach (var item in temp)
                {
                    sudoku[item.Key] = (char)item.Value.First();
                    sudokuHelper[item.Key].Remove(item.Value.First());
                }

                for (int i = 0; i < sudoku.Length; i++)
                {
                    char num = sudoku[i];
                    if (num > 0)
                    {
                        RemoveNumber(i, sudoku[i]);
                    }
                }

                Console.WriteLine(sudokuHelper.Where(x => x.Value.Count > 0).Count());


                //Console.Clear();
                //foreach (var item in res)
                //{
                //    Console.WriteLine($"{item.Key} : {item.Value[0]}");
                //}

            } while (sudokuHelper.Values.Any(x => x.Count == 1));
        }

        private void RemoveNumber(int sourceIdx, char number)
        {
            var square = GetIndexesInSquareFromIndex(sourceIdx);
            var row = GetRowIndexesFromIndex(sourceIdx);
            var column = GetColumnIndexesFromIndex(sourceIdx);

            RNumber(square, number);
            RNumber(row, number);
            RNumber(column, number);

            void RNumber(IEnumerable<int> collection, char num)
            {
                foreach (var n in collection)
                {
                    if (n != sourceIdx)
                        sudokuHelper[n].Remove(num);
                }
            }
        }
    }
}
