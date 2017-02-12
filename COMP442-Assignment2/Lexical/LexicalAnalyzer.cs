using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment1.Lexical
{
    /*
        The lexical analyzer works by using IState classes as
        nodes to recreate a DFA as shown in the report for this
        assignment. Numbered variables refered to the numbered nodes
        of this DFA

        For COMP 442 Assignment 1, Michael Bilinsky 26992358
    */
    public class LexicalAnalyzer
    {
        // The root node of the DFA
        IState root;        

        public LexicalAnalyzer()
        {
            // Initialize matches that will be reused here
            ICharacterMatch letters = new ListCharacterMatch(generateLetters());
            ICharacterMatch nonZero = new ListCharacterMatch(generateNonZeroes());
            ICharacterMatch digit = new ListCharacterMatch(generateDigits());

            ICharacterMatch zero = new SimpleCharacterMatch('0');
            ICharacterMatch period = new SimpleCharacterMatch('.');
            ICharacterMatch equals = new SimpleCharacterMatch('=');
            ICharacterMatch asterisk = new SimpleCharacterMatch('*');
            ICharacterMatch slash = new SimpleCharacterMatch('/');
            ICharacterMatch greaterThan = new SimpleCharacterMatch('>');
            ICharacterMatch lessThan = new SimpleCharacterMatch('<');

            // The error state:
            IState err = new SimpleFinalState(false, "Error", true);

            // Setup comment nodes
            IState s42 = new SimpleFinalState(false, "Asterisk", false);

            IState s41 = new SimpleFinalState(false, "Line comment", false);
            IState s43 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { new SimpleCharacterMatch((char)10), s41} }, err);

            IState s40 = new SimpleIntermediateState();
            s40.addTransition(new SimpleCharacterMatch((char)13), s43);

            

            IState s39 = new SimpleFinalState(false, "Block comment", false);
            IState s37 = new SimpleIntermediateState();
            IState s38 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { slash, s39 } }, s37);

            s37.addTransition(asterisk, s38);

            IState s36 = new SimpleFinalState(true, "Slash", false);
            IState s35 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { asterisk, s37 }, { slash, s40 } }, s36);

            // Brackets
            IState s29 = new SimpleFinalState(false, "Open parenthesis", false);
            IState s30 = new SimpleFinalState(false, "Close parenthesis", false);
            IState s31 = new SimpleFinalState(false, "Open curly bracket", false);
            IState s32 = new SimpleFinalState(false, "Close curly bracket", false);
            IState s33 = new SimpleFinalState(false, "Open square bracket", false);
            IState s34 = new SimpleFinalState(false, "Close square bracket", false);

            // Equality, greater than or equal signs
            IState s28 = new SimpleFinalState(false, "Greater than or equal", false);
            IState s27 = new SimpleFinalState(true, "Greater than", false);
            IState s26 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { equals, s28 } }, s27);
            IState s25 = new SimpleFinalState(false, "Less than or equal", false);
            IState s24 = new SimpleFinalState(false, "Not equal", false);
            IState s23 = new SimpleFinalState(true, "Less than", false);
            IState s22 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { greaterThan, s24 }, { equals, s25} }, s23);
            IState s21 = new SimpleFinalState(false, "Double equals", false);
            IState s20 = new SimpleFinalState(true, "Equals", false);
            IState s19 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { equals, s21 } }, s20);

            // Operators
            IState s14 = new SimpleFinalState(false, "Period", false);
            IState s15 = new SimpleFinalState(false, "Semi-colon", false);
            IState s16 = new SimpleFinalState(false, "Comma", false);
            IState s17 = new SimpleFinalState(false, "Plus", false);
            IState s18 = new SimpleFinalState(false, "Minus", false);

            // Numbers
            IState s12 = new SimpleFinalState(true, "Float (non-zero)", true);
            IState s11 = new SimpleFinalState(true, "Float (zero)", true);
            IState s13 = new SimpleIntermediateState(err);
            IState s10 = new SimpleIntermediateState(s12);
            IState s9 = new SimpleIntermediateState(s11);
            IState s8 = new SimpleIntermediateState(err);

            s13.addTransition(zero, s13);
            s13.addTransition(nonZero, s10);
            s10.addTransition(nonZero, s10);
            s10.addTransition(zero, s13);
            s9.addTransition(zero, s13);
            s9.addTransition(nonZero, s10);
            s8.addTransition(zero, s9);
            s8.addTransition(nonZero, s10);

            IState s7 = new SimpleFinalState(true, "Integer (non-zero)", true);
            IState s5 = new SimpleFinalState(true, "Integer (zero)", true);
            IState s6 = new SimpleIntermediateState(s7);
            s6.addTransition(digit, s6);
            s6.addTransition(period, s8);

            IState s4 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() { { period, s8 } }, s5);

            // Identifiers
            IState s3 = new SimpleFinalState(true, "Identifier", true);
            IState s2 = new SimpleIntermediateState(s3);
            s2.addTransition(letters, s2);
            s2.addTransition(digit, s2);
            s2.addTransition(new SimpleCharacterMatch('_'), s2);

            // Main entrypoint
            IState s1 = new SimpleIntermediateState(new Dictionary<ICharacterMatch, IState>() {
                {letters, s2 },
                {zero, s4 },
                {nonZero, s6 },
                {period, s14 },
                {new SimpleCharacterMatch(';'), s15 },
                {new SimpleCharacterMatch(','), s16 },
                {new SimpleCharacterMatch('+'), s17 },
                {new SimpleCharacterMatch('-'), s18 },
                {equals, s19 },
                {lessThan, s22 },
                {greaterThan, s26 },
                {new SimpleCharacterMatch('('), s29 },
                {new SimpleCharacterMatch(')'), s30 },
                {new SimpleCharacterMatch('{'), s31 },
                {new SimpleCharacterMatch('}'), s32 },
                {new SimpleCharacterMatch('['), s33 },
                {new SimpleCharacterMatch(']'), s34 },
                {slash, s35 },
                {asterisk, s42 }
            }, err);

            s1.addTransition(new ListCharacterMatch(new List<char> { ' ', (char)10 , (char)13 }), s1);

            root = s1;
        }

        // Tokenize an input
        public List<IToken> Tokenize(string input)
        {
            // Ensure that the input ends with a new line
            input += System.Environment.NewLine;

            List <IToken> tokens = new List<IToken>();
            IState state = root;
            int count = 0;
            int tokenStart = 0;
            int line = 1;

            while(count < input.Length)
            {
                char character = input[count];

                // Get the next state for the given character
                state = state.getNextState(character);

                if (state.isFinalState())
                {
                    bool backtrack = state.backTrack();
                    string content = input.Substring(tokenStart, count - tokenStart + (backtrack ? 0 : 1)).Trim();
                    IToken token = state.token();
                    token.setInfo(content, line);

                    tokens.Add(token);

                    state = root;

                    // If we need to backtrack, don't increment the counter
                    if (backtrack)
                    {
                        tokenStart = count;
                        continue;
                    }                    

                    tokenStart = count + 1;
                }

                // Count new lines
                if (character == (char)10)
                {
                    line++;
                }

                count++;
            }

            return tokens;
        }

        // Generate a list of letters
        private List<char> generateLetters()
        {
            List<char> letters = new List<char>();

            for (int i = 0; i < 26; i++)
            {
                letters.Add((char)('a' + i));
            }

            for (int i = 0; i < 26; i++)
            {
                letters.Add((char)('A' + i));
            }

            return letters;
        }

        // Generate a list of non-zero numbers
        private List<char> generateNonZeroes()
        {
            List<char> digits = new List<char>();

            for (int i = 0; i < 9; i++)
            {
                digits.Add((char)('1' + i));
            }

            return digits;
        }

        // Generate a list of numbers including zero
        private List<char> generateDigits()
        {
            List<char> digits = new List<char>();

            for (int i = 0; i < 10; i++)
            {
                digits.Add((char)('0' + i));
            }

            return digits;
        }
    }

    
}
