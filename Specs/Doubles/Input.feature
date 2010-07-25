Feature: Input
	In order to easily write step definitions
	As a specflow user
	I want tables that quickly transform into test doubles with predefined property values
	
Scenario: Input with properties
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
	
Scenario: Input with properties not matching
	Given I have Stuff defined as following
	 | Field         | Value               |
	 | AString       | This is a string    |
	 | AnInt         | 234                 |
	 | ADouble       | 23.598              |
	 | ADateTime     | 2010-07-17 22:34:48 |
	When I do nothing
	Then my Stuff values are NOT as following
	 | Field         | Value               |
	 | AString       | This is a string    |
	 | AnInt         | 568                 |
	 | ADouble       | 23.598              |
	 | ADateTime     | 2010-07-17 22:34:48 |
	And I do not get any exception
	
Scenario: Input with properties matching on subset of properties
	Given I have Stuff defined as following
	 | Field         | Value               |
	 | AString       | This is a string    |
	 | AnInt         | 234                 |
	 | ADouble       | 23.598              |
	 | ADateTime     | 2010-07-17 22:34:48 |
	When I do nothing
	Then my Stuff values contains the following
	 | Field         | Value               |
	 | AString       | This is a string    |
	 | ADouble       | 23.598              |
	And I do not get any exception
	
Scenario: Input with properties not matching on subset of properties
	Given I have Stuff defined as following
	 | Field         | Value               |
	 | AString       | This is a string    |
	 | AnInt         | 234                 |
	 | ADouble       | 23.598              |
	 | ADateTime     | 2010-07-17 22:34:48 |
	When I do nothing
	Then my Stuff values does NOT contain the following
	 | Field         | Value               |
	 | AString       | This is a string    |
	 | ADouble       | 28.598              |
	And I do not get any exception
	
Scenario: Input with unknown property
	Given I have Stuff defined as following
	 | Field           | Value               |
	 | AWrongProperty  | Whatever            |
	When I do nothing
	Then I get an exception 'Unknown property AWrongProperty on type Specs.Oaz.SpecFlowHelpers.Doubles.Stuff'
	
