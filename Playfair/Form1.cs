
namespace Playfair
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        string[,] matrix = new string[5, 5];
        string new_Alphabet, temp;

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.ToUpper();

            textBox2.Text = textBox1.Text;
            textBox2.Text = RemoveSpaces(textBox2.Text);
            textBox2.Text = SeparateText(textBox2.Text);


            textBox3.Text = textBox3.Text.ToUpper();
            textBox3.Text = RemoveSpaces(textBox3.Text);
            new_Alphabet = CreateNewAlphabet(textBox3.Text);
            textBox3.Text = new_Alphabet;

            //afisez matricea
            textBox4.Text = "";
            matrix = PrintMatrix(new_Alphabet, textBox4);

            //criptez
            temp = Encrypt(textBox2.Text, matrix);
            textBox2.Text = temp;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox2.Text.ToUpper();

            textBox3.Text = textBox3.Text.ToUpper();
            textBox3.Text = RemoveSpaces(textBox3.Text);
            new_Alphabet = CreateNewAlphabet(textBox3.Text);
            textBox3.Text = new_Alphabet;
            //afisez matricea
            textBox4.Text = "";
            matrix = PrintMatrix(new_Alphabet, textBox4);

            //decriptarea
            temp = Decrypt(textBox2.Text, matrix);

            for (int i = 0; i < temp.Length - 2; i += 2)
            {
                if (temp[i] == temp[i + 2])
                {
                    temp = temp.Remove(i + 1, 1);
                }
            }
            //if last element is x,z,y remove it
            if (temp[temp.Length - 1] == 'X' || temp[temp.Length - 1] == 'Z' || temp[temp.Length - 1] == 'Y')
            {
                temp = temp.Remove(temp.Length - 1);
            }
            textBox1.Text = temp;



        }

        //sterg spatii
        public static string RemoveSpaces(string input)
        {
            return new string(input.ToCharArray()
                           .Where(c => !Char.IsWhiteSpace(c))
                                      .ToArray());
        }
        //inseram X intre literele egale
        public static string SeparateText(string input)
        {
            //if there is the same letter in a pair, add an X beetwen them
            for (int i = 0; i < input.Length - 1; i += 2)
            {
                if (input[i] == input[i + 1])
                {
                    input = input.Insert(i + 1, "X");
                }
            }

            //if the text has an odd number of letters, add an X at the end
            if (input.Length % 2 != 0)
            {
                input += "X";
            }
            return input;
        }
        //stergem dublicatele
        public static string RemoveDublicates(string input)
        {
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (!output.Contains(input[i]))
                {
                    output += input[i];
                }
            }
            return output;
        }
        //creem nou alfabet
        public static string CreateNewAlphabet(string key)
        {
            key = RemoveDublicates(key);
            string alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
            string newAlphabet = key;
            for (int i = 0; i < alphabet.Length; i++)
            {
                if (!newAlphabet.Contains(alphabet[i]))
                {
                    newAlphabet += alphabet[i];
                }
            }
            RemoveDublicates(newAlphabet);
            return newAlphabet;
        }
        //inseram in matrice si afisam
        public static string[,] PrintMatrix(string new_Alphabet, TextBox textBox4)
        {
            string[,] matrix = new string[5, 5];
            int k = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    matrix[i, j] = new_Alphabet[k].ToString();
                    k++;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    textBox4.Text += matrix[i, j] + " ";
                }
                textBox4.Text += Environment.NewLine;
            }
            return matrix;

        }



        //functia pentru playfair de criptare
        public static string Encrypt(string input, string[,] matrix)
        {
            string output = "";
            //encryption
            for (int i = 0; i < input.Length - 1; i += 2)
            {
                int row1 = 0, row2 = 0, col1 = 0, col2 = 0;
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        if (matrix[j, k] == input[i].ToString())
                        {
                            row1 = j;
                            col1 = k;
                        }
                        if (matrix[j, k] == input[i + 1].ToString())
                        {
                            row2 = j;
                            col2 = k;
                        }
                    }
                }
                if (row1 == row2)
                {
                    output += matrix[row1, (col1 + 1) % 5];
                    output += matrix[row2, (col2 + 1) % 5];
                }
                else if (col1 == col2)
                {
                    output += matrix[(row1 + 1) % 5, col1];
                    output += matrix[(row2 + 1) % 5, col2];
                }
                else
                {
                    output += matrix[row1, col2];
                    output += matrix[row2, col1];
                }
            }
            return output;
        }
        //functia pentru playfair de decriptare
        public static string Decrypt(string input, string[,] matrix)
        {
            string output = "";
            //decryption
            for (int i = 0; i < input.Length - 1; i += 2)
            {
                int row1 = 0, row2 = 0, col1 = 0, col2 = 0;
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        if (matrix[j, k] == input[i].ToString())
                        {
                            row1 = j;
                            col1 = k;
                        }
                        if (matrix[j, k] == input[i + 1].ToString())
                        {
                            row2 = j;
                            col2 = k;
                        }
                    }
                }
                if (row1 == row2)
                {
                    output += matrix[row1, (col1 - 1) % 5];
                    output += matrix[row2, (col2 - 1) % 5];
                }
                else if (col1 == col2)
                {
                    output += matrix[(row1 - 1) % 5, col1];
                    output += matrix[(row2 - 1) % 5, col2];
                }
                else
                {
                    output += matrix[row1, col2];
                    output += matrix[row2, col1];
                }
            }
            return output;
        }

    }

}


