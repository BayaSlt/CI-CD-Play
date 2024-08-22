using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CorrectContactOwnerInCrm
{
    class Program
    {
        static void Main(string[] args) {
            // Create a list for the accounts to be updated
            var accountsToUpdate = new List<Entity>();

            // Retrieve all accounts
            var allAccounts = Helper.RetrieveAll(Helper.Service, "account");

            // Loop through all accounts
            foreach (var accountEntity in allAccounts)
            {
                var account = accountEntity.ToEntity<Account>();

                // Create a QueryExpression to retrieve all child accounts for the current account
                var query = new QueryExpression("account")
                {
                    ColumnSet = new ColumnSet(false), // We don't need to retrieve any columns
                    Criteria = new FilterExpression
                    {
                        Conditions = {
                            new ConditionExpression("parentaccountid", ConditionOperator.Equal, account.Id)
                        }
                    }
                };

                // Retrieve child accounts
                var childAccounts = Helper.Service.RetrieveMultiple(query);

                // Check if the number of child accounts is greater than 0
                if (childAccounts.Entities.Count > 0)
                {
                    // If the field is not null, add the account to the update list
                    var parentAccount = new Entity("account", account.Id);
                    parentAccount["kpit_isparentaccount_bit"] = true;
                    accountsToUpdate.Add(parentAccount);
                }
            }

            // Update the accounts
            var updateCounter = 0;
            foreach (var entity in accountsToUpdate)
            {
                updateCounter++;
                try
                {
                    Helper.Service.Update(entity);
                    Console.WriteLine($"{DateTime.Now}: Updated {updateCounter} of {accountsToUpdate.Count} accounts ({entity.Id})");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error updating account {entity.Id}: {e.Message}");
                }
            }
        }
    }
}
