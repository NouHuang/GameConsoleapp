using System;

class Program
{
    static int[] playerA = { 1, 1 }; // Human
    static int[] playerB = { 1, 1 }; // AI
    static bool isPlayerATurn = false; // 👈 AI starts first now

    static void Main()
    {
        Console.WriteLine(" Player A vs Player B (AI)");
        Console.WriteLine("Goal: Avoid any of your numbers reaching 10 or more!");

        while (true)
        {
            ShowStatus();

            if (isPlayerATurn)
            {
                Console.WriteLine("\nYour Turn - Player A:");
                PlayerMove(playerA, playerB);

                if (IsLose(playerA))
                {
                    Console.WriteLine(" Player A Lose !  Player B (AI) Victory!");
                    break;
                }
            }
            else
            {
                Console.WriteLine("\nAI Turn - Player B:");
                AIMove(playerB, playerA);

                if (IsLose(playerB))
                {
                    Console.WriteLine(" Player B (AI) Lose!  Player A Victory!");
                    break;
                }
            }

            isPlayerATurn = !isPlayerATurn;
        }

        Console.WriteLine("\n Game Over ");
        Console.ReadKey();
    }

    static void ShowStatus()
    {
        Console.WriteLine($"\nPlayer A: [{playerA[0]}] [{playerA[1]}]");
        Console.WriteLine($"Player B: [{playerB[0]}] [{playerB[1]}]");
    }

    static void PlayerMove(int[] player, int[] opponent)
    {
        int myIndex = GetChoice("Choose your number to add to (Left = 0 & Right = 1): ");
        int opIndex = GetChoice("Choose opponent number to add from (Left = 0 & Right = 1): ");

        int sum = player[myIndex] + opponent[opIndex];
        Console.WriteLine($"You chose: {player[myIndex]} + {opponent[opIndex]} = {sum}");
        player[myIndex] = sum;
    }

    static void AIMove(int[] ai, int[] opponent)
    {
        int bestMy = -1;
        int bestOp = -1;

        for (int my = 0; my < 2; my++)
        {
            for (int op = 0; op < 2; op++)
            {
                int sum = ai[my] + opponent[op];
                if (sum >= 10) continue;

                int[] tempAI = (int[])ai.Clone();
                tempAI[my] = sum;

                bool playerHasSafeMove = false;
                for (int pMy = 0; pMy < 2; pMy++)
                {
                    for (int pOp = 0; pOp < 2; pOp++)
                    {
                        int futureSum = opponent[pMy] + tempAI[pOp];
                        if (futureSum < 10)
                        {
                            playerHasSafeMove = true;
                        }
                    }
                }

                if (!playerHasSafeMove)
                {
                    bestMy = my;
                    bestOp = op;
                    goto MakeMove;
                }

                if (bestMy == -1)
                {
                    bestMy = my;
                    bestOp = op;
                }
            }
        }

    MakeMove:
        if (bestMy != -1)
        {
            int sum = ai[bestMy] + opponent[bestOp];
            Console.WriteLine($"AI chose: {ai[bestMy]} + {opponent[bestOp]} = {sum}");
            ai[bestMy] = sum;
        }
        else
        {
            ai[0] += opponent[0];
            Console.WriteLine($"AI forced to lose: {ai[0]} (added opponent[0])");
        }
    }

    static int GetChoice(string message)
    {
        int choice;
        do
        {
            Console.Write(message);
        }
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 1);
        return choice;
    }

    static bool IsLose(int[] player)
    {
        return player[0] > 10 || player[1] > 10;
    }
}
