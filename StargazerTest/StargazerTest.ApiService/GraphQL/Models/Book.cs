using GraphQL.Types;

namespace StargazerTest.ApiService.GraphQL.Models;

public class Book
{
    public string Title { get; set; }

    public string AuthorName { get; set; }
}

public class BookType : ObjectType<Book>
{
    protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
    {
        descriptor.Field(t => t.Title);
        descriptor.Field(t => t.AuthorName);
    }
}