using BookStore.API.Data;
using BookStore.API.DTOs;
using BookStore.API.Schema.Subscriptions;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Schema.Mutations
{
    public class Mutation
    {
        private readonly BookStoreDbContext _context;

        public Mutation(IDbContextFactory<BookStoreDbContext> contextFactory)
        {
            _context = contextFactory.CreateDbContext();
        }

        public async Task<BookResult> CreateBook(BookTypeInput bookInput, [Service] ITopicEventSender topicEventSender)
        {
            var book = new BookDTO()
            {
                Title = bookInput.Title,
                Language = bookInput.Language,
                ThumbnailUrl = bookInput.Thumbnail,
                CategoryId = bookInput.CategoryId
            };

            var result = await _context.Books.AddAsync(book);
            _context.SaveChanges();

            var bookResult = new BookResult()
            {
                Id = book.Id,
                Title = bookInput.Title,
                Language = bookInput.Language,
                CategoryId = bookInput.CategoryId
            };

            await topicEventSender.SendAsync(nameof(Subscription.BookCreated), bookResult);
            return bookResult;
        }

        public async Task<BookResult> UpdateBook(int id, BookTypeInput bookInput, [Service] ITopicEventSender topicEventSender)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                throw new GraphQLException(new Error("Book not found.", "COURSE_NOT_FOUND"));
            }

            book.Title = bookInput.Title;
            book.Language = bookInput.Language;
            book.CategoryId = bookInput.CategoryId;
            book.ThumbnailUrl = bookInput.Thumbnail;

            _context.Update(book);
            _context.SaveChanges();

            var bookResult = new BookResult()
            {
                Id = book.Id,
                Title = book.Title,
                Language = book.Language,
                Thumbnail = book.ThumbnailUrl,
                CategoryId = book.CategoryId,
            };

            string updateBookTopic = $"{book.Id}_{nameof(Subscription.BookUpdated)}";
            await topicEventSender.SendAsync(updateBookTopic, bookResult);

            return bookResult;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                throw new GraphQLException(new Error("Book not found.", "COURSE_NOT_FOUND"));
            }

            _context.Books.Remove(book);
            var result = _context.SaveChanges();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
