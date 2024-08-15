using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace CorrectContactOwnerInCrm {
    public class ExampleProgram {
        static void Main2(string[] args) {
            
            var contactsToUpdate = new List<Entity>();
            var allAccounts = Helper.RetrieveAll(Helper.Service, "account");
            var allContacts = Helper.RetrieveAll(Helper.Service, "contact");

            var accountDictionary = new Dictionary<Guid, Account>();
            allAccounts.ForEach(account => {
                accountDictionary.Add(account.Id, account.ToEntity<Account>());
            });

            var accountContactDictionary = new Dictionary<Guid, List<Contact>>();
            allContacts.ForEach(entity => {
                var contact = entity.ToEntity<Contact>();
                if(accountContactDictionary.ContainsKey(contact.ParentCustomerId.Id))
                {
                    accountContactDictionary[contact.ParentCustomerId.Id].Add(contact);
                }
                else {
                    if (contact.ParentCustomerId.LogicalName != "account") return;
                    accountContactDictionary.Add(contact.ParentCustomerId.Id, new List<Contact> {contact});
                }
            });
            
            foreach (var keyValuePair in accountContactDictionary) {
                if(!accountDictionary.ContainsKey(keyValuePair.Key)) {
                    Console.WriteLine("Firma nicht in der Liste");
                    return;
                }
                
                var account = accountDictionary[keyValuePair.Key];
                var accountOwner = account.OwnerId;
                
                keyValuePair.Value.ForEach(contact => {
                    if (contact.OwnerId.Id.Equals(accountOwner.Id)) return;
                    
                    var entity = new Entity("contact", contact.Id);
                    entity.Attributes.Add("ownerid", accountOwner);
                    contactsToUpdate.Add(entity);
                });
            }

            var updateStringList = new List<string>();
            var updateCounter = 0;
            contactsToUpdate.ForEach(entity => {
                updateStringList.Add(entity.Id.ToString());
                updateCounter++;

                try {
                    Helper.Service.Update(entity);
                    Console.WriteLine(DateTime.Now + ": " + updateCounter + " von " + contactsToUpdate.Count + ". Kontakt aktualisiert (" + entity.Id + ")");
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                }
            });
            
        }
    }
}