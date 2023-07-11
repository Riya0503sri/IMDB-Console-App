Feature: Genre

This feature performs CRUD operations on Genre class

Scenario Outline: Create a Genre
	Given I am a client
	When I am making a post request to '/genres' with the following Data '<request>'
	Then response code must be <statusCode>
	Examples: 
	| request             | statusCode |
	| {"Name":"TestName"} | 201        |
	| {"Name":""}         | 400        |

Scenario: Get All Genres
	Given I am a client
	When I make GET Request to '/genres'
	Then response code must be 200
	And response data must look like '[{"id":1,"name":"TestName"}]'

Scenario Outline: Get a Genre by Id
	Given I am a client
	When I make GET Request to '<endpoint>'
	Then response code must be <statusCode>
	And response data must look like '<response>'
	Examples: 
	| endpoint    | statusCode | response                   |
	| /genres/1   | 200        | {"id":1,"name":"TestName"} |
	| /genres/100 | 404        | Not found                  |

Scenario Outline: Update a Genre
	Given I am a client
	When I make PUT Request '<endpoint>' with the following Data '<request>'
	Then response code must be <statusCode>
	Examples: 
	| endpoint    | request             | statusCode |
	| /genres/1   | {"Name":"TestName"} | 200        |
	| /genres/1   | {"Name":""}         | 400        |
	| /genres/100 | {"Name":"TestName"} | 400        |

Scenario Outline: Delete a Genre
	Given I am a client
	When I make Delete Request '<endpoint>'
	Then response code must be <statusCode>
	Examples: 
	| endpoint    | statusCode |
	| /genres/1   | 200        |
	| /genres/100 | 404        |