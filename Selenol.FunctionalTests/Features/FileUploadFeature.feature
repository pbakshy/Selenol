Feature: FileUploadFeature
	As an user I want to have ability to work with strong typed file upload element.

Scenario: All file uploads
	Given that I am viewing "Elements" page
	Then there are file uploads with id "file-1, file-2"

Scenario Outline: Uploading file
	Given that I am viewing "Elements" page
	When I look at file upload with id "<id>"
	And I choose a file for the file upload
	Then the file upload has the file

	Examples: 
	| id     |
	| file-1 |
	| file-2 |
