using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ1
{
    class myDatabase
    {
        public String conSt = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=QLTVS;Integrated Security=True";
        SqlConnection myConnection;
        public myDatabase()
        {
            myConnection = new SqlConnection(conSt);
        }
        public DataTable getData(string sql)
        {
            SqlDataAdapter myDa = new SqlDataAdapter(sql, myConnection);
            DataTable myDt = new DataTable();
            myDa.Fill(myDt);
            return myDt;

        }
    }
}
