using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
	/// <summary>
	/// 抽象工厂类
	/// </summary>
	public abstract class AbstractFactory
	{
		/// <summary>
		/// 抽象方法，创建对象，每个具体工厂各自实现
		/// </summary>
		/// <returns>返回创建好的产品对象实例</returns>
		public abstract Product CreateProduct();
	}
}
