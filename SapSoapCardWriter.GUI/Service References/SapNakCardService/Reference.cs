﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SapSoapCardWriter.GUI.SapNakCardService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:sap-com:document:sap:rfc:functions", ConfigurationName="SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GET")]
    public interface Z_CRM_NEBIH_CARD_FILE_GET {
        
        // CODEGEN: Generating message contract since the operation Z_CRM_NEBIH_CARD_FILE_GET is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="urn:sap-com:document:sap:rfc:functions:Z_CRM_NEBIH_CARD_FILE_GET:Z_CRM_NEBIH_CARD" +
            "_FILE_GETRequest", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GETResponse1 Z_CRM_NEBIH_CARD_FILE_GET(SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GETRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:sap-com:document:sap:rfc:functions:Z_CRM_NEBIH_CARD_FILE_GET:Z_CRM_NEBIH_CARD" +
            "_FILE_GETRequest", ReplyAction="*")]
        System.Threading.Tasks.Task<SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GETResponse1> Z_CRM_NEBIH_CARD_FILE_GETAsync(SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GETRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class Z_CRM_NEBIH_CARD_FILE_GET_DATA : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string cARD_IDField;
        
        private string pASSWDField;
        
        private string uNAMEField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string CARD_ID {
            get {
                return this.cARD_IDField;
            }
            set {
                this.cARD_IDField = value;
                this.RaisePropertyChanged("CARD_ID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string PASSWD {
            get {
                return this.pASSWDField;
            }
            set {
                this.pASSWDField = value;
                this.RaisePropertyChanged("PASSWD");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string UNAME {
            get {
                return this.uNAMEField;
            }
            set {
                this.uNAMEField = value;
                this.RaisePropertyChanged("UNAME");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class Z_CRM_NEBIH_CARD_FILE_GETResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string cARD_NAKField;
        
        private string cARD_NEBIHField;
        
        private string eRRORField;
        
        private string wRITE_KEYField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string CARD_NAK {
            get {
                return this.cARD_NAKField;
            }
            set {
                this.cARD_NAKField = value;
                this.RaisePropertyChanged("CARD_NAK");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string CARD_NEBIH {
            get {
                return this.cARD_NEBIHField;
            }
            set {
                this.cARD_NEBIHField = value;
                this.RaisePropertyChanged("CARD_NEBIH");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string ERROR {
            get {
                return this.eRRORField;
            }
            set {
                this.eRRORField = value;
                this.RaisePropertyChanged("ERROR");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string WRITE_KEY {
            get {
                return this.wRITE_KEYField;
            }
            set {
                this.wRITE_KEYField = value;
                this.RaisePropertyChanged("WRITE_KEY");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class Z_CRM_NEBIH_CARD_FILE_GETRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:sap-com:document:sap:rfc:functions", Order=0)]
        public SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GET_DATA Z_CRM_NEBIH_CARD_FILE_GET;
        
        public Z_CRM_NEBIH_CARD_FILE_GETRequest() {
        }
        
        public Z_CRM_NEBIH_CARD_FILE_GETRequest(SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GET_DATA Z_CRM_NEBIH_CARD_FILE_GET) {
            this.Z_CRM_NEBIH_CARD_FILE_GET = Z_CRM_NEBIH_CARD_FILE_GET;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class Z_CRM_NEBIH_CARD_FILE_GETResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="urn:sap-com:document:sap:rfc:functions", Order=0)]
        public SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GETResponse Z_CRM_NEBIH_CARD_FILE_GETResponse;
        
        public Z_CRM_NEBIH_CARD_FILE_GETResponse1() {
        }
        
        public Z_CRM_NEBIH_CARD_FILE_GETResponse1(SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GETResponse Z_CRM_NEBIH_CARD_FILE_GETResponse) {
            this.Z_CRM_NEBIH_CARD_FILE_GETResponse = Z_CRM_NEBIH_CARD_FILE_GETResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface Z_CRM_NEBIH_CARD_FILE_GETChannel : SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GET, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Z_CRM_NEBIH_CARD_FILE_GETClient : System.ServiceModel.ClientBase<SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GET>, SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GET {
        
        public Z_CRM_NEBIH_CARD_FILE_GETClient() {
        }
        
        public Z_CRM_NEBIH_CARD_FILE_GETClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Z_CRM_NEBIH_CARD_FILE_GETClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Z_CRM_NEBIH_CARD_FILE_GETClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Z_CRM_NEBIH_CARD_FILE_GETClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GETResponse1 SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GET.Z_CRM_NEBIH_CARD_FILE_GET(SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GETRequest request) {
            return base.Channel.Z_CRM_NEBIH_CARD_FILE_GET(request);
        }
        
        public SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GETResponse Z_CRM_NEBIH_CARD_FILE_GET(SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GET_DATA Z_CRM_NEBIH_CARD_FILE_GET1) {
            SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GETRequest inValue = new SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GETRequest();
            inValue.Z_CRM_NEBIH_CARD_FILE_GET = Z_CRM_NEBIH_CARD_FILE_GET1;
            SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GETResponse1 retVal = ((SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GET)(this)).Z_CRM_NEBIH_CARD_FILE_GET(inValue);
            return retVal.Z_CRM_NEBIH_CARD_FILE_GETResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GETResponse1> SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GET.Z_CRM_NEBIH_CARD_FILE_GETAsync(SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GETRequest request) {
            return base.Channel.Z_CRM_NEBIH_CARD_FILE_GETAsync(request);
        }
        
        public System.Threading.Tasks.Task<SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GETResponse1> Z_CRM_NEBIH_CARD_FILE_GETAsync(SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GET_DATA Z_CRM_NEBIH_CARD_FILE_GET) {
            SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GETRequest inValue = new SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GETRequest();
            inValue.Z_CRM_NEBIH_CARD_FILE_GET = Z_CRM_NEBIH_CARD_FILE_GET;
            return ((SapSoapCardWriter.GUI.SapNakCardService.Z_CRM_NEBIH_CARD_FILE_GET)(this)).Z_CRM_NEBIH_CARD_FILE_GETAsync(inValue);
        }
    }
}
