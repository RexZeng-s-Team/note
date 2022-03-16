using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
	/// <summary>
	/// 工厂类
	/// </summary>
	public class Factory
	{
		/// <summary>
		/// 创建对象,只负责创建对象实例，根据不同的传参
		/// </summary>
		public Product CreateProduct(string name)
		{
			//获取当前程序集
			Assembly ass = Assembly.GetCallingAssembly();
			//解析程序集名称
			AssemblyName assName = new AssemblyName(ass.FullName);
			//获取程序集的类型
			Type t = ass.GetType(assName.Name + "." + name);
			//创建类的实例对象
			Product o = (Product)Activator.CreateInstance(t);
			return o;
		}
	}
}
