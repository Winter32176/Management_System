using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.SessionStart
{
    class SessionStart
    {
        private string sessionID;
        static private int userID=0;
        private string userName;
        private DateTime logTime;

        public string SessionID { get { return this.sessionID; }}
        public int UserID { get { return userID; } }
        public string UserName { get { return this.userName; } }
        public DateTime LogTime { get { return this.logTime; } }

        public SessionStart(string userName)
        { 
            ++userID;
            sessionID = userName+ userID + ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
            this.userName = userName;
            this.logTime = DateTime.Now;
        }


    }
}
