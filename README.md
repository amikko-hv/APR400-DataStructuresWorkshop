# APR400 — Data Structures & Algorithms Workshop

A library console application built with C#.  

The codebase is intentionally correct in behaviour but deliberately poor in its choice of data structures and algorithms.
The code utlizes collections provided by .NET and functionality is covered by unit tests written with xUnit.

## Task

The app manages books, loans and members. The app works in terms of behavior. However, it has issues when it comes to 
utilizing the right data structures and algorithms to achieve efficient performance.

You should:

- Review the code for potential issues related to data structures and algorithms.
- Refactor the code to use more appropriate data structures and algorithms.
- Ensure that the refactored code maintains the same functionality and behavior as the original code.
- Consider the Big O time complexity before and after your changes.

You are unlikley to be able to fix all issues in the code. Focus on a few changes. Make sure you truly understand why it 
is a problem and how your change improves the situation.

## Things to consider

- ``LibraryService`` contains the core logic. Focus your attention there.
- Rely on the unit tests to ensure correct behavior is maintained.
- Which collection is used and is there a better choice?
- Does the code use generic or non-generic collections?
