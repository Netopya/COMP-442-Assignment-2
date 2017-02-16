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
    }
}
