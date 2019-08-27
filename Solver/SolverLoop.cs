using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Lib
{
    public class SolverLoop : SolverBase
    {
        public SolverLoop(string sInput) : base(sInput)
        {
        }

        public override char[] Solve()
        {
            int sidelength = (int)Math.Sqrt(sudoku.Length);
        int squarelength = (int)Math.Sqrt(sidelength);
            /*
             *Start loop
             *  Find least occuring number
             *  Find squares where number is missing
             *      Check rows and columns
             *      If check passes, place number
             *redo loop
             */

            do
            {
                var res = sudoku
                    .Where(x => Char.GetNumericValue(x) > 0)
                    .GroupBy(x => x)
                    .Select(x => new { Item = x.Key, Count = x.Count() })
                    .Where(x => x.Count < 8)
                    .OrderByDescending(x => x.Count).ToArray();

                for (int i = 0; i<res.Length; i++) //this loop procedes whenever a number can be in two spots
                {
                    //iterate through squares
                    for (int j = 0; j<squarelength; j++)
                    {
                        for (int l = 0; l<squarelength; l++)
                        {
                            int squareBaseRow = j * squarelength;
        int squareBaseCol = l * squarelength;

        var selectedSquare = GetSquareFromCoordinate(j * squarelength, l * squarelength).ToList();

                            if (!selectedSquare.Contains(res[i].Item))
                            {
                                for (int m = 0; m<squarelength; m++)
                                {
                                    for (int n = 0; n<squarelength; n++)
                                    {
                                        if (selectedSquare.ElementAt(n* m) == 0)
                                        {
                                            int actRow = squareBaseRow + m;
        int actCol = squareBaseCol + n;
    }
}
                                }
                            }
                        }
                    }
                }
            } while (sudoku.Where(x => x == 0).Count() > 0);

            return null;
        }
    }
}
