Feature: TutorialsServiceTests
	As a Developer
	I want to add a new Tutorial through API
	In order to make it available for applications.
	
	Background: 
		Given the Endpoint https://localhost:7070/api/v1/tutorials is available
		
@tutorial-adding
Scenario: Add Tutorial with unique Title
	When a Post Request is sent
	| Title  | Description       | CategoryId |
	| Sample | A Sample Tutorial | 1          |
 Then A Response is received with Status 200
 And a Tutorial Resource is included in Response Body
 | Id | Title  | Description       | CategoryId |
 | 1  | Sample | A Sample Tutorial | 1          |

@tutorial-adding
Scenario: Add Tutorial with existing Title
	Given A Tutorial is already stored
	| Id | Title    | Description           | CategoryId |
	| 1  | Ultimate | The Ultimate Tutorial | 1          |
 When a Post Request is sent
 | Title    | Description           | CategoryId |
 | Ultimate | The Ultimate Tutorial | 1          |
 Then A Response is received with Status 400
 And An Error Message is returned with value "Tutorial title already exists."