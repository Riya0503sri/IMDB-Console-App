using Microsoft.Extensions.Options;
using RestApiAssignment4.Models.Entities;
using RestApiAssignment4.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace RestApiAssignment4.Repositories
{
	public class ProducerRepository: BaseRepository<Producer>, IProducerRepository
	{
		public ProducerRepository(IOptions<ConnectionString> connectionString)
			: base(connectionString.Value.IMDBDB)
		{
		}

		public IEnumerable<Producer> Get()
		{
			const string query = @"
SELECT [Id]
	,[Name]
	,[Bio]
	,[Dob]
	,[Gender]
FROM [Foundation].[Producers]";
			return GetAll(query).ToList();
		}

		public Producer Get(int id)
		{
			const string query = @"
SELECT [Id]
	,[Name]
	,[Bio]
	,[Dob]
	,[Gender]
FROM [Foundation].[Producers]
where Id=@Id";
			return Get(query, new { Id = id });
		}

		public int Create(Producer producer)
		{
			const string query = @"
INSERT INTO [Foundation].[Producers] (
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
			FROM[Foundation].[Producers](NOLOCK)
WHERE Id = SCOPE_IDENTITY()";
			return Create<int>(query, producer);
			
		}

		public void Update(Producer producer)
		{
			const string query = @"
UPDATE [Foundation].[Producers]
SET [Name] = @Name
	,[Bio] = @Bio
	,[Dob] = @Dob
	,[Gender] = @Gender
WHERE [Id] = @Id";
			UpdateOrDelete(query, new { producer.Id, producer.Name, producer.Bio, producer.Dob, producer.Gender });
			
		}

		public void Delete(int id)
		{
			const string query = @"
DELETE 
FROM Foundation.Movie_Actor
WHERE MovieId IN (
			SELECT id
			FROM Foundation.Movies
			WHERE ProducerId = @Id
			);

DELETE
FROM Foundation.Movie_Genre
WHERE MovieId IN (
			SELECT id
			FROM Foundation.Movies
			WHERE ProducerId = @Id
			);

DELETE
FROM [Foundation].[Movies]
WHERE [ProducerId] = @Id;
			
DELETE
FROM [Foundation].[Producers]
WHERE [Id] = @Id";
			UpdateOrDelete(query, new { id });
		}
	}
}
