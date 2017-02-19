using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using COMP442_Assignment2.Lexical;
using COMP442_Assignment2.Syntactic;
using System.Linq;

namespace Assignment2_UnitTests
{
    /// <summary>
    /// Summary description for UnitTest2
    /// </summary>
    [TestClass]
    public class UnitTest2
    {
        LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer();
        SyntacticAnalyzer syntacticAnalyzer = new SyntacticAnalyzer();

        [TestMethod]
        public void TestDemoCode()
        {
            var tokens = lexicalAnalyzer.Tokenize("class Utility { int var1[4][5][7][8][9][1][0]; float var2; int findMax(int array[100]) { int maxValue; int idx; maxValue = array[100]; for( int idx = 99; idx > 0; idx = idx - 1 ) { if(array[idx] > maxValue) then { maxValue = array[idx]; }else{}; }; return (maxValue); }; int findMin(int array[100]) { int minValue; int idx; minValue = array[100]; for( int idx = 1; idx <= 99; idx = ( idx ) + 1) { if(array[idx] < maxValue) then { maxValue = array[idx]; }else{}; }; return (minValue); }; }; program { int sample[100]; int idx; int maxValue; int minValue; Utility utility; Utility arrayUtility[2][3][6][7]; for(int t = 0; t<=100 ; t = t + 1) { get(sample[t]); sample[t] = (sample[t] * randomize()); }; maxValue = utility.findMax(sample); minValue = utility.findMin(sample); utility. var1[4][1][0][0][0][0][0] = 10; arrayUtility[1][1][1][1].var1[4][1][0][0][0][0][0] = 2; put(maxValue); put(minValue); }; float randomize() { float value; value = 100 * (2 + 3.0 / 7.0006); value = 1.05 + ((2.04 * 2.47) - 3.0) + 7.0006 ; return (value); }; ");
            var result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());
        }

        // Test code at the level of the prog token
        [TestMethod]
        public void TestProgLevelCode()
        {
            // 1 class, 1 program
            var tokens = lexicalAnalyzer.Tokenize("class foo { }; program {  };");
            var result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // 1 program
            tokens = lexicalAnalyzer.Tokenize("program {  };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // 2 classes, 1 program, 2 functions
            tokens = lexicalAnalyzer.Tokenize("class foo { }; class foo { }; program {  }; float foo() {}; float random() {};");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // 2 classes, no program, 2 functions
            tokens = lexicalAnalyzer.Tokenize("class foo { }; class foo { }; float foo() {}; float random() {};");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());
        }

        [TestMethod]
        public void TestFunctionBody()
        {
            // illegal func decleration in program
            var tokens = lexicalAnalyzer.Tokenize("program { int findMin(int array[100]){}; };");
            var result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // func declaration in class
            tokens = lexicalAnalyzer.Tokenize("class foo { int findMin(int array[100]){}; }; program {  };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // func declaration in functionlist
            tokens = lexicalAnalyzer.Tokenize("program {  };  int findMin(int array[100]){};");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // different types of function types
            tokens = lexicalAnalyzer.Tokenize("class foo { int findMin(int array[100]){}; float findMin(int array[100]){}; sometype findMin(int array[100]){};}; program {  };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // missing type
            tokens = lexicalAnalyzer.Tokenize("class foo { findMin(int array[100]){}; }; program {  };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());
        }

        // Test various types of statements
        [TestMethod]
        public void TestStatements()
        {
            // Basic if statement
            var tokens = lexicalAnalyzer.Tokenize("program { if (foo==3) then else; };");
            var result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // If with blocks 
            tokens = lexicalAnalyzer.Tokenize("program { if (foo==3) then { foo = foo + 1; } else { foo = foo == 5; }; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // If with single line blocks 
            tokens = lexicalAnalyzer.Tokenize("program { if (foo==3) then foo = foo + 1; else foo = foo == 5; ; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // If wrong placement of statement block 
            tokens = lexicalAnalyzer.Tokenize("program { if (foo==3) { foo = foo + 1; } then else { foo = foo == 5; }; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // If with assignment instead of expr
            tokens = lexicalAnalyzer.Tokenize("program { if (foo=3) then { foo = foo + 1; } else { foo = foo == 5; }; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Basic for statement
            tokens = lexicalAnalyzer.Tokenize("program { for (int foo = 0; 45 > foo; foo = foo + 1) ; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Basic for statement with block
            tokens = lexicalAnalyzer.Tokenize("program { for (int foo = 0; 45 > foo; foo = foo + 1) { foo = 3; } ; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Basic get statement
            tokens = lexicalAnalyzer.Tokenize("program { get(foo); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Get statement with idnests
            tokens = lexicalAnalyzer.Tokenize("program { get(foo[5][6].bar[1][100]); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Incorrect get statement with relation expression
            tokens = lexicalAnalyzer.Tokenize("program { get(foo[5][6].bar[1][100] > monkey); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Basic put statement
            tokens = lexicalAnalyzer.Tokenize("program { put(100); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Put statement with idnests
            tokens = lexicalAnalyzer.Tokenize("program { put(foo[5][6].bar[1][100] > monkey); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Put statement with mathematic expression
            tokens = lexicalAnalyzer.Tokenize("program { put(1 + 1); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Incorrect put statement with variable
            tokens = lexicalAnalyzer.Tokenize("program { put(foo someid); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Basic return statement
            tokens = lexicalAnalyzer.Tokenize("program { return(50); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Incorrect return statement with variable
            tokens = lexicalAnalyzer.Tokenize("program { return(foo someid); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());
        }

        // Test statements in different locations
        [TestMethod]
        public void TestStatementLocation()
        {
            // Basic if in funcdef
            var tokens = lexicalAnalyzer.Tokenize("program { }; int function() {if (foo==3) then else;};");
            var result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // If statement in class decleration
            tokens = lexicalAnalyzer.Tokenize("class className { int function() {if (foo==3) then else;}; }; program { };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());
        }

    }
}
