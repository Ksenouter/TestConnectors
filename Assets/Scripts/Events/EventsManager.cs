using Connectors;

namespace Events
{
    public static class EventsManager
    {
        
        public static readonly ParamEvent<Connector> ConnectorClick = new ParamEvent<Connector>();
        
    }
}
