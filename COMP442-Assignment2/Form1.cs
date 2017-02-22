using COMP442_Assignment2.Lexical;
using COMP442_Assignment2.Syntactic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        string outputLocation = Application.StartupPath;

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
            string lexicalTokens = string.Join(System.Environment.NewLine, tokens.Where(x => !x.isError()).Select(x => x.getName()).ToArray());
            string errorTokens = string.Join(System.Environment.NewLine, tokens.Where(x => x.isError()).Select(x => x.getName()).ToArray());

            txtLexTokens.Text = lexicalTokens;
            txtLexErrors.Text = errorTokens;

            outputToFile("outputLexicalTokens.txt", lexicalTokens);
            outputToFile("outputLexicalErrors.txt", errorTokens);


            // SHOW LEX ERRORS

            var result = synAnalyzer.analyzeSyntax(tokens);

            string syntacticDerivation = string.Join(Environment.NewLine, result.Derivation.Select(x => string.Join(" ", x.Select(y => y.getProductName()).Reverse())));
            string syntacticErrors = string.Join(Environment.NewLine, result.Errors);

            txtDerivation.Text = syntacticDerivation;
            txtSynErrors.Text = syntacticErrors;

            outputToFile("outputSyntacticDerivation.txt", syntacticDerivation);
            outputToFile("outputSyntacticErrors.txt", syntacticErrors);

            if (result.Derivation.Any())
            {
                tabControl1.SelectTab(2);
            }

            label1.Text = result.Errors.Any() ? "Status: Error in Syntax" : "Status: Valid Syntax";
        }

        private void outputToFile(string filename, string data)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(outputLocation + "/" + filename, false))
            {
                file.WriteLine(data);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", outputLocation);
        }
    }
}
