namespace BookReader.Models.Book
{
    public class ViewBookViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public int AuthorId { get; set; }
    }
}
