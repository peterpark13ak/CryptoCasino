using System;
using System.Collections.Generic;
using System.Linq;
using WebCasino.Service.Abstract;
using WebCasino.Service.DTO.Game;
using WebCasino.Service.Utility.RandomGeneration;

namespace WebCasino.Service
{
    public class GameService : IGameService
    {
        private readonly IRandomGenerator randomNumberGenerator;

        public GameService(IRandomGenerator randomNumberGenerator)
        {
            this.randomNumberGenerator = randomNumberGenerator;
        }

        public GameSymbolDTO[,] GenerateBoard(int rows, int cols)
        {
            if(rows < 1 || cols < 1)
            {
                throw new ArgumentOutOfRangeException("", "Matrix dimensions cannot be negative");
            }

            var numbers = randomNumberGenerator.GenerateNumber(0, 101, rows * cols).ToList();
            var matrix = new GameSymbolDTO[rows, cols];
            var counter = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int f = 0; f < cols; f++)
                {
                    matrix[i, f] = new GameSymbolDTO(numbers[counter], 0, 100);
                    counter++;
                }
            }

            return matrix;
        }

        public GameResultsDTO GameResults(GameSymbolDTO[,] board)
        {
            var winningRows = new List<int>();
            double winCoeff = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                double rowWin = 0;
                var defChar = board[i, 0];
                for (int f = 0; f < board.GetLength(1); f++)
                {
                    if(defChar.SymbolName == "wild" && board[i,f].SymbolName != "wild")
                    {
                        defChar = board[i, f];
            
                    }
                    else if(defChar.SymbolName != "wild" && board[i,f].SymbolName != defChar.SymbolName && board[i,f].SymbolName != "wild")
                    {
                        rowWin = 0;
                        break;
                    }
                        rowWin += board[i, f].Coefficient;
                    
                }
                if(rowWin > 0)
                {
                    winningRows.Add(i);
                }
                winCoeff += rowWin;

            }
            return new GameResultsDTO()
            {
                GameBoard = board,
                WinCoefficient = Math.Round(winCoeff, 2),
                WinningRows = winningRows
            };
        }
    }
}
