using System;
using System.Linq;

namespace RestAPI.Models
{
    public class Message
    {
        private static readonly string valid = " ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.,;?'-/()\"@=";
        private static readonly string[] morseCodes =
        {
            " /",".-","-...","-.-.","-..",".","..-.","--.","....","..",".---","-.-",".-..","--",
            "-.","---",".--.","--.-",".-.","...","-","..-","...-",".--","-..-","-.--","--..",
            "-----",".----","..---","...--","....-",".....","-....","--...","---..","----.",
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
            char[] uCase = plainMessage.ToUpper().ToCharArray();

            string[] res = uCase.Select((ch, i) =>
            {
                if (!valid.Contains(ch)) return ch.ToString();
                return morseCodes[Array.FindIndex(valid.ToCharArray(), nth => nth == ch)]
                    + ((i != uCase.Length - 1) ? " " : "");
            })
            .ToArray();

            return string.Join("", res);
        }
    }
}