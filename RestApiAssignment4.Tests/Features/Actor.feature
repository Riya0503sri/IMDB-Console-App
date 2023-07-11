Feature: Actor

This feature performs CRUD operations on Actor class

Scenario Outline: Create an Actor
	Given I am a client
	When I am making a post request to '/actors' with the following Data '<request>'
	Then response code must be <statusCode>
	Examples: 
	| request                                                             | statusCode |
	| {"Name":"TestName","Bio":"TestBio","DOB":"2002-02-02","Gender":"M"} | 201        |
	| {"Name":"","Bio":"TestBio","DOB":"2002-02-02","Gender":"M"}         | 400        |
	| {"Name":"TestName","Bio":"","DOB":"2002-02-02","Gender":"M"}        | 400        |
	| {"Name":"TestName","Bio":"TestBio","DOB":"","Gender":"M"}           | 400        |
	| {"Name":"TestName","Bio":"TestBio","DOB":"2002-02-02","Gender":""}  | 400        |
	| {"Name":"TestName","Bio":"TestBio","DOB":"2024-01-01","Gender":"M"} | 400        |
	| {"Name":"TestName","Bio":"TestBio","DOB":"2002-02-02","Gender":"a"} | 400        |

Scenario: Get All Actors
	Given I am a client
	When I make GET Request to '/actors'
	Then response code must be 200
	And response data must look like '[{"id":1,"name":"TestName","gender":"M","dob":"2002-02-02T00:00:00","bio":"TestBio"}]'

Scenario Outline: Get an Actor by Id
	Given I am a client
	When I make GET Request to '<endpoint>'
	Then response code must be <statusCode>
	And response data must look like '<response>'
	Examples: 
	| endpoint    | statusCode | response                                                                            |
	| /actors/1   | 200        | {"id":1,"name":"TestName","gender":"M","dob":"2002-02-02T00:00:00","bio":"TestBio"} |
	| /actors/100 | 404        | Not found                                                                           |

Scenario Outline: Update an Actor
	Given I am a client
	When I make PUT Request '<endpoint>' with the following Data '<request>'
	Then response code must be <statusCode>
	Examples: 
	| endpoint    | request                                                             | statusCode |
	| /actors/1   | {"Name":"TestName","Bio":"TestBio","DOB":"2002-02-02","Gender":"M"} | 200        |
	| /actors/1   | {"Name":"","Bio":"TestBio","DOB":"2002-02-02","Gender":"M"}         | 400        |
	| /actors/1   | {"Name":"TestName","Bio":"","DOB":"2002-02-02","Gender":"M"}        | 400        |
	| /actors/1   | {"Name":"TestName","Bio":"TestBio","DOB":"2002-02-02","Gender":""}  | 400        |
	| /actors/1   | {"Name":"TestName","Bio":"TestBio","DOB":"2024-01-01","Gender":"M"} | 400        |
	| /actors/1   | {"Name":"TestName","Bio":"TestBio","DOB":"2002-02-02","Gender":"a"} | 400        |
	| /actors/100 | {"Name":"TestName","Bio":"TestBio","DOB":"2002-02-02","Gender":"M"} | 400        |

Scenario Outline: Delete an Actor
	Given I am a client
	When I make Delete Request '<endpoint>'
	Then response code must be <statusCode>
	Examples: 
	| endpoint    | statusCode |
	| /actors/1   | 200        |
	| /actors/100 | 404        |