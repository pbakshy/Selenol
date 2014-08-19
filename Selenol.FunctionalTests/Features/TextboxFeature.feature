Feature: TextboxFeature
	As an user I want to have ability to work with strong typed textbox element.

Scenario Outline: Read default text
	Given that I am viewing "Elements" page
	When I look at a textbox with id "<id>"
	Then text "<text>" appears in the textbox

	Examples: 
	| text            | id        |
	| some test value | textbox-1 |
	|                 | textbox-2 |

Scenario: All textboxes:
	Given that I am viewing "Elements" page
	Then there are textboxes with id "textbox-1, textbox-2"

Scenario Outline: Type text
	Given that I am viewing "Elements" page
	When I look at a textbox with id "<id>"
	And I clear the textbox 
	And I type text "sometext" to the textbox 
	Then text "sometext" appears in the textbox

	Examples: 
	| id        |
	| textbox-1 |
	| textbox-2 |
