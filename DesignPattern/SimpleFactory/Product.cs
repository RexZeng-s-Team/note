using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
	/// <summary>
	/// 抽象产品类
	/// </summary>
	public abstract class Product
	{
		/// <summary>
		/// 抽象方法，每个具体的产品各自实现
		/// </summary>
		public abstract void ShowInfo();
	}
}
