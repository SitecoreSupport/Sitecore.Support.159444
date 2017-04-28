using Sitecore.ContentTesting;
using Sitecore.ContentTesting.Reports;
using Sitecore.Data;

namespace Sitecore.Support.ContentTesting.Reports
{
    public class ContentTestPerformanceFactory : Sitecore.ContentTesting.Reports.ContentTestPerformanceFactory
    {
             public override IPersonalizationPerformance GetPersonalizationPerformanceInItem(Sitecore.Data.ID contentItemId, Sitecore.Data.ID testId)
        {
            return new Sitecore.Support.ContentTesting.Reports.PersonalizationPerformanceInItem(contentItemId, testId);
        }

        public override IContentTestPerformance GetPerformanceForTest(ITestConfiguration test) =>
           new SitecoreContentTestPerformance(test);

        public override IPersonalizationPerformance GetPersonalizationPerformance(ITestConfiguration test) =>
            new PersonalizationPerformanceInTest(test.ContentItem.ID, test.TestDefinitionItem.ID);

      
        public override IPersonalizationPerformance GetPersonalizationPerformanceInTest(ID contentItemId, ID testId) =>
            new PersonalizationPerformanceInTest(contentItemId, testId);
    }

}

