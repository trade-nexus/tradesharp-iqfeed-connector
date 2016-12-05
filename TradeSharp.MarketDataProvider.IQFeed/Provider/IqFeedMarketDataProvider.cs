using System;
using System.Threading;
using TraceSourceLogger;
using TradeHub.Common.Core.Utility;
using TradeSharp.MarketDataProvider.IQFeed.ValueObject;
using Constants = TradeHub.Common.Core.Constants;

namespace TradeSharp.MarketDataProvider.IQFeed.Provider
{
    public class IqFeedMarketDataProvider
    {
        private Type _type = typeof(IqFeedMarketDataProvider);

        private AsyncClassLogger _logger;

        /// <summary>
        /// Indicates if the Provider session is connected or not
        /// </summary>
        private bool _isConnected;

        /// <summary>
        /// Contains Provider name used through out TradeSharp
        /// </summary>
        private readonly string _marketDataProviderName;

        /// <summary>
        /// Contains connection details required for communication
        /// </summary>
        private ConnectionParameters _connectionParameters;

        #region Events

        /// <summary>
        /// Fired each time a Logon is arrived
        /// </summary>
        public event Action<string> LogonArrived;

        /// <summary>
        /// Fired each time a Logout is arrived
        /// </summary>
        public event Action<string> LogoutArrived;

        #endregion

        /// <summary>
        /// Responsible for connecting to local IQ Feed Connector application
        /// </summary>
        private ConnectionForm _connectionForm;

        public IqFeedMarketDataProvider()
        {
            // Create object for logging details
            _logger = new AsyncClassLogger("IqFeedDataProvider");
            _logger.SetLoggingLevel();
            _logger.LogDirectory(Constants.DirectoryStructure.MDE_LOGS_LOCATION);

            // Set provider name
            _marketDataProviderName = Constants.MarketDataProvider.IqFeed;

            // Object will be used for connecting to local IQ Feed Connector application
            _connectionForm = new ConnectionForm(_logger);
        }

        #region Connection Methods

        /// <summary>
        /// Indicates if the Provider session is connected or not
        /// </summary>
        public bool IsConnected()
        {
            return _isConnected;
        }

        /// <summary>
        /// Connects/Starts a client
        /// </summary>
        public bool Start()
        {
            try
            {
                // Read account credentials
                ReadConnectionParameters();

                if (_connectionParameters == null)
                {
                    _logger.Info("Connection Parameters unavailable", _type.FullName, "Start");

                    return false;
                }

                // Send credential details to connection form
                _connectionForm.Connect(_connectionParameters.LoginId, _connectionParameters.Password,
                    _connectionParameters.ProductId, _connectionParameters.ProductVersion);

                Thread.Sleep(4000);

                return true;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, _type.FullName, "Start");
            }
            return false;
        }

        /// <summary>
        /// Disconnects/Stops a client
        /// </summary>
        public bool Stop()
        {
            try
            {
                if (_isConnected)
                {
                    //// Close connection with the local IQFeed application
                    //_connectionForm.Stop();

                    _isConnected = false;

                    // Raise event to notify listeners
                    if (LogoutArrived != null)
                    {
                        LogoutArrived(_marketDataProviderName);
                    }

                    return true;
                }

                return false;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, _type.FullName, "Stop");
                return false;
            }
        }

        #endregion

        /// <summary>
        /// Reads required connections pamareters from stored file
        /// </summary>
        private void ReadConnectionParameters()
        {
            // Read parameters from given file
            var parameters = ParameterReader.ReadParamters("IqFeedParams.xml", "IqFeed");

            if (parameters.Count == 4)
            {
                string loginId;
                parameters.TryGetValue("LoginID", out loginId);

                string password;
                parameters.TryGetValue("Password", out password);

                string productionId;
                parameters.TryGetValue("ProductID", out productionId);

                string productVersion;
                parameters.TryGetValue("ProductVersion", out productVersion);

                // Create new object
                _connectionParameters = new ConnectionParameters(loginId, password, productionId, productVersion);

                return;
            }

            _connectionParameters = null;
        }
    }
}
