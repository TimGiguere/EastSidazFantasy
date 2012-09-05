using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EastSidazFantasy
{
    public class YahooLeague
    {
        private int m_iLeagueId;
        private string m_sLeagueName;
        private List<YahooTeam> m_aTeams;

        public YahooLeague()
        {
            m_aTeams = new List<YahooTeam>();
        }

        public int LeagueId
        {
            get { return m_iLeagueId; }
            set { m_iLeagueId = value; }
        }

        public string LeagueName
        {
            get { return m_sLeagueName; }
            set { m_sLeagueName = value; }
        }

        public List<YahooTeam> Teams
        {
            get { return m_aTeams; }
            set { m_aTeams = value; }
        }

        public void ListTeams(string sContents)
        {
            XmlDocument oDoc = new XmlDocument();
            oDoc.LoadXml(sContents);
            XmlElement oRootNode = (XmlElement)oDoc.ChildNodes[1];
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(oDoc.NameTable);
            nsmgr.AddNamespace("yahoo", "http://fantasysports.yahooapis.com/fantasy/v2/base.rng");
            XmlElement oTeams = (XmlElement)oRootNode.SelectSingleNode("descendant::yahoo:league/yahoo:teams", nsmgr);
            if (oTeams != null)
            {
                m_aTeams.Clear();
                XmlNodeList oList = oTeams.SelectNodes("descendant::yahoo:team", nsmgr);
                foreach (XmlElement myElement in oList)
                {
                    YahooTeam myTeam = new YahooTeam();
                    XmlElement oNode = (XmlElement)myElement.SelectSingleNode("descendant::yahoo:team_id", nsmgr);
                    myTeam.TeamID = Convert.ToInt32(oNode.InnerText);
                    oNode = (XmlElement)myElement.SelectSingleNode("descendant::yahoo:name", nsmgr);
                    myTeam.TeamName = oNode.InnerText;
                    m_aTeams.Add(myTeam);
                }
            }
        }
    }
}
