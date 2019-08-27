using System;
using Sudoku.Lib;
namespace Sudoku.Cons
{
    class Program
    {
        static void Main(string[] args)
        {
            //input: https://www.websudoku.com/?level=1&set_id=7198889832
            string sudokuEasy = "0,0,5,3,6,0,0,8,4,0,0,0,8,0,0,0,1,3,8,0,0,0,2,1,0,0,0,3,0,7,5,9,0,1,0,0,5,1,0,0,0,0,0,3,9,0,0,6,0,3,4,7,0,5,0,0,0,6,8,0,0,0,1,4,5,0,0,0,9,0,0,0,6,3,0,0,5,2,8,0,0";

            //input: https://www.websudoku.com/?level=2&set_id=63590627
            string sudokuMedium = "9,3,8,0,0,6,0,0,0,7,2,0,0,0,3,0,9,0,0,1,0,0,0,7,0,0,2,3,0,0,4,0,0,7,0,1,0,0,0,0,0,0,0,0,0,4,0,1,0,0,9,0,0,5,1,0,0,8,0,0,0,4,0,0,9,0,6,0,0,0,1,8,0,0,0,2,0,0,6,7,9";

            //input: https://www.websudoku.com/?level=3&set_id=2228127832
            string sudokuHard = "0,3,0,1,6,0,0,0,0,0,1,5,8,9,0,0,0,0,0,0,0,0,0,5,0,0,3,0,4,0,0,0,2,0,0,9,2,0,6,0,0,0,4,0,7,3,0,0,6,0,0,0,8,0,6,0,0,7,0,0,0,0,0,0,0,0,0,5,9,2,4,0,0,0,0,0,4,6,0,7,0";


            SolverDict s = new SolverDict(sudokuEasy);

            var solved = s.Solve();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int val = solved[i * 9 + j];
                    Console.Write(val.ToString().PadRight(2));

                    if ((j + 1) % 3 == 0)
                        Console.Write("  ");
                }
                if ((i + 1) % 3 == 0)
                    Console.WriteLine();

                Console.WriteLine();
            }


            Console.ReadKey();
        }
    }
}
