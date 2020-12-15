using System;
using System.Collections.Generic;
using System.Text;
using WebCasino.Service.DTO.Game;

namespace WebCasino.Service.Abstract
{
    public interface IGameService
    {
        GameSymbolDTO[,] GenerateBoard(int rows, int cols);
        GameResultsDTO GameResults(GameSymbolDTO[,] board);
    }
}
