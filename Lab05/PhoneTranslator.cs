using System;
using System.Collections.Generic;
using System.Text;

namespace Lab05
{
    public class PhoneTranslator
    {
        string Letters = "ABCDEFGHIJKLMNOPQRSTUVWYZ";
        string Numbers = "2223334445556667778889999";

        public string ToNumber(string alfanumericPhoneNumber)
        {
            var NumericPhoneNumber = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(alfanumericPhoneNumber))
            {
                alfanumericPhoneNumber = alfanumericPhoneNumber.ToUpper();
                foreach (var c in alfanumericPhoneNumber)
                {
                    if ("0123456789".IndexOf(c) >= 0)
                    {
                        NumericPhoneNumber.Append(c);
                    }
                    else
                    {
                        var Index = Letters.IndexOf(c);
                        if (Index >= 0)
                        {
                            NumericPhoneNumber.Append(Numbers[Index]);
                        }
                    }
                }
            }

            return NumericPhoneNumber.ToString();
        }
    }
}
