using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Data
{
    public class Storage
    {
        public static DocumentStore GetStore
        {
            get
            {
                var result = new DocumentStore();
                result.Url = "http://Windows8:8080";
                result.DefaultDatabase = "Test";
                result.Initialize();
                return result;
            }
        }
    
    }
}
