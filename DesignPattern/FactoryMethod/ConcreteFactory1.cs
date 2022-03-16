using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
	/// <summary>
	/// 具体工厂1
	/// </summary>
	public class ConcreteFactory1 : AbstractFactory
	{
		/// <summary>
		/// 创建ConcreteProduct1对象实例
		/// </summary>
		/// <returns>返回ConcreteProduct1对象</returns>
		public override Product CreateProduct()
		{
			return new ConcreteProduct1();
		}
	}
}
