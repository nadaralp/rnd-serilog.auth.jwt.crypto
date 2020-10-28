using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
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
            _logger.LogInformation("starting S3 File upload execution");
            var watch = Stopwatch.StartNew();
            try
            {
                await _amazonS3.EnsureBucketExistsAsync(bucketName);

                _logger.LogInformation("executing thread={thread}", Thread.CurrentThread.ManagedThreadId);
                Stream imageStream = formFile.OpenReadStream();
                PutObjectRequest putObject = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = formFile.FileName,
                    InputStream = imageStream,
                    CannedACL = S3CannedACL.PublicRead
                };

                PutObjectResponse response = await _amazonS3.PutObjectAsync(putObject);
                watch.Stop();
                _logger.LogInformation("executed single upload in:{time} seconds", watch.ElapsedMilliseconds / 1000d);
            }
            catch (AmazonS3Exception e)
            {
                _logger.LogError(e.Message + e.StackTrace);
            }
        }

        public async Task UploadFilesInBatchAsync(IFormFileCollection formFiles, string bucketName)
        {
            _logger.LogInformation("starting batch file uploads with {amount_of_files} files ====== V1", formFiles.Count);
            Stopwatch watch = Stopwatch.StartNew();

            IEnumerable<Task> tasks = formFiles.Select(file => UploadFileAsync(file, bucketName));
            await Task.WhenAll(tasks);

            watch.Stop();
            _logger.LogInformation("uploaded batch in {time} seconds ===== V1", watch.ElapsedMilliseconds / 1000d);
        }

        public async Task UploadFilesInBatchAsyncV2(IFormFileCollection formFiles, string bucketName)
        {
            _logger.LogInformation("starting batch file uploads with {amount_of_files} files ====== V2", formFiles.Count);
            Stopwatch watch = Stopwatch.StartNew();

            foreach (IFormFile file in formFiles)
            {
                await UploadFileAsync(file, bucketName);
            }

            watch.Stop();
            _logger.LogInformation("uploaded batch in {time} seconds ===== V2", watch.ElapsedMilliseconds / 1000d);
        }

        public async Task UploadFilesInBatchAsyncV3(IFormFileCollection formFiles, string bucketName)
        {
            _logger.LogInformation("starting batch file uploads with {amount_of_files} files ==== V3", formFiles.Count);
            Stopwatch watch = Stopwatch.StartNew();

            Parallel.ForEach(formFiles,
                file => UploadFileAsync(file, bucketName));

            watch.Stop();
            _logger.LogInformation("uploaded batch in {time} seconds ==== V3", watch.ElapsedMilliseconds / 1000d);
        }
    }
}