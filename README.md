# import.io client

## Pure C# wrapper on [import.io](http://import.io) API service

Client can be easily integrated in your web service for crawling 🕷️ or data analysis 📈 to provide strongly typed entities to work with.

---

## Traits

- Lightweight 🍃
- Simple 🐣
- Extendable 🌍
- **TO BE DONE** :This module can be downloaded as NuGet package 📦

---

## Usage

### Configure client with your API key

```csharp
// Standart configuration for client.
// **BaseUri** = https://import.io
var client = new ImportIOClient("Your API key");
// Change configuration for your client when object is created.
client.ConfigureClient(x => x.BaseUri = new Uri("newUri"));
```

## Data Client ℹ️

### Get data from the extractor's latest crawl

```csharp
// Pass **extractorID** for your specific extractor
// optional : **ContentFormat (JSON by default)**
var extractorId = "9a172415a6-0002-44fb-0ae2-3d12757aea2b"
var content = await client.Data.GetLatestRunResultAsync(extractorId, *ContentFormat.CSV*)
```

## Schedule Client 🗓️

### Get list of all existing schedules

```csharp
// **schedules -** collection of strongly typed Schedule objects
var schedules = await client.Schedule.GetSchedulesAsync()
```

## Generic API call

### Execute your GET call for any other resource with generic implementation

Result is returned as a **string**

Pass required URI as a combination of Field objects, don't forget to configure **BaseUri**

```csharp
// Desired URI to execute GET call: 
// https://store.import.io/crawlRun/{crawlRunId}/_attachment/log/{attachmentId}
client.ConfigureClient(x => x.BaseUri = new Uri("https://store.import.io"));
string crawlRunId = "yourCrawlId";
string attachmentId = "yourAttachId";
var content = await client.GetRawDataAsync(new[]
            {
                new Field("crawlRun"),
                new Field(crawlRunId),
                new Field("_attachment"),
                new Field("log"),
                new Field(attachmentId)
            });
```