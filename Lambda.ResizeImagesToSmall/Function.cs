using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Lambda.ResizeImagesToSmall
{
    public class Function
    {
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        public async Task<string> FunctionHandler(S3Event s3Event, ILambdaContext context)
        {
            S3EventNotification.S3EventNotificationRecord record = s3Event.Records[0];
            S3EventNotification.S3Entity s3 = record.S3;

            string accessKey = Environment.GetEnvironmentVariable(Environment.GetEnvironmentVariable("AWS_ACCOUNT__ACCESS_KEY"));
            string securityKey = Environment.GetEnvironmentVariable(Environment.GetEnvironmentVariable("AWS_ACCOUNT__SECRET_KEY"));

            AmazonS3Client s3Client = new AmazonS3Client(accessKey, securityKey, RegionEndpoint.EUCentral1);

            GetObjectRequest getObjectRequest = new GetObjectRequest
            {
                BucketName = s3.Bucket.Name,
                Key = s3.Object.Key
            };

            GetObjectResponse objectReturned = await s3Client.GetObjectAsync(getObjectRequest);

            // uploading the file to different S3 bucket.
            PutObjectRequest putObjectRequest = new PutObjectRequest
            {
                BucketName = "test-lambda-1231",
                Key = s3.Object.Key
            };

            await s3Client.PutObjectAsync(putObjectRequest);

            //PutObjectRequest putObjectRequest = new PutObjectRequest
            //{
            //    BucketName = "test-lambda-1231",
            //    ContentBody = ""
            //};

            return "ok";
        }
    }
}