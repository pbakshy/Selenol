Feature: LinkFeature
	As an user I want to have ability to work with strong typed link element.

Scenario: All links
	Given that I am viewing "Elements" page
	Then there are links with id "link-1, link-2"

Scenario Outline: Get text
	Given that I am viewing "Elements" page
	When I look at a link with id "<id>"
	Then the link has text "<text>"

	Examples: 
	| id     | text   |
	| link-1 | Link 1 |
	| link-2 | Link 2 |

Scenario Outline: Click on link
	Given that I am viewing "Elements" page
	When I click on link with "<id>"
	Then I see an alert with text "<text>"

	Examples: 
	| id     | text           |
	| link-1 | Link 1 clicked |
	| link-2 | Link 2 clicked |