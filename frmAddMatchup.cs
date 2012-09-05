using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EastSidazFantasy
{
    public partial class frmAddMatchup : Form
    {
        private YahooLeague m_pLeague;        

        public frmAddMatchup()
        {
            InitializeComponent();
        }

        public YahooLeague League
        {
            set { m_pLeague = value; }
        }

        public int Team1
        {
            get { return cmbTeam1.SelectedIndex + 1; }
        }

        public int Team2
        {
            get { return cmbTeam2.SelectedIndex + 1; }
        }

        public DateTime StartDate
        {
            get { return dteStartDate.Value; }
        }

        public DateTime EndDate
        {
            get { return dteEndDate.Value; }
        }

        private void frmAddMatchup_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            foreach (YahooTeam myTeam in m_pLeague.Teams)
            {
                cmbTeam1.Items.Add(myTeam.TeamName);
                cmbTeam2.Items.Add(myTeam.TeamName);
            }
            if (cmbTeam1.Items.Count > 0)
                cmbTeam1.SelectedIndex = 0;
            if (cmbTeam2.Items.Count > 0)
                cmbTeam2.SelectedIndex = 0;
        }
    }
}