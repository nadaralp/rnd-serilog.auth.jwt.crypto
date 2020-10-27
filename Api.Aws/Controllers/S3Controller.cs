using Api.Aws.Services.S3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IS3Service _s3Repository;

        public S3Controller(IS3Service s3Repository)
        {
            _s3Repository = s3Repository;
        }

        
        [HttpPost("simple")]
        public async Task<ActionResult<string>> SimpleS3Uplaod([FromForm] IFormFile formFile)
        {
            try
            {
                await _s3Repository.UploadFileAsync(formFile, BucketNames.SimpleUploadBucket);
            }
            catch(Exception e)
            {
            }
            
            return "ok";

            
        }

        [HttpPost("simple_batch")]
        public ActionResult<string> SimpleS3Uplaod(IFormFileCollection files)
        {
            return "ok";
        }
        [
            
        HttpPost("image_resize")]
        public ActionResult<string> ImageResizeByLambdaTrigger(IFormFileCollection files)
        {
            // Upload to S3

            // Upload will trigger Lambda function

            // Lambda function will resize and upload to new S3 bucket.

            return "ok";
        }
    }
}
