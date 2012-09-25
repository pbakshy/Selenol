Feature: TextboxFeature
	As an user I want to have ability to work with strong typed textbox element.

Scenario: Type text
	Given that I am viewing "Elements" page
	When I type text "sometext" to textbox with id "textbox"
	Then text "sometext" appears in textbox with id "textbox"
