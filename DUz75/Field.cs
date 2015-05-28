using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineReaper
{
    public class Field
    {
        readonly int rows;
        readonly int columns;
        readonly int mines;

        Place[,] field;

        public int VisibleNum
        {
            get
            {
                int visibleNum = 0;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (field[i, j].IsVisible)
                            visibleNum++;
                    }
                }
                return visibleNum;
            }
        }

        public Place this[int i, int j]
        {
            get { return field[i, j]; }
        }
        public Field(int rows, int columns, int mines)
        {
            this.rows = rows;
            this.columns = columns;
            this.mines = mines > rows * columns ? rows * columns : mines;

            if (mines < 0)
            {
                throw new ApplicationException("Negative number of mines!");
            }
            field = new Place[rows, columns];
            Init();
        }

        public void Init()
        {
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    field[i, j] = new Place(i, j);
            Generate();
        }

        private void Generate()
        {
            PlantMines();
            Evaluate();
        }

        private void PlantMines()
        {
            int mineNum = 0;
            Random rnd = new Random();
            while (mineNum < mines)
            {
                int row = rnd.Next(0, rows);
                int column = rnd.Next(0, columns);

                if (!field[row, column].IsMined)
                {
                    field[row, column].IsMined = true;
                    mineNum++;
                    continue;
                }
            }
        }

        private void Evaluate()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (!field[i, j].IsMined)
                    {
                        field[i, j].Value = GetValue(i, j);
                    }
                }
            }
        }

        private int GetValue(int row, int column)
        {
            int res = 0;
            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (int columnOffset = -1; columnOffset <= 1; columnOffset++)
                {

                    if (row + rowOffset < 0)
                        continue;
                    if (row + rowOffset >= rows)
                        continue;
                    if (column + columnOffset < 0)
                        continue;
                    if (column + columnOffset >= columns)
                        continue;
                    if (field[row + rowOffset, column + columnOffset].IsMined)
                    {
                        res++;
                    }
                }
            }
            return res;
        }

        public int RowCount
        {
            get { return rows; }
        }

        public int ColumnCount
        {
            get { return columns; }
        }

        public int Mines
        {
            get { return mines; }
        }

        public bool Step(int row, int col)
        {
            Place place = field[row, col];
            if (place.IsMined)
                return true;
            place.IsVisible = true;
            if (place.Value == 0)
                MakeVisible(row, col);
            return false;
        }

        private void MakeVisible(int row, int col)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    try
                    {
                        if (field[row + i, col + j].Value == 0
                            && (i != 0 || j != 0)
                            && !field[row + i, col + j].IsReavealing
                            )
                        {
                            field[row + i, col + j].IsReavealing = true;
                            MakeVisible(row + i, col + j);
                        }
                        field[row + i, col + j].IsVisible = true;
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }
                }
            }
        }

        public bool IsWon()
        {
            return RowCount * ColumnCount - Mines == VisibleNum ? true : false;
        }
    }
}
