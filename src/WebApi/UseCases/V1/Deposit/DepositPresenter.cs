namespace WebApi.UseCases.V1.Deposit
{
    using System.Linq;
    using Application.Boundaries.Deposit;
    using Domain;
    using Domain.Accounts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Generates Deposit presentations.
    /// </summary>
    public sealed class DepositPresenter : IDepositOutputPort
    {
        private readonly Notification _notification;

        public DepositPresenter(Notification notification)
        {
            this._notification = notification;
        }

        /// <summary>
        /// ViewModel result.
        /// </summary>
        /// <returns>IActionResult</returns>
        public IActionResult? ViewModel { get; private set; }

        public void Invalid()
        {
            var errorMessages = this._notification
                .ErrorMessages
                .ToDictionary(item => item.Key, item => item.Value.ToArray());

            var problemDetails = new ValidationProblemDetails(errorMessages);
            this.ViewModel = new BadRequestObjectResult(problemDetails);
        }

        public void DepositedSuccessful(IAccount account) =>
            this.ViewModel = new OkObjectResult(new DepositResponse(account));

        public void NotFound() =>
            this.ViewModel = new NotFoundObjectResult("Account not found.");
    }
}
