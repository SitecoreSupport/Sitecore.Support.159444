# Sitecore.Support.159444
Reach metric calculated incorrectly for personalized component in the Personalized experience dialog.

## Solution 1
There a way to address this issue by tweaking the defined query for get exposure metrics.
* Navigate to `/sitecore/system/Settings/Content Testing/Report Queries/Ruleset Exposure` item
* Replace SQL query in the `Data` field to this one:
```SQL
SELECT
	[RuleId],
    SUM([Visitors]) as [Visitors]
FROM 
	[dbo].[Fact_RulesExposure]
WHERE
	[Date] >= @StartDate AND [Date] <= @EndDate AND
	[ItemId] = @ItemId AND
	[RuleSetId] = @RuleSetId
GROUP BY [RuleId]
```

## Solution 2
Use the code based solution located in the branches of this project. You can download already build zip with the DLL and the config file for it.

## License  
This patch is licensed under the [Sitecore Corporation A/S License for GitHub](https://github.com/sitecoresupport/Sitecore.Support.159444/blob/master/LICENSE).  

## Download  
Downloads are available via [GitHub Releases](https://github.com/sitecoresupport/Sitecore.Support.159444/releases).  

[![Github All Releases](https://img.shields.io/github/downloads/SitecoreSupport/Sitecore.Support.159444/total.svg)](https://github.com/SitecoreSupport/Sitecore.Support.159444/releases)
