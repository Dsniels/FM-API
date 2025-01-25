using System;
using Azure.Storage.Blobs;

namespace Core.Interfaces;

public interface IBlobRepository
{

    public Task<Uri> UploadFile(string blobContainer, Stream content, string contentType, string fileName);


}
