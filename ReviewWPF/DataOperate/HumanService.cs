using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewWPF.DataOperate
{
    public class HumanService
    {
        private static string constr = "server=localhost;User Id=root;password=123456;Database=mytest";

        public static List<HumanEnt> GetHumanList()
        {
            MySqlConnection mycon = new MySqlConnection(constr);
            DataSet ds = new DataSet();
            mycon.Open();
            MySqlCommand cmd = new MySqlCommand("select * from human", mycon);
            MySqlDataAdapter command = new MySqlDataAdapter(cmd);
            command.Fill(ds, "ds");
            var data = ds.Tables[0];
            List<HumanEnt> list = new List<HumanEnt>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                HumanEnt ent = new HumanEnt()
                {
                    ID = (int)data.Rows[i]["id"],
                    Name = data.Rows[i]["name"].ToString(),
                    Age = (int)data.Rows[i]["age"],
                    Sex = (int)data.Rows[i]["sex"],
                    Phone = data.Rows[i]["phone"].ToString(),
                    Education = (EducationEnum)(int)data.Rows[i]["education"],
                    Email = data.Rows[i]["email"].ToString(),
                    Birthday = (DateTime)data.Rows[i]["birthday"]
                };
                list.Add(ent);
            }
            mycon.Close();
            return list;
        }

        public static bool InsertHuman(HumanEnt ent)
        {
            MySqlConnection mycon = new MySqlConnection(constr);
            mycon.Open();
            MySqlCommand mycmd = new MySqlCommand("insert into human(name,age,sex,phone,education,email,birthday) values('" + ent.Name + "','" + ent.Age + "','" + ent.Sex + "','" + ent.Phone + "','" + (int)ent.Education + "','" + ent.Email + "','" + ent.Birthday + "')", mycon);
            if (mycmd.ExecuteNonQuery() > 0)
            {
                mycon.Close();
                return true;
            }
            mycon.Close();
            return false;
        }

        public static bool DeleteHuman(int id)
        {
            MySqlConnection mycon = new MySqlConnection(constr);
            mycon.Open();
            MySqlCommand mycmd = new MySqlCommand("DELETE FROM human WHERE Id =" + id, mycon);
            if (mycmd.ExecuteNonQuery() > 0)
            {
                mycon.Close();
                return true;
            }
            mycon.Close();
            return false;
        }

        public static bool UpdateHuman(HumanEnt ent)
        {
            MySqlConnection mycon = new MySqlConnection(constr);
            mycon.Open();
            MySqlCommand mycmd = new MySqlCommand("UPDATE human SET name='" + ent.Name + "',age='" + ent.Age + "',sex='" + ent.Sex + "',phone='" + ent.Phone + "',education='" + (int)ent.Education + "',email='" + ent.Email + "',birthday='" + ent.Birthday + "' where id='" + ent.ID + "'", mycon);
            if (mycmd.ExecuteNonQuery() > 0)
            {
                mycon.Close();
                return true;
            }
            mycon.Close();
            return false;
        }
    }
}
