using addCard_backend.DTOs;

namespace addCard_backend.Utils.Extentions
{
    public static class CreditCardMask
    {
        private static string mask(string creditcard)
        {
            return $"{creditcard.Substring(0, 2)}{"".PadLeft(10, 'X')}{creditcard.Substring(creditcard.Length - 4)}";
        }

        public static IEnumerable<CreditCardIdDTO> MaskCreditCards(this IEnumerable<CreditCardIdDTO> source)
        {
            foreach (CreditCardIdDTO creditcard in source)
            {
                creditcard.CreditcardNumber = mask(creditcard.CreditcardNumber);
                yield return creditcard;
            }
        }

        public static CreditCardIdDTO MaskCreditCard(this CreditCardIdDTO creditcard)
        {
            creditcard.CreditcardNumber = mask(creditcard.CreditcardNumber);
            return creditcard;
        }

        public static IEnumerable<CreditCardDTO> MaskCreditCards(this IEnumerable<CreditCardDTO> source)
        {
            foreach (CreditCardDTO creditcard in source)
            {
                creditcard.CreditcardNumber = mask(creditcard.CreditcardNumber);
                yield return creditcard;
            }
        }

        public static CreditCardDTO MaskCreditCard(this CreditCardDTO creditcard)
        {
            creditcard.CreditcardNumber = mask(creditcard.CreditcardNumber);
            return creditcard;
        }
    }
}

