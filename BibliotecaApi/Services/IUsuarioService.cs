using BibliotecaApi.DTOs;
using BibliotecaApi.Models;

namespace BibliotecaApi.Services
{
    public interface IUsuarioService
    {
        /// <summary>
        /// Retrieves all users matching the specified filters.
        /// </summary>
        /// <returns>A list of users matching the filters.</returns>
        Task<List<Usuario>> GetAllUsersAsync(string? nome, string? email, string? role);

        /// <summary>
        /// Retrieves a user by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The user if found; otherwise, null.</returns>
        Task<Usuario?> GetUsersByIdAsync(int id);

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="user">The book to add.</param>
        Task<CreateUser> CreateUserAsync(CreateUser user);

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="user">The user with updated information.</param>
        Task UpdateUserAsync(Usuario user);

        /// <summary>
        /// Deletes a User by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete.</param>
        Task DeleteUserAsync(int id);
    }
}
