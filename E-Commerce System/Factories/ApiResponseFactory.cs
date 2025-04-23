using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerce_System.Factories
{
    public class ApiResponseFactory
    {

            //Context =>  ModelState ===> Dictionary <String , ModelstateEntry> 
            // String => kEY , name of parameter 
            //  ModelStateEntry => Object ==> errors
            //1]Get All Errors In ModelState Entry
            //2] Create Custom Response 
            //3] Return 

           public static IActionResult CustomValidationErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.Where(M => M.Value.Errors.Any())
                  .Select(M => new ValidationError
                  {
                      Filed = M.Key,
                      Errors = M.Value.Errors.Select(e => e.ErrorMessage)
                  });
            var response = new ValidationErrorResponse
            {
                ValidationErrors = errors
            };


            return new BadRequestObjectResult(response);
        }
    }
}
