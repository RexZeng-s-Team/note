using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Product product = new ConcreteFactory1().CreateProduct();
			product.ShowInfo();
			product = new ConcreteFactory2().CreateProduct();
			product.ShowInfo();
			Console.ReadKey();
		}
	}
}
