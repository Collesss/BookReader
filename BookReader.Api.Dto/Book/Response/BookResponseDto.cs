﻿namespace BookReader.Api.Dto.Book.Response
{
    public class BookResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public int AuthorId { get; set; }
    }
}
