using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.Options;
using VideoService.VideoServiceBL.Services;
using VideoServiceBL.Services.Interfaces;
using VideoServiceDAL.Persistence;

namespace VideoServiceBL.Services
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly VideoServiceDbContext _context;
        private readonly IMapper _mapper;
        private IGenreService _genreService;
        private IMovieService _movieService;
        private ICryptService _cryptService;
        private IUserService _userService;

        public IOptions<AuthSettings> Settings { get; set; }

        public UnitOfWorkService(VideoServiceDbContext context, IMapper mapper, IOptions<AuthSettings> settings)
        {
            _context = context;
            _mapper = mapper;
            Settings = settings;
        }

        public IGenreService GenreService => _genreService ?? 
                                             (_genreService = new GenreService(_context, _mapper));

        public IMovieService MovieService => _movieService ??
                                             (_movieService = new MovieService(_context, _mapper));


        public IUserService UserService => _userService ??
                                             (_userService = new UserService(_context, _mapper, CryptService, Settings));

        public ICryptService CryptService => _cryptService ??
                                             (_cryptService = new CryptService());

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}