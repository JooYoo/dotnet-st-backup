using egRelationalDT.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace egRelationalDT.ActionResults
{
    public class CustomActionResult : IActionResult
    {
        private readonly CustomActionResultVM _resultVM;
        public CustomActionResult(CustomActionResultVM resultVM)
        {
            _resultVM = resultVM;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_resultVM.Exception ?? _resultVM.Publisher as object)
            {
                StatusCode = _resultVM.Exception != null ? StatusCodes.Status500InternalServerError : StatusCodes.Status200OK
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
