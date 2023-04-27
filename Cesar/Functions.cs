
namespace Cesar
{
    internal class Functions
    {
        public static void Check_Key(int numericUpDown1_Value)
        {
            //check if numericUpDown1 is beetwen 1 and 25
            if (numericUpDown1_Value < 1 || numericUpDown1_Value > 25)
            {
                MessageBox.Show("Please enter a number beetwen 1 and 25");
                return;
            }
        }
    }

}
