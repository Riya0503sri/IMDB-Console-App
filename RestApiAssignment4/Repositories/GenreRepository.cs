using Microsoft.Extensions.Options;
using RestApiAssignment4.Models.Entities;
using RestApiAssignment4.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.SqlClient;
using Dapper;

namespace RestApiAssignment4.Repositories
{
	public class GenreRepository : BaseRepository<Genre>,IGenreRepository
	{
		private readonly string _connectionString;
		public GenreRepository(IOptions<ConnectionString> connectionString)
			: base(connectionString.Value.IMDBDB)
		{
			_connectionString = connectionString.Value.IMDBDB;
		}
		public List<Genre> GetByMovieId(int id)
		{
			const string query = @"
SELECT g.Id
	,Name
FROM Foundation.Movie_Genre mg
INNER JOIN Foundation.Genres g ON mg.GenreId = g.Id
WHERE MovieId = @id";
			using var connection = new SqlConnection(_connectionString);
			return connection.Query<Genre>(query, new { Id = id }).ToList();
		}
		public IEnumerable<Genre> Get()
		{
			const string query = @"
SELECT [Id]
	,[Name]
FROM [Foundation].[Genres]";
			return GetAll(query).ToList();
		}

		public Genre Get(int id)
		{
			const string query = @"
SELECT [Id]
	,[Name]
FROM [Foundation].[Genres]
WHERE Id = @id";
			return Get(query, new { Id = id });
		}

		public int Create(Genre genre)
		{
			const string query = @"
INSERT INTO [Foundation].[Genres] 
(Name)
VALUES (@Name)

SELECT [Id],
[Name]
from [Foundation].[Genres](NOLOCK)
WHERE Id = SCOPE_IDENTITY()";
			return Create<int>(query,genre);
		}

		public void Update(Genre genre)
		{
			const string query = @"
UPDATE [Foundation].[Genres]
SET Name = @Name
WHERE Id = @id";
			UpdateOrDelete(query, new { genre.Id, genre.Name });
			

		}

		public void Delete(int id)
		{
			const string query = @"
DELETE
FROM [Foundation].[Movie_Genre]
WHERE GenreId = @id;

DELETE
FROM [Foundation].[Genres]
WHERE Id = @id";
			UpdateOrDelete(query, new { Id = id });
			
		}
	}
}
