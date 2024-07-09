using Model.Interfaces;
using Model.ModelForEF;

namespace ConsoleAppLayer
{
    internal class MethodProgram
    {

        public static IBook InsertBookParameters(bool quantityOn = false, bool optionalParameters = false)
        {
            string bookParameters = "";
            int bookQuantity = 0;

            IBook book = new Book();

            string[] parametersBookInput = { "Title", "AuthorName", "AuthorSurname", "PublishingHouse" };
            Dictionary<string, string> parametersBookOutput = new Dictionary<string, string>();

            foreach (string parameter in parametersBookInput)
            {
                do
                {
                    Console.Write($"Type {parameter}: ");
                    string par = Console.ReadLine();
                    bookParameters = string.IsNullOrWhiteSpace(par) ? null : par;

                } while (string.IsNullOrWhiteSpace(bookParameters) && optionalParameters == false);

                parametersBookOutput.Add(parameter, bookParameters);
            }

            if (quantityOn == true)
            {
                do
                {
                    Console.Write($"Type Quantity:");
                    bookParameters = Console.ReadLine();
                    bookQuantity = (int.TryParse(bookParameters, out bookQuantity)) ? bookQuantity : 0;
                } while (bookQuantity == 0);

                book.Quantity = bookQuantity;
            }

            return new Book() { Title = parametersBookOutput["Title"], AuthorName = parametersBookOutput["AuthorName"], AuthorSurname = parametersBookOutput["AuthorSurname"], PublishingHouse = parametersBookOutput["PublishingHouse"], Quantity = bookQuantity };
        }



        public static string InsertUserParameters(string credential)
        {
            string operationUser = "";
            do
            {
                Console.Write($"What is your {credential}: ");
                operationUser = Console.ReadLine() ?? "";
            } while (string.IsNullOrWhiteSpace(operationUser));

            return operationUser;
        }



        public static bool RequestTheUserForReservationHistory(string filter)
        {
            string operationUser = "";
            do
            {
                Console.WriteLine($"\r\nDo you want to filter booking history by {filter}: (Type Y or N)");
                operationUser = Console.ReadLine() ?? "";

            } while (!operationUser.Equals("Y", StringComparison.OrdinalIgnoreCase) && !operationUser.Equals("N", StringComparison.OrdinalIgnoreCase));

            return (operationUser.Equals("Y", StringComparison.OrdinalIgnoreCase)) ? false : true;
        }



        public static bool CheckListIsNullOrEmpity(IEnumerable<Object> objects)
        {
            bool result = objects?.Any() ?? false;
            if (!result)
            {
                Console.WriteLine("\r\nNot Found!\r\n");
                return false;
            }
            return true;
        }

        public static IBook SetNewValuesBookToModify(IBook bookWithLastValues, IBook bookWithNewValues)
        {
            bookWithNewValues.Title = (string.IsNullOrWhiteSpace(bookWithNewValues.Title)) ? bookWithLastValues.Title : bookWithNewValues.Title;
            bookWithNewValues.AuthorName = (string.IsNullOrWhiteSpace(bookWithNewValues.AuthorName)) ? bookWithLastValues.AuthorName : bookWithNewValues.AuthorName;
            bookWithNewValues.AuthorSurname = (string.IsNullOrWhiteSpace(bookWithNewValues.AuthorSurname)) ? bookWithLastValues.AuthorSurname : bookWithNewValues.AuthorSurname;
            bookWithNewValues.PublishingHouse = (string.IsNullOrWhiteSpace(bookWithNewValues.PublishingHouse)) ? bookWithLastValues.PublishingHouse : bookWithNewValues.PublishingHouse;

            return bookWithNewValues;
        }
    }
}