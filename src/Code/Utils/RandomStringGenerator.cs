using System;

namespace Triangles.Code.Utils
{
	public class RandomStringGenerator
	{
		private readonly Random _rng = new Random();
		private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

		public string RandomString(int size)
		{
			char[] buffer = new char[size];

			for (int i = 0; i < size; i++)
			{
				buffer[i] = _chars[_rng.Next(_chars.Length)];
			}
			return new string(buffer);
		}
	}
}