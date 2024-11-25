# GitHubTopStories

Run application and in browser put the url below:

http://localhost:5007/beststories/getbeststories?n=5

n is a parameter to fetch number of best stories.

The property commentCount is not implemented as there is no relationship between stories and comments.
Story fetched from GitHubApi does not have the property "parent" according to GitHub.
There is an example of the story:

{"by":"redm","descendants":1426,"id":42177767,"kids":[42177866,42181921,.....,42180975,42177919],"score":1229,"time":1731968647,"title":"DOJ will push Google to sell off Chrome","type":"story","url":"https://www.bloomberg.com/news/articles/2024-11-18/doj-will-push-google-to-sell-off-chrome-to-break-search-monopoly"}

"....." means some of ids are skipped.