using Secucard.Connect.Client.Config;

namespace Secucard.Connect
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Secucard.Connect.Auth;
    using Secucard.Connect.Client;
    using Secucard.Connect.Net;
    using Secucard.Connect.Net.Rest;
    using Secucard.Connect.Net.Stomp;
    using Secucard.Connect.Product.Document;
    using Secucard.Connect.Product.General;
    using Secucard.Connect.Product.Loyalty;
    using Secucard.Connect.Product.Payment;
    using Secucard.Connect.Product.Service;
    using Secucard.Connect.Product.Smart;
    using Secucard.Connect.Storage;
    using Secucard.Connect.Trace;
    using SmartTransactionsService = Secucard.Connect.Product.Smart.TransactionsService;

    /// <summary>
    /// Actual Client
    /// </summary>
    public class SecucardConnect
    {
        public const string Version = "1.8.1";
        private readonly ClientConfiguration _configuration;
        private readonly ClientContext _context;
        private readonly Dictionary<string, IService> _serviceDict;
        private volatile bool _isConnected;

        #region ### easy access services ### 

        public General General { get; set; }
        public Document Document { get; set; }
        public Payment Payment { get; set; }
        public Loyalty Loyalty { get; set; }
        public Services Services { get; set; }
        public Smart Smart { get; set; }

        #endregion

        public event AuthEvent AuthEvent;
        public event ConnectionStateChangedEventHandler ConnectionStateChangedEvent;

        #region ### Open / Close ###

        /// <summary>
        ///    Authenticate and open internal resources
        /// </summary>
        public void Open()
        {
            if (_isConnected) return;

            try
            {
                var token = _context.TokenManager.GetToken(true);
                SecucardTrace.Info("Auth successfull. Token = {0}", token);
            }
            catch (AuthError)
            {
                Close();
                throw;
            }

            try
            {
                foreach (var channel in _context.Channels.Values) channel.Open();
            }
            catch (Exception)
            {
                Close();
                throw;
            }

            _isConnected = true;
            OnConnectionStateChangedEvent(new ConnectionStateChangedEventArgs {Connected = _isConnected});

            SecucardTrace.Info("Secucard connect client opened.");
        }

        /// <summary>
        ///    Gracefully closes this instance and releases all resources.
        /// </summary>
        public void Close()
        {
            _isConnected = false;
            try
            {
                foreach (var channel in _context.Channels.Values)
                {
                    channel.Close();
                }
            }
            catch (Exception e)
            {
                SecucardTrace.Info(e.ToString());
            }

            OnConnectionStateChangedEvent(new ConnectionStateChangedEventArgs {Connected = _isConnected});

            SecucardTrace.Info("Secucard connect client closed.");
        }

        #endregion

        #region ### Events ###

        private void TokenManagerOnTokenManagerStatusUpdateEvent(object sender, TokenManagerStatusUpdateEventArgs args)
        {
            // Send Events vom Auth Provider an pass it up to client
            OnAuthEvent(new AuthEventArgs {Status = args.Status, DeviceAuthCodes = args.DeviceAuthCodes});
        }

        private void OnAuthEvent(AuthEventArgs args)
        {
            if (AuthEvent != null) AuthEvent(this, args);
        }

        private void OnConnectionStateChangedEvent(ConnectionStateChangedEventArgs args)
        {
            if (ConnectionStateChangedEvent != null) ConnectionStateChangedEvent(this, args);
        }

        private void HandelChannelEvents()
        {
            // Raise connection state changed on client
            // Raise channel events to interested services.
        }

        #endregion

        #region ### Factory Client ###

        private SecucardConnect(ClientConfiguration configuration)
        {
            _configuration = configuration;

            // Setup Trace if directory is there
            if (!string.IsNullOrWhiteSpace(configuration.TraceDir))
            {
                string dir = configuration.TraceDir;
                if (!Path.IsPathRooted(configuration.TraceDir))
                    dir = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                        configuration.TraceDir);
                if (!Directory.Exists(Path.GetDirectoryName(dir))) Directory.CreateDirectory(Path.GetDirectoryName(dir));
                var listener = new SecucardTraceListener(dir) { Name = "SecucardTraceListener" };
                System.Diagnostics.Trace.Listeners.Add(listener);
                SecucardTrace.EmptyLine();
            }

            // Check if Data storage was passed. Otherwise create memory storage
            if (configuration.DataStorage == null)
                configuration.DataStorage = new MemoryDataStorage();

            var context = new ClientContext
            {
                AppId = _configuration.AppId,
            };


            _context = context;

            var authConfig = _configuration.AuthConfig;
            var stompConfig = _configuration.StompConfig;
            var restConfig = _configuration.RestConfig;

            SecucardTrace.Info(string.Format("Creating client with configuration: {0}", configuration));

            if (_configuration.ClientAuthDetails == null)
            {
                //TODO:
            }

            context.DefaultChannel = _configuration.DefaultChannel;

            var restChannel = new RestChannel(restConfig, context);
            context.Channels.Add(ChannelOptions.ChannelRest, restChannel);


            if (_configuration.StompEnabled)
            {
                var stompChannel = new StompChannel(stompConfig, context);
                context.Channels.Add(ChannelOptions.ChannelStomp, stompChannel);
                stompChannel.StompEventArrivedEvent += context.EventDispatcher.StompMessageArrivedEvent;
            }

            var restAuth = new RestAuth(authConfig)
            {
                UserAgentInfo =
                    "secucardconnect-net-" + Version + "/net:" + Environment.OSVersion + " " + Environment.Version
            };
            context.TokenManager = new TokenManager(authConfig, _configuration.ClientAuthDetails, restAuth);
            context.TokenManager.TokenManagerStatusUpdateEvent += TokenManagerOnTokenManagerStatusUpdateEvent;


            // Prepare resource downloader
            ResourceDownloader.Get().Cache = configuration.DataStorage;
            ResourceDownloader.Get().RestChannel = restChannel;


            _serviceDict = ServiceFactory.CreateServices(context);
            WireServiceInstances();
        }

        public static SecucardConnect Create(ClientConfiguration configuration)
        {
            if (configuration == null)
                configuration = ClientConfiguration.GetDefault();

            if (configuration.DataStorage == null)
                throw new Exception("Missing cache implementation found in config.");

            var client = new SecucardConnect(configuration);

            return client;
        }

        #endregion

        public void HandleEvent(object sender, string body)
        {
            _context.EventDispatcher.MessageArrivedEvent(sender, body);
        }

        #region ### Services ###

        public T GetService<T>()
        {
            return (T) _serviceDict[typeof (T).Name];
        }

        private void WireServiceInstances()
        {
            Document = new Document {Uploads = GetService<UploadsService>()};

            General = new General
            {
                News = GetService<NewsService>(),
                Accountdevices = GetService<AccountDevicesService>(),
                Accounts = GetService<AccountsService>(),
                Merchants = GetService<MerchantsService>(),
                Publicmerchants = GetService<PublicMerchantsService>(),
                Stores = GetService<StoresService>(),
                GeneralTransactions = GetService<GeneralTransactionsService>()
            };

            Payment = new Payment
            {
                Containers = GetService<ContainersService>(),
                Customers = GetService<CustomerPaymentService>(),
                Secupaycreditcards = GetService<SecupayCreditcardsService>(),
                Secupaydebits = GetService<SecupayDebitsService>(),
                Secupayprepays = GetService<SecupayPrepaysService>(),
                Contracts = GetService<ContractService>(),
                Secupayinvoices = GetService<SecupayInvoicesService>(),
                PaymentTransactions = GetService<PaymentTransactionsService>()
            };

            Loyalty = new Loyalty
            {
                Cards = GetService<CardsService>(),
                CustomerLoyalty = GetService<CustomerLoyaltyService>(),
                Merchantcards = GetService<MerchantCardsService>(),
            };


            Services = new Services
            {
                Identrequests = GetService<IdentRequestsService>(),
                Identresults = GetService<IdentResultsService>()
            };

            Smart = new Smart
            {
                Checkins = GetService<CheckinsService>(),
                Idents = GetService<IdentsService>(),
                Transactions = GetService<SmartTransactionsService>()
            };
        }

        #endregion
    }
}