Feature: TextboxFeature
	As an user I want to have ability to work with strong typed textbox element.

Scenario Outline: Read default text
	Given that I am viewing "Elements" page
	Then text "<text>" appears in textbox with id "<id>"

	Examples: 
	| text            | id        |
	| some test value | textbox-1 |
	|                 | textbox-2 |

Scenario: All textboxes:
	Given that I am viewing "Elements" page
	Then there are textboxes with id "textbox-1, textbox-2"

Scenario Outline: Type text
	Given that I am viewing "Elements" page
	When I clear textbox with id "<id>"
	When I type text "sometext" to textbox with id "<id>"
	Then text "sometext" appears in textbox with id "<id>"

	Examples: 
	| id        |
	| textbox-1 |
	| textbox-2 |
