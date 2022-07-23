# C#高级编程

## .NET类型

C#有15中预定义的类型，其中有13中值类型，2种引用类型

### 值类型

|名称|类型|说明|位数|范围|
|----|---|----|---|----|
|sbyte|System.SByte|8位有符号整数||-2^7^-2^7^-1|
|short|System.Int16|16位有符号整数||-2^15^-2^15^-1|
|int|System.Int32|32位有符号整数||-2^31^-2^31^-1|
|long|System.Int64|64位有符号整数||-2^63^-2^63^-1|
|byte|System.Byte|8位无符号整数||0-2^8^-1|
|ushort|System.UInt16|16位无符号整数||0-2^16^-1|
|uint|System.UInt32|32位无符号整数||0-2^32^-1|
|ulong|System.UInt64|64位无符号整数||0-2^64^-1|
|float|System.Single|32位单精度浮点数|7||
|double|System.Double|64位双精度浮点数|15~16||
|decimal|System.Decimal|128位高精度十进制数表示法|28||
|bool|System.Boolean|true或者false||true/false|
|char|System.Char|表示一个16位Unicode字符|16||

### 引用类型

|名称|类型|说明|
|----|---|----|
|object|System.Object|根类型，其他类型都是从它派生而来|
|string|System.String|Unicode字符串|

## 方法

### 扩展方法

有很多扩展类的方法，继承是其中一种，能够给对象添加方法。在不能使用继承的情况下，比如封闭类，使用扩展方法也可以很好的给对象增加方法。扩展方法是静态的，是类的一部分，但实际上没有放在类的源码中。

     ```C#
     /// <summary>
     /// 原始类
     /// </summary>
     public class Test
     {
         //todo
     }
     /// <summary>
     /// 扩展方法-静态类
     /// </summary>
     public static class TestExtend
     {
         /// <summary>
         /// 扩展的方法
         /// </summary>
         /// <param name="test">需要扩展类的实例</param>
         public static void ShowHello(this Test test)
         {
            Console.WriteLine("Hello world");
         }
     }
     /// <summary>
     /// 实际调用的时候，原始类的对象可以使用扩展出来的方法
     /// </summary>
     static void Main()
     {
         Test test = new Test();
         test.ShowHello();
     }
     ```

## 泛型

泛型的几个优点和缺点

+ 性能：在使用泛型的情况下，有些情况可以使用object类型代替，虽然功能上实现可行，但是使用object类型代替会造成类型之间的转换，避免不了拆箱和装箱的操作。在C#中，值类型存储在栈上，而引用类型存储在堆上。值类型转换为引用类型比较容易，经过装箱即可，反过来引用类型转化为值类型，拆箱需要使用类型前置转换运算符。所以，使用泛型可以在很大程度上提升性能。
+ 类型安全性：和ArrayList类一样，如果使用对象，就可以在这个集合中添加任意类型。

     ```C#
     var list = new ArrayList();
     list.Add(44);//列表中添加一个数值类型的元素
     list.Add("mystring");//列表中添加一个字符串类型的元素
     list.Add(new MyClass());//列表中添加一个自定义类型的元素
     ```

     对于上述的列表，如果需要遍历，列表中的元素类型不一致，遍历的时候就会出错，运行时出错。
     像这种错误应该今早发现，在泛型类List<\T>中，定义了允许使用的类型，这样，错误就能在编译的时候发现。

+ 二进制代码重用：泛型允许更好地重用二进制代码，可以定义一次，并且可以用许多不同的类型实例化
+ 代码的扩展：在用不同的特定类型实例化泛型时，会创建很多的代码，因为泛型类的定义会放在程序集中，所以用特定类型实例化泛型类不会在IL代码中复制这些类。
+ 命名约定：1.泛型类型的名称用字母T作为前缀；2.如果没有特殊要求，泛型类型允许用任意类型代替，并且只是用了一个泛型类型，就可以用ifuT作为泛型类型的名称；3.如果泛型类型有特定的要求，或者使用了两个或多个泛型类型，就应该给泛型类型使用描述性的名称

     ```C#
     ///泛型方法示例
     public void ShowInt(int a)
     {
         Console.WriteLine("This is {0},parameter={1},type={2}",
                            typeof(Form1).Name, a.GetType().Name, a);
     }
     public void ShowString(string text)
     {
         Console.WriteLine("This is {0},parameter={1},type={2}",
                            typeof(Form1).Name, text.GetType().Name, text);
     }
     public void ShowDateTime(DateTime dateTime)
     {
         Console.WriteLine("This is {0},parameter={1},type={2}",
                            typeof(Form1).Name, dateTime.GetType().Name, dateTime);
     }
     public void ShowObject(Object o)
     {
         Console.WriteLine("This is {0},parameter={1},type={2}",
                            typeof(Form1).Name, o.GetType().Name, o);
     }
     public void Show<T> (T t)
     {
         Console.WriteLine("This is {0},parameter={1},type={2}",
                            typeof(Form1).Name, t.GetType().Name, t);
     }
     ```

     `ShowObject`这种方法也可以实现代码不重复，但是会增加拆箱和装箱的动作，增加了性能损耗，并且存在一定的类型安全问题，在类型相互转换的时候可能会出现转换失败的情况。

     声明泛型类型时，确定了类型实参，所以不需要拆箱和装箱，而且将大量的安全检查从运行时转移到了编译时，保证了类型安全。

### 泛型类

泛型类示例：

```C#
///泛型类
public class Show<T>
{
    public void ShowMsg(T t)
    {
        Console.WriteLine("This is ShowMsg,parameter={0},type={1}",
                        t.GetType().Name, t);
    }
}
public class Program
{
    public void Main(string[] args)
    {
        Show<int> show_i = new Show<int>();
        show_i.ShowMsg(999);
        Show<string> show_s = new Show<string>();
        show_s.ShowMsg("888");
    }
}
```

在创建泛型类时，还需要一些其他C#关键字。

默认值

现在给`Show<T>`类添加一个`GetInstance()`的方法，在这个方法中，应把类型T指定为`null`，但是，不能把`null`赋值给泛型类型。原因是泛型类型也可以实例化为值类型，而`null`只能用于引用类型。为了解决这个问题，可以使用`default`关键字。通过`default`关键字，将`null`赋值给引用类型，将`0`赋值为值类型。

```C#
///泛型类
public class Show<T>
{
    public T Getinstance()
    {
        T t = default;
        return t;
    }
}
```

约束

定义泛型类时，可以对客户端代码能够在实例化类时用于类型参数的几种类型施加限制。如果客户端代码尝试使用约束所不允许的类型来实例化类，则会产生编译时错误。这些限制成为约束。通过使用`where`上下文关键字指定约束。

|约束|说明|
|----|----|
|where T:struct|对于结构约束，类型T必须是值类型|
|where T:class|类约束指定类型T必须是引用类型|
|where T:IFoo|指定类型T必须实现接口IFoo|
|where T:Foo|指定类型T必须派生自基类foo|
|where T:new()|这是一个构造函数约束，指定类型T必须有一个默认构造函数|
|where T1:T2|这个约束也可以指定，类型T1派生自泛型类型T2|

继承

泛型类可以实现泛型接口，也可以派生自一个类，要求是必须重复接口的泛型类型，或者必须指定基类的类型。

```C#
///泛型基类
public class Base<T>
{
    //todo
}
///泛型类
public class Derived<T>: Base<string>
{
    //todo
}
```

派生类可以使泛型类或者非泛型类，还可以定义一个部分的特殊操作。

```C#
///抽象的泛型类
public abstract class Calc<T>
{
    public abstract T Add(T x, T y);
    public abstract T Sub(T x, T y);
}
///非泛型类作为派生类
public class IntClass : Calc<int>
{
    public override abstract int Add(int x, int y);
    public override abstract int Sub(int x, int y);
}
///定义一个泛型参数
public class Query<TRequest, TResult>
{
    //todo
}
///派生类
public class StringQuery<TRequest>: Query<TRequest, string>
{
    //todo
}
```

静态成员

泛型类的静态成员只能在类的一个实例中共享，不同的具体实现中，静态成员有不同的值。

### 泛型接口

使用泛型可以定义接口，在接口中定义的方法可以带泛型参数。

协变：如果泛型类型用`out`关键字标注，泛型接口就是协变的。这也意味着返回类型只能是`T`。
抗变：如果泛型类型用`in`关键字标注，泛型接口就是抗变的。这样，接口只能把泛型类型T用作其方法的输入。

```C#
//泛型接口
public interface IDisplay<T>
{
    void Show(T item);
}
//泛型类实现泛型接口
public class ShapDisplay<T>:IDisplay<T>
{
    public void Show(T item)
    {
        Console.WriteLine("测试成功");
    }
}
//自定义一个父类
public class ParentClass
{

}
//自定义一个子类
public class SubClass:ParentClass
{

}
//实际调用测试
class Program
{
    static void Main(string[] args)
    {
        //用子类实例化泛型类，称为子类对象
        IDisplay<SubClass> sub1 = new ShapDisplay<SubClass>();

        //用父类实例化泛型类，称为父类对象
        IDisplay<ParentClass> par1 = new ShapDisplay<ParentClass>();

        //用父类类型接收子类对象，协变
        IDisplay<ParentClass> parent = sub1;

        //用子类类型接收父类对象，抗变
        IDisplay<SubClass> sub = par1;
    }
}
```

如果按照上述的代码进行编译，是不会通过的，原因是我们在定义接口的时候，没有加`out`或`in`关键字，即泛型接口默认不支持协变和抗变。

```C#
//修改泛型接口，只增加out关键字，那么泛型接口支持抗变
public interface IDisplay<out T>
{
    T Show();
}
//泛型类实现泛型接口
public class ShapDisplay<T>:IDisplay<T>
{
    T item = default;
    public T Show()
    {
        return item;
    }
}
//修改泛型接口，只增加out关键字，那么泛型接口支持抗变
public interface IDisplay<in T>
{
    void Show(T item);
}
//泛型类实现泛型接口
public class ShapDisplay<T>:IDisplay<T>
{
    public void Show(T item)
    {
        Console.WriteLine("测试成功");
    }
}
//修改泛型接口，同时有out和in关键字
public interface IDisplay<in T, out K>
{
    void Show(T item);
    K ShowK();
}
public class ShapDisplay<T,K> : IDisplay<T,K>
{
    K item = default;
    public void Show(T item)
    {
        Console.WriteLine("测试成功");
    }
    public K ShowK()
    {
        return item;
    }
}
```

小结：

+ 泛型接口，如果泛型类型前没有关键字`out`后者`in`标注，则该泛型接口不支持抗变和协变，即只能是什么对象指向什么类型。
+ 如果泛型接口使用关键字`out`标注，表示其方法的输出为`T`类型，也就是方法的返回值。可以用父类类型指向子类对象。
+ 如果泛型接口使用关键字`in`标注，表示其方法的输入为`T`类型，也就是方法的参数，可以用子类的类型执行父类的对象

### 泛型结构

与类相似，结构也可以是泛型的，只是没有继承特性。
`Nullable<T>`结构：表示可分配有`null`的值类型。

```C#
Nullable<int> x;
x = 4;
x += 3;
if(x.HasValue)
{
    int y = x.Value;
}
x = null;
```

一般情况下，申明一个值类型的变量是不允许为空的。也可以使用类似`int?`来声明变量，此时为可空类型。

### 泛型方法

## 反射

反射指程序可以访问、检测和修改它本身状态或行为的一种能力。
程序集包含模块，而模块包含类型，类型又包含成员。反射则提供了封装程序集、模块和类型的对象。
优点：

+ 反射提高了成都徐的灵活性和扩展性
+ 降低耦合性，提高自适应能力
+ 允许程序创建和控制任何类的对象，无需提前硬编码目标类

缺点：

+ 性能问题：使用反射基本上是一种解释操作，用于字段和方法接入时要远慢于直接代码。因此反射机制主要应用再对灵活性和拓展性要求很高的系统框架上，普通程序不建议使用。
+ 使用反射会模糊程序内部逻辑，程序员希望在源代码钟看到程序的逻辑，反射却绕过了源代码的技术，因而会带来维护的问题，反射代码比相应的直接代码更复杂。

特点：

+ 允许在运行时查看特性信息
+ 允许审查集合的各种类型，以及实例化这些类型
+ 允许延迟绑定的方法和属性
+ 允许在运行时创建新类型，然后使用这些类型执行一些任务

用途：

+ 使用`Assembly`定义和加载程序集，加载在程序集清单中列出模块，以及从此程序集中查找类型并创建该类型的实例。
+ 使用`Module`了解包含模块的程序集以及模块中的类等，还可以获取在模块上定义的所有全局方法或其他特定的非全局方法
+ 使用`ConstructorInfo`了解构造函数的名称、参数、访问修饰符和实现详细信息等
+ 使用`MethodInfo`了解方法的名称、返回类型、参数、访问修饰符和实现详细信息等
+ 使用`FieldInfo`了解字段的名称、返回类型、参数、访问修饰符和实现详细信息等，并获取或设置字段值
+ 使用`EventInfo`了解事件的名称、事件处理程序数据类型、自定义属性、声明类型和反射类型等，添加或移除事件处理程序
+ 使用`PropertyInfo`了解属性的名称、数据类型、声明类型、反射类型和只读或可写状态等，获取或设置属性值
+ 使用`ParameterInfo`了解参数的名称、数据类型、是入参还是出参，以及参数在方法签名中的位置等

反射用到的名称空间

```C#
    System.Reflection
```

反射用到的主要的类

```C#
    System.Type;                //通过这个类剋访问任何给定数据类型的信息
    System.Reflection.Assembly; //可以用于访问给定程序集的信息，或者把这个程序集加载到程序中
```

`System.Type`类
使用这个类只是为了存储类型的引用。对于反射起着核心的作用，是一个抽象的基类，有与每种数据类型对应的派生类，我们使用这个派生类的对象的方法、字段和属性来查找有关该类型的所有信息。
获取给定类型的`Type`引用有3中常用的方式

```C#
//使用typeof运算符
Type type = typeof(string);
//使用对象GetType()方法
string s = "Hello world"
Type type = s.GetType();
//调用Type类的静态方法GetType()
Type type = Type.GetType("Hello world");
```

`Type`类的常用的属性
|属性|说明|
|----|---|
|FullName|数据类型的完全限定名称|
|NameSpace|定义数据类型的命名空间名称|
|IsAbstract|指示该类型是否为抽象类型|
|IsArray|指示该类型是否为数组|
|IsClass|指示该类型是否为类|
|IsEnum|指示该类型是否为枚举|
|IsInterface|指示该类型是否为接口|
|IsPublic|指示该类型是否为共有的|
|IsSealed|指示该类型是否为密封类|
|IsValueType|指示该类型是否为值类型|

`Type`类的常用的方法
|方法|说明|
|----|----|
|GetConstructor()</br>GetConstructors()|返回ConstructorInfo类型，用于取得该类型的构造函数的信息|
|GetEvent()</br>GetEvents()|返回EventInfo类型，用于取得该类型的事件信息|
|GetField()</br>GetFields()|返回FieldInfo类型，用于取得该类型的字段(成员变量)信息|
|GetInterface()</br>GetInterfaces()|返回InterfaceInfo类型，用于取得该类型实现的接口信息|
|GetMember()</br>GetMembers()|返回MemberInfo类型，用于取得该类型的成员信息|
|GetMethod()</br>GetMethods()|返回MethodInfo类型，用于取得该类型的方法信息|
|GetProperty()</br>GetProperties()|返回PropertyInfo类型，用于取得该类型的属性信息|

## 特性

特性提供功能强大的方法，用以将元数据或声明信息与代码相关联。特性与程序实体关联后，即可在运行时使用反射的技术查询特性。
特性的属性：

+ 特性可向程序中添加元数据。元数据是有关在程序中定义的类型的信息。
+ 可以将一个或多个特性应用到整个程序集、模块或较小的程序元素
+ 特性可以与方法和属性相同的方式接受参数
+ 程序可以使用反射检查自己的元数据或其他程序内的元数据

在生成的时候会生成pdb文件，这个文件中存储了关于程序集内部的所有的成员信息，例如，成员的数据类型，属性类型，方法返回值，方法入参类型，就是程序集内部所有的定义信息的数据信息，是存储定义信息的一类数据信息，程序集里面的所有关于声明类的数据信息，包括方法间调用，都是存放在元数据里面。

1. 声明自定义特性
2. 构建自定义特性
3. 在目标程序元素上应用自定义特性
4. 通过反射访问特性

```C#
public class TestAttribute : Attribute
{
    private string _remark;

    public string Remark
    {
        get { return _remark; }
        set { _remark = value; }
    }


    public TestAttribute(string remark)
    {
        _remark = remark;
    }
}
public enum Operator
{
    [Test("增加")]
    Add,
    [Test("删除")]
    Remove,
    [Test("查找")]
    Find
}

public static class OperatorEx
{
    public static void GetRemark(this Operator @operator)
    {
        Type type = typeof(Operator);

        var name = type.GetEnumName(@operator);

        FieldInfo item = type.GetField(name);

        Console.WriteLine(item.GetCustomAttribute<TestAttribute>()?.Remark);
    }
}
public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");
        Operator.Add.GetRemark();
        Operator.Remove.GetRemark();
        Operator.Find.GetRemark();
        Console.ReadLine();
    }
}
```

## 运算符

1. 条件运算符：也称三元运算符

```C#
condition ? true_value : false_value ;
```

其中`condition`是条件判断的表达式，`true_value`是`condition`为真时的值，`false_value`是`condition`为假时的值。
2. checked和unchecked运算符
如果把一个代码块标记为`checked`，CLR就会做溢出检查，如果溢出就会抛出`OverflowException`异常。

```C#
byte b=255;
checked
{
    b++;
}
Console.WriteLine(b);
```

运行这段代码，可以接收到一个异常报错。
对所有未标记的代码进行溢出检查，修改项目文件：

```xml
<PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp2.0</TargetFramework>
    <CheckForOverflowUnderflow>true</CheckForOverflowUnderflow>
</PropertyGroup>
```

如果要禁止溢出检查，则可以把代码块标记为`unchecked`：

```C#
byte b=255;
unchecked
{
    b++;
}
Console.WriteLine(b);
```

运行这段代码，不会抛出异常，但会丢失数据，因为`byte`数据类型不能包含256，溢出的位会被丢弃，变量`b`得到的值是0。
3. is运算符
`is`运算符可以检查对象是否与特定的类型兼容(兼容是指对象是该类型，或者派生自该类型)

```C#
int i = 42;
if(i is 42)
{
    Console.WriteLine("i has the value 42");
}
object o = null;
if(o is null)
{
    Console.WriteLine("o is null");
}
```

使用具有类型匹配的`is`运算符，可以在类型的右边声明变量。如果`is`运算符返回true，则该变量通过对该类型的对象的引用来填充。

```C#
public static void AMethodUsingPatternMatching(object o)
{
    if(o is Person P)
    {
        Console.WriteLine("");
    }
}
AMethodUsingPatternMatching (new Person());
```

`is`运算符的`if`语句内可以使用该变量。
4. as运算符
`as`运算符用于执行引用类型的显示类型转换。如果要转换的类型与指定的类型兼容，转换就会成功进行；如果类型不兼容，就会返回`null`值。

```C#
object o1 = "some string";
object o2 = 5;
string s1 = o1 as string;
string s2 = o2 as string;
```

`as`运算符允许在一步中进行安全的类型转换，不需要先使用`is`测试类型，再进行类型转换。
5. sizeof运算符
使用`sizeof`运算符可以确定栈中*值类型*需要的长度(单位是字节)。类不能使用这个运算符。
6. typeof运算符
`typeof`运算符返回一个表示特定类型的`System.Type`对象。
7. nameof运算符
该运算符接受一个符号、属性或方法，并返回其名称。
8. 空合并运算符
空合并运算符`??`提供了一种快捷方式，可以在处理可控类型和引用类型时表示`null`值的可能性。

```C#
int? a = null;
int b;
b = a ?? 10;//b has the value 10
a = 3;
b = a ?? 10;//b has the value 3
```

如果第一个操作数不是`null`，整个表达式就等于第一个操作数的值；如果第一个操作数是`null`，整个表达式就等于第二个操作数的值。
9. 空值条件运算符
示例

```C#
public void ShowPerson(Person p)
{
    if(p == null) return;
    string firstName = p.FirstName;
}
//使用空值条件运算符
public void ShowPerson(Person p)
{
    string firstName = p?.FirstName;
}
```

## 比较对象的相等

1. ReferenceEquals()方法
`ReferenceEquals()`是一个静态方法，测试两个引用是否指向类的同一个实例，特别是两个引用是否包含内存中的相同地址。

```C#
static void ReferenceEqualsSample()
{
    SomeClass x = new SomeClass(), y = new SomeClass(), z = x;
    //return true
    bool b1 = object.ReferenceEquals(null,null);
    //return false
    bool b2 = object.ReferenceEquals(null,x);
    //return false because x and y references different objects
    bool b3 = object.ReferenceEquals(x,y);
    //return true because x and z references the same object
    bool b4 = object.ReferenceEquals(x,z);
}
```

上面的例子说明了如果两个引用指向同一个对象实例，则返回`true`。
2. Equals()虚方法
这个是虚方法，可以在自己的类中重写，从而按照值来比较对象。值得注意的是，重写的代码不应该抛出异常。
3. 静态的Equals()方法
静态版本和虚方法的作用相同，区别是静态版本带有两个参数，并对它们进行相等性比较。如果参与比较的两个引用都是`null`，就返回`true`，如果只有一个引用是`null`，就返回`false`。如果两个引用实际上引用了某个对象，就调用`Equals()`的虚实例版本。
4. 比较运算符(==)
比较运算符是严格的值比较和严格的引用比较之间的中间选项。

## 集合

### 概述

数组的大小是固定的，在声明数组的时候就一直数组的大小，系统分配的控件固定不变。如果数组的大小是动态，可以实现动态的增大或者减小，就要使用集合。比如`List<T>`集合类。

大多数集合类都可以在`System.Collections`和`System.Collections.Generic`名称空间中找到。

|接口|说明|
|----|---|
|IEnumerable\<T\>|如果将`foreach`语句用于集合，就需要`IEnumerable`接口。这个接口定义了方法`GetEnumerator()`,返回一个实现了`IEnumerable`接口的枚举|
|ICollection\<T\>|`ICollection\<T\>`接口由泛型集合类实现。使用这个接口可以获得集合中的元素个数(`Count`属性),把集合复制到数组中(`CopyTo()`方法)，还可以从集合中添加和删除元素(`Add()、Remove()、Clear()`)|
|IList\<T\>|该接口用于可通过位置访问其中的元素的列表，这个接口定义了一个索引器，可以在集合的指定位置插入或删除某些项(`Insert()Remove()`)，派生自`ICollection\<T\>`|
|ISet\<T\>|`ISet\<T\>`由集实现，集允许合并不同的集，获得两个集的交集，检查两个集是否由重复。派生自`ICollection\<T\>`|
|IDictionary\<TKey,TValue\>|`IDictionary\<TKey,TValue\>`接口由包含类似键和值的泛型集合类实现。使用这个接口可以访问所有的键和值，使用键类型的索引器可以访问某些项，还可以添加或删除某些项|
|ILookup\<TKey,TValue\>|`ILookup\<TKey,TValue\>`接口类似于`IDictionary\<TKey,TValue\>`接口，实现该接口的集合有键和值，且可以通过一个键包含多个值|
|IComparer\<T\>|由比较器实现，通过`Comparer()`方法给集合中的元素排序|
|IEqualityComparar\<T\>|由一个比较器实现，该比较器可用于字典中的键。使用该接口，可以对对象进行相等性比较|

### 列表

.NET Framework为动态列表提供了泛型类`List<T>`。

1. 创建列表

```C#
var intList = new List<int>();
Var personList = new List<Person>();
```

在`List<T>`泛型类的实现代码中，使用了一个`T`类型的数组，通过重新分配内存，创建一个新数组，`Array.Copy()`方法将旧数组中的元素复制到新数组中。

```C#
intList.Capacity = 20;//这个属性可以获取或者设置集合的容量
intList.TrimExcess();//这个方法去除不需要的容量，如果元素个数超过了容量的90%，这个方法什么都不做
```

调用默认的构造函数，就可以创建列表对象
2. 集合初始值设定项

```C#
var insList = new List<int>() { 1,2};
```

集合初始值设定项没有反映在已编译的程序集IL代码中。编译器会把集合初始值设定项转换成对初始值设定项列表中的每一项调用`Add()`方法。
3. 添加元素

使用`Add()`方法可以给列表添加元素。
使用`AddRange()`方法，可以一次给集合添加多个元素。
集合初始值设定项只能在声明集合时使用。`AddRange()`方法则可以在初始化集合后调用。如果在创建集合后动态获取数据，就需要调用`AddRange()`方法。
4. 插入元素

使用`Insert()`方法可以在指定位置插入元素，使用`InserRange()`方法可以插入大量元素的功能，类似于前面的`AddRange()`方法。如果索引集大于集合中的元素个数，就会抛出异常。
5. 访问元素

```C#
int num = intList[2];//通过下标或者索引，访问序号为2的元素，第三个元素
```

可以通过索引访问的集合类有`ArrayList、StringCollection和List<T>`。
6. 删除元素

删除元素可以利用索引，也可以传递要删除的元素。

```C#
var intList = new List<int>() {1,2,3,4,5};
//使用索引删除第三个元素
intList.RemoveAt(2);
//使用传递元素的方式删除第三个元素
intList.Remove(3);
//删除多个元素
intList.RemoveRange(1,3);
```

`RemoveRange()`方法可以从集合中删除许多元素，第一个参数制定了开始删除元素的索引，第二个参数制定了要删除的元素个数。
7. 搜索

|方法|说明|
|----|----|
|Contains(T)|确定某元素是否在`List<T>`中|
|Exists(Predicate\<T\>)|确定`List<T>`是否包含与指定条件匹配的元素|
|Find(Predicate\<T\>)|搜索与指定条件相匹配的元素，并返回整个`List<T>`中的第一个匹配元素|
|FindAll(Predicate\<T\>)|检索与指定条件匹配的所有元素|
|FindIndex(Predicate\<T\>)|搜索与指定条件相匹配的元素，并返回整个集合中的第一个匹配元素的索引|
|FindLast\<T\>|搜索与指定条件相匹配的元素，并返回这个集合中的最后一个匹配的元素|

注意每个方法的返回值。
8. 排序

集合中可以使用`Sort()`方法对元素进行排序，采用**快速排序**算法，比较整个集合中的所有元素，知道整个列表排好序为止。
9. 只读集合
创建集合后，默认是可读可写的，`List<T>`集合的`AsReadOnly()`方法返回`ReadOnlyCollection<T>`类型的对象。

### 队列

队列是其元素以先进先出的方式来处理的集合。

```C#
using System;
using System.Collections.Generic;

class Example
{
    public static void Main()
    {
        Queue<string> numbers = new Queue<string>();
        numbers.Enqueue("one");
        numbers.Enqueue("two");
        numbers.Enqueue("three");
        numbers.Enqueue("four");
        numbers.Enqueue("five");

        // A queue can be enumerated without disturbing its contents.
        foreach( string number in numbers )
        {
            Console.WriteLine(number);
        }

        Console.WriteLine("\nDequeuing '{0}'", numbers.Dequeue());
        Console.WriteLine("Peek at next item to dequeue: {0}",
            numbers.Peek());
        Console.WriteLine("Dequeuing '{0}'", numbers.Dequeue());

        // Create a copy of the queue, using the ToArray method and the
        // constructor that accepts an IEnumerable<T>.
        Queue<string> queueCopy = new Queue<string>(numbers.ToArray());

        Console.WriteLine("\nContents of the first copy:");
        foreach( string number in queueCopy )
        {
            Console.WriteLine(number);
        }

        // Create an array twice the size of the queue and copy the
        // elements of the queue, starting at the middle of the
        // array.
        string[] array2 = new string[numbers.Count * 2];
        numbers.CopyTo(array2, numbers.Count);

        // Create a second queue, using the constructor that accepts an
        // IEnumerable(Of T).
        Queue<string> queueCopy2 = new Queue<string>(array2);

        Console.WriteLine("\nContents of the second copy, with duplicates and nulls:");
        foreach( string number in queueCopy2 )
        {
            Console.WriteLine(number);
        }

        Console.WriteLine("\nqueueCopy.Contains(\"four\") = {0}",
            queueCopy.Contains("four"));

        Console.WriteLine("\nqueueCopy.Clear()");
        queueCopy.Clear();
        Console.WriteLine("\nqueueCopy.Count = {0}", queueCopy.Count);
    }
}
```

队列使用`System.Collection.Generic`名称空间中的泛型类`Queue<T>`实现。在内部，使用T类型的数组，这类似于`List<T>`类型。内部没有实现`ICollection<T>`接口，因此不能使用方法`Add()`和`Remove()`方法。没有实现接口`IList<T>`，不能使用索引进行访问元素。队列只允许在队列中添加元素，存在队列的尾部，从队列的头部获取元素。

|方法/属性|说明|
|----|----|
|Count|返回队列中的元素个数|
|Enqueue()|在队列的一端添加一个元素|
|Dequeue()|在队列的头部读取和删除一个元素|
|Peek()|从队列的头部读取一个元素，但不删除|
|TrimExcess()|重新设置队列的容量。|

### 栈

栈是与队列非常类似的另一个容器，只是要使用不同的方法访问栈。最后添加到栈中的元素会最先读取。

```C#
using System;
using System.Collections.Generic;

class Example
{
    public static void Main()
    {
        Stack<string> numbers = new Stack<string>();
        numbers.Push("one");
        numbers.Push("two");
        numbers.Push("three");
        numbers.Push("four");
        numbers.Push("five");

        // A stack can be enumerated without disturbing its contents.
        foreach( string number in numbers )
        {
            Console.WriteLine(number);
        }

        Console.WriteLine("\nPopping '{0}'", numbers.Pop());
        Console.WriteLine("Peek at next item to destack: {0}",
            numbers.Peek());
        Console.WriteLine("Popping '{0}'", numbers.Pop());

        // Create a copy of the stack, using the ToArray method and the
        // constructor that accepts an IEnumerable<T>.
        Stack<string> stack2 = new Stack<string>(numbers.ToArray());

        Console.WriteLine("\nContents of the first copy:");
        foreach( string number in stack2 )
        {
            Console.WriteLine(number);
        }

        // Create an array twice the size of the stack and copy the
        // elements of the stack, starting at the middle of the
        // array.
        string[] array2 = new string[numbers.Count * 2];
        numbers.CopyTo(array2, numbers.Count);

        // Create a second stack, using the constructor that accepts an
        // IEnumerable(Of T).
        Stack<string> stack3 = new Stack<string>(array2);

        Console.WriteLine("\nContents of the second copy, with duplicates and nulls:");
        foreach( string number in stack3 )
        {
            Console.WriteLine(number);
        }

        Console.WriteLine("\nstack2.Contains(\"four\") = {0}",
            stack2.Contains("four"));

        Console.WriteLine("\nstack2.Clear()");
        stack2.Clear();
        Console.WriteLine("\nstack2.Count = {0}", stack2.Count);
    }
}
```

|成员|说明|
|----|----|
|Count|返回栈中的元素个数|
|Push()|在栈顶添加一个元素|
|Pop()|从栈顶删除一个元素，并返回该元素|
|Peek()|返回栈顶的元素，但是不删除|
|Contains()|确定某个元素是否在栈中|

### 链表

`LinkedList<T>`是一个双向链表，其元素指向它前面和后面的元素，通过移动到下一个元素可以正向遍历整个链表，通过移动到前一个元素可以反向遍历整个链表。

```C#
using System;
using System.Text;
using System.Collection.Generic;

public class Example
{
    public static void Main()
    {
        // create the link list.
        string[] words = {"the","fox","jumps","over","the","dog"};
        LinkedList<string> sentence = new LinkedList<string>(words);
        Display(sentence, "The linked list values:");
        Console.WriteLine("sentence.Contains(\"junmps\") = {0}",sentence.Contains("jumps"));

        // Add the word 'today' to the beginning of the linked list.
        sentence.AddFirst("today");
        Display(sentence, "Test 1: Add 'today' to beginning of the linked list:");

        // Move the first node to be the last node.
        LinkedListNode<string> mark1 = sentence.First;
        sentence.RemoveFirst();
        sentence.AddLast(mark1);
        Display(sentence, "Test 2: Move first node to be last node");

        // Change the last node to 'yesterday'.
        sentence.RemoveLast();
        sentence.AddLast("yesterday");
        Display(sentence, "Test 3: Change the last node to 'yesterday':");

        // Move the last node to be the first node
        mark1 = sentence.Last;
        sentence.RemoveLast();
        sentence.AddFirst(mark1);
        Display(sentence, "Test 4: Move the last node to be the first node");

        // Indicate the last occurence of 'the'
        sentence.RemoveFirst();
        LinkedListNode<string> current = sentence.FindLast("the");
        IndicateNode(current, "Test 5: Indicate last occurence of 'the':");

        // Add 'lazy' and 'old' after 'the'
        sentence.AddAfter(current, "old");
        sentence.AddAfter(current, "lazy");
        IndicateNode(current, "Test 6: Add 'lazy' and 'old' after 'the':");

        // Indicate 'fox' node
        current = sentence.Find("fox");
        IndicateNode(current, "Test 7: Indicate 'fox' node");

        // Add 'quick' and 'brown' before 'fox'
        sentence.AddBefore(current, "quick");
        sentence.AddBefore(current, "brown");
        IndicateNode(current, "Test 8: Add 'quick' and 'brown' before 'fox'");

        // Keep a reference to the current node, 'fox',
        // and to the previous node in the list. Indicate the 'dog' node.
        mark1 = current;
        LinkedListNode<string> mark2 = current.Previous;
        current = sentence.Find("dog");
        IndicateNode(current, "Test 9: Indicate the 'dog' node:");

        // The AddBefore method throws an InvalidOperationException
        // if you try to add a node that already belongs to a list.
        Console.WriteLine("Test 10: Throw exception by adding node (fox) already in the list:");
        try
        {
            sentence.AddBefore(current, mark1);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Exception message: {0}", ex.Message);
        }
        Console.WriteLine();

        // Remove the node referred to by mark1, and then add it
        // before the node referred to by current.
        // Indicate the node referred to by current.
        sentence.Remove(mark1);
        sentence.AddBefore(current, mark1);
        IndicateNode(current, "Test 11: Move a referenced node (fox) before the current node (dog):");

        // Remove the node referred to by current.
        sentence.Remove(current);
        IndicateNode(current, "Test 12: Remove current node (dog) and attempt to indicate it:");

        // Add the node after the node referred to by mark2.
        sentence.AddAfter(mark2, current);
        IndicateNode(current, "Test 13: Add node removed in test 11 after a referenced node (brown):");

        // The Remove method finds and removes the
        // first node that that has the specified value.
        sentence.Remove("old");
        Display(sentence, "Test 14: Remove node that has the value 'old':");

        // When the linked list is cast to ICollection(Of String),
        // the Add method adds a node to the end of the list.
        sentence.RemoveLast();
        ICollection<string> icoll = sentence;
        icoll.Add("rhinoceros");
        Display(sentence, "Test 15: Remove last node, cast to ICollection, and add 'rhinoceros':");

        Console.WriteLine("Test 16: Copy the list to an array:");
        // Create an array with the same number of
        // elements as the linked list.
        string[] sArray = new string[sentence.Count];
        sentence.CopyTo(sArray, 0);

        foreach (string s in sArray)
        {
            Console.WriteLine(s);
        }

        // Release all the nodes.
        sentence.Clear();

        Console.WriteLine();
        Console.WriteLine("Test 17: Clear linked list. Contains 'jumps' = {0}",
            sentence.Contains("jumps"));

        Console.ReadLine();
    }
    private static void Display(LinkedList<string> words, string test)
    {
        Console.WriteLine(test);
        foreach(string word in words)
        {
            Console.WriteLine($"{word} ");
        }
        Console.WriteLine();
        Console.WriteLine();
    }
    private static void IndicateNode(LinkedListNode<string> node, string test)
    {
        Console.WriteLine(test);
        if(node.List == null)
        {
            Console.WriteLine($"Node '{node.Value}' is not int the list.\n");
            return;
        }
        
        StringBuilder result = new StringBuilder($"({node.Value})");
        LinkedListNode<string> nodeP = node.Previous;

        while(nodeP != null)
        {
            result.Insert(0,nodeP.Value + " ");
            nodeP = nodeP.Previous;
        }

        node = node.Next;
        while(node != null)
        {
            result.Append(" " + node.Value);
            node = node.Next;
        }

        Console.WriteLine(result);
        Console.WriteLine();
    }
}
```

链表的优点是，如果将元素插入列表的中间位置，使用链表就会非常快。在插入一个元素时，只需要修改上一个元素的`Next`引用和下一个元素的`Previous`引用，使他们引用所插入的元素。
链表的缺点时只能一个一个地访问元素，需要比较长的时间来查找位于链表中中间或尾部的元素。
链表不能在列表中仅存储元素，还必须存储每个元素的上一个和下一个元素的信息。这就是`LinkedList<T>`包含`LinkedListNode<T>`类型的元素的原因。使用`LinkedListNode<T>`类，可以获得列表中的上一个和下一个元素，定义了属性`List`、`Next`、`Previous`、`Value`。


如果有需要基于建对所需集合排序，就是用`SortedList<TKey,TValue>`类。这个类按照键给元素排序。创建一个空列表后，使用`Add()`方法添加元素，除此之外，还可以使用索引器将元素添加到列表。

1. 字典的初始化

   ```C#
    Dictionary<int,string> dic = new Dictionary<int,string>()
    {
        [3] = "three",
        [7] = "seven"
    };
   ```

C#提供了一个语法，在声明时可以初始化字典。
另外一种初始化的操作，就是先声明一个空的字典，然后再使用`Add()`方法进行添加元素的操作。

    ```C#
    Dictionary<int,string> dic = new Dictionary<int,string>();
    dic.Add(3,"three");
    dic.Add(7,"seven");
    ```

在使用字典的时候，注意字典键和值的类型。
2. 字典的属性

|属性名称|说明|
|--------|---|
|Comparer|获取用于确定字典中的键是否相等的`IEqualityComparar<\T>`|
|Count|获取包含在字典中的键/值对的数目|
|Item[TKey]|获取或设置与指定的键关联的值|
|Keys|获得一个包含字典中的键的集合|
|Values|获得一个包含字典中的值的集合|

`Keys`和`Values`返回的值也是一个集合。
3. 常用的方法

|方法名称|说明|
|-------|----|
|Add(TKey,TValue)|将指定的键值对添加到字典中|
|Clear()|将所有的键值对从字典中移除|
|Remove(TKey)|移除指定键的值|

4.键的类型
用作字典中键的类型必须重写`Object`类的`GetHashCode()`方法。
`GetHashCode()`方法的实现必须满足如下条件：

+ 相同的对象应总是返回相同的值
+ 不同的对象可以返回相同的值
+ 不能抛出异常
+ 至少使用一个实例字段
+ 散列代码最好在对象的生存期中不发生变化

除此之外，最好还满足如下条件：

+ 应执行很快，计算的开销不大
+ 散列代码值应平均分布在int可以存储的整个数字范围上

### 有序列表和有序字典

`SortedList<TKey,TValue>`和`SortedDictionary<TKey,TValue>`的功能相同，都用来存储按键排序的键值对，且无重复。
而内部实现差异很大，`SortedDictionary<TKey,TValue>`的内部实现是红黑二叉搜索树，而`SortedList<TKey,TValue>`的内部是两个数组，分别存储键和值的序列。

```C#
//SortedDictionary类
public class SortedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, IReadOnlyDictionary<TKey, TValue> 
{
    private TreeSet<KeyValuePair<TKey, TValue>> _set;

    public SortedDictionary(IDictionary<TKey,TValue> dictionary, IComparer<TKey> comparer) 
    {
            if( dictionary == null) 
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
            }

            _set = new TreeSet<KeyValuePair<TKey, TValue>>(new KeyValuePairComparer(comparer));

            foreach(KeyValuePair<TKey, TValue> pair in dictionary) 
            {
                _set.Add(pair);
            }            
        }
}
//SortedList类
public class SortedList : IDictionary, ICloneable
{
    private Object[] keys;
    private Object[] values;
    private int _size;
    private IComparer comparer;
    private static Object[] emptyArray = EmptyArray<Object>.Value; 

    public SortedList() 
    {
        Init();
    }
    private void Init()
    {
        keys = emptyArray;
        values = emptyArray;
        _size = 0;
        comparer = new Comparer(CultureInfo.CurrentCulture);
    }
}
```

两者的增删查操作

```C#
//SortedDictionary类
public void Add(TKey key, TValue value) 
{
    if( key == null) 
    {
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
    }
    _set.Add(new KeyValuePair<TKey, TValue>(key, value));
}
public bool Remove(TKey key) 
{
    if( key == null) 
    {
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
    }

    return _set.Remove(new KeyValuePair<TKey, TValue>(key, default(TValue)));
}
public TValue this[TKey key] 
{
    get 
    {
        if ( key == null) 
        {
            ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);                    
        }

        TreeSet<KeyValuePair<TKey, TValue>>.Node node = _set.FindNode(new KeyValuePair<TKey, TValue>(key, default(TValue)));
        if ( node == null) 
        {
            ThrowHelper.ThrowKeyNotFoundException();                    
        }

        return node.Item.Value;
    }
    set 
    {
        if( key == null) 
        {
            ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
        }
    
        TreeSet<KeyValuePair<TKey, TValue>>.Node node = _set.FindNode(new KeyValuePair<TKey, TValue>(key, default(TValue)));
        if ( node == null) 
        {
            _set.Add(new KeyValuePair<TKey, TValue>(key, value));                        
        } 
        else 
        {
            node.Item = new KeyValuePair<TKey, TValue>( node.Item.Key, value);
            _set.UpdateVersion();
        }
    }
}
//SortedList类
//增，先二分查找到要插入的位置，然后插入，将目标位置之后的所有数据都向后移动
public virtual void Add(Object key, Object value) 
{
    if (key == null) throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
    Contract.EndContractBlock();
    int i = Array.BinarySearch(keys, 0, _size, key, comparer);
    if (i >= 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_AddingDuplicate__", GetKey(i), key));
    Insert(~i, key, value);
}
private void Insert(int index, Object key, Object value) 
{
    if (_size == keys.Length) EnsureCapacity(_size + 1);
    if (index < _size) 
    {
        Array.Copy(keys, index, keys, index + 1, _size - index);
        Array.Copy(values, index, values, index + 1, _size - index);
    }
    keys[index] = key;
    values[index] = value;
    _size++;
    version++;
}
//删，先通过键找到要删除的位置索引，二分查找，把索引之后所有数据前移，然后把最后一个位置赋值为default
public virtual void Remove(Object key) 
{
    int i = IndexOfKey(key);
    if (i >= 0) 
    RemoveAt(i);
}
public virtual int IndexOfKey(Object key)
{
    if (key == null)
        throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
    Contract.EndContractBlock();
    int ret = Array.BinarySearch(keys, 0, _size, key, comparer);
    return ret >= 0 ? ret : -1;
}
public virtual void RemoveAt(int index)
{
    if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
    Contract.EndContractBlock();
    _size--;
    if (index < _size)
    {
        Array.Copy(keys, index + 1, keys, index, _size - index);
        Array.Copy(values, index + 1, values, index, _size - index);
    }
    keys[_size] = null;
    values[_size] = null;
    version++;
}
//查
public virtual Object this[Object key]
{
    get
    {
        int i = IndexOfKey(key);
        if (i >= 0) return values[i];
        return null;
    }
    set
    {
        if (key == null) throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
        Contract.EndContractBlock();
        int i = Array.BinarySearch(keys, 0, _size, key, comparer);
        if (i >= 0)
        {
            values[i] = value;
            version++;
            return;
        }
        Insert(~i, key, value);
    }
}
```

## 性能

通常情况下，以大写的`O`记号表示操作时间：

1. O(1)表示无论集合中有多少数据项，这个操作时间都不变。例如，`ArrayList`类的`Add()`方法就是这样，无论列表中的元素有多少个，在列表末尾添加一个新元素的时间都不变。
2. O(n)表示对于集合执行一个操作需要的时间在最坏的情况下是N。
3. O(log n)表示操作需要的时间随集合中元素的增加而增加，但每个元素需要增加的时间不是线性的，二是呈对数曲线。
