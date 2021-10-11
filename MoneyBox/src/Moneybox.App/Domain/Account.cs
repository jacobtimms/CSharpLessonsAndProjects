using System;
using Moneybox.App.Domain.Services;

namespace Moneybox.App
{
    public class Account
    {
        public const decimal PayInLimit = 4000m;

        public Guid Id { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }

        public Account DeductFromBalance(Account account, decimal amount, INotificationService notificationService)
        {
            var fromBalance = account.Balance - amount;
            if (fromBalance < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }

            if (fromBalance < 500m)
            {
                notificationService.NotifyFundsLow(account.User.Email);
            }

            account.Balance = account.Balance - amount;
            account.Withdrawn = account.Withdrawn - amount;

            return account;
        }

        public Account AddToBalance(Account account, decimal amount, INotificationService notificationService)
        {
            var paidIn = account.PaidIn + amount;
            if (paidIn > Account.PayInLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            if (Account.PayInLimit - paidIn < 500m)
            {
                notificationService.NotifyApproachingPayInLimit(account.User.Email);
            }

            account.Balance = account.Balance + amount;
            account.PaidIn = account.PaidIn + amount;

            return account;
        }

    }
}
