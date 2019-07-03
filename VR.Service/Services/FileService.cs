using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VR.Dto;
using Service.Common.ServiceResult;
using VR.Data;
using VR.Service.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace VR.Service.Services
{
    public class FileService : IFileService
    {

        public static string StaticFilesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");

        private DataContext _contextFile;
        private IMapper _mapper;

        public FileService(DataContext contextFile, IMapper mapper)
        {
            _contextFile = contextFile;
            _mapper = mapper;
        }

        public async Task<ServiceResult<UpdateMyImageDto>> UpdateMyImage(UpdateMyImageDto model)
        {

            var path = Path.Combine(StaticFilesDirectory, "Profile", model.UserId.ToString());

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var newDirectory = Path.Combine(StaticFilesDirectory, "Profile", model.UserId.ToString());
            var files = Directory.EnumerateFiles(path, "*.*" );

            
            if (files.Count() > 0)
            {
                foreach (var f in files)
                {
                    System.IO.File.Delete(f); 
                }
            }

            var file = model.File;

            using (var stream = file.OpenReadStream())
            {
                var filePath = Path.Combine(newDirectory, file.FileName);

                model.Path = filePath;
                model.MimeType = file.ContentType;

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    
                    fileStream.Dispose();
                }
                stream.Dispose();
            }

            _contextFile.Files.Add(_mapper.Map<VR.Data.Model.File>(
                new VR.Data.Model.File()
                {
                    Id = new Guid(),
                    Size = model.File.Length,
                    Name = model.File.FileName,
                    UploadTime = new DateTime(),
                    LastModifiedDate = new DateTime(),
                    LastModified = new DateTime().ToString(),
                    UserId = model.UserId,
                    MimeType = model.File.ContentType,
                    Path = model.Path
                })
            );
            _contextFile.SaveChanges();
            

            return new ServiceResult<UpdateMyImageDto>(model);
        }

        public ServiceResult<FileByIdDto> GetByIdFile(Guid userId)
        {
            var path = Path.Combine(StaticFilesDirectory,"Profile",userId.ToString());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                System.IO.File.Copy(Path.Combine(StaticFilesDirectory, "user.png"), Path.Combine(path, "user.png"));
            }
            var files = Directory.EnumerateFiles(path, "*.*");

            FileInfo file1;

            FileByIdDto imagesPath = new FileByIdDto();

            if (files.Count() == 1)
            {
                string pathFile = "";

                foreach (var i in files)
                {
                    file1 = new FileInfo(i.ToString());
                    pathFile = file1.Name;
                }

                imagesPath.Paths = pathFile;
            }

            return new ServiceResult<FileByIdDto>(imagesPath);
        }

        public ServiceResult<string> GetCompletePath(string p_path, Guid userId, int width, int height)
        {
            var path = "";
            if (string.IsNullOrEmpty(p_path))
            {
                path = Path.Combine(StaticFilesDirectory, "Profile", userId.ToString());
            }
            else
            {
                path = p_path;
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                System.IO.File.Copy(Path.Combine(StaticFilesDirectory, "user.png"), Path.Combine(path, "user.png"));
            }

            FileByIdDto imagesPath = new FileByIdDto();

            var files = Directory.EnumerateFiles(path, "*.*");

            if (files.Count() == 1)
            {
                string pathFile = "";

                foreach (var i in files)
                {
                    pathFile = i;
                }

                imagesPath.Paths = pathFile;
            }

            FileInfo fileInfo = new FileInfo(imagesPath.Paths);

            var outputStream = new MemoryStream();

            if (!fileInfo.Exists)
            {
                return new ServiceResult<string>();
            }

            using (var image = Image.Load(fileInfo.FullName))
            {
                image.Mutate(x => x
                    .Resize(width, height));

                image.SaveAsPng(outputStream);

                outputStream.Seek(0, SeekOrigin.Begin);

                byte[] bytes = outputStream.ToArray();

                var extentionImg = "";
                switch (fileInfo.Extension)
                {
                    case ".jpg":
                        extentionImg = "data:image/jpg;base64,";
                        break;
                    case ".png":
                        extentionImg = "data:image/png;base64,";
                        break;
                    default:
                        extentionImg = "data:image/png;base64,";
                        break;
                }

                var resultUrl = extentionImg + Convert.ToBase64String(bytes);
                ServiceResult<string> resultImg = new ServiceResult<string>(resultUrl);

                return resultImg;
            }
        }

        public ServiceResult<FileByIdDto> RemoveProfilePhoto(Guid userId, int width, int height)
        {
            var path = Path.Combine(StaticFilesDirectory, "Profile", userId.ToString());

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                System.IO.File.Copy(Path.Combine(StaticFilesDirectory, "user.png"), Path.Combine(path, "user.png"));
            }
            DirectoryInfo files = new DirectoryInfo(path);

            var fileSystem = files.EnumerateFileSystemInfos();

            foreach (var i in fileSystem)
            {
                File.Delete(Path.Combine(path, i.Name));
            }

            File.Copy(Path.Combine(StaticFilesDirectory, "user.png"), Path.Combine(path, "user.png"));
             
            return new ServiceResult<FileByIdDto>(new FileByIdDto()
            {
                Paths = GetCompletePath(path, userId, width, height).Response
            });
        }


        public ServiceResult<FileByIdDto> GetCompletePathHolographSign(Guid userId)
        {
            var path = Path.Combine(StaticFilesDirectory, "HolographsSigns","Sign_"+ userId.ToString());

            FileByIdDto imagesPath = new FileByIdDto
            {
                IsDeleted = false
            };

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                System.IO.File.Copy(Path.Combine(StaticFilesDirectory, "sign.png"), Path.Combine(path, "sign.png"));
                imagesPath.IsDeleted = true;
            }
            
            var files = Directory.EnumerateFiles(path, "*.*");

            if (files.Count() == 1)
            {
                string pathFile = "";

                foreach (var i in files)
                {
                    pathFile = i;
                }

                if (pathFile.Equals(Path.Combine(path, "sign.png")))
                {
                    imagesPath.IsDeleted = true;
                }
                imagesPath.Paths = pathFile;
            }

            return new ServiceResult<FileByIdDto>(imagesPath);
        }


        public ServiceResult<string> UrlSignHolograph(Guid supervisorid)
        {
            var notif = new ServiceResult<string>();
            var result = GetCompletePathHolographSign(supervisorid);

            FileInfo fileInfo = new FileInfo(result.Response.Paths);

            if (!fileInfo.Exists)
            {
                notif.AddError("Error","El archivo no existe.");
                return notif;
            }

            var resultUrl = Convert.ToBase64String(System.IO.File.ReadAllBytes(result.Response.Paths));
            return new ServiceResult<String>(resultUrl);
        }



        public ServiceResult<List<ImageDto>> GetUrlExpenditureRefundFile(Guid expenditureId)
        {
            var expenditure = _contextFile.Files.Where(x => x.ExpenditureId == expenditureId).ToList();
            List<ImageDto> ImageDtoList = new List<ImageDto>();

            if (expenditure.Count() == 0)
            {
                return new ServiceResult<List<ImageDto>>(ImageDtoList);
            }

            foreach (var exp in expenditure)
            {
                ImageDtoList.Add(new ImageDto()
                {
                    Name = exp.Name,
                    Size = exp.Size,
                    Type = exp.MimeType,
                    UrlImages = "data:image/jpg;base64," + Convert.ToBase64String(exp.Image),
                    LastModifiedDate = exp.LastModifiedDate
                });
            }

            return new ServiceResult<List<ImageDto>>(ImageDtoList);

        }

        public ServiceResult<FileCreateFromRefundDto> AddExpenditureRefundImage(FileCreateFromRefundDto image)
        {
            string base64 = image.Image.Substring(image.Image.IndexOf(',') + 1);
            byte[] data = Convert.FromBase64String(base64);
            Data.Model.File newFile = new Data.Model.File()
            {
                Id = new Guid(),
                UserId = image.UserId,
                Path = image.Path,
                ExpenditureId = image.ExpenditureId,
                Image = data,
                MimeType = image.MimeType
            };
            
            _contextFile.Files.Add(newFile);
            _contextFile.SaveChanges();
            return new ServiceResult<FileCreateFromRefundDto>(
                _mapper.Map<FileCreateFromRefundDto>(image)
                );
        }


        public async Task<ServiceResult<UpdateMyImageDto>> HolographSignUpdate(UpdateMyImageDto model)
        {
            var path = Path.Combine(StaticFilesDirectory, "HolographsSigns", "Sign_"+model.UserId.ToString());
            model.IsDeleted = false;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var newDirectory = Path.Combine(StaticFilesDirectory, "HolographsSigns", "Sign_"+model.UserId.ToString());
            var files = Directory.EnumerateFiles(path, "*.*");

            if (files.Count() > 0)
            {
                foreach (var f in files)
                {
                    System.IO.File.Delete(f);
                }
            }

            var file = model.File;

            using (var stream = file.OpenReadStream())
            {
                var filePath = Path.Combine(newDirectory, file.FileName);

                model.Path = filePath;
                model.MimeType = file.ContentType;

                

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);

                    fileStream.Dispose();
                }
                stream.Dispose();
            }
            
            _contextFile.Files.Add(_mapper.Map<VR.Data.Model.File>(
                new VR.Data.Model.File()
                    {
                        Id = new Guid(),
                        Size = model.File.Length,
                        Name = model.File.FileName,
                        UploadTime = new DateTime(),
                        LastModifiedDate = new DateTime(),
                        LastModified = new DateTime().ToString(),
                        UserId = model.UserId,
                        MimeType = model.File.ContentType,
                        Path = model.Path
                     }
                ));
            _contextFile.SaveChanges();


            return new ServiceResult<UpdateMyImageDto>(model);
        }

        public ServiceResult<FileByIdDto> RemoveHolographSign(Guid userId)
        {
            var path = Path.Combine(StaticFilesDirectory, "HolographsSigns", "Sign_" + userId.ToString());
            FileByIdDto pathByIdDto = new FileByIdDto();
            pathByIdDto.IsDeleted = true;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                System.IO.File.Copy(Path.Combine(StaticFilesDirectory, "sign.png"), Path.Combine(path, "sign.png"));
            }
            DirectoryInfo files = new DirectoryInfo(path);

            var fileSystem = files.EnumerateFileSystemInfos();

            foreach (var i in fileSystem)
            {
                File.Delete(Path.Combine(path, i.Name));
            }

            File.Copy(Path.Combine(StaticFilesDirectory, "sign.png"), Path.Combine(path, "sign.png"));

            pathByIdDto.Paths = path;
            return new ServiceResult<FileByIdDto>(pathByIdDto);
        }

    }
}
