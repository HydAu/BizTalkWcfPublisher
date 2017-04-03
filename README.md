# BizTalkWcfPublisher
## (for BizTalk 2010)
Ever publish a WCF front-end for a BizTalk application? Hate using the wizard repeatedly every time you make a change? Well look no further then, because this project is for you! This console application allows you to publish your own WCF front-end using the WcfServiceDescription.xml file generated for you by the wizard (or, perhaps you rolled your own?). It also validates your file for you too with the handy -v switch, which we've thrown in there at no additional charge. 

Included in the source code is the schema for the WcfServiceDescription.xml file, so if you're interested in building your own configuration file from scratch, you might want to look it over.

Or, you can do the following:

* download the 2006 R2 version of the BTS WCF Publishing Wizard at [url:http://www.microsoft.com/en-us/download/details.aspx?id=21973]
* Modify the wizard's configuration file as outlined by Mikael Sand's blog: [url:http://blogical.se/blogs/mikael_sand/archive/2011/10/17/btswcfservicepublishing-and-support-for-net-framework-4.aspx]

Their version accepts more parameters. This console application simply runs with the configuration file you created.

Enjoy!
