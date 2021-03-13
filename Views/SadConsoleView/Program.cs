using System;
using Microsoft.Xna.Framework;
using SadConsole;
using Game = SadConsole.Game;

namespace SadConsoleView
{
	public static class Program
	{
		static readonly MainScreen screen = new();
		static DateTime? exitTime;

		static void Main()
		{
			Game.Create(80, 25);

			Game.OnInitialize = Init;
			Game.OnUpdate = Update;

			Game.Instance.Run();
			Game.Instance.Dispose();
		}

		static void Update(GameTime obj)
		{
			if (exitTime.HasValue)
			{
				if (DateTime.Now > exitTime)
					Game.Instance.Exit();
				else
					return;
			}

			if (!screen.IsRunning)
			{
				screen.ShutDown();
				exitTime = DateTime.Now + TimeSpan.FromSeconds(3);
			}
		}

		static void Init()
		{
			Global.CurrentScreen = screen;
		}
	}
}