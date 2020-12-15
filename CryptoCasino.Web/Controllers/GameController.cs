using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCasino.Service.Abstract;
using WebCasino.Web.Models.GameViewModels;
using WebCasino.Web.Utilities.Wrappers;

namespace WebCasino.Web.Controllers
{
    [Authorize(Roles = "Player")]
    public class GameController : Controller
    {
        private readonly IGameService gameService;
        private readonly ITransactionService transactionService;
        private readonly IUserWrapper userWrapper;

        public GameController(IGameService gameService, ITransactionService transactionService, IUserWrapper userWrapper)
        {
            this.gameService = gameService;
            this.transactionService = transactionService;
            this.userWrapper = userWrapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Small()
        {
            var board = this.gameService.GenerateBoard(4, 3);
            var dto = new GameViewModel()
            {
                Board = board,
                GameBoardRows = 4,
                GameBoardCols = 3,
                GameName = "Secrets of Cleopatra",
                ImagePrefix = "cleopatra"
            };

            return View(dto);
        }

        public IActionResult Medium()
        {
            var board = this.gameService.GenerateBoard(5, 5);
            var dto = new GameViewModel()
            {
                Board = board,
                GameBoardRows = 5,
                GameBoardCols = 5,
                GameName= "Sir Cash a Lot",
                ImagePrefix = "cash"
            };

            return View(dto);
        }

        public IActionResult Big()
        {
            var board = this.gameService.GenerateBoard(8, 5);
            var dto = new GameViewModel()
            {
                Board = board,
                GameBoardRows = 8,
                GameBoardCols = 5,
                GameName = "Gold Diggers",
                ImagePrefix = "diggers"
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Bet(GameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = this.userWrapper.GetUserId(HttpContext.User);
                await this.transactionService.AddStakeTransaction(userId, model.BetAmount, $"Stake at slot game {model.GameBoardRows} x {model.GameBoardCols}");
                var board = this.gameService.GenerateBoard(model.GameBoardRows, model.GameBoardCols);
                var gameModel = this.gameService.GameResults(board);
                if (gameModel.WinCoefficient > 0)
                {
                    await this.transactionService.AddWinTransaction(userId, gameModel.WinCoefficient * model.BetAmount, $"Win at slot game {model.GameBoardRows} x {model.GameBoardCols}");
                }
                var dto = new GameViewModel()
                {
                    Board = board,
                    WinCoef = gameModel.WinCoefficient,
                    BetAmount = model.BetAmount,
                    WinningRows = gameModel.WinningRows
                };

                return Json(dto);

            }

            TempData["Invalid bet"] = "We couldn't place your bet";
            return RedirectToAction("index", "game");
        }

    }
}