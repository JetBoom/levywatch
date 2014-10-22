/*
 * LevyWatch
 * A program for monitoring and alerting for levy increases in Darkfall: Unholy Wars.
 * Uses tesseract-ocr to read and parse screenshots to convert in-game graphics to text for processing.
 * 
 * Created by William Moodhe
 * williammoodhe@gmail.com
 * http://www.github.com/jetboom
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace levywatch
{
	static class LevyWatch
	{
		// OCR settings
		public const string OCRFILE = "out.txt";
		public const string OCRPARAMS = "-psm 6";
		public const string TEMPIMAGEFILE = "temp.png";
		public const int IMGW = 150;
		public const int IMGH = 150;
		public const int WAITTICKS = 6;

		// Screen coords
		public static int MAPCENTERBUTTONX = 12;
		public static int MAPCENTERBUTTONY = 626;

		// Statistics
		public static float POPULARITY_VERYHOT = 0.75f;
		public static float POPULARITY_HOT = 0.5f;
		public static float POPULARITY_WARM = 0.3f;
		public static int DECAY_SECONDS = 120;

		public static string DFWINDOWTITLE = "Darkfall Unholy Wars";

		[STAThread]
		static void Main()
		{
			LevyList.Load();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new frmLevyWatch());
			AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
		}

		public static void LoadConfig()
		{
			string[] lines;

			try { lines = File.ReadAllLines("config.cfg"); }
			catch { return; }

			foreach (string line in lines)
			{
				if (line.Length == 0 || line.Substring(0, 1) == "#" || line.Substring(0, 2) == "//")
					continue;

				int delim = line.IndexOf("=");
				if (delim > 0)
				{
					string property = line.Substring(0, delim).ToUpper();
					string value = line.Substring(delim + 1);
					if (value.Length == 0)
						continue;

					switch (property)
					{
						case "VERYHOT":
						{
							/*int ival = -1;
							int.TryParse(value, out ival);*/
							float ival = -1;
							float.TryParse(value, out ival);
							if (ival > 0)
								POPULARITY_VERYHOT = ival;

							break;
						}
						case "HOT":
						{
							/*int ival = -1;
							int.TryParse(value, out ival);*/
							float ival = -1;
							float.TryParse(value, out ival);
							if (ival > 0)
								POPULARITY_HOT = ival;

							break;
						}
						case "WARM":
						{
							/*int ival = -1;
							int.TryParse(value, out ival);*/
							float ival = -1;
							float.TryParse(value, out ival);
							if (ival > 0)
								POPULARITY_WARM = ival;

							break;
						}
						case "DECAY_SECONDS":
						{
							int ival = -10;
							int.TryParse(value, out ival);
							if (ival != -10)
								DECAY_SECONDS = ival;

							break;
						}
						case "GAME_WINDOW_SUBSTRING":
						{
							DFWINDOWTITLE = value;

							break;
						}
						case "MAP_CENTER_BUTTON_X":
						{
							int ival = -1;
							int.TryParse(value, out ival);
							if (ival > 0)
								MAPCENTERBUTTONX = ival;

							break;
						}
						case "MAP_CENTER_BUTTON_Y":
						{
							int ival = -1;
							int.TryParse(value, out ival);
							if (ival > 0)
								MAPCENTERBUTTONY = ival;

							break;
						}
						case "DISABLED":
						{
							string[] levynames = value.Split(',');
							foreach (string levyname in levynames)
							{
								Levy levy = LevyList.Get(levyname);
								if (levy != null)
									levy.Enabled = false;
							}

							break;
						}
					}
				}
			}
		}

		static void OnProcessExit(object sender, EventArgs e)
		{
			try { File.Delete(TEMPIMAGEFILE); }
			catch { }
			try { File.Delete(OCRFILE); }
			catch { }
		}
	}
}
