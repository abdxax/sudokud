using System;

namespace Sudoku
{
    class Program
    {
        

            static char[,] LoadTable()
            {

                char[,] table = new char[9,9];

            for (int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    table[i,j] = '.';
                }
            }

                Console.WriteLine("Enter Sudoku table:");
            while (true)
            {
                Console.WriteLine("Row ");
                int row = int.Parse(Console.ReadLine());
                Console.WriteLine("Colum ");
                int col = int.Parse(Console.ReadLine());
                Console.WriteLine("Value ");
                String vals = Console.ReadLine();
                char[] val_ch = vals.PadRight(9).Substring(0, 9).ToCharArray();
                table[row-1,col-1] = val_ch[0];
                Console.WriteLine(vals + "");
                Console.WriteLine("You need to enter more : (y or n)");
                string cho = Console.ReadLine();
                if (cho == "n")
                {
                    break;
                }


            }

               /*for (int i = 0; i < 9; i++)
                {
              

                    string line = Console.ReadLine();
                    table[i] = line.PadRight(9).Substring(0, 9).ToCharArray();
                    
                   
                    for (int j = 0; j < 9; j++)
                        if (table[i][j] < '0' || table[i][j] > '9')
                            table[i][j] = '.';
                }*/

                return table;

            }

            static void PrintTable(char[,] table, int stepsCount)
            {
                Console.WriteLine();
                Console.WriteLine("Solved table after {0} steps:", stepsCount);
                for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write("{0}", table[i, j] + "");
                }
                Console.WriteLine();
            }
                     
                  
            }

            static char[] GetCandidates(char[,] table, int row, int col)
            {

                string s = "";

                for (char c = '1'; c <= '9'; c++)
                {

                    bool collision = false;

                    for (int i = 0; i < 9; i++)
                    {
                        if (table[row,i] == c ||
                            table[i,col] == c ||
                            table[(row - row % 3) + i / 3,(col - col % 3) + i % 3] == c)
                        {
                            collision = true;
                            break;
                        }
                    }

                    if (!collision)
                        s += c;

                }

                return s.ToCharArray();

            }

            static bool Solve(char[,] table, ref int stepsCount)
            {

                bool solved = false;

                int row = -1;
                int col = -1;
                char[] candidates = null;

                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++)
                        if (table[i,j] == '.')
                        {
                            char[] newCandidates = GetCandidates(table, i, j);
                            if (row < 0 || newCandidates.Length < candidates.Length)
                            {
                                row = i;
                                col = j;
                                candidates = newCandidates;
                            }
                        }

                if (row < 0)
                {
                    solved = true;
                }
                else
                {

                    for (int i = 0; i < candidates.Length; i++)
                    {
                        table[row,col] = candidates[i];
                        stepsCount++;
                        if (Solve(table, ref stepsCount))
                        {
                            solved = true;
                            break;
                        }
                        table[row,col] = '.';
                    }
                }

                return solved;

            }

            static void Main(string[] args)
            {

                while (true)
                {

                    char[,] table = LoadTable();
                    int stepsCount = 0;
                    if (Solve(table, ref stepsCount))
                        PrintTable(table, stepsCount);
                    else
                        Console.WriteLine("Could not solve this Sudoku.");

                    Console.WriteLine();
                    Console.Write("More? (y/n) ");
                    if (Console.ReadLine().ToLower() != "y")
                        break;

                }
            }
        }
    }

