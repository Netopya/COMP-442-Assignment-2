# COMP-442-Assignment-2
Syntactic Analyzer

This assignment is a top-down table driven syntactic analyzer written in C# using .NET 4.5 as a Windows Forms application. A stand-alone executable (COMP442-Assignment2.exe) is included to run the program. The solution can be opened for analysis and the running of unit tests with Visual Studio 2015. When inputting text, it is important that line endings are in the Windows CR+LF format. All output is written to text files in the same directory as the executable file. To input code, simply enter in code in the “Code:” textbox and then click the “Analyze!” button to perform lexical and syntactic analyses. The tokenized output will be shown in the “Lexical Analysis” tab broken into the tokens in the “Tokens:” textbox and any errors will be shown in the “Errors:” textbox. The result of lexical analysis will be shown under the “Syntax Analysis” tab with the derivation and any error message with recovery operations. Furthermore, tests are defined as Visual Studio unit tests and can be run from the Test Explorer. See UnitTest1.cs and UnitTest2.cs for a list of unit tests and their expected output. AtoCC’s kfG Edit tool was used to verify the validity of the grammar and the tool from http://hackingoff.com/compilers/predict-first-follow-set was used to develop the First and Follow sets.
