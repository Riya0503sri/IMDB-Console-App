Feature: Movie

This feature performs CRUD operations on Movie class

Scenario Outline: Create a Movie
	Given I am a client
	When I am making a post request to '/movies' with the following Data '<request>'
	Then response code must be <statusCode>
	Examples:  
	| request                                                                                                                             | statusCode |
	| {"Name":"TestName","YearOfRelease":"2000","Plot":"TestPlot","ActorIds":[1],"GenreIds":[1],"ProducerId":"1","PosterURL":"TestURL"}   | 201        |
	| {"Name":"","YearOfRelease":"2000","Plot":"TestPlot","ActorIds":[1],"GenreIds":[1],"ProducerId":"1","PosterURL":"TestURL"}           | 400        |
	| {"Name":"TestName","YearOfRelease":"","Plot":"TestPlot","ActorIds":[1],"GenreIds":[1],"ProducerId":"1","PosterURL":"TestURL"}       | 400        |
	| {"Name":"TestName","YearOfRelease":"2000","Plot":"","ActorIds":[1],"GenreIds":[1],"ProducerId":"1","PosterURL":"TestURL"}           | 400        |
	| {"Name":"TestName","YearOfRelease":"2000","Plot":"TestPlot","ActorIds":[],"GenreIds":[1],"ProducerId":"1","PosterURL":"TestURL"}    | 400        |
	| {"Name":"TestName","YearOfRelease":"2000","Plot":"TestPlot","ActorIds":[1],"GenreIds":[],"ProducerId":"1","PosterURL":"TestURL"}    | 400        |
	| {"Name":"TestName","YearOfRelease":"2000","Plot":"TestPlot","ActorIds":[1],"GenreIds":[1],"ProducerId":"","PosterURL":"TestURL"}    | 400        |
	| {"Name":"TestName","YearOfRelease":"2048","Plot":"TestPlot","ActorIds":[1],"GenreIds":[1],"ProducerId":"1","PosterURL":"TestURL"}   | 400        |
	| {"Name":"TestName","YearOfRelease":"2000","Plot":"TestPlot","ActorIds":[100],"GenreIds":[1],"ProducerId":"1","PosterURL":"TestURL"} | 400        |
	| {"Name":"TestName","YearOfRelease":"2000","Plot":"TestPlot","ActorIds":[1],"GenreIds":[100],"ProducerId":"1","PosterURL":"TestURL"} | 400        |
	| {"Name":"TestName","YearOfRelease":"2000","Plot":"TestPlot","ActorIds":[1],"GenreIds":[1],"ProducerId":"100","PosterURL":"TestURL"} | 400        |

Scenario: Get All Movies
	Given I am a client
	When I make GET Request to '/movies'
	Then response code must be 200
	And response data must look like '[{"id":1,"name":"TestName","yearOfRelease":2000,"plot":"TestPlot","actors":[{"id":1,"name":"TestName","gender":"M","dob":"2002-02-02T00:00:00","bio":"TestBio"}],"genres":[{"id":1,"name":"TestName"}],"producer":{"id":1,"name":"TestName","gender":"M","dob":"2002-02-02T00:00:00","bio":"TestBio"},"posterURL":"TestURL"}]'

Scenario Outline: Get a Movie by Id
	Given I am a client
	When I make GET Request to '<endpoint>'
	Then response code must be <statusCode>
	And response data must look like '<response>'
	Examples: 
	| endpoint    | statusCode | response                                                                                                                                                                                                                                                                                                                    |
	| /movies/1   | 200        | {"id":1,"name":"TestName","yearOfRelease":2000,"plot":"TestPlot","actors":[{"id":1,"name":"TestName","gender":"M","dob":"2002-02-02T00:00:00","bio":"TestBio"}],"genres":[{"id":1,"name":"TestName"}],"producer":{"id":1,"name":"TestName","gender":"M","dob":"2002-02-02T00:00:00","bio":"TestBio"},"posterURL":"TestURL"} |
	| /movies/100 | 404        | Not found                                                                                                                                                                                                                                                                                                                   |

Scenario Outline: Update a Movie
	Given I am a client
	When I make PUT Request '<endpoint>' with the following Data '<request>'
	Then response code must be <statusCode>
	Examples: 
	| endpoint   | request                                                                                                                             | statusCode |
	| /movies/1  | {"Name":"TestName","YearOfRelease":"2000","Plot":"TestPlot","ActorIds":[1],"GenreIds":[1],"ProducerId":"1","PosterURL":"TestURL"}   | 200        |
	| /movies/1  | {"Name":"","YearOfRelease":"2000","Plot":"TestPlot","ActorIds":[1],"GenreIds":[1],"ProducerId":"1","PosterURL":"TestURL"}           | 400        |
	| /movies/1  | {"Name":"TestName","YearOfRelease":"","Plot":"TestPlot","ActorIds":[1],"GenreIds":[1],"ProducerId":"1","PosterURL":"TestURL"}       | 400        |
	| /movies/1  | {"Name":"TestName","YearOfRelease":"2000","Plot":"","ActorIds":[1],"GenreIds":[1],"ProducerId":"1","PosterURL":"TestURL"}           | 400        |
	| /movies/1  | {"Name":"TestName","YearOfRelease":"2000","Plot":"TestPlot","ActorIds":[],"GenreIds":[1],"ProducerId":"1","PosterURL":"TestURL"}    | 400        |
	| /movies/1  | {"Name":"TestName","YearOfRelease":"2000","Plot":"TestPlot","ActorIds":[1],"GenreIds":[],"ProducerId":"1","PosterURL":"TestURL"}    | 400        |
	| /movies/1  | {"Name":"TestName","YearOfRelease":"2000","Plot":"TestPlot","ActorIds":[1],"GenreIds":[1],"ProducerId":"","PosterURL":"TestURL"}    | 400        |
	| /movies/1  | {"Name":"TestName","YearOfRelease":"2048","Plot":"TestPlot","ActorIds":[1],"GenreIds":[1],"ProducerId":"1","PosterURL":"TestURL"}   | 400        |
	| /movies/1  | {"Name":"TestName","YearOfRelease":"2000","Plot":"TestPlot","ActorIds":[100],"GenreIds":[1],"ProducerId":"1","PosterURL":"TestURL"} | 400        |
	| /movies/1  | {"Name":"TestName","YearOfRelease":"2000","Plot":"TestPlot","ActorIds":[1],"GenreIds":[100],"ProducerId":"1","PosterURL":"TestURL"} | 400        |
	| /movies/1  | {"Name":"TestName","YearOfRelease":"2000","Plot":"TestPlot","ActorIds":[1],"GenreIds":[1],"ProducerId":"100","PosterURL":"TestURL"} | 400        |
	| movies/100 | {"Name":"TestName","YearOfRelease":"2000","Plot":"TestPlot","ActorIds":[1],"GenreIds":[1],"ProducerId":"1","PosterURL":"TestURL"}   | 400        |

Scenario Outline: Delete a Movie
	Given I am a client
	When I make Delete Request '<endpoint>'
	Then response code must be <statusCode>
	Examples: 
	| endpoint    | statusCode |
	| /movies/1   | 200        |
	| /movies/100 | 404        |