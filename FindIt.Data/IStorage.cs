using Raven.Client.Document;
using System;
namespace FindIt.Data
{
    public interface IStorage
    {
        DocumentStore DocumentStore { get; }
  
    }
}
