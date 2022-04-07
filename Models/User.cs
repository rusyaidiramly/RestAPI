using System;
using System.Globalization;

namespace RestAPI.Models
{
    public class User
    {
        private DateTime _dob;
        private string _nric;
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; private set; }
        public string NRIC
        {
            get { return _nric; }
            set { _nric = value; ExtractDOB(value); }
        }

        public string DOB
        {
            get { return _dob.ToString("dd/MM/yyyy"); }
            private set { ConvertDOB(value); }
        }

        private void ExtractDOB(string nric)
        {
            if (nric.Length != 12) return;

            int year, month, day;
            bool isNumericYear = int.TryParse(nric.Substring(0, 2), out year);
            bool isNumericMonth = int.TryParse(nric.Substring(2, 2), out month);
            bool isNumericDay = int.TryParse(nric.Substring(4, 2), out day);

            if (!isNumericYear || !isNumericMonth || !isNumericDay) return;

            int currentYear = int.Parse(DateTime.Now.ToString("yy"));
            year = CultureInfo.CurrentCulture.Calendar.ToFourDigitYear(year);

            DOB = $"{day}/{month}/{year}";
        }

        private void ConvertDOB(string sDOB)
        {
            string[] dmy = sDOB.Split('/');
            if (dmy.Length != 3) return;

            int dd = int.Parse(dmy[0]);
            int mm = int.Parse(dmy[1]);
            int yy = int.Parse(dmy[2]);

            _dob = new DateTime(yy, mm, dd);

            CalculateAge();
        }

        private void CalculateAge()
        {
            Age = DateTime.Now.Year - _dob.Year;
        }

    }
}
