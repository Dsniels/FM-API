using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class BlobFile : Base
{
    [Required]
    public Uri Uri {get; set;}
    [Required]
    public string Tag {get; set;}
    [Required]
    public string Title {get; set;}

}
