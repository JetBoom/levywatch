using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace levywatch
{
	static class LevyList
	{
		private static List<Levy> levies = new List<Levy>();
		private static List<Levy> enabled_levies = new List<Levy>();

		public static Levy Get(string name)
		{
			try { return levies.First(item => item.Name == name); }
			catch { return null; }
		}

		public static List<Levy> GetAll()
		{
			return levies;
		}

		public static List<Levy> GetAllEnabled()
		{
			return enabled_levies;
		}

		public static void UpdateEnabledList()
		{
			enabled_levies.Clear();

			foreach (Levy levy in GetAll())
			{
				if (levy.Enabled)
					enabled_levies.Add(levy);
			}
		}

		public static int MaxPopularity()
		{
			return Math.Max(levies.Max(item => item.Popularity), 4);
		}

		public static void Load()
		{
			levies.Clear();

			System.IO.StreamReader file;
			try { file = new System.IO.StreamReader("data/levylist.dat"); }
			catch { return; }

			string name = "";
			int x = -1;
			int y = -1;

			uint type = 0;

			string line;
			while ((line = file.ReadLine()) != null)
			{
				if (line.Length == 0 || line.Substring(0, 1) == "#")
					continue;

				if (type == 0)
					name = line;
				else
				{
					int coord;
					try { coord = int.Parse(line); }
					catch { coord = -1; }

					if (type == 1)
						x = coord;
					else
						y = coord;
				}

				if (++type > 2)
				{
					type = 0;

					Levy levy = new Levy();
					levy.Name = name;
					levy.x = x;
					levy.y = y;
					levies.Add(levy);
				}
			}

			file.Close();

			UpdateEnabledList();
		}

		public static void ResetAll()
		{
			foreach (Levy levy in levies)
				levy.Reset();
		}
	}

	public class Levy
	{
		public string Name = "Unnamed";
		public int Percent = 0;

		private int m_Popularity = 0; //private double m_Popularity = 0D;
		public int Popularity //public double Popularity
		{
			get
			{
				return m_Popularity;
			}
			set
			{
				m_Popularity = Math.Max(0, value);
			}
		}

		public int DisplayPopularity
		{
			get
			{
				/*int ipop = (int)Math.Ceiling(m_Popularity * 1000);
				return Math.Min(1000, ipop);*/
				return m_Popularity;
			}
		}

		public float RelativePopularity
		{
			get
			{
				return (float)Popularity / (float)LevyList.MaxPopularity();
			}
		}

		// Screen coords.
		public int x = -1;
		public int y = -1;

		private bool m_Enabled = true;

		public bool Enabled
		{
			get { return m_Enabled; }
			set
			{
				m_Enabled = value;

				LevyList.UpdateEnabledList();

				if (pnlLevyPanel != null)
					pnlLevyPanel.SetEnabled(m_Enabled);
			}
		}

		public bool FirstTimeUpdated = false;
		public bool FailedLastCheck = false;
		public DateTime LastUpdate;
		private DateTime LastDecay;

		public LevyPanel pnlLevyPanel;

		public Levy()
		{
			LastUpdate = DateTime.Now;
			LastDecay = DateTime.Now;

			Enabled = true;
		}

		public void UpdatePanel()
		{
			if (pnlLevyPanel == null)
				return;

			pnlLevyPanel.UpdatePanel();
		}

		public void SetPercent(int newpercent)
		{
			if (FirstTimeUpdated)
			{
				if (newpercent < Percent) // Levy goes down (requisitioned).
				{
					//Popularity += 100D;
					Popularity++;

					try
					{
						SoundPlayer player = new SoundPlayer(@"sound/levy_req.wav");
						player.Play();
					}
					catch { }
				}
				else if (newpercent > Percent) // Levy goes up.
				{
					/*double dT = DeltaTime();
					double dPerc = Math.Min(10D, (double)(newpercent - Percent));

					Popularity += 2D * dPerc / dT - 1 / dT;*/
					Popularity += (newpercent - Percent);

					//string sndname = Popularity >= LevyWatch.POPULARITY_VERYHOT ? "sound/popularity_hot.wav" : "sound/percent_up.wav";
					string sndname = RelativePopularity >= LevyWatch.POPULARITY_VERYHOT ? "sound/popularity_hot.wav" : "sound/percent_up.wav";
					try
					{
						SoundPlayer player = new SoundPlayer(@sndname);
						player.Play();
					}
					catch { }
				}
				else if (LevyWatch.DECAY_SECONDS > 0 && DateTime.Now >= LastDecay + TimeSpan.FromSeconds(LevyWatch.DECAY_SECONDS))
				{
					LastDecay = DateTime.Now;
					Popularity--;
				}
				/*else // Levy stays the same.
				{
					// dPop = -1/dT
					Popularity -= 1 / DeltaTime();
				}*/
			}
			else
				FirstTimeUpdated = true;

			Percent = newpercent;
			LastUpdate = DateTime.Now;

			UpdatePanel();
		}

		public double DeltaTime()
		{
			TimeSpan delta = DateTime.Now - LastUpdate;
			return delta.TotalMilliseconds * 0.001D;
		}

		public void Reset()
		{
			Percent = 0;
			Popularity = 0; //0D;
			LastUpdate = DateTime.Now;
			LastDecay = DateTime.Now;
			FirstTimeUpdated = false;
			FailedLastCheck = false;
			UpdatePanel();
		}
	}
}