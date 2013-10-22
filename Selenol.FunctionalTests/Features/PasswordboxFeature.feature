Feature: PasswordFeature
	As an user I want to have ability to work with strong typed passwordbox element.

Scenario Outline: Read default password
	Given that I am viewing "Elements" page
	When I look at a password field with id "<id>"
	Then text "<text>" appears in the password field

	Examples: 
	| text            | id         |
	| pass            | password-1 |
	|                 | password-2 |

Scenario: Type password
	Given that I am viewing "Elements" page
	When I look at a password field with id "password-1"
	And I clear the password field
	And I type password "newpassword" to the password field
	Then text "newpassword" appears in the password field
