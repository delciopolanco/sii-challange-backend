using System;
namespace addCard_backend.DTOs
{
	public class PaginatedListDTO<T>
	{
        public int Total { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public List<T> List { get; set; }

    }
}

