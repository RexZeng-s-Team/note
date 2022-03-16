## 数据绑定

1. 数据绑定是一种关系，从源对象提取一些信息，并用这些信息设置目标对象的属性，是应用程序UI与业务逻辑之间建立连接的过程。

   1. 数据和数据之间交互的简单而一致的方法
   2. 元素能够以公共语言运行时CLR对象和XML的形式绑定到各种数据源中的数据
   3. 元素中数据的外部表现形式发生更改，则基础数据可以自动刚更新以反映更改
   4. 典型用法是将服务器或本地配置数据放置到窗体或其他UI控件中

2. 基础模型

   ![image-20210414134909679](C:\Users\hspcadmin\AppData\Roaming\Typora\typora-user-images\image-20210414134909679.png)

   ​	每个绑定有四个组件：绑定目标对象，目标属性(依赖项属性)，绑定源，绑定源值路径

3. 数据流方向

   ![image-20210414135119091](C:\Users\hspcadmin\AppData\Roaming\Typora\typora-user-images\image-20210414135119091.png)

   	1. OneWay：绑定导致对源属性的更改会自动更新目标属性，但是对目标属性的更改不会传播回源属性。
   	2. TwoWay：绑定导致对源属性的更改会自动更新目标属性，而对目标属性的更改会也自动更新源属性。
   	3. OneWayToSource：与OneWay方向相反。
   	4. OneTime：源属性初始化目标属性，不传播后续更改。

4. 触发源的更新情况

   ​		![image-20210414135616430](C:\Users\hspcadmin\AppData\Roaming\Typora\typora-user-images\image-20210414135616430.png)

   | 名称            | 说明                                                         |
   | --------------- | ------------------------------------------------------------ |
   | PropertyChanged | 当目标属性发生变化时立即更新源                               |
   | LostFocus       | 当目标属性发生变化并且目标丢失焦点时更新源                   |
   | Explicit        | 除非调用BindingExpression.UpdateSource()方法，否则无法更新源 |

5. 创建绑定

   ​	绑定源

   1. 在元素上直接设置DataContext属性，从上级继承DataContext值
   2. 通过设置Binding上的Source属性来显示指定绑定源
   3. 使用ElementName属性或者RelativeSource属性指定绑定源

## 依赖项属性

1. 定义依赖项属性

   1. 定义表示属性的对象，DependencyProperty类的实例

      ```c#
      public static readonly DependencyProperty MarginProperty;
      ```

      根据约定，依赖项属性的字段的名称是在普通属性的末尾处加上单词Property

   2. 注册依赖项属性

      在任何使用属性的代码之前完成注册-在相关联的类的静态构造函数中进行

      ```c#
      static FrameworkElement()
      {
          FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata(new Thickness(),FrameworkPropertyMetadataOptions.AffectMeasure);
          MarginProperty = DependencyProperty.Register("Margin",typeof(Thickness),typeof(FrameworkElement),metadata,new ValidateValueCallback(FrameworkElement.IsMarginValid));
      }
      ```

      1. 属性名称
      2. 属性使用的数据类型
      3. 拥有该属性的类型
      4. 一个具有附加属性设置的FrameworkPropertyMetadata对象，该要素是可选的
      5. 一个用于验证属性的回调函数，可选

   3. 添加属性包装器

      相对与传统的.NET属性(典型的属性过程是检索或设置某个私有字段的值)，WPF依赖项属性的属性过程是使用在DependencyObject基类中定义的GetValue和SetValue方法。

      ```c#
      public Thickness Margin
      {
          set{SetValue(MarginProperty,value);}
          get{return (Thickness)GetValue(MarginProperty);}
      }
      ```

      创建属性封装器时，应当只包含SetValue和GetValue方法的调用。

2. 使用依赖项属性

   WPF的许多功能使用依赖项属性都是通过每个属性都支持的两个关键行为——更改通知和动态值识别

   1. 更改通知(值发生变化的时候执行的具体动作)

      依赖项属性的值发生变化的时候，会触发受保护的名为OnPropertyChangeCallback()的方法。

      *************************依赖项属性不会自动引发事件以通知属性值发生了变化，一可以使用属性值创建绑定，二可以编写能够自动改变其他属性或开始动画的触发器

   2. 动态识别值

      依赖项属性依赖于多个属性提供者，每个提供者都有各自的优先级。

      优先级从低到高的顺序：

      ​	1). 默认值(由FrameworkPropertyMetadata对象设置的值)

      ​	2). 继承而来的值

      ​	3). 来自主题样式的值

      ​	4). 来自项目样式的值

      ​	5). 本地值(使用代码或者XAML直接为对象设置的值)

      WPF决定属性值的四个步骤：

      ​	1). 确定基本值

      ​	2). 如果属性是使用表达式设置的，就对表达式进行求值(目前支持数据绑定和资源)

      ​	3). 如果属性是动画的目标，就应用动画

      ​	4). 运行CoreceValueCallback回调函数来修正属性值

   3. 具体代码

      ```c#
      //申明依赖项属性
      public static readonly DependencyProperty TestProperty;
      //注册依赖项属性
      FrameworkPropertyMetadata meta = new FrameworkPropertyMetadata();
      meta.DefaultValue = 100;
      meta.CoerceValueCallback = new CoerceCallback(TestPropertyCoerceValueCallback);
      meta.PropertyChangedCallback = new PropertyChangedCallback(TestPropertyPropertyChangedCallback);
      
      TestProperty = DependencyProperty.Register("Test",typeof(int),typeof(CurrentClass),meta,new ValidateValueCallback(TestPropertyValidateValueCallback));
      
      //回调函数,属性注册时执行，属性数据改变时执行
      //验证依赖项属性的值的类型和格式是否有效，有效返回true，无效返回false
      private static bool TestPropertyValidateValueCallback(object value)
      {
          //具体执行的操作
          return true;
      }
      
      //在ValidateValueCallback之后执行，可以对属性值做详细验证
      private static object TestPropertyCoerceValueCallback(DenpendncyObject sender,object value)
      {
          //这个回调函数必须返回object
          return value;
      }
      
      //属性值发生变化时执行
      private static void TestPropertyChangedCallback(DependencyObject d,DependencyPropertyChangedEventArgs e)
      {
          //最后执行
      }
      
      //包装依赖项属性
      public int Test
      {
          get { return (int)GetValue(TestProperty);}
          set { SetValue(TestProperty,value);}
      }
      ```

   4. 附加的依赖项属性

      附加属性是一类特殊的依赖项属性，由WPF属性系统管理，不同之处在于附加属性被应用到的类并非定义附加属性的类。

      ```C#
      //Grid类定义了Row和Column附加属性，被用于设置面板包含的元素应被放到哪个单元格
      class Grid
      {
          FrameworkPropertyMetadata meta = new FrameworkPropertyMetadata();
          public static DependencyProperty Row;
          public static DependencyProperty Column;
          Row = DependencyProperty.RegisterAttached("Row",typeof(int),typeof(Gird),meta,new ValidateValueCallback(Grid.IsIntValueNotNegative));
          //Column属性的注册同Row
      }
      //DockPanel类定义了Dock附加属性
      class DockPanel
      {
          public static DependencyProperty Dock;
          //具体的注册省略，使用DependencyProperty.RegisterAttached
      }
      //Canvas类定义了Left、Right、Top和Button附加属性
      class Canvas
      {
          public static DependencyProperty Left;
          public static DependencyProperty Right;
          public static DependencyProperty Top;
          public static DependencyProperty Button;
          //具体的注册省略，使用DependencyProperty.RegisterAttached
      }
      ```

      附加属性和依赖项属性的区别：

      1. 附加属性和依赖项属性的注册方法不同
      2. 包装方式不同。(依赖项属性的包装同普通的属性，通过属性自身获取或者设置属性值，只有定义和注册的类可以使用该属性；附加属性的包装方式不同于普通属性，要重新写两个get和set的方法，以达到获取和设置属性值的作用，这两个方法是公有静态的，所以不止当前的类可以用)

   5. 属性验证

      WPF提供了两种方法验证依赖项属性的值是否非法：ValidateValueCallback和CoerceValueCallback

      1. 首先，CoerceValueCallback方法有机会修改提供的值
      2. 接下来激活ValidateValueCallback方法，由于不能设置属性的具体对象，不能检查其他属性值
      3. 最后，前两个阶段如果都成功，就会触发PropertyChangedCallback方法，引发更改事件。

## 命令

   在应用程序中，使用路由事件可以响应广泛的鼠标和键盘动作，但是，事件是非常低级的元素，在实际应用中，功能被划分为一些高级的任务，可以通过各种不同的动作和用户界面元素触发，包括菜单，上下文菜单，键盘快捷键以及工具栏。这些任务就是所谓的命令，将控件连接到命令，从而不需要重复编写事件处理代码。

   ![image-20210414142043455](C:\Users\hspcadmin\AppData\Roaming\Typora\typora-user-images\image-20210414142043455.png)

   1. 命令模型

      1. 命令：表示应用程序任务，并且跟踪任务是否能够被执行，然而命令实际上不包含执行应用程序任务的代码。
      2. 命令绑定：每个命令绑定针对用户界面的具体区域，将命令连接到相关的应用程序逻辑。
      3. 命令源：命令源触发命令。
      4. 命令目标：在其中执行命令的元素。

      ```C#
      //WPF命令模型的核心System.Windows.Input.ICommand
      public interface ICommand
      {
          //应用程序任务逻辑
          void Execute(object parameter);
          //返回命令的状态，可用返回true，不可用返回false
          bool CanExecute(object parameter);
          //命令状态改变时引发这个事件
          event EventHandler CanExecuteChanged;
      }
      
      //RoutedCommand
      //唯一一个实现了ICommand接口的类，该类中不包含任何应用程序逻辑，只代表命令。
      //RoutedUICommand类继承自RoutedCommand类，用于具有文本的命令，文本显示在用户界面中的某些地方
      ```


      命令库
    
      每个应用程序都有大量的命令，但是很多命令是通用的，WPF提供了基本命令库
    
      | 名称                | 说明                                       |
      | ------------------- | ------------------------------------------ |
      | ApplicationCommands | 通用命令，copy,cut,paste...                |
      | NavigationCommands  | 导航命令                                   |
      | EditingCommands     | 文档编辑命令                               |
      | ComponentCommands   | 由用户界面使用的命令，移动和选择内容的命令 |
      | MediaCommands       | 处理多媒体的命令                           |
    
      开始使用一个命令，必须做三件事：
    
      	1. 定义一个命令
      	2. 定义命令的实现
      	3. 为命令创建一个触发器
    
      自定义命令的步骤：
    
      	1. 创建命令类：如果没有涉及到业务逻辑，一般使用WPF中RouteCommand类，如果要声明一些复杂的类，可以实现RouteCommand类的继承或者ICommand类。
    
        2. 声明命令实例：一般情况下，命令是普遍使用的，在程序中只需要有一个实例即可，单例模式
        3. 指明命令的源：通常是可以点击的控件
        4. 指明命令的目标：命令的作用对象
        5. 设置命令关联：

   2. 执行命令

      1. 命令源

         为了触发命令，需要由命令源，最简单的方法就是将命令关联到实现了ICommandSource接口的控件。

         ICommandSource接口定义了三个属性，Command(指向连接的命令，唯一必须)；CommandParameter(提供其他希望随命令发送的数据)；CommadnTarget(确定将在其中执行命令的元素)

   ```xaml
          <button command = "ApplicationCommands.New">New</button>
   ```

      2. 命令绑定
    
         当将命令关联到命令源时，命令源会自动禁用
    
         ```C#
         //create the binding
         CommandBinding commandBinding = new CommandBinding(ApplicationCommands.New);
         //attach the event handler
         //因为ApplicationCommands.New只是个命令，没有实现命令的具体操作
         commandBinding.Executed += NewCommand_Executed;
         //register the binding
         this.CommandBindings.Add(commandBinding);
         private void NewCommand_Executed(object sender, ExecuteRoutedEventArgs args)
         {
             MessageBox.Show("new command triggered by " + args.Source.Tostring());
         }
         ```
​         

   消息和事件

​	消息Message就是用于描述某个事件所发生的信息，事件Event则是用户操作应用程序产生的动作或Windows系统自身所产生的动作。事件是原因，消息是结果，事件产生消息，消息对应事件。

1. 消息基础概念

   消息源：消息的来源，发出这个消息的对象

   消息名：消息的唯一标示

   消息数据：消息发出后附带的数据，有可能是空数据

   消息的分类：

   ​	系统消息：操作系统发送出来的消息，消息名称是固定的

   ​	自定义消息：有开发者自己定义，消息名称可以任意定义

## 路由事件

相对于传统的简单的事件模型，事件的响应者使用事件处理器做出响应，如果事件的宿主不能直接访问事件的响应者，用户控件的内部事件不能被外部访问，简单事件模型就出现了问题。路由事件的事件拥有者和事件的响应者之间没有直接的显示订阅关系，时间的额拥有者只负责激发事件，事件的响应者则使用事件监听器对事件进行监听。

路由事件出现的三种方式：

	1. 直接路由事件：源于一个元素，不传递给其他元素。
	2. 冒泡路由事件：在包含层次中向上传递
	3. 隧道路由事件：在包含层次中向下传递

```C#
//定义、注册和封装路由事件
public abstract class ButtonBase : ContenControl,...
{
    //The event definition
    public static readonly RoutedEvent ClickEvent;
    //The event registration
    /*
    	第一个参数为路由事件的名称
    	第二个参数是路由事件的策略，事件的传递方式
    	第三个参数用于指定事件处理器的类型
    	第四个参数用于指定事件的宿主是哪种类型
    */
    static ButtonBase()
    {
        ButtonBase.ClickEvent = EventManager.RegisterRoutedEvent(
        "CLick",RoutingStrategy.Bubble,typeof(RoutedEventHandler),typeof(ButtonBase));
        ...
    }
    //The traditional event wrapper
    public event RoutedEventHandler Click
    {
        add
        {
            base.AddHandler(ButtonBase.ClickEvent,value);
        }
        remove
        {
            base.RemoveHandler(ButtonBase.ClickEvent,value);
        }
    }
}

//共享路由事件
UIEvent.MouseUpEvent = Mouse.MouseUpEvent.AddOwner(typeof(UIEvent));

//处理，引发路由事件
//XAML
<Image Source="happyface.jpg" Stretch="None" Name="img" MouseUp="img_MouseUp"/>
//cs
img.MouseUp += new MouseButtonEventHandler(img_MouseUp);//img.MouseUp+=img_MouseUp;
//事件封装器cs
img.AddHandler(Image.MouseUpEvent, new MouseButtonEventHandler(img_MouseUp));

//断开事件连接
img.MouseUp -= img_MouseUp;
img.RemoveHandler(Image.MouseUpEvent, new MouseButtonEventHandler(img_MouseUp));
```

## 事件路由

事件的路由指引发事件后事件的传递，同传统的.NET事件相比，除了直接路由外，由于元素的层次嵌套，引发事件的元素可以在层次中向上或者向下传递事件的引发信号，从而引发嵌套元素中响应的事件或者执行同样的事件处理器。

*如果事件在执行到某个节点的时候不希望继续传递下去，设置Handed属性即可

## WPF事件

	1. 生命周期事件：元素被初始化、加载或卸载时发生这些事件
	2. 鼠标事件：鼠标动作的结果
	3. 键盘事件：键盘动作的结果
	4. 手写笔事件：手写笔动作的结果
	5. 多点触控事件：win7中，一根或者多根手指在多点触控屏幕上触摸的结果

## BeginInvoke模型

1. BeginInvoke()函数定义

   ```c#
   public DispatcherOperation BeginInvoke(DispatcherPriority priority, Delegate method, Object arg, params Object[] args)
   {
       return this.BeginInvokeImpl(priority, method, this.CombineParameters(arg, args), false);
   }
   ```

   这个函数用来向WPF系统中插入一个由method参数所传入的工作项。这些插入到系统中的工作项将根据priority参数所指定的优先级进行排列。第三个和第四个参数将被作为参数传递进method所表示的工作项中。

   这个函数的返回值是DispatcherOperation类型的一个实例，通过该实例，开发人员可以和系统中的工作项进行交互。DispatcherOperation实例的priority属性用来操作插入工作项的优先级，一个工作项的优先级越高，就会被越早执行。同时，在代码中，开发人员可以通过DispatcherOperation类的status属性来访问当前工作项的状态，在工作项执行完毕之后，可以通过Result属性得到工作项的运行结果。

   BeginInvoke()函数的内部实现中可以看出，在每次调用BeginInvoke()函数的时候，其内部都会调用BeginInvokeImp()函数。在调用BeginInvokeImpl()函数之前，WPF内部将通过CombineParameters()函数将传入的参数arg与args合并。因此在调用BeginInvoke()函数的时候，您并不需要担心参数arg与args之间的区别。

   ```c#
   internal DispatcherOperation BeginInvokeImpl(DispatcherPriority priority, Delegate method, object args, bool isSingleParameter)
   {
       ……
       DispatcherOperation data = new DispatcherOperation(this, method, priority, args, isSingleParameter) 
       {
           _item = this._queue.Enqueue(priority, data)
       };
       this.RequestProcessing();
       ……
       return data;
   }
   private bool RequestProcessing()
   {
        DispatcherPriority maxPriority = this._queue.MaxPriority;
        switch (maxPriority)
        {
            case DispatcherPriority.Invalid:
            case DispatcherPriority.Inactive:
                return true;
        }
        if (_foregroundPriorityRange.Contains(maxPriority))
        {
            return this.RequestForegroundProcessing();
        }
        return this.RequestBackgroundProcessing();
   }
   ```

   DispatcherPriority枚举

   | Name            | Value | Description                                            |
   | --------------- | ----- | ------------------------------------------------------ |
   | ApplicationIdle | 2     | 在应用程序空闲时处理操作                               |
   | Background      | 4     | 在完成所有其他非空闲操作后处理操作                     |
   | ContextIdle     | 3     | 在完成后台操作处理操作                                 |
   | DataBind        | 8     | 按与数据绑定相同的优先级处理操作                       |
   | Inactive        | 0     | 操作未处理                                             |
   | Input           | 5     | 按与输入相同的优先级处理                               |
   | Invalid         | -1    | 无效的优先级                                           |
   | Loaded          | 6     | 在布局和呈现已完成，即将按输入优先级处理项之前处理操作 |
   | Normal          | 9     | 按正常优先级处理操作，典型的应用程序优先级             |
   | Render          | 7     | 与呈现相同的优先级处理操作                             |
   | send            | 10    | 在其他异步操作之前处理操作，最高优先级                 |
   | systemIdle      | 1     | 在系统空闲时处理操作                                   |

   在BeginInvokeImpl()函数中，系统将首先创建一个DispatcherOperation类的实例，用来记录当前所有请求执行的任务的信息，将该实例记录在成员`_queue`中，`_queue`会根据传入的优先级`priority`在内部对各个任务进行排列。然后，WPF就调用了`RequestBackgroundProcessing()`函数请求异步执行该任务。

2. RequestBackgroundProcessing()方法定义

   ```C#
   private bool RequestProcessing()
   {
       DispatcherPriority maxPriority = this._queue.MaxPriority;
       switch (maxPriority)
       {
           case DispatcherPriority.Invalid:
           case DispatcherPriority.Inactive:
               return true;
       }
       if (_foregroundPriorityRange.Contains(maxPriority))
       {
           return this.RequestForegroundProcessing();
       }
       return this.RequestBackgroundProcessing();
   }
   ```

   这个方法中，如果优先级是`Invalid`和`Inactive`的时候，直接返回true，不执行具体的操作。接下来，WPF会根据优先级是否存在于`_foregroundPriorityRange`来决定需要调用的是`RequestForegroundProcessing()`还是`RequestBackgroundProcessing()`。但是不管怎样，WPF都将最终调用`RequestForegroundProcessing()`函数。

3. RequestForegroundProcessing()方法

   ```C#
   private bool RequestForegroundProcessing()
   {
       ……
       return MS.Win32.UnsafeNativeMethods.TryPostMessage(new HandleRef(this, this._window.Value.Handle), _msgProcessQueue, IntPtr.Zero, IntPtr.Zero);
   }
   ```

   `BeginInvoke()`函数的执行逻辑实际上非常简单：在调用`BeginInvoke()`函数时，WPF将在成员`_queue`中记录该异步调用的相关信息。接下来，其将向Windows系统发送一个_`msgProcessQueue`消息。那我们可以大胆猜想：对BeginInvoke()函数所传入的回调函数的调用就是在对`_msgProcessQueue`消息的处理中完成的。

4. _msgProcessQueue消息

   ```C#
   private IntPtr WndProcHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
   {
       ……
       else if (msg == _msgProcessQueue)
       {
           this.ProcessQueue();
       }
       ……
   }
   ```

   当`_msgProcessQueue`消息到达时，`ProcessQueue()`函数将被执行。这里的`WndProcHook`就是WPF向当前窗口中添加的消息处理函数钩子。而`ProcessQueue()`函数则调用了`BeginInvoke()`函数所插入到成员`_queue`中的回调函数。

5. ProcessQueue()方法

   ```C#
   private void ProcessQueue()
   {
       ……
       operation = this._queue.Dequeue();
       ……
       if (operation != null)
       {
       ……
           operation.Invoke();
       ……
       }
   }
   ```

6. Incoke和BeginInvoke的区别

   `Invoke`或者`BeginInvoke`方法都需要一个委托对象作为参数。委托类似于回调函数的地址，因此调用者通过这两个方法就可以把需要调用的函数地址封送给界面线程。这些方法里面如果包含了更改控件状态的代码，那么由于最终执行这个方法的是界面线程，从而避免了竞争条件，避免了不可预料的问题。如果其它线程直接操作界面线程所属的控件，那么将会产生竞争条件，造成不可预料的结果。

   使用`Invoke`完成一个委托方法的封送，就类似于使用`SendMessage`方法来给界面线程发送消息，是一个同步方法。也就是说在`Invoke`封送的方法被执行完毕前，`Invoke方`法不会返回，从而调用者线程将被阻塞。

   使用`BeginInvoke`方法封送一个委托方法，类似于使用`PostMessage`进行通信，这是一个异步方法。也就是该方法封送完毕后马上返回，不会等待委托方法的执行结束，调用者线程将不会被阻塞。但是调用者也可以使用`EndInvoke`方法或者其它类似`WaitHandle`机制等待异步操作的完成。

   但是在内部实现上，`Invoke`和`BeginInvoke`都是用了`PostMessage`方法，从而避免了`SendMessage`带来的问题。而`Invoke`方法的同步阻塞是靠`WaitHandle`机制来完成的。

   如果你的后台线程在更新一个UI控件的状态后不需要等待，而是要继续往下处理，那么你就应该使用`BeginInvoke`来进行异步处理。

   如果你的后台线程需要操作UI控件，并且需要等到该操作执行完毕才能继续执行，那么你就应该使用`Invoke`。否则，在后台线程和主截面线程共享某些状态数据的情况下，如果不同步调用，而是各自继续执行的话，可能会造成执行序列上的问题，虽然不发生死锁，但是会出现不可预料的显示结果或者数据处理错误。

   可以看到`ISynchronizeInvoke`有一个属性，`InvokeRequired`。这个属性就是用来在编程的时候确定，一个对象访问UI控件的时候是否需要使用`Invoke`或者`BeginInvoke`来进行封送。如果不需要那么就可以直接更新。在调用者对象和UI对象同属一个线程的时候这个属性返回`false`。在后面的代码分析中我们可以看到，`Control`类对这一属性的实现就是在判断调用者和控件是否属于同一个线程的。

7. 

## 多线程

1. 使用线程的理由

   + 可以使用线程将代码同其他代码隔离，提高应用程序的可靠性
   + 可以使用线程来简化编码
   + 可以使用线程来实现并发执行

2. 基本知识

   + 进程和线程：进程是操作系统执行程序的基本单位，拥有应用程序的资源，进程的资源被线程共享，线程不拥有资源

   + 前台线程和后台线程：通过Thread类新建的线程默认为前台线程，当所有前台线程关闭时，所有的后台线程也会被直接终止，不会抛出异常

   + 挂起和唤醒：由于线程的执行顺序和程序的执行情况不可预知，挂起和.唤醒可能会发生死锁，尽量少用

   + 阻塞线程：Join，阻塞调用线程，直到该线程结束

   + 终止线程：Abort，终止后的线程不可唤醒；Interrupt，通过捕获异常可以继续执行

   + 线程优先级：默认为Normal

     ```C#
     //框架源码
     namespace System.Threading
     {
         [Serializable]
         [System.Runtime.InteropServices.ComVisible(true)]
         public enum ThreadPriority
         {
             /*===========================================
             ** Constants for thread priorities.
             ============================================*/
             Lowest = 0,
             BelowNormal = 1,
             Normal = 2,
             AboveNormal = 3,
             Highest = 4
         }
     }
     ```

3. 线程的使用

   + 最简单的多线程调用-Thread

   ```C#
   public sealed class Thread : System.Runtime.ConstrainedExecution.CriticalFinalizerObject
   ```

   被sealed修饰的类，不可以产生子类，即不可被继承。

   构造函数

   ```C#
   namespace SYstem.Threading
   {
       public sealed class Thread : System.Runtime.ConstrainedExecution.CriticalFinalizerObject
       {
           /*=========================================================================
           ** Creates a new Thread object which will begin execution at
           ** start.ThreadStart on a new thread when the Start method is called.
           **
           ** Exceptions: ArgumentNullException if start == null.
           =========================================================================*/
           [System.Security.SecuritySafeCritical]  // auto-generated
           public Thread(ThreadStart start) {
               if (start == null) {
                   throw new ArgumentNullException("start");
               }
               Contract.EndContractBlock();
               SetStartHelper((Delegate)start,0);  //0 will setup Thread with default stackSize
           }
    
           [System.Security.SecuritySafeCritical]  // auto-generated
           public Thread(ThreadStart start, int maxStackSize) {
               if (start == null) {
                   throw new ArgumentNullException("start");
               }
               if (0 > maxStackSize)
                   throw new ArgumentOutOfRangeException("maxStackSize",Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
               Contract.EndContractBlock();
               SetStartHelper((Delegate)start, maxStackSize);
           }
           [System.Security.SecuritySafeCritical]  // auto-generated
           public Thread(ParameterizedThreadStart start) {
               if (start == null) {
                   throw new ArgumentNullException("start");
               }
               Contract.EndContractBlock();
               SetStartHelper((Delegate)start, 0);
           }
    
           [System.Security.SecuritySafeCritical]  // auto-generated
           public Thread(ParameterizedThreadStart start, int maxStackSize) {
               if (start == null) {
                   throw new ArgumentNullException("start");
               }
               if (0 > maxStackSize)
                   throw new ArgumentOutOfRangeException("maxStackSize",Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
               Contract.EndContractBlock();
               SetStartHelper((Delegate)start, maxStackSize);
           }
       }
   }
   ```

   ```C#
   namespace System.Threading
   {
       public delegate void ThreadStart();
       public delegate void ParameterizedThreadStart(Object obj);
   }
   ```

   `Thread`类有4个重载的构造函数，`ThreadStart`和`ParameterizedThreadStart`是两个不含返回值的委托，`maxStackSize`指定该线程最大的访问栈空间。

   ```C#
   public class Program
   {
       public void Main(string[] args)
       {
           //创建没有参数的线程
           Thread thread1 = new Thread(()=>
           {
   			//do...
           });
           //创建含有参数的线程
           Thread thread2 = new Thread((Object o)=>
           {
                //do...                           
           });
           //启动线程
           thread1.Start();
           thread2.Start(Object o);
       }
   }
   ```

   + 线程池ThreadPool

   由于线程的创建和销毁需要耗费一定的开销，过多的使用线程会造成内存资源的浪费，处于对性能的考虑，于是引入了线程池的概念。线程池维护一个请求队列，线程池的代码从队列中提取任务，然后委派给线程池的一个线程执行，线程执行完毕不会被立即销毁，这样既可以在后台执行任务，又可以减少线程创建和销毁所带来的开销。

   ```C#
   public static class ThreadPool
   {
       public static bool QueueUserWorkItem (System.Threading.WaitCallback callBack);
       public static bool QueueUserWorkItem (System.Threading.WaitCallback callBack, object state);
   }
   ```

   ```C#
   public class Program
   {
       public void Main(string[] args)
       {
           ThreadPool.QueueUserWorkItem(()=>
           {
   			//do...
           });
           ThreadPool.QueueUserWorkItem((Obeject o)=>
           {
   			//do...
           });
       }
   }
   ```

   + Task

   ```C#
   public class Task : IAsyncResult, IDisposable
   ```

   ```C#
   public class Program
   {
       public void Main(string[] args)
       {
           //new方式实例化一个task，需要通过start方法启动
           Task task1 = new Task(() =>
           {
                //do...
           });
           task1.Start();
           Task task2 = new Task((t) =>
           {
                //do...
           },"hello");
           task2.Start();
           //通过Task.Factory.StartNew实例化
           Task task3 = Task.Factory.StartNew(() =>
   		{
   			//do...
   		});
           //Task.Run将任务放在线程池队列，返回并启动一个Task
   		Task task4 = Task.Run(() =>
   		{
   			//do...
   		});
       }
   }
   ```

   常用的方法

   | 方法名称       | 说明                                                       |
   | -------------- | ---------------------------------------------------------- |
   | ConfigureAwait | 配置一个bool值，指定是否需要await                          |
   | ContinueWith   | 两个Task之间的顺序关系，指定一个Task的执行顺序在另一个之后 |
   | Delay          | 延迟完成                                                   |
   | Dispose        | 释放当前Task所有资源                                       |
   | FromCanceled   | 创建一个Task，这个任务是因为特定的原因取消                 |
   | FromException  | 创建一个Task，这个任务是因为特定的原因执行                 |
   | FromResult     | 创建一个Task，这个任务有特定的返回结果                     |
   | Run            | 创建并开启一个Task                                         |
   | Start          | 开启一个Task                                               |
   | Wait           | 等待当前的Task结束，阻塞线程                               |
   | WaitAll        | 等待所有的Task结束，阻塞线程，适用于Task[]                 |
   | WaitAny        | 等待任一的Task结束，阻塞线程，适用于Task[]                 |
   | WhenAll        | 创建一个Task在所有的Task结束之后，阻塞线程，适用于Task[]   |
   | WhenAny        | 创建一个Task在任一的Task结束之后，阻塞线程，适用于Task[]   |

   + 委托异步执行

   委托的异步执行主要有两个方法，`BeginInvoke`和`Invoke`。

   ```C#
   public class Program
   {
       private delegate Test();
       public void Main(string[] args)
       {
           Test test = (()=>{});
           test.BeginInvoke();//不阻塞线程
           test.EndInvoke();//异步执行结束
           test.Invoke();//阻塞线程
       }
   }
   ```

4. 线程同步

   + **原子操作`Interlocked`**：所有的方法都是执行一次原子读取或一次写入操作。每个操作都是不可拆分的指令，在32位系统中，int型的数据属于原子的，访问这个数据只需要一条指令操作即可，long型数据是64位，访问这个数据需要2条独立的指令，如果两个线程同时访问这个数据，很有可能得到两个指令叠加的数据。

   + **加锁y`Lock`**：创建一个对象，最好是私有的，在锁开始的时候会对这个对象中的某一个属性进行置位，表示为当前状态为锁状态，其他线程在访问的时候检查到为锁状态时，会在一个队列中排队等待。在当前锁执行完成后，这个属性置位为解锁状态，线程队列会自动分配一个线程继续访问。

   + **添加监视器`Monitor`**：通过`Monitor.Enter()` 和 `Monitor.Exit()`实现排它锁的获取和释放，获取之后独占资源，不允许其他线程访问。还有一个`TryEnter`方法，请求不到资源时不会阻塞等待，可以设置超时时间，获取不到直接返回false。

   + **读写锁`ReadWriterLock`**：一般情况，数据的读操作要多于写操作，让读操作锁为共享锁，多个线程可以并发读取资源，而写操作为独占锁，只允许一个线程操作。

   + **事件`Event`类实现同步**：`EventWaitHandle`类继承自`WaitHandle`，且派生出两个子类`AutoResetEvent`和`ManualResetEvent`，前者自动重置事件，后者手动重置事件。线程中调用`WaitOne`方法，可以使线程暂停，等待状态置位后继续，通过调用`Set`方法置位。

     ```C#
     AutoResetEvent autoResetEvent = new AutoResetEvent(false);
     		ManualResetEvent manual = new ManualResetEvent(false);
     		private void button1_Click(object sender, EventArgs e)
     		{
     			Console.WriteLine($"*************Start**************{System.Threading.Thread.CurrentThread.ManagedThreadId}");
     
     			Task.Run(() =>
     			{
     				Console.WriteLine($"*******start1*******{System.Threading.Thread.CurrentThread.ManagedThreadId}");
     				autoResetEvent.WaitOne();
     				Console.WriteLine($"*******end1*******{System.Threading.Thread.CurrentThread.ManagedThreadId}");
     			});
     			Task.Run(() =>
     			{
     				Console.WriteLine($"*******start2*******{System.Threading.Thread.CurrentThread.ManagedThreadId}");
     				manual.WaitOne();
     				Console.WriteLine($"*******end2*******{System.Threading.Thread.CurrentThread.ManagedThreadId}");
     			});
     			Console.WriteLine($"*************End**************{System.Threading.Thread.CurrentThread.ManagedThreadId}");
     		}
     
     		private void button2_Click(object sender, EventArgs e)
     		{
     			autoResetEvent.Set();
     			manual.Set();
     		}
     ```

   + **信号量Semaphore**：如同计算机基础中的信号量的概念，如果信号量是0，表示当前资源不足，需要等待，线程无法进行访问。当占用资源的线程结束后，释放资源，信号量增加1，此时信号量大于0，表明有资源，线程可以进行访问，当线程开始进行访问资源的时候，信号量减少1.

   + **互斥量`Mutex`**：排他型封锁，独占资源，与信号量的用法差不多

     ```mermaid
     classDiagram
     	WaitHandle <|-- Mutex
     	WaitHandle <|-- Semaphore
     	WaitHandle <|-- EventWaitHandle
     	EventWaitHandle <|-- AutoResetEvent
     	EventWaitHandle <|-- ManualResetEvent
     ```

     

## 委托

1. 理解委托

   Delegate类似于C++中的指针，是存有某个方法的引用的一种引用类型变量，可以在运行时被改变，特别用于实现事件和回调方法。**将方法作为方法的参数**

2. 实例

   ```C#
   public void GreetPeople(string name)
   {
       EnglishGreeting(name);
   }
   public void EnglishGreeting(stirng name)
   {
    	Console.WriteLine("Good Morning, " + name);   
   }
   ```

   上面的例子中是一个实现打招呼的方法，现在假设这个程序需要进行全球化，不是所有的人都能认识英文，需要对程序进行扩展，能实现多国语言的打招呼。

   ```C#
   public enum Language
   {
       English,Chinese
   }
   public void GreetPeople(string name,Language lang)
   {
       switch(lang)
       {
           case Language.English:
               EnglishGreeting(name);
               breake;
           case Language.Chinese:
               ChineseGreeting(name);
               breake;
       }
   }
   public void EnglishGreeting(stirng name)
   {
    	Console.WriteLine("Good Morning, " + name);   
   }
   public void ChineseGreeting(string name)
   {
       Console.WriteLine("早上好, " + name); 
   }
   ```

   这样，确实解决了问题，也很容易想到，但是解决方案的可扩展性很差，每次增加一个语言，就不得不反复修改枚举和GreetPeople()方法。

   在考虑新的解决方案之前，我们先看看 GreetPeople 的方法签名：

   ```c#
      public void GreetPeople(string name, Language lang);
   ```

   我们仅看 string name，在这里，string 是参数类型，name 是参数变量，当我们赋给 name 字符串“Liker”时，它就代表“Liker”这个值；当我们赋给它“李志中”时，它又代表着“李志中”这个值。然后，我们可以在方法体内对这个 name 进行其他操作。哎，这简直是废话么，刚学程序就知道了。

   如果你再仔细想想，假如 GreetPeople() 方法可以接受一个参数变量，这个变量可以代表另一个方法，当我们给这个变量赋值 EnglishGreeting 的时候，它代表着 EnglsihGreeting() 这个方法；当我们给它赋值ChineseGreeting 的时候，它又代表着 ChineseGreeting() 法。我们将这个参数变量命名为 MakeGreeting，那么不是可以如同给 name 赋值时一样，在调用 GreetPeople()方法的时候，给这个MakeGreeting 参数也赋上值么(ChineseGreeting 或者EnglsihGreeting 等)？然后，我们在方法体内，也可以像使用别的参数一样使用MakeGreeting。但是，由于 MakeGreeting 代表着一个方法，它的使用方式应该和它被赋的方法(比如ChineseGreeting)是一样的，比如：MakeGreeting(name);

   好了，有了思路了，我们现在就来改改GreetPeople()方法，那么它应该是这个样子了：

   ```C#
   public void GreetPeople(string name, *** MakeGreeting)
   {
          MakeGreeting(name);
   }
   ```

   注意到 *** ，这个位置通常放置的应该是参数的类型，但到目前为止，我们仅仅是想到应该有个可以代表方法的参数，并按这个思路去改写 GreetPeople 方法，现在就出现了一个大问题：这个代表着方法的 MakeGreeting 参数应该是什么类型的？

   说明：这里已不再需要枚举了，因为在给MakeGreeting 赋值的时候动态地决定使用哪个方法，是 ChineseGreeting 还是 EnglishGreeting，而在这个两个方法内部，已经对使用“Good Morning”还是“早上好”作了区分。

   聪明的你应该已经想到了，现在是委托该出场的时候了，但讲述委托之前，我们再看看MakeGreeting 参数所能代表的 ChineseGreeting()和EnglishGreeting()方法的签名：

   **public void EnglishGreeting(string name)**

   **public void ChineseGreeting(string name)**

   如同 name 可以接受 String 类型的“true”和“1”，但不能接受bool 类型的true 和int 类型的1 一样。MakeGreeting 的参数类型定义应该能够确定 MakeGreeting 可以代表的方法种类，再进一步讲，就是 MakeGreeting 可以代表的方法的参数类型和返回类型。

   于是，委托出现了：它定义了 MakeGreeting 参数所能代表的方法的种类，也就是 MakeGreeting 参数的类型。

   本例中委托的定义：

       public delegate void GreetingDelegate(string name);

   与上面 EnglishGreeting() 方法的签名对比一下，除了加入了delegate 关键字以外，其余的是不是完全一样？现在，让我们再次改动GreetPeople()方法，如下所示：

   ```C#
   public delegate void GreetingDelegate(string name);
   public void GreetPeople(string name, GreetingDelegate MakeGreeting)
   {
       MakeGreeting(name);
   }
   ```

   如你所见，委托 GreetingDelegate 出现的位置与 string 相同，string 是一个类型，那么 GreetingDelegate 应该也是一个类型，或者叫类(Class)。但是委托的声明方式和类却完全不同，这是怎么一回事？实际上，委托在编译的时候确实会编译成类。因为 Delegate 是一个类，所以在任何可以声明类的地方都可以声明委托。更多的内容将在下面讲述，现在，请看看这个范例的完整代码：

   ```C#
   public delegate void GreetingDelegate(string name);
   class Program
   {
       private static void EnglishGreeting(string name)
       {
           Console.WriteLine("Good Morning, " + name);
       }
       private static void ChineseGreeting(string name)
       {
           Console.WriteLine("早上好, " + name);
       }
       private static void GreetPeople(string name, GreetingDelegate MakeGreeting)
       {
           MakeGreeting(name);
       }
       static void Main(string[] args)
       {
           GreetPeople("Liker",EnglishGreeting);
           GreetPeople("李志忠"，ChineseGreeting);
           Console.ReadLine();
       }
   }
   ```

   我们现在对委托做一个**总结**：委托是一个类，它定义了方法的类型，使得可以将方法当作另一个方法的参数来进行传递，这种将方法动态地赋给参数的做法，可以避免在程序中大量使用If … Else(Switch)语句，同时使得程序具有更好的可扩展性。

   ```C#
   ///委托类型GreetingDelegate和类型String的地位一样，都是定义了一种参数类型
   ///方法一
   static void Main(string[] args)
   {
       GreetPeople("Liker",EnglishGreeting);
       GreetPeople("李志忠"，ChineseGreeting);
       Console.ReadLine();
   }
   ///方法二
   static void Main(string[] args)
   {
       GreetingDelegate delegate1, delegate2;
       delegate1 = EnglishGreeting;
       delegate2 = ChineseGreeting;
       GreetPeople("Liker", delegate1);
       GreetPeople("李志中", delegate2);
       Console.ReadLine();
   }
   ```

   这里，我想说的是委托不同于 string 的一个特性：**可以将多个方法赋给同一个委托，或者叫将多个方法绑定到同一个委托，当调用这个委托的时候，将依次调用其所绑定的方法。**在这个例子中，语法如下：

   ```C#
   ///方法一
   static void Main(string[] args)
   {
       GreetingDelegate delegate1;
       delegate1 = EnglishGreeting; 
       delegate1 += ChineseGreeting;
       GreetPeople("Liker", delegate1);
       Console.ReadLine();
   }
   ///方法二
   static void Main(string[] args)
   {
       GreetingDelegate delegate1;
       delegate1 = EnglishGreeting;
       delegate1 += ChineseGreeting; 
       delegate1("Liker");
       Console.ReadLine();
   }
   ```

   **注意这里，第一次用的“=”，是赋值的语法；第二次，用的是“+=”，是绑定的语法。如果第一次就使用“+=”，将出现“使用了未赋值的局部变量”的编译错误。**我们也可以使用下面的代码来这样简化这一过程：

   ```C#
   GreetingDelegate delegate1 = new GreetingDelegate(EnglishGreeting);
   delegate1 += ChineseGreeting;
   ```

   既然给委托可以绑定一个方法，那么也应该有办法取消对方法的绑定，很容易想到，这个语法是“-=”：

   ```C#
   delegate1 -= ChineseGreeting;
   ```

   小结：**使用委托可以将多个方法绑定到同一个委托变量，当调用此变量时(这里用“调用”这个词，是因为此变量代表一个方法)，可以依次调用所有绑定的方法。**

3. 委托实现方式

   **简单实现**

   ```C#
   static void Main(string[] args)
   {
       GreetingDelegate delegate1, delegate2;
       delegate1 = EnglishGreeting;
       delegate2 = ChineseGreeting;
       GreetPeople("Liker", delegate1);
       GreetPeople("李志中", delegate2);
       Console.ReadLine();
   }
   ```

   **多播/多组**

   ```C#
   static void Main(string[] args)
   {
       GreetingDelegate delegate1;
       delegate1 = EnglishGreeting;
       delegate1 += ChineseGreeting; 
       delegate1("Liker");
       Console.ReadLine();
   }
   ```

   **委托数组**：delegate是一种类型，或者说是一个类，那么就可以组成一个delegate类型的数组

   ```C#
   ///System.Delegate源码
   namespace System
   {
       public abstract class Delegate : ICloneable, ISerializable
       {
           ...
       }
   }
   ```

   ```C#
   //声明一个委托
   delegate double Operations(double x);
   
   struct MathOperations
   {
       public static double MultiplyByTwo(double value)
       {
           return value * 2;
       }
   
       public static double Square(double value)
       {
           return value * value;
       }
   }
   
   class Program
   {
       static void Main()
       {
           Operations[] operations =
           {
               MathOperations.MultiplyByTwo,
               MathOperations.Square
           };
   
           for (int i = 0; i < operations.Length; i++)
           {
               Console.WriteLine("Using operations[{0}]:", i);
               DisplayNumber(operations[i], 2.0);
               DisplayNumber(operations[i], 7.94);
               Console.ReadLine();
           }
       }
   
       static void DisplayNumber(Operations action, double value)
       {
           double result = action(value);
           Console.WriteLine(
               "Input Value is {0}, result of operation is {1}", value, result);
       }
   }
   ```

   **Action<T>和Func<T>**

   使用委托时，除了为每个参数和返回类型定义一个新委托类型之外，还可以使用.NET Framework提供的泛型委托Action<T>和Func<T>，它们提供了从无参一直到最多16个参数的重载，如果方法需要获取16个以上的参数，就必须定义自己的委托类型，但这种情况应该是及其罕见的。其中Action<T>类可以调用没有返回值的方法，Func<T>允许调用带返回类型的方法。

   ```C#
   //带有返回值的方法
   public int Sum(int a, int b)
   {
       return a + b;
   }
   //不带返回值的方法
   public void Out(string text)
   {
       Console.WriteLine(text);
   }
   //常规的委托
   private delegate int D1(int a, int b);
   private delegate void D2(string text);
   public static void Main(string[] args)
   {
       D1 d1 = Sum;
       D2 d2 = Out;
       Console.WriteLine(d1(1,2));
       d2("hello");
       Action<string> action = Out;
       action("world");
       Func<int, int, int> func = Sum;
       Console.WriteLine(func(3,4));
   }
   ```

   **匿名方法**

   ```c#
   public void Main(string[] args)
   {
       Func<int,int,int> func = delegate(int a, int b)
       {
         	return a + b;  
       };
       Console.WriteLine(func(3,4));
   }
   ```

   **Lambda表达式**

   ```C#
   //不含参数
   Func<string> f = () => "linging";
   //一个参数
   Func<int, string> f1 = (int n) => n.Tostring();
   Func<int, string> f2 = (n) => n.Tostring();
   Func<int, string> f3 = n => n.Tostring();
   //多个参数
   Func<int, int, string> f4 = (int n,int m) => (n + m).Tostring();
   Func<int, int, string> f5 = (n, m) => (n + m).Tostring();
   ```

## 反射

## 泛型

1. 概述

   类是对象的模板，类是具有相同属性和行为的对象的抽象。

   generic paradigm通用的范式，是类型的模板。作为模板的类通过实例化产生不同的对象，而泛型是通过不同的类型实参产生不同的类型。

   我们在编程时，经常会遇到功能非常相似的模块，只是处理的数据不一样。

   **5种泛型：类，结构，接口，委托和方法**

2. 示例

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

3. 泛型的约束

   所谓的泛型约束，实际上就是约束类型T，使T必须遵循一定的规则。

   | 约束            | 说明                                                         |
   | --------------- | ------------------------------------------------------------ |
   | T : struct      | 类型参数必须是值类型                                         |
   | T : class       | 类型参数必须是引用类型，也适用于类，接口，委托或数组类型     |
   | T : new()       | 类型参数必须是具有无参数的公共构造函数，与其他约束一起使用时置于最后 |
   | T : <Base>      | 类型参数必须是指定的基类或派生自指定的基类                   |
   | T : <Interface> | 类型参数必须是指定的接口或实现指定的接口，可以多接口约束，约束接口也可以使泛型的 |

   ```C#
   //指定泛型参数为值类型
   class MyList<T> where T : struct
   //指定泛型参数为引用类型
   class MyList<T> where T : class
   //指定泛型参数为无参数的公共构造函数
   class MyList<T> where T : new()
   //指定泛型参数为指定基类
   class MyList<T> where T : Object
   //指定泛型参数为指定接口
   class MyList<T> where T : Interface
   ```

4. 协变和逆变

   ```C#
   public interface IMyList<in inT, out outT>
   {
       void Show(inT t);
       outT Get();
       outT Do(inT t);
   }
   public class MyList<T1, T2> : IMyList<T1, T2>
   {
       public void Show(T1 t)
       {
           Console.WriteLine(t.GetType().Name);
       }
       public T2 Get()
       {
           Console.WriteLine(typeof(T2).Name);
           return default(T2);
       }
       public T2 Do(T1 t)
       {
           Console.WriteLine(t.GetType().Name);
           Console.WriteLine(typeof(T2).Name);
           return default(T2);
       }
   }
   public class Program
   {
       public void Main(string[] args)
       {
           //严格按照类的类型进行实例化
           IMyList<Cat,Animal> myList1 = new MyList<Cat,Animal>();
           //协变，Cat是Animal的子类，返回结果的类型是父类，实例化的时候可以使用子类类型
           IMyList<Cat,Animal> myList2 = new MyList<Cat,Cat>();
           //逆变，在泛型接口/类中，不会使用子类独有的方法，使用父类作为入参可行
           IMyList<Cat,Animal> myList3 = new MyList<Animal,Animal>();
           //逆变+协变
           IMyList<Cat,Animal> myList3 = new MyList<Animal,Cat>();
       }
   }
   ```

   

## 事件

## 特性

## 属性

