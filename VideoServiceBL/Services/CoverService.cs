using System;
using System.IO;
using AutoMapper;
using Microsoft.Extensions.Logging;
using VideoServiceBL.DTOs;
using VideoServiceBL.Services.Interfaces;
using VideoServiceDAL.Models;
using VideoServiceDAL.Persistence;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using VideoServiceBL.Exceptions;

namespace VideoServiceBL.Services
{
    public class CoverService : BaseService<Cover, CoverDto>, ICoverService
    {
        private readonly int MAX_BYTES = 5120000;

        private readonly ImageFormat[] ACCEPTED_FILE_TYPES =
            {ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif, ImageFormat.Bmp};

        private readonly IMovieService _movieService;
        private readonly ILogger<BaseService<Cover, CoverDto>> _logger;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _host;
        private readonly PhotoSettings _photoSettings;

        public CoverService(VideoServiceDbContext context, IMovieService movieService,
            ILogger<BaseService<Cover, CoverDto>> logger, IMapper mapper, IHostingEnvironment host,
            IOptionsSnapshot<PhotoSettings> options) : base(context, logger, mapper)
        {
            _movieService = movieService;
            _logger = logger;
            _mapper = mapper;
            _host = host;
            _photoSettings = options.Value;
        }


//        private Image GetReducedImage(int width, int height, Stream resourceImage)
//        {
//            try
//            {
//                var image = Image.FromStream(resourceImage);
//                var thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);
//
//                return thumb;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError("Could`t create Thumbnail Image", ex);
//                throw new BusinessLogicException("Could`t create Thumbnail Image", ex);
//            }
//        }

        public async Task<CoverDto> SaveImage(long movieId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new BusinessLogicException("There is no photo.");
            }

            if (file.Length > _photoSettings.MaxBytes)
            {
                throw new BusinessLogicException("Photo cannot be bigger than 5120000 bytes.");
            }

            var filePathAndName = GetFilePathAndName(file);

            try
            {
                using (var stream = new FileStream(filePathAndName[0], FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    SetThumbnails(stream, filePathAndName[1]);
                }
            }
            catch (BusinessLogicException businessLogicException)
            {
                _logger.LogError(businessLogicException.Message, businessLogicException);
            }
            catch (ArgumentException)
            {
                throw new BusinessLogicException("Invalid file format.");
            }

            var movie = await _movieService.GetMovieWithGenreWithCoverByIdAsync(movieId);

            if (movie.Cover?.FileName != null)
            {
                var oldPhoto = movie.Cover.FileName;
                DeletePhotoIfExist(filePathAndName[1], oldPhoto);
            }

            var coverDto = new CoverDto
            {
                FileName = filePathAndName[1]
            };

            var addedCover = await AddAsync(coverDto);

            movie.CoverId = addedCover.Id;
            await _movieService.UpdateAsync(movie, movie.Id);

            return addedCover;
        }

        private void SetThumbnails(Stream stream, string fileName)
        {
            var originalImage = Image.FromStream(stream);

            if (_photoSettings.IsSupported(originalImage))
            {
                throw new ArgumentException();
            }

            CreateAndSaveThumbnail(originalImage, 500, fileName);
            originalImage.Dispose();
        }

//        private static void SetCorrectOrientation(Image image)
//        {
//            //property id = 274 describe EXIF orientation parameter
//            if (Array.IndexOf(image.PropertyIdList, 274) > -1)
//            {
//                var orientation = (int)image.GetPropertyItem(274).Value[0];
//                switch (orientation)
//                {
//                    case 1:
//                        // No rotation required.
//                        break;
//                    case 2:
//                        image.RotateFlip(RotateFlipType.RotateNoneFlipX);
//                        break;
//                    case 3:
//                        image.RotateFlip(RotateFlipType.Rotate180FlipNone);
//                        break;
//                    case 4:
//                        image.RotateFlip(RotateFlipType.Rotate180FlipX);
//                        break;
//                    case 5:
//                        image.RotateFlip(RotateFlipType.Rotate90FlipX);
//                        break;
//                    case 6:
//                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
//                        break;
//                    case 7:
//                        image.RotateFlip(RotateFlipType.Rotate270FlipX);
//                        break;
//                    case 8:
//                        image.RotateFlip(RotateFlipType.Rotate270FlipNone);
//                        break;
//                }
//                // This EXIF data is now invalid and should be removed.
//                image.RemovePropertyItem(274);
//            }
//        }

        private void CreateAndSaveThumbnail(Image image, int size, string fileName)
        {
            var thumbnailSize = GetThumbnailSize(image, size);

            using (var bitmap = ResizeImage(image, thumbnailSize.Width, thumbnailSize.Height))
            {
                bitmap.Save(fileName, ImageFormat.Jpeg);
            }
        }

        private static Size GetThumbnailSize(Image original, int size = 500)
        {
            var originalWidth = original.Width;
            var originalHeight = original.Height;

            double factor;
            if (originalWidth > originalHeight)
            {
                factor = (double) size / originalWidth;
            }
            else
            {
                factor = (double) size / originalHeight;
            }

            return new Size((int) (originalWidth * factor), (int) (originalHeight * factor));
        }

        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var result = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                graphics.DrawImage(image, 0, 0, result.Width, result.Height);
            }

            return result;
        }

        public void DeletePhotoIfExist(string serverPath, string photoPath)
        {
            if (photoPath == null)
                throw new ArgumentNullException(nameof(photoPath));

            if (File.Exists(Path.Combine(serverPath, photoPath)))
            {
                File.Delete(Path.Combine(serverPath, photoPath));
            }
        }

        private string[] GetFilePathAndName(IFormFile file)
        {
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            return new[] {filePath, fileName};
        }

    }
}