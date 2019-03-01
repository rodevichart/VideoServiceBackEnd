using System;

namespace VideoServiceBL.Services.Interfaces
{
    public interface IUnitOfWorkService: IDisposable
    {
        IGenreService GenreService { get; }
        IMovieService MovieService { get; }
        IUserService UserService { get; }
        ICryptService CryptService { get; }
    }
}