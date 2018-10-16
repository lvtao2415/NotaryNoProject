using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NotaryNoProject.GloBal
{
    public class HelpData
    {
        public static DataTable GetProjectLables(string projectid)
        {
            string command = string.Format(@"select * from ProjectLable where DelStaus=0 and ProjectID={0} ", projectid);
            return HelpSQL.Search(command.ToString());
        }


    }
}
