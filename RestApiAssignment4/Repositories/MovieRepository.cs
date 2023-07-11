using Dapper;
using Microsoft.Extensions.Options;
using RestApiAssignment4.Models.Entities;
using RestApiAssignment4.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System;

namespace RestApiAssignment4.Repositories
{
	public class MovieRepository: BaseRepository<Movie>, IMovieRepository
	{
		private readonly string _connectionString;
		public MovieRepository(IOptions<ConnectionString> connectionString)
			: base(connectionString.Value.IMDBDB)
		{
			_connectionString = connectionString.Value.IMDBDB;
		}

		public IEnumerable<Movie> Get()
		{
			const string query = @"
SELECT Id
	,Name
	,YearOfRelease
	,Plot
	,ProducerId
	,PosterURL
FROM Foundation.Movies";
			return GetAll(query).ToList();
		}

		public Movie Get(int id)
		{
			const string query = @"
SELECT Id
	,Name
	,YearOfRelease
	,Plot
	,ProducerId
	,PosterURL
FROM Foundation.Movies
WHERE Id = @id";
			return Get(query, new { Id = id });
		}
		public int Create(Movie movie, List<int> actorIds, List<int> genreIds)
		{
			using var connection = new SqlConnection(_connectionString);
			const string procedure = "usp_insert_movie";

			var parameters = new DynamicParameters();
			parameters.Add("@Name", movie.Name, DbType.String);
			parameters.Add("@Plot", movie.Plot, DbType.String);
			parameters.Add("@YearOfRelease", movie.YearOfRelease, DbType.Int32);
			parameters.Add("@PosterURL", movie.PosterURL, DbType.String);
			parameters.Add("@ProducerId", movie.ProducerId, DbType.Int32);
			parameters.Add("@ActorIds", string.Join(' ', actorIds), DbType.String);
			parameters.Add("@GenreIds", string.Join(' ', genreIds), DbType.String);
			parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

			connection.Query(procedure, parameters, commandType: CommandType.StoredProcedure);
			return parameters.Get<int>("@Id");
		}
		public void Update(Movie movie, List<int> actorIds, List<int> genreIds)
		{
			using var connection = new SqlConnection(_connectionString);
			const string procedure = "usp_update_movie";
			var parameters = new
			{
				movie.Id,
				movie.Name,
				movie.YearOfRelease,
				movie.Plot,
				movie.PosterURL,
				ActorIds = string.Join(' ', actorIds),
				GenreIds = string.Join(' ', genreIds),
				movie.ProducerId
			};
			connection.Execute(procedure, parameters, commandType: CommandType.StoredProcedure);
			
		}
		
		public void Delete(int id)
		{
			const string query = @"
DELETE
FROM Foundation.Movie_Actor
WHERE MovieId = @Id;

DELETE
FROM Foundation.Movie_Genre
WHERE MovieId = @Id;

DELETE
FROM Foundation.Reviews
WHERE MovieId = @Id;

DELETE
FROM Foundation.Movies
WHERE Id = @Id";

			UpdateOrDelete(query, new { Id = id });

			
		}
	}
}

