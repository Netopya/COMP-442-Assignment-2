using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using COMP442_Assignment2.Lexical;

namespace Assignment2_UnitTests
{
    /*
       A set of unit tests to test the lexical analyzer

       For COMP 442 Assignment 1, Michael Bilinsky 26992358
   */
    [TestClass]
    public class UnitTest1
    {
        LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer();

        [TestMethod]
        public void TestIdentifiers()
        {
            var output = lexicalAnalyzer.Tokenize("HelloWorld");

            Assert.AreEqual("<Identifier (HelloWorld) Line: 1>", output[0].getName());

            output = lexicalAnalyzer.Tokenize("Hello World");

            Assert.AreEqual("<Identifier (Hello) Line: 1>", output[0].getName());
            Assert.AreEqual("<Identifier (World) Line: 1>", output[1].getName());

            output = lexicalAnalyzer.Tokenize("Hello_123");
            Assert.AreEqual("<Identifier (Hello_123) Line: 1>", output[0].getName());

            output = lexicalAnalyzer.Tokenize("123a");
            Assert.AreEqual("<Integer (non-zero) (123) Line: 1>", output[0].getName());
            Assert.AreEqual("<Identifier (a) Line: 1>", output[1].getName());
        }

        [TestMethod]
        public void TestIntegers()
        {
            var output = lexicalAnalyzer.Tokenize("123");
            Assert.AreEqual("<Integer (non-zero) (123) Line: 1>", output[0].getName());

            output = lexicalAnalyzer.Tokenize("0");
            Assert.AreEqual("<Integer (zero) (0) Line: 1>", output[0].getName());

            output = lexicalAnalyzer.Tokenize("123-123");
            Assert.AreEqual("<Integer (non-zero) (123) Line: 1>", output[0].getName());
            Assert.AreEqual("<Minus Line: 1>", output[1].getName());
            Assert.AreEqual("<Integer (non-zero) (123) Line: 1>", output[2].getName());
        }

        [TestMethod]
        public void TestFloats()
        {
            var output = lexicalAnalyzer.Tokenize("1.023");
            Assert.AreEqual("<Float (non-zero) (1.023) Line: 1>", output[0].getName());

            output = lexicalAnalyzer.Tokenize("0.023");
            Assert.AreEqual("<Float (non-zero) (0.023) Line: 1>", output[0].getName());

            output = lexicalAnalyzer.Tokenize("0.0230");
            Assert.AreEqual("<Error (0.0230) Line: 1>", output[0].getName());

            output = lexicalAnalyzer.Tokenize("0.0");
            Assert.AreEqual("<Float (zero) (0.0) Line: 1>", output[0].getName());

            output = lexicalAnalyzer.Tokenize("1.0120ab");
            Assert.AreEqual("<Error (1.0120a) Line: 1>", output[0].getName());
            Assert.AreEqual("<Identifier (b) Line: 1>", output[1].getName());

            output = lexicalAnalyzer.Tokenize(".012");
            Assert.AreEqual("<Period Line: 1>", output[0].getName());
            Assert.AreEqual("<Integer (zero) (0) Line: 1>", output[1].getName());
            Assert.AreEqual("<Integer (non-zero) (12) Line: 1>", output[2].getName());
        }

        [TestMethod]
        public void TestOperators()
        {
            var output = lexicalAnalyzer.Tokenize("== <> < > <= >= ; , .");
            Assert.AreEqual("<Double equals Line: 1>", output[0].getName());
            Assert.AreEqual("<Not equal Line: 1>", output[1].getName());
            Assert.AreEqual("<Less than Line: 1>", output[2].getName());
            Assert.AreEqual("<Greater than Line: 1>", output[3].getName());
            Assert.AreEqual("<Less than or equal Line: 1>", output[4].getName());
            Assert.AreEqual("<Greater than or equal Line: 1>", output[5].getName());
            Assert.AreEqual("<Semi-colon Line: 1>", output[6].getName());
            Assert.AreEqual("<Comma Line: 1>", output[7].getName());
            Assert.AreEqual("<Period Line: 1>", output[8].getName());

            output = lexicalAnalyzer.Tokenize("+ - * / =");
            Assert.AreEqual("<Plus Line: 1>", output[0].getName());
            Assert.AreEqual("<Minus Line: 1>", output[1].getName());
            Assert.AreEqual("<Asterisk Line: 1>", output[2].getName());
            Assert.AreEqual("<Slash Line: 1>", output[3].getName());
            Assert.AreEqual("<Equals Line: 1>", output[4].getName());
        }

        [TestMethod]
        public void TestBrackets()
        {
            var output = lexicalAnalyzer.Tokenize("(){}[]");
            Assert.AreEqual("<Open parenthesis Line: 1>", output[0].getName());
            Assert.AreEqual("<Close parenthesis Line: 1>", output[1].getName());
            Assert.AreEqual("<Open curly bracket Line: 1>", output[2].getName());
            Assert.AreEqual("<Close curly bracket Line: 1>", output[3].getName());
            Assert.AreEqual("<Open square bracket Line: 1>", output[4].getName());
            Assert.AreEqual("<Close square bracket Line: 1>", output[5].getName());

        }

        [TestMethod]
        public void TestComments()
        {
            var output = lexicalAnalyzer.Tokenize("// Some comment");
            Assert.AreEqual("<Line comment Line: 1>", output[0].getName());

            output = lexicalAnalyzer.Tokenize("/* A block comment 123 []{}(***) +- / \\ " + System.Environment.NewLine + " some more text */");
            Assert.AreEqual("<Block comment Line: 2>", output[0].getName());
        }

        [TestMethod]
        public void TestReservedWords()
        {
            var output = lexicalAnalyzer.Tokenize("and not or if then else for class");
            Assert.AreEqual("<and Line: 1>", output[0].getName());
            Assert.AreEqual("<not Line: 1>", output[1].getName());
            Assert.AreEqual("<or Line: 1>", output[2].getName());
            Assert.AreEqual("<if Line: 1>", output[3].getName());
            Assert.AreEqual("<then Line: 1>", output[4].getName());
            Assert.AreEqual("<else Line: 1>", output[5].getName());
            Assert.AreEqual("<for Line: 1>", output[6].getName());
            Assert.AreEqual("<class Line: 1>", output[7].getName());

        }

        [TestMethod]
        public void TestLineNumbering()
        {
            var nl = Environment.NewLine;
            var output = lexicalAnalyzer.Tokenize("Hello" + nl + "World" + nl + "123");
            Assert.AreEqual("<Identifier (Hello) Line: 1>", output[0].getName());
            Assert.AreEqual("<Identifier (World) Line: 2>", output[1].getName());
            Assert.AreEqual("<Integer (non-zero) (123) Line: 3>", output[2].getName());
        }

        [TestMethod]
        public void TestIllegalCharacters()
        {
            var output = lexicalAnalyzer.Tokenize("! _ &");
            Assert.IsTrue(output[0].isError() && output[1].isError() && output[2].isError());
            Assert.AreEqual("<Error (!) Line: 1>", output[0].getName());
            Assert.AreEqual("<Error (_) Line: 1>", output[1].getName());
            Assert.AreEqual("<Error (&) Line: 1>", output[2].getName());
        }
    }
}
