using mastering_.NET_API.Dtos;
using mastering_.NET_API.Models;

namespace mastering_.NET_API.Mappers
{
    public static class MovieMappers
    {
        public static Movie getMovieFromAddMovieDto(AddMovieDto movie)
        {
            Movie Newmovie = new Movie();
            Newmovie.Titre = movie.Titre;
            Newmovie.Description = movie.Description;
            Newmovie.DateDeSortie = movie.DateDeSortie;
            Newmovie.Duree = movie.Duree;
            foreach (var langue in movie.Langues)
            {
                Newmovie.Langue |= langue;
            }
            Newmovie.Pays = movie.Pays;
            Newmovie.Url = movie.Url;
            Newmovie.Realisateur = movie.Realisateur;
            Newmovie.GenreId = movie.GenreId;

            return Newmovie;

        }

        public static Movie getMovieFromUpdateMovieDto(Movie Newmovie, UpdateMovieDto movie)
        {
            Newmovie.Titre = movie.Titre;
            Newmovie.Description = movie.Description;
            Newmovie.DateDeSortie = movie.DateDeSortie;
            Newmovie.Duree = movie.Duree;
            foreach (var langue in movie.Langues)
            {
                Newmovie.Langue |= langue;
            }
            Newmovie.Pays = movie.Pays;
            Newmovie.Url = movie.Url;
            Newmovie.Realisateur = movie.Realisateur;
            Newmovie.GenreId = movie.GenreId;

            return Newmovie;

        }
    }
}
