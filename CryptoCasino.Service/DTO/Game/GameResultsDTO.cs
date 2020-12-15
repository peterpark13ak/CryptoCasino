using System;
using System.Collections.Generic;
using System.Text;

namespace WebCasino.Service.DTO.Game
{
    public class GameResultsDTO
    {
        public GameSymbolDTO[,] GameBoard { get; set; }
        public double WinCoefficient { get; set; }

        public IList<int> WinningRows { get; set; }
    }
}
