<div align="center">
	<h1>TerraMours.Chat.Ava平台版</h1>
</div>

![](https://img.shields.io/github/stars/TerraMours/TerraMours_Gpt_Api) ![](https://img.shields.io/github/forks/TerraMours/TerraMours_Gpt_Api)

中文简介 | [English](README-EN.md)

## 简介

TerraMours.Chat.Ava ，是TerraMours 项目组为GPT项目开发的基于avalonia框架的跨平台客户端，调用**[TerraMours_Gpt_Api](https://github.com/TerraMours/TerraMours_Gpt_Api)**后端服务接口。目前已测试Windows和openKylin的linux系统中正常运行。

# 项目截图

### windows

![登陆界面](https://www.raokun.top/upload/2023/08/image-20230809171641547.png)

![聊天界面](https://www.raokun.top/upload/2023/08/image-20230809171723534.png)

### openKylin系统-linux

![登陆界面](https://www.raokun.top/upload/2023/08/image-1691574060766.png)
![聊天界面](https://www.raokun.top/upload/2023/08/image-20230809173026090.png)

## 1.nuget包引用

![image-20230717150959484](https://www.raokun.top/upload/2023/07/image-20230717150959484.png)

### 引用包介绍：

* Avalonia  版本11.0.0-rc1.1，稳定版本，其他基于avalonia的包要选用支持11.0.0-rc1.1的版本

* Avalonia.ReactiveUI  MVVM 架构模式的工具库，创建avalonia项目时会提示选择。

* [DialogHost.Avalonia](https://www.nuget.org/packages/DialogHost.Avalonia)  它提供了一种简单的方式来显示带有信息的对话框或在需要信息时提示用户。

* FluentAvaloniaUI   UI库，并将更多WinUI控件引入Avalonia

* System.Data.SQLite  本地数据库SQLite

* CsvHelper Csv导入导出工具库

* [Markdown.Avalonia](https://www.nuget.org/packages/Markdown.Avalonia)  用于显示markdown文本的工具，用于展示聊天结果的渲染

* Betalgo.OpenAI  调用ChatGpt的扩展库

  

```xml
<PackageReference Include="Avalonia" Version="11.0.0-rc1.1" />
<PackageReference Include="Avalonia.Desktop" Version="11.0.0-rc1.1" />
<PackageReference Include="Avalonia.Themes.Fluent" Version="11.0.0-rc1.1" />
<PackageReference Include="Avalonia.Fonts.Inter" Version="11.0.0-rc1.1" />
<!--Condition below is needed to remove Avalonia.Diagnostics package from build output in Release configuration.-->
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



## 2.功能介绍

项目开发的功能分为如下：

### 1.通用框架：

* VMLocator: ViewModel 定位器。方便地获取和管理 ViewModel 实例，从而实现界面和数据的解耦和模块化，提高代码的可维护性和可测试性。
* 国际化： 使用 CultureInfo.CurrentCulture 来实现多语言支持和本地化
* 本地化数据：通过SQLite实现数据本地化
* CSV导入导出：实现数据的迁移和补充
* 自定义快捷键： 自定义快捷键，方便操作。发挥客户端的按键优势。
* 自定义字体
* 全局样式

### 2.界面交互

* LoginView.axaml  **加载登陆界面**。
* MainWindow.axaml  **首页**
* MainView.axaml  **主界面**
* DataGridView.axaml  **会话列表**
* ChatView.axaml  **聊天界面**
* ApiSettingsView.axaml  **API配置**



## 项目发布

**打开命令行工具，并导航到你的 Avalonia 项目的根目录**

windows

```sh
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=false /p:PublishReadyToRun=true /p:IncludeNativeLibrariesForSelfExtract=true
```

linux

```
dotnet publish -c Release -r linux-x64 --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=false /p:PublishReadyToRun=true /p:IncludeNativeLibrariesForSelfExtract=true
```



## 技术总结

技术总结记录在我的博客中
[基于Avalonia 11.0.0+ReactiveUI 的跨平台项目开发1-通用框架](https://www.raokun.top/archives/ji-yu-avalonia1100reactiveui-de-kua-ping-tai-xiang-mu-kai-fa-1--tong-yong-kuang-jia)

[基于Avalonia 11.0.0+ReactiveUI 的跨平台项目开发2-功能开发](https://www.raokun.top/archives/ji-yu-avalonia1100reactiveui-de-kua-ping-tai-xiang-mu-kai-fa-2--gong-neng-kai-fa)





## 整体项目文档

- [项目文档地址](https://terramours.site/)



## 后端服务特性

##### 1.SignalR+Hangfire 实现后台任务队列和实时通讯

##### 2.automapper 模型自动映射

##### 3.对接口统一返回结果中间件ApiResponse{code,message,data}封装

##### 4.Semantic Kernel 调用chatgpt

##### 5.日志服务Seq

##### 6.Stable Diffusion 图片生成

## 开发功能

- **AI聊天**：发起聊天，基于Semantic Kernel，目前写了chatgpt，常用模型：gpt-3.5-turbo，可支持gpt-3.5-turbo-16K,gpt-4
- **AI绘图**：基于Stable Diffusion和chatgpt的dallE模型的图片生成
- **聊天记录**：聊天记录管理，查询使用者会话信息。（todo：创建微调模型）
- **敏感词管理**: 敏感词管理，自定义敏感词过滤，加强系统安全
- **Key池管理**：Key池管理，支持管理者添加多个key组成Key池，调用ai接口时轮询，加强稳定性
- **系统提示词**：系统提示词，添加各种角色提示词，让使用者能更好的使用ai对话。


## web端在线预览

- [TerraMours Admin 预览地址](https://demo.terramours.site/)

## 前端服务

- [TerraMours_Gpt_Web](https://github.com/TerraMours/TerraMours_Gpt_Web)

## 开源作者

[@Raokun](https://github.com/raokun)

[@firstsaofan](https://github.com/orgs/TerraMours/people/firstsaofan)



## 交流

`TerraMours Admin` 是完全开源免费的项目，在帮助开发者更方便地进行中大型管理系统开发，同时也提供微信和 QQ 交流群，使用问题欢迎在群内提问。

  <div style="display:flex;">
  	<div style="padding-right:24px;">
  		<p>QQ交流群</p>
      <img src="https://www.raokun.top/upload/2023/06/%E4%BA%A4%E6%B5%81%E7%BE%A4.png" style="width:200px" />
  	</div>
		<div>
			<p>添加本人微信，欢迎来技术交流，业务咨询</p>
			<img src="https://www.raokun.top/upload/2023/04/%E5%BE%AE%E4%BF%A1%E5%9B%BE%E7%89%87_20230405192146.jpg" style="width:180px" />
		</div>
  </div>


## 捐赠

如果你觉得这个项目对你有帮助，可以请 TerraMours 组员喝杯咖啡表示支持，TerraMours 开源的动力离不开各位的支持和鼓励。

  <div style="display:flex;">
  	<div style="padding-right:24px;">
  		<p>微信</p>
      <img src="https://www.raokun.top/upload/2023/04/%E5%BE%AE%E4%BF%A1%E6%94%B6%E6%AC%BE.jpg" style="width:200px" />
  	</div>
	<div style="padding-right:24px;">
  		<p>支付宝</p>
      <img src="https://www.raokun.top/upload/2023/04/%E6%94%AF%E4%BB%98%E5%AE%9D%E6%94%B6%E6%AC%BE.jpg" style="width:200px" />
  	</div>
  </div>
