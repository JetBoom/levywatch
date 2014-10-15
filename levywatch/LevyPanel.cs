using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace levywatch
{
	public partial class LevyPanel : UserControl
	{
		public Levy m_levy;

		public LevyPanel()
		{
			InitializeComponent();
		}

		public void SetEnabled(bool enabled)
		{
			if (enabled)
			{
				chboxEnabled.CheckState = CheckState.Checked;
				BackColor = Color.Transparent;
			}
			else
			{
				chboxEnabled.CheckState = CheckState.Unchecked;
				BackColor = Color.DarkGray;

				if (m_levy != null)
				{
					m_levy.FirstTimeUpdated = false;
					m_levy.FailedLastCheck = false;
				}
			}
		}

		public void UpdatePanel()
		{
			if (m_levy == null)
				return;

			int ipopularity = m_levy.DisplayPopularity;
			float rpopularity = m_levy.RelativePopularity;

			lblName.Text = m_levy.Name;
			lblPercent.Text = m_levy.Percent + "%";
			lblPercent.ForeColor = m_levy.Percent == 100 ? Color.Purple : Color.White;
			lblPopularity.Text = ipopularity.ToString();

			if (m_levy.FailedLastCheck)
				lblPopularity.ForeColor = lblName.ForeColor = Color.DarkGray;
			else if (rpopularity >= LevyWatch.POPULARITY_VERYHOT)
				lblPopularity.ForeColor = lblName.ForeColor = Color.Red;
			else if (rpopularity >= LevyWatch.POPULARITY_HOT)
				lblPopularity.ForeColor = lblName.ForeColor = Color.Yellow;
			else if (rpopularity >= LevyWatch.POPULARITY_WARM)
				lblPopularity.ForeColor = lblName.ForeColor = Color.Lime;
			else if (rpopularity > 0)
				lblPopularity.ForeColor = lblName.ForeColor = Color.LightBlue;
			else
				lblPopularity.ForeColor = lblName.ForeColor = Color.White;
		}

		private void chboxEnabled_CheckedChanged(object sender, EventArgs e)
		{
			m_levy.Enabled = chboxEnabled.CheckState == CheckState.Checked;
		}
	}
}
