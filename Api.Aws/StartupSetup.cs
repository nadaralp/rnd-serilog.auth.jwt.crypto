using Amazon;
using Amazon.S3;
using Api.Aws.Services.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.Core.Interfaces;

namespace Api.Aws
{
    public class StartupSetup : IStartupSetup
    {
        public void AddDependenciesForLayer(IServiceCollection services, IConfiguration configuration)
        {
            #region Aws Client Singleton Register

            string accessKey = configuration.GetSection("AWS_ACCOUNT:ACCESS_KEY")?.Value;
            string securityKey = configuration.GetSection("AWS_ACCOUNT:SECRET_KEY")?.Value;

            AmazonS3Client s3Client = new AmazonS3Client(accessKey, securityKey, RegionEndpoint.EUCentral1);
            services.AddSingleton<IAmazonS3>(s3Client);

            #endregion Aws Client Singleton Register

            services.AddSingleton<S3Service>();
        }
    }
}