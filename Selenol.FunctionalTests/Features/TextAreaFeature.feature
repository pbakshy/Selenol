Feature: TextAreaFeature
	As an user I want to have ability to work with strong typed text area element.

Scenario Outline: Read default text
	Given that I am viewing "Elements" page
	Then text "<text>" appears in text area with id "<id>"

	Examples: 
	| text                            | id          |
	| There is some really long text. | text-area-1 |
	|                                 | text-area-2 |

Scenario Outline: Type text
	Given that I am viewing "Elements" page
	When I clear text area with id "<id>"
	And I type text "sometext" to text area with id "<id>"
	Then text "sometext" appears in text area with id "<id>"

	Examples: 
	| id          |
	| text-area-1 |
	| text-area-2 |
