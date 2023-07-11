Feature: Producer

This feature performs CRUD operations on Producer class

Scenario Outline: Create a Producer
	Given I am a client
	When I am making a post request to '/producers' with the following Data '<request>'
	Then response code must be <statusCode>
	Examples: 
	| request                                                             | statusCode |
	| {"Name":"TestName","Bio":"TestBio","DOB":"2002-02-02","Gender":"M"} | 201        |
	| {"Name":"","Bio":"TestBio","DOB":"2002-02-02","Gender":"M"}         | 400        |
	| {"Name":"TestName","Bio":"","DOB":"2002-02-02","Gender":"M"}        | 400        |
	| {"Name":"TestName","Bio":"TestBio","DOB":"","Gender":"M"}           | 400        |
	| {"Name":"TestName","Bio":"TestBio","DOB":"2002-02-02","Gender":""}  | 400        |
	| {"Name":"TestName","Bio":"TestBio","DOB":"2025-01-01","Gender":"M"} | 400        |
	| {"Name":"TestName","Bio":"TestBio","DOB":"2002-02-02","Gender":"a"} | 400        |

Scenario: Get All Producers
	Given I am a client
	When I make GET Request to '/producers'
	Then response code must be 200
	And response data must look like '[{"id":1,"name":"TestName","gender":"M","dob":"2002-02-02T00:00:00","bio":"TestBio"}]'

Scenario Outline: Get a Producer by Id
	Given I am a client
	When I make GET Request to '<endpoint>'
	Then response code must be <statusCode>
	And response data must look like '<response>'
	Examples: 
	| endpoint       | statusCode | response                                                                            |
	| /producers/1   | 200        | {"id":1,"name":"TestName","gender":"M","dob":"2002-02-02T00:00:00","bio":"TestBio"} |
	| /producers/100 | 404        | Not found                                                                           |

Scenario Outline: Update a Producer
	Given I am a client
	When I make PUT Request '<endpoint>' with the following Data '<request>'
	Then response code must be <statusCode>
	Examples: 
	| endpoint       | request                                                             | statusCode |
	| /producers/1   | {"Name":"TestName","Bio":"TestBio","DOB":"2002-02-02","Gender":"M"} | 200        |
	| /producers/1   | {"Name":"","Bio":"TestBio","DOB":"2002-02-02","Gender":"M"}         | 400        |
	| /producers/1   | {"Name":"TestName","Bio":"","DOB":"2002-02-02","Gender":"M"}        | 400        |
	| /producers/1   | {"Name":"TestName","Bio":"TestBio","DOB":"","Gender":"M"}           | 400        |
	| /producers/1   | {"Name":"TestName","Bio":"TestBio","DOB":"2002-02-02","Gender":""}  | 400        |
	| /producers/1   | {"Name":"TestName","Bio":"TestBio","DOB":"2024-01-01","Gender":"M"} | 400        |
	| /producers/1   | {"Name":"TestName","Bio":"TestBio","DOB":"2002-02-02","Gender":"a"} | 400        |
	| /producers/100 | {"Name":"TestName","Bio":"TestBio","DOB":"2002-02-02","Gender":"M"} | 400        |

Scenario Outline: Delete a Producer
	Given I am a client
	When I make Delete Request '<endpoint>'
	Then response code must be <statusCode>
	Examples: 
	| endpoint       | statusCode |
	| /producers/1   | 200        |
	| /producers/100 | 404        |