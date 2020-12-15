using System;
using System.ComponentModel.DataAnnotations;
using WebCasino.Entities;

namespace WebCasino.Web.Areas.Administration.Models
{
	public class TransactionViewModel
	{
        public TransactionViewModel()
        {
        }

        public TransactionViewModel(Transaction transaction)
        {
            this.Alias = transaction.User.Alias;
            this.CreatedOn = ((DateTime)transaction.CreatedOn).ToShortDateString();
            this.NormalisedAmount = Math.Floor(transaction.NormalisedAmount * 100) / 100;
            this.OriginalAmount = Math.Floor(transaction.OriginalAmount * 100) / 100;
            this.Description = transaction.Description;
            this.TransactionTypeName = transaction.TransactionType.Name;
            this.Email = transaction.User.Email;
            this.UserId = transaction.User.Id;
            this.Id = transaction.Id;
            this.Action = this.SetButtonHtml(transaction.User.Id, transaction.Id);
        }

        public string Id { get; set; }

        public string CreatedOn { get; set; }

        public string TransactionTypeName { get; set; }

        [Range(0, double.MaxValue)]
        public double NormalisedAmount { get; set; }

        [Range(10, 100)]
        public string Description { get; set; }

        public double OriginalAmount { get; set; }

        public string Email { get; set; }

        public string Alias { get; set; }

        public string UserId { get; set; }

        public string Action { get; set; }

        private string SetButtonHtml(string userId, string transId)
        {
            return $"<a href=\"/Administration/UserAdministration/UserAccountSettings/{userId}\" class=\"btn btn-info btn-link btn-icon btn-sm btn-tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" data-container=\"body\" data-animation=\"true\" data-original-title=\"Go to User Settings\"><i class=\"tim-icons icon-single-02\"></i></a><a href=\"/Administration/Transactions/Details/{transId}\" class=\"btn btn-success btn-link btn-icon btn-sm\" data-toggle=\"tooltip\" data-placement=\"top\" data-container=\"body\" data-animation=\"true\" data-original-title=\"Go to Transaction Details\"><i class=\"tim-icons icon-settings\"></i></a>";
        }
    }
}