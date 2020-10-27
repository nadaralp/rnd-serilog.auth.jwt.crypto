using Amazon;
using Amazon.S3;
using Api.Aws.Services.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Aws
{
    public class StartupSetup : IStartupSetup
    {
        public void AddDependenciesForLayer(IServiceCollection services, IConfiguration configuration)
        {
            #region Aws Client Singleton Register


            //string accessKey = configuration.GetSection("AwsCredentials:User:AccessKeyId")?.Value;
            //string securityKey = configuration.GetSection("AwsCredentials:User:SecretKey")?.Value;

            //AmazonS3Client s3Client = new AmazonS3Client(accessKey, securityKey, RegionEndpoint.EUCentral1);
            //services.AddSingleton<IAmazonS3>(s3Client);
            #endregion

            //services.AddSingleton<IS3Service, S3Service>();
        }
    }
}
