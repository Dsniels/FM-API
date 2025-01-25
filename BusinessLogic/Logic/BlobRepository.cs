using System;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BusinessLogic;
using Core.Interfaces;
using Microsoft.AspNetCore.Internal;

namespace BusinessLogic.Logic;

public class BlobRepository : IBlobRepository
{

    private readonly BlobServiceClient _blobServiceClient;

    public BlobRepository(BlobServiceClient blobServiceClient){
        _blobServiceClient = blobServiceClient;
    }

    public async Task<Uri> UploadFile(string blobContainer, Stream content, string contentType, string fileName)
    {
        var containerCliente = GetContainerClient(blobContainer);
        var blobCliente = containerCliente.GetBlobClient(fileName);
        await blobCliente.UploadAsync(content, new BlobHttpHeaders{ContentType = contentType });
        return blobCliente.Uri;
        
    }


    private  BlobContainerClient GetContainerClient(string name){
        var containerCliente = _blobServiceClient.GetBlobContainerClient(name);
        containerCliente.CreateIfNotExistsAsync();
        return containerCliente;
    }

}
