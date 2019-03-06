using Natterbase.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Natterbase.Utility
{
    public class LogAudit
    {

        public static string Log(string action, string user_id, byte status)
        {
            NatterbaseEntities db = new NatterbaseEntities();

            audit audit = new audit {
                action = action,
                user_id = user_id,
                status = status
            };
            
            db.audits.Add(audit);
            db.SaveChanges();

            return "Log";
        }
    }
}