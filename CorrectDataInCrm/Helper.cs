using System;
using System.Collections.Generic;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CorrectContactOwnerInCrm {
    internal static class Helper {
        private static IOrganizationService _service = null;

        public static IOrganizationService Service {
            get {
                if (_service == null) {
                    string connectionString =
                        "AuthType=ClientSecret;ServiceUri=https://kpdevelopment.crm4.dynamics.com/;ClientId=540bea4d-dada-46cb-aaa6-914fb8f5a094;ClientSecret=uOX8Q~OQW0VClfbEv2BvLf6zIek72eDtZUqftbvY;";
                    var serviceClient = new ServiceClient(connectionString);

                    _service = serviceClient;
                    Console.WriteLine($"Got the connection to CRM.");
                }
                return _service;
            }
        }
        
        public static List<Entity> RetrieveAll(IOrganizationService service, string entityLogicalName)
        {
            var pageNumber = 1;
            var pagingCookie = string.Empty;
            var result = new List<Entity>();
 
            EntityCollection resp;

            var query = getQuery(entityLogicalName);
            var opportunityList = service.RetrieveMultiple(query).Entities;
 
            do
            {
                if (pageNumber != 1)
                {
                    query.PageInfo.PageNumber = pageNumber;
                    query.PageInfo.PagingCookie = pagingCookie;
                }
                resp = service.RetrieveMultiple(query);
                if (resp.MoreRecords)
                {
                    pageNumber++;
                    pagingCookie = resp.PagingCookie;
                }
                result.AddRange(resp.Entities);
            }
            while (resp.MoreRecords);
 
            return result;
        }

        static QueryExpression getQuery(string entityLogicalName) {
            // Instantiate QueryExpression query
            var query = new QueryExpression(entityLogicalName);
            //query.AddOrder("pde_name", OrderType.Ascending);
                
            // Add columns to query.ColumnSet
            query.ColumnSet.AddColumns("ownerid");
            if (entityLogicalName == "contact") {
                query.ColumnSet.AddColumns("parentcustomerid");
                query.Criteria.AddCondition("parentcustomerid", ConditionOperator.NotNull);
            }
                
            
            return query;
        }
    }
}