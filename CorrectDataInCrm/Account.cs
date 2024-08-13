namespace CorrectContactOwnerInCrm {
    [System.Runtime.Serialization.DataContractAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("account")]
    public partial class Account : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
		
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public Account() : 
            base(EntityLogicalName)
        {
        }
		
        public const string EntityLogicalName = "account";
		
        public const string EntitySchemaName = "Account";
		
        public const string PrimaryIdAttribute = "accountid";
		
        public const string PrimaryNameAttribute = "name";
		
        public const string EntityLogicalCollectionName = "accounts";
		
        public const string EntitySetName = "accounts";
		
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
        public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
        private void OnPropertyChanged(string propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
		
        private void OnPropertyChanging(string propertyName)
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, new System.ComponentModel.PropertyChangingEventArgs(propertyName));
            }
        }
        
        /// <summary>
        /// Geben Sie den Benutzer oder das Team ein, der bzw. das mit der Verwaltung des Datensatzes betraut ist. Dieses Feld wird aktualisiert, wenn der Datensatz einem anderen Benutzer zugewiesen wird.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("ownerid")]
        public Microsoft.Xrm.Sdk.EntityReference OwnerId
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("ownerid");
            }
            set
            {
                this.OnPropertyChanging("OwnerId");
                this.SetAttributeValue("ownerid", value);
                this.OnPropertyChanged("OwnerId");
            }
        }
    }
}