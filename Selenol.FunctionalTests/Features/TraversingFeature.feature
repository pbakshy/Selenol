Feature: TraversingFeature
	As an user I want to have ability to traverse through elements.

Scenario Outline: Traverse to parents
	Given that I am viewing "Traversing" page
	When I look at an element with id "<id>"
	And I go to a parent of the element
	Then the element id is "<first_parent_id>"
	When I go to a parent of the element
	Then the element id is "<second_parent_id>"

	Examples: 
	| id           | first_parent_id | second_parent_id |
	| subblock-1-1 | block-1         | root             |
	| subblock-1-3 | block-1         | root             |
	| subblock-2-1 | block-2         | root             |

Scenario Outline: Traverse through subling
	Given that I am viewing "Traversing" page
	When I look at an element with id "<id>"
	And I go to a next sibling of the element
	Then the element id is "<next_sibling_id>"
	When I go to a previous sibling of the element
	Then the element id is "<id>"

	Examples: 
	| id           | next_sibling_id |
	| subblock-1-1 | subblock-1-2    |
	| subblock-1-2 | subblock-1-3    |
	| subblock-1-3 | subblock-1-4    |
	| subblock-2-1 | subblock-2-2    |
