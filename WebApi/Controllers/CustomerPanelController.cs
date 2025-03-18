using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.CustomerPanel.Commands.BuyMovie;
using WebApi.Applications.CustomerPanel.Commands.AddFavoriteGenre;
using WebApi.DBOperations;
using WepApi.Applications.CustomerPanel.Queries.GetMyFavoriteGenres;
using WepApi.Applications.CustomerPanel.Queries.GetMyMovies;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]s")]
    public class CustomerPanelController: ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CustomerPanelController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{email}/favorite-genres")]
        public ActionResult GetMyFavoriteGenres(string email)
        {
            GetMyFavoriteGenresQuery query = new GetMyFavoriteGenresQuery(_context, _mapper);
            query.CustomerEmail = email;

            GetMyFavoriteGenresQueryValidator validator = new GetMyFavoriteGenresQueryValidator();
            validator.ValidateAndThrow(query);

            var obj = query.Handle();
            return Ok(obj);
        }   
        [HttpGet("{email}/my-movies")]
        public ActionResult GetMyMovies(string email)
        {
            GetMyMoviesQuery query = new GetMyMoviesQuery(_context, _mapper);
            query.CustomerEmail = email;

            GetMyMoviesQueryValidator validator = new GetMyMoviesQueryValidator();
            validator.ValidateAndThrow(query);

            var obj = query.Handle();
            return Ok(obj);
        }    

        [HttpPut("{email}/add-favorite-genre")] 
        public IActionResult AddFavGenre(string email, [FromBody] AddFavoriteGenreModel genre)
        {
            AddFavoriteGenreCommand command = new AddFavoriteGenreCommand(_context);
            command.Email = email;
            command.Model = genre;

            AddFavoriteGenreCommandValidator validator = new AddFavoriteGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpPost("{email}/buy-movie")] 
        public IActionResult BuyMovie(string email, [FromBody] BuyMovieModel movie)
        {
            BuyMovieCommand command = new BuyMovieCommand(_context);
            command.Email = email;
            command.Model = movie;

            BuyMovieCommandValidator validator = new BuyMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}