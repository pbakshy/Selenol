Feature: FormFeature
	As an user I want to have ability to work with strong typed form element.

Scenario: All form
	Given that I am viewing "Elements" page
	Then there are forms with id "form-1, form-2"

Scenario Outline: Submit a form
	Given that I am viewing "Elements" page
	When I submit form with id "<id>"
	Then I will navigate to page "<page>"

	Examples: 
	| id     | page  |
	| form-1 | Page1 |
	| form-2 | Page2 |
