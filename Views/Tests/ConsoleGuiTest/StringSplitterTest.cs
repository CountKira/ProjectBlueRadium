﻿using System;
using Views.ConsoleGui;
using Xunit;

namespace ConsoleGuiTest
{
	public class StringSplitterTest
	{
		[Fact]
		public void Foo()
		{
			const string text = "ab cd";
			var split = StringSplitter.BreakText(text, 2);
			Assert.Equal("abcd", split);
		}

		[Fact]
		public void Foo2()
		{
			const string text = "ab cd ef";
			var split = StringSplitter.BreakText(text, 2);
			Assert.Equal("abcdef", split);
		}
	}
}