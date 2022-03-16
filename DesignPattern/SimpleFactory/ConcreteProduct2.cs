using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
	/// <summary>
	/// 具体产品2
	/// </summary>
	public class ConcreteProduct2 : Product
	{
		/// <summary>
		/// 具体产品2
		/// </summary>
		public override void ShowInfo()
		{
			Console.WriteLine("this is ConcreteProduct2");
		}
	}
}
