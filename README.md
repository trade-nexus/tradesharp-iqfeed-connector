## IMPORTANT ##
This is an old project and hasn't been actively maintained since 2015 and we're not providing any paid support for it.

### IQFeed-Connector

This project contains a connector of IQFeed for TradeSharp (a C# based Algorithmic Trading Platform).

Find more about TradeSharp [here](https://www.tradesharp.se/).

***

### Getting Started

#### Tools

+ Microsoft Visual Studio 2012 or higher
+ .NET Framework 4.5.1

#### Prerequisites

+ Working TradeSharp Application
+ Demo/Live Account for IQFeed 
+ IQFeed Client 5.2.5.0 [Download Link](http://www.iqfeed.net/index.cfm?displayaction=support&section=download)

#### Setting up The Connector

1. Download the connector zip file or clone the repository.

2. Install IQFeed Client 5.2.5.0.

3. Update *IqFeedParams.xml* file in **TradeSharp.MarketDataProvider.IQFeed** project. The file should have similar content as shown below.
```
<?xml version="1.0" encoding="utf-8" ?>
<IqFeed>
  <LoginID>234567</LoginID>
  <Password>abcdefg</Password>
  <ProductID>PRODUCT_ID_IQFEED</ProductID>
  <ProductVersion>5.2.5.0</ProductVersion>
</IqFeed>
```

**NOTE: LoginID, Password and ProductID are obtained by creating a Demo/Live account for IQFeed. Set ProductVesion as 5.2.5.0**

***

### Compiling Code

**Clean** and **Build** the code from Visual Studio. 

1. Right click on the **TradeSharp.MarketDataProvider.IQFeed** project.

2. Select **Clean**.

3. Right click again and select **Build**.

4. Repeat the steps for **TradeSharp.MarketDataProvider.IQFeed.Tests** project.

***

### Bugs

Please report bugs [here](https://github.com/trade-nexus/bugs)
