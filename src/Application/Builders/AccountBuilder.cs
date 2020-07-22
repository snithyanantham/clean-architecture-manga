﻿namespace Domain.Accounts
{
    using System;
    using Customers.ValueObjects;

    /// <summary>
    /// 
    /// </summary>
    public sealed class AccountBuilder
    {
        private readonly IAccountFactory _accountFactory;
        private readonly Notification _notification;

        private CustomerId? _customerId;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountFactory"></param>
        /// <param name="notification"></param>
        public AccountBuilder(
            IAccountFactory accountFactory, Notification notification)
        {
            this._accountFactory = accountFactory;
            this._notification = notification;
        }

        public AccountBuilder Customer(Guid customerId)
        {
            this._customerId = CustomerId.Create(this._notification, customerId);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IAccount Build()
        {
            if (!this._customerId.HasValue ||
                !this._notification.IsValid)
            {
                return AccountNull.Instance;
            }

            return this.BuildInternal();
        }

        private IAccount BuildInternal() =>
            this._accountFactory
                .NewAccount(this._customerId!.Value);
    }
}
