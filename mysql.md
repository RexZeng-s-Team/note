## 根据列名称查询表名

``` sql
SELECT COLUMN_NAME,TABLE_NAME,TABLE_SCHEMA FROM INFORMATION_SCHEMA.COLUMNS WHERE COLUMN_NAME='';
```

## 修改表以及表中字符型字段的字符集

``` sql
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

#### 查询的同时构建新的sql语句

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

   ```
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

   ```
   #sql语句
   SET GLOBAL sql_mode = 'modes...';
   或
   SET SESSION sql_mode = 'modes...';
   ```

   使用sql语句更改sql_mode的时候，不需要重启数据库，如果更改后没有效果，更改配置文件，重启数据库。

   mysql的配置文件在其安装路径下的config文件夹中my.cnf，有的时候在/etc/my.cnf中也可以配置

   ```
   [mysqld]
   sql_mode="STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION,PIPES_AS_CONCAT"
   ```

   在段落[mysqld]中有个配置项是sql_mode

3. 常用的sql_mode值

   | mode值                     | 说明                                                         |
   | -------------------------- | ------------------------------------------------------------ |
   | ONLY_FULL_GROUP_BY         | 对于GROUP BY聚合操作，如果在SELECT中的列，没有在GROUP BY中出现，那么这个SQL是不合法的，因为列不在GROUP BY从句中 |
   | NO_AUTO_VALUE_ON_ZERO      | 该值影响自增长列的插入。默认设置下，插入0或NULL代表生成下一个自增长值。如果用户希望插入的值为0，而该列又是自增长的，那么这个选项就有用了 |
   | STRICT_TRANS_TABLES        | 在该模式下，如果一个值不能插入到一个事务中，则中断当前的操作，对非事务表不做限制 |
   | NO_ZERO_IN_DATE            | 在严格模式下，不允许日期和月份为零                           |
   | NO_ZERO_DATE               | 设置该值，mysql数据库不允许插入零日期，插入零日期会抛出错误而不是警告 |
   | ERROR_FOR_DIVISION_BY_ZERO | 在insert或update过程中，如果数据被零除，则产生错误而非警告。如果未给出该模式，那么数据被零除时Mysql返回NULL |
   | NO_AUTO_CREATE_USER        | 禁止GRANT创建密码为空的用户                                  |
   | NO_ENGINE_SUBSTITUTION     | 如果需要的存储引擎被禁用或未编译，那么抛出错误。不设置此值时，用默认的存储引擎替代，并抛出一个异常 |
   | PIPES_AS_CONCAT            | 将\|\|视为字符串的连接操作符而非或运算符，这和Oracle数据库是一样是，也和字符串的拼接函数Concat想类似 |
   | ANSI_QUOTES                | 启用ANSI_QUOTES后，不能用双引号来引用字符串，因为它被解释为识别符 |

   升级mysql到8.0后，sql_mode不支持NO_AUTO_CREATE_USER，不用设置这个值

## 设置表中一个字段等于另一个字段

```sql
update table_name set '新字段'='旧字段'
```

