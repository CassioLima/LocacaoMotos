using System.Text.Json.Serialization;

namespace Application
{
    public class CommandResultError : CommandResult, ICommandResult
    {
        public CommandResultError() { }
        public CommandResultError(string? message)
        {
            var mensagem = new
            {
                mensagem = message
            };
            Content = mensagem;
        }

        [JsonIgnore]
        public override bool Success { get; set; }
        public override object? Content { get; set; }
        [JsonIgnore]
        public override string Mensagem { get; set; }
    }
}
