using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Test.Models;
using System.IO;

namespace Test.Controllers
{
    public class QueueController : Controller
    {

        public IActionResult Data()
        {
            SqlConnection con = new SqlConnection("Server=tcp:itrw316.database.windows.net,1433;Initial Catalog=processes;Persist Security Info=False;User ID=P28732375;Password=Piesang1998!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");
            SqlCommand cmd = new SqlCommand("SELECT * FROM processes", con);
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            List<Process> PList = new List<Process>();
            char name = 'A';
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Process temp = new Process(Convert.ToInt32(ds.Tables[0].Rows[i]["p_arrival_time"]), Convert.ToInt32(ds.Tables[0].Rows[i]["p_length"]),name);
                PList.Add(temp);
                name++;
            }
            con.Close();

            return View(PList);
    }

        [HttpPost]
        public IActionResult Add(int time, int length)
        {
            SqlConnection con = new SqlConnection("Server=tcp:itrw316.database.windows.net,1433;Initial Catalog=processes;Persist Security Info=False;User ID=P28732375;Password=Piesang1998!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");
            SqlCommand cmd = new SqlCommand("INSERT INTO processes(p_arrival_time, p_length, p_priority) VALUES("+time+","+length+",1)",con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("Queue","Home");
        }

        public IActionResult Clear()
        {
            SqlConnection con = new SqlConnection("Server=tcp:itrw316.database.windows.net,1433;Initial Catalog=processes;Persist Security Info=False;User ID=P28732375;Password=Piesang1998!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");
            SqlCommand clear = new SqlCommand("clear_table", con);
            SqlCommand reset = new SqlCommand("reset_auto", con);
            clear.CommandType = CommandType.StoredProcedure;
            reset.CommandType = CommandType.StoredProcedure;
            con.Open();
            clear.ExecuteNonQuery();
            reset.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("Data", "Queue");
        }

        public IActionResult Quanta()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Schedule(int Queue1, int Queue2, int Queue3)
        {
            SqlConnection con = new SqlConnection("Server=tcp:itrw316.database.windows.net,1433;Initial Catalog=processes;Persist Security Info=False;User ID=P28732375;Password=Piesang1998!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");
            SqlCommand cmd = new SqlCommand("SELECT * FROM processes", con);
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            List<Process> PList = new List<Process>();
            char name = 'A';
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Process temp = new Process(Convert.ToInt32(ds.Tables[0].Rows[i]["p_arrival_time"]), Convert.ToInt32(ds.Tables[0].Rows[i]["p_length"]),name);
                
                PList.Add(temp);
                name++;
            }
            con.Close();
            
            int q1 = Queue1;
            int q2 = Queue2;
            int q3 = Queue3;

            ViewBag.Queue1 = q1;
            ViewBag.Queue2 = q2;
            ViewBag.Queue3 = q3;

            PList.Sort();

            ViewBag.List = PList;

            ViewBag.QUEUE = ScheduleList(PList, q1, q2, q3);

            return View(PList);
        }

        public string[] ScheduleList(List<Process> P, int q1,int q2,int q3)
        {
            int ExecutionLength = 0;
            foreach (Process item in P)
            {
                ExecutionLength += item.length;
            }

            string[] order = new string[ExecutionLength];
            string temp = "";

            for (int i = 0; i < (ExecutionLength + 100); i++)
            {
                for (int j = 0; j < P.Count(); j++)
                {
                    if (P[j].priority == 1 && !P[j].added)
                    {
                        if (P[j].done > P[j].length)
                        {
                            P[j].added = true;
                            break;
                        }
                        else
                        {
                            for (int k = 0; k < q1; k++)
                            {
                                if (P[j].done > P[j].length)
                                {
                                    P[j].added = true;
                                    break;
                                }
                                temp = String.Concat(temp, Char.ToString(P[j].name) + P[j].done + ",");
                                P[j].done++;
                                
                                P[j].priority = 2;
                            }
                        }
                    }
                    else if (P[j].priority == 2 && !P[j].added)
                    {
                        if (P[j].done > P[j].length)
                        {
                            P[j].added = true;
                            break;
                        }
                        else
                        {
                            for (int k = 0; k < q2; k++)
                            {
                                if (P[j].done > P[j].length)
                                {
                                    P[j].added = true;
                                    break;
                                }
                                temp = String.Concat(temp, Char.ToString(P[j].name) + P[j].done + ",");
                                P[j].done++;
                                
                            }
                            P[j].priority = 3;
                        }
                    }
                    else if (P[j].priority == 3 && !P[j].added)
                    {
                        if (P[j].done > P[j].length)
                        {
                            P[j].added = true;
                            break;
                        }
                        else
                        {
                            for (int k = 0; k < q3; k++)
                            {
                                if (P[j].done > P[j].length)
                                {
                                    P[j].added = true;
                                    break;

                                }
                                temp = String.Concat(temp, Char.ToString(P[j].name) + P[j].done + ",");
                                P[j].done++;
                                
                            }
                        }
                    }
                }
            }

            order = temp.Split(',');
            
            return order;
        }
    }
}