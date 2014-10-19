using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace levywatch
{
	public partial class frmLevyWatch : Form
	{
		private enum ProgramState {Inactive, Waiting, Active};
		private ProgramState STATE = ProgramState.Inactive;

		private Label lblMouseCoords;

		// Timer loop variables
		private int T_LevyID = -1;
		private int T_WaitTicks = LevyWatch.WAITTICKS;
		//

		KeyboardHook hook_F1 = new KeyboardHook();
		KeyboardHook hook_F2 = new KeyboardHook();
		KeyboardHook hook_F3 = new KeyboardHook();
		KeyboardHook hook_F4 = new KeyboardHook();
		KeyboardHook hook_F5 = new KeyboardHook();
		KeyboardHook hook_F6 = new KeyboardHook();

		[DllImport("user32.dll", CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
		public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

		private const int MOUSEEVENTF_LEFTDOWN = 0x02;
		private const int MOUSEEVENTF_LEFTUP = 0x04;
		private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
		private const int MOUSEEVENTF_RIGHTUP = 0x10;

		public frmLevyWatch()
		{
			InitializeComponent();

			lblMouseCoords = new Label();
			lblMouseCoords.Parent = pnlLevies;
			lblMouseCoords.Name = "MouseCoordsLabel";
			lblMouseCoords.Text = "Mouse Position: 0 0";
			lblMouseCoords.Size = new System.Drawing.Size(200, 21);
			lblMouseCoords.ForeColor = Color.Red;
			lblMouseCoords.Dock = DockStyle.Top;
			lblMouseCoords.Visible = false;
			pnlLevies.Controls.Add(lblMouseCoords);

			hook_F1.KeyPressed += new EventHandler<KeyPressedEventArgs>(KeyPressed_F1);
			hook_F1.RegisterHotKey(Keys.F1);

			hook_F2.KeyPressed += new EventHandler<KeyPressedEventArgs>(KeyPressed_F2);
			hook_F2.RegisterHotKey(Keys.F2);

			hook_F3.KeyPressed += new EventHandler<KeyPressedEventArgs>(KeyPressed_F3);
			hook_F3.RegisterHotKey(Keys.F3);

			hook_F4.KeyPressed += new EventHandler<KeyPressedEventArgs>(KeyPressed_F4);
			hook_F4.RegisterHotKey(Keys.F4);

			hook_F5.KeyPressed += new EventHandler<KeyPressedEventArgs>(KeyPressed_F5);
			hook_F5.RegisterHotKey(Keys.F5);

			hook_F6.KeyPressed += new EventHandler<KeyPressedEventArgs>(KeyPressed_F6);
			hook_F6.RegisterHotKey(Keys.F6);
		}

		private void KeyPressed_F1(object sender, KeyPressedEventArgs e)
		{
			btnStart_Click(sender, e);
		}

		private void KeyPressed_F2(object sender, KeyPressedEventArgs e)
		{
			lblMouseCoords.Visible = !lblMouseCoords.Visible;
		}

		private void KeyPressed_F3(object sender, KeyPressedEventArgs e)
		{
			Clipboard.SetText(Cursor.Position.X + "\n" + Cursor.Position.Y);
		}

		private void KeyPressed_F4(object sender, KeyPressedEventArgs e)
		{
			Application.Exit();
		}

		private void KeyPressed_F5(object sender, KeyPressedEventArgs e)
		{
			foreach (Levy levy in LevyList.GetAll())
				levy.Enabled = levy.Popularity > 0;
		}

		private void KeyPressed_F6(object sender, KeyPressedEventArgs e)
		{
			foreach (Levy levy in LevyList.GetAll())
				levy.Enabled = true;
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			if (STATE == ProgramState.Inactive)
				SetProgramState(ProgramState.Waiting);
			else
				SetProgramState(ProgramState.Inactive);
		}

		private void timerMain_Tick(object sender, EventArgs e)
		{
			if (lblMouseCoords.Visible)
				lblMouseCoords.Text = "Mouse Position: " + Cursor.Position.X + " " + Cursor.Position.Y;
			else
			{
				if (STATE == ProgramState.Active)
					TickActive();
				else if (STATE == ProgramState.Waiting)
					TickWaiting();
			}
		}

		private void TickActive()
		{
			if (!DFWindowActive())
			{
				SetProgramState(ProgramState.Waiting);
				return;
			}

			if (T_LevyID == -1)
			{
				ClickMapCenter();
				T_LevyID = 0;
			}
			else
			{
				List<Levy> levies = LevyList.GetAllEnabled();
				if (T_LevyID < levies.Count)
				{
					Levy levy = levies[T_LevyID];
					if (levy != null && levy.x >= 0)
					{
						MoveMouseTo(levy.x, levy.y);
						if (T_WaitTicks == 1)
						{
							ScreenGrab();
							InitiateOCR();
							int perc = ParseOCR();
							if (perc == -1)
							{
								levy.FailedLastCheck = true;
								levy.UpdatePanel();
							}
							else
							{
								levy.FailedLastCheck = false;
								levy.SetPercent(perc);
							}
						}
					}
				}
				else
					T_WaitTicks = 0;

				T_WaitTicks--;
				if (T_WaitTicks <= 0)
				{
					T_WaitTicks = LevyWatch.WAITTICKS;
					T_LevyID++;
					if (T_LevyID >= levies.Count)
						T_LevyID = -1;
				}
			}
		}

		private void TickWaiting()
		{
			if (DFWindowActive())
			{
				SetProgramState(ProgramState.Active);
			}
		}

		private void MoveMouseTo(int x, int y)
		{
			Cursor.Position = new Point(x, y);
		}

		private void ClickAt(int x, int y)
		{
			MoveMouseTo(x, y);
			mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)x, (uint)y, 0, 0);
		}

		private void ClickMapCenter()
		{
			for (int i=0; i < 2; i++)
				ClickAt(LevyWatch.MAPCENTERBUTTONX, LevyWatch.MAPCENTERBUTTONY);
		}

		private void SetProgramState(ProgramState newstate)
		{
			if (newstate == ProgramState.Active)
				btnStart.Text = "Stop Watching";
			else if (newstate == ProgramState.Inactive)
			{
				btnStart.Text = "Start Watching";
				LevyList.ResetAll();
			}
			else if (newstate == ProgramState.Waiting)
				btnStart.Text = "Waiting for Darkfall map screen...";

			STATE = newstate;

			if (STATE != ProgramState.Active)
			{
				T_LevyID = -1;
				T_WaitTicks = LevyWatch.WAITTICKS;
			}

			timerMain.Enabled = STATE != ProgramState.Inactive;
		}

		// Grabs the screen at the mouse position, crops it, resizes it to be readable by OCR, and outputs it to a temp file.
		private void ScreenGrab()
		{
			int scrw = Screen.PrimaryScreen.Bounds.Width;
			int scrh = Screen.PrimaryScreen.Bounds.Height;
			int mx = Cursor.Position.X;
			int my = Cursor.Position.Y;
			int x = Math.Min(Math.Max(0, mx - LevyWatch.IMGW / 2), Screen.PrimaryScreen.Bounds.Width);
			int y = my;

			if (my > scrh / 2)
				y = my - LevyWatch.IMGH;

			Bitmap img = ImageManager.ScreenShot(x, y, LevyWatch.IMGW, LevyWatch.IMGH);
			ImageManager.RemoveNoise(img);
			ImageManager.Resize(ref img, 4.0f);
			ImageManager.SaveImage(img, LevyWatch.TEMPIMAGEFILE);
		}

		private void InitiateOCR()
		{
			ProcessStartInfo info = new ProcessStartInfo("tesseract", LevyWatch.TEMPIMAGEFILE + " out " + LevyWatch.OCRPARAMS);
			info.WorkingDirectory = Directory.GetCurrentDirectory();
			info.WindowStyle = ProcessWindowStyle.Hidden;

			timerMain.Enabled = false;

			Process proc = Process.Start(info);
			proc.WaitForExit();

			timerMain.Enabled = true;
		}

		private int ParseOCR()
		{
			int percent = -1;

			string contents;
			try { contents = File.ReadAllText(LevyWatch.OCRFILE); }
			catch { return 0; }

			Console.WriteLine(contents);

			int index = contents.IndexOf("%");
			if (index >= 0)
			{
				Regex r = new Regex(@"([0-9]+)[%]");
				Match m = r.Match(contents);
				if (m.Success)
				{
					string match = m.Value.Substring(0, m.Value.Length - 1);
					try { percent = int.Parse(match); }
					catch { }
				}
			}

			try { File.Delete(LevyWatch.OCRFILE); }
			catch { }

			return percent;
		}

		private bool DFWindowActive()
		{
			string windowtitle = WindowTitle.GetActiveWindowTitle();
			if (windowtitle == null)
				return false;

			return windowtitle.IndexOf(LevyWatch.DFWINDOWTITLE) == 0;
		}

		private void frmLevyWatch_Load(object sender, EventArgs e)
		{
			List<Levy> levies = LevyList.GetAll();

			int i = 0;
			foreach (Levy levy in levies.Reverse<Levy>())
			{
				LevyPanel panel = new LevyPanel();
				panel.Parent = pnlLevies;
				panel.Name = "LevyPanel" + i++;
				panel.Dock = DockStyle.Top;

				levy.pnlLevyPanel = panel;
				panel.m_levy = levy;

				panel.UpdatePanel();

				pnlLevies.Controls.Add(panel);
			}

			LevyWatch.LoadConfig();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("LevyWatch v1.0.0\n\nby William \"JetBoom\" Moodhe\nwilliammoodhe@gmail.com\n\nUses tesseract OCR");
		}

		private void helpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Hotkeys:\nCTRL + F1 - Start/stop\nCTRL + F2 - Pause\nCTRL + F3 - Copy coords to clipboard\nCTRL + F4 - Close\nCTRL + F5 - Disable all levys with no popularity.\nCTRL + F6 - Enable all levys.\n\nBe on the map screen, fully zoomed out.\nDisable monster spawns, political map, and chat.\nYou currently must be in borderless window at 1920x1080 resolution - nothing else will work.\n\nRead the readme for anything else.");
		}
	}
}
