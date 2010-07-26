Feature: Verification
	In order to easily write step definitions
	As a specflow user
	I want method call that can be checked against expectations
	
Scenario: Basic verification
	Given a verifiable receiver
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

Scenario: Verification with multiple int parameters
	Given a verifiable receiver
	When I put two numbers [26,478]
	Then the receiver got in Natural syntax the following commands
	 | Commands                                |
	 | put two numbers 26 478                  |
	And the receiver did not get in Natural syntax the following commands
	 | Commands                                |
	 | put two numbers 26 48                   |

Scenario: Verification with multiple syntaxes
	Given a verifiable receiver
	When I put two numbers [26,478]
	Then the receiver got in Functions syntax the following commands
	 | Commands                                |
	 | PutTwoNumbers(26,478)                   |

Scenario: Verification with list of commands
	Given a verifiable receiver
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
