Feature: Tables
	In order to easily write step definitions
	As a specflow user
	I want tables that quickly transform into mocks
	
Scenario: Mock with properties
	Given I have Stuff defined as following
	 | Field         | Value               |
	 | AString       | This is a string    |
	 | AnInt         | 234                 |
	 | ADouble       | 23.598              |
	 | ADateTime     | 2010-07-17 22:34:48 |
	When I do nothing
	Then my Stuff values are as following
	 | Field         | Value               |
	 | AString       | This is a string    |
	 | AnInt         | 234                 |
	 | ADouble       | 23.598              |
	 | ADateTime     | 2010-07-17 22:34:48 |
	And I do not get any exception
