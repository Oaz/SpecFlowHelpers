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
