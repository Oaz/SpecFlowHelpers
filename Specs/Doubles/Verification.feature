Feature: Verification
	In order to easily write step definitions
	As a specflow user
	I want method call that can be checked against expectations
	
Scenario: Basic verification
	Given a spy receiver without behaviour
	When I do something with name 'name1'
	Then the receiver got in Natural syntax the following commands
	 | Commands                                |
	 | do something "name1"                    |
	And the receiver did not get in Natural syntax the following commands
	 | Commands                                |
	 | do something "name2"                    |
	And the receiver did not get in Natural syntax the following commands
	 | Commands                                |
	 | do some other thing "name1"             |
	And the receiver did not get in Natural syntax the following commands
	 | Commands                                |
	 | do something "name1" "name2"            |
	And the receiver did not get in Natural syntax the following commands
	 | Commands                                |
	 | do something "name1"                    |
	 | do something "name2"                    |

Scenario: Verification with unknown method
	Given a spy receiver without behaviour
	When I do something with name 'name1'
	Then the receiver did not get in Natural syntax the following commands
	 | Commands                                |
	 | do some other thing "name1"             |
	And I get an exception: Type <Specs.Oaz.SpecFlowHelpers.Doubles.Receiver> does not have a method named <DoSomeOtherThing>


Scenario: Verification with multiple int parameters
	Given a spy receiver without behaviour
	When I put two numbers [26,478]
	Then the receiver got in Natural syntax the following commands
	 | Commands                                |
	 | put two numbers 26 478                  |
	And the receiver did not get in Natural syntax the following commands
	 | Commands                                |
	 | put two numbers 26 48                   |

Scenario: Verification with multiple syntaxes
	Given a spy receiver without behaviour
	When I put two numbers [26,478]
	Then the receiver got in Functions syntax the following commands
	 | Commands                                |
	 | PutTwoNumbers(26,478)                   |

Scenario: Verification with list of commands
	Given a spy receiver without behaviour
	When I do something with name 'name1'
	And I put two numbers [333,1]
	And I do something with name 'name2'
	And I put two numbers [8,44]
	Then the receiver got in Natural syntax the following commands
	 | Commands                                |
	 | do something "name1"                    |
	 | put two numbers 333 1                   |
	 | do something "name2"                    |
	 | put two numbers 8 44                    |
	And the receiver did not get in Natural syntax the following commands
	 | Commands                                |
	 | put two numbers 333 1                   |
	 | do something "name1"                    |
	 | do something "name2"                    |
	 | put two numbers 8 44                    |
	And the receiver did not get in Natural syntax the following commands
	 | Commands                                |
	 | do something "name1"                    |
	 | do something "name2"                    |
	 | put two numbers 8 44                    |
	And the receiver did not get in Natural syntax the following commands
	 | Commands                                |
	 | do something "name1"                    |
	 | put two numbers 333 1                   |
	 | do something "name2"                    |

Scenario: Verification with behaviour
	Given a spy receiver with the following behaviour
	 | Commands                                              |
	 | compute the magic number from 23 => 48                |
	When I compute the magic number from 23 and put both numbers
	Then the receiver got in Natural syntax the following commands
	 | Commands                                |
	 | compute the magic number from 23        |
	 | put two numbers 23 48                   |
