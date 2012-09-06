using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections;
//using System.Data.OleDb;
using System.Data.Odbc;
using System.IO;
using System.Xml;

namespace EastSidazFantasy
{

    public struct Matchup
    {
        public int ID;
        public int Team1;
        public int Team2;
        public DateTime StartDate;
        public DateTime EndDate;
    }

    public partial class Form1 : Form
    {
        private string m_sConsumerKey;
        private string m_sSignature;
        private string m_sOauth_token;
        private string m_sOauth_token_secret;
        private string m_sOauth_verifier;
        private string m_sFullOauth_token;
        private string m_sFullOauth_token_secret;
        private string m_sOauth_Session_Handle;
        //private OleDbConnection m_pConnection;
        private OdbcConnection m_pConnection;
        private ESOAuth m_pOAuthClass;
        private List<YahooLeague> m_pLeagues = new List<YahooLeague>();
        private List<Matchup> m_pMatchups = new List<Matchup>();

        public Form1()
        {
            InitializeComponent();
        }

        private void webBrowser1_Navigated(object sender, System.Windows.Forms.WebBrowserNavigatedEventArgs e)
        {
            if (e.Url.PathAndQuery.StartsWith("/fantasy?oauth_token"))
            {
                webBrowser1.Visible = false;
                webBrowser1.Stop();
                string[] sQueryString = e.Url.PathAndQuery.Split(new char[] { '?' });
                string[] sPairs = sQueryString[1].Split(new char[] { '&' });
                foreach (string myString in sPairs)
                {
                    string[] sValues = myString.Split(new char[] { '=' });
                    if (sValues[0] == "oauth_verifier")
                    {
                        string sSql = "UPDATE tblOAuth SET oauth_verifier = ? WHERE oauth_token = ?" ; // \"" + m_sOauth_token + "\"";
                        //OleDbCommand pCommand = new OleDbCommand(sSql, m_pConnection);
                        OdbcCommand pCommand = new OdbcCommand(sSql, m_pConnection);
                        OdbcParameter pParam = new OdbcParameter();
                        pParam.Value = sValues[1];
                        pCommand.Parameters.Add(pParam);
                        pParam = new OdbcParameter();
                        pParam.Value = m_sOauth_token;
                        pCommand.Parameters.Add(pParam);
                        pCommand.ExecuteNonQuery();

                        m_sOauth_verifier = sValues[1];
                        break;
                    }
                }
                GetToken();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.Text = this.Text + " - Version " + Application.ProductVersion;
            m_pOAuthClass = new ESOAuth();
            OpenDatabaseConnection();
            GetApplicationKeys();
            if (!(GetRequestToken()))
            {
                GetRequestAuthorization();
            }
            else
            {
                RefreshToken();
            }
        }

        private void RefreshToken()
        {
            string sURL = m_pOAuthClass.GenerateRefreshToken(m_sConsumerKey, m_sSignature, m_sFullOauth_token_secret, m_sFullOauth_token, m_sOauth_Session_Handle);
            WebClient myClient = new WebClient();
            string sContents = myClient.DownloadString(sURL);
            string[] sSplit = sContents.Split(new char[] { '&' });
            foreach (string myString in sSplit)
            {
                string[] sValues = myString.Split(new char[] { '=' });
                if (sValues[0] == "oauth_token")
                    m_sFullOauth_token = sValues[1];
                if (sValues[0] == "oauth_token_secret")
                    m_sFullOauth_token_secret = sValues[1];
                if (sValues[0] == "oauth_session_handle")
                    m_sOauth_Session_Handle = sValues[1];
            }
            string sSql = "UPDATE tblOAuth SET full_oauth_token = ?, " + //\"" + m_sFullOauth_token + "\", " +
                          "full_oauth_token_secret = ?, " + // \"" + m_sFullOauth_token_secret + "\", " +
                          "oauth_session_handle = ? " + // \"" + m_sOauth_Session_Handle + "\" " +
                          "WHERE oauth_token = ?"; // \"" + m_sOauth_token + "\"";
            //OleDbCommand pCommand = new OleDbCommand(sSql, m_pConnection);
            OdbcCommand pCommand = new OdbcCommand(sSql, m_pConnection);
            OdbcParameter pParam = new OdbcParameter();
            pParam.OdbcType = OdbcType.Text;
            pParam.Size = 1024;
            pParam.Value = m_sFullOauth_token;
            pCommand.Parameters.Add(pParam);
            pParam = new OdbcParameter();
            pParam.OdbcType = OdbcType.Text;
            pParam.Size = 1024;
            pParam.Value = m_sFullOauth_token_secret;
            pCommand.Parameters.Add(pParam);
            pParam = new OdbcParameter();
            pParam.OdbcType = OdbcType.Text;
            pParam.Size = 1024;
            pParam.Value = m_sOauth_Session_Handle;
            pCommand.Parameters.Add(pParam);
            pParam = new OdbcParameter();
            pParam.OdbcType = OdbcType.Text;
            pParam.Size = 1024;
            pParam.Value = m_sOauth_token;
            pCommand.Parameters.Add(pParam);
            pCommand.ExecuteNonQuery();
            GetLeagues();
        }

        private void GetLeagues()
        {
            string sURL = m_pOAuthClass.GenerateURL("http://fantasysports.yahooapis.com/fantasy/v2/users;use_login=1/games;game_keys=mlb/leagues", m_sConsumerKey, m_sSignature, m_sFullOauth_token, m_sFullOauth_token_secret);
            WebClient myClient = new WebClient();
            string sContents = myClient.DownloadString(sURL);
            ListLeagues(sContents);
        }

        private void ListLeagues(string sContents)
        {
            XmlDocument oDoc = new XmlDocument();
            oDoc.LoadXml(sContents);
            XmlElement oRootNode = (XmlElement)oDoc.ChildNodes[1]; //(XmlElement)oDoc.SelectSingleNode("/fantasy_content/users/user/games/game/leagues");
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(oDoc.NameTable);
            nsmgr.AddNamespace("yahoo", "http://fantasysports.yahooapis.com/fantasy/v2/base.rng");
            XmlElement oLeagues = (XmlElement)oRootNode.SelectSingleNode("descendant::yahoo:users/yahoo:user/yahoo:games/yahoo:game[yahoo:code=\"mlb\"]/yahoo:leagues", nsmgr);
            if (oLeagues != null)
            {
                m_pLeagues.Clear();
                cmbLeagues.Items.Clear();
                XmlNodeList oList = oLeagues.SelectNodes("descendant::yahoo:league", nsmgr);
                foreach (XmlElement myElement in oList)
                {
                    YahooLeague myLeague = new YahooLeague();
                    XmlElement oNode = (XmlElement)myElement.SelectSingleNode("descendant::yahoo:league_id", nsmgr);
                    myLeague.LeagueId = Convert.ToInt32(oNode.InnerText);
                    oNode = (XmlElement)myElement.SelectSingleNode("descendant::yahoo:name", nsmgr);
                    myLeague.LeagueName = oNode.InnerText;
                    m_pLeagues.Add(myLeague);
                    cmbLeagues.Items.Add(oNode.InnerText);
                }
                if (cmbLeagues.Items.Count > 0)
                    cmbLeagues.SelectedIndex = 0;
            }
        }

        private void GetToken()
        {
            try
            {
                string sURL = m_pOAuthClass.GenerateGetToken(m_sConsumerKey, m_sSignature, m_sOauth_token_secret, m_sOauth_token, m_sOauth_verifier);
                WebClient myClient = new WebClient();
                string sContents = myClient.DownloadString(sURL);
                string[] sSplit = sContents.Split(new char[] { '&' });
                foreach (string myString in sSplit)
                {
                    string[] sValues = myString.Split(new char[] { '=' });
                    if (sValues[0] == "oauth_token")
                        m_sFullOauth_token = sValues[1];
                    if (sValues[0] == "oauth_token_secret")
                        m_sFullOauth_token_secret = sValues[1];
                    if (sValues[0] == "oauth_session_handle")
                        m_sOauth_Session_Handle = sValues[1];
                }
                string sSql = "UPDATE tblOAuth SET full_oauth_token = ?, " + //\"" + m_sFullOauth_token + "\", " +
                              "full_oauth_token_secret = ?, " + //\"" + m_sFullOauth_token_secret + "\", " +
                              "oauth_session_handle = ? " + //\"" + m_sOauth_Session_Handle + "\" " +
                              "WHERE oauth_token = ?"; //\"" + m_sOauth_token + "\"";
                //OleDbCommand pCommand = new OleDbCommand(sSql, m_pConnection);
                OdbcCommand pCommand = new OdbcCommand(sSql, m_pConnection);
                OdbcParameter pParam = new OdbcParameter();
                pParam.OdbcType = OdbcType.Text;
                pParam.Size = 1024;
                pParam.Value = m_sFullOauth_token;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.OdbcType = OdbcType.Text;
                pParam.Size = 1024;
                pParam.Value = m_sFullOauth_token_secret;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.OdbcType = OdbcType.Text;
                pParam.Size = 1024;
                pParam.Value = m_sOauth_Session_Handle;                
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.OdbcType = OdbcType.Text;
                pParam.Size = 1024;
                pParam.Value = m_sOauth_token;
                pCommand.Parameters.Add(pParam);
                pCommand.ExecuteNonQuery();

                GetLeagues();
            }
            catch (Exception ex)
            {
                clsStatic.ShowError(ex.ToString());
            }
        }

        private void GetRequestAuthorization()
        {
            string sURI = "https://api.login.yahoo.com/oauth/v2/request_auth?oauth_token=" + m_sOauth_token;
            webBrowser1.Visible = true;
            webBrowser1.Navigate(sURI);
        }

        private void GetApplicationKeys()
        {
            try
            {
                string sSql = "SELECT * FROM tblKeys";
                //OleDbCommand pCommand = new OleDbCommand(sSql, m_pConnection);
                //OleDbDataReader pReader = pCommand.ExecuteReader();
                OdbcCommand pCommand = new OdbcCommand(sSql, m_pConnection);
                OdbcDataReader pReader = pCommand.ExecuteReader();
                if (pReader.Read())
                {
                    m_sConsumerKey = pReader["oauth_consumer_key"].ToString();
                    m_sSignature = pReader["oauth_signature"].ToString();
                }
                pReader.Close();
            }
            catch (Exception ex)
            {
                clsStatic.ShowError(ex.ToString());
            }
        }

        private void OpenDatabaseConnection()
        {
            string sDatabase = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\system\\FantasySettings.mdb";
            //string sConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDatabase;
            string sConnection = "Driver={Microsoft Access Driver (*.mdb)};Dbq=" + sDatabase + ";Uid=Admin;Pwd=;";
            //m_pConnection = new OleDbConnection(sConnection);
            m_pConnection = new OdbcConnection(sConnection);
            m_pConnection.Open();
        }

        private bool GetRequestToken()
        {
            /* Example request:
             * https://api.login.yahoo.com/oauth/v2/get_request_token?
               oauth_nonce=123456789
               &oauth_timestamp=1257965367
               &oauth_consumer_key=dj0yJmk9bk9WUkZhUDhOd2VHJmQ9WVdrOVFYRTBkVTFyTjJzbWNHbzlNVGs1T1RnMk5UYzJNZy0tJnM9Y29uc3VtZXJzZWNyZXQmeD05Nw--
               &oauth_signature_method=plaintext
               &oauth_signature=f900ab95a43dfd51a58a389eba776e445484f19c%26
               &oauth_version=1.0
               &xoauth_lang_pref=en-us
               &oauth_callback=http://fantasysports.yahoo.com
             */

            bool bResult = false;
            try
            {
                string sSql = "SELECT * FROM tblOAuth";
                //OleDbCommand pCommand = new OleDbCommand(sSql, m_pConnection);
                //OleDbDataReader pReader = pCommand.ExecuteReader();
                OdbcCommand pCommand = new OdbcCommand(sSql, m_pConnection);
                OdbcDataReader pReader = pCommand.ExecuteReader();

                if (pReader.Read()) //already got a token, just refresh it
                {
                    m_sOauth_token = pReader["oauth_token"].ToString();
                    m_sOauth_token_secret = pReader["oauth_token_secret"].ToString();
                    m_sOauth_verifier = pReader["oauth_verifier"].ToString();
                    m_sFullOauth_token = pReader["full_oauth_token"].ToString();
                    m_sFullOauth_token_secret = pReader["full_oauth_token_secret"].ToString();
                    m_sOauth_Session_Handle = pReader["oauth_session_handle"].ToString();
                    pReader.Close();
                    bResult = true;
                }
                else //no token, need to create a new one and add it to DB
                {
                    pReader.Close();
                    string sURL = m_pOAuthClass.GenerateGetRequestToken(m_sConsumerKey, m_sSignature, "http://fantasysports.yahoo.com");
                    WebClient myClient = new WebClient();
                    string sContents = myClient.DownloadString(sURL);

                    string[] sSplit = sContents.Split(new char[] { '&' });
                    foreach (string myString in sSplit)
                    {
                        string[] sValues = myString.Split(new char[] { '=' });
                        if (sValues[0] == "oauth_token")
                            m_sOauth_token = sValues[1];
                        if (sValues[0] == "oauth_token_secret")
                            m_sOauth_token_secret = sValues[1];
                    }

                    sSql = "INSERT INTO tblOAuth (oauth_token, oauth_token_secret) " +
                           "VALUES (?, ?)"; //\"" + m_sOauth_token + "\", \"" + m_sOauth_token_secret + "\")";
                    //pCommand = new OleDbCommand(sSql, m_pConnection);
                    pCommand = new OdbcCommand(sSql, m_pConnection);
                    pCommand.CommandType = CommandType.Text;
                    OdbcParameter pParam1 = new OdbcParameter();
                    pParam1.Value = m_sOauth_token;
                    pCommand.Parameters.Add(pParam1);
                    OdbcParameter pParam2 = new OdbcParameter();
                    pParam2.Value = m_sOauth_token_secret;
                    pCommand.Parameters.Add(pParam2);
                    pCommand.ExecuteNonQuery();
                    bResult = false;
                }
            }
            catch (Exception ex)
            {
                clsStatic.ShowError(ex.ToString());
            }
            return bResult;
        }

        void Form1_Disposed(object sender, System.EventArgs e)
        {
            if (m_pConnection.State == ConnectionState.Open)
                m_pConnection.Close();
        }

        private void cmbLeagues_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTeams();
            RefreshMatchups(m_pLeagues[cmbLeagues.SelectedIndex]);
            RefreshScores(m_pLeagues[cmbLeagues.SelectedIndex]);
        }

        private void RefreshScores(YahooLeague pLeague)
        {
            grdScores.Rows.Clear();
            foreach (Matchup myMatchup in m_pMatchups)
            {
                int iAtBats1 = 0, iRuns1 = 0, iHits1 = 0, iHR1 = 0, iRBI1 = 0, iSB1 = 0, iSF1 = 0, iBB1 = 0, iHBP1 = 0;
                int iWin1 = 0, iSave1 = 0, iOuts1 = 0, iHitsAllowed1 = 0, iER1 = 0, iWalks1 = 0, iK1 = 0;
                float fAVG1 = 0, fOBP1 = 0, fERA1 = 0, fWHIP1 = 0;
                string sSql = "SELECT SUM(AtBats) As SumAtBats, SUM(Runs) As SumRuns, SUM(Hits) As SumHits, SUM(HR) As SumHR, SUM(RBI) As SumRBI, SUM(SF) As SumSF, " +
                              "SUM(SB) As SumSB, SUM(BB) As SumBB, SUM(HBP) As SumHBP, SUM(Wins) As SumWins, SUM(Saves) As SumSaves, SUM(Outs) As SumOuts, SUM(HitsAllowed) As SumHitsAllowed, " +
                              "SUM(EarnedRuns) As SumEarnedRuns, SUM(Walks) As SumWalks, SUM(Strikeouts) As SumStrikeouts " +
                              "FROM tblStatistics WHERE (TeamID = ?) AND (StatsDate >= ?) AND (StatsDate <= ?)";

                //OleDbCommand pCommand = new OleDbCommand(sSql, m_pConnection);
                //pCommand.Parameters.Add(new OleDbParameter("@TeamID", myMatchup.Team1));
                //pCommand.Parameters.Add(new OleDbParameter("@StartDate", myMatchup.StartDate.ToShortDateString()));
                //pCommand.Parameters.Add(new OleDbParameter("@EndDate", myMatchup.EndDate.ToShortDateString()));
                OdbcCommand pCommand = new OdbcCommand(sSql, m_pConnection);
                OdbcParameter pParam1 = new OdbcParameter();
                pParam1.Value = myMatchup.Team1;
                pCommand.Parameters.Add(pParam1);
                OdbcParameter pParam2 = new OdbcParameter();
                pParam2.Value = myMatchup.StartDate.ToShortDateString();
                pCommand.Parameters.Add(pParam2);
                OdbcParameter pParam3 = new OdbcParameter();
                pParam3.Value = myMatchup.EndDate.ToShortDateString();
                pCommand.Parameters.Add(pParam3);

                //OleDbDataReader dr = pCommand.ExecuteReader();
                OdbcDataReader dr = pCommand.ExecuteReader();
                if (dr.Read())
                {
                    int.TryParse(dr["SumAtBats"].ToString(), out iAtBats1);
                    int.TryParse(dr["SumRuns"].ToString(), out iRuns1);
                    int.TryParse(dr["SumHits"].ToString(), out iHits1);
                    int.TryParse(dr["SumHR"].ToString(), out iHR1);
                    int.TryParse(dr["SumRBI"].ToString(), out iRBI1);
                    int.TryParse(dr["SumSB"].ToString(), out iSB1);
                    int.TryParse(dr["SumSF"].ToString(), out iSF1);
                    int.TryParse(dr["SumBB"].ToString(), out iBB1);
                    int.TryParse(dr["SumHBP"].ToString(), out iHBP1);
                    int.TryParse(dr["SumWins"].ToString(), out iWin1);
                    int.TryParse(dr["SumSaves"].ToString(), out iSave1);
                    int.TryParse(dr["SumOuts"].ToString(), out iOuts1);
                    int.TryParse(dr["SumHitsAllowed"].ToString(), out iHitsAllowed1);
                    int.TryParse(dr["SumEarnedRuns"].ToString(), out iER1);
                    int.TryParse(dr["SumWalks"].ToString(), out iWalks1);
                    int.TryParse(dr["SumStrikeouts"].ToString(), out iK1);

                    fAVG1 = (float)iHits1 / (float)iAtBats1;
                    fOBP1 = (float)(iHits1 + iBB1 + iHBP1) / (float)(iAtBats1 + iBB1 + iHBP1 + iSF1);
                    if (iOuts1 > 0)
                    {
                        fERA1 = (float)(iER1 * 9) / (float)((float)iOuts1 / (float)3);
                        fWHIP1 = (float)(iWalks1 + iHitsAllowed1) / (float)((float)iOuts1 / (float)3);
                    }
                }
                dr.Close();

                int iAtBats2 = 0, iRuns2 = 0, iHits2 = 0, iHR2 = 0, iRBI2 = 0, iSB2 = 0, iSF2 = 0, iBB2 = 0, iHBP2 = 0;
                int iWin2 = 0, iSave2 = 0, iOuts2 = 0, iHitsAllowed2 = 0, iER2 = 0, iWalks2 = 0, iK2 = 0;
                float fAVG2 = 0, fOBP2 = 0, fERA2 = 0, fWHIP2 = 0;
                //pCommand = new OleDbCommand(sSql, m_pConnection);
                //pCommand.Parameters.Add(new OleDbParameter("@TeamID", myMatchup.Team2));
                //pCommand.Parameters.Add(new OleDbParameter("@StartDate", myMatchup.StartDate.ToShortDateString()));
                //pCommand.Parameters.Add(new OleDbParameter("@EndDate", myMatchup.EndDate.ToShortDateString()));
                pCommand = new OdbcCommand(sSql, m_pConnection);
                pParam1 = new OdbcParameter();
                pParam1.Value = myMatchup.Team2;
                pCommand.Parameters.Add(pParam1);
                pParam2 = new OdbcParameter();
                pParam2.Value = myMatchup.StartDate.ToShortDateString();
                pCommand.Parameters.Add(pParam2);
                pParam3 = new OdbcParameter();
                pParam3.Value = myMatchup.EndDate.ToShortDateString();
                pCommand.Parameters.Add(pParam3);
                //pCommand = new OdbcCommand(sSql, m_pConnection);

                dr = pCommand.ExecuteReader();
                if (dr.Read())
                {
                    int.TryParse(dr["SumAtBats"].ToString(), out iAtBats2);
                    int.TryParse(dr["SumRuns"].ToString(), out iRuns2);
                    int.TryParse(dr["SumHits"].ToString(), out iHits2);
                    int.TryParse(dr["SumHR"].ToString(), out iHR2);
                    int.TryParse(dr["SumRBI"].ToString(), out iRBI2);
                    int.TryParse(dr["SumSB"].ToString(), out iSB2);
                    int.TryParse(dr["SumSF"].ToString(), out iSF2);
                    int.TryParse(dr["SumBB"].ToString(), out iBB2);
                    int.TryParse(dr["SumHBP"].ToString(), out iHBP2);
                    int.TryParse(dr["SumWins"].ToString(), out iWin2);
                    int.TryParse(dr["SumSaves"].ToString(), out iSave2);
                    int.TryParse(dr["SumOuts"].ToString(), out iOuts2);
                    int.TryParse(dr["SumHitsAllowed"].ToString(), out iHitsAllowed2);
                    int.TryParse(dr["SumEarnedRuns"].ToString(), out iER2);
                    int.TryParse(dr["SumWalks"].ToString(), out iWalks2);
                    int.TryParse(dr["SumStrikeouts"].ToString(), out iK2);

                    fAVG2 = (float)iHits2 / (float)iAtBats2;
                    fOBP2 = (float)(iHits2 + iBB2 + iHBP2) / (float)(iAtBats2 + iBB2 + iHBP2 + iSF2);
                    if (iOuts2 > 0)
                    {
                        fERA2 = (float)(iER2 * 9) / (float)((float)iOuts2 / (float)3);
                        fWHIP2 = (float)(iWalks2 + iHitsAllowed2) / (float)((float)iOuts2 / (float)3);
                    }
                }
                dr.Close();

                int iScore1 = 0, iScore2 = 0;
                int[] Team1Wins = new int[11];
                int[] Team2Wins = new int[11];
                if (iRuns1 > iRuns2)
                {
                    iScore1++;
                    Team1Wins[0] = 1;
                }
                else if (iRuns2 > iRuns1)
                {
                    iScore2++;
                    Team2Wins[0] = 1;
                }
                if (iHR1 > iHR2)
                {
                    iScore1++;
                    Team1Wins[1] = 1;
                }
                else if (iHR2 > iHR1)
                {
                    iScore2++;
                    Team2Wins[1] = 1;
                }
                if (iRBI1 > iRBI2)
                {
                    iScore1++;
                    Team1Wins[2] = 1;
                }
                else if (iRBI2 > iRBI1)
                {
                    iScore2++;
                    Team2Wins[2] = 1;
                }
                if (iSB1 > iSB2)
                {
                    iScore1++;
                    Team1Wins[3] = 1;
                }
                else if (iSB2 > iSB1)
                {
                    iScore2++;
                    Team2Wins[3] = 1;
                }
                if (fAVG1 > fAVG2)
                {
                    iScore1++;
                    Team1Wins[4] = 1;
                }
                else if (fAVG2 > fAVG1)
                {
                    iScore2++;
                    Team2Wins[4] = 1;
                }
                if (fOBP1 > fOBP2)
                {
                    iScore1++;
                    Team1Wins[5] = 1;
                }
                else if (fOBP2 > fOBP1)
                {
                    iScore2++;
                    Team2Wins[5] = 1;
                }
                if (iWin1 > iWin2)
                {
                    iScore1++;
                    Team1Wins[6] = 1;
                }
                else if (iWin2 > iWin1)
                {
                    iScore2++;
                    Team2Wins[6] = 1;
                }                
                if (iSave1 > iSave2)
                {
                    iScore1++;
                    Team1Wins[7] = 1;
                }
                else if (iSave2 > iSave1)
                {
                    iScore2++;
                    Team2Wins[7] = 1;
                }
                if (iK1 > iK2)
                {
                    iScore1++;
                    Team1Wins[8] = 1;
                }
                else if (iK2 > iK1)
                {
                    iScore2++;
                    Team2Wins[8] = 1;
                }
                if (fERA1 < fERA2)
                {
                    iScore1++;
                    Team1Wins[9] = 1;
                }
                else if (fERA2 < fERA1)
                {
                    iScore2++;
                    Team2Wins[9] = 1;
                }
                if (fWHIP1 < fWHIP2)
                {
                    iScore1++;
                    Team1Wins[10] = 1;
                }
                else if (fWHIP2 < fWHIP1)
                {
                    iScore2++;
                    Team2Wins[10] = 1;
                }

                int iRow = grdScores.Rows.Add(myMatchup.ID, pLeague.Teams[myMatchup.Team1 - 1].TeamName, iRuns1, iHR1, iRBI1, iSB1, fAVG1.ToString("F3"), fOBP1.ToString("F3"), iWin1, iSave1, iK1, fERA1.ToString("F2"), fWHIP1.ToString("F2"), iScore1);
                for (int n = 0; n < 11; n++)
                {
                    if (Team1Wins[n] == 1)
                    {
                        DataGridViewCell myCell = grdScores.Rows[iRow].Cells[n + 2];
                        myCell.Style.Font = new Font(myCell.InheritedStyle.Font, FontStyle.Bold);
                    }
                }
                iRow = grdScores.Rows.Add(myMatchup.ID, pLeague.Teams[myMatchup.Team2 - 1].TeamName, iRuns2, iHR2, iRBI2, iSB2, fAVG2.ToString("F3"), fOBP2.ToString("F3"), iWin2, iSave2, iK2, fERA2.ToString("F2"), fWHIP2.ToString("F2"), iScore2);
                for (int n = 0; n < 11; n++)
                {
                    if (Team2Wins[n] == 1)
                    {
                        DataGridViewCell myCell = grdScores.Rows[iRow].Cells[n + 2];
                        myCell.Style.Font = new Font(myCell.InheritedStyle.Font, FontStyle.Bold);
                    }
                }
            }
        }

        private void GetTeams()
        {
            string sLeagueID = m_pLeagues[cmbLeagues.SelectedIndex].LeagueId.ToString();
            string sURL = m_pOAuthClass.GenerateURL("http://fantasysports.yahooapis.com/fantasy/v2/league/mlb.l." + sLeagueID + "/teams", m_sConsumerKey, m_sSignature, m_sFullOauth_token, m_sFullOauth_token_secret);
            WebClient myClient = new WebClient();
            string sContents = myClient.DownloadString(sURL);
            m_pLeagues[cmbLeagues.SelectedIndex].ListTeams(sContents);
        }

        private void btnNewMatchup_Click(object sender, EventArgs e)
        {
            YahooLeague myLeague = m_pLeagues[cmbLeagues.SelectedIndex];
            frmAddMatchup f = new frmAddMatchup();
            f.League = myLeague;
            if (f.ShowDialog() == DialogResult.OK)
            {
                AddMatchup(myLeague.LeagueId, f.Team1, f.Team2, f.StartDate, f.EndDate);
                RefreshMatchups(myLeague);
            }
        }

        private void RefreshMatchups(YahooLeague pLeague)
        {
            try
            {
                m_pMatchups.Clear();
                grdMatchups.Rows.Clear();
                string sSql = "SELECT * FROM tblMatchups WHERE LeagueID = ? ORDER BY ID ASC";
                //OleDbCommand pCommand = new OleDbCommand(sSql, m_pConnection);
                OdbcCommand pCommand = new OdbcCommand(sSql, m_pConnection);
                //pCommand.Parameters.Add(new OleDbParameter("@LeagueID", pLeague.LeagueId));
                OdbcParameter pParam = new OdbcParameter();
                pParam.Value = pLeague.LeagueId;
                pCommand.Parameters.Add(pParam);
                //OleDbDataReader dr = pCommand.ExecuteReader();
                OdbcDataReader dr = pCommand.ExecuteReader();
                while (dr.Read())
                {
                    string sID = dr["ID"].ToString();
                    string sTeam1 = pLeague.Teams[Convert.ToInt32(dr["Team1"]) - 1].TeamName;
                    string sTeam2 = pLeague.Teams[Convert.ToInt32(dr["Team2"]) - 1].TeamName;
                    string sStart = Convert.ToDateTime(dr["StartDate"].ToString()).ToShortDateString();
                    string sEnd = Convert.ToDateTime(dr["EndDate"].ToString()).ToShortDateString();
                    grdMatchups.Rows.Add(sID, sTeam1, sTeam2, sStart, sEnd);

                    Matchup myMatchup = new Matchup();
                    myMatchup.ID = Convert.ToInt32(sID);
                    myMatchup.Team1 = Convert.ToInt32(dr["Team1"]);
                    myMatchup.Team2 = Convert.ToInt32(dr["Team2"]);
                    myMatchup.StartDate = Convert.ToDateTime(dr["StartDate"]);
                    myMatchup.EndDate = Convert.ToDateTime(dr["EndDate"]);
                    m_pMatchups.Add(myMatchup);
                }
                dr.Close();

                cmbMatchups.Items.Clear();
                foreach (Matchup thisMatchup in m_pMatchups)
                    cmbMatchups.Items.Add(pLeague.Teams[thisMatchup.Team1 - 1].TeamName + " vs. " + pLeague.Teams[thisMatchup.Team2 - 1].TeamName);

                if (cmbMatchups.Items.Count > 0)
                    cmbMatchups.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                clsStatic.ShowError(ex.ToString());
            }
        }

        private void AddMatchup(int iLeagueID, int iTeam1, int iTeam2, DateTime dteStart, DateTime dteEnd)
        {
            try
            {
                string sSql = "INSERT INTO tblMatchups (LeagueID, Team1, Team2, StartDate, EndDate)" +
                               "VALUES (?, ?, ?, ?, ?)";
                //OleDbCommand pCommand = new OleDbCommand(sSql, m_pConnection);
                //pCommand.Parameters.Add(new OleDbParameter("@LeagueID", iLeagueID));
                //pCommand.Parameters.Add(new OleDbParameter("@Team1", iTeam1));
                //pCommand.Parameters.Add(new OleDbParameter("@Team2", iTeam2));
                //pCommand.Parameters.Add(new OleDbParameter("@StartDate", dteStart.ToShortDateString()));
                //pCommand.Parameters.Add(new OleDbParameter("@EndDate", dteEnd.ToShortDateString()));
                OdbcCommand pCommand = new OdbcCommand(sSql, m_pConnection);
                OdbcParameter pParam = new OdbcParameter();
                pParam.Value = iLeagueID;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iTeam1;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iTeam2;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = dteStart.ToShortDateString();
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = dteEnd.ToShortDateString();
                pCommand.Parameters.Add(pParam);

                pCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsStatic.ShowError(ex.ToString());
            }
        }

        private void dataGridView1_RowsRemoved(object sender, System.Windows.Forms.DataGridViewRowsRemovedEventArgs e)
        {/*
            int iID = Convert.ToInt32(grdMatchups.Rows[e.RowIndex].Cells[0].Value);
            string sSql = "DELETE FROM tblMatchups WHERE ID = @ID";
            OleDbCommand pCommand = new OleDbCommand(sSql, m_pConnection);
            pCommand.Parameters.Add(new OleDbParameter("@ID", iID));
            pCommand.ExecuteNonQuery();

            RefreshMatchups(m_pLeagues[cmbLeagues.SelectedIndex]);*/
        }

        private void UpdateMatchups()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                YahooLeague myLeague = m_pLeagues[cmbLeagues.SelectedIndex];
                List<DateTime> aPreviousDates = GetStatsDates(myLeague);
                string sSql = "SELECT * FROM tblMatchups WHERE LeagueID = ?";
                //OleDbCommand pCommand = new OleDbCommand(sSql, m_pConnection);
                //pCommand.Parameters.Add(new OleDbParameter("@LeagueID", myLeague.LeagueId));
                //OleDbDataReader dr = pCommand.ExecuteReader();
                OdbcCommand pCommand = new OdbcCommand(sSql, m_pConnection);
                OdbcParameter pParam = new OdbcParameter();
                pParam.Value = myLeague.LeagueId;
                pCommand.Parameters.Add(pParam);
                OdbcDataReader dr = pCommand.ExecuteReader();
                DateTime NowDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                while (dr.Read())
                {
                    int iTeam1 = Convert.ToInt32(dr["Team1"]);
                    int iTeam2 = Convert.ToInt32(dr["Team2"]);
                    DateTime startDate = Convert.ToDateTime(dr["StartDate"]);
                    DateTime endDate = Convert.ToDateTime(dr["EndDate"]);
                    TimeSpan mySpan = endDate.Subtract(startDate);                    
                    for (int i = 0; i <= mySpan.TotalDays; i++)
                    {
                        DateTime thisDate = startDate.AddDays(i);
                        if ((thisDate < NowDate) && (!(aPreviousDates.Contains(thisDate))))
                        {
                            string sURL = m_pOAuthClass.GenerateURL("http://fantasysports.yahooapis.com/fantasy/v2/team/mlb.l." + myLeague.LeagueId.ToString() + ".t." + iTeam1.ToString() + "/roster;date=" + thisDate.ToString("yyyy-MM-dd"), m_sConsumerKey, m_sSignature, m_sFullOauth_token, m_sFullOauth_token_secret);
                            WebClient myClient = new WebClient();
                            string sContents = myClient.DownloadString(sURL);
                            List<int> aTeam1Players = GetPlayerIDs(sContents);
                            UpdateStatistics(myLeague, iTeam1, aTeam1Players, thisDate);

                            sURL = m_pOAuthClass.GenerateURL("http://fantasysports.yahooapis.com/fantasy/v2/team/mlb.l." + myLeague.LeagueId.ToString() + ".t." + iTeam2.ToString() + "/roster;date=" + thisDate.ToString("yyyy-MM-dd"), m_sConsumerKey, m_sSignature, m_sFullOauth_token, m_sFullOauth_token_secret);
                            myClient = new WebClient();
                            sContents = myClient.DownloadString(sURL);
                            List<int> aTeam2Players = GetPlayerIDs(sContents);
                            UpdateStatistics(myLeague, iTeam2, aTeam2Players, thisDate);
                        }
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                clsStatic.ShowError(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void UpdateStatistics(YahooLeague myLeague, int iTeam, List<int> aPlayers, DateTime currentDate)
        {
            XmlDocument oDoc = new XmlDocument();
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(oDoc.NameTable);
            nsmgr.AddNamespace("yahoo", "http://fantasysports.yahooapis.com/fantasy/v2/base.rng");
            foreach (int myPlayer in aPlayers)
            {
                int iAtBats = 0, iRuns = 0, iHits = 0, iHR = 0, iRBI = 0, iSB = 0, iSF = 0, iBB = 0, iHBP = 0;
                int iWin = 0, iSave = 0, iOuts = 0, iHitsAllowed = 0, iER = 0, iWalks = 0, iK = 0;
                string sURL = m_pOAuthClass.GenerateURL("http://fantasysports.yahooapis.com/fantasy/v2/player/mlb.p." + myPlayer.ToString() + "/stats;type=date;date=" + currentDate.ToString("yyyy-MM-dd"), m_sConsumerKey, m_sSignature, m_sFullOauth_token, m_sFullOauth_token_secret);
                WebClient myClient = new WebClient();
                string sContents = myClient.DownloadString(sURL);
                oDoc.LoadXml(sContents);
                XmlElement oRootNode = (XmlElement)oDoc.ChildNodes[1]; //(XmlElement)oDoc.SelectSingleNode("/fantasy_content/users/user/games/game/leagues");                
                XmlElement oStats = (XmlElement)oRootNode.SelectSingleNode("descendant::yahoo:player/yahoo:player_stats/yahoo:stats", nsmgr);
                if (oStats != null)
                {
                    XmlNodeList oStatList = oStats.SelectNodes("descendant::yahoo:stat", nsmgr);
                    foreach (XmlElement myElement in oStatList)
                    {
                        XmlElement oStat = (XmlElement)myElement.SelectSingleNode("descendant::yahoo:stat_id", nsmgr);
                        XmlElement oValue = (XmlElement)myElement.SelectSingleNode("descendant::yahoo:value", nsmgr);
                        if (oValue.InnerText != "-")
                        {
                            if (oStat.InnerText == "6")
                                iAtBats = Convert.ToInt32(oValue.InnerText);
                            else if (oStat.InnerText == "7")
                                iRuns = Convert.ToInt32(oValue.InnerText);
                            else if (oStat.InnerText == "8")
                                iHits = Convert.ToInt32(oValue.InnerText);
                            else if (oStat.InnerText == "12")
                                iHR = Convert.ToInt32(oValue.InnerText);
                            else if (oStat.InnerText == "13")
                                iRBI = Convert.ToInt32(oValue.InnerText);
                            else if (oStat.InnerText == "15")
                                iSF = Convert.ToInt32(oValue.InnerText);
                            else if (oStat.InnerText == "16")
                                iSB = Convert.ToInt32(oValue.InnerText);
                            else if (oStat.InnerText == "18")
                                iBB = Convert.ToInt32(oValue.InnerText);
                            else if (oStat.InnerText == "20")
                                iHBP = Convert.ToInt32(oValue.InnerText);
                            else if (oStat.InnerText == "28")
                                iWin = Convert.ToInt32(oValue.InnerText);
                            else if (oStat.InnerText == "32")
                                iSave = Convert.ToInt32(oValue.InnerText);
                            else if (oStat.InnerText == "33")
                                iOuts = Convert.ToInt32(oValue.InnerText);
                            else if (oStat.InnerText == "34")
                                iHitsAllowed = Convert.ToInt32(oValue.InnerText);
                            else if (oStat.InnerText == "37")
                                iER = Convert.ToInt32(oValue.InnerText);
                            else if (oStat.InnerText == "39")
                                iWalks = Convert.ToInt32(oValue.InnerText);
                            else if (oStat.InnerText == "42")
                                iK = Convert.ToInt32(oValue.InnerText);
                        }
                    }
                }

                string sSql = "INSERT INTO tblStatistics (LeagueID, TeamID, PlayerID, StatsDate, AtBats, " +
                              "Runs, Hits, HR, RBI, SB, SF, BB, HBP, Wins, Saves, Outs, HitsAllowed, " +
                              "EarnedRuns, Walks, Strikeouts) VALUES (?, ?, " +
                              "?, ?, ?, ?, ?, ?, ?, ?, ?, " +
                              "?, ?, ?, ?, ?, ?, ?, ?, ?)";
                //OleDbCommand pCommand = new OleDbCommand(sSql, m_pConnection);
                //pCommand.Parameters.Add(new OleDbParameter("@LeagueID", myLeague.LeagueId));
                //pCommand.Parameters.Add(new OleDbParameter("@TeamID", iTeam));
                //pCommand.Parameters.Add(new OleDbParameter("@PlayerID", myPlayer));
                //pCommand.Parameters.Add(new OleDbParameter("@StatsDate", currentDate.ToShortDateString()));
                //pCommand.Parameters.Add(new OleDbParameter("@AtBats", iAtBats));
                //pCommand.Parameters.Add(new OleDbParameter("@Runs", iRuns));
                //pCommand.Parameters.Add(new OleDbParameter("@Hits", iHits));
                //pCommand.Parameters.Add(new OleDbParameter("@HR", iHR));
                //pCommand.Parameters.Add(new OleDbParameter("@RBI", iRBI));
                //pCommand.Parameters.Add(new OleDbParameter("@SB", iSB));
                //pCommand.Parameters.Add(new OleDbParameter("@SF", iSF));
                //pCommand.Parameters.Add(new OleDbParameter("@BB", iBB));
                //pCommand.Parameters.Add(new OleDbParameter("@HBP", iHBP));
                //pCommand.Parameters.Add(new OleDbParameter("@Wins", iWin));
                //pCommand.Parameters.Add(new OleDbParameter("@Saves", iSave));
                //pCommand.Parameters.Add(new OleDbParameter("@Outs", iOuts));
                //pCommand.Parameters.Add(new OleDbParameter("@HitsAllowed", iHitsAllowed));
                //pCommand.Parameters.Add(new OleDbParameter("@EarnedRuns", iER));
                //pCommand.Parameters.Add(new OleDbParameter("@Walks", iWalks));
                //pCommand.Parameters.Add(new OleDbParameter("@Strikeouts", iK));
                OdbcCommand pCommand = new OdbcCommand(sSql, m_pConnection);
                OdbcParameter pParam = new OdbcParameter();
                pParam.Value = myLeague.LeagueId;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iTeam;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = myPlayer;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = currentDate.ToShortDateString();
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iAtBats;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iRuns;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iHits;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iHR;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iRBI;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iSB;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iSF;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iBB;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iHBP;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iWin;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iSave;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iOuts;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iHitsAllowed;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iER;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iWalks;
                pCommand.Parameters.Add(pParam);
                pParam = new OdbcParameter();
                pParam.Value = iK;
                pCommand.Parameters.Add(pParam);

                pCommand.ExecuteNonQuery();
            }
        }

        private List<int> GetPlayerIDs(string sContents)
        {
            List<int> aResult = new List<int>();
            List<string> aPositions = GetPositions();
            XmlDocument oDoc = new XmlDocument();
            oDoc.LoadXml(sContents);
            XmlElement oRootNode = (XmlElement)oDoc.ChildNodes[1]; //(XmlElement)oDoc.SelectSingleNode("/fantasy_content/users/user/games/game/leagues");
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(oDoc.NameTable);
            nsmgr.AddNamespace("yahoo", "http://fantasysports.yahooapis.com/fantasy/v2/base.rng");
            XmlElement oPlayers = (XmlElement)oRootNode.SelectSingleNode("descendant::yahoo:team/yahoo:roster/yahoo:players", nsmgr);
            if (oPlayers != null)
            {
                XmlNodeList oList = oPlayers.SelectNodes("descendant::yahoo:player", nsmgr);
                foreach (XmlElement myPlayer in oList)
                {
                    XmlElement oPosition = (XmlElement)myPlayer.SelectSingleNode("descendant::yahoo:selected_position/yahoo:position", nsmgr);
                    if (oPosition != null)
                    {
                        if (aPositions.Contains(oPosition.InnerText))
                        {
                            XmlElement oID = (XmlElement)myPlayer.SelectSingleNode("descendant::yahoo:player_id", nsmgr);
                            aResult.Add(Convert.ToInt32(oID.InnerText));
                        }
                    }
                }
            }

            return aResult;
        }

        private List<string> GetPositions()
        {
            List<string> aPositions = new List<string>();
            aPositions.Add("C");
            aPositions.Add("1B");
            aPositions.Add("2B");
            aPositions.Add("3B");
            aPositions.Add("SS");
            aPositions.Add("OF");
            aPositions.Add("Util");
            aPositions.Add("SP");
            aPositions.Add("RP");
            return aPositions;
        }

        private List<DateTime> GetStatsDates(YahooLeague pLeague)
        {
            List<DateTime> aResult = new List<DateTime>();
            string sSql = "SELECT DISTINCT StatsDate FROM tblStatistics WHERE LeagueID = ?";
            //OleDbCommand pCommand = new OleDbCommand(sSql, m_pConnection);
            //pCommand.Parameters.Add(new OleDbParameter("@LeagueID", pLeague.LeagueId));
            //OleDbDataReader dr = pCommand.ExecuteReader();
            OdbcCommand pCommand = new OdbcCommand(sSql, m_pConnection);
            OdbcParameter pParam = new OdbcParameter();
            pParam.Value = pLeague.LeagueId;
            pCommand.Parameters.Add(pParam);
            OdbcDataReader dr = pCommand.ExecuteReader();
            while (dr.Read())
            {
                aResult.Add(Convert.ToDateTime(dr["StatsDate"]));
            }
            dr.Close();

            return aResult;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateMatchups();
            RefreshScores(m_pLeagues[cmbLeagues.SelectedIndex]);
            RefreshDaily(m_pLeagues[cmbLeagues.SelectedIndex]);
        }

        private void cmbMatchups_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDaily(m_pLeagues[cmbLeagues.SelectedIndex]);
        }

        private void RefreshDaily(YahooLeague pLeague)
        {
            if (grdDaily.Rows.Count > 0)
                grdDaily.Rows.Clear();
            if (cmbMatchups.SelectedIndex > -1)
            {
                Matchup myMatchup = m_pMatchups[cmbMatchups.SelectedIndex];
                AddDailyScores(pLeague, myMatchup.ID, myMatchup.Team1, myMatchup.StartDate, myMatchup.EndDate);
                AddDailyScores(pLeague, myMatchup.ID, myMatchup.Team2, myMatchup.StartDate, myMatchup.EndDate);
            }
        }

        private void AddDailyScores(YahooLeague pLeague, int iID, int iTeam, DateTime dteStartDate, DateTime dteEndDate)
        {
            try
            {
                TimeSpan mySpan = dteEndDate.Subtract(dteStartDate);
                for (int i = 0; i <= mySpan.TotalDays; i++)
                {
                    DateTime currDate = dteStartDate.AddDays(i);
                    int iAtBats1 = 0, iRuns1 = 0, iHits1 = 0, iHR1 = 0, iRBI1 = 0, iSB1 = 0, iSF1 = 0, iBB1 = 0, iHBP1 = 0;
                    int iWin1 = 0, iSave1 = 0, iOuts1 = 0, iHitsAllowed1 = 0, iER1 = 0, iWalks1 = 0, iK1 = 0;
                    float fAVG1 = 0, fOBP1 = 0, fERA1 = 0, fWHIP1 = 0;
                    string sSql = "SELECT SUM(AtBats) As SumAtBats, SUM(Runs) As SumRuns, SUM(Hits) As SumHits, SUM(HR) As SumHR, SUM(RBI) As SumRBI, SUM(SF) As SumSF, " +
                                  "SUM(SB) As SumSB, SUM(BB) As SumBB, SUM(HBP) As SumHBP, SUM(Wins) As SumWins, SUM(Saves) As SumSaves, SUM(Outs) As SumOuts, SUM(HitsAllowed) As SumHitsAllowed, " +
                                  "SUM(EarnedRuns) As SumEarnedRuns, SUM(Walks) As SumWalks, SUM(Strikeouts) As SumStrikeouts " +
                                  "FROM tblStatistics WHERE (TeamID = ?) AND (StatsDate = ?)";

                    //OleDbCommand pCommand = new OleDbCommand(sSql, m_pConnection);
                    //pCommand.Parameters.Add(new OleDbParameter("@TeamID", iTeam));
                    //pCommand.Parameters.Add(new OleDbParameter("@StartDate", currDate.ToShortDateString()));
                    OdbcCommand pCommand = new OdbcCommand(sSql, m_pConnection);
                    OdbcParameter pParam = new OdbcParameter();
                    pParam.Value = iTeam;
                    pCommand.Parameters.Add(pParam);
                    pParam = new OdbcParameter();
                    pParam.Value = currDate.ToShortDateString();
                    pCommand.Parameters.Add(pParam);

                    //OleDbDataReader dr = pCommand.ExecuteReader();
                    OdbcDataReader dr = pCommand.ExecuteReader();
                    if (dr.Read())
                    {
                        int.TryParse(dr["SumAtBats"].ToString(), out iAtBats1);
                        int.TryParse(dr["SumRuns"].ToString(), out iRuns1);
                        int.TryParse(dr["SumHits"].ToString(), out iHits1);
                        int.TryParse(dr["SumHR"].ToString(), out iHR1);
                        int.TryParse(dr["SumRBI"].ToString(), out iRBI1);
                        int.TryParse(dr["SumSB"].ToString(), out iSB1);
                        int.TryParse(dr["SumSF"].ToString(), out iSF1);
                        int.TryParse(dr["SumBB"].ToString(), out iBB1);
                        int.TryParse(dr["SumHBP"].ToString(), out iHBP1);
                        int.TryParse(dr["SumWins"].ToString(), out iWin1);
                        int.TryParse(dr["SumSaves"].ToString(), out iSave1);
                        int.TryParse(dr["SumOuts"].ToString(), out iOuts1);
                        int.TryParse(dr["SumHitsAllowed"].ToString(), out iHitsAllowed1);
                        int.TryParse(dr["SumEarnedRuns"].ToString(), out iER1);
                        int.TryParse(dr["SumWalks"].ToString(), out iWalks1);
                        int.TryParse(dr["SumStrikeouts"].ToString(), out iK1);

                        fAVG1 = (float)iHits1 / (float)iAtBats1;
                        fOBP1 = (float)(iHits1 + iBB1 + iHBP1) / (float)(iAtBats1 + iBB1 + iHBP1 + iSF1);
                        if (iOuts1 > 0)
                        {
                            fERA1 = (float)(iER1 * 9) / (float)((float)iOuts1 / (float)3);
                            fWHIP1 = (float)(iWalks1 + iHitsAllowed1) / (float)((float)iOuts1 / (float)3);
                        }
                    }
                    dr.Close();

                    grdDaily.Rows.Add(iID, pLeague.Teams[iTeam - 1].TeamName, currDate.ToShortDateString(), iRuns1, iHR1, iRBI1, iSB1, fAVG1.ToString("F3"), fOBP1.ToString("F3"), iWin1, iSave1, iK1, fERA1.ToString("F2"), fWHIP1.ToString("F2"));
                }
            }
            catch (Exception ex)
            {
                clsStatic.ShowError(ex.ToString());
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Filter = "Comma Delimited Files(*.csv)|*.csv";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ExportMatchups(ofd.FileName, m_pLeagues[cmbLeagues.SelectedIndex]);
            }
        }

        private void ExportMatchups(string sFileName, YahooLeague pLeague)
        {
            StringBuilder sb = new StringBuilder();
            if (grdScores.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataGridViewColumn myColumn in grdScores.Columns)
                {
                    if (!(myColumn.HeaderText.Contains(",")))
                        sb.Append(myColumn.HeaderText + ",");
                    else
                        sb.Append(String.Format("\"{0}\",", myColumn.HeaderText));
                    if (i == 1)
                        sb.Append("Date,");
                    i++;
                }
                if (sb.Length > 0)
                    sb.Remove(sb.Length - 1, 1);
                sb.Append(Environment.NewLine);

                for (int j = 0; j < m_pMatchups.Count; j++)
                {
                    cmbMatchups.SelectedIndex = j;
                    foreach (DataGridViewRow myRow in grdDaily.Rows)
                    {
                        if (!(myRow.IsNewRow))
                        {
                            foreach (DataGridViewCell myCell in myRow.Cells)
                            {
                                if (!(myCell.Value.ToString().Contains(",")))
                                    sb.Append(myCell.Value.ToString() + ",");
                                else
                                    sb.Append(String.Format("\"{0}\",", myCell.Value.ToString()));
                            }
                        }
                        if (sb.Length > 0)
                            sb.Remove(sb.Length - 1, 1);
                        sb.Append(Environment.NewLine);
                    }                                        
                }

                foreach (DataGridViewRow myRow in grdScores.Rows)
                {
                    if (!(myRow.IsNewRow))
                    {
                        i = 0;
                        foreach (DataGridViewCell myCell in myRow.Cells)
                        {
                            if (!(myCell.Value.ToString().Contains(",")))
                                sb.Append(myCell.Value.ToString() + ",");
                            else
                                sb.Append(String.Format("\"{0}\",", myCell.Value.ToString()));
                            if (i == 1)
                                sb.Append("Total,");
                            i++;
                        }
                    }
                    if (sb.Length > 0)
                        sb.Remove(sb.Length - 1, 1);
                    sb.Append(Environment.NewLine);
                }

                using (TextWriter tw = new StreamWriter(sFileName))
                {
                    tw.Write(sb.ToString());
                }
            }
        }
    }
}

