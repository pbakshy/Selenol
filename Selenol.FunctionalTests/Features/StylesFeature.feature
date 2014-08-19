Feature: StylesFeature
	As an user I want to have ability to access element css styles

Scenario Outline: Get style
	Given that I am viewing "Styles" page
	When I look at an element with id "<id>"
	Then width of the element is <width>
	And height of the element is <height>
	And color of the element is "<color>"
	And border of the element is "<border>"

	Examples: 
	| id        | width | height | color     | border |
	| element-1 | 150   | 200    | DarkGreen | double |
	| element-2 | 100   | 40     | Red       | dashed |
	| element-3 | 300   | 300    | Blue      | solid  |
