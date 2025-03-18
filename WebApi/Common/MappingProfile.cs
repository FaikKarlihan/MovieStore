using System.Linq;
using AutoMapper;
using WebApi.Entities;
using WebApi.Applications.ActorOperations.Commands.UpdateActor;
using WebApi.Applications.ActorOperations.Queries.GetActorDetail;
using WebApi.Applications.ActorOperations.Queries.GetActors;
using WebApi.Applications.ActorOperations.Commands.CreateActor;
using WebApi.Applications.DirectorOperations.Commands.CreateDirector;
using WebApi.Applications.DirectorOperations.Commands.UpdateDirector;
using WebApi.Applications.DirectorOperations.Queries.GetDirectors;
using WebApi.Applications.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.Applications.MovieOperations.Commands.CreateMovie;
using WebApi.Applications.MovieOperations.Queries.GetMovies;
using WebApi.Applications.MovieOperations.Queries.GetMovieDetail;
using WebApi.Applications.MovieOperations.Commands.UpdateMovie;
using WebApi.Applications.OrderOperations.Queries.GetOrderDetail;
using WebApi.Applications.OrderOperations.Commands.UpdateOrder;
using WebApi.Applications.OrderOperations.Queries.GetOrders;
using WebApi.Applications.CustomerOperations.Commands.CreateCustomer;
using WebApi.Applications.CustomerOperations.Commands.UpdateCustomer;
using WebApi.Applications.CustomerOperations.Queries.GetCustomers;
using WebApi.Applications.CustomerOperations.Queries.GetCustomerDetail;
using WepApi.Applications.CustomerPanel.Queries.GetMyFavoriteGenres;
using WepApi.Applications.CustomerPanel.Queries.GetMyMovies;
using WebApi.Applications.DbLoggsOperations;

namespace WebApi.Common
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            //Actor
            CreateMap<CreateActorModel, Actor>()
            .ForMember(dest=> dest.Movies, opt=> opt.Ignore());

            CreateMap<UpdateActorModel, Actor>()
            .ForMember(dest=> dest.Movies, opt=> opt.Ignore());

            CreateMap<Actor, GetActorsViewModel>()
            .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies.Select(b => b.Name).ToList()));

            CreateMap<Actor, GetActorDetailModel>()
            .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies.Select(b => b.Name).ToList()));

            //Director
            CreateMap<CreateDirectorModel, Director>()
            .ForMember(dest=> dest.Movies, opt=> opt.Ignore());

            CreateMap<UpdateDirectorModel, Director>()
            .ForMember(dest=> dest.Movies, opt=> opt.Ignore());

            CreateMap<Director, GetDirectorsViewModel>()
            .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies.Select(b => b.Name).ToList()));

            CreateMap<Director, GetDirectorDetailViewModel>()
            .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies.Select(b => b.Name).ToList()));

            //Movie
             CreateMap<CreateMovieModel, Movie>()
            .ForMember(dest => dest.Director, opt => opt.Ignore())
            .ForMember(dest => dest.Genre, opt => opt.Ignore())    // Manually set in handle
            .ForMember(dest => dest.Actors, opt => opt.Ignore());

            CreateMap<Movie, GetMoviesViewModel>()
            .ForMember(dest=> dest.Director, opt=> opt.MapFrom(src=> src.Director.Name +" "+src.Director.Surname))
            .ForMember(dest=> dest.Genre, opt=> opt.MapFrom(src=> src.Genre.Name))
            .ForMember(dest=> dest.Actors, opt=> opt.MapFrom(src=> src.Actors.Select(b=> b.Name+" "+b.Surname).ToList()));

            CreateMap<Movie, GetMovieDetailViewModel>()
            .ForMember(dest=> dest.Director, opt=> opt.MapFrom(src=> src.Director.Name +" "+src.Director.Surname))
            .ForMember(dest=> dest.Genre, opt=> opt.MapFrom(src=> src.Genre.Name))
            .ForMember(dest=> dest.Actors, opt=> opt.MapFrom(src=> src.Actors.Select(b=> b.Name+" "+b.Surname).ToList()));

            CreateMap<UpdateMovieModel, Movie>()
            .ForMember(dest=> dest.Name, opt=> opt.Ignore())
            .ForMember(dest=> dest.Actors, opt=> opt.Ignore())
            .ForMember(dest=> dest.Director, opt=> opt.Ignore())
            .ForMember(dest=> dest.Genre, opt=> opt.Ignore())
            .ForMember(dest=> dest.Price, opt=> opt.Ignore());

            //Order
            CreateMap<Order, GetOrderDetailViewModel>()
            .ForMember(dest=> dest.Customer, opt=> opt.MapFrom(src=> src.Customer.Name+" "+src.Customer.Surname))
            .ForMember(dest=> dest.Movie, opt=> opt.MapFrom(src=> src.Movie.Name));
            CreateMap<Order, GetOrdersViewModel>()
            .ForMember(dest=> dest.Customer, opt=> opt.MapFrom(src=> src.Customer.Name+" "+src.Customer.Surname))
            .ForMember(dest=> dest.Movie, opt=> opt.MapFrom(src=> src.Movie.Name));

            //Customer
            CreateMap<CreateCustomerModel, Customer>();
            CreateMap<Customer, GetCustomersViewModel>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name+" "+src.Surname))
            .ForMember(dest => dest.PurchasedMovies, opt => opt.MapFrom(src => src.PurchasedMovies.Select(b=> b.Name).ToList()))
            .ForMember(dest => dest.FavoriteGenres, opt => opt.MapFrom(src => src.FavouriteGenres.Select(b=> b.Name).ToList()));
            CreateMap<Customer, GetCustomerDetailViewModel>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name+" "+src.Surname))
            .ForMember(dest => dest.PurchasedMovies, opt => opt.MapFrom(src => src.PurchasedMovies.Select(b=> b.Name).ToList()))
            .ForMember(dest => dest.FavoriteGenres, opt => opt.MapFrom(src => src.FavouriteGenres.Select(b=> b.Name).ToList()));
            CreateMap<Customer, FavoriteGenresViewModel>()
            .ForMember(dest => dest.FavoriteGenres, opt => opt.MapFrom(src => src.FavouriteGenres.Select(b=> b.Name).ToList()));
            CreateMap<Customer, MyMoviesViewModel>()
            .ForMember(dest => dest.PurchasedMovies, opt => opt.MapFrom(src => src.PurchasedMovies.Select(b=> b.Name).ToList()));

            //Loggs
            CreateMap<DbLoggs, GetDbLoggsViewModel>();
        }
    }
}