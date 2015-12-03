using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya
{
    class House
    {
        public int nut = 4;
        public int owner;
        public House(int owner)
        {
            this.owner = owner;
        }
    }
    class Player {
        public bool isAI = false;
        public int point = 0;
        public Player(bool isAI)
        {
            this.isAI = isAI;
        }
        public int run(House[] circles) {
            if (!isAI) {
                int ch = 0;
                while (ch > circles.Length / 2 || ch < 1) {
                    Console.WriteLine("Select a number between 1 and 6"); 
                    ch = int.Parse(Console.ReadLine());
                }
                return ch-1;
            }
            int choice = 6;
            int maxpoint = 0;
            for (int i = 6; i < 12; i++)
            {
                int[] nutTemp = new int[12];
                for (int j = 0; j < circles.Length; j++)
                {
                    nutTemp[j] = (int)circles[j].nut;
                }
                int tempPoint = 0;
                if (nutTemp[i] != 0)
                {
                    //TODO: AI kiválasztás
                    int hIndex = i;
                    int n = nutTemp[hIndex];
                    nutTemp[hIndex] = 0;
                    while (n > 0)
                    {
                        hIndex++;
                        if (hIndex == nutTemp.Length)
                        {
                            hIndex = 0;
                        }
                        if (hIndex != i)
                        {
                            n--;
                            nutTemp[hIndex]++;
                        }
                    }
                    while (circles[hIndex].owner == 1 && (nutTemp[hIndex] == 2 || nutTemp[hIndex] == 3))
                    {
                        tempPoint += nutTemp[hIndex];
                        hIndex--;
                        if (hIndex == -1)
                        {
                            hIndex = nutTemp.Length - 1;
                        }
                    }
                }
                if (tempPoint >= maxpoint)
                {
                    choice = i;
                    maxpoint = tempPoint;
                }
                while (circles[choice].nut==0) {
                    choice--;
                }
            }
            return choice;
        }
    }
    class Program
    {
        static public House[] circle = new House[12];
        static Player player1 = new Player(false); // Real
        static Player player2 = new Player(true); // AI
        static bool turn = true;
        static bool checkState() {
            if (player1.point == 24 && player2.point==24)
            {
                Console.WriteLine("Drawn");
                return false;
            }
            if (player1.point > 24)
            {
                Console.WriteLine("Player WIN!");
                return false;
            }
            if (player2.point > 24)
            {
                Console.WriteLine("Computer WIN!");
                return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            for (int i = 0; i < circle.Length; i++)
            {
                if (i < circle.Length / 2)
                {
                    circle[i] = new House(1);
                }
                else {
                    circle[i] = new House(2);
                }
            }
            while (checkState())
            {
                draw(turn);
                int select = 0;
                if (turn)
                {
                    select = player1.run(circle);
                    while (circle[select].nut==0) {
                        Console.WriteLine("Thats not a valid choice");
                        select = player1.run(circle);
                    }
                }
                else {
                    select = player2.run(circle);
                }
                Move(select,turn);
                if (!turn) {
                    Console.WriteLine("AI step:"+select);
                    Console.ReadKey();
                }
                draw(turn);
                turn = !turn;
            }
            
        }
        static void Move(int hIndex,bool player) {
            int starterHouse = hIndex;
            int n = circle[hIndex].nut;
            circle[hIndex].nut = 0;
            while (n > 0) {
                hIndex++;
                if (hIndex == circle.Length) {
                    hIndex = 0;
                }
                if (hIndex!=starterHouse)
                {
                    n--;
                    circle[hIndex].nut++;
                }
            }
            int enemy=1;
            if (player) {
                enemy = 2;
            }
            while (circle[hIndex].owner == enemy && (circle[hIndex].nut == 2 || circle[hIndex].nut == 3)) {
                if (player)
                {
                    player1.point += circle[hIndex].nut;
                }
                else {
                    player2.point += circle[hIndex].nut;
                }
                circle[hIndex].nut = 0;
                hIndex--;
                if (hIndex == -1) {
                    hIndex = circle.Length - 1;
                }
            }
        }
        static void draw(bool turn) {
            Console.Clear();
            ConsoleColor playerColor = ConsoleColor.Yellow;
            ConsoleColor AIColor = ConsoleColor.Blue;
            Console.ForegroundColor = playerColor;
            Console.Write("PLAYER:"+player1.point+"\t");
            Console.ForegroundColor = AIColor;
            Console.WriteLine("AI:" + player2.point);
            for (int i = 0; i < circle.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        Console.SetCursorPosition(0, 9);
                        break;
                    case 1:
                        Console.SetCursorPosition(1*2, 12);
                        break;
                    case 2:
                        Console.SetCursorPosition(4*2, 13);
                        break;
                    case 3:
                        Console.SetCursorPosition(6*2, 13);
                        break;
                    case 4:
                        Console.SetCursorPosition(9*2,12);
                        break;
                    case 5:
                        Console.SetCursorPosition(10*2, 9);
                        break;
                    case 6:
                        Console.SetCursorPosition(10*2, 7);
                        break;
                    case 7:
                        Console.SetCursorPosition(9*2,4);
                        break;
                    case 8:
                        Console.SetCursorPosition(6*2, 3);
                        break;
                    case 9:
                        Console.SetCursorPosition(4*2, 3);
                        break;
                    case 10:
                        Console.SetCursorPosition(1*2, 4);
                        break;
                    case 11:
                        Console.SetCursorPosition(0, 7);
                        break;
                    default:
                        break;
                }
                if (i < 6)
                {
                    Console.ForegroundColor = playerColor;
                }
                else {
                    Console.ForegroundColor = AIColor;
                }
                Console.Write(circle[i].nut);
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, 15);
            Console.WriteLine("1-2-----3---4-----5-6\n".Replace('-',' '));

        }
        
    }
}
