using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Factory factory = new Factory();
			Product product = factory.CreateProduct("ConcreteProduct2");
			product.ShowInfo();
			Console.ReadKey();
		}
	}
}
