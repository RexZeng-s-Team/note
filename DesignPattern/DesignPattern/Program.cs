using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDesignPattern
{
	class Program
	{
		static void Main(string[] args)
		{
			//画一个圆形
			AbstractFactory.ShapeFactory shapeFactory = new AbstractFactory.ShapeFactory();
			AbstractFactory.IShape shape = shapeFactory.GetShape("circle");
			shape.Draw();
			//如果不使用抽象工厂模式
			AbstractFactory.Circle circle = new AbstractFactory.Circle();
			circle.Draw();
			//使用和不使用抽象工厂模式的区别，使用工厂模式，只需要和具体的工厂类和抽象的产品类有关系，无论在增加多少产品，使用方不需要知道，也没有耦合关系
			Console.ReadKey();
		}
	}
}
