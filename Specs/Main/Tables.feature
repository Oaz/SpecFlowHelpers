Feature: Tables
	In order to easily write step definitions
	As a specflow user
	I want tables that quickly transform into objects
	
Scenario: Object with properties
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
	
Scenario: Object with properties not matching
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
	
Scenario: Object with properties matching on subset of properties
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
	
Scenario: Object with properties not matching on subset of properties
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
	
Scenario: Object with unknown property
	Given I have Stuff defined as following
	 | Field           | Value               |
	 | AWrongProperty  | Whatever            |
	When I do nothing
	Then I get an exception 'Unknown property AWrongProperty on type Specs.Oaz.SpecFlowHelpers.Stuff'
	
Scenario: group of objects with properties
	Given I have lots of Stuff defined as following
	 | AString          | AnInt               | ADouble         | ADateTime               |
	 | This is a string | 234                 | 23.598          | 2010-07-17 22:34:48     |
	 | Yeah!            | 666                 | -21.65          | 2038-01-19 03:14:08     |
	 | Still there      | 1234568             | 3.14159265      | 1970-01-01 00:00:00     |
	When I do nothing
	Then all my Stuff values are strictly equal to the following
	 | AString          | AnInt               | ADouble         | ADateTime               |
	 | This is a string | 234                 | 23.598          | 2010-07-17 22:34:48     |
	 | Yeah!            | 666                 | -21.65          | 2038-01-19 03:14:08     |
	 | Still there      | 1234568             | 3.14159265      | 1970-01-01 00:00:00     |
	And all my Stuff values are strictly equal to the following
	 | AString          | ADouble             | AnInt           | ADateTime               |
	 | This is a string | 23.598              | 234             | 2010-07-17 22:34:48     |
	 | Yeah!            | -21.65              | 666             | 2038-01-19 03:14:08     |
	 | Still there      | 3.14159265          | 1234568         | 1970-01-01 00:00:00     |
	And I do not get any exception
	
Scenario: group of objects with properties not matching values
	Given I have lots of Stuff defined as following
	 | AString          | AnInt               | ADouble         | ADateTime               |
	 | This is a string | 234                 | 23.598          | 2010-07-17 22:34:48     |
	 | Yeah!            | 666                 | -21.65          | 2038-01-19 03:14:08     |
	 | Still there      | 1234568             | 3.14159265      | 1970-01-01 00:00:00     |
	When I do nothing
	Then all my Stuff values are NOT strictly equal to the following
	 | AString          | AnInt               | ADouble         | ADateTime               |
	 | This is a string | 235                 | 23.598          | 2010-07-17 22:34:48     |
	 | Yeah!            | 666                 | -21.65          | 2038-01-19 03:14:08     |
	 | Still there      | 1234568             | 3.14159265      | 1970-01-01 00:00:00     |
	And all my Stuff values are NOT strictly equivalent to the following
	 | AString          | AnInt               | ADouble         | ADateTime               |
	 | This is a string | 235                 | 23.598          | 2010-07-17 22:34:48     |
	 | Yeah!            | 666                 | -21.65          | 2038-01-19 03:14:08     |
	 | Still there      | 1234568             | 3.14159265      | 1970-01-01 00:00:00     |
	And all my Stuff values are NOT strictly equivalent to the following
	 | AString          | AnInt               | ADouble         | ADateTime               |
	 | Still there      | 1234568             | 3.14159265      | 1970-01-01 00:00:00     |
	 | This is a string | 235                 | 23.598          | 2010-07-17 22:34:48     |
	 | Yeah!            | 666                 | -21.65          | 2038-01-19 03:14:08     |
	And I do not get any exception
	
Scenario: group of objects in different orders with properties
	Given I have lots of Stuff defined as following
	 | AString          | AnInt               | ADouble         | ADateTime               |
	 | This is a string | 234                 | 23.598          | 2010-07-17 22:34:48     |
	 | Yeah!            | 666                 | -21.65          | 2038-01-19 03:14:08     |
	 | Still there      | 1234568             | 3.14159265      | 1970-01-01 00:00:00     |
	When I do nothing
	Then all my Stuff values are strictly equivalent to the following
	 | AString          | AnInt               | ADouble         | ADateTime               |
	 | Still there      | 1234568             | 3.14159265      | 1970-01-01 00:00:00     |
	 | This is a string | 234                 | 23.598          | 2010-07-17 22:34:48     |
	 | Yeah!            | 666                 | -21.65          | 2038-01-19 03:14:08     |
	And all my Stuff values are strictly equivalent to the following
	 | AString          | ADouble             | AnInt           | ADateTime               |
	 | Still there      | 3.14159265          | 1234568         | 1970-01-01 00:00:00     |
	 | This is a string | 23.598              | 234             | 2010-07-17 22:34:48     |
	 | Yeah!            | -21.65              | 666             | 2038-01-19 03:14:08     |
	And all my Stuff values are NOT strictly equal to the following
	 | AString          | AnInt               | ADouble         | ADateTime               |
	 | Still there      | 1234568             | 3.14159265      | 1970-01-01 00:00:00     |
	 | This is a string | 234                 | 23.598          | 2010-07-17 22:34:48     |
	 | Yeah!            | 666                 | -21.65          | 2038-01-19 03:14:08     |
	And I do not get any exception
	
Scenario: group of objects with subset of properties
	Given I have lots of Stuff defined as following
	 | AString          | AnInt               | ADouble         | ADateTime               |
	 | This is a string | 234                 | 23.598          | 2010-07-17 22:34:48     |
	 | Yeah!            | 666                 | -21.65          | 2038-01-19 03:14:08     |
	 | Still there      | 1234568             | 3.14159265      | 1970-01-01 00:00:00     |
	When I do nothing
	Then all my Stuff values are loosely equal to the following
	 | AString          | AnInt               |
	 | This is a string | 234                 |
	 | Yeah!            | 666                 |
	 | Still there      | 1234568             |
	And all my Stuff values are loosely equal to the following
	 | AnInt            | AString          |
	 | 234              | This is a string |
	 | 666              | Yeah!            |
	 | 1234568          | Still there      |
	And all my Stuff values are NOT strictly equal to the following
	 | AString          | AnInt               |
	 | This is a string | 234                 |
	 | Yeah!            | 666                 |
	 | Still there      | 1234568             |
	And I do not get any exception
	
Scenario: group of objects with subset of properties not matching values
	Given I have lots of Stuff defined as following
	 | AString          | AnInt               | ADouble         | ADateTime               |
	 | This is a string | 234                 | 23.598          | 2010-07-17 22:34:48     |
	 | Yeah!            | 666                 | -21.65          | 2038-01-19 03:14:08     |
	 | Still there      | 1234568             | 3.14159265      | 1970-01-01 00:00:00     |
	When I do nothing
	Then all my Stuff values are NOT loosely equal to the following
	 | AString          | AnInt               |
	 | This is a string | 235                 |
	 | Yeah!            | 666                 |
	 | Still there      | 1234568             |
	And all my Stuff values are NOT loosely equivalent to the following
	 | AString          | AnInt               |
	 | This is a string | 235                 |
	 | Yeah!            | 666                 |
	 | Still there      | 1234568             |
	And all my Stuff values are NOT loosely equivalent to the following
	 | AnInt            | AString          |
	 | 235              | This is a string |
	 | 666              | Yeah!            |
	 | 1234568          | Still there      |
	And I do not get any exception
	
Scenario: group of objects in different orders with subset of properties
	Given I have lots of Stuff defined as following
	 | AString          | AnInt               | ADouble         | ADateTime               |
	 | This is a string | 234                 | 23.598          | 2010-07-17 22:34:48     |
	 | Yeah!            | 666                 | -21.65          | 2038-01-19 03:14:08     |
	 | Still there      | 1234568             | 3.14159265      | 1970-01-01 00:00:00     |
	When I do nothing
	Then all my Stuff values are loosely equivalent to the following
	 | AString          | AnInt               |
	 | Still there      | 1234568             |
	 | This is a string | 234                 |
	 | Yeah!            | 666                 |
	And all my Stuff values are loosely equivalent to the following
	 | AnInt            | AString          |
	 | 234              | This is a string |
	 | 666              | Yeah!            |
	 | 1234568          | Still there      |
	And all my Stuff values are NOT loosely equal to the following
	 | AString          | AnInt               |
	 | Still there      | 1234568             |
	 | This is a string | 234                 |
	 | Yeah!            | 666                 |
	And I do not get any exception
	