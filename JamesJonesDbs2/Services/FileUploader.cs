using JamesJonesApplication.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UploadEncryption.Services
{
    /// <summary>
    /// This class provides methods that allow for working with files being recieved as an IFormFile,
    /// That will be written to disk, and then read from again at a later date.
    /// This includes functionality for generating File names to avoid clashes in the file system
    /// This class does not provide functionality for storing information in a database - this will need to be added
    /// </summary>
    public class FileUploaderService
    {
        string _uploadRootPath; 
        //DI encryption
        private readonly EncryptionService _encryptionService;

        public FileUploaderService(IWebHostEnvironment env, EncryptionService encryptionService)
        {
            // the absolute path to the 'Uploads' folder - this folder needs to be created in the wwwroot folder
            // whis will be relative to the current location of the Solution (or deployed application)
            // for example, the current location is: C:\TAFE Weekly\BR Week 13\Security\UploadEncryption Part 1 - Upload Only\UploadEncryption\wwwroot\Uploads
            _uploadRootPath = Path.Combine(env.WebRootPath, "Uploads");
            //Add the Encryption service to the ctor
            _encryptionService = encryptionService;
        }

        /// <summary>
        /// Calculate and return a unique file name (with the file extension)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string UniqueFileName(string fileName)
        {
            try
            {
                // Retrieve a reference to the 'Upload' folder
                DirectoryInfo dir = new DirectoryInfo(_uploadRootPath);

                // check to see if there is already a file in the folder with a matching name. 
                // only checks the immediate directory - creates a FileInfo class for each file
                // and compares against the uploaded files name
                if (!dir.EnumerateFiles().Any(c => c.Name.Equals(fileName)))
                {
                    // return the original filename - there is not existing file with the same name
                    return fileName;
                }

                // Retrieve the extension
                string extension = fileName.Split('.').LastOrDefault();

                // Set a default extension
                if(String.IsNullOrEmpty(extension))
                {
                    extension = "txt";
                }

                // return a generated filename (with the original extension)
                return $"{Guid.NewGuid()}.{extension}";

            }
            catch (Exception e)
            {
                // potentially log the exception, or return it in a way that is manageable by the user
                // Could be presented as an error message in a toast
                return null;
            }
        }


        /// <summary>
        /// Write the file contents to disk - ensuring a unique file name
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task SaveFile(IFormFile file)
        {
            string fileName = UniqueFileName(file.FileName);

            byte[] fileContents;
            //Create ne memroy stream to take the file, copy the file into memory stream and wtite the memory stream into the byte array
            using (var stream = new MemoryStream())
            {
                //Copy
                await file.CopyToAsync(stream);
                //Into array to process encryption
                fileContents = stream.ToArray();
            }

            //Encrypts the data arrray[] and returns the processed data
            var encryptedData = _encryptionService.EncryptByteArray(fileContents);




            // create a new MemoryStream
            using (var stream = new MemoryStream(encryptedData))
            {

                // Build a target filepath for the uploaded files
                var targetFile = Path.Combine(_uploadRootPath, fileName);

                // create a new FileStream - with the path and the FileMode
                using (var fileStream = new FileStream(targetFile, FileMode.Create))
                {
                    // push the MemoryStream into the FileStream - forcing the data to disk
                    stream.WriteTo(fileStream);
                }
            }

        }

        /// <summary>
        /// Loads and returns the file on disk, or null;
        /// </summary>
        /// <param name="fileName">the fully qualified (extension included) name of the file</param>
        /// <returns></returns>
        public FileInfo LoadFile(string fileName)
        {
            DirectoryInfo dir = new DirectoryInfo(_uploadRootPath);

            // check to see if the file exists - returning null if ti does not
            if (!dir.EnumerateFiles().Any(c => c.Name.Equals(fileName)))
            {
                return null;
            }

            return dir.EnumerateFiles().Where(c => c.Name.Equals(fileName)).FirstOrDefault();            
        }


        /// <summary>
        /// Return the MIME type of given fileName (must include the extension)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetFileExtension(string fileName)
        {
            // attempt to get the fileType
            if(new FileExtensionContentTypeProvider().TryGetContentType(fileName, out string contentType))
            {
                return contentType;
            }

            return null;

        }

        /// <summary>
        /// Retrieve a byte[] that represents a files data
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<byte[]> ReadFileIntoMemory(string fileName)
        {

            var file = LoadFile(fileName);

            if (file == null)
            {
                return null;
            }

            // 1. demonstration of reading with MemoryStream (almost creating an unecessary MemoryStream)
            using (var memStream = new MemoryStream())
            {
                using (var fileStream = File.OpenRead(file.FullName))
                {
                    //pull file from memory
                    fileStream.CopyTo(memStream);

                    //Convert to byte array
                    var ecryptedData = memStream.ToArray();

                    //return decrypted file to user
                    return _encryptionService.DecryptByteArray(ecryptedData);

                }
            }

            // 2. demonstration of reading with File class
            // return await File.ReadAllBytesAsync(file.FullName);

           
        }


        /// <summary>
        /// Retrieve the path of a file to be loaded, or null
        /// </summary>
        /// <param name="fileName">the fully qualified (extension included) name of the file</param>
        /// <returns></returns>
        public async Task<string> GetFilePath(string fileName)
        {
            var file = LoadFile(fileName);

            if(file == null)
            {
                return null;
            }

            // assuming one level of folder nesting
            var directory = file.Directory.Name;

            if (directory.Equals("Uploads"))
            {
                return $"/{directory}/{file.Name}";
            }
            else
            {
                return $"/Uploads/{directory}/{file.Name}";
            }

        }
    }
}
