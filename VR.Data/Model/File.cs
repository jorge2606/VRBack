using System;
using SixLabors.ImageSharp;
using System.Collections.Generic;
using Service.Common.ServiceResult;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;

namespace VR.Data.Model
{
    public class File
    {
        public Guid Id { set; get; }
        public string Path { set; get; }
        public  Guid UserId { set; get; }
        public DateTime UploadTime { set; get; }
        public DateTime DeleteTime { set; get; }
        public string MimeType { set; get; }
        public byte[] Image { set; get; }
        [ForeignKey("Expenditure")]
        public Guid ExpenditureId { set; get; }
        public string Name { set; get; }
        public long Size { set; get; }
        public string LastModified { set; get; }
        public DateTime LastModifiedDate { set; get; }
        public Expenditure Expenditure { set; get; }
    }
}
