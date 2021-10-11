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
    public class MoneyboxTransferTests
    {
        private readonly TransferMoney _sut;
        private readonly Mock<IAccountRepository> _accountRepoMock = new();
        private readonly Mock<INotificationService> _notificationMock = new();
        private readonly ITestOutputHelper output;

        public MoneyboxTransferTests(ITestOutputHelper output)
        {
            _sut = new TransferMoney(_accountRepoMock.Object, _notificationMock.Object);
            this.output = output;
        }


        [Fact]
        public void TestTransferMethod_CheckMockedProperties()
        {
            // ARRANGE
            var fromAccountId = Guid.NewGuid();
            var toAccountId = Guid.NewGuid();

            //From Customer Account
            var fromCustomerName = "jacob";
            var fromCustomerEmail = "jacob@mail";
            decimal fromBalance = 5m;
            decimal fromPaidIn = 0m;
            decimal fromWithdrawn = 0m;

            var fromAccount = new Account
            {
                Id = fromAccountId,
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Name = fromCustomerName,
                    Email = fromCustomerEmail
                },
                Balance = fromBalance,
                PaidIn = fromPaidIn,
                Withdrawn = fromWithdrawn
            };

            //To Customer Account
            var toCustomerName = "steve";
            var toCustomerEmail = "steve@mail";
            decimal toBalance = 5m;
            decimal toPaidIn = 0m;
            decimal toWithdrawn = 0m;

            var toAccount = new Account
            {
                Id = toAccountId,
                User = new User
                {
                    Id = new Guid(),
                    Name = toCustomerName,
                    Email = toCustomerEmail
                },
                Balance = toBalance,
                PaidIn = toPaidIn,
                Withdrawn = toWithdrawn
            };

            //Setup mocked properites
            _accountRepoMock.Setup(x => x.GetAccountById(fromAccountId))
                .Returns(fromAccount);
            _accountRepoMock.Setup(x => x.GetAccountById(toAccountId))
                .Returns(toAccount);

            // ASSERT  
            Assert.Equal(fromCustomerName, fromAccount.User.Name);
            Assert.Equal(toCustomerName, toAccount.User.Name);
            Assert.Equal(fromCustomerEmail, fromAccount.User.Email);
            Assert.Equal(toCustomerEmail, toAccount.User.Email);
            Assert.Equal(fromBalance, fromAccount.Balance);
            Assert.Equal(toBalance, toAccount.Balance);
            Assert.Equal(fromPaidIn, fromAccount.PaidIn);
            Assert.Equal(toPaidIn, toAccount.PaidIn);
            Assert.Equal(fromWithdrawn, fromAccount.Withdrawn);
            Assert.Equal(toWithdrawn, toAccount.Withdrawn);
        }

        [Fact]
        public void TestTransferMethod_CheckUpdatedBalanceWithdrawnAndPaidIn()
        {
            // ARRANGE
            var fromAccountId = Guid.NewGuid();
            var toAccountId = Guid.NewGuid();
            decimal ammount = 5m;

            //From Customer Account
            var fromCustomerName = "jacob";
            var fromCustomerEmail = "jacob@mail";
            decimal fromBalance = 5m;
            decimal fromPaidIn = 0m;
            decimal fromWithdrawn = 0m;

            var fromAccount = new Account
            {
                Id = fromAccountId,
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Name = fromCustomerName,
                    Email = fromCustomerEmail
                },
                Balance = fromBalance,
                PaidIn = fromPaidIn,
                Withdrawn = fromWithdrawn
            };

            //To Customer Account
            var toCustomerName = "steve";
            var toCustomerEmail = "steve@mail";
            decimal toBalance = 5m;
            decimal toPaidIn = 0m;
            decimal toWithdrawn = 0m;

            var toAccount = new Account
            {
                Id = toAccountId,
                User = new User
                {
                    Id = new Guid(),
                    Name = toCustomerName,
                    Email = toCustomerEmail
                },
                Balance = toBalance,
                PaidIn = toPaidIn,
                Withdrawn = toWithdrawn
            };

            //Setup mocked properites
            _accountRepoMock.Setup(x => x.GetAccountById(fromAccountId))
                .Returns(fromAccount);
            _accountRepoMock.Setup(x => x.GetAccountById(toAccountId))
                .Returns(toAccount);
            _accountRepoMock.Setup(x => x.Update(It.IsAny<Account>()))
                .Callback((Account account) =>
                {
                    if (account.Id == fromAccountId)
                    {
                        fromAccount = account;
                    }
                    else if (account.Id == toAccountId)
                    {
                        toAccount = account;
                    }
                    else { throw new ArgumentException("Account GUID does not match mocked Id's"); }
                }
            );

            // ACT
            _sut.Execute(fromAccountId, toAccountId, ammount);

            // ASSERT  
            Assert.Equal(0m, fromAccount.Balance);
            Assert.Equal(10m, toAccount.Balance);
            Assert.Equal(-5m, fromAccount.Withdrawn);
            Assert.Equal(5m, toAccount.PaidIn);
        }

        [Fact]
        public void TestTransferMethod_ThrowsInsufficientFundsException()
        {
            // ARRANGE
            var fromAccountId = Guid.NewGuid();
            var toAccountId = Guid.NewGuid();

            //From Customer Account
            var fromCustomerName = "jacob";
            var fromCustomerEmail = "jacob@mail";
            decimal fromBalance = 5m;
            decimal fromPaidIn = 0m;
            decimal fromWithdrawn = 0m;

            var fromAccount = new Account
            {
                Id = fromAccountId,
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Name = fromCustomerName,
                    Email = fromCustomerEmail
                },
                Balance = fromBalance,
                PaidIn = fromPaidIn,
                Withdrawn = fromWithdrawn
            };

            //To Customer Account
            var toCustomerName = "steve";
            var toCustomerEmail = "steve@mail";
            decimal toBalance = 5m;
            decimal toPaidIn = 0m;
            decimal toWithdrawn = 0m;

            var toAccount = new Account
            {
                Id = toAccountId,
                User = new User
                {
                    Id = new Guid(),
                    Name = toCustomerName,
                    Email = toCustomerEmail
                },
                Balance = toBalance,
                PaidIn = toPaidIn,
                Withdrawn = toWithdrawn
            };

            //Setup mocked properites
            _accountRepoMock.Setup(x => x.GetAccountById(fromAccountId))
                .Returns(fromAccount);
            _accountRepoMock.Setup(x => x.GetAccountById(toAccountId))
                .Returns(toAccount);

            // ASSERT  
            Assert.Throws<InvalidOperationException>(() => _sut.Execute(fromAccountId, toAccountId, 10m));
            Assert.Throws<InvalidOperationException>(() => _sut.Execute(fromAccountId, toAccountId, 100m));
            Assert.Throws<InvalidOperationException>(() => _sut.Execute(fromAccountId, toAccountId, 123.5m));
        }


        [Fact]
        public void TestTransferMethod_ThrowsPayInLimitReachedException()
        {
            // ARRANGE
            var fromAccountId = Guid.NewGuid();
            var toAccountId = Guid.NewGuid();

            //From Customer Account
            var fromCustomerName = "jacob";
            var fromCustomerEmail = "jacob@mail";
            decimal fromBalance = 5m;
            decimal fromPaidIn = 0m;
            decimal fromWithdrawn = 0m;

            var fromAccount = new Account
            {
                Id = fromAccountId,
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Name = fromCustomerName,
                    Email = fromCustomerEmail
                },
                Balance = fromBalance,
                PaidIn = fromPaidIn,
                Withdrawn = fromWithdrawn
            };

            //To Customer Account
            var toCustomerName = "steve";
            var toCustomerEmail = "steve@mail";
            decimal toBalance = 5m;
            decimal toPaidIn = 0m;
            decimal toWithdrawn = 0m;

            var toAccount = new Account
            {
                Id = toAccountId,
                User = new User
                {
                    Id = new Guid(),
                    Name = toCustomerName,
                    Email = toCustomerEmail
                },
                Balance = toBalance,
                PaidIn = toPaidIn,
                Withdrawn = toWithdrawn
            };

            //Setup mocked properites
            _accountRepoMock.Setup(x => x.GetAccountById(fromAccountId))
                .Returns(fromAccount);
            _accountRepoMock.Setup(x => x.GetAccountById(toAccountId))
                .Returns(toAccount);

            // ASSERT  
            Assert.Throws<InvalidOperationException>(() => _sut.Execute(fromAccountId, toAccountId, 4000.1m));
            Assert.Throws<InvalidOperationException>(() => _sut.Execute(fromAccountId, toAccountId, 4001m));
            Assert.Throws<InvalidOperationException>(() => _sut.Execute(fromAccountId, toAccountId, 5000m));
            Assert.Throws<InvalidOperationException>(() => _sut.Execute(fromAccountId, toAccountId, 20000m));
        }

        [Fact]
        public void TestTransferMethod_TriggersFundsLowNotification()
        {
            // ARRANGE
            var fromAccountId = Guid.NewGuid();
            var toAccountId = Guid.NewGuid();
            decimal ammount = 5m;

            //From Customer Account
            var fromCustomerName = "jacob";
            var fromCustomerEmail = "jacob@mail";
            decimal fromBalance = 5m;
            decimal fromPaidIn = 0m;
            decimal fromWithdrawn = 0m;

            var fromAccount = new Account
            {
                Id = fromAccountId,
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Name = fromCustomerName,
                    Email = fromCustomerEmail
                },
                Balance = fromBalance,
                PaidIn = fromPaidIn,
                Withdrawn = fromWithdrawn
            };

            //To Customer Account
            var toCustomerName = "steve";
            var toCustomerEmail = "steve@mail";
            decimal toBalance = 5m;
            decimal toPaidIn = 0m;
            decimal toWithdrawn = 0m;

            var toAccount = new Account
            {
                Id = toAccountId,
                User = new User
                {
                    Id = new Guid(),
                    Name = toCustomerName,
                    Email = toCustomerEmail
                },
                Balance = toBalance,
                PaidIn = toPaidIn,
                Withdrawn = toWithdrawn
            };

            //Setup mocked properites
            _accountRepoMock.Setup(x => x.GetAccountById(fromAccountId))
                .Returns(fromAccount);
            _accountRepoMock.Setup(x => x.GetAccountById(toAccountId))
                .Returns(toAccount);

            //ACT
            _sut.Execute(fromAccountId, toAccountId, ammount);

            //ASSERT
            _notificationMock.Verify(x => x.NotifyFundsLow(fromCustomerEmail), Times.Once);
        }

        [Fact]
        public void TestTransferMethod_NotifyApproachingPayInLimit()
        {
            // ARRANGE
            var fromAccountId = Guid.NewGuid();
            var toAccountId = Guid.NewGuid();
            decimal ammount = 600m;

            //From Customer Account
            var fromCustomerName = "jacob";
            var fromCustomerEmail = "jacob@mail";
            decimal fromBalance = 600m;
            decimal fromPaidIn = 0m;
            decimal fromWithdrawn = 0m;

            var fromAccount = new Account
            {
                Id = fromAccountId,
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Name = fromCustomerName,
                    Email = fromCustomerEmail
                },
                Balance = fromBalance,
                PaidIn = fromPaidIn,
                Withdrawn = fromWithdrawn
            };

            //To Customer Account
            var toCustomerName = "steve";
            var toCustomerEmail = "steve@mail";
            decimal toBalance = 3000m;
            decimal toPaidIn = 3000m;
            decimal toWithdrawn = 0m;

            var toAccount = new Account
            {
                Id = toAccountId,
                User = new User
                {
                    Id = new Guid(),
                    Name = toCustomerName,
                    Email = toCustomerEmail
                },
                Balance = toBalance,
                PaidIn = toPaidIn,
                Withdrawn = toWithdrawn
            };

            //Setup mocked properites
            _accountRepoMock.Setup(x => x.GetAccountById(fromAccountId))
                .Returns(fromAccount);
            _accountRepoMock.Setup(x => x.GetAccountById(toAccountId))
                .Returns(toAccount);

            //ACT
            _sut.Execute(fromAccountId, toAccountId, ammount);

            //ASSERT
            _notificationMock.Verify(x => x.NotifyApproachingPayInLimit(toCustomerEmail), Times.Once);
        }

        [Fact]
        public void TestTransferMethod_DoesNotTriggersFundsLowNotification()
        {
            // ARRANGE
            var fromAccountId = Guid.NewGuid();
            var toAccountId = Guid.NewGuid();
            decimal ammount = 5m;

            //From Customer Account
            var fromCustomerName = "jacob";
            var fromCustomerEmail = "jacob@mail";
            decimal fromBalance = 1005m;
            decimal fromPaidIn = 0m;
            decimal fromWithdrawn = 0m;

            var fromAccount = new Account
            {
                Id = fromAccountId,
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Name = fromCustomerName,
                    Email = fromCustomerEmail
                },
                Balance = fromBalance,
                PaidIn = fromPaidIn,
                Withdrawn = fromWithdrawn
            };

            //To Customer Account
            var toCustomerName = "steve";
            var toCustomerEmail = "steve@mail";
            decimal toBalance = 5m;
            decimal toPaidIn = 0m;
            decimal toWithdrawn = 0m;

            var toAccount = new Account
            {
                Id = toAccountId,
                User = new User
                {
                    Id = new Guid(),
                    Name = toCustomerName,
                    Email = toCustomerEmail
                },
                Balance = toBalance,
                PaidIn = toPaidIn,
                Withdrawn = toWithdrawn
            };

            //Setup mocked properites
            _accountRepoMock.Setup(x => x.GetAccountById(fromAccountId))
                .Returns(fromAccount);
            _accountRepoMock.Setup(x => x.GetAccountById(toAccountId))
                .Returns(toAccount);

            //ACT
            _sut.Execute(fromAccountId, toAccountId, ammount);

            //ASSERT
            _notificationMock.Verify(x => x.NotifyFundsLow(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void TestTransferMethod_DoesNotNotifyApproachingPayInLimit()
        {
            // ARRANGE
            var fromAccountId = Guid.NewGuid();
            var toAccountId = Guid.NewGuid();
            decimal ammount = 600m;

            //From Customer Account
            var fromCustomerName = "jacob";
            var fromCustomerEmail = "jacob@mail";
            decimal fromBalance = 600m;
            decimal fromPaidIn = 0m;
            decimal fromWithdrawn = 0m;

            var fromAccount = new Account
            {
                Id = fromAccountId,
                User = new User
                {
                    Id = Guid.NewGuid(),
                    Name = fromCustomerName,
                    Email = fromCustomerEmail
                },
                Balance = fromBalance,
                PaidIn = fromPaidIn,
                Withdrawn = fromWithdrawn
            };

            //To Customer Account
            var toCustomerName = "steve";
            var toCustomerEmail = "steve@mail";
            decimal toBalance = 0m;
            decimal toPaidIn = 0m;
            decimal toWithdrawn = 0m;

            var toAccount = new Account
            {
                Id = toAccountId,
                User = new User
                {
                    Id = new Guid(),
                    Name = toCustomerName,
                    Email = toCustomerEmail
                },
                Balance = toBalance,
                PaidIn = toPaidIn,
                Withdrawn = toWithdrawn
            };

            //Setup mocked properites
            _accountRepoMock.Setup(x => x.GetAccountById(fromAccountId))
                .Returns(fromAccount);
            _accountRepoMock.Setup(x => x.GetAccountById(toAccountId))
                .Returns(toAccount);

            //ACT
            _sut.Execute(fromAccountId, toAccountId, ammount);

            //ASSERT
            _notificationMock.Verify(x => x.NotifyApproachingPayInLimit(It.IsAny<string>()), Times.Never);
        }
    }
}
