﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using System.Windows.Forms;

namespace TRUNGTAMTINHOC.TangDB
{
    public class TruyCapDB
    {
        private static TruyCapDB instance;
        public static TruyCapDB Instance //crtl r e
        {
            get { if (instance == null) instance = new TruyCapDB(); return TruyCapDB.instance; }
            private set { TruyCapDB.instance = value; }
        }

        private static string connStr = "";// User Id = c##bv_schema;Password=bv_schema";
        public static string ConnStr { get => connStr; set => connStr = value; }



        //type la loai doi tuong thuc thi: procedure, function, ..
        public DataTable ExecuteParameterQuery(string query, string type = null, object[] parameter = null) //Truy van du lieu tu data base, khong can chu y dau ',' hay @, hay khoang trang
        {
            DataTable data = new DataTable();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConnStr))
                {

                    connection.Open();

                    OracleCommand command = new OracleCommand();
                    command.Connection = connection;

                    if (type != null && type == "procedure")
                    {
                        command.CommandText = query.Split(' ')[0];
                        command.CommandType = CommandType.StoredProcedure;

                        if (parameter != null)
                        {
                            string[] listPara = query.Split(' ');
                            int i = 1;
                            foreach (string item in listPara)
                            {
                                command.Parameters.Add(item, parameter[i]);
                                i++;
                            }
                        }
                    }
                    else
                    {
                        command.CommandText = query;
                    }


                    OracleDataAdapter adapter = new OracleDataAdapter(command);

                    adapter.Fill(data);

                    connection.Close();

                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message); //Them using System.Windows.Forms;
            }

            return data;
        }

        public int ExecuteParameterNonQuery(string query, string type = null, object[] parameter = null) //Tra ve so dong insert, delete thanh cong
        {
            int data = 0;
            try
            {
                using (OracleConnection connection = new OracleConnection(ConnStr))
                {

                    connection.Open();

                    OracleCommand command = new OracleCommand();
                    command.Connection = connection;

                    if (type != null && type == "procedure")
                    {
                        command.CommandText = query.Split(' ')[0];
                        command.CommandType = CommandType.StoredProcedure;

                        if (parameter != null)
                        {
                            string[] listPara = query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (i != 0)
                                {
                                    command.Parameters.Add(item, parameter[i - 1]);

                                }
                                i++;
                            }
                        }
                    }
                    else
                    {
                        command.CommandText = query;
                    }


                    data = command.ExecuteNonQuery();

                    connection.Close();

                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message); //Them using System.Windows.Forms;
            }

            return data;
        }


        public object ExecuteScalar(string query, string type = null, object[] parameter = null)
        {
            object data = 0;

            try
            {
                using (OracleConnection connection = new OracleConnection(ConnStr))
                {

                    connection.Open();

                    OracleCommand command = new OracleCommand();
                    command.Connection = connection;

                    if (type != null && type == "procedure")
                    {
                        command.CommandText = query.Split(' ')[0];
                        command.CommandType = CommandType.StoredProcedure;

                        if (parameter != null)
                        {
                            string[] listPara = query.Split(' ');
                            int i = 0;
                            foreach (string item in listPara)
                            {
                                if (i != 0)
                                {
                                    command.Parameters.Add(item, parameter[i - 1]);

                                }
                                i++;
                            }
                        }
                    }
                    else
                    {
                        command.CommandText = query;
                    }


                    data = command.ExecuteScalar();

                    connection.Close();

                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message); //Them using System.Windows.Forms;
            }

            return data;
        }

        
    }
}
