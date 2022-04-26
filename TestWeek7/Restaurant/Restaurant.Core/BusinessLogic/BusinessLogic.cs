using Restaurant.Core.Entities;
using Restaurant.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Core.BusinessLogic
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly IDishRepository _dishRepository;
        private readonly IAccountRepository _accountRepository;

        // Constructor
        public BusinessLogic(
            IDishRepository dishRepository,
            IAccountRepository accountRepository
            )
        {
            _dishRepository = dishRepository;
            _accountRepository = accountRepository;
        }
        public Result AddDish(Dish dish)
        {
            if (dish == null)
                return new Result(false, "Invalid dish data");

            var result = _dishRepository.Add(dish);

            return new Result(result, result ? null : "Cannot add dish");
        }

        public Result RemoveDish(Dish dish)
        {
            if (dish == null)
                return new Result(false, "Invalid dish data");

            var result = _dishRepository.Remove(dish);

            return new Result(result, result ? null : "Cannot remove dish");
        }

        public IList<Dish> GetAllDishes(Func<Dish, bool> predicate)
            => _dishRepository.GetAll(predicate) as IList<Dish>;

        public Dish GetDish(int id)
            => _dishRepository.Get(id);

        public Result UpdateDish(Dish dish)
        {
            if (dish == null)
                return new Result(false, "Invalid dish data");

            var result = _dishRepository.Update(dish);

            return new Result(result, result ? null : "Cannot update dish");
        }

        public Account GetAccount(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            return _accountRepository.GetByEmail(email);
        }

        public Result AddAccount(Account account)
        {
            if (account == null)
                return new Result(false, "Invalid account data");

            var result = _accountRepository.Add(account);

            return new Result(result, result ? null : "Cannot add account");
        }

        public Result RegisterAccount(string email, string password)
        {
            var account = new Account
            {
                Email = email,
                Role = Role.User,
                Password = Utils.AccountUtils.Hash(password, Constants.PREFERRED_HASHING_ALGORITHM)
            };

            try
            {
                bool result = _accountRepository.Add(account);

                return new Result(result, result ? null : "Cannot register account");
            }
            catch (Exception ex)
            {
                return new Result(false, "Cannot add account", ex);
            }
        }

        public Result CheckAccount(string email, string password)
        {
            var account = GetAccount(email);

            if (account == null)
                return new Result(false, "Account not found)");

            string hashedPassword = Utils.AccountUtils.Hash(password, Constants.PREFERRED_HASHING_ALGORITHM);

            if (!account.Password.Equals(hashedPassword))
                return new Result(false, "Password for the account does not match");

            return new Result();
        }

        public Result RemoveDishById(int id)
        {
            if (id <= 0)
                return new Result(false, "Invalid ID");

            var result = _dishRepository.RemoveByKey(id);

            return new Result(result, result ? null : "Cannot remove dish");
        }
    }
}