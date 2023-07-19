# dotnet命令

## 概述

`dotnet` - .NET CLI 的通用驱动程序

`dotnet`命令有两个函数：

+ 提供了用于处理.NET项目的命令
+ 运行.NET应用程序

`dotnet`命令可以使用不同的选项完成以下操作：

+ 显示有关环境的信息
+ 运行命令
+ 运行应用程序

## **常规命令**

| 命令                | 函数                                          |
| ------------------- | --------------------------------------------- |
| dotnet build        | 生成.NET应用程序                              |
| dotnet build-server | 与通过生成启动的服务器进行交互                |
| dotnet clean        | 清除生成输出                                  |
| dotnet exec         | 运行应用程序                                  |
| dotnet help         | 显示命令更详细的在线文档                      |
| dotnet migrate      | 将有效的预览版项目迁移到.NET Core SDK 1.0项目 |
| dotnet msbuild      | 提供对MSBuild命令行的访问权限                 |
| dotnet new          | 为给定模板初始化C#或F#项目                    |
| dotnet pack         | 创建代码的NuGet包                             |
| dotnet publish      | 发布.NET依赖于框架或独立应用程序              |
| dotnet restore      | 还原给定应用程序的依赖项                      |
| dotnet run          | 从源运行应用程序                              |
| dotnet sdk check    | 显示已安装SDK和运行时版本的最新状态           |
| dotnet sln          | 用于添加、删除和列出解决方案文件中项目的选项  |
| dotnet store        | 将程序集存储到运行时包存储区                  |
| dotnet test         | 使用测试运行程序运行测试                      |

### dotnet build

生成项目及其所有依赖项

```shell
dotnet build [<PROJECT>|<SOLUTION>] [-a|--arch <ARCHITECTURE>]
    [-c|--configuration <CONFIGURATION>] [-f|--framework <FRAMEWORK>]
    [--force] [--interactive] [--no-dependencies] [--no-incremental]
    [--no-restore] [--nologo] [--no-self-contained] [--os <OS>]
    [-o|--output <OUTPUT_DIRECTORY>] [-r|--runtime <RUNTIME_IDENTIFIER>]
    [--self-contained [true|false]] [--source <SOURCE>] [--use-current-runtime, --ucr [true|false]]
    [-v|--verbosity <LEVEL>] [--version-suffix <VERSION_SUFFIX>]

dotnet build -h|--help
```

+ `[<PROJECT>|<SOLUTION>]`：要生成的项目或解决方案文件。如果没有指定，会在当前目录下搜索文件扩展名为`proj`或`sln`结尾的文件并使用该文件
+ `[-a|--arch <ARCHITECTURE>]`：指定目标体系结构，例如：在`win-x64`计算机上，指定`--arch x86`会将RID设置为`win-x86`
+ `[-c|--configuration <CONFIGURATION>]`：定义生成配置，大多数项目的默认配置为`Debug`
+ `[-f|--framework <FRAMEWORK>]`：编译特定框架，必须在项目文件中定义该框架。示例：`net7.0`，`net462`
+ `[--force]`：强制解析所有依赖项，即使上次还原已经成功，也不例外。指定此标记等同于删除`project.assets.json`文件
+ `[--interactive]`：允许命令停止并等待用户输入或操作。例如：完成身份验证
+ `[--no-dependencies]`：忽略项目到项目引用，并仅生成指定的根项目
+ `[--no-incremental]`：将生成标记为增量生成不安全，此标记关闭增量编译，并强制完全重新生成项目依赖关系图
+ `[--no-restore]`：在生成期间不执行隐式还原
+ `[--nologo]`：不显示启动版权标志或版权信息
+ `[--no-self-contained]`：将应用程序发布为与框架相关的应用程序
+ `[--os <OS>]`：指定目标操作系统
+ `[-o|--output <OUTPUT_DIRECTORY>]`：放置生成二进制文件的目录
+ `[-r|--runtime <RUNTIME_IDENTIFIER>]`：指定目标运行时
+ `[--self-contained [true|false]]`：.NET运行时随应用程序一同发布，因此无需在目标计算机上安装运行时
+ `[--source <SOURCE>]`：要在还原操作期间使用的NuGet包源的URI
+ `[--use-current-runtime, --ucr [true|false]]`：根据计算机之一将`RuntimeIdentifier`设置为平台可移植的`RuntimeIdentifier`
+ `[-v|--verbosity <LEVEL>]`：设置命令的详细级别，允许使用的值为`q[uiet]`、`m[inimal]`、`n[ormal]`、`d[etailed]`、`diag[nostic]`
+ `[--version-suffix <VERSION_SUFFIX>]`：设置生成项目时使用的`$(VersionSuffix)`属性的值

### dotnet clean

清除项目输出

```shell
dotnet clean [<PROJECT>|<SOLUTION>] [-c|--configuration <CONFIGURATION>]
    [-f|--framework <FRAMEWORK>] [--interactive]
    [--nologo] [-o|--output <OUTPUT_DIRECTORY>]
    [-r|--runtime <RUNTIME_IDENTIFIER>] [-v|--verbosity <LEVEL>]

dotnet clean -h|--help
```

+ `[<PROJECT>|<SOLUTION>]`：要清理的MSBuild项目或解决方案
+ `[-c|--configuration <CONFIGURATION>]`：定义生成配置
+ `[-f|--framework <FRAMEWORK>]`：在生成时指定的框架
+ `[--interactive]`：允许命令停止并等待用户输入或操作。例如：完成身份验证
+ `[--nologo]`：不显示启动版权标志或版权信息
+ `[-o|--output <OUTPUT_DIRECTORY>]`：放置生成二进制文件的目录
+ `[-r|--runtime <RUNTIME_IDENTIFIER>]`：指定目标运行时
+ `[-v|--verbosity <LEVEL>]`：设置命令的详细级别，允许使用的值为`q[uiet]`、`m[inimal]`、`n[ormal]`、`d[etailed]`、`diag[nostic]`

### dotnet msbuild

生成项目及其所有依赖项

### dotnet new

####  `dotnet new <TEMPLATE>`

根据指定模板，创建新的项目、配置文件或解决方案

```shell
dotnet new <TEMPLATE> [--dry-run] [--force] [-lang|--language {"C#"|"F#"|VB}]
    [-n|--name <OUTPUT_NAME>] [-f|--framework <FRAMEWORK>] [--no-update-check]
    [-o|--output <OUTPUT_DIRECTORY>] [--project <PROJECT_PATH>]
    [-d|--diagnostics] [--verbosity <LEVEL>] [Template options]

dotnet new -h|--help
```



## **项目引用**

| 命令                    | 函数         |
| ----------------------- | ------------ |
| dotnet add reference    | 添加项目引用 |
| dotnet list reference   | 列出项目引用 |
| dotnet remove reference | 删除项目引用 |

## **NuGet包**

| 命令                  | 函数        |
| --------------------- | ----------- |
| dotnet add package    | 添加NuGet包 |
| dotnet remove package | 删除NuGet包 |

## **NuGet命令**

| 命令                        | 函数                         |
| --------------------------- | ---------------------------- |
| dotnet nuget delete         | 从服务器删除或取消列出包     |
| dotnet  nuget push          | 将包推送到服务器，并将其发布 |
| dotnet nuget locals         | 清除或列出本地NuGet资源      |
| dotnet nuget add source     | 添加NuGet源                  |
| dotnet nuget disable source | 禁用NuGet源                  |
| dotnet nuget enable source  | 启动NuGet源                  |
| dotnet nuget list source    | 列出所有NuGet源              |
| dotnet nuge remove source   | 删除NuGet源                  |
| dotnet nuget update source  | 更新NuGet源                  |

## **工作负载命令**

| 命令                      | 函数                                 |
| ------------------------- | ------------------------------------ |
| dotnet workload install   | 安装可选的工作负载                   |
| dotnet workload list      | 列出已安装的所有工作负载             |
| dotnet workload repair    | 修复所有已安装的工作负载             |
| dotnet workload search    | 列出所选工作负载或所有可用的工作负载 |
| dotnet workload uninstall | 卸载工作负载                         |
| dotnet workload update    | 重新安装所有已安装的工作负载         |

## **全局、工具路径和本地工具命令**

| 命令                  | 函数                                                  |
| --------------------- | ----------------------------------------------------- |
| dotnet tool install   | 在计算机安装工具                                      |
| dotnet tool list      | 列出计算机上当前安装的所有全局、工具路径或本地工具    |
| dotnet tool search    | 在NuGet.org中搜索其名称或元数据中具有指定搜索词的工具 |
| dotnet tool uninstall | 从计算机中卸载工具                                    |
| dotnet tool update    | 更新计算机安装的工具                                  |

## **其他工具**

| 工具         | 函数                                                         |
| ------------ | ------------------------------------------------------------ |
| dev-certs    | 创建和管理开发证书                                           |
| ef           | Entity Framework Core命令行工具                              |
| user-secrets | 管理开发用户机密                                             |
| watch        | 当应用程序检测到原地阿玛中的更改时，重启或热重载应用程序的文件观察程序 |

