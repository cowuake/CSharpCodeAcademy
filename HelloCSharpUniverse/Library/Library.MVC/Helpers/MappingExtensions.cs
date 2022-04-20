using Library.Core.Entities;
using Library.MVC.Models.Library;

namespace Library.MVC.Helpers
{
    public static class MappingExtensions
    {
        public static CreateEditBookViewModel ToCreateEditBookViewModel(this Book book)
        {
            return new CreateEditBookViewModel()
            {
                Author = book.Author,
                ISBN = book.ISBN,
                Summary = book.Summary,
                Title = book.Title,
                Pages = book.Pages,
                Publisher = book.Publisher,
                Year = book.Year,
                Edition = book.Edition,
                Language = book.Language,
                Note = book.Note,
                BookCategoryId = book.BookCategoryId,
            };
        }

        public static BookDetailsViewModel ToBookDetailsViewModel(this Book book)
        {
            return new BookDetailsViewModel()
            {
                Author = book.Author,
                ISBN = book.ISBN,
                Summary = book.Summary,
                Title = book.Title,
                Pages = book.Pages,
                Publisher = book.Publisher,
                Year = book.Year,
                Edition = book.Edition,
                Language = book.Language,
                Note = book.Note,
            };
        }

        public static Book ToBook(this CreateEditBookViewModel model)
        {
            return new Book
            {
                Author = model.Author,
                Title = model.Title,
                ISBN = model.ISBN,
                Summary = model.Summary,
                Pages = model.Pages,
                Publisher = model.Publisher,
                Year = model.Year,
                Edition = model.Edition,
                Language = model.Language,
                Note = model.Note,
            };
        }

        public static Book ToBook(this BookDetailsViewModel model)
        {
            return new Book
            {
                Author = model.Author,
                Title = model.Title,
                ISBN = model.ISBN,
                Summary = model.Summary,
                Pages = model.Pages,
                Publisher = model.Publisher,
                Year = model.Year,
                Edition = model.Edition,
                Language = model.Language,
                Note = model.Note,
            };
        }
    }
}