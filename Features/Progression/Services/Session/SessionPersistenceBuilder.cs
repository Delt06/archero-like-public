using System.Collections.Generic;

namespace Features.Progression.Services.Session
{
    public class SessionPersistenceBuilder
    {
        public ISessionPersistenceHandler[] Build() => _handlers.ToArray();

        public SessionPersistenceBuilder AddHandler<T>() where T : struct
        {
            var handler = new SessionPersistenceHandler<T>();
            _handlers.Add(handler);
            return this;
        }

        private readonly List<ISessionPersistenceHandler> _handlers = new List<ISessionPersistenceHandler>();
    }
}