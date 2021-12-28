using Konsole;
using Sjerrul.AdventOfCode2021.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Sjerrul.AdventOfCode2021.Day21
{
    public class Day21Solver : SolverBase, ISolve
    {
        private IConsole answers;
        private IConsole scores;
        private IConsole board;
        private IConsole eventwindow;

        public Day21Solver(string inputPath) : base(inputPath)
        {

            this.eventwindow = Window.OpenBox("Event", 200, 3);
            this.scores = Window.OpenBox("Scores", 200, 3);
            this.board = Window.OpenBox("Board", 200, 20);
            this.answers = Window.OpenBox("Answers", 200, 3);
        }

        public async Task Part1()
        {
            int dicerolls = 0;

            int player1Postion = 2;
            int player2Postion = 5;


            int player1Score = 0;
            int player2Score = 0;

            bool player1Turn = true;

            bool gameEnded = false;

            RenderBoard(player1Postion, player2Postion, player1Score, player2Score);
            while (!gameEnded)
            {
                int roll = Roll();
                dicerolls++;

                if (player1Turn)
                {
                    player1Postion += roll;

                    int newPosition = player1Postion % 10;
                    if (newPosition == 0)
                    {
                        player1Postion = 10;
                        player1Score += 10;
                    }
                    else
                    {
                        player1Postion = newPosition;
                        player1Score += newPosition;
                    }

                    player1Turn = false;
                    this.eventwindow.WriteLine($"Player 1 rolls {roll} and moves to space {player1Postion} for score {player1Score}");

                }

                else
                {
                    player2Postion += roll;
                    int newPosition = player2Postion % 10;
                    if (newPosition == 0)
                    {
                        player2Postion = 10;
                        player2Score += 10;
                    }
                    else
                    {
                        player2Postion = newPosition;
                        player2Score += newPosition;
                    }

                    player1Turn = true;
                    this.eventwindow.WriteLine($"Player 2 rolls {roll} and moves to space {player2Postion} for score {player2Score}");
                }

                RenderBoard(player1Postion, player2Postion, player1Score, player2Score);
                Thread.Sleep(1000);

                if (player1Score >= 1000 || player2Score >= 1000)
                {
                    gameEnded = true;
                }

            }


            int answer = 0;
            if (player1Score >= 1000)
            {
                answer = (dicerolls * 3) * player2Score;
                this.answers.WriteLine($"Player 1 won! Position x dicerolls = {answer}");
            }

            if (player2Score >= 1000)
            {
                answer = (dicerolls * 3) * player1Score;
                this.answers.WriteLine($"Player 2 won! Position x dicerolls = {answer}");
            }
        }

        int lastRoll = 1;
        private int Roll()
        {
            int roll1 = lastRoll++;
            int roll2 = lastRoll++;
            int roll3 = lastRoll++;

            return roll1 + roll2 + roll3;
        }

        private void RenderBoard(int player1Position, int player2Position, int player1Score, int player2Score)
        {
            int width = 8;
            int height = 4;

            for (int i = 0; i < 10; i++)
            {
                if (i == player1Position - 1)
                {
                    new Draw(this.board).Box(i * width, height, (i * width) + width, height + height, "P1", LineThickNess.Double);

                }
                else if (i == player2Position - 1)
                {
                    new Draw(this.board).Box(i * width, height, (i * width) + width, height + height, "P2", LineThickNess.Double);
                }
                else
                {
                    new Draw(this.board).Box(i * width, height, (i * width) + width, height + height, $"{i}");
                }
            }

            this.scores.WriteLine($"Player 1: {player1Score} - Player 2: {player2Score}");
        }

        public async Task Part2()
        {
            int dicerolls = 0;

            int player1Postion = 2;
            int player2Postion = 5;


            int player1Score = 0;
            int player2Score = 0;

            bool player1Turn = true;

            bool gameEnded = false;
            while (!gameEnded)
            {
                int roll = Roll();
                dicerolls++;

                if (player1Turn)
                {
                    player1Postion += roll;

                    int newPosition = player1Postion % 10;
                    if (newPosition == 0)
                    {
                        player1Postion = 10;
                        player1Score += 10;
                    }
                    else
                    {
                        player1Postion = newPosition;
                        player1Score += newPosition;
                    }

                    player1Turn = false;
                    this.board.WriteLine($"Player 1 rolls {roll} and moves to space {player1Postion} for score {player1Score}");
                }

                else
                {
                    player2Postion += roll;
                    int newPosition = player2Postion % 10;
                    if (newPosition == 0)
                    {
                        player2Postion = 10;
                        player2Score += 10;
                    }
                    else
                    {
                        player2Postion = newPosition;
                        player2Score += newPosition;
                    }

                    player1Turn = true;
                    this.board.WriteLine($"Player 2 rolls {roll} and moves to space {player2Postion} for score {player2Score}");

                }

                if (player1Score >= 1000 || player2Score >= 1000)
                {
                    gameEnded = true;
                }

            }

            int answer = 0;
            if (player1Score >= 1000)
            {
                answer = (dicerolls * 3) * player2Score;
            }

            if (player2Score >= 1000)
            {
                answer = (dicerolls * 3) * player1Score;
            }

            this.answers.WriteLine($"{answer}");
        }

    }
}
