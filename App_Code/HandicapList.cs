using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Handicaps
/// </summary>
public class Handicap
{
        public string ID { get; set; }
        public string Name { get; set;}
        public string Index { get; set; }
        public DateTime Date { get; set; }

        public Handicap(string id, string name, string index, DateTime date)
        {
            ID = id;
            Name = name;
            Index = index;
            Date = date;
        }
        public Handicap() { }
        public static string Filename {get; set;}
        public static string DisplayPath { get; set; }
        private static readonly char[] delimiterChars = { ',' };
        private static string _filename;
        private static string _source;
        private static DateTime _hcpDate;

        public static string GetFilename(Settings clubSettings)
        {
            string path = HttpContext.Current.Server.MapPath("");
            string url = "~\\App_Data\\" + clubSettings.ClubID + "-handicaps.csv";
            _filename = HttpContext.Current.Server.MapPath(url);
            string[] fn = _filename.Split('\\');
            string newPath = "..";
            for (int i = 3; i > 0; i--)
            {
                newPath = newPath + "\\" + fn[fn.Length - i];
            }
            DisplayPath = newPath;
            Filename = _filename;
            return _filename;

        }

        public static DateTime GetHandicapDate(string filename)
        {
            DateTime createTime;
            if (System.IO.File.Exists(filename))
            {
                createTime = System.IO.File.GetLastWriteTime(filename);
            }
            else
            {
                MrTimeZone tz = new MrTimeZone();
                createTime = tz.eastTimeNow();
            }
            int hday = 1;
            if (createTime.Day > 14) hday = 15;
            DateTime handicapDate = new DateTime(createTime.Year, createTime.Month, hday);
            return handicapDate;

        }
        public static string Source(Settings clubSettings)
        {
            string hs = "Maryland State Golf Association Handicap System";
            string fn = Handicap.GetFilename(clubSettings);

            if (System.IO.File.Exists(fn))
            {
                hs = string.Format("File = {0}", Handicap.DisplayPath);
            }
            return "SOURCE: "+hs;
        }

        public static List<Handicap> LoadHandicaps(string MSGAClubID, string fileName, DateTime hcpDate)
        {
            _filename = fileName;
            _hcpDate = hcpDate;

            List<Handicap> clubHcps = new List<Handicap>();
            if (System.IO.File.Exists(_filename))
            {
                clubHcps = GetHandicapsFromFile(_filename, _hcpDate);
            }
            else
            {
                clubHcps = GetHandicapsFromMSGAService(MSGAClubID, _hcpDate);
            }
            var lst = from p in clubHcps
                      orderby p.ID
                      select new { p.ID, p.Name, p.Index, p.Date };
            List<Handicap> rst = new List<Handicap>();
            foreach (var entry in lst)
            {
                Handicap h = new Handicap()
                {
                    ID = entry.ID,
                    Name = entry.Name,
                    Index = entry.Index,
                    Date = entry.Date
                };
                rst.Add(h);
            }
            return rst;
        }

        public static List<Handicap> GetHandicapsFromFile(string fileName, DateTime hcpDate)
        {
            string _filename = fileName;
            DateTime _hcpDate = hcpDate;

            List<Handicap> hcpTarget = new List<Handicap>();
            string[] lines = System.IO.File.ReadAllLines(_filename);
            string prev = "";
            foreach (String line in lines)
            {
                if (line.Length > 0)
                {
                    string[] fields = line.Split(delimiterChars);
                    if (fields.Length > 7)
                    {
                        if (fields[4] != prev)
                        {
                            if (fields[8].Trim() != "CourseName1")
                            {
                                prev = fields[4];
                                Handicap e = new Handicap()
                                {
                                    ID = fields[4].Trim(),
                                    Date = _hcpDate
                                };
//                               e.Index = "NONE";
                                e.Index = ToHcp(fields[8].Trim());
                                string name = fields[6].Trim().ToUpper() + ", " + fields[7].Trim().ToUpper();
                                e.Name = name.Substring(1, (name.Length - 2));

                                hcpTarget.Add(e);
                            }
                        }
                    }
                }
            }
            return hcpTarget;
        }


        private static string ToHcp(string h)
        {
            string rst = "";
            foreach (char x in h)
            {
                if ((x == ' ') || (x == 65533))
                {
                }
                else
                {
                    rst += x;
                }
            }
            return rst;
        }

        public static List<Handicap> GetHandicapsFromMSGAService(string Club, DateTime hcpDate)
        {
            List<Handicap> hcpTarget = new List<Handicap>();

            var client = new MSGAServiceReference.MemberServiceClient();
            MSGAServiceReference.Credentials mscred = new MSGAServiceReference.Credentials();
            MSGAServiceReference.FetchMembersByClubRequest msreq = new MSGAServiceReference.FetchMembersByClubRequest();
            MSGAServiceReference.FetchMembersResponse msresp = new MSGAServiceReference.FetchMembersResponse();

            mscred.Username = "MisgaService";
            mscred.Password = "M1sga$3rv1c3";
            mscred.Source = "MISGA";

            msreq.Credentials = mscred;
            msreq.SourceClubId = Club;
            msreq.FromDate = new DateTime(2010, 1, 27);
            msreq.ActiveOnly = true;

            msresp = client.FetchMembersByClub(msreq);

            int numberOfMembers = msresp.MemberList.Count();
            MrTimeZone tz = new MrTimeZone();
            DateTime nullDate = tz.eastTimeNow().AddMonths(-2);

            for (var i = 1; i < numberOfMembers; i++)
            {
                //                string active = (msresp.MemberList[i].IsActive) ? "        " : "Inactive";
                //                string name = msresp.MemberList[i].LastName + ", " + msresp.MemberList[i].FirstName + " [" + msresp.MemberList[i].FriendlyId + "] " + active;
                //                string msg = string.Format("{2} - {0}; {1} {3}<br/>\n", msresp.MemberList[i].NetworkId, name, i, msresp.MemberList[i].HandicapInfo[0].DisplayHandicap);

                Handicap e = new Handicap();
                e.ID = msresp.MemberList[i].NetworkId.ToString();
                e.Name = msresp.MemberList[i].LastName.ToUpper() + ", " + msresp.MemberList[i].FirstName.ToUpper();
                try
                {
                    e.Index = msresp.MemberList[i].HandicapInfo[0].DisplayHandicap;
                    e.Date = Convert.ToDateTime(msresp.MemberList[i].HandicapInfo[0].EffectiveOn);
                }
                catch
                {
                    e.Index = "null";
                    e.Date =nullDate;
                }
                hcpTarget.Add(e);
            }

            return hcpTarget;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}",ID, Name, Index, Date.ToString());
        }
    }
 