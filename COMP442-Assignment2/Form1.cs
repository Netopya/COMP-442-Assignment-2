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
        LexicalAnalyzer lexAnalyzer;
        SyntacticAnalyzer synAnalyzer;

        public Form1()
        {
            lexAnalyzer = new LexicalAnalyzer();
            synAnalyzer = new SyntacticAnalyzer();

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var syn = new SyntacticAnalyzer();
            //syn.printPredicts();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "analyzing...";

            var code = textBox1.Text;

            var tokens = lexAnalyzer.Tokenize(code);

            // SHOW LEX ERRORS

            var status = synAnalyzer.analyzeSyntax(tokens.Where(x => !x.isError()).ToList());

            //label1.Text = status ? "Valid Syntax" : "Invalid Syntax";

            Console.WriteLine(string.Join(Environment.NewLine, status.Derivation.Select(x => string.Join(" ", x.Select(y => y.getProductName()).Reverse()))));
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
