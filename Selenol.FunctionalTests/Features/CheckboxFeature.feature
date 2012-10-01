Feature: CheckboxFeature
	As an user I want to have ability to work with strong typed checkbox element.

Scenario Outline: Get value
	Given that I am viewing "Elements" page
	When I look at a checkbox with id "<id>"
	Then the checkbox has value "<value>"

	Examples: 
	| id         | value |
	| checkbox-1 | true  |
	| checkbox-2 | false |

Scenario: All checkboxes
	Given that I am viewing "Elements" page
	Then there are checkboxes with id "checkbox-1, checkbox-2"

Scenario Outline: Change state
	Given that I am viewing "Elements" page
	When I look at a checkbox with id "<id>"
	And I check the checkbox
	Then the checkbox has value "true"
	When I uncheck the checkbox
	Then the checkbox has value "false"

	Examples: 
	| id         |
	| checkbox-1 |
	| checkbox-2 |