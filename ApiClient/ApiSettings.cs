using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMoon.ClientApp
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ApiInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public string Template { get; set; }
        public string ContentType { get; set; }
        public string Group { get; set; }
        public Parameter[] Parameters { get; set; }
    }
    public class Parameter
    {
        public string Name { get; set; }
        public string Template { get; set; }
    }
    public class Credentials
    {
        public string Locker { get; set; }
        public string Key { get; set; }
    }

    public class ApiEnviroment
    {
        public string Name { get; set; }
        public string Root { get; set; }
        public string Group { get; set; }
        public Credentials Credential { get; set; }
    }

    public class ApiSettings
    {
        public List<ApiEnviroment> Enviroments { get; set; }
        public List<ApiInfo> Apis { get; set; }
    }





}
