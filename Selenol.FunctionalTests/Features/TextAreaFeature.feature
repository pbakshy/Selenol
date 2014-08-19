Feature: TextAreaFeature
	As an user I want to have ability to work with strong typed text area element.

Scenario Outline: Read default text
	Given that I am viewing "Elements" page
	When I look at a text area with id "<id>"
	Then text "<text>" appears in the text area

	Examples: 
	| text                            | id          |
	| There is some really long text. | text-area-1 |
	|                                 | text-area-2 |

Scenario: All text areas:
	Given that I am viewing "Elements" page
	Then there are text areas with id "text-area-1, text-area-2"

Scenario Outline: Type text
	Given that I am viewing "Elements" page
	When I look at a text area with id "<id>"
	And I clear the text area
	And I type text "sometext" to the text area
	Then text "sometext" appears in the text area

	Examples: 
	| id          |
	| text-area-1 |
	| text-area-2 |
