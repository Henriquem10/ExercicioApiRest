using BibliotecaApi.Models;

namespace BibliotecaApi.Services
{

    /// <summary>
    /// Provides methods to manage <see cref="Livro"/> entities.
    /// </summary>
    public interface ILivroService
    {
        /// <summary>
        /// Retrieves all books matching the specified filters.
        /// </summary>
        /// <param name="autor">The author of the book.</param>
        /// <param name="titulo">The title of the book.</param>
        /// <param name="ISBN">The ISBN of the book.</param>
        /// <param name="ano">The publication year of the book.</param>
        /// <returns>A list of books matching the filters.</returns>
        Task<List<Livro>> GetAllAsync(string? autor, string? titulo, string? ISBN, int? ano);

        /// <summary>
        /// Retrieves a book by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <returns>The book if found; otherwise, null.</returns>
        Task<Livro?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new book to the collection.
        /// </summary>
        /// <param name="livro">The book to add.</param>
        Task AddAsync(Livro livro);

        /// <summary>
        /// Updates an existing book.
        /// </summary>
        /// <param name="livro">The book with updated information.</param>
        Task UpdateAsync(Livro livro);

        /// <summary>
        /// Deletes a book by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the book to delete.</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Marks a book as borrowed.
        /// </summary>
        /// <param name="livro">The book to borrow.</param>
        Task Emprestar(Livro livro);

        /// <summary>
        /// Marks a book as returned.
        /// </summary>
        /// <param name="livro">The book to return.</param>
        Task Devolver(Livro livro);
    }
}
