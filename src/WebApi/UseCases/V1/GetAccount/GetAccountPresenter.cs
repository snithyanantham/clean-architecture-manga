namespace WebApi.UseCases.V1.GetAccount
{
    using System.Linq;
    using Application.Boundaries.GetAccount;
    using Domain;
    using Domain.Accounts;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Renders an Account with its Transactions
    /// </summary>
    public sealed class GetAccountPresenter : IGetAccountOutputPort
    {
        private readonly Notification _notification;

        public GetAccountPresenter(Notification notification)
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

        public void Successful(IAccount account) =>
            this.ViewModel = new OkObjectResult(new GetAccountResponse(account));

        public void NotFound() =>
            this.ViewModel = new NotFoundObjectResult("Account not found.");
    }
}
