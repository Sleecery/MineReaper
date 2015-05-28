using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MineReaper
{
    class MinesweeperGame
    {
        ConsoleUI consoleUI;

        public MinesweeperGame()
        {
            consoleUI = new ConsoleUI();
        }

        public void save()
        {
            Field field = consoleUI.Field;
            using (Stream stream = new FileStream(@"C:\minesweeper.dat", FileMode.OpenOrCreate)) 
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, field);
            }
        }
        public void load()
        {
            using (Stream stream = new FileStream(@"C:\minesweeper.dat", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                Field field = (Field)bf.Deserialize(stream);
                consoleUI.Field = field;
            }
        }
    }
}
