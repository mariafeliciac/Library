namespace BusinessLogicLayer
{
    public class PublisherClassEvent
    {

        public event EventHandler<string> BookDeletedSuccessfully;

        public event EventHandler<string> BookDeletedUnsuccessfully;

        public event EventHandler BookUpdateSuccessfully;

        public event EventHandler BookUpdateUnsuccessfully;

        public event EventHandler AddBookSuccessfully;

        public event EventHandler AddBookQuantitySuccessfully;


        internal void OnBookDeletedUnsuccessfully(object sender, string bookTitle)
        {
            if (this.BookDeletedUnsuccessfully != null)
            {
                this.BookDeletedUnsuccessfully(sender, bookTitle);
            }
        }

        internal void OnBookDeletedSuccessfully(object sender, string bookTitle)
        {
            if (this.BookDeletedSuccessfully != null)
            {
                this.BookDeletedSuccessfully(sender, bookTitle);
            }
        }

        internal void OnBookUpdateUnsuccessfully(object sender, EventArgs e)
        {
            if (this.BookUpdateUnsuccessfully != null)
            {
                this.BookUpdateUnsuccessfully(sender, e);
            }
        }

        internal void OnBookUpdateSuccessfully(object sender, EventArgs e)
        {
            if (this.BookUpdateSuccessfully != null)
            {
                BookUpdateSuccessfully(sender, e);
            }
        }

        internal void OnAddBookSuccessfully(object sender, EventArgs e)
        {
            if (this.AddBookSuccessfully != null)
            {
                this.AddBookSuccessfully(sender, e);
            }
        }

        internal void OnAddBookQuantitySuccessfully(object sender, EventArgs e)
        {
            if (this.AddBookQuantitySuccessfully != null)
            {
                this.AddBookQuantitySuccessfully(sender, e);
            }
        }
    }
}
