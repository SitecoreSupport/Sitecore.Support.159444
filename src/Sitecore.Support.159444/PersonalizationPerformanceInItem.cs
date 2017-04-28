using Sitecore.Analytics.Reporting;
using Sitecore.Data;
using System;

namespace Sitecore.Support.ContentTesting.Reports
{
    public class PersonalizationPerformanceInItem : Sitecore.ContentTesting.Reports.PersonalizationPerformanceInItem
    {

        private Sitecore.Support.ContentTesting.Analytics.Reporting.RulesetExposureQuery _rulesetExposureQuery;

        public PersonalizationPerformanceInItem(ID contentItemId, ID testId) : base(contentItemId, testId)
        {
        }

        public PersonalizationPerformanceInItem(ID contentItemId, ID testId, ReportDataProviderBase reportDataProvider) : base(contentItemId, testId, reportDataProvider)
        {
        }

        public override long GetRuleReach(Guid ruleSetId, Guid ruleId)
        {
            if (this._rulesetExposureQuery == null || this._rulesetExposureQuery.RulesetId != ruleSetId)
            {
                this._rulesetExposureQuery = this.GetRulesetExposure(ruleSetId);
            }
            return this._rulesetExposureQuery.GetExposure(ruleId);
        }

        public override int GetReachRate(Guid ruleSetId, Guid ruleId)
        {
            if ((this._rulesetExposureQuery == null) || (this._rulesetExposureQuery.RulesetId != ruleSetId))
            {
                this._rulesetExposureQuery = this.GetRulesetExposure(ruleSetId);
            }
            return this._rulesetExposureQuery.GetExposureIntegerPercentage(ruleId);
        }


        protected Sitecore.Support.ContentTesting.Analytics.Reporting.RulesetExposureQuery GetRulesetExposure(Guid rulesetId)
        {
            DateTime utcNow = DateTime.UtcNow;
            DateTime startDate = this.GetStartDate(utcNow);
            Sitecore.Support.ContentTesting.Analytics.Reporting.RulesetExposureQuery query = new Sitecore.Support.ContentTesting.Analytics.Reporting.RulesetExposureQuery(base.ContentItemId, rulesetId, base.ReportDataProvider)
            {
                ReportStart = startDate,
                ReportEnd = utcNow
            };
            query.Execute();
            return query;
        }


    }
}
