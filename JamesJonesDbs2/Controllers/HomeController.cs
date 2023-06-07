using JamesJonesDbs2.Models;
using JamesJonesDbs2.Models.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UploadEncryption.Services;

namespace JamesJonesDbs2.Controllers
{
    /// <summary>
    /// Controller dedicated to managing home page
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Dependency Injections
        /// </summary>
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _databaseContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly FileUploaderService _uploader;

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="databaseContext"></param>
        /// <param name="webHostEnvironment"></param>
        public HomeController(ILogger<HomeController> logger, DatabaseContext databaseContext, IWebHostEnvironment webHostEnvironment, FileUploaderService fileUploaderService)
        {
            _logger = logger;
            _databaseContext = databaseContext;
            _webHostEnvironment = webHostEnvironment;
            _uploader = fileUploaderService;
            
        }

        /// <summary>
        /// Returns view of the Home page and sets a session id to 1 if a user is active
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (_webHostEnvironment.IsDevelopment())
            {
                if (HttpContext.Session.Get("ID") == null)
                {
                    HttpContext.Session.SetString("ID", "0");
                }
            }
            return View();
        }
        /// <summary>
        /// Returns the about us page with the multi media material
        /// </summary>
        /// <returns></returns>
        public IActionResult AboutUs()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Upload()
        {
            return View();
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        /// <summary>
        /// Handle the Form posting of the uploaded file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ImageUpload(IFormFile file)
        {
            // Add security control for the file upload
            // DDOS attacks can come from not restricting file sizes - overloading the server's memory
            // Scripting attacks can come from uploaded files that can be browser-executed when access by another party
            var validationResult = ValidateFileUpload(file);

            if (validationResult != null && validationResult.Count > 0)
            {
                foreach (var error in validationResult)
                {
                    ModelState.AddModelError("UploadError", error);
                }
                return View("Upload");
            }

            await _uploader.SaveFile(file);
            return View("Upload");
        }

        private List<string> ValidateFileUpload(IFormFile file)
        {
            List<string> errors = new List<string>();
            // very roughly 10 Megabytes
            if (file.Length > 10000000)
            {
                errors.Add("File exceeds the 10MB size limit");
            }

            if (file.FileName.Contains('.'))
            {
                string[] acceptableExtensions = { "png", "bmp", "jpg", "jpeg" };
                string extension = file.FileName.Split('.').LastOrDefault();
                if (extension == null)
                {
                    errors.Add("File does not have an acceptable extension");
                }
                else
                {
                    if (!acceptableExtensions.Any(c => c.Equals(extension)))
                    {
                        // no match for extension
                        errors.Add($"The file extension of {extension} is not allowed");
                    }
                }
            }

            return errors;


        }

        /// <summary>
        /// Find and return the path to the selected image to the View
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LoadImage(string fileName)
        {

            try
            {
                byte[] fileBytes = await _uploader.ReadFileIntoMemory(fileName);

                var imageData = System.Convert.ToBase64String(fileBytes);

                ViewData["ImageSource"] = $"data:image/png;base64,{imageData}";
                ViewData["ImageAlt"] = "Image Loaded";
                return View("Upload");
            }
            catch
            {
                return View("Upload");
            }

        }

        /// <summary>
        /// Download a file based on the filename
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            byte[] fileBytes = await _uploader.ReadFileIntoMemory(fileName);

            // The 2 lines below will return the MIME type associated with the file - this may prevent downloading
            // the MIME type - "application/octet-stream" will force a download, rather than an attempted open of a file

            //string extensionResult = _uploader.GetFileExtension(fileName);
            //string fileExtension = String.IsNullOrEmpty(extensionResult) ? "text/plain" : extensionResult;


            if (fileBytes == null || fileBytes.Length == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return File(fileBytes, "application/octet-stream", fileDownloadName: fileName);

            // This return can be used with the lines commented out above
            //return File(fileBytes, fileExtension, fileDownloadName: fileName);
        }
    }
}