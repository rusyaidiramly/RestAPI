
namespace RestAPI.Models
{
    public class Message
    {
        private static readonly string[] letterM =//A to Z uppercase 65 to 90 ASCII
        {
            ".-","-...","-.-.","-..",".","..-.","--.","....","..",".---","-.-",".-..","--",
            "-.","---",".--.","--.-",".-.","...","-","..-","...-",".--","-..-","-.--","--.."
        };

        private static readonly string[] digitM =//0 to 9 48 to 57 ASCII
        {
            "-----",".----","..---","...--","....-",".....","-....","--...","---..","----."
        };

        private static readonly string[] punct =
        {
            ".",",",";","?","'","-","/","(",")","\"","@","="
        };

        private static readonly string[] punctM =//punct
        {
            ".-.-.-","--..--","---...","..--..",".----.","-....-","-..-.","-.--.-","-.--.-",".-..-.",".--.-.","-...-"
        };

        private string plainMessage;
        public string MessageID { get; set; }
        public string PlainMessage
        {
            get { return plainMessage; }
            set { plainMessage = value; MorseCode = ToMorseCode(); }
        }
        public string MorseCode { get; private set; }
        public int AuthorID { get; set; }

        public string ToMorseCode()
        {
            string morseCode = "";
            string uCase = plainMessage.ToUpper(), temp, alph, num;
            for (int i = 0; i < uCase.Length; i++)
            {
                temp = uCase[i].ToString();
                for (int j = 0; j <= 26; j++)
                {
                    int re = j + 65;
                    alph = ((char)re).ToString();
                    if (temp == alph)
                    {
                        morseCode += letterM[j];
                        if (i != uCase.Length - 1) morseCode += " ";
                        break;
                    }
                }

                for (int j = 0; j <= 9; j++)
                {
                    int re = j + 48;
                    num = ((char)re).ToString();
                    if (temp == num)
                    {
                        morseCode += digitM[j];
                        if (i != uCase.Length - 1) morseCode += " ";
                        break;
                    }
                }

                for (int j = 0; j < 12; j++)
                {
                    if (temp == punct[j])
                    {
                        morseCode += punctM[j];
                        if (i != uCase.Length - 1) morseCode += " ";
                        break;
                    }
                }

                if (temp == " ")
                {
                    morseCode += "/ ";
                }
            }

            return morseCode;

        }

    }
}