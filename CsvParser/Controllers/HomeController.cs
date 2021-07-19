using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CsvParser.Contracts;
using CsvParser.Models;
using CsvParser.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CsvParser.Controllers
{
    /// <summary>
    /// This file does not need to be unit tested. You shouldn't need to modify this.
    /// Bonus Task:
    /// Use your favourite dependency injection framework (i.e. Autofac, Ninject) to inject all the services.
    /// This project uses MVC5 so make sure you grab the right implementation.
    /// Bonus Task:
    /// Validate the input to the post function on the client side. You can use javascript or Razor syntax,
    /// but don't use any 3rd party code for this.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICsvFileHandler _csvFileHandler;

        public HomeController(ILogger<HomeController> logger)
        {
            _csvFileHandler = new CsvFileHandler(new ParsingService(), new ValidationService(), new FileService());

            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(FileUploadModel fileUploadModel)
        {
            if (fileUploadModel?.File == null || fileUploadModel.File.Length <= 0)
                return HandleError("You need to click Choose File first, then Submit.");

            var result = _csvFileHandler.ParseCsvFile(fileUploadModel.File, fileUploadModel.ContainsHeader);
            if (!result.Success)
                return HandleError(result.ErrorMessage);

            return View("FormattedDisplay", new FormattedDisplayModel
            {
                OriginalFileName = fileUploadModel.File.FileName,
                CsvTable = result.ParsedCsvContent
            });
        }

        private IActionResult HandleError(string errorMessage)
        {
            _logger.LogError(errorMessage);
            return RedirectToAction("Error", new { errorMessage });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        public IActionResult Error(string errorMessage)
        {
            var model = new ErrorViewModel { ErrorMessage = errorMessage };
            return View(model);
        }
    }
}