using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Data
{
    public class Storage : FindIt.Data.IStorage
    {

        private readonly DocumentStore _DocumentStore;

        public DocumentStore DocumentStore
        {
            get
            {
                return _DocumentStore;
            }
        }
        public Storage()
        {
            _DocumentStore = new Raven.Client.Document.DocumentStore { ConnectionStringName = "RavenDB" };
            _DocumentStore.DefaultDatabase = "Test";
            _DocumentStore.Initialize();
        }


      
    }
}
