using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VR.Dto
{
    public class FileCreateDto : FileDeleteDto
    {
        public Guid Id { set; get; }
        public string Path { set; get; }
        public Guid UserId { set; get; }
        public IFormFile File { set; get; }
        public string MimeType { set; get; }
        public DateTime UploadTime { set; get; }
        public byte[] Image { set; get; }
        public Guid ExpenditureId { set; get; }

        public FileCreateDto()
        {
            UploadTime = DateTime.Now;
            DeleteTime = DateTime.MinValue;
        }

    }

    public class FileCreateFromRefundDto : FileDeleteDto
    {
        public Guid Id { set; get; }
        public string Path { set; get; }
        public Guid UserId { set; get; }
        public string MimeType { set; get; }
        public DateTime UploadTime { set; get; }
        public string Image { set; get; }
        public Guid ExpenditureId { set; get; }

        public FileCreateFromRefundDto()
        {
            UploadTime = DateTime.Now;
            DeleteTime = DateTime.MinValue;
        }
    }


    public class UpdateMyImageDto : FileCreateDto
    {
        public Boolean IsDeleted { set; get; }
    }

    public class FileDeleteDto
    {
        public DateTime DeleteTime { set; get; }
    }

    public class FileByIdDto
    {
        public string Paths { set; get; }
        public Boolean IsDeleted { set; get; }
    }

    public class ResponseStreamImage
    {
        public Stream File { set; get; }
    }


}
