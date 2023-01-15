namespace BookReader.Api.Dto.Author.Response
{
    public class AuthorResponseDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string AboutAuthor { get; set; }

        public DateTime Birthday { get; set; }
    }
}
