using System;

namespace client;

public enum TicketEvent
{
    TicketBought
} ;

public class TicketEventArgs:EventArgs
{
    private readonly TicketEvent userEvent;
    private readonly Object data;

    public TicketEventArgs(TicketEvent userEvent, object data)
    {
        this.userEvent = userEvent;
        this.data = data;
    }

    public TicketEvent UserEventType
    {
        get { return userEvent; }
    }

    public object Data
    {
        get { return data; }
    }
}