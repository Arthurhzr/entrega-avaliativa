using System.Collections.Concurrent;
using System.Collections.Generic;

public class MessagesStorage
{
    private readonly ConcurrentBag<string> _messages = new();

    public void AddMessage(string msg)
    {
        _messages.Add(msg);
    }

    public IEnumerable<string> GetAll()
    {
        return _messages;
    }
}
