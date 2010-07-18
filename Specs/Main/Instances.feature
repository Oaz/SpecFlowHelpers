Feature: Instances
	In order to easily write step definitions
	As a specflow user
	I want instances of objects accessible through all steps
	
Scenario: Unique instance
	Given I have a unique instance of Foo
	When I do nothing
	Then I have the same unique instance of Foo
	
Scenario: Named instance
	Given I have an instance of Foo named foo
	When I do nothing
	Then I have the same instance of Foo named foo
	
Scenario: Named instance is null by default
	When I do nothing
	Then I do not have an instance of Foo named foo
	
Scenario: Instances with same name but different type
	Given I have an instance of Foo named foo
	And I have an instance of Bar named foo
	When I do nothing
	Then I have the same instance of Foo named foo
	And I have the same instance of Bar named foo
	
Scenario: Instances with same name and parent/child type
	Given I have an instance of Foo named foo
	When I do nothing
	Then  do not have an instance of FooBar named foo even if FooBar inherits from Foo
