using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace CorrectContactOwnerInCrm
{
    class Program
    {
        static void Main(string[] args) {
            var contactsToUpdate = new List<Entity>();
            var allAccounts = Helper.RetrieveAll(Helper.Service, "account");
            
            Console.WriteLine("Got " + allAccounts.Count + " Accounts");
        }
    }
}
