Feature: Double
	In order to easily write step definitions
	As a specflow user
	I want test doubles
	
Scenario: Test double has behaviour
	Given I have a test double named 'foo'
	And behaviour of method 'GetName' for 'foo' returns 'bar'
	When I call method 'GetName' on 'foo'
	Then I get the value 'bar'
	And the value type is 'System.String'
	
Scenario: Test double has another behaviour
	Given I have a test double named 'foo'
	And behaviour of method 'GetValue' for 'foo' returns '23'
	When I call method 'GetValue' on 'foo'
	Then I get the value '23'
	And the value type is 'System.Int32'
	
Scenario: Test double should have behaviour on method returning value
	Given I have a test double named 'foo'
	When I call method 'GetName' on 'foo'
	Then I get an exception: Unexpected call 'GetName' on test double

Scenario: Test double should have behaviour on method returning value when other behaviour is defined
	Given I have a test double named 'foo'
	And behaviour of method 'GetValue' for 'foo' returns '23'
	When I call method 'GetName' on 'foo'
	Then I get an exception: Unexpected call 'GetName' on test double
	
Scenario: Test double has behaviour defined by table
	Given I have a test double named 'foo'
	And behaviour for 'foo' is defined as following
	 | Commands                                |
	 | get value => 48                         |
	When I call method 'GetValue' on 'foo'
	Then I get the value '48'
	And the value type is 'System.Int32'
