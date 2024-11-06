using static MassTransit.Monitoring.Performance.BuiltInCounters;

namespace Application
{
    public class CommandResult : ICommandResult
    {
        public CommandResult() { }
        public CommandResult(bool success, string? message, object? content)
        {
            if (content == null)
            {
                var mensagem = new
                {
                    mensagem = message
                };
                Content = mensagem;
            }
            else
            {
                Content = content;
            }
            
            Success = success;
        }

        public virtual object? Content { get; set; }
        public virtual bool Success { get; set; }
        public virtual string Mensagem { get; set; }

        public CommandResult(System.Collections.Generic.IReadOnlyCollection<CommandResult> notifications, object content)
        {
            Success = notifications?.Count == 0;
            Mensagem = notifications != null ? string.Join(",", notifications.Select(item => item.Mensagem).ToArray()) : "";
            Content = content;
        }
    }
}
