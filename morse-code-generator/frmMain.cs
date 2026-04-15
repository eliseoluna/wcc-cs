using System;
//using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class frmMain : Form
    {
        #region declarations section

        public bool blnConverted = false; // Indicates if a Morse conversion has occured
        private static Regex _isChar = new Regex("^[,.?0-9a-zA-Z ]$"); // User input validation

        public string[,] arrMorse = { {",", "--..--" }, {".", ".-.-.-" }, {"?", "..--.." }, {"0", "-----" },
                                    {"1", ".----" }, {"2", "..---"}, {"3", "...--" }, {"4", "....-" },
                                    {"5", "....." }, {"6", "-...."}, {"7", "--..." }, {"8", "---.." },{"9", "----." },
                                    {"A", ".-" }, {"B", "-..." }, {"C", "-.-." }, {"D", "-.." },
                                    {"E", "." }, {"F", "..-."}, {"G", "--."}, {"H", "...."},
                                    {"I", ".." }, {"J", ".---"}, {"K", "-.-"}, {"L", ".-.."},
                                    {"M", "--" }, {"N", "-."}, {"O", "---"}, {"P", ".--."},
                                    {"Q", "--.-" }, {"R", ".-."}, {"S", "..."}, {"T", "-"},
                                    {"U", "..-" }, {"V", "...-"}, {"W", ".--"}, {"X", "-..-"},
                                    {"Y", "-.--" }, {"Z", "--.."}, {" ", "0"} };
        #endregion
        public frmMain()
        {
            InitializeComponent();
        }
        #region frmMain_Load()
        private void frmMain_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region btnExit_Click()
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region btnClear_Click()
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtText.Text = string.Empty;
            lblOutput.Text = string.Empty;
            txtText.Focus();
        }
        #endregion

        #region parseString()
        private void parseString()
        {
            string strPlainText = txtText.Text; // Get the user text - spaces = new line
            string strCode = string.Empty;
            blnConverted = true;

            try
            {
                for (int z = 0; z <= strPlainText.Length - 1; z++) // Count the nubmer of elems needed iun picMorse array
                {
                    strCode = getCode(strPlainText[z]); // Get the numeric equivalent to morse code for each letter
                    if (strCode == "0")
                    {
                        lblOutput.Text += "\n";
                    }
                    else
                    {
                        lblOutput.Text += strCode.PadRight(6);
                    }
                }
            }
            catch (Exception ex) // Handle the exception
            {
                MessageBox.Show("Generic Exception Handler: ", ex.ToString());
            }
        }
        #endregion

        #region getCode()
        private string getCode(char strLetter) // Looks up character and find the morse numveric code from array
        {
            string strReturn = string.Empty;

            for (int y = 0; y <= arrMorse.Length - 1; y++)
            {
                if (arrMorse[y,0] == Convert.ToString(char.ToUpper(strLetter)))
                {
                    strReturn = arrMorse[y,1];
                    break;
                }
            }
            return strReturn;
        }
        #endregion

        #region btnConvert_Click
        private void btnConvert_Click(object sender, EventArgs e)
        {
            parseString();

            if (blnConverted == true)
            {
                btnConvert.Enabled = false; // Set button States
                btnClear.Enabled = true;
            }
        }
        #endregion

        #region IsInputAlphabetical()
        public static bool IsInputAlphabetical(string str)
        {
            Match m = _isChar.Match(str); // Validate
            return m.Success;
        }
        #endregion

        #region txtText_TextChanged()
        private void txtText_TextChanged(object sender, EventArgs e)
        {
            if (txtText.Text.Trim().Length > 0) // Set button button states
            {
                btnClear.Enabled = true;
                btnConvert.Enabled = true;
            }
            else // No chars
            {
                btnClear.Enabled = false;
                btnConvert.Enabled = false;
                btnClear.PerformClick(); // Calls clear button click event
            }
        }
        #endregion

        #region txtText_KeyPress()
        private void txtText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (IsInputAlphabetical(e.KeyChar.ToString()) == false)
            {
                if (Convert.ToInt32(e.KeyChar) != 8) //Allows the backspace - ASCII 8 
                {
                    e.Handled = true;
                }
                else if (Convert.ToInt32(e.KeyChar) == 8 && blnConverted == true)
                    btnClear.PerformClick(); // Calls clear button click event
            }
        }
        #endregion

    }
}
