using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDesignPattern.AbstractFactory
{
	/// <summary>
	/// 颜色，接口
	/// 产品族级别
	/// </summary>
	public interface IColor
	{
		/// <summary>
		/// 填充颜色
		/// </summary>
		void Fill();
	}
	/// <summary>
	/// 红色，具体实现
	/// 具体产品
	/// </summary>
	public class Red : IColor
	{
		/// <summary>
		/// 填充颜色
		/// </summary>
		public void Fill()
		{
			Console.WriteLine("color = red");
		}
	}
	/// <summary>
	/// 绿色，具体实现
	/// 具体产品
	/// </summary>
	public class Green : IColor
	{
		/// <summary>
		/// 填充颜色
		/// </summary>
		public void Fill()
		{
			Console.WriteLine("color = green");
		}
	}
	/// <summary>
	/// 蓝色，具体实现
	/// 具体产品
	/// </summary>
	public class Blue : IColor
	{
		/// <summary>
		/// 填充颜色
		/// </summary>
		public void Fill()
		{
			Console.WriteLine("color = blue");
		}
	}
}
