using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using DllProject;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LetQueryReplaceCustomerData()
        {
            var letQuery = "let myCulmn = range x from 1 to 10 step 1";
            var actualLetQuery = " let CustomerData0 = range CustomerData1 from 1 to 10 step 1 ";
            Assert.AreEqual(ClearQueryFromCustomerData.ReplaceCustomerDataInQuery(letQuery), actualLetQuery);
        }
        [TestMethod]
        public void AsQueryReplaceCustomerData()
        {
            var LookupQuery = "union withsource = TableName(range x from 1 to 10 step 1 | as T1), (range x from 1 to 10 step 1 | as T2)";
            var actualLookupQuery = " union CustomerData0 = CustomerData1( range CustomerData2 from 1 to 10 step 1| as CustomerData3),( range CustomerData2 from 1 to 10 step 1| as CustomerData4) ";
            Assert.AreEqual(ClearQueryFromCustomerData.ReplaceCustomerDataInQuery(LookupQuery), actualLookupQuery);
        }
        [TestMethod]
        public void PatternStatementReplaceCustomerData()
        {
            var patternQuery = "declare pattern app = (applicationId: int)[eventNum: int]{('ApplicationX').['StopEvents'] = { database('AppX').Events | where EventType == 'StopEvent' };};";
            var actualPatternQuery = " declare pattern CustomerData0 =( CustomerData1 : int) [ CustomerData2 : int ] {( 'CustomerData3'). [ 'CustomerData4' ] = { database( 'CustomerData5'). Events| where EventType == 'CustomerData6' }; }; ";
            Assert.AreEqual(ClearQueryFromCustomerData.ReplaceCustomerDataInQuery(patternQuery), actualPatternQuery);
        }
        [TestMethod]
        public void RangeOperatorReplaceCustomerData()
        {
            var rangeQuery = "let myCulmn = range x from 1 to 10 step 1";
            var actualRangeQuery = " let CustomerData0 = range CustomerData1 from 1 to 10 step 1 ";
            Assert.AreEqual(ClearQueryFromCustomerData.ReplaceCustomerDataInQuery(rangeQuery), actualRangeQuery);
        }
        [TestMethod]
        public void NameAndTypeDeclarationReplaceCustomerData()
        {
            var namedAndTypeDeclareQuery = "let weekday = (day:int) {case(day == 0, 'Sun', day == 1, 'Mon', 'Sat')};";
            var actualNamedAndTypeDeclareQuery = " let CustomerData0 =( CustomerData1 : int) { case( CustomerData1 == 0, 'CustomerData2', CustomerData1 == 1, 'CustomerData3', 'CustomerData4') }; ";
            Assert.AreEqual(ClearQueryFromCustomerData.ReplaceCustomerDataInQuery(namedAndTypeDeclareQuery), actualNamedAndTypeDeclareQuery);
        }
        [TestMethod]
        public void SerializeReplaceCustomerData()
        {
            var serializeQuery = "T | serialize rn=row_number()";
            var actualserializeQuery = " T| serialize CustomerData0 = row_number() ";
            Assert.AreEqual(ClearQueryFromCustomerData.ReplaceCustomerDataInQuery(serializeQuery), actualserializeQuery);
        }
        [TestMethod]
        public void ProjectReplaceCustomerData()
        {
            var projectQuery = "T| project MyText = strcat('MachineLearningX', tostring(toint(rand(10))))";
            var actualProjectQuery = " T| project CustomerData0 = strcat( 'CustomerData1', tostring( toint( rand( 10)))) ";
            Assert.AreEqual(ClearQueryFromCustomerData.ReplaceCustomerDataInQuery(projectQuery), actualProjectQuery);
        }
        [TestMethod]
        public void ProjectRenameReplaceCustomerData()
        {
            var projectRenameQuery = "T | project-rename newColumnName = columnName";
            var actualProjectRenameQuery = " T| project-rename CustomerData0 = columnName ";
            Assert.AreEqual(ClearQueryFromCustomerData.ReplaceCustomerDataInQuery(projectRenameQuery), actualProjectRenameQuery);
        }
        [TestMethod]
        public void SummarizeReplaceCustomerData()
        {
            var summarizeQuery = "Sales | summarize NumTransactions=count()";
            var actualSummarizeQuery = " Sales| summarize CustomerData0 = count() ";
            Assert.AreEqual(ClearQueryFromCustomerData.ReplaceCustomerDataInQuery(summarizeQuery), actualSummarizeQuery);
        }
        [TestMethod]
        public void PrintReplaceCustomerData()
        {
            var printQuery = "print x=1";
            var actualPrintQuery = " print CustomerData0 = 1 ";
            Assert.AreEqual(ClearQueryFromCustomerData.ReplaceCustomerDataInQuery(printQuery), actualPrintQuery);
        }
        [TestMethod]
        public void ExtendReplaceCustomerData()
        {
            var extendQuery = "T | extend letter = 'a'";
            var actualExtendQuery = " T| extend CustomerData0 = 'CustomerData1' ";
            Assert.AreEqual(ClearQueryFromCustomerData.ReplaceCustomerDataInQuery(extendQuery), actualExtendQuery);
        }
        [TestMethod]
        public void ParseReplaceCustomerData()
        {
            var parseQuery = "T | parse Text with 'ActivityName = ' name ', ActivityType = ' type";
            var actualParseQuery = " T| parse Text with 'CustomerData0' CustomerData1 'CustomerData2' CustomerData3 ";
            Assert.AreEqual(ClearQueryFromCustomerData.ReplaceCustomerDataInQuery(parseQuery), actualParseQuery);
        }
        [TestMethod]
        public void StringReplaceCustomerData()
        {
            var stringQuery = "let weekday = 'Sunday'";
            var actualStringQuery = " let CustomerData0 = 'CustomerData1' ";
            Assert.AreEqual(ClearQueryFromCustomerData.ReplaceCustomerDataInQuery(stringQuery), actualStringQuery);
        }
        [TestMethod]
        public void NumberNotCDReplaceCustomerData()
        {
            var stringQuery = "let weekday = 1";
            var actualStringQuery = " let CustomerData0 = 1 ";
            Assert.AreEqual(ClearQueryFromCustomerData.ReplaceCustomerDataInQuery(stringQuery), actualStringQuery);
        }
        [TestMethod]
        public void NumberCDReplaceCustomerData()
        {
            var stringQuery = "let id = '324243021'";
            var actualStringQuery = " let CustomerData0 = 'CustomerData1' ";
            Assert.AreEqual(ClearQueryFromCustomerData.ReplaceCustomerDataInQuery(stringQuery), actualStringQuery);
        }
        [TestMethod]
        public void LongQueryReplaceCustomerData()
        {
            var longQuery = "let RecordsThatMeetConnectionCondition_0 = Connections| where DestinationEntityType =~'AadIdentity'| where DefinitionKey =~'bf82a334-13b6-ca57-ea75-096fc2ffce50' and DestinationEntityType =~'AadIdentity'| project-away FinalOperand *| project-rename Connection_0 = Title, Destination_EntityType_0 = DestinationEntityType, Destination_EntityName_0 = DestinationEntityName, Destination_EntityIdentifiers_0 = DestinationEntityIdentifiers| extend FinalOperand = true;let RecordsThatMeetHasCondition_0 = Insights| where DefinitionKey =~'1f24d55a-df0f-4772-9090-4629c2d6bfff'| summarize InsightsTitle_Labels = make_set(Title) by InternalEntityId| extend FinalOperand = true;Entities| where EntityType =~'microsoft.compute/virtualmachines'| lookup kind = leftouter RecordsThatMeetHasCondition_0 on InternalEntityId| join kind = leftouter RecordsThatMeetConnectionCondition_0 on $left.InternalEntityId == $right.SourceInternalEntityId| extend InsightsTitle_Labels_ = InsightsTitle_Labels| where FinalOperand and FinalOperand1| project-away InsightsTitle_Labels, Source *| project-keep EntityName, EntityType, EntityIdentifiers, CloudProvider, Connection *, Destination_EntityType *, Destination_EntityName *, Destination_EntityIdentifiers *, Labels *| order by EntityName desc ";
            var actualLongQuery = " let CustomerData0 = Connections| where DestinationEntityType =~ 'CustomerData1'| where DefinitionKey =~ 'CustomerData2' and DestinationEntityType =~ 'CustomerData1'| project-away CustomerData7 *| project-rename CustomerData3 = Title, CustomerData4 = DestinationEntityType, CustomerData5 = DestinationEntityName, CustomerData6 = DestinationEntityIdentifiers| extend CustomerData7 = true; let CustomerData8 = Insights| where DefinitionKey =~ 'CustomerData9'| summarize CustomerData10 = make_set( Title) by InternalEntityId| extend CustomerData7 = true; Entities| where EntityType =~ 'CustomerData11'| lookup CustomerData12 = leftouter CustomerData8 on InternalEntityId| join CustomerData12 = leftouter CustomerData0 on $left. InternalEntityId == $right. SourceInternalEntityId| extend CustomerData13 = CustomerData10| where CustomerData7 and FinalOperand1| project-away CustomerData10, Source *| project-keep EntityName, EntityType, EntityIdentifiers, CloudProvider, Connection *, Destination_EntityType *, Destination_EntityName *, Destination_EntityIdentifiers *, Labels *| order by EntityName desc ";
            Assert.AreEqual(ClearQueryFromCustomerData.ReplaceCustomerDataInQuery(longQuery), actualLongQuery);

        }
    }
}