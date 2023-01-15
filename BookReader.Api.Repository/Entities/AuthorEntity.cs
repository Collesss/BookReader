namespace BookReader.Api.Repository.Entities
{
    public class AuthorEntity
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string AboutAuthor { get; set; }

        public DateTime Birthday { get; set; }
    }
}
