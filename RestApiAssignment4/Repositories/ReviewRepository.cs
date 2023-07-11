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
	public class ReviewRepository: BaseRepository<Review>, IReviewRepository
	{
		private readonly string _connectionString;
		public ReviewRepository(IOptions<ConnectionString> connectionString)
			: base(connectionString.Value.IMDBDB)
		{
			_connectionString = connectionString.Value.IMDBDB;
		}
		public IEnumerable<Review> Get(int movieId)
		{
			const string query = @"
SELECT Id
	,Message
FROM Foundation.Reviews
WHERE MovieId = @movieId";
			using var connection = new SqlConnection(_connectionString);
			return connection.Query<Review>(query, new { MovieId = movieId }).ToList();
		}

		public Review Get(int id,int movieId)
		{
			const string query = @"
SELECT Id
	,Message
FROM Foundation.Reviews
WHERE Id = @id and MovieId=@movieId";
			return Get(query, new { Id = id,MovieId=movieId });
		}

		public int Create(Review review)
		{
			const string query = @"
INSERT INTO Foundation.Reviews (
	MovieId
	,Message
	)

VALUES (
	@MovieId
	,@Message
	)
SELECT [Id],
[Message]
from [Foundation].[Reviews]
WHERE Id = SCOPE_IDENTITY()";
			return Create<int>(query, review);
			
		}

		public void Update(Review review)
		{
			const string query = @"
UPDATE Foundation.Reviews
SET MovieId = @MovieId
	,Message = @Message
WHERE Id = @id";
			UpdateOrDelete(query, new { review.Id, review.MovieId, review.Message });
		}

		public void Delete(int id)
		{
			const string query = @"
DELETE
FROM Foundation.Reviews
WHERE Id = @id";
			UpdateOrDelete(query, new { Id = id });
		}
	}
}
