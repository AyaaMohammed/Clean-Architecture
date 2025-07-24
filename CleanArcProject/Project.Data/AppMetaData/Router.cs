using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "Api";
        public const string version= "v1";
        public const string Rule = $"{root}/{version}";

        public static class StudentRouting
        {
            public const string Prefix = $"{Rule}/Student";
            public const string GetAll = $"{Prefix}/GetAll";
            public const string GetById = $"{Prefix}/GetById/{{id}}";
            public const string Add = $"{Prefix}/Add";
            public const string Edit = $"{Prefix}/Edit";
            public const string Delete = $"{Prefix}/Delete/{{id}}";
            public const string Pagnated = $"{Prefix}/Pagnated";
        }
    }
}
    