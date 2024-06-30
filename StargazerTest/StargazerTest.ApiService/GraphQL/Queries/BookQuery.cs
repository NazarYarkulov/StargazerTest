using System.Data;
using StargazerTest.ApiService.GraphQL.Models;

namespace StargazerTest.ApiService.GraphQL.Queries;

public class BookQuery
{
    private readonly IDbConnection dbConnection;
        
    public BookQuery(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }
    public async Task<IEnumerable<Book>> GetBook()
    {
        var books = new List<Book>();
        var command = dbConnection.CreateCommand();
        command.CommandText = "SELECT Title, AuthorName FROM Books";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            books.Add(new Book
            {
                Title = reader.GetString(0),
                AuthorName = reader.GetString(1)
            });
        }

        return books;
    }
}