using COMP442_Assignment2.Lexical;
using COMP442_Assignment2.Syntactic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMP442_Assignment2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var syn = new SyntacticAnalyzer();
            //syn.printPredicts();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LexicalAnalyzer analyzer = new LexicalAnalyzer();

            var code = textBox1.Text;

            var tokens = analyzer.Tokenize(code);

            Console.WriteLine(string.Join(" ", tokens.Where(x => !x.isError()).Select(x => x.getName()).ToArray()));

            var syn = new SyntacticAnalyzer();
            var status = syn.analyzeSyntax(tokens);

            

            Console.WriteLine(status ? "Valid Syntax" : "Invalid Syntax");

        }
    }
}
