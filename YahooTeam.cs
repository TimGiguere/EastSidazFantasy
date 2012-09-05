using System;
using System.Collections.Generic;
using System.Text;

namespace EastSidazFantasy
{
    public class YahooTeam
    {
        private int m_iTeamID;
        private string m_sTeamName;

        public int TeamID
        {
            get { return m_iTeamID; }
            set { m_iTeamID = value; }
        }

        public string TeamName
        {
            get { return m_sTeamName; }
            set { m_sTeamName = value; }
        }
    }
}
