
using System;
using System.Text.RegularExpressions;

namespace Cesar
{
    public partial class Form1 : Form
    {   //string alphabet uppercase
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Clear_Alphabet();
            //read the text from the textbox1 and convert it to uppercase and to string
            string text = textBox1.Text.ToUpper();

            //encrypt the text using cesar algorithm
            //if checkbox1 is checked
            if (checkBox1.Checked)
            {
                //make new alphabet with the shift
                string newAlphabet = textBox3.Text.ToUpper();
                if (Check_Text(textBox3.Text) == true)
                    return;
                //check key word
                if (Check_Key_Word(textBox3.Text) == true)
                    return;

                //remove letters who are duplicated
                for (int i = 0; i < newAlphabet.Length; i++)
                {
                    if (newAlphabet.IndexOf(newAlphabet[i]) != newAlphabet.LastIndexOf(newAlphabet[i]))
                    {
                        newAlphabet = newAlphabet.Remove(newAlphabet.LastIndexOf(newAlphabet[i]), 1);
                    }
                }
                //complete the new alphabet with the rest of the letters
                for (int i = 0; i < alphabet.Length; i++)
                {
                    if (!newAlphabet.Contains(alphabet[i]))
                    {
                        newAlphabet += alphabet[i];
                    }
                }

                if (Check_Text(text) == true)
                    return;
                //encryt the text using the new alphabet
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == ' ')
                    {
                        textBox2.Text += "";
                        continue;
                    }
                    //get the index of the letter in the alphabet
                    int index = newAlphabet.IndexOf(text[i]);
                    //get the index of the letter in the alphabet after the shift
                    int newIndex = (index + ((int)numericUpDown1.Value)) % 26;
                    //get the letter from the alphabet using the new index
                    char newChar = newAlphabet[newIndex];
                    //add the new letter to the encrypted text
                    textBox2.Text += newChar;

                }
                if (Check_Text(text) == true)
                    return;
                textBox2.Text += Environment.NewLine;
                //print new alphabet
                textBox4.Text = newAlphabet;

            }
            else
            {
                if (Check_Text(text) == true)
                    return;
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == ' ')
                    {
                        textBox2.Text += "";
                        continue;
                    }
                    //get the index of the letter in the alphabet
                    int index = alphabet.IndexOf(text[i]);
                    Create_Cypher_Text(index);

                }
                //insert new line in textbox2
                textBox2.Text += Environment.NewLine;
                textBox4.Text += alphabet;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            Clear_Alphabet();

            //read text from textbox2 and convert it to uppercase and to string
            string text = textBox2.Text.ToUpper();
            if (Check_Text(text) == true)
                return;
            if (checkBox1.Checked)
            {
                //check key word
                if (Check_Key_Word(textBox3.Text) == true)
                    return;
                //make new alphabet with the shift
                string newAlphabet = textBox3.Text.ToUpper();
                if (Check_Text(textBox3.Text) == true)
                    return;
                //remove letters who are duplicated
                for (int i = 0; i < newAlphabet.Length; i++)
                {
                    if (newAlphabet.IndexOf(newAlphabet[i]) != newAlphabet.LastIndexOf(newAlphabet[i]))
                    {
                        newAlphabet = newAlphabet.Remove(newAlphabet.LastIndexOf(newAlphabet[i]), 1);
                    }
                }
                //complete the new alphabet with the rest of the letters
                for (int i = 0; i < alphabet.Length; i++)
                {
                    if (!newAlphabet.Contains(alphabet[i]))
                    {
                        newAlphabet += alphabet[i];
                    }
                }

                //decrypt the text using cesar algorithm with the new alphabet
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == ' ')
                    {
                        textBox1.Text += "";
                        continue;
                    }
                    //get the index of the letter in the alphabet
                    int index = newAlphabet.IndexOf(text[i]);
                    //get the index of the letter in the alphabet after the shift
                    int newIndex = (index - ((int)numericUpDown1.Value)) % 26;
                    if (newIndex < 0)
                    {
                        newIndex = alphabet.Length + newIndex;
                    }
                    //get the letter from the alphabet using the new index
                    char newChar = newAlphabet[newIndex];
                    //add the new letter to the decrypted text
                    textBox1.Text += newChar;

                }
                if (Check_Text(text) == true)
                    return;


                textBox4.Text += newAlphabet;
            }
            //decrypt the text using cesar algorithm
            else
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == ' ')
                    {
                        textBox1.Text += ' ';
                        continue;
                    }
                    //get the index of the letter in the alphabet
                    int index = alphabet.IndexOf(text[i]);
                    //get the index of the letter in the alphabet after the shift
                    int newIndex = (index - ((int)numericUpDown1.Value)) % 26;
                    if (newIndex < 0)
                    {
                        newIndex = alphabet.Length + newIndex;
                    }
                    //get the letter from the alphabet using the new index
                    char newChar = alphabet[newIndex];
                    ////add the new letter to the decrypted text
                    textBox1.Text += newChar;

                }
                if (Check_Text(text) == true)
                    return;

                textBox4.Text += alphabet;
            }

            //insert new line in textbox1
            textBox1.Text += Environment.NewLine;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //clear textbox1,2
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            numericUpDown1.Value = 1;
            checkBox1.Checked = false;
            textBox4.Clear();
        }

        public void Clear_Alphabet()
        {
            textBox4.Clear();
        }
        public void Create_Cypher_Text(int index)
        {
            //get the index of the letter in the alphabet after the shift
            int newIndex = (index + ((int)numericUpDown1.Value)) % 26;
            //get the letter from the alphabet using the new index
            char newChar = alphabet[newIndex];
            //add the new letter to the encrypted text
            textBox2.Text += newChar;
        }
        public static bool Check_Text(string text)
        {
            int count = 0;
            //check if text contains letters A-Z || a-z
            for (int i = 0; i < text.Length; i++)
            {
                // check if text is from A to Z
                if (text[i] >= 'A' && text[i] <= 'Z' || text[i] >= 'a' && text[i] <= 'z' || text[i] == ' ')
                {
                    count++;
                }


            }


            if (count == text.Length)
                return false;
            else
            {
                MessageBox.Show("Enter letters between A-Z or a-z ");
                return true;
            }


        }
        public static bool Check_Key_Word(string text)
        {
            //check if text is more than 7 letters
            if (text.Length + 1 < 7)
            {
                MessageBox.Show("Please enter a key word with more than 7 letters");
                return true;
            }
            return false;
        }
    }

}
//create function
//function to encrypt the text
