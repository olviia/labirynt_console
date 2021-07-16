using System;

namespace testlabirynth
{

    class Program
    {
        public static int width = 10;
        static public int height = 12;

        public static int block_freq = 28;
        public static char cell = '#';
        public static char dog = '@';
        public static int dogX, dogY = 0;

        public static int dx, dy = 0;
        public static int finishX = 9;
        public static int finishY = 11;
        // public int newX, newY = 0;
        public static char[,] field = new char[height, width];

        public static bool reached_finish = false;


        public static void Main(string[] args)
        {

            Generate();
            Draw();

            while (!IsEndGame())
            {
                GetInput();
                Logic(); 
                Console.Clear();
                Draw();
            }
            Console.WriteLine("Congradulations!");
        }
        static void GetInput()
        {
            string inp = Console.ReadLine();
            if (inp.Length == 0)
            {
                return;
            }
            char first_symbol = Convert.ToChar(inp.Substring(0));


            if (first_symbol == 'w' || first_symbol == 'W')
            {
                dy = -1;
            }
            else if (first_symbol == 'a' || first_symbol == 'A')
            {
                dx = -1;
            }
            else if (first_symbol == 's' || first_symbol == 'S')
            {
                dy = 1;
            }
            else if (first_symbol == 'd' || first_symbol == 'D')
            {
                dx = 1;
            }

        }
        static bool IsEndGame()
        {
            return reached_finish;
        }
        static void TryToGo(int newX, int newY)
        {
            if (CanGoTo(newX, newY))
            {
                GoTo(newX, newY);
            }
        }
        static bool IsWalkable(int X, int Y)
        {
            if (field[Y, X] == cell)
            {
                return false;
            }
            else 
            {
                return true; 
            }
        }
        static bool CanGoTo(int newX, int newY)
        {
            if (newX<0 || newY<0 || newX>=width || newY >= height)
            {
                return false;
            }
            else if (!IsWalkable(newX, newY))
            {
                return false;
            }
            else 
            {
                return true; 
            }
            
        }
        static void CheckFinish()
        {
            if (dogX == finishX && dogY == finishY)
            {
                reached_finish = true;
            }
        }
        static void GoTo(int newX, int newY)
        {
            dogX = dogX + dx;
            dogY = dogY + dy;
        }

        static void Logic()
        {
            TryToGo((dogX+dx), (dogY+dy));
            CheckFinish();
        }

        static void GenerateField()
        {
            Random random_num = new Random();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int rnd = random_num.Next(0, 100);
                    if (rnd < block_freq)
                    {
                        field[i, j] = '#';
                    }
                    else
                    {
                        field[i, j] = '.';
                    }                 
                }
            }
            finishY = random_num.Next(0, 11);
            finishX = random_num.Next(0, 9);
            field[finishY, finishX] = 'O';
        }

        static void Draw()
        {
            char symbol;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i ==dogY && j == dogX)
                    {
                        symbol = dog;
                    }
                    else
                    {
                        symbol = field[i, j];
                    }
                    Console.Write(symbol);
                    
                }
                Console.WriteLine();
            }
            dx = 0;
            dy = 0;
        }

        static void PlaceDog()
        {
            Random random = new Random();
            Random rand = random;
            dogX = rand.Next(0, width - 1);
            dogY = rand.Next(0, height - 1);
        }

        static void Generate()
        {
            GenerateField();
            PlaceDog();
        }

    }
}
