##Bugs
 - quantity check in Order class, Receipt method is not consistent with HtmlReceipt method, cause the rules of applying discount are different.
 - in Order class HtmlReceipt method, for loop index <= _lines.Count throw out of boundary exception, should be index < _lines.Count.
 - All unit tests failed since the expect receipt result strings are different with Order class outputs
 - DateTime.Now been hard coded in Order class Receipt and HtmlReceipt methods, won't be able to get the same results in unit tests, need to pass the DateTime variable in.

##Areas need to consider refactoring
 - Use decimal for price instead of double
 - Add a new field for policy type instead of using price, since price can be changed
 - Hard coded Car, Motorcycle and Home prices in Policy class, every time add a new policy or change price, have to update this class
 - The logic of calculate line price and apply discounts are hard coded is Order class, with several if else statements in for lopp, hard to maintain. And for Receipt and HtmlReceipt methods, the logic of calculating total and tax are duplicates. Think of using Dependency injection with Factory patter/IoC to refactor it
 - Receipt have some key words, which should be reused, hard coded in Order class means we have to change the code every time we need to update the receipt text. Better to put them in receipt templates, and load the templates from external resources
 - Need to update unit test respectively, unit tests can be writtern down before refactoring as TDD