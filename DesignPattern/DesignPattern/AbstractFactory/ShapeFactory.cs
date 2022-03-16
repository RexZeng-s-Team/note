using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDesignPattern.AbstractFactory
{
	/// <summary>
	/// 形状工厂，具体实现
	/// </summary>
	class ShapeFactory : AbstractFactory
	{
		/// <summary>
		/// 获取颜色，具体实现
		/// </summary>
		/// <returns></returns>
		public override IColor GetColor(string type)
		{
			return null;
		}
		/// <summary>
		/// 获取形状，具体实现
		/// </summary>
		/// <returns></returns>
		public override IShape GetShape(string type)
		{
			if (type.ToLower() == "circle")
			{
				return new Circle();
			}
			else if (type.ToLower() == "square")
			{
				return new Square();
			}
			else if (type.ToLower() == "rectangle")
			{
				return new Rectangle();
			}
			else
			{
				return null;
			}
		}
	}
}
