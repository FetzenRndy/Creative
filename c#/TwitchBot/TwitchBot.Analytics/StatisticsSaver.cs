using System;
using System.IO;
using System.Timers;
using System.Diagnostics;

using Newtonsoft.Json;

namespace TwitchBot.Analytics
{
	public class StatisticsSaver
	{
		private readonly ChatAnalyzer analyzer;
		private readonly string filePath;

		public event Action OnSave;

		public StatisticsSaver(string filePath, ChatAnalyzer analyzer)
		{
			this.filePath = filePath;
			this.analyzer = analyzer;
		}

		public void Save()
		{
			try
			{
				if (!Directory.Exists("./statistics"))
				{
					Directory.CreateDirectory("./statistics");
				}

				File.WriteAllText(
					this.filePath,
					JsonConvert.SerializeObject(SaveFilePayload.FromAnalyzer(this.analyzer), Formatting.Indented)
				);

				this.OnSave?.Invoke();
			}
			catch(Exception e)
			{
				Debug.WriteLine("Could not save statistics - \n" + e);
			}
		}

		public Timer AsTimer(int interval)
		{
			var timer = new Timer(interval);
			timer.Elapsed += (sender, e) => this.Save();

			return timer;
		}
	}
}
