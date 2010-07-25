Feature: Verification
	In order to easily write step definitions
	As a specflow user
	I want method call that can be checked against expectations
	
Scenario: Basic verification
	Given a verifiable receiver
	When I do something with name 'n1'
	Then the receiver got in 'Sentences' syntax the following commands
	 | Commands                                |
	 | do something "n1"                       |
	And the receiver did not get in 'Sentences' syntax the following commands
	 | Commands                                |
	 | do something "n2"                       |
	And the receiver did not get in 'Sentences' syntax the following commands
	 | Commands                                |
	 | do some other thing "n1"                |
	And the receiver did not get in 'Sentences' syntax the following commands
	 | Commands                                |
	 | do something "n1" "n2"                  |
	And the receiver did not get in 'Sentences' syntax the following commands
	 | Commands                                |
	 | do something "n1"                       |
	 | do something "n2"                       |
