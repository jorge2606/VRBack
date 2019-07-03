using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.FileProviders;
using Service.Common.ServiceResult;
using VR.Web.Helpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using VR.Dto;
using VR.Service.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
using SixLabors.ImageSharp.Formats.Png;
using Image = SixLabors.ImageSharp.Image;

namespace VR.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class FileController : ControllerBase
    {
        private IFileService _fileService;
        private IFileProvider _fileProvider;

        public FileController(IFileService fileService, IHostingEnvironment env)
        {
            _fileService = fileService;
            _fileProvider = env.ContentRootFileProvider;
        }

        [HttpPost("UpdateMyImage")]
        [Authorize]
        public async Task<IActionResult> UpdateMyImage(UpdateMyImageDto fileCreateDto)
        {
            fileCreateDto.UserId = GetIdUser();

           var result = await _fileService.UpdateMyImage(fileCreateDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response.File);
        }

        [HttpGet("{userId}")]
        [Authorize]
        public IActionResult FileById(Guid userId)
        {
           var result = _fileService.GetByIdFile(userId);
            if (!result.IsSuccess)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("removePhoto/{userId}/{width}/{height}")]
        [Authorize]
        public IActionResult RemoveFile(Guid userId, int width, int height)
        {
            var result = _fileService.RemoveProfilePhoto(userId, width, height);
            if (!result.IsSuccess)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("removeHolographSign/{userId}")]
        [Authorize]
        public IActionResult RemoveHolographSign(Guid userId)
        {
            var result = _fileService.RemoveHolographSign(userId);
            if (!result.IsSuccess)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("{userId}/{width}/{height}")]
        [Authorize]
        public IActionResult ResizeImage(Guid userId, int width, int height)
        {
            if (width < 0 || height < 0) { return BadRequest();}

            var result = _fileService.GetCompletePath("",userId, width,height);
            return Ok(result);
            
        }

        [HttpPost("HolographSignUpdate")]
        [Authorize]
        public IActionResult HolographSignUpdate(UpdateMyImageDto image)
        {
            if (image.UserId == null)
            {
                image.UserId = GetIdUser();
            }
            var result = _fileService.HolographSignUpdate(image);

            if (!result.Result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Result.Response);
        }

        public Guid GetIdUser()
        {
            var currentUser = Helpers.HttpContext.Current.User.Claims;
            var result = Guid.Empty;
            foreach (var i in currentUser)
            {
                if (i.Type.Equals("NameIdentifier"))
                {
                    result = Guid.Parse(i.Value);
                }
            }

            return result;
        }

        [HttpGet("HolographSign/{userId}/{width}/{height}")]
        [Authorize]
        public IActionResult HolographSign(Guid userId, int width, int height)
        {
            if (width < 0 || height < 0) { return BadRequest(); }
            var result = _fileService.GetCompletePathHolographSign(userId);

            FileInfo fileInfo = new FileInfo(result.Response.Paths);

            var outputStream = new MemoryStream();

            if (!fileInfo.Exists) { return NotFound(); }

            using (var image = Image.Load(fileInfo.FullName))
            {
                image.Mutate(x => x
                    .Resize(width, height));

                image.SaveAsJpeg(outputStream);

                outputStream.Seek(0, SeekOrigin.Begin);

                return File(outputStream, "image/jpeg");
            }

        }


        [HttpGet("HolographSignUrl/{userId}/{width}/{height}")]
        [Authorize]
        public IActionResult HolographSignUrl(Guid userId, int width, int height)
        {
            if (width < 0 || height < 0) { return BadRequest(); }
            var result = _fileService.GetCompletePathHolographSign(userId);

            FileInfo fileInfo = new FileInfo(result.Response.Paths);

            if (!fileInfo.Exists) { return NotFound(); }

            var resultUrl = Convert.ToBase64String(System.IO.File.ReadAllBytes(result.Response.Paths));
            ServiceResult<String> resultUrlEnd = new ServiceResult<String>(resultUrl);
            return Ok(resultUrlEnd);

        }


        [HttpGet("ExpenditureRefund/{expId}/{width}/{height}")]
        [Authorize]
        public IActionResult ExpenditureRefund(Guid expId, int width, int height)
        {
            if (width < 0 || height < 0) { return BadRequest(); }
            var result = _fileService.GetUrlExpenditureRefundFile(expId);

            if (result.Response == null || result.Response.Count() == 0)
            {
                result.AddError("0","No tiene image");
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("ExpenditureRefundUrl/{expId}/{width}/{height}")]
        [Authorize]
        public IActionResult ExpenditureRefundUrl(Guid expId, int width, int height)
        {
            if (width < 0 || height < 0) { return BadRequest(); }
            var result = _fileService.GetUrlExpenditureRefundFile(expId);

            if (result.Response == null)
            {
                result.AddError("0", "No tiene image");
                return BadRequest(result);
            }
            
            return Ok(result);


        }
    }

}
