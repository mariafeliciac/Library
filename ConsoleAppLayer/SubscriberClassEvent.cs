using BusinessLogicLayer;

namespace ConsoleAppLayer
{
    internal class SubscriberClassEvent
    {
        private PublisherClassEvent publisherClass;

        public SubscriberClassEvent(PublisherClassEvent publisher)
        {
            publisherClass = publisher;
            publisherClass.BookDeletedSuccessfully += PrintDeletionSuccessfulMethod;
            publisherClass.BookDeletedUnsuccessfully += PrintDeletionUnsuccessfulMethod;
            publisherClass.BookUpdateSuccessfully += PrintUpdateSuccessfulMethod;
            publisherClass.BookUpdateUnsuccessfully += PrintUpdateUnsuccessfulMethod;
            publisherClass.AddBookSuccessfully += PrintAddBookSuccessfulMethod;
            publisherClass.AddBookQuantitySuccessfully += PrintAddBookQuantitySuccessfulMethod;
        }

        private void PrintAddBookQuantitySuccessfulMethod(object? sender, EventArgs e)
        {
            Console.WriteLine("\r\nThe book was already present and only the quantity has been updated\r\n");
        }

        private void PrintAddBookSuccessfulMethod(object? sender, EventArgs e)
        {
            Console.WriteLine("\r\nThe book has been added!\r\n");
        }

        private void PrintUpdateUnsuccessfulMethod(object sender, EventArgs e)
        {
            Console.WriteLine("\r\nDuplicate book! Editing is not possible!\r\n");
        }

        private void PrintUpdateSuccessfulMethod(object sender, EventArgs e)
        {
            Console.WriteLine("\r\nBook edited successfully!\r\n");
        }

        internal void PrintDeletionUnsuccessfulMethod(object sender, string bookTitle)
        {
            Console.WriteLine($"\r\nIt is not possible to cancel the book {bookTitle} because there are active reservations!\r\n");
        }

        internal void PrintDeletionSuccessfulMethod(object sender, string bookTitle)
        {
            Console.WriteLine($"\r\nThe book {bookTitle} has been deleted!\r\n");
        }

    }
}
