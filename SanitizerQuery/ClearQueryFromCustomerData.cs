﻿using CleanQueryFromCustomerData;
using Kusto.Language;
using Kusto.Language.Syntax;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace DllProject
{
    public class ClearQueryFromCustomerData
    {
        /// <summary>
        /// wrapper function-call the all functions in order to find and replace the Customer Data
        /// </summary>
        /// <param name="query">a Kusto query</param>
        /// <returns>if the query is valid, return a clean query-without Customer Data.
        /// else return list of the validate errors</returns>
        public static object ReplaceCustomerDataInQuery(string query)
        {
            var queryValidateErrors = ValidateQuery(query);
            if (queryValidateErrors.Count != 0)
            {
                foreach (var error in queryValidateErrors)
                {
                    if(error.Code.ToString() != "KS204") 
                        return queryValidateErrors.Select(x => x.Message).ToList();
                }
            }
            var customerDataWordsAndAlternateWords = PassQueryFindCustomerData(query);
            return customerDataWordsAndAlternateWords.Count > 0 ? BuildCleanQueryReplaceCustomerData(query, customerDataWordsAndAlternateWords) : query;            
        }

        /// <summary>
        /// Validation checks to the query
        /// </summary>
        /// <param name="query">a Kusto query</param>
        /// <returns>true if the query is valid and false if not</returns>
        private static IReadOnlyList<Diagnostic>? ValidateQuery(string query)
        {
            //func GetDiagnostics find validate errors in KQL query 
            return query == String.Empty ? new List<Diagnostic>() { new Diagnostic("", "The query is empty") } : KustoCode.ParseAndAnalyze(query).GetDiagnostics();
        }

        /// <summary>
        /// pass the query, find the customer data.
        /// </summary>
        /// <param name="query">a Kusto query</param>
        /// <returns>hash table:key-all customer data had found. value-Replacement word</returns>
        private static Hashtable PassQueryFindCustomerData(string query)
        {
            var indexCustomerData = 0;
            var customerDataWordsAndAlternateWords = new Hashtable();
            var parseQuery = KustoCode.Parse(query);
            var regexNumbersContainCustomerData = ConfigRegexCustomerDataInNumber.GetConfigData();
            var lstRegaexNumbersContainCustomerData = new List<string>();
            lstRegaexNumbersContainCustomerData.Add(regexNumbersContainCustomerData.idCheck);
            lstRegaexNumbersContainCustomerData.Add(regexNumbersContainCustomerData.citizenshipNumberCheck);
            lstRegaexNumbersContainCustomerData.Add(regexNumbersContainCustomerData.creditCardCheack);
            lstRegaexNumbersContainCustomerData.Add(regexNumbersContainCustomerData.ssnCheck);
            SyntaxElement.WalkNodes(parseQuery.Syntax,
            n =>
            {
                switch (n.Kind)
                {
                    case SyntaxKind.NameDeclaration:
                        if (customerDataWordsAndAlternateWords[n.GetFirstToken().ValueText] == null)
                            customerDataWordsAndAlternateWords.Add(n.GetFirstToken().ValueText.ToString(), "CustomerData" + indexCustomerData++);
                        break;
                    case SyntaxKind.StringLiteralExpression:
                        if (customerDataWordsAndAlternateWords[n.ToString().Trim()] == null)
                            customerDataWordsAndAlternateWords.Add(n.ToString().Trim(), "'CustomerData" + indexCustomerData++ + "'");
                        break;
                    case SyntaxKind.LongLiteralExpression:
                        var customerDataInNum = false;
                        for (int i = 0; i < lstRegaexNumbersContainCustomerData.Count && !customerDataInNum; i++)
                        {
                            var currentCheckRegex = lstRegaexNumbersContainCustomerData[i];
                            customerDataInNum = new Regex(currentCheckRegex).Match(n.GetFirstToken().ToString().Trim()).Success;
                        }
                        if (customerDataInNum)
                            customerDataWordsAndAlternateWords.Add(n.GetFirstToken().ToString().Trim(), "CustomerData" + indexCustomerData++);
                        break;
                }
            });
            return customerDataWordsAndAlternateWords;
        }


        /// <summary>
        /// Replace the customer data words 
        /// </summary>
        /// <param name="query">a Kusto query</param>
        /// <param name="customerDataWordsAndAlternateWords">list of all customer data had found in this query and the alternate words</param>
        /// <returns>new query without customer data</returns>
        public static string BuildCleanQueryReplaceCustomerData(string query, Hashtable customerDataWordsAndAlternateWords)
        {
            var parseQuery = KustoCode.Parse(query);
            var splitQuery = parseQuery.GetLexicalTokens().ToList();
            var cleanQuery = "";
            var regexNotSpaceBefore = new Regex("^[|.,~;()]");
            splitQuery.ForEach(word => cleanQuery += customerDataWordsAndAlternateWords[word.Text] != null ?
            regexNotSpaceBefore.Match(customerDataWordsAndAlternateWords[word.Text].ToString()).Success ?
            customerDataWordsAndAlternateWords[word.Text].ToString() : " " + customerDataWordsAndAlternateWords[word.Text].ToString() :
            regexNotSpaceBefore.Match(word.Text).Success ? word.Text : " " + word.Text);
            return cleanQuery;
        }
    }
}
