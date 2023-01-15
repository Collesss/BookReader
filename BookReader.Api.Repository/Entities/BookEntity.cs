namespace BookReader.Api.Repository.Entities
{
    public class BookEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public int AuthorId { get; set; }
    }
}
