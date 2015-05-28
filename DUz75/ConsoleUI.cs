using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MineReaper
{
    class ConsoleUI
    {
        Field field;
        public ConsoleUI()
        {
            field = new Field(9, 9, 1);
            while (true)
            {
                Console.Clear();
                UpdateUI();
                HandleInput();
            }
        }

        private void UpdateUI()
        {
            ShowBattlefield();
            Console.WriteLine("New game: new game");
            Console.WriteLine("Exit game: x"); 
            Console.WriteLine("Step: step xy  //x=row, y=column, e.g. step b4");
        }

        private void HandleInput()
        {
            string input = Console.ReadLine();
            Regex rx = new Regex(@"^((?<exit>x)|(?<newgame>new game)|(?<step>step (?<row>[A-"
                + Convert.ToChar('A' + field.RowCount - 1)
                + "])(?<col>[0-" + (field.ColumnCount - 1) + "])))$",
                RegexOptions.IgnoreCase);
            Match match = rx.Match(input);
            if (match.Groups["exit"].Value != "")
                Environment.Exit(0);
            else if (match.Groups["newgame"].Value != "")
                field = new Field(field.RowCount,
                    field.ColumnCount,
                    field.Mines);
            else if (match.Groups["step"].Value != "")
            {
                int row = Convert.ToInt32(match.Groups["row"].Value[0] - 'a');
                int col = Convert.ToInt32(match.Groups["col"].Value[0] - '0');
                bool iDied = field.Step(row, col);
                bool isWon = field.IsWon();
                if (iDied)
                {
                    Console.Clear();
                    ShowBattlefield();
                    Console.WriteLine("You stepped on a mine\nDo you want to start a new game? y/n");
                    EndOfTheGame();
                }
                else if (isWon)
                {
                    Console.Clear();
                    ShowBattlefield();
                    Console.WriteLine("You won!\nDo you want to start a new game? y/n");
                    EndOfTheGame();
                }

            }
        }

        public void ShowBattlefield()
        {
            Console.Write("  ");
            for (int i = 0; i < field.ColumnCount; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < field.RowCount; i++)
            {
                Console.Write(Convert.ToChar('A' + i) + " ");
                for (int j = 0; j < field.ColumnCount; j++)
                {
                    if (field[i, j].IsVisible)
                    {
                        if (field[i, j].IsMined)
                        {
                            Console.Write("X ");
                        }
                        Console.Write(field[i, j].Value + " ");
                    }
                    else
                        Console.Write("# ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine();
        }

        private void EndOfTheGame()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input.Equals("y"))
                {
                    field.Init();
                    break;
                }
                else if (input.Equals("n"))
                    Environment.Exit(0);
                else Console.WriteLine("Wrong input!");
            }
        }

        internal void GetSize()
        {
                
        }
    }
}
