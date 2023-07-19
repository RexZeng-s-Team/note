## Windows下配置Sonarqube+Scanner

### 下载安装包

1. 下载Sonarqube

   地址：`https://www.sonarqube.org/downloads/`

   如果是个人简单使用，使用Community版本即可，功能满足，下载前看清楚支持的检查语言。

   ![image-20211214110516699](C:\Users\chendx36743\AppData\Roaming\Typora\typora-user-images\image-20211214110516699.png)

2. 下载sonar-scanner

   地址：`https://docs.sonarqube.org/latest/analysis/scan/sonarscanner/`

   下载scanner 的时候可以根据自己使用的版本下载，一般情况下版本向下兼容

   ![image-20211214110613174](C:\Users\chendx36743\AppData\Roaming\Typora\typora-user-images\image-20211214110613174.png)

3. 下载JDK

   地址：`https://www.oracle.com/java/technologies/downloads/`

   JDK的版本可以根据自己的需要下载安装，注意sonarqube对JDK的版本要求。

   ![image-20211214111024732](C:\Users\chendx36743\AppData\Roaming\Typora\typora-user-images\image-20211214111024732.png)

### 安装

1. 安装JDK

   *如果SonarQube和Sonar-Scanner不在同一个IP下，则两个IP都需要安装JDK*

   找到刚下载的JDK安装包，如：jdk-17_window-x64_bin.exe，双击进行安装，一路next即可，注意记录安装目录。

   添加环境变量

   点击`开始菜单-设置`，搜索`查看高级系统设置`,打开

   ![image-20211214112327506](C:\Users\chendx36743\AppData\Roaming\Typora\typora-user-images\image-20211214112327506.png)

   ![image-20211214112409213](C:\Users\chendx36743\AppData\Roaming\Typora\typora-user-images\image-20211214112409213.png)

   点击图中右侧`新建`按钮，新建一个环境变量，路径使用`浏览`找到刚才JDK的安装路径，指向`bin`文件夹，如：`D:\Java\bin`。然后点击确定，直到全部关闭。

   环境变量添加成功检测

   使用组合键`win+R`打开命令提示符，键入`java --version`敲击回车，能看到正常的返回版本。

   ![image-20211214112859663](C:\Users\chendx36743\AppData\Roaming\Typora\typora-user-images\image-20211214112859663.png)

2. 安装SonarQube

   在任意目录下解压SonarQube安装包

   ![img](https://img-blog.csdnimg.cn/20191017173158838.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzIyNjYwMDkz,size_16,color_FFFFFF,t_70)

   打开`bin`文件夹，找到windows运行路径

   ![img](https://img-blog.csdnimg.cn/20191017174540854.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzIyNjYwMDkz,size_16,color_FFFFFF,t_70)

   双击启动，第一次启动有点慢

   ![在这里插入图片描述](https://img-blog.csdnimg.cn/20191017174648768.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzIyNjYwMDkz,size_16,color_FFFFFF,t_70)

   看到图中标志说明已经启动成功

3. 安装Sonar-Scanner

   将下载好的Sonar-Scanner安装包解压到任意路径，安装JDK的方法新建一个环境变量。

   ![image-20211214113804297](C:\Users\chendx36743\AppData\Roaming\Typora\typora-user-images\image-20211214113804297.png)

### 登录SonarQube

1. 浏览器登录

   打开浏览器，输入地址：`http://ip:9000`，这里ip是刚才安装`SonarQube`所在的电脑IP，端口如果没有自定义，默认为9000 。输入用户名和密码，点击登录。默认的用户名和密码均为admin。

   ![image-20211214114254021](C:\Users\chendx36743\AppData\Roaming\Typora\typora-user-images\image-20211214114254021.png)

2. 创建项目

   ![image-20211214114511262](C:\Users\chendx36743\AppData\Roaming\Typora\typora-user-images\image-20211214114511262.png)

   输入项目名称，点击设置

   ![image-20211214114602660](C:\Users\chendx36743\AppData\Roaming\Typora\typora-user-images\image-20211214114602660.png)

   根据自己需要，这里只是简单自测

   ![image-20211214114627058](C:\Users\chendx36743\AppData\Roaming\Typora\typora-user-images\image-20211214114627058.png)

   输入一个token，登录的时候使用，点击生成

   ![image-20211214114744395](C:\Users\chendx36743\AppData\Roaming\Typora\typora-user-images\image-20211214114744395.png)

   选择需要扫描的版本和语言

   ![image-20211214114958417](C:\Users\chendx36743\AppData\Roaming\Typora\typora-user-images\image-20211214114958417.png)

   

### 扫描

1. 找到需要扫描的`.sln`或`csproj`路径，进入命令提示符，切换到这个路径，然后执行命令

   ```shell
   SonarScanner.MSBuild.exe begin /k:"testproject" /d:sonar.host.url="http://10.188.113.45:9000" /d:sonar.login="admin" /d:sonar.password="admin1"
   ```

   `/d:sonar.login="admin" /d:sonar.password="admin1"`还可以使用创建的token登录。

   如果有报错涉及到权限问题，`the token you provided does not have sufficient tights`，在网页上进行权限设置。

   ![image-20211214115913357](C:\Users\chendx36743\AppData\Roaming\Typora\typora-user-images\image-20211214115913357.png)

2. 执行命令

   ```shell
   MSBuild.exe (.sln/.csproj) -t:rebuild -p:configuration="release" -m:8
   ```

   关于MSuild命令的参数参考链接

   [MSBuild命令行参考](https://docs.microsoft.com/zh-cn/visualstudio/msbuild/msbuild-command-line-reference?view=vs-2022)

   如果提示`MSBuild`是未知命令，添加环境变量可用，VS自带有安装MSBuild.exe，路径为VS安装目录下的MSBuild文件夹，环境变量路径为`VS\MSBuild\Current|Bin`，VS是VS2019的安装路径。

3. 执行命令

   ```shell
   SonarScanner.MSBuild.exe end /d:sonar.login="admin" /d:sonar.password="admin1"
   ```

   ![image-20211214120413306](C:\Users\chendx36743\AppData\Roaming\Typora\typora-user-images\image-20211214120413306.png)

4. 刷新SonarQube页面，可以看到扫描结果

   ![image-20211214120458922](C:\Users\chendx36743\AppData\Roaming\Typora\typora-user-images\image-20211214120458922.png)

