namespace BeerTracker.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ValidateSerialNumberAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            string number = value.ToString();

            if (number.Length != 5)
            {
                return false;
            }
            foreach (char symbol in number)
            {
                if (!char.IsDigit(symbol))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
