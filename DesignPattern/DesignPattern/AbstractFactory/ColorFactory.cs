using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDesignPattern.AbstractFactory
{
	/// <summary>
	/// 具体的某一个工厂，主要负责颜色生产
	/// </summary>
	class ColorFactory : AbstractFactory
	{
		/// <summary>
		/// 具体工厂，创建具体的对象，这是职责
		/// </summary>
		/// <param name="colortype"></param>
		/// <returns></returns>
		public override IColor GetColor(string type)
		{
			if (type.ToLower() == "red")
			{
				return new Red();
			}
			else if (type.ToLower() == "green")
			{
				return new Green();
			}
			else if(type.ToLower() == "blue")
			{
				return new Blue();
			}
			else
			{
				return null;
			}
		}

		public override IShape GetShape(string type)
		{
			return null;
		}
	}
}
