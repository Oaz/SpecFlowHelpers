Feature: Verification
	In order to easily write step definitions
	As a specflow user
	I want method call that can be checked against expectations
	
Scenario: Basic verification
	Given a verifiable receiver
	When I do something with name 'name1'
	Then the receiver got in 'Sentences' syntax the following commands
	 | Commands                                |
	 | do something "name1"                    |
	And the receiver did not get in 'Sentences' syntax the following commands
	 | Commands                                |
	 | do something "name2"                    |
	And the receiver did not get in 'Sentences' syntax the following commands
	 | Commands                                |
	 | do some other thing "name1"             |
	And the receiver did not get in 'Sentences' syntax the following commands
	 | Commands                                |
	 | do something "name1" "name2"            |
	And the receiver did not get in 'Sentences' syntax the following commands
	 | Commands                                |
	 | do something "name1"                    |
	 | do something "name2"                    |

Scenario: Verification with multiple int parameters
	Given a verifiable receiver
	When I put two numbers [26,478]
	Then the receiver got in 'Sentences' syntax the following commands
	 | Commands                                |
	 | put two numbers 26 478                  |
	And the receiver did not get in 'Sentences' syntax the following commands
	 | Commands                                |
	 | put two numbers 26 48                   |
