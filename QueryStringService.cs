using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApi
{
    public class QueryStringService
    {
        private string queryStr;
        public QueryStringService(string queryString) {
            queryStr = queryString.Remove(0, 1);
        }

        public string getData(string name) {
            string[] data = queryStr.Contains("&") ? queryStr.Split("&") : new string[] {queryStr};
            string result = null;

            for (int i = 0; i < data.Length; i++){
                if (name == data[i].Split("=")[0].Trim()) {result = data[i].Split("=")[1].Trim();}
            }

            return result;
        }
    }
}
