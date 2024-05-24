using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Counting_HTMLtags
{
    public partial class countingHTMLtags : Form
    {
        public countingHTMLtags()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // See https://aka.ms/new-console-template for more information
            string tag;
            tag = textBox2.Text;
            
            string word = tag;
            string sentence = File.ReadAllText(textBox1.Text); 
            int count = 0;
            foreach (Match match in Regex.Matches(sentence, word, RegexOptions.IgnoreCase))
            {
                count++;
            }
           
            label4.Text = count + " times";


        }


        private void button2_Click(object sender, EventArgs e)
        {
            // Opening file explorer window
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|XML (*.xml*)|*.xml*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            // display file location in text box
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fdlg.FileName;
            }
        }


        
    }
}
