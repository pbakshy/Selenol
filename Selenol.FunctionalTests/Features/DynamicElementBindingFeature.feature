Feature: DynamicElementsBinding
	As an user I want to have ability to work with dynamically initialized members of page object.

Scenario: Dynamically initialize multi select by id
	When I am acessing "Elements" page using page object
	Then value of FirstSelect must be 'rat-value'

Scenario: Dyamically initialize button by tag name
	When I am acessing "Elements" page using page object
	Then id of SecondButton must be 'button-2'

Scenario: Dyamically initialize radio buttons by name
	When I am acessing "Elements" page using page object
	Then second radio button in the sequence must be checked

Scenario: Dyamically initialize checkox by class
	When I am acessing "Elements" page using page object
	Then SecondCheckbox must be unchecked
