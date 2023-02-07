namespace PersonaServer.Modules.AccountManagement.Models;

public record ShowMessageViewModel(ShowMessageViewModel.MessageType RelatedMessageType,string Message)
{
    public enum MessageType
    {
        Success=0,
        Warning=1,
        Error=2
    }
};