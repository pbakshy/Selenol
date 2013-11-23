Feature: WebDriverNavigation
	As a user I want be able to get strong typed Page after navigation using WebDriver

Scenario: Go to valid page
	When I go to "Elements" page by "Elements.html" url
	Then I should get instance of "ElementsPage"

Scenario: Go to invalid page
	When I go to "Elements" page by "Styles.html" url
	Then I should get "PageValidationException" exception
