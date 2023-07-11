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
	public class ActorRepository : BaseRepository<Actor>,IActorRepository
	{
		private readonly string _connectionString;
		public ActorRepository(IOptions<ConnectionString> connectionString)
			: base(connectionString.Value.IMDBDB)
		{
			_connectionString = connectionString.Value.IMDBDB;
		}
		public List<Actor> GetByMovieId(int id)
		{
			const string query = @"
SELECT a.Id
	,Name
	,Gender
	,DOB
	,Bio
FROM Foundation.Movie_Actor ma
INNER JOIN Foundation.Actors a ON ma.ActorId = a.id
WHERE MovieId = @id";
			using var connection = new SqlConnection(_connectionString);
			return connection.Query<Actor>(query, new { Id = id }).ToList();
		}
		public IEnumerable<Actor> Get()
		{
			const string query = @"
SELECT [Id]
	,[Name]
	,[Bio]
	,[Dob]
	,[Gender]
FROM [Foundation].[Actors]";
			return GetAll(query);
		}

		public Actor Get(int id)
		{
			const string query = @"
SELECT [Id]
	,[Name]
	,[Bio]
	,[Dob]
	,[Gender]
FROM [Foundation].[Actors]
where Id=@Id";
			return Get(query, new { Id = id });
		}

		public int Create(Actor actor)
		{
			const string query = @"
INSERT INTO [Foundation].[Actors] (
	 [Name]
	,[Bio]
	,[Dob]
	,[Gender]
	)
VALUES (
	 @Name
	,@Bio
	,@Dob
	,@Gender
	)
SELECT [Id]
,[Name]
,[Bio]
,[Gender]
,[Dob]
			FROM[Foundation].[Actors](NOLOCK)
WHERE Id = SCOPE_IDENTITY()";
			return Create<int>(query, actor);
			
		}

		public void Update(Actor actor)
		{
			const string query = @"
UPDATE [Foundation].[Actors]
SET [Name] = @Name
	,[Bio] = @Bio
	,[Dob] = @Dob
	,[Gender] = @Gender
WHERE [Id] = @Id";
			UpdateOrDelete(query, new { actor.Id, actor.Name, actor.Bio, actor.Dob, actor.Gender });
			
		}

		public void Delete(int id)
		{
			const string query = @"
DELETE
FROM [Foundation].[Movie_Actor]
WHERE [ActorId] = @Id;

DELETE
FROM [Foundation].[Actors]
WHERE [Id] = @Id";
			UpdateOrDelete(query, new { id });
			
		}
	}
}
