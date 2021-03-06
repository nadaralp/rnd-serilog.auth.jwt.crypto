﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Aws.Services.S3
{
    public interface IS3Service
    {
        Task UploadFileAsync(IFormFile formFile, string bucketName);

        Task UploadFilesInBatchAsync(IFormFileCollection formFiles, string bucketName);
    }
}