namespace BookReader.Api.Dto.Author.Request
{
    public class CreateAuthorRequestDto
    {
        public string FullName { get; set; }

        public string AboutAuthor { get; set; }

        public DateTime Birthday { get; set; }
    }
}
