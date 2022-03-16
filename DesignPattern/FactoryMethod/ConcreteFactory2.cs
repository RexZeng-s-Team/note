using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
	/// <summary>
	/// 具体工厂2
	/// </summary>
	public class ConcreteFactory2 : AbstractFactory
	{
		/// <summary>
		/// 创建ConcreteProduct2对象实例
		/// </summary>
		/// <returns>放回ConcreteProduct2对象</returns>
		public override Product CreateProduct()
		{
			return new ConcreteProduct2();
		}
	}
}
