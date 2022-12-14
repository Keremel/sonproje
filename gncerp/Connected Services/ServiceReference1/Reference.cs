//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceReference1
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://odemeler.garanti.com.tr/", ConfigurationName="ServiceReference1.FirmAccountActivitySoap")]
    public interface FirmAccountActivitySoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://odemeler.garanti.com.tr/services/FirmAccountActivitySoap", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<ServiceReference1.FirmAccountActivityResponse1> FirmAccountActivityAsync(ServiceReference1.FirmAccountActivityRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://odemeler.garanti.com.tr/")]
    public partial class FirmAccountActivity
    {
        
        private SecureToken securetokenField;
        
        private string firmCodeField;
        
        private string startDateField;
        
        private string endDateField;
        
        private string branchNumField;
        
        private string accountNumField;
        
        private string iBANField;
        
        private string transactionIdField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public SecureToken securetoken
        {
            get
            {
                return this.securetokenField;
            }
            set
            {
                this.securetokenField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
        public string FirmCode
        {
            get
            {
                return this.firmCodeField;
            }
            set
            {
                this.firmCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string StartDate
        {
            get
            {
                return this.startDateField;
            }
            set
            {
                this.startDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public string EndDate
        {
            get
            {
                return this.endDateField;
            }
            set
            {
                this.endDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public string BranchNum
        {
            get
            {
                return this.branchNumField;
            }
            set
            {
                this.branchNumField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
        public string AccountNum
        {
            get
            {
                return this.accountNumField;
            }
            set
            {
                this.accountNumField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string IBAN
        {
            get
            {
                return this.iBANField;
            }
            set
            {
                this.iBANField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
        public string TransactionId
        {
            get
            {
                return this.transactionIdField;
            }
            set
            {
                this.transactionIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://odemeler.garanti.com.tr/")]
    public partial class SecureToken
    {
        
        private string userIdField;
        
        private string passwordField;
        
        private string createTimestampField;
        
        private string encodedField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public string UserId
        {
            get
            {
                return this.userIdField;
            }
            set
            {
                this.userIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
        public string Password
        {
            get
            {
                return this.passwordField;
            }
            set
            {
                this.passwordField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string CreateTimestamp
        {
            get
            {
                return this.createTimestampField;
            }
            set
            {
                this.createTimestampField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public string Encoded
        {
            get
            {
                return this.encodedField;
            }
            set
            {
                this.encodedField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://odemeler.garanti.com.tr/")]
    public partial class AccountActivityDetail
    {
        
        private string activityDateField;
        
        private string amountField;
        
        private string balanceField;
        
        private string explanationField;
        
        private string transactionIdField;
        
        private string transactionReferenceIdField;
        
        private string corrBankNumField;
        
        private string corrBranchNumField;
        
        private string corrIBANField;
        
        private string corrVKNField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public string ActivityDate
        {
            get
            {
                return this.activityDateField;
            }
            set
            {
                this.activityDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
        public string Amount
        {
            get
            {
                return this.amountField;
            }
            set
            {
                this.amountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string Balance
        {
            get
            {
                return this.balanceField;
            }
            set
            {
                this.balanceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public string Explanation
        {
            get
            {
                return this.explanationField;
            }
            set
            {
                this.explanationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public string TransactionId
        {
            get
            {
                return this.transactionIdField;
            }
            set
            {
                this.transactionIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
        public string TransactionReferenceId
        {
            get
            {
                return this.transactionReferenceIdField;
            }
            set
            {
                this.transactionReferenceIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public string CorrBankNum
        {
            get
            {
                return this.corrBankNumField;
            }
            set
            {
                this.corrBankNumField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
        public string CorrBranchNum
        {
            get
            {
                return this.corrBranchNumField;
            }
            set
            {
                this.corrBranchNumField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
        public string CorrIBAN
        {
            get
            {
                return this.corrIBANField;
            }
            set
            {
                this.corrIBANField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
        public string CorrVKN
        {
            get
            {
                return this.corrVKNField;
            }
            set
            {
                this.corrVKNField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://odemeler.garanti.com.tr/")]
    public partial class FirmAccountDetail
    {
        
        private string accountNameField;
        
        private string accountTypeField;
        
        private string branchNumField;
        
        private string accountNumField;
        
        private string currencyCodeField;
        
        private string iBANField;
        
        private string openDateField;
        
        private string lastActivityDateField;
        
        private string balanceField;
        
        private string blockedAmountField;
        
        private string availableBalanceField;
        
        private string creditLimitField;
        
        private string availableBalanceWithCreditField;
        
        private AccountActivityDetail[] accountActivitiesField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public string AccountName
        {
            get
            {
                return this.accountNameField;
            }
            set
            {
                this.accountNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
        public string AccountType
        {
            get
            {
                return this.accountTypeField;
            }
            set
            {
                this.accountTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string BranchNum
        {
            get
            {
                return this.branchNumField;
            }
            set
            {
                this.branchNumField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public string AccountNum
        {
            get
            {
                return this.accountNumField;
            }
            set
            {
                this.accountNumField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public string CurrencyCode
        {
            get
            {
                return this.currencyCodeField;
            }
            set
            {
                this.currencyCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string IBAN
        {
            get
            {
                return this.iBANField;
            }
            set
            {
                this.iBANField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public string OpenDate
        {
            get
            {
                return this.openDateField;
            }
            set
            {
                this.openDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
        public string LastActivityDate
        {
            get
            {
                return this.lastActivityDateField;
            }
            set
            {
                this.lastActivityDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
        public string Balance
        {
            get
            {
                return this.balanceField;
            }
            set
            {
                this.balanceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
        public string BlockedAmount
        {
            get
            {
                return this.blockedAmountField;
            }
            set
            {
                this.blockedAmountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=10)]
        public string AvailableBalance
        {
            get
            {
                return this.availableBalanceField;
            }
            set
            {
                this.availableBalanceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=11)]
        public string CreditLimit
        {
            get
            {
                return this.creditLimitField;
            }
            set
            {
                this.creditLimitField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=12)]
        public string AvailableBalanceWithCredit
        {
            get
            {
                return this.availableBalanceWithCreditField;
            }
            set
            {
                this.availableBalanceWithCreditField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AccountActivities", IsNullable=true, Order=13)]
        public AccountActivityDetail[] AccountActivities
        {
            get
            {
                return this.accountActivitiesField;
            }
            set
            {
                this.accountActivitiesField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://odemeler.garanti.com.tr/")]
    public partial class FirmAccountActivityResponse
    {
        
        private string returnValField;
        
        private string messageTextField;
        
        private string currentDateTimeField;
        
        private string customerNumField;
        
        private FirmAccountDetail[] firmAccountsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public string ReturnVal
        {
            get
            {
                return this.returnValField;
            }
            set
            {
                this.returnValField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
        public string MessageText
        {
            get
            {
                return this.messageTextField;
            }
            set
            {
                this.messageTextField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string CurrentDateTime
        {
            get
            {
                return this.currentDateTimeField;
            }
            set
            {
                this.currentDateTimeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public string CustomerNum
        {
            get
            {
                return this.customerNumField;
            }
            set
            {
                this.customerNumField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FirmAccounts", IsNullable=true, Order=4)]
        public FirmAccountDetail[] FirmAccounts
        {
            get
            {
                return this.firmAccountsField;
            }
            set
            {
                this.firmAccountsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FirmAccountActivityRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://odemeler.garanti.com.tr/", Order=0)]
        public ServiceReference1.FirmAccountActivity FirmAccountActivity;
        
        public FirmAccountActivityRequest()
        {
        }
        
        public FirmAccountActivityRequest(ServiceReference1.FirmAccountActivity FirmAccountActivity)
        {
            this.FirmAccountActivity = FirmAccountActivity;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FirmAccountActivityResponse1
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://odemeler.garanti.com.tr/", Order=0)]
        public ServiceReference1.FirmAccountActivityResponse FirmAccountActivityResponse;
        
        public FirmAccountActivityResponse1()
        {
        }
        
        public FirmAccountActivityResponse1(ServiceReference1.FirmAccountActivityResponse FirmAccountActivityResponse)
        {
            this.FirmAccountActivityResponse = FirmAccountActivityResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface FirmAccountActivitySoapChannel : ServiceReference1.FirmAccountActivitySoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class FirmAccountActivitySoapClient : System.ServiceModel.ClientBase<ServiceReference1.FirmAccountActivitySoap>, ServiceReference1.FirmAccountActivitySoap
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public FirmAccountActivitySoapClient() : 
                base(FirmAccountActivitySoapClient.GetDefaultBinding(), FirmAccountActivitySoapClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.FirmAccountActivitySoap.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public FirmAccountActivitySoapClient(EndpointConfiguration endpointConfiguration) : 
                base(FirmAccountActivitySoapClient.GetBindingForEndpoint(endpointConfiguration), FirmAccountActivitySoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public FirmAccountActivitySoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(FirmAccountActivitySoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public FirmAccountActivitySoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(FirmAccountActivitySoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public FirmAccountActivitySoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ServiceReference1.FirmAccountActivityResponse1> ServiceReference1.FirmAccountActivitySoap.FirmAccountActivityAsync(ServiceReference1.FirmAccountActivityRequest request)
        {
            return base.Channel.FirmAccountActivityAsync(request);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.FirmAccountActivityResponse1> FirmAccountActivityAsync(ServiceReference1.FirmAccountActivity FirmAccountActivity)
        {
            ServiceReference1.FirmAccountActivityRequest inValue = new ServiceReference1.FirmAccountActivityRequest();
            inValue.FirmAccountActivity = FirmAccountActivity;
            return ((ServiceReference1.FirmAccountActivitySoap)(this)).FirmAccountActivityAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.FirmAccountActivitySoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.FirmAccountActivitySoap))
            {
                return new System.ServiceModel.EndpointAddress("https://inboundrstintws.garanti.com.tr/services/FirmAccountActivitySoap");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return FirmAccountActivitySoapClient.GetBindingForEndpoint(EndpointConfiguration.FirmAccountActivitySoap);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return FirmAccountActivitySoapClient.GetEndpointAddress(EndpointConfiguration.FirmAccountActivitySoap);
        }
        
        public enum EndpointConfiguration
        {
            
            FirmAccountActivitySoap,
        }
    }
}
