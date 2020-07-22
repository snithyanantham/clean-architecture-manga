// <copyright file="SeedData.cs" company="Ivan Paulovich">
// Copyright © Ivan Paulovich. All rights reserved.
// </copyright>

namespace Infrastructure.DataAccess
{
    using System;
    using Domain.Accounts.ValueObjects;
    using Domain.Customers.ValueObjects;
    using Domain.Security.ValueObjects;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Credit = Entities.Credit;
    using Debit = Entities.Debit;

    /// <summary>
    /// </summary>
    public static class SeedData
    {
        public static readonly ExternalUserId DefaultExternalUserId =
            new ExternalUserId("github/ivanpaulovich");

        public static readonly UserId DefaultUserId =
            new UserId(new Guid("E278EE65-6C41-42D6-9A73-838199A44D62"));

        public static readonly CustomerId DefaultCustomerId =
            new CustomerId(new Guid("197d0438-e04b-453d-b5de-eca05960c6ae"));

        public static readonly AccountId DefaultAccountId =
            new AccountId(new Guid("4c510cfe-5d61-4a46-a3d9-c4313426655f"));

        public static readonly CreditId DefaultCreditId =
            new CreditId(new Guid("7BF066BA-379A-4E72-A59B-9755FDA432CE"));

        public static readonly DebitId DefaultDebitId =
            new DebitId(new Guid("31ADE963-BD69-4AFB-9DF7-611AE2CFA651"));

        public static void Seed(ModelBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Entity<User>()
                .HasData(
                    new
                    {
                        UserId = DefaultUserId,
                        ExternalUserId = DefaultExternalUserId
                    });

            builder.Entity<Customer>()
                .HasData(
                    new
                    {
                        CustomerId = DefaultCustomerId,
                        FirstName = new Name(Messages.UserName),
                        LastName = new Name(Messages.UserName),
                        SSN = new SSN(Messages.UserSSN),
                        UserId = DefaultUserId
                    });

            builder.Entity<Account>()
                .HasData(
                    new
                    {
                        AccountId = DefaultAccountId,
                        CustomerId = DefaultCustomerId
                    });

            builder.Entity<Credit>()
                .HasData(
                    new
                    {
                        CreditId = DefaultCreditId,
                        AccountId = DefaultAccountId,
                        TransactionDate = DateTime.UtcNow,
                        Value = 400m,
                        Currency = Currency.Dollar.Code
                    });

            builder.Entity<Debit>()
                .HasData(
                    new
                    {
                        DebitId = DefaultDebitId,
                        AccountId = DefaultAccountId,
                        TransactionDate = DateTime.UtcNow,
                        Value = 400m,
                        Currency = Currency.Dollar.Code
                    });


        }
    }
}
