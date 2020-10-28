using Amazon.Runtime.Internal.Util;
using Api.Aws.Services.S3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Aws.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class S3LambdaController : ControllerBase
    {
        private readonly S3Service _s3Service;
        private readonly ILogger<S3LambdaController> _logger;

        public S3LambdaController(S3Service s3Service, ILogger<S3LambdaController> logger)
        {
            _s3Service = s3Service;
            _logger = logger;
        }

        [HttpPost("uploadDirty")]
        public async Task<ActionResult<string>> UploadDirtyImage(IFormFile file)
        {
            try
            {
                await _s3Service.UploadFileAsync(file, BucketNames.DirtyUpload);
                return "uploaded successfully";
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                return BadRequest("upload failed, check " + e.Message);
            }
        }
    }
}