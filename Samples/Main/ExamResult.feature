Feature: Exam Results
	In order to know if I get my degree
	As a student
	I want to know the results

Scenario: 01 I get my degree 
	When the scores are
	 | Course       |  Score  | 
	 | Litterature  |  15     | 
	 | Mathematics  |  12     | 
	 | Economics    |  16     | 
	Then I get my degree
	
Scenario: 02 I do not get my degree 
	When the scores are
	 | Course       |  Score  | 
	 | Litterature  |   4     | 
	 | Mathematics  |  12     | 
	 | Economics    |   8     | 
	Then I do not get my degree
