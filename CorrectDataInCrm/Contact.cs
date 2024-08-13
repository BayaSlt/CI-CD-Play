namespace CorrectContactOwnerInCrm {
    [System.Runtime.Serialization.DataContractAttribute()]
    public enum ContactState
    {
		
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Aktiv = 0,
		
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Inaktiv = 1,
    }
	
    /// <summary>
    /// Person, mit der eine Unternehmenseinheit eine Geschäftsbeziehung unterhält, wie z.B. ein Kunde, ein Lieferant und ein Kollege.
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("contact")]
    public partial class Contact : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
		
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public Contact() : 
            base(EntityLogicalName)
        {
        }
		
        public const string EntityLogicalName = "contact";
		
        public const string EntitySchemaName = "Contact";
		
        public const string PrimaryIdAttribute = "contactid";
		
        public const string PrimaryNameAttribute = "fullname";
		
        public const string EntityLogicalCollectionName = "contacts";
		
        public const string EntitySetName = "contacts";
		
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
        /// Wählen Sie die übergeordnete Firma oder den übergeordneten Kontakt für den Kontakt aus, um eine direkte Verknüpfung mit zusätzlichen Details wie Finanzinformationen, Aktivitäten und Verkaufschancen bereitzustellen.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("parentcustomerid")]
        public Microsoft.Xrm.Sdk.EntityReference ParentCustomerId
        {
	        get
	        {
		        return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("parentcustomerid");
	        }
	        set
	        {
		        this.OnPropertyChanging("ParentCustomerId");
		        this.SetAttributeValue("parentcustomerid", value);
		        this.OnPropertyChanged("ParentCustomerId");
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