Feature: Review

This feature performs CRUD operations on Review class

Scenario Outline: Create a Review
	Given I am a client
	When I am making a post request to '<endpoint>' with the following Data '<request>'
	Then response code must be <statusCode>
	Examples: 
	| endpoint            | request                   | statusCode |
	| /movies/1/reviews   | {"Message":"TestComment"} | 201        |
	| /movies/1/reviews   | {"Message":""}            | 400        |
	| /movies/100/reviews | {"Message":"TestComment"} | 400        |

Scenario Outline: Get All Reviews
	Given I am a client
	When I make GET Request to '<endpoint>'
	Then response code must be <statusCode>
	And response data must look like '<response>'
	Examples: 
	| endpoint            | statusCode | response                           |
	| /movies/1/reviews   | 200        | [{"id":1,"message":"TestComment"}] |
	| /movies/100/reviews | 404        | Not found                          |

Scenario Outline: Get a Review by Id
	Given I am a client
	When I make GET Request to '<endpoint>'
	Then response code must be <statusCode>
	And response data must look like '<response>'
	Examples: 
	| endpoint              | statusCode | response                         |
	| /movies/1/reviews/1   | 200        | {"id":1,"message":"TestComment"} |
	| /movies/1/reviews/100 | 404        | Not found                        |
	| /movies/100/reviews/1 | 404        | Not found                        |

Scenario Outline: Update a Review
	Given I am a client
	When I make PUT Request '<endpoint>' with the following Data '<request>'
	Then response code must be <statusCode>
	Examples: 
	| endpoint              | request                   | statusCode |
	| /movies/1/reviews/1   | {"Message":"TestComment"} | 200        |
	| /movies/1/reviews/1   | {"Message":""}            | 400        |
	| /movies/100/reviews/1 | {"Message":"TestComment"} | 400        |
	| /movies/1/reviews/100 | {"Message":"TestComment"} | 400        |

Scenario Outline: Delete a Review
	Given I am a client
	When I make Delete Request '<endpoint>'
	Then response code must be <statusCode>
	Examples: 
	| endpoint              | statusCode |
	| /movies/1/reviews/1   | 200        |
	| /movies/100/reviews/1 | 404        |
	| /movies/1/reviews/100 | 404        |