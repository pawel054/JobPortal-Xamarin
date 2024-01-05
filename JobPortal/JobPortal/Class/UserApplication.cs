using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Class
{
    public class UserApplication
    {
        public int ID { get; set; }
        public int userID { get; set; }
        public int offerID { get; set; }
        public string Status { get; set; }
        public string PositionName { get; set; }

        public UserApplication(int iD, int userID, int offerID, string status)
        {
            ID = iD;
            this.userID = userID;
            this.offerID = offerID;
            Status = status;
        }

        public UserApplication(int userID, int offerID, string status)
        {
            this.userID = userID;
            this.offerID = offerID;
            Status = status;
        }

        public UserApplication(int iD, int userID, int offerID, string status, string PositionName)
        {
            ID = iD;
            this.userID = userID;
            this.offerID = offerID;
            Status = status;
            this.PositionName = PositionName;
        }
    }
}
