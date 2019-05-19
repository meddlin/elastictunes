using Nest;
using System;

namespace ElasticTunes.DataResources
{
    /// <summary>
    /// Thread-safe singleton for creating a connection to an ElasticSearch backend.
    /// </summary>
    public class ElasticConnection
    {
        private static ElasticConnection _instance;
        private static object objectLock = new object();

        public ElasticClient Client { get; }
        private ConnectionSettings _settings;

        /// <summary>
        /// Private constructor for singleton pattern.
        /// </summary>
        /// <param name="resource">A <c>Uri</c> pointing to the ElasticSearch server instance.</param>
        /// <param name="defaultIndex"></param>
        private ElasticConnection(Uri resource, string index)
        {
            if (resource != null && !string.IsNullOrWhiteSpace(index))
            {
                _settings = _settings ?? new ConnectionSettings(resource).DefaultIndex(index);
                Client = Client ?? new ElasticClient(_settings);
            }
            else
            {
                throw new Exception(
                    $@"Cannot create connection to Elastic server.
                        Resource string: ${resource.OriginalString}
                        Default index: ${index}");
            }
        }

        private ElasticConnection(Uri resource)
        {
            if (resource != null)
            {
                _settings = _settings ?? new ConnectionSettings(resource);
                Client = Client ?? new ElasticClient(_settings);
            }
            else
            {
                throw new Exception($@"Cannot create connection to Elastic server. Resource string: ${resource.OriginalString}");
            }
        }

        /// <summary>
        /// Using a singleton pattern over an object lock to ensure no other instance of <c>ElasticConnection</c>
        /// can be created.
        /// </summary>
        /// <param name="resource"></param>
        /// <returns>The current instance of <c>ElasticConnection</c>.</returns>
        public static ElasticConnection Instance(Uri resource, string index)
        {
            if (_instance == null)
            {
                lock (objectLock)
                {
                    if (_instance == null) _instance = (string.IsNullOrEmpty(index)) ? new ElasticConnection(resource) : new ElasticConnection(resource, index);
                }
            }

            return _instance;
        }
    }
}