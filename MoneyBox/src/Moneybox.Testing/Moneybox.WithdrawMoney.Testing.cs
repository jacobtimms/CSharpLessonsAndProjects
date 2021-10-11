using Moneybox.App;
using Moq;
using System;
using Xunit;
using Xunit.Abstractions;
using Moneybox.App.DataAccess;
using Moneybox.App.Features;
using Moneybox.App.Domain.Services;

namespace Moneybox.Testing
{
    public class MoneyboxWithdrawTests
    {
        private readonly WithdrawMoney _sut;
        private readonly Mock<IAccountRepository> _accountRepoMock = new();
        private readonly Mock<INotificationService> _notificationMock = new();

        private readonly ITestOutputHelper output;


        public MoneyboxWithdrawTests(ITestOutputHelper output)
        {
            _sut = new WithdrawMoney(_accountRepoMock.Object, _notificationMock.Object);
            this.output = output;
        }


        [Fact]
        public void TestWithdrawMethod_CheckMockedProperties()
        {
            // ARRANGE
            var accountId = Guid.NewGuid();

            //From Customer Account
            var customerName = "jacob";
            var customerEmail = "jacob@mail";
            decimal balance = 5m;
            decimal paidIn = 0m;
            decimal withdrawn = 0m;

            var account = new Account
            {
                Id = accountId,
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Name = customerName,
                    Email = customerEmail
                },
                Balance = balance,
                PaidIn = paidIn,
                Withdrawn = withdrawn
            };

            //Setup mocked properites
            _accountRepoMock.Setup(x => x.GetAccountById(accountId))
                .Returns(account);

            // ASSERT  
            Assert.Equal(customerName, account.User.Name);
            Assert.Equal(customerEmail, account.User.Email);
            Assert.Equal(balance, account.Balance);
            Assert.Equal(paidIn, account.PaidIn);
            Assert.Equal(withdrawn, account.Withdrawn);
        }

        [Fact]
        public void TestWithdrawMethod_CheckBalanceAndWithdrawn()
        {
            // ARRANGE
            var accountId = Guid.NewGuid();
            decimal ammount = 5m;

            //From Customer Account
            var customerName = "jacob";
            var customerEmail = "jacob@mail";
            decimal balance = 5m;
            decimal paidIn = 0m;
            decimal withdrawn = 0m;

            var account = new Account
            {
                Id = accountId,
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Name = customerName,
                    Email = customerEmail
                },
                Balance = balance,
                PaidIn = paidIn,
                Withdrawn = withdrawn
            };

            //Setup mocked properites
            _accountRepoMock.Setup(x => x.GetAccountById(accountId))
                .Returns(account);
            _accountRepoMock.Setup(x => x.Update(It.IsAny<Account>()))
                .Callback((Account updatedAccount) =>
                {
                    if (account.Id == accountId)
                    {
                        account = updatedAccount;
                    }
                    else { throw new ArgumentException("Account GUID does not match mocked Id's"); }
                }
            );

            // ACT
            _sut.Execute(accountId, ammount);

            // ASSERT  
            Assert.Equal(0m, account.Balance);
            Assert.Equal(-5m, account.Withdrawn);
        }

        [Fact]
        public void TestWithdrawMethod_ThrowsInsufficientFundsException()
        {
            // ARRANGE
            var accountId = Guid.NewGuid();

            //From Customer Account
            var customerName = "jacob";
            var customerEmail = "jacob@mail";
            decimal balance = 5m;
            decimal paidIn = 0m;
            decimal withdrawn = 0m;

            var account = new Account
            {
                Id = accountId,
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Name = customerName,
                    Email = customerEmail
                },
                Balance = balance,
                PaidIn = paidIn,
                Withdrawn = withdrawn
            };

            //Setup mocked properites
            _accountRepoMock.Setup(x => x.GetAccountById(accountId))
                .Returns(account);

            // ASSERT  
            Assert.Throws<InvalidOperationException>(() => _sut.Execute(accountId, 10m));
            Assert.Throws<InvalidOperationException>(() => _sut.Execute(accountId, 100m));
            Assert.Throws<InvalidOperationException>(() => _sut.Execute(accountId, 123.5m));
        }

        [Fact]
        public void TestWithdrawMethod_ThrowsPayInLimitReachedException()
        {
            // ARRANGE
            var accountId = Guid.NewGuid();

            //From Customer Account
            var customerName = "jacob";
            var customerEmail = "jacob@mail";
            decimal balance = 5m;
            decimal paidIn = 0m;
            decimal withdrawn = 0m;

            var account = new Account
            {
                Id = accountId,
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Name = customerName,
                    Email = customerEmail
                },
                Balance = balance,
                PaidIn = paidIn,
                Withdrawn = withdrawn
            };

            //Setup mocked properites
            _accountRepoMock.Setup(x => x.GetAccountById(accountId))
                .Returns(account);
            _accountRepoMock.Setup(x => x.Update(It.IsAny<Account>()));

            // ASSERT  
            Assert.Throws<InvalidOperationException>(() => _sut.Execute(accountId, 4000.1m));
            Assert.Throws<InvalidOperationException>(() => _sut.Execute(accountId, 4001m));
            Assert.Throws<InvalidOperationException>(() => _sut.Execute(accountId, 5000m));
            Assert.Throws<InvalidOperationException>(() => _sut.Execute(accountId, 20000m));
        }

        [Fact]
        public void TestWithdrawMethod_TriggersFundsLowNotification()
        {
            // ARRANGE
            var accountId = Guid.NewGuid();
            decimal ammount = 5m;

            //From Customer Account
            var customerName = "jacob";
            var customerEmail = "jacob@mail";
            decimal balance = 5m;
            decimal paidIn = 0m;
            decimal withdrawn = 0m;

            var account = new Account
            {
                Id = accountId,
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Name = customerName,
                    Email = customerEmail
                },
                Balance = balance,
                PaidIn = paidIn,
                Withdrawn = withdrawn
            };

            //Setup mocked properites
            _accountRepoMock.Setup(x => x.GetAccountById(accountId))
                .Returns(account);

            // ACT
            _sut.Execute(accountId, ammount);

            // ASSERT  
            _notificationMock.Verify(x => x.NotifyFundsLow(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void TestWithdrawMethod_DoesNotTriggersFundsLowNotification()
        {
            // ARRANGE
            var accountId = Guid.NewGuid();
            decimal ammount = 5m;

            //From Customer Account
            var customerName = "jacob";
            var customerEmail = "jacob@mail";
            decimal balance = 1005m;
            decimal paidIn = 0m;
            decimal withdrawn = 0m;

            var account = new Account
            {
                Id = accountId,
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Name = customerName,
                    Email = customerEmail
                },
                Balance = balance,
                PaidIn = paidIn,
                Withdrawn = withdrawn
            };

            //Setup mocked properites
            _accountRepoMock.Setup(x => x.GetAccountById(accountId))
                .Returns(account);

            // ACT
            _sut.Execute(accountId, ammount);

            // ASSERT  
            _notificationMock.Verify(x => x.NotifyFundsLow(It.IsAny<string>()), Times.Never);
        }
    }
}
