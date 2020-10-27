using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Aws.Services.S3
{
    public class S3Service : IS3Service
    {
        // Guide to upload file:
        // https://docs.aws.amazon.com/AmazonS3/latest/dev/UploadObjSingleOpNET.html


        private readonly IAmazonS3 _amazonS3;
        private readonly ILogger<S3Service> _logger;

        public S3Service(IAmazonS3 amazonS3, ILogger<S3Service> logger)
        {
            _amazonS3 = amazonS3;
            _logger = logger;
        }

        public async Task UploadFileAsync(IFormFile formFile, string bucketName)
        {
            try
            {
                Stream imageStream = null;
                await formFile.CopyToAsync(imageStream);

                PutObjectRequest putObject = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = formFile.FileName,
                    InputStream = imageStream
                };

                PutObjectResponse response = await _amazonS3.PutObjectAsync(putObject);
            }

            catch(AmazonS3Exception e)
            {
                _logger.LogError(e.Message + e.StackTrace);
            }
            
        }
    }
}
