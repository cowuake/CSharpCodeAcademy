using Library.Core.Entities;
using Library.MVC.Models.BookGenres;
using Library.MVC.Models.Library;
using System.Collections.Generic;
using System.Linq;

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
                BookGenreId = book.BookGenreId,
            };
        }

        public static BookDetailsViewModel ToBookDetailsViewModel(this Book book, BookGenre genre)
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
                Genre = genre.Name, // The genre name is what we want to visualize among book details
            };
        }

        public static IEnumerable<LibraryViewModel> ToEnumerableLibraryViewModel(this IEnumerable<Book> books)
        {
            return books.Select(book => new LibraryViewModel
            {
                Author = book.Author,
                ISBN = book.ISBN,
                Title = book.Title,
                Genre = book.BookGenre.Name,
            });
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
                BookGenreId = model.BookGenreId,
            };
        }

        public static IEnumerable<ListBookGenresViewModel> ToEnumerableListBookCategoryViewModel(this IEnumerable<BookGenre> genres)
        {
            return genres.Select(bc => new ListBookGenresViewModel
            {
                BooksCount = bc.Books.Count(),
                Name = bc.Name,
                Id = bc.Id
            });
        }

        public static BookGenre ToBookGenre(this CreateEditBookGenreViewModel model)
        {
            return new BookGenre
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static CreateEditBookGenreViewModel ToCreateEditBookGenreViewModel(this BookGenre genre)
        {
            return new CreateEditBookGenreViewModel()
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }
        public static DetailsBookGenreViewModel ToDetailsBookCategoryViewModel(this BookGenre bookCategory, IEnumerable<Book> books)
        {
            return new DetailsBookGenreViewModel
            {
                Id = bookCategory.Id,
                Name = bookCategory.Name,
                Books = books.ToEnumerableLibraryViewModel()
            };
        }
    }
}