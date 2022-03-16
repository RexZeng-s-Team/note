using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
	/// <summary>
	/// 具体产品1
	/// </summary>
	public class ConcreteProduct1 : Product
	{
		/// <summary>
		/// 具体产品1
		/// </summary>
		public override void ShowInfo()
		{
			Console.WriteLine("this is ConcreteProduct1");
		}
	}
}
