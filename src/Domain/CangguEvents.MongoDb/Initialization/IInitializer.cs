﻿using System.Threading.Tasks;

 namespace CangguEvents.MongoDb.Initialization
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}