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
            label1.Text = "Status: analyzing...";

            var code = txtCodeInput.Text;

            var tokens = lexAnalyzer.Tokenize(code);

            // Seperate the correct and error output
            txtLexTokens.Text = string.Join(System.Environment.NewLine, tokens.Where(x => !x.isError()).Select(x => x.getName()).ToArray());
            txtLexErrors.Text = string.Join(System.Environment.NewLine, tokens.Where(x => x.isError()).Select(x => x.getName()).ToArray());


            // SHOW LEX ERRORS

            var result = synAnalyzer.analyzeSyntax(tokens);

            label1.Text = result.Errors.Any() ? "Status: Error in Syntax" : "Status: Valid Syntax";

            txtDerivation.Text = string.Join(Environment.NewLine, result.Derivation.Select(x => string.Join(" ", x.Select(y => y.getProductName()).Reverse())));
            txtSynErrors.Text = string.Join(Environment.NewLine, result.Errors);

            if(result.Derivation.Any())
            {
                tabControl1.SelectTab(2);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
