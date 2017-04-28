using Sitecore.Analytics.Reporting;
using Sitecore.ContentTesting.Analytics;
using Sitecore.ContentTesting.Analytics.Reporting;
using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Sitecore.Support.ContentTesting.Analytics.Reporting
{
    public class RulesetExposureQuery : TimeRangeItemBasedReportingQuery
    {
        // Fields
        private readonly Dictionary<Guid, long> _visitors;

        // Methods
        public RulesetExposureQuery(ID itemId, Guid rulesetId, ReportDataProviderBase reportProvider = null) : base(ReportQueryItems.RulesetExposure, reportProvider, null)
        {
            this.ItemId = itemId;
            this.RulesetId = rulesetId;
            this._visitors = new Dictionary<Guid, long>();
        }

        public override void Execute()
        {
            this._visitors.Clear();
            this.TotalVisitors = 0L;
            Dictionary<string, object> parameters = new Dictionary<string, object> {
            {
                "@StartDate",
                base.ReportStart
            },
            {
                "@EndDate",
                base.ReportEnd
            },
            {
                "@ItemId",
                this.ItemId
            },
            {
                "@RuleSetId",
                this.RulesetId
            }
        };
            foreach (DataRow row in base.ExecuteQuery(parameters).Rows)
            {
                Guid key = (Guid)row["RuleId"];
                long num = (long)row["Visitors"];
                if (!this._visitors.ContainsKey(key))
                {
                    this._visitors.Add(key, num);
                }
                else
                {
                    long old = 0;
                    if (this._visitors.TryGetValue(key, out old))
                        this._visitors[key]= old + num;
                }
                this.TotalVisitors += num;
            }
        }

        public long GetExposure(Guid ruleId)
        {
            if (this._visitors.ContainsKey(ruleId))
            {
                return this._visitors[ruleId];
            }
            return 0L;
        }

        public int GetExposureIntegerPercentage(Guid ruleId)
        {
            Sitecore.Support.ContentTesting.Analytics.Calculation.KPIsCalculation calculation = new Sitecore.Support.ContentTesting.Analytics.Calculation.KPIsCalculation();
            List<KeyValuePair<Guid, long>> values = this._visitors.ToList<KeyValuePair<Guid, long>>();
            return calculation.GetIntegerPercentage<Guid>(this.TotalVisitors, values, ruleId);
        }

        // Properties
        public ID ItemId { get; private set; }

        public Guid RulesetId { get; private set; }

        public long TotalVisitors { get; private set; }
    }


}