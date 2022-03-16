using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDesignPattern.AbstractFactory
{
	/// <summary>
	/// 抽象工厂
	/// 自身是抽象的，其中的各方法也是抽象的，每一个具体的工厂都必须实现所有的方法，能够独立生产任何一个产品
	/// </summary>
	public abstract class AbstractFactory
	{
		/// <summary>
		/// 获取形状-抽象方法，子类必须实现
		/// </summary>
		/// <returns></returns>
		public abstract IShape GetShape(string type);
		/// <summary>
		/// 获取颜色-抽象方法，子类必须实现
		/// </summary>
		/// <returns></returns>
		public abstract IColor GetColor(string type);
	}
}
