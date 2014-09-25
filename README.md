Dojo7000
========

Coding dojo group

## Code smells
###Duplicated Code
**Same code, more then one place.**
####**Cases:**

Same expression, but in two methods.

> **Refactoring:** Extract Method

Same expression in sibling classes

>**Refactoring:** **Extract Method**, **Pull up Field** . If code is similar but not same use **Form Template Method**. **Substitute Algorithm** if same thing is done different way (choose clearer of algorithms).

Same expression in unrelated classes

>**Refactoring:** Extract Class, or only make one uses the other


###Long Method

**The longer a method is, the more difficult it is to understand**

Be aggressive about decomposing methods

Use good naming

90% of the time, just [Extract Method](http://refactoring.com/catalog/extractMethod.html)

**What to extract?** Look for comments explaining a piece of code 


## Refactorings
 - [Extract Method](http://refactoring.com/catalog/extractMethod.html)
 - [Pull Up Field](http://refactoring.com/catalog/pullUpField.html)
 - [Form Template Method](http://refactoring.com/catalog/formTemplateMethod.html)
 - [Substitute Algorithm](http://refactoring.com/catalog/substituteAlgorithm.html)
 - [Extract Class](http://refactoring.com/catalog/extractClass.html)
