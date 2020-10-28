using Amazon.Runtime.Internal.Util;
using Api.Aws.Services.S3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Aws.Controllers
{
    [ApiController, Route("api/s3")]
    public class S3Controller : ControllerBase
    {
        private readonly S3Service _s3Service;
        private readonly ILogger<S3Controller> _logger;

        public S3Controller(S3Service s3Service, ILogger<S3Controller> logger)
        {
            _s3Service = s3Service;
            _logger = logger;
        }

        [HttpPost("simple")]
        public async Task<ActionResult<string>> SimpleS3Uplaod([FromForm] IFormFile formFile)
        {
            try
            {
                await _s3Service.UploadFileAsync(formFile, BucketNames.SimpleUploadBucket);
            }
            catch (Exception e)
            {
            }

            return "ok";
        }

        [HttpPost("simple_batch")]
        public async Task<ActionResult<string>> SimpleS3Uplaod([FromForm] IFormFileCollection files)
        {
            for (int i = 0; i < 20; i++)
            {
                files.Append(files[0]);
            }

            try
            {
                await _s3Service.UploadFilesInBatchAsync(files, BucketNames.SimpleUploadBucket);
                await _s3Service.UploadFilesInBatchAsyncV2(files, BucketNames.SimpleUploadBucket);
                await _s3Service.UploadFilesInBatchAsyncV3(files, BucketNames.SimpleUploadBucket);
                return Ok("ok");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        [HttpPost("image_resize")]
        public ActionResult<string> ImageResizeByLambdaTrigger(IFormFileCollection files)
        {
            // Upload to S3

            // Upload will trigger Lambda function

            // Lambda function will resize and upload to new S3 bucket.

            return "ok";
        }
    }
}