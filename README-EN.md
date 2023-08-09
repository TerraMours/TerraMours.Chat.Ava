<div align="center">
	<h1>TerraMours.Chat.Ava Platform Edition</h1>
</div>

![](https://img.shields.io/github/stars/TerraMours/TerraMours_Gpt_Api) ![](https://img.shields.io/github/forks/TerraMours/TerraMours_Gpt_Api)

[中文简介](README.md) | English

## Introduction

TerraMours.Chat.Ava is a cross-platform client based on the Avalonia framework developed by the TerraMours project team for the GPT project. It calls the backend service interface of **[TerraMours_Gpt_Api](https://github.com/TerraMours/TerraMours_Gpt_Api)**. It has been tested to run successfully on Windows and openKylin Linux systems.

# Project Screenshots

### Windows

![Login Page](https://www.raokun.top/upload/2023/08/image-20230809171641547.png)

![Chat Page](https://www.raokun.top/upload/2023/08/image-20230809171723534.png)

### openKylin Linux System

![Login Page](https://www.raokun.top/upload/2023/08/image-1691574060766.png)

![Chat Page](https://www.raokun.top/upload/2023/08/image-20230809173026090.png)

## 1. NuGet Package References

![image-20230717150959484](https://www.raokun.top/upload/2023/07/image-20230717150959484.png)

### Package References:

* Avalonia Version 11.0.0-rc1.1. Stable version. Other Avalonia-based packages should choose a version that supports 11.0.0-rc1.1.
* Avalonia.ReactiveUI: Utility library for the MVVM architecture pattern. Prompted to choose when creating an Avalonia project.
* [DialogHost.Avalonia](https://www.nuget.org/packages/DialogHost.Avalonia): Provides a simple way to display dialogs with messages or prompts for user input.
* FluentAvaloniaUI: UI library that introduces more WinUI controls into Avalonia.
* System.Data.SQLite: Local database SQLite.
* CsvHelper: Library for CSV import and export.
* [Markdown.Avalonia](https://www.nuget.org/packages/Markdown.Avalonia): Tool for displaying markdown text, used for rendering chat results.
* Betalgo.OpenAI: Extension library for calling ChatGpt.

```xml
<PackageReference Include="Avalonia" Version="11.0.0-rc1.1" />
<PackageReference Include="Avalonia.Desktop" Version="11.0.0-rc1.1" />
<PackageReference Include="Avalonia.Themes.Fluent" Version="11.0.0-rc1.1" />
<PackageReference Include="Avalonia.Fonts.Inter" Version="11.0.0-rc1.1" />
<!-- Condition below is needed to remove Avalonia.Diagnostics package from build output in Release configuration. -->
<PackageReference Condition="'$(Configuration)' == 'Debug'" Include="Avalonia.Diagnostics" Version="11.0.0-rc1.1" />
<PackageReference Include="Avalonia.Xaml.Interactivity" Version="11.0.0-rc1.1" />
<PackageReference Include="Avalonia.ReactiveUI" Version="11.0.0-rc1.1" />
<PackageReference Include="Avalonia.AvaloniaEdit" Version="11.0.0-rc1.1" />
<PackageReference Include="AvaloniaEdit.TextMate" Version="11.0.0-rc1.1" />
<PackageReference Include="DialogHost.Avalonia" Version="0.7.4" />
<PackageReference Include="FluentAvaloniaUI" Version="2.0.0-rc1" />
<PackageReference Include="System.Data.SQLite" Version="1.0.117" />
<PackageReference Include="CsvHelper" Version="30.0.1" />
<PackageReference Include="Markdown.Avalonia" Version="11.0.0-d1" />
<PackageReference Include="Markdown.Avalonia.SyntaxHigh" Version="11.0.0-d1" />
<PackageReference Include="Markdown.Avalonia.Tight" Version="11.0.0-d1" />
<PackageReference Include="Betalgo.OpenAI" Version="7.1.2-beta" />
```



## 2. Feature Introduction

The project development features are as follows:

### 1. Common Framework:

- VMLocator: ViewModel Locator. It facilitates the retrieval and management of ViewModel instances, thus achieving decoupling and modularization of the interface and data, improving code maintainability and testability.
- Internationalization: Implements multilingual support and localization using CultureInfo.CurrentCulture.
- Localized Data: Implements data localization using SQLite.
- CSV Import/Export: Implements data migration and supplementation.
- Custom Shortcut Keys: Allows customization of shortcut keys for easy operation, leveraging the advantages of client keyboard.
- Custom Fonts.
- Global Styles.

### 2. Interface Interaction

- LoginView.axaml: Loads the login interface.
- MainWindow.axaml: Homepage.
- MainView.axaml: Main interface.
- DataGridView.axaml: Session list.
- ChatView.axaml: Chat interface.
- ApiSettingsView.axaml: API configuration.

## Project Deployment

Open the command-line tool and navigate to the root directory of your Avalonia project.

Windows:

```
sh复制代码dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=false /p:PublishReadyToRun=true /p:IncludeNativeLibrariesForSelfExtract=true
```

Linux:

```
sh复制代码dotnet publish -c Release -r linux-x64 --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=false /p:PublishReadyToRun=true /p:IncludeNativeLibrariesForSelfExtract=true
```

## Technical Summary

The technical summary is recorded in my blog.

[Cross-platform Project Development Based on Avalonia 11.0.0+ReactiveUI - Common Framework](https://www.raokun.top/archives/ji-yu-avalonia1100reactiveui-de-kua-ping-tai-xiang-mu-kai-fa-1--tong-yong-kuang-jia)

[Cross-platform Project Development Based on Avalonia 11.0.0+ReactiveUI - Feature Development](https://www.raokun.top/archives/ji-yu-avalonia1100reactiveui-de-kua-ping-tai-xiang-mu-kai-fa-2--gong-neng-kai-fa)

## Overall Project Documentation

- [Project Documentation](https://terramours.site/)

## Backend Service Features

##### 1. SignalR+Hangfire: Implements background task queue and real-time communication.

##### 2. AutoMapper: Automatic mapping of models.

##### 3. ApiResponse Middleware: Wraps the unified return result of interfaces as ApiResponse{code,message,data}.

##### 4. Semantic Kernel: Invokes chatgpt.

##### 5. Logging Service: Seq.

##### 6. Stable Diffusion: Image generation.

## Development Features

- **AI Chat**: Initiate chat using the Semantic Kernel. Currently developed models include chatgpt, commonly used models are gpt-3.5-turbo, gpt-3.5-turbo-16K, gpt-4.
- **AI Drawing**: Generate images using Stable Diffusion and dallE model of chatgpt.
- **Chat Records**: Manage chat records and query user session information. (Todo: Create fine-tuned models)
- **Sensitive Word Management**: Manage sensitive words and customize sensitive word filtering to enhance system security.
- **Key Pool Management**: Manage key pools with multiple keys added by administrators. Use round-robin when calling AI interfaces to enhance stability.
- **System Prompt Words**: Add various role prompt words to enable users to better use AI dialogue.

## Web-based Preview

- [TerraMours Admin Preview](https://demo.terramours.site/)

## Frontend Service

- [TerraMours_Gpt_Web](https://github.com/TerraMours/TerraMours_Gpt_Web)

## Open Source Authors

[@Raokun](https://github.com/raokun)

[@firstsaofan](https://github.com/orgs/TerraMours/people/firstsaofan)

## Communication

`TerraMours Admin` is a completely open-source and free project, designed to help developers develop medium to large-scale management systems more conveniently. We also provide WeChat and QQ communication groups to answer any usage questions.

  <div style="display:flex;">
  	<div style="padding-right:24px;">
  		<p>QQ Group</p>
      <img src="https://www.raokun.top/upload/2023/06/%E4%BA%A4%E6%B5%81%E7%BE%A4.png" style="width:200px" />
  	</div>
		<div>
			<p>Add me on WeChat, welcome to discuss technology or business inquiries</p>
			<img src="https://www.raokun.top/upload/2023/04/%E5%BE%AE%E4%BF%A1%E5%9B%BE%E7%89%87_20230405192146.jpg" style="width:180px" />
		</div>
  </div>

## Donations

If you find this project helpful, you can buy TerraMours team a cup of coffee as a token of support. The motivation behind TerraMours' open-source projects comes from the support and encouragement of everyone.

<div style="display:flex;"> <div style="padding-right:24px;"> <p>WeChat</p> <img src="https://www.raokun.top/upload/2023/04/微信收款.jpg" style="width:200px" /> </div> <div style="padding-right:24px;"> <p>Alipay</p> <img src="https://www.raokun.top/upload/2023/04/支付宝收款.jpg" style="width:200px" /> </div> </div>
