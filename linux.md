# 常用命令

## 用户

1. 查看用户、用户组

   ``` 
   cat /etc/passwd				#查看所有用户列表
   w						   #查看当前活跃的用户列表
   cat /etc/group				#查看用户组
   ```

   ```
   groups					    #查看当前登录用户的组内成员
   groups #root				#查看root用户坐在的组，以及组内成员
   whoami					    #查看当前登录用户名
   ```

2. 新用户添加

   ```
   useradd -d /home/cdx -m cdx
   ```

   语法：useradd 选项 用户名

   参数说明：

   | 参数 | 说明                                                         |
   | ---- | ------------------------------------------------------------ |
   | -c   | comment，指定一段注释描述                                    |
   | -d   | 目录，指定用户主目录，如果该目录不存在，同时使用-m选项，可以创建主目录 |
   | -g   | 用户组，指定用户所属的用户组                                 |
   | -G   | 用户组，指定用户所属的附加组                                 |
   | -s   | shell文件，指定用户的登录shell                               |
   | -u   | 用户号，指定用户的用户号，同时使用-o选项，可以重复使用其他用户的标识号 |

3. 删除账号

   ```
   userdel -r cdx
   ```

   语法：userdel 选项 用户名

   如果一个用户的账号不再使用，可以从系统中删除。删除用户账号就是要将/etc/passwd等系统文件中的该用户记录删除，必要时还删除用户的主目录。-r 选项的作用就是把用户的主目录一起删除。

4. 修改账号

   ```
   usermod -d /home/c
   ```

   语法：usermod 选项 用户名

   常用的选项和添加用户命令中的选项一致

5. 用户口令管理

   用户管理的一项重要内容是用户口令的管理。用户账号刚创建时没有口令，但是被系统锁定，无法使用，必须为其指定口令后才可以使用，即使是指定空口令。

   指定和修改用户口令的Shell命令是passwd。超级用户可以为自己和其他用户指定口令，普通用户只能用它修改自己的口令。命令的格式为：

   passwd 选项 用户名

   可使用的选项：

   - -l 锁定口令，即禁用账号。
   - -u 口令解锁。
   - -d 使账号无口令。
   - -f 强迫用户下次登录时修改口令。

   如果默认用户名，则修改当前用户的口令。

   例如，假设当前用户是sam，则下面的命令修改该用户自己的口令：

   $ passwd

   如果是超级用户，可以用下列形式指定任何用户的口令：

   \# passwd sam

   ```
   [root@admin ~]# passwd sam
   Changing password for user sam.
   New password:
   BAD PASSWORD: it is too simplistic/systematic
   BAD PASSWORD: is too simple
   Retype new password:
   passwd: all authentication tokens updated successfully.
   ```

   为用户指定空口令时，执行下列形式的命令：

   \# passwd -d sam

   此命令将用户sam的口令删除，这样用户sam下一次登录时，系统就不再询问口令。

   passwd命令还可以用-l(lock)选项锁定某一用户，使其不能登录，例如：

   \# passwd -l sam
   
6. groupadd 命令用于将新组加入系统

   ```
   groupadd [－g gid] [－o]] [－r] [－f] groupname
   
   －g gid：指定组ID号。
   －o：允许组ID号，不必惟一。
   －r：加入组ID号，低于499系统账号。
   －f：加入已经有的组时，发展程序退出。
   ```

7. groupdel命令删除组

   ```
   group [options] GROUP
   ```

6. 

9. 账户登录异常

   问题描述：有多个账户的情况，有某一个账户不能登录Linux。

   ```
   failed to execute /bin/bash:Resource temporarily unavilable
   ```

   使用其他账号登录后，切换至问题账户

   ```
   [admin@localhost ~]$ sudo su - scloan
   Last login: Tue Jun 12 14:06:31 CST 2018 on pts/3
   su: failed to execute /bin/bash: Resource temporarily unavailable
   ```

   解释：资源不足，查看系统内存和磁盘空间使用情况，未发现明显异常。其他账号都可以正常切换登录。

   检查配置文件

   ```
   [trade@am4126 ~]$ cat /etc/security/limits.d/20-nproc.conf
   # Default limit for number of user's processes to prevent
   # accidental fork bombs.
   # See rhbz #432903 for reasoning.
   
   *          soft    nproc     65536
   *          hard    nproc     65536
   ```

   后面两行的`*`表示登录账号，后面的`65536`则表示登录资源限制。在允许的情况，可以将其设置为无限制`unlimited`，修改完成后重新登录，恢复正常。

## 磁盘管理

1. df  (disk free)

   ![image-20211012135220456](C:\Users\chendx36743\AppData\Roaming\Typora\typora-user-images\image-20211012135220456.png)

   第一列指定文件系统的名称，第二列指定一个特定的文件系统1K-块1K是1024字节为单位的总内存。已用和可用列正在使用中，分别指定的内存量。使用列指定使用的内存的百分比，而最后一栏"安装在"指定的文件系统的挂载点。

   语法：df 选项 文件

   参数说明：

   | 参数                | 说明                             |
   | ------------------- | -------------------------------- |
   | -a                  | 包含所有的具有0 blocks的文件系统 |
   | --block-size={SIZE} | 使用{SIZE}大小的blocks           |
   | -h                  | 使用人类可读的格式               |

2. du  (disk usage)

   语法

   ```
   du [-abcDhHklmsSx][-L <符号连接>][-X <文件>][--block-size][--exclude=<目录或文件>][--max-depth=<目录层数>][--help][--version][目录或文件]
   ```

   参数说明：

   | 参数 | 说明                                                         |
   | ---- | ------------------------------------------------------------ |
   | -a   | 列出所有的文件与目录容量，因为默认仅统计目录底下的文件量而已 |
   | -h   | 以人比较易读的容量格式显示                                   |
   | -s   | 列出总量而已，而不列出每个个别的目录占用容量                 |
   | -S   | 不包括子目录下的总计                                         |
   | -k   | 以KBytes列出容量显示                                         |
   | -m   | 以MBytes列出容量显示                                         |

   ```
   [trade@am4126 am4newtran]$ ll
   total 0
   drwxrwxr-x 3 trade trade 23 Oct 13 16:06 scripts
   drwxrwxr-x 2 trade trade 61 Oct 13 16:08 tmp
   [trade@am4126 am4newtran]$ du -a
   16      ./tmp/AM4.0_转换机-AM4.0-NEWTRANV202101.01.003.zip
   16      ./tmp
   0       ./scripts/webclient/AM4newtran
   0       ./scripts/webclient
   0       ./scripts
   16      .
   [trade@am4126 am4newtran]$ du -h
   16K     ./tmp
   0       ./scripts/webclient/AM4newtran
   0       ./scripts/webclient
   0       ./scripts
   16K     .
   [trade@am4126 am4newtran]$ du -s
   16      .
   [trade@am4126 am4newtran]$ du -S
   16      ./tmp
   0       ./scripts/webclient/AM4newtran
   0       ./scripts/webclient
   0       ./scripts
   0       .
   [trade@am4126 am4newtran]$ du -k
   16      ./tmp
   0       ./scripts/webclient/AM4newtran
   0       ./scripts/webclient
   0       ./scripts
   16      .
   [trade@am4126 am4newtran]$ du -m
   1       ./tmp
   0       ./scripts/webclient/AM4newtran
   0       ./scripts/webclient
   0       ./scripts
   1       .
   ```

## 内存

### 物理内存和虚拟内存

​	直接从物理内存读写数据要比从硬盘读写数据快很多，所以我们希望数据的读写都在内存当中完成，而内存是有限的，这就引出了物理内存和虚拟内存的概念。

​	物理内存就是系统硬件提供的大小，是真正的内存，相对于物理内存，在Linux下还有一个需内内存的概念，就是为了满足物理内存的不足而提出的策略，利用磁盘空间虚拟出的一块逻辑内存，用作虚拟内存的磁盘空间被称为交换空间swap space。

​	作为物理内存的扩展，Linux会在物理内存不足时，使用交换分区的虚拟内存，更详细的说，就是内核会将暂时不使用的内存块信息写到交换空间，这样，物理内存得到了释放，这块内存就可以用于其他目的，当需要用到原始的内容时，这些信息会被重新从交换空间读入物理内存。

​	Linux的内存管理采取的时分页存取机制，为了保证物理内存得到充分的利用，内核会在适当的时候将物理内存中不经常使用的数据块自动交换到虚拟内存中，而将经常使用的信息保留早物理内存。

1. Linux系统会不时的进行页面交换操作，以保持6尽可能多的空闲物理内存，即使并没有什么事情需要内存，Linux也会交换出暂时不用的内存页面，这样避免等待交换所需要的时间。
2. Linux进行页面交换时有条件的，不是所有页面在不用是都交换到虚拟内存中，Linux内核根据“最近最经常使用”算法，仅仅将一些不经常使用的页面文件交换到虚拟内存。
3. 交换空间的页面在使用时会首先被交换到物理内存，如果此时没有足够的物理内存来容纳这些页面，他们又会马上交换出去，这样就会导致假死机、服务异常

### 命令

1. 常用free命令来查看内存

   > 可用和已用的物理内存总量
   >
   > 系统中交换内存的总量
   >
   > 内核使用的缓冲区和缓存

   ```
   ludir@ubuntu:~$ free
                 total        used        free      shared  buff/cache   available
   Mem:        3995088      876132     2501732        1976      617224     2879272
   Swap:       2097148           0     2097148
   ```

   解释：

   **total**：总计物理内存的大小

   **used**：已使用内存大小

   **free**：可用内存大小

   **shared**：多个进程共享的内存总额

   **Buffers/cached**：磁盘缓存的大小

   **available**： 列显示还可以被应用程序使用的物理内存大小

   + **free 与 available**
     在 free 命令的输出中，有一个 free 列，同时还有一个 available 列。这二者到底有何区别？
     free 是真正尚未被使用的物理内存数量。至于 available 就比较有意思了，它是从应用程序的角度看到的可用内存数量。Linux 内核为了提升磁盘操作的性能，会消耗一部分内存去缓存磁盘数据，就是我们介绍的 buffer 和 cache。所以对于内核来说，buffer 和 cache 都属于已经被使用的内存。当应用程序需要内存时，如果没有足够的 free 内存可以用，内核就会从 buffer 和 cache 中回收内存来满足应用程序的请求。所以从应用程序的角度来说，available  = free + buffer + cache。请注意，这只是一个很理想的计算方式，实际中的数据往往有较大的误差。

   还可以使用命令 free -m 或者 free -g 来友好显示，已MB或者GB为单位显示，如果想有统计结果显示内存总量，可以使用free -mt，简单计算每列的内存总量

   ```
   [trade@am4126 ~]$ free -m
                 total        used        free      shared  buff/cache   available
   Mem:          64428       62115         478         111        1833        1391
   Swap:         16383        8152        8231
   [trade@am4126 ~]$ free -g
                 total        used        free      shared  buff/cache   available
   Mem:             62          60           0           0           1           1
   Swap:            15           7           8
   [trade@am4126 ~]$ free -t
                 total        used        free      shared  buff/cache   available
   Mem:       65974800    63659580      420476      114072     1894744     1368908
   Swap:      16777212     8348556     8428656
   Total:     82752012    72008136     8849132
   [trade@am4126 ~]$ free -mt
                 total        used        free      shared  buff/cache   available
   Mem:          64428       62168         584         111        1675        1338
   Swap:         16383        8157        8226
   Total:        80812       70325        8811
   [trade@am4126 ~]$ free -gt
                 total        used        free      shared  buff/cache   available
   Mem:             62          60           0           0           1           1
   Swap:            15           7           8
   Total:           78          68           8
   ```

2. top命令

   top命令提供正在运行的系统的实时动态视图，检查每个进程的内存使用情况。最大的优点就是发现可能已经失控的进程ID，可以根据这个来进行故障排除。

   ```
   [trade@am4126 ~]$ top
   top - 11:14:33 up 16 days, 17:26,  3 users,  load average: 2.31, 2.22, 2.01
   Tasks: 413 total,   1 running, 412 sleeping,   0 stopped,   0 zombie
   %Cpu(s):  9.6 us,  5.6 sy,  0.0 ni, 84.0 id,  0.0 wa,  0.0 hi,  0.8 si,  0.0 st
   KiB Mem : 65974800 total,   596560 free, 63631676 used,  1746564 buff/cache
   KiB Swap: 16777212 total,  8443256 free,  8333956 used.  1400120 avail Mem
   
     PID USER      PR  NI    VIRT    RES    SHR S  %CPU %MEM     TIME+ COMMAND
   13153 zookeep+  20   0 6549088   1.0g   4600 S  57.3  1.6   1382:31 java
     937 trade     20   0 9764684   2.1g   3988 S  48.3  3.3 883:50.54 java
    5835 rabbitmq  20   0    9.8g 374676   2208 S  27.5  0.6   7946:26 beam.smp
   ```

   如果想top可以更友好的显示，可以使用命令 top -o %MEM，按进程所用内存对所有进程进行排序。

   ```
   [trade@am4126 ~]$ top -o %MEM
   top - 11:16:11 up 16 days, 17:27,  3 users,  load average: 2.43, 2.33, 2.07
   Tasks: 414 total,   1 running, 413 sleeping,   0 stopped,   0 zombie
   %Cpu(s):  9.6 us,  3.9 sy,  0.0 ni, 86.3 id,  0.0 wa,  0.0 hi,  0.3 si,  0.0 st
   KiB Mem : 65974800 total,   552240 free, 63644928 used,  1777632 buff/cache
   KiB Swap: 16777212 total,  8443256 free,  8333956 used.  1386760 avail Mem
   
     PID USER      PR  NI    VIRT    RES    SHR S  %CPU %MEM     TIME+ COMMAND
   22574 root      20   0   18.7g  13.6g   3184 S  12.6 21.5   6062:06 mysqld
   12635 trade     20   0   18.6g   3.9g     40 S  15.9  6.2   1545:47 hsserver
   22289 trade     20   0   12.3g   2.3g   2672 S   0.7  3.6 240:44.82 java
   ```

3. vmstat

   Virtual Memory Statistics 虚拟内存统计

   ```
   [trade@am4126 ~]$ vmstat 5 5
   procs -----------memory---------- ---swap-- -----io---- -system-- ------cpu-----
    r  b   swpd   free   buff  cache   si   so    bi    bo   in   cs us sy id wa st
    3  0 8478596 355524      0 1784424    1    1    11    57    2    1  7  3 90  0  0
    1  0 8482692 433004      0 1768892    0  796     0   871 75247 129625  7  3 89  0  0
    3  0 8482692 679008      0 1532900    0    0     0    83 73903 127921 10  3 86  0  0
    2  0 8482692 679876      0 1535360    0    0     0    54 82807 147780  8  4 89  0  0
    3  0 8482692 586956      0 1578388    0    0     0   250 75577 132199 11  4 86  0  0
   ```

   字段说明

   | 字段名称     | 说明                                      |
   | ------------ | ----------------------------------------- |
   | procs.r      | 进程，运行队列中进程数量                  |
   | procs.b      | 进程，等待IO的进程数量                    |
   | memory.swpd  | 内存，使用虚拟内存大小                    |
   | memory.free  | 内存，可用内存大小                        |
   | memory.buff  | 内存，用作缓冲的内存大小                  |
   | memory.cache | 内存，用作缓存的内存大小                  |
   | swap.si      | 交换区，每秒从交换区到内存的大小          |
   | swap.so      | 交换区，每秒从内存到交换区的大小          |
   | io.bi        | IO，每秒读取的块数(1块=1024 bytes)        |
   | io.bo        | IO，每秒写入的块数(1块=1024 bytes)        |
   | system.in    | 系统，每秒中断数，包括时钟中断(interrupt) |
   | system.cs    | 系统，每秒上下文切换数 count/second       |
   | cpu.us       | cpu，用户进程执行时间user time            |
   | cpu.sy       | cpu，系统进程执行时间                     |
   | cpu.id       | cpu，空闲时间                             |
   | cpu.wa       | cpu，等待IO时间                           |

   vmstat -a 显示活跃和非活跃内存

   vmstat -f 显示从系统启动至今的fork数量

   vmstat -s 查看内存使用的详细信息，显示内存相关统计信息及多种系统活动数量

   vmstat -d 产看磁盘的读写

   vmstat -p /dev/sda1 显示指定磁盘分区统计信息

4. /proc/meminfo

   几乎所有的内存信息都是来自这个虚拟的文件，包含了有关系统的试试动态信息。

## 进程

1. ps命令-查看静态的进程的统计信息

   参数说明：

   > a：显示当前终端下的所有进程信息，包括其他用户的进程
   >
   > u：使用已用户为主的格式输出进程信息
   >
   > x：显示当前用户在所有终端下的进程信息
   >
   > -e：显示系统内的所有进程信息
   >
   > -l：使用长格式显示进程信息
   >
   > -f：使用完整的格式显示进程信息
   
2. pstree命令

   这个命令可以输出系统中各进程的树形结构，以更加直观地判断个进程之间的相互关系。

   默认没有参数的情况下，只显示进程名称，-p 显示对应的PID号，-u 显示对应的用户名，-a 显示完整的命令信息

3. 进程控制

   一般情况下，在命令终端输入的命令在前台执行，用户需要等到该进程执行结束后才能继续输入其他命令。如果在命令的后面加 & 符号，进程启动后直接放入后台运行。

   > 1.如果在执行过程中，按`ctrl+z`组合可以将当前进程挂起，调入后台并停止执行。
   >
   > 2.使用 `jobs -l` 查看后台的进程，使用`bg`命令background，可以将后台中暂停的任务恢复执行，继续在后台执行；使用`fg`命令foreground，可以将后台任务重新恢复到前台运行。一般情况，`bg`和`fg`名命令都需要任务编号作为参数，除非列表中只有一个任务。
   >
   > 3.使用`kill`命令终止进程，需要使用PID作为参数，如果终止操作执行失败，可以增加参数-9。
   >
   > 4.使用`killall`命令通过进程名称来终止进程，当需要结束多个相同名称的进程时，这个命令将更加方便。
   >
   > 5.使用`pkill`命令可以根据进程的名称，运行该进程的用户，进程所在的终端等多种属性终止特定的进程。


## 端口

1. netstat命令查看端口状态

   参数说明：

   | 参数名称 | 说明                                                      |
   | -------- | --------------------------------------------------------- |
   | -t       | 指明显示TCP端口                                           |
   | -u       | 指明显示UDP端口                                           |
   | -l       | 仅显示监听套接字                                          |
   | -p       | 显示进程标识符和程序名称，每一个套接字/端口都属于一个程序 |
   | -n       | 不进行DNS轮询，显示IP                                     |

   - 查看所有TCP端口

     ```
     [trade@am4126 ~]$ netstat -ntlp
     ```
     
   - 查看所有8088端口
   
     ```
     [trade@am4126 ~]$ netstat -ntulp | grep 8088
     ```
   
2. lsof命令

   查看指定端口所运行的程序

   ```
   [trade@am4126 ~]$ lsof -i:10012
   COMMAND    PID  USER   FD   TYPE    DEVICE SIZE/OFF NODE NAME
   java      5658 trade  459u  IPv4 266375312      0t0  TCP am4126:51690->am4126:10012 (ESTABLISHED)
   ```

3. ps命令

   参数`-e`显示所有进程，显示每个程序所使用的环境变量，`-f`全格式，用ASCII字符显示树状结构，表达程序见的相互关系。

   查看进程是否存在

   ```
   [trade@am4126 ~]$ ps -ef | grep nginx
   comp     13887     1  0 Oct26 ?        00:00:00 nginx: master process ./nginx
   ```

## 文件处理

1. find基本语法

   ```
   find [PATH] [option] [action]
   
   # 与时间有关的参数：
   -mtime n : n为数字，意思为在n天之前的“一天内”被更改过的文件；
   -mtime +n : 列出在n天之前（不含n天本身）被更改过的文件名；
   -mtime -n : 列出在n天之内（含n天本身）被更改过的文件名；
   -newer file : 列出比file还要新的文件名
   # 例如：
   find /root -mtime 0 # 在当前目录下查找今天之内有改动的文件
   
   # 与用户或用户组名有关的参数：
   -user name : 列出文件所有者为name的文件
   -group name : 列出文件所属用户组为name的文件
   -uid n : 列出文件所有者为用户ID为n的文件
   -gid n : 列出文件所属用户组为用户组ID为n的文件
   # 例如：
   find /home/hadoop -user hadoop # 在目录/home/hadoop中找出所有者为hadoop的文件
   
   # 与文件权限及名称有关的参数：
   -name filename ：找出文件名为filename的文件
   -size [+-]SIZE ：找出比SIZE还要大（+）或小（-）的文件
   -tpye TYPE ：查找文件的类型为TYPE的文件，TYPE的值主要有：一般文件（f)、设备文件（b、c）、
                目录（d）、连接文件（l）、socket（s）、FIFO管道文件（p）；
   -perm mode ：查找文件权限刚好等于mode的文件，mode用数字表示，如0755；
   -perm -mode ：查找文件权限必须要全部包括mode权限的文件，mode用数字表示
   -perm +mode ：查找文件权限包含任一mode的权限的文件，mode用数字表示
   # 例如：
   find / -name passwd # 查找文件名为passwd的文件
   find . -perm 0755 # 查找当前目录中文件权限的0755的文件
   find . -size +12k # 查找当前目录中大于12KB的文件，注意c表示byte
   ```

2. ls命令

   ```
   -a ：全部的档案，连同隐藏档( 开头为 . 的档案) 一起列出来～ 
   -A ：全部的档案，连同隐藏档，但不包括 . 与 .. 这两个目录，一起列出来～ 
   -d ：仅列出目录本身，而不是列出目录内的档案数据 
   -f ：直接列出结果，而不进行排序 (ls 预设会以档名排序！) 
   -F ：根据档案、目录等信息，给予附加数据结构，例如： 
   *：代表可执行档； /：代表目录； =：代表 socket 档案； |：代表 FIFO 档案； 
   -h ：将档案容量以人类较易读的方式(例如 GB, KB 等等)列出来； 
   -i ：列出 inode 位置，而非列出档案属性； 
   -l ：长数据串行出，包含档案的属性等等数据； 
   -n ：列出 UID 与 GID 而非使用者与群组的名称 (UID与GID会在账号管理提到！) 
   -r ：将排序结果反向输出，例如：原本档名由小到大，反向则为由大到小； 
   -R ：连同子目录内容一起列出来； 
   -S ：以档案容量大小排序！ 
   -t ：依时间排序 
   --color=never ：不要依据档案特性给予颜色显示； 
   --color=always ：显示颜色 
   --color=auto ：让系统自行依据设定来判断是否给予颜色 
   --full-time ：以完整时间模式 (包含年、月、日、时、分) 输出 
   --time={atime,ctime} ：输出 access 时间或 改变权限属性时间 (ctime) 
   而非内容变更时间 (modification time)     例如：ls [-aAdfFhilRS] 目录名称 ls [--color={none,auto,always}] 目录名称 ls [--full-time] 目录名称
   ```

3. cp命令

   语法：

   ```
   cp [options] source dest
   ```

   参数说明：

   ```
   -a:此选项通常在复制目录时使用，它保留链接、文件属性，并复制目录下的所有内容。其作用等于dpR参数组合
   -d：复制时保留链接。这里所说的链接相当于 Windows 系统中的快捷方式
   -f：覆盖已经存在的目标文件而不给出提示
   -i：与 -f 选项相反，在覆盖目标文件之前给出提示，要求用户确认是否覆盖，回答 y 时目标文件将被覆盖
   -p：除复制文件的内容外，还把修改时间和访问权限也复制到新文件中
   -r：若给出的源文件是一个目录文件，此时将复制该目录下所有的子目录和文件
   -l：不复制文件，只是生成链接文件
   ```

4. rm命令

   语法
   ```
   rm [options] name
   ```
   参数说明
   ```
   -f：force，忽略不存在的文件，不会出现警告信息
   -i：互动模式，在删除前会询问用户是否操作
   -r：递归删除，最常用于目录删除，它是一个非常危险的参数
   ```

5. mv命令

   语法

   ```
   mv [options] source dest
   ```

   参数说明

   ```
   -b：当目标文件或目录存在时，在执行覆盖前，会为其创建一个备份
   -i：如果指定移动的源目录或文件与目标的目录或文件同名，则会先询问是否覆盖旧文件，输入也表示直接覆盖
   -f：如果指定移动的源目录或文件与目标的目录或文件同名，不会询问，直接覆盖旧文件
   -n：不要覆盖任何已经存在的文件或目录
   -u：当源文件比目标文件新或者目标文件不存在时，才执行移动操作
   ```

6. tar压缩/解压缩

   ```
   -c ：新建打包文件
   -t ：查看打包文件的内容含有哪些文件名
   -x ：解打包或解压缩的功能，可以搭配-C（大写）指定解压的目录，注意-c,-t,-x不能同时出现在同一条命令中
   -j ：通过bzip2的支持进行压缩/解压缩
   -z ：通过gzip的支持进行压缩/解压缩
   -v ：在压缩/解压缩过程中，将正在处理的文件名显示出来
   -f filename ：filename为要处理的文件
   -C dir ：指定压缩/解压缩的目录dir
   ```

   ```
   ludir@ubuntu:~$ tar -czvf test.tar.gz a.c				#将a.c文件压缩到test.tar.gz文件
   a.c
   ludir@ubuntu:~$ tar -tzvf test.tar.gz					#查看test.tar.gz压缩包里的文件
   -rw-rw-r-- ludir/ludir       0 2021-11-02 23:53 a.c
   ludir@ubuntu:~$ tar -xzvf test.tar.gz					#解压缩文件test.tar.gz
   a.c
   ```

7. gzip压缩/解压缩

   语法：

   ```
   gzip [option] file_name
   ```

   参数说明：

   ```
   -c 将输出写到标准输出上，并保留原有文件。
   -d 将压缩文件解压。
   -l 对每个压缩文件，显示下列字段：
        压缩文件的大小；未压缩文件的大小；压缩比；未压缩文件的名字
   -r 递归式地查找指定目录并压缩其中的所有文件或者是解压缩。
   -t 测试，检查压缩文件是否完整。
   -v 对每一个压缩和解压的文件，显示文件名和压缩比。
   -num 用指定的数字 num 调整压缩的速度，-1 或 --fast 表示最快压缩方法（低压缩比），
   -9 或--best表示最慢压缩方法（高压缩比）。系统缺省值为 6。
   ```

   实例：

   ```
   gzip *
   % 把当前目录下的每个文件压缩成 .gz 文件。
   
   gzip -dv *
   % 把当前目录下每个压缩的文件解压，并列出详细的信息。
   
   gzip -l *
   % 详细显示例1中每个压缩的文件的信息，并不解压。
   
   gzip usr.tar
   % 压缩 tar 备份文件 usr.tar，此时压缩文件的扩展名为.tar.gz。
   ```

8. mkdir创建目录命令

   ```
   mkdir [option] direction
   -m, --mode=模式，设定权限<模式> (类似 chmod)，而不是 rwxrwxrwx 减 umask
    -p, --parents  可以是一个路径名称。此时若路径中的某些目录尚不存在,加上此选项后,系统将自动建立好那些尚不存在的目录,即一次可以建立多个目录; 
    -v, --verbose  每次创建新目录都显示信息
   ```

9. rmdir删除目录命令

   ```
   rmdir [option] direction
   -p 递归删除目录dirname，当子目录删除后其父目录为空时，也一同被删除。如果整个路径被删除或者由于某种原因保留部分路径，则系统在标准输出上显示相应的信息。 
   -v --verbose  显示指令执行过程 
   ```

10. chmod命令，改变文件权限

    ```
    chmod [-R] xyz file or direction
    -R 表示递归的持续更新，即连同子目录下的所有文件都会更改
    ```

11. chown命令，改变文件所有者

    ```
    chown [para] [owner][:[group]] file
    -c 显示更改的部分的信息
    -f 忽略错误信息
    -h 修复符号链接
    -R 处理指定目录及其子目录下的所有文件
    -v 显示详细的处理信息
    -deference 作用与符号链接的执行，而不是链接文件本身
    ```

12. chgrp命令，改变文件所属组

    ```
    chgrp [para] file or direction
    -c 当发生改变时输出调试信息
    -f 不显示错误信息
    -R 处理指定目录及其子目录下的所有文件
    -v 运行时显示详细的处理信息
    --deference 作用与符号链接的指向，而不是符号链接本身
    --no-deference 作用域符号链接本身
    ```


## 文本

1. cat 用途是连接文件或标准输出并打印。这个命令常用来显示文件内容，或者将几个文件连接起来显示，或者从标准输入读取内容并显示，它常与重定向符号配合使用。
2. more命令和cat的功能一样都是查看文件里的内容，但是有所不同的是more可以按页来查看文件的内容，还支持直接跳转行等功能。
3. less命令用法比起more更加有弹性。在more的时候，我们并没有办法向前面翻，只能往后面看，但若使用了less时，就可以使用`pageup``pagedown`等按键的功能前后翻看文件，更容易来看一个文件的内容。
