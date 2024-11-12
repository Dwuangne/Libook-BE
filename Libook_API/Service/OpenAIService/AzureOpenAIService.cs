using Azure.AI.OpenAI;
using Libook_API.Models.DTO;
using OpenAI.Chat;
using Libook_API.Utils;
using Libook_API.Service.BookService;
using SendGrid.Helpers.Mail;

namespace Libook_API.Service.OpenAIService
{
    public class AzureOpenAIService : IAzureOpenAIService
    {
        private readonly AzureOpenAIClient azureOpenAIClient;
        private readonly IConfiguration configuration;
        private readonly IBookService bookService;

        public AzureOpenAIService(AzureOpenAIClient azureOpenAIClient, IConfiguration configuration, IBookService bookService)
        {
            this.azureOpenAIClient = azureOpenAIClient;
            this.configuration = configuration;
            this.bookService = bookService;
        }

        public async Task<IEnumerable<BookResponseDTO?>> GetBookRecommendationAsync(BookResponseDTO bookOrigin, List<BookResponseDTO> potentialBooks)
        {
            var bookOriginDescription = $"I have a book with the following details:\n" +
                $" -Name: {bookOrigin.Name}\n " +
                $" -Price: {bookOrigin.Price}\n " +
                $" -AuthorID: {bookOrigin.AuthorId}\n" +
                $" -SupplierID: {bookOrigin.SupplierId}";

            var bookDescriptions = potentialBooks.Select(b =>
                $"BookID: {b.Id}\n" +
                $" -Name: {b.Name}\n" +
                $" -Price: {b.Price}\n" +
                $" -AuthorID: {b.AuthorId}\n" +
                $" -SupplierID: {b.SupplierId}\n"
            ).ToList();

            var prompt = $"Please suggest up to 8 books from ONLY the list below that are most suitable to the above book. " +
                $"Here is the list of available books:\n" +
                $"{string.Join("\n\n", bookDescriptions)}\n" +
                $"Only return the BookID of each recommended book without any additional information and avoid repeating data. Just this format:\n " +
                $"1.BookID: [book-id]\n" +
                $"2.BookID: [book-id]";
            try
            {
                var modelName = configuration["AzureOpenAI:Model"];
                ChatClient chatClient = azureOpenAIClient.GetChatClient(modelName);
                ChatCompletion completion = chatClient.CompleteChat(
                [
                    // System messages represent instructions or other guidance about how the assistant should behave
                    new SystemChatMessage("You are a helpful assistant that talk like a expert book recommendations."),
                    new UserChatMessage(bookOriginDescription),
                    new AssistantChatMessage("It looks like you've got a book information there! "),
                    new UserChatMessage(prompt),
                ]);
                var content = completion.Content[0].Text;

                List<Guid> bookIds = ConvertString.ExtractBookIds(content);
                List<BookResponseDTO> bookRecomendations = new List<BookResponseDTO>();
                foreach (var bookId in bookIds)
                {
                    var bookResponse = bookService.GetBookByIdAsync(bookId).Result;
                    if (bookResponse != null)
                    {
                        bookRecomendations.Add(bookResponse);
                    }

                }
                return bookRecomendations;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting book recommendations: {ex.Message}");
                return new List<BookResponseDTO>();
            }
        }

        //public async Task<string> BasicChat()
        //{
        //    try
        //    {
        //        ChatClient chatClient = azureOpenAIClient.GetChatClient("gpt-35-turbo");

        //        ChatCompletion completion = chatClient.CompleteChat(
        //        [
        //            // System messages represent instructions or other guidance about how the assistant should behave
        //            new SystemChatMessage("You are a helpful assistant that talks like a pirate."),
        //            // User messages represent user input, whether historical or the most recent input
        //            new UserChatMessage("Hi, can you help me?"),
        //            // Assistant messages in a request represent conversation history for responses
        //            new AssistantChatMessage("Arrr! Of course, me hearty! What can I do for ye?"),
        //            new UserChatMessage("What's the best way to train a parrot?"),
        //        ]);

        //        Console.WriteLine($"{completion.Role}: {completion.Content[0].Text}");

        //        // Return the completion content as a string
        //        return completion.Content[0].Text;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Lỗi mất tiêu rồi. " + e.Message);

        //        // Return the error message as a string
        //        return "An error occurred: " + e.Message;
        //    }
        //}

    }
}