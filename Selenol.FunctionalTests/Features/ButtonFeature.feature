Feature: ButtonFeature
	As an user I want to have ability to work with strong typed button element.

Scenario: All buttons
	Given that I am viewing "Elements" page
	Then there are buttons with id "button-1, button-2"

Scenario Outline: Get text
	Given that I am viewing "Elements" page
	When I look at a button with id "<id>"
	Then the button has text "<text>"

	Examples: 
	| id       | text     |
	| button-1 | Button 1 |
	| button-2 | Button 2 |

Scenario Outline: Click on button
	Given that I am viewing "Elements" page
	When I click on button with "<id>"
	Then I see an alert with text "<text>"

	Examples: 
	| id       | text             |
	| button-1 | Button 1 clicked |
	| button-2 | Button 2 clicked |
