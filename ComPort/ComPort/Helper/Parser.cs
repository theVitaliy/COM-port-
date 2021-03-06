﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ComPort
{
    public static class Parser
    {
        public static int GetStatus(String data)
        {
            char[] delimiterChars = { '\n', '\t', '\r' };
            string[] words = data.Split(delimiterChars);
            string newData = "";
            foreach (string s in words)
            {
                newData += s;
            }
            DateTime localDate = DateTime.Now;

                char[] delimiterChars1 = { ',', '\n', '\t', '\r', '=' };
                string[] words1 = newData.Split(delimiterChars1);

                int i = 0;
                int[] z = new int[words1.Length];
                foreach (string s in words1)
                {
                    if (int.TryParse(s, out z[i]))
                        i++;
                }

                return z[0];
           
        }

        public static Data GetParesedData(String data)
        {
            char[] delimiterChars = {'\n', '\t', '\r' };
            string[] words = data.Split(delimiterChars);
            string newData="";
            foreach (string s in words)
            {
                newData += s;
            }
            DateTime localDate = DateTime.Now;
            if (ValidateData(newData))
            {

                Data goodData;
                char[] delimiterChars1 = { ',', '\n', '\t', '\r'};
                string[] words1 = newData.Split(delimiterChars1);
                //string[] words2 = new string[words1.Length-1];
                int q = 0;
                //for(; q< words1.Length - 1; q++)
                //{ 
                //    words2[q] = words1[q];
                //}
                int i = 0;
                int[] z = new int[words1.Length];
                foreach (string s in words1)
                {
                    if (int.TryParse(s, out z[i]))
                        i++;
                }
                
                String time1 = localDate.ToShortDateString().ToString();
                String time2 = localDate.ToShortTimeString().ToString();

                String sum = time1 + " " + time2;
                string substr1 = " PM";
                string substr2 = " AM";
               
                String allout = "machine= " + z[0].ToString() + "\r \n" + sum + "\r \n" + z[2].ToString() + "\r \n" + "status =" + z[1].ToString();

                sum = sum.Replace(substr1, "");
                sum = sum.Replace(substr2, "");



                String log = "machine= " + z[0].ToString() + ", " + sum + ", " + z[2].ToString() + ". " + "status =" + z[1].ToString();
                Loger.SetLog(log);
                goodData = new Data(z[2],z[1],z[0],allout);
                return goodData;
            }
            else
            {
                Data badData = null;
                return badData;
            }
        }

        public static List<Data> GetAllParsedData(String data )
        {
            char[] delimiterChars1 = { '#' };
            string[] words1 = data.Split(delimiterChars1);
            List<Data> allData = new List<Data>();
            foreach (String s in words1)
            {
                Data da = GetParesedData(s);
                allData.Add(da);
            }
            return allData;
        }

        private static bool ValidateData(string data)
        {
            char[] delimiterChars = { '\n', '\t', '\r' };
            string[] words = data.Split(delimiterChars);
            string newData = "";
            foreach (string s in words)
            {
                newData += s;
            }
            
            Match m = Regex.Match(newData, "[A][,](([0-9]|[0-1][0-5]))[,][0-9][,][0-9]{1,3}");
            if (m.Success)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static int GetSpeed(String speed)
        {
            int iSpeed = -1;

            char[] delimiterChars = { '\n', '\t', '\r', ' ' };
            string[] words = speed.Split(delimiterChars);
            foreach (String s in words)
            {
                if (!int.TryParse(s, out iSpeed))
                {
                    iSpeed = -1;
                }
            }
            return iSpeed;
        }


    }
}
