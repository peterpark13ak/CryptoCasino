using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebCasino.Service.Abstract;
using WebCasino.Service.Utility.TableFilterUtilities;
using WebCasino.Web.Areas.Administration.Models;

namespace WebCasino.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class TransactionsController : Controller
    {
        private readonly ITransactionService service;

        public TransactionsController(ITransactionService service)
        {
            this.service = service ?? throw new System.ArgumentNullException(nameof(service));
        }

        public IActionResult History()
        {
            return View();
        }


        public async Task<IActionResult> Details(string id)
        {
            var userTransaction = await this.service.RetrieveUserTransaction(id);

            var model = new TransactionDetailsViewModel(userTransaction);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FilterTable(DataTableModel model)
        {
            var result = await this.service.GetFiltered(model);
            var tableModel = new TransactionTableViewModel()
            {
                Transactions = result.Transactions.Select(tr => new TransactionViewModel(tr)),
                Draw = model.draw,
                RecordsTotal = result.RecordsTotal,
                RecordsFiltered = result.RecordsFiltered
            };

            return Json(tableModel);
        }

    }
}