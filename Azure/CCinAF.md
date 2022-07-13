# How to use Feature Flags in Function as a Service - FaaS?
FaaS providers: Azure Functions or AWS Lambda
## What is FaaS and Azure Functions?
FaaS providers serve with event-driven services in a "serverless" environment which abstracts the servers away from the developer, saving the hustle and quite possibly the resources as these "functions" only cost cents per usage.
## Challanges when using FaaS
Stateless and application live for a short period of time. Why is caching a problem here? - Because developers have no power over when the application stops so the cached items are deleted. Next time the application starts, the cache is empty.

FaaS has both pros and cons to take into account when it comes to the practice.

Why you should consider the usage of FaaS:
* Easy scalability:
    The vendor manages the scaling, and it happens automatically. The developer responsibility is practically eliminated.
* Simplified code:
    You can either upload one function at a time, or your entire application. It also gives you the freedom to write backend code for independent functions.
* Decreased latency:
    FaaS vendors have the capabilitty to run applications closer to end users, eliminating the long distances between the user and the server.

Pro And Con:
* Costs:
    FaaS providers typically provide Pay-per-Use service, and costs a few cents per usage, making it a dirt cheap solution in some cases, however, depending on the processes you are running, this price can scale up to much higher than dedicated servers.

You have to make some trade-offs, when it comes to FaaS:
*   Vendor lock-in:
    If you build your application on a FaaS platform, you have to rely on your provider, and it's really difficult to switch from one serverless provider, to another.
* Testing and monitoring:
    depending on the vendor, you may have to face challenges when creating a test environment, and in general, abstracting the server away also takes away the opportunity to monitor how often and how long do your function apps occur, and possibly makes debugging complicated.

    ## What do feature flags do?

* Decouple features rollout from code deployment
    * Enabling without broken, WIP features
    * to avoid delaying the release of features that are ready
    * to deploy features to specific users or users with a shared attribute
* saves the hustle and the resources of configuration files

## Sample application
My chosen language is [C#](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-function-vs-code?pivots=programming-language-csharp "Azure Functions C# Quickstart"), but the developers can also choose from a variety of other languages, such as[PowerShell](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-function-vs-code?pivots=programming-language-powershell "Azure Functions PowerShell Quickstart"), [Java](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-function-vs-code?pivots=programming-language-java "Azure Functions Java Quickstart"), [JavaScript](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-function-vs-code?pivots=programming-language-javascript "Azure Functions JavaScript Quickstart"), [Python](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-function-vs-code?pivots=programming-language-python "Azure Functions Python Quickstart") and [TypeScript](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-function-vs-code?pivots=programming-language-typescript "Azure Functions TypeScript Quickstart").

For easy understanding I have decided to review the uses of ConfigCat Feature Flags on a modification of the [Azure Functions C# Quickstart](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-function-vs-code?pivots=programming-language-csharp "Azure Functions C# Quickstart").
The original function of the application asks for a name as parameter, or if it has been already given one, greets the user on a html output. My addition is that if you greet a returning costumer in your application, indicated by the feature flag giving the application a true value, it greets the costumer accordingly. 
## How to deploy the application to Azure Functions
The official detailed tutorial to deploy your application to Asure Functions can be found on the [Azure Functions C# Quickstart](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-function-vs-code?pivots=programming-language-csharp "Azure Functions C# Quickstart"), so I just included a condensed step-by-step explanation on how to use it in Visual Studio Code.
1. Install the requirements
    - [Visual Studio Code](https://code.visualstudio.com/ "Visualstudio.com")
    - [Node.Js](https://nodejs.org/ "nodejs.org")
    - Visual Studio Code Extension for your preferred language
    - [VSC Azure Functions Extension](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azurefunctions "Azure Functions Extension Marketplace Page")
1. Go to the Azure Functions Extension in VSC
    - Select the language you would like to use
    - Select the template (HTTP Trigger in my case)
    - Provide a namespace (My.Functions for example)
    - Select the Authorisation level (Anonymous)
    - Select the way to open your project (Add to workspace)
1. Now, let's stop and admire your creation. It's a working function, that you can run locally. Just kidding, let us paste in the generated code from our Feature Flag from ConfigCat. The example that ConfigCat generates to your profile should be a fine material to test, if it is implemented successfully. Note, that this part of the process may differ according to your language of choice. For this description I use C#.
    1. Since Azure Functions currently does not support custom SDK installation, you have to manually provide the ConfigCat.Client in your Azure.csproj file.
    Paste `<ItemGroup>
    <PackageReference Include="ConfigCat.Client" Version="6.0.0" /> [Alt](https://imgur.com/VRS8oy0 "Azure.csproj")
  </ItemGroup>` where other itemgroups are listed.
    1. Paste `using ConfigCat.Client;` to the namespaces in CSharp.cs. [Alt](https://imgur.com/fmbcYgm "using ConfigCat.Client")
    1. Paste the copypastes from the 3. step on the [ConfigCat Feature Flag page](https://app.configcat.com/08d87b7f-026c-43d3-82e1-afcfd84e71e3/08d87b7f-027f-48ef-8b27-84dab935d3e7/08d87b7f-0278-472b-876d-a6e65bc0f06a "Feature Flags & Settings") into the code, and include a guard in the feature you would like to switch on/off with the logical variable it grants you, `costumerFlag` in my case. Change the refresh value - in C# the value of TimeToLiveSeconds -, to 1, so you do not have to wait to check out the results of your changes.[Alt](https://imgur.com/ggBLEvF "Code")
1. To deploy to Azure Functions, sign in, or sign up to Azure Functions, and than press the "Deploy to Function App..." button, and slect "Create new Function App in Azure" and enter a name for your function app.
## How to use the app? What do we get as a result?
Once you deployed it successfully, you can find your function at "Functions *Read-Only*". Rightclick it, and choose the "Copy Function Url" option. Paste it in your browser and add "?name=" and a name to the url.
Feel free to switch the feature flag on and off on ConfigCat, and don't forget to save your changes to see if the result changes according to it.
[The result of my sample application](https://costumer.azurewebsites.net/api/CSharp?name=David "in case your customers name is not David, change the name value in the Url.")