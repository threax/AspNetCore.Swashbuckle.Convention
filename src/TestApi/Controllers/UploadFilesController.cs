using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Controllers
{
    /// <summary>
    /// Test uploading files
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class UploadFilesController : Controller
    {
        private IHostingEnvironment hostingEnv;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="env"></param>
        public UploadFilesController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }

        //Want to support multiple files, but can't get it working right now.

        ///// <summary>
        ///// Upload files.
        ///// </summary>
        ///// <param name="files">The files uploaded.</param>
        //[HttpPost]
        //public long UploadFiles([FromBody] IList<IFormFile> files)
        //{
        //    long size = 0;
        //    foreach (var file in files)
        //    {
        //        var filename = ContentDispositionHeaderValue
        //                        .Parse(file.ContentDisposition)
        //                        .FileName
        //                        .Trim('"');

        //        filename = hostingEnv.WebRootPath + $@"\{filename}";
        //        size += file.Length;
        //        //using (FileStream fs = System.IO.File.Create(filename))
        //        //{
        //        //    file.CopyTo(fs);
        //        //    fs.Flush();
        //        //}
        //    }
        //    return size;
        //}

        /// <summary>
        /// Upload file. Should show a file picker.
        /// </summary>
        /// <param name="file">The files uploaded.</param>
        [HttpPost]
        public long UploadFile(IFormFile file)
        {
            long size = 0;
            var filename = ContentDispositionHeaderValue
                            .Parse(file.ContentDisposition)
                            .FileName
                            .Trim('"');

            filename = hostingEnv.WebRootPath + $@"\{filename}";
            size += file.Length;
            return size;
        }

        /// <summary>
        /// Upload file, should need a bearer.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public long UploadFileAuth(IFormFile file)
        {
            long size = 0;
            var filename = ContentDispositionHeaderValue
                            .Parse(file.ContentDisposition)
                            .FileName
                            .Trim('"');

            filename = hostingEnv.WebRootPath + $@"\{filename}";
            size += file.Length;
            return size;
        }

        /// <summary>
        /// Get the content, should have file set for its 200 response schema.
        /// </summary>
        /// <remarks>
        /// This is not implemented and will throw an exception if called.
        /// </remarks>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpGet("{*file}")]
        [Produces("text/html")]
        public FileStreamResult GetContent(String file)
        {
            throw new NotImplementedException();
        }
    }
}
