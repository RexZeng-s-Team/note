# 常用命令

## 根据列名称查询表名

```sql
SELECT COLUMN_NAME,TABLE_NAME,TABLE_SCHEMA FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME='';
```

## 修改表以及表中字符型字段的字符集

```sql
ALTER TABLE '库.表' CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_bin;
```

## 查询数据库中字符集为gbk的表

```sql
SELECT
    TABLE_SCHEMA '库名',
    TABLE_NAME '表名',
    TABLE_COLLATION '排序规则'
FROM
    INFORMATION_SCHEMA.`TABLES`
WHERE
    TABLE_COLLATION = 'gbk_bin'
```

## 查询的同时构建新的sql语句

```sql
SELECT
    TABLE_SCHEMA '库名',
    TABLE_NAME '表名',
    TABLE_COLLATION '排序规则',
    CONCAT('ALTER TABLE ',TABLE_SCHEMA,'.',TABLE_NAME,' CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_bin;') '新的SQL'
FROM
    INFORMATION_SCHEMA.`TABLES`
WHERE
    TABLE_COLLATION = 'gbk_bin'
```

## 查询表中某个字段的最大值

```sql
#命令行语句
mysql> select MAX(asset_id) from jc_tasset;
+---------------+
| MAX(asset_id) |
+---------------+
|            21 |
+---------------+
1 row in set
#增加一个as语法，可以在结果中显示设置的字段名称
mysql> select MAX(asset_id) as max_asset_id from jc_tasset;
+--------------+
| max_asset_id |
+--------------+
|           21 |
+--------------+
1 row in set
#相应的sql语句
SELECT MAX(asset_id) FROM jc_tasset
SELECT MAX(asset_id) AS max_asset_id FROM jc_tasset
```

## 执行脚本时报错语法错误-datetime类型默认值不能为0

sql_mode设置的值有错误

1. 查询sql_mode

   ```sql
   #命令行语句
   mysql> show variables like 'sql_mode';
   +---------------+-----------------------------------------------------------------------------------------------------------+
   | Variable_name | Value                                                                                                     |
   +---------------+-----------------------------------------------------------------------------------------------------------+
   | sql_mode      | PIPES_AS_CONCAT,STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION |
   +---------------+-----------------------------------------------------------------------------------------------------------+
   1 row in set
   #sql语句
   SELECT @@global.sql_mode;
   or
   SELECT @@session.sql_mode;
   ```

2. 设置sql_mode

   ```sql
   #sql语句
   SET GLOBAL sql_mode = 'modes...';
   或
   SET SESSION sql_mode = 'modes...';
   ```

   使用sql语句更改sql_mode的时候，不需要重启数据库，如果更改后没有效果，更改配置文件，重启数据库。

   mysql的配置文件在其安装路径下的config文件夹中my.cnf，有的时候在/etc/my.cnf中也可以配置

   ```sql
   [mysqld]
   sql_mode="STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION,PIPES_AS_CONCAT"
   ```

   在段落[mysqld]中有个配置项是sql_mode

3. 常用的sql_mode值

   | mode值                      | 说明                                                                       |
   | -------------------------- | ------------------------------------------------------------------------ |
   | ONLY_FULL_GROUP_BY         | 对于GROUP BY聚合操作，如果在SELECT中的列，没有在GROUP BY中出现，那么这个SQL是不合法的，因为列不在GROUP BY从句中 |
   | NO_AUTO_VALUE_ON_ZERO      | 该值影响自增长列的插入。默认设置下，插入0或NULL代表生成下一个自增长值。如果用户希望插入的值为0，而该列又是自增长的，那么这个选项就有用了  |
   | STRICT_TRANS_TABLES        | 在该模式下，如果一个值不能插入到一个事务中，则中断当前的操作，对非事务表不做限制                                 |
   | NO_ZERO_IN_DATE            | 在严格模式下，不允许日期和月份为零                                                        |
   | NO_ZERO_DATE               | 设置该值，mysql数据库不允许插入零日期，插入零日期会抛出错误而不是警告                                    |
   | ERROR_FOR_DIVISION_BY_ZERO | 在insert或update过程中，如果数据被零除，则产生错误而非警告。如果未给出该模式，那么数据被零除时Mysql返回NULL         |
   | NO_AUTO_CREATE_USER        | 禁止GRANT创建密码为空的用户                                                         |
   | NO_ENGINE_SUBSTITUTION     | 如果需要的存储引擎被禁用或未编译，那么抛出错误。不设置此值时，用默认的存储引擎替代，并抛出一个异常                        |
   | PIPES_AS_CONCAT            | 将\|\|视为字符串的连接操作符而非或运算符，这和Oracle数据库是一样是，也和字符串的拼接函数Concat想类似               |
   | ANSI_QUOTES                | 启用ANSI_QUOTES后，不能用双引号来引用字符串，因为它被解释为识别符                                   |

   升级mysql到8.0后，sql_mode不支持NO_AUTO_CREATE_USER，不用设置这个值

## 设置表中一个字段等于另一个字段

```sql
update table_name set '新字段'='旧字段'
```

## 添加、删除用户和授权

1. 新建用户

   登录数据库

   ```sql
   @>musql -u root -p
   @>[passwd]
   ```

   创建用户

   ```sql
   mysql>insert into musql.user(Host,User,Password) values("localhost","test",password("1234"));
   ```

   这就创建了一个用户名为`test`，密码是`1234`的用户。

   + 此处的`localhost`是指用户只能在本地登录，不能在另外一台机器上远程登录。如果要远程登录的话，将`localhost`改为`%`，表示在任何一台电脑上都可以登录。

2. 用户授权

   授权格式：grant 权限 on 数据库.* to 用户名@登录主机 identified by "密码"；

   登录数据库

   ```sql
   @>musql -u root -p
   @>[passwd]
   ```

   为用户创建一个数据库

   ```sql
   mysql>create database testDB;
   ```

   授权`test`用户拥有`testDB`数据库的**所有权限**(某个数据库的所有权限)

   ```sql
   mysql>grant all privileges on testD.* to test@localhost identified by '1234';
   mysql>flush privileges;//刷新系统权限表
   ```

   格式：grant 权限 on 数据库.* to 用户名@登录主机 identified by "密码"；

   授权`test`用户拥有`testDB`数据库的**部分权限**(某个数据库的所有权限)

   ```sql
   mysql>grant select,update privileges on testDB.* to test@localhost identified by '1234';
   mysql>flush privileges;//刷新系统权限表
   ```

   授权`test`用户拥有所有数据库的**所有权限**(某个数据库的所有权限)

   ```sql
   mysql>grant all privileges on *.* to test@localhost identified by '1234';
   mysql>flush privileges;//刷新系统权限表
   ```

   授权`test`用户拥有所有数据库的**部分权限**(某个数据库的所有权限)

   ```sql
   mysql>grant select,update privileges on *.* to test@localhost identified by '1234';
   mysql>flush privileges;//刷新系统权限表
   ```

3. 删除用户

   ```sql
   @>musql -u root -p
   @>[passwd]
   mysql>Delete FROM user Where User='test' and Host='localhost';
   mysql>flush privileges;
   mysql>drop database testDB;//删除用户的数据库
   mysql>drop user test@'%';//删除账号及权限
   mysql>drop user test@'localhost';//删除账号及权限
   ```

4. 修改指定用户密码

   ```sql
   @>musql -u root -p
   @>[passwd]
   mysql>update mysql.user set password=password('新密码') where User='test' and Host='localhost';
   mysql>flush privileges;
   ```

5. 列出所有数据库

   ```sql
   @>musql -u root -p
   @>[passwd]
   mysql>show databases;
   ```

6. 切换数据库

   ```sql
   @>musql -u root -p
   @>[passwd]
   mysql>use '数据库名称'
   ```

7. 列出所有表

   ```sql
   @>musql -u root -p
   @>[passwd]
   mysql>show tables;
   ```

8. 显示数据表结构

   ```sql
   @>musql -u root -p
   @>[passwd]
   mysql>describe 表名称;
   ```

9. 删除数据库和数据表

   ```sql
   @>musql -u root -p
   @>[passwd]
   mysql>drop databases 数据库名称;
   mysql>drop table 表名称;
   ```

## 查询某个表的记录总数

```sql
SELECT COUNT(*) FROM TABLE_NAME
```

## NOT IN 和IN

MySQL中的IN运算符用来判断表达式的值是否位于给出的列表中。如果是，返回值1，否则返回值为0.

NOT IN的作用恰好相反，用来判断表达式的值是否不存在于给出的列表中。如果是，返回值1，否则返回值为0.

```sql
mysql> SELECT 1 IN (1,2,3),10 IN (1,2,3);
+--------------+---------------+
| 1 IN (1,2,3) | 10 IN (1,2,3) |
+--------------+---------------+
|            1 |             0 |
+--------------+---------------+
1 row in set

mysql> SELECT 1 NOT IN (1,2,3),10 NOT IN (1,2,3);
+------------------+-------------------+
| 1 NOT IN (1,2,3) | 10 NOT IN (1,2,3) |
+------------------+-------------------+
|                0 |                 1 |
+------------------+-------------------+
1 row in set

mysql> SELECT NULL NOT IN (1,2,3),10 NOT IN (1,2,3);
+---------------------+-------------------+
| NULL NOT IN (1,2,3) | 10 NOT IN (1,2,3) |
+---------------------+-------------------+
| NULL                |                 1 |
+---------------------+-------------------+
1 row in set

mysql> SELECT NULL NOT IN (1,2,3),10 NOT IN (1,NULL,3);
+---------------------+----------------------+
| NULL NOT IN (1,2,3) | 10 NOT IN (1,NULL,3) |
+---------------------+----------------------+
| NULL                | NULL                 |
+---------------------+----------------------+
1 row in set

mysql> SELECT NULL NOT IN (1,2,3),10 NOT IN (10,NULL,3);
+---------------------+-----------------------+
| NULL NOT IN (1,2,3) | 10 NOT IN (10,NULL,3) |
+---------------------+-----------------------+
| NULL                |                     0 |
+---------------------+-----------------------+
1 row in set

mysql> SELECT NULL IN (1,2,3),10 IN (10,NULL,3);
+-----------------+-------------------+
| NULL IN (1,2,3) | 10 IN (10,NULL,3) |
+-----------------+-------------------+
| NULL            |                 1 |
+-----------------+-------------------+
1 row in set

mysql> SELECT NULL IN (1,2,3),10 IN (1,2,3);
+-----------------+---------------+
| NULL IN (1,2,3) | 10 IN (1,2,3) |
+-----------------+---------------+
| NULL            |             0 |
+-----------------+---------------+
1 row in set
```

一般情况，可以嵌套`select`语句

```sql
SELECT * FROM jc_tfundinfo WHERE fund_id NOT IN (SELECT fund_id FROM jc_tasset)
```

如果两张表中有相同的字段，可以用来比较差异

## UPDATE

更新表数据操作

```sql
UPDATE TABLE_NAME SET COLUMN_NAME = VALUE_NEW WHERE CONDITIONS
```

+ 可以同时更新一个或多个字段
+ 可以在WHERE子句中指定任何条件
+ 可以在单独表中同时更新数据

## INSERT

插入表数据

1. 向表中插入一行数据

   ```sql
   INSERT INTO table1 (column1,column2,...)
   VALUES (value1,value2,...);
   ```

   在表中插入新行的时候要注意：

   + 首先，值的数量必须与列的数量相同，此外，列和值必须是对应的，因为数据库系统将通过他们在列表中的相对位置来匹配他们
   + 其次，在添加新行之前，数据库系统检查所有完整性约束，例如外键约束、主键约束、检查约束和非空约束。如果违反了其中一个约束，数据库系统将发出错误并终止语句，而不向表中插入任何行

   如果值序列与表中列的顺序匹配，则无需指定列。

   ```sql
   INSERT INTO table1
   VALUES (column1,column2,...);
   ```

2. 向表中插入多行数据

   ```sql
   INSERT INTO table1 (column1,column2,...)
   VALUES (value1,value2,...),(value1,value2,...),(value1,value2,...);
   ```

3. 从其他表复制行记录

   ```sql
   INSERT INTO table1(column1,column2)
   SELECT
       column1,
       column2
   FROM
       table2
   WHERE
       condition1;
   ```

   在这个语法中，使用`SELECT`称为子选择而不是`VALUES`子句。子选择可以包含连接，以便可以组合来自多个表的数据。执行语句的时候，数据库系统在插入数据之前首先评估子选择

## 连接

   首先创建两张需要JOIN的表：

   ```sql
   CREATE TABLE tbl_emp (
   id INT (11) NOT NULL AUTO_INCREMENT,
   name VARCHAR (30) DEFAULT NULL,
   deptID VARCHAR (40) DEFAULT NULL,
   PRIMARY KEY (id)
   ) ENGINE = INNODB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8mb4 COLLATE=utf8mb4_bin;

   CREATE TABLE tbl_dept (
   id INT (11) NOT NULL AUTO_INCREMENT,
   deptName VARCHAR (30) DEFAULT NULL,
   locAdd VARCHAR (40) DEFAULT NULL,
   PRIMARY KEY (‘id‘)
   ) ENGINE = INNODB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_bin;
   ```

   新建的表展示为：

   |id|deptName|locAdd|
   |--|----|------|
   |1 |A   |11    |
   |2 |B   |12    |
   |3 |C   |13    |
   |4 |D   |14    |
   |5 |F   |15    |

   |id|name|deptID|
   |--|--------|------|
   |1 |z       |1     |
   |2 |y       |1     |
   |3 |x       |1     |
   |4 |w       |2     |
   |5 |v       |2     |
   |6 |u       |3     |
   |7 |t       |4     |
   |8 |s       |51    |

1. INNER JOIN

   内连接或者等值连接，获取两个表中字段匹配关系的记录，取得是是两个表的交集，共有的部分

   ```sql
   SELECT *
   FROM tbl_emp a
   INNER JOIN tbl_dept b
   ON a.deptID=b.id
   ```

   结果展示为

   |id|name|deptID|id1|deptName|locAdd|
   |--|--------|--|---|----|------|
   |1 |z       |1 |1  |A   |11    |
   |2 |y       |1 |1  |A   |11    |
   |3 |x       |1 |1  |A   |11    |
   |4 |w       |2 |2  |B   |12    |
   |5 |v       |2 |2  |B   |12    |
   |6 |u       |3 |3  |C   |13    |
   |7 |t       |4 |4  |D   |14    |

2. LEFT JOIN

   左连接，获取左表所有记录，即使右表没有对应匹配的记录，以左表为主表

   ```sql
   SELECT *
   FROM tbl_emp a
   LEFT JOIN tbl_dept b
   ON a.deptID=b.id
   ```

   结果展示为

   |id|name|deptID|id1|deptName|locAdd|
   |--|--------|--|---|----|------|
   |1 |z       |1 |1  |A   |11    |
   |2 |y       |1 |1  |A   |11    |
   |3 |x       |1 |1  |A   |11    |
   |4 |w       |2 |2  |B   |12    |
   |5 |v       |2 |2  |B   |12    |
   |6 |u       |3 |3  |C   |13    |
   |7 |t       |4 |4  |D   |14    |
   |8 |s       |51|NULL|NULL|NULL |

3. RIGHT JOIN

    右连接，获取右表所有记录，即使左表没有对应匹配的记录，以右表为主表

    ```sql
    SELECT *
    FROM tbl_emp a
    RIGHT JOIN tbl_dept b
    ON a.deptID=b.id
    ```

    结果展示为

    |id|name|deptID|id1|deptName|locAdd|
    |--|--------|--|---|----|------|
    |1 |z       |1 |1  |A   |11    |
    |2 |y       |1 |1  |A   |11    |
    |3 |x       |1 |1  |A   |11    |
    |4 |w       |2 |2  |B   |12    |
    |5 |v       |2 |2  |B   |12    |
    |6 |u       |3 |3  |C   |13    |
    |7 |t       |4 |4  |D   |14    |
    |NULL|NULL  |NULL|5|F   |15    |

4. 查找左表独立存在的数据

   ```sql
   SELECT
	   *
   FROM
	   tbl_emp a
   LEFT JOIN tbl_dept b ON a.deptID = b.id
   WHERE
	   b.id IS NULL
   ```

   结果展示为

   |id|name|deptID|id1|deptName|locAdd|
   |--|--------|--|---|----|------|
   |8 |s       |51|NULL|NULL|NULL |

5. 查找右表独立存在的数据

   ```sql
   SELECT
    *
   FROM
     tbl_emp a
   LEFT JOIN tbl_dept b ON a.deptID = b.id
   WHERE
     b.id IS NULL
   ```

    结果展示为

    |id|name|deptID|id1|deptName|locAdd|
    |--|--------|--|---|----|------|
    |NULL|NULL|NULL|5|F|15|

6. 查找两个表的并集

    ```sql
    SELECT
    	*
    FROM
    	tbl_emp a
    LEFT JOIN tbl_dept b ON a.deptID = b.id
    UNION
    SELECT
    	*
    FROM
    	tbl_emp a
    RIGHT JOIN tbl_dept b ON a.deptID = b.id
    ```

    结果展示为

    |id|name|deptID|id1|deptName|locAdd|
    |--|--------|--|---|----|------|
    |1 |z       |1 |1  |A   |11    |
    |2 |y       |1 |1  |A   |11    |
    |3 |x       |1 |1  |A   |11    |
    |4 |w       |2 |2  |B   |12    |
    |5 |v       |2 |2  |B   |12    |
    |6 |u       |3 |3  |C   |13    |
    |7 |t       |4 |4  |D   |14    |
    |8 |s       |51|NULL|NULL|NULL |
    |NULL|NULL  |NULL |5|F   |15   |

7. 查找两个表独立存在的数据的并集

    ```sql
    SELECT
    	*
    FROM
    	tbl_emp a
    LEFT JOIN tbl_dept b ON a.deptID = b.id
    WHERE
    	b.id IS NULL
    UNION
    	SELECT
    		*
    	FROM
    		tbl_emp a
    	RIGHT JOIN tbl_dept b ON a.deptID = b.id
    	WHERE
    		a.deptID IS NULL
    ```

    结果展示为

    |id|name|deptID|id1|deptName|locAdd|
    |--|--------|--|---|----|------|
    |8 |s       |51|NULL|NULL|NULL |
    |NULL|NULL  |NULL|5  |F   |15  |
