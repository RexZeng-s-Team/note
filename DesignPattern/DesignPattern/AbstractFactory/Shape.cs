using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDesignPattern.AbstractFactory
{	
	/// <summary>
	/// 形状-接口
	/// 产品族级别
	/// </summary>
	public interface IShape
	{
		/// <summary>
		/// 画图形状
		/// </summary>
		void Draw();
	}
	/// <summary>
	/// 圆形，形状的具体实现
	/// 具体产品
	/// </summary>
	public class Circle : IShape
	{
		public void Draw()
		{
			Console.WriteLine("shape = circle");
		}
	}
	/// <summary>
	/// 正方形，形状的具体实现
	/// 具体产品
	/// </summary>
	public class Square : IShape
	{
		public void Draw()
		{
			Console.WriteLine("shape = square");
		}
	}
	/// <summary>
	/// 矩形，形状的具体实现
	/// 具体产品
	/// </summary>
	public class Rectangle : IShape
	{
		public void Draw()
		{
			Console.WriteLine("shape = rectangle");
		}
	}
}
