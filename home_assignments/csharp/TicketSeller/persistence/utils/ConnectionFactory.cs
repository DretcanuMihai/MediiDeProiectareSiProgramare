﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace persistence.utils
{
    public abstract class ConnectionFactory
    {
        protected ConnectionFactory()
        {
        }

        private static ConnectionFactory instance;
        
        public static ConnectionFactory GetInstance()
        {
            if (instance == null)
            {
                Assembly assem = Assembly.GetExecutingAssembly();
                Type[] types = assem.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsSubclassOf(typeof(ConnectionFactory)))
                        instance = (ConnectionFactory) Activator.CreateInstance(type);
                }
            }

            return instance;
        }
        
        public abstract IDbConnection CreateConnection(IDictionary<string, string> props);
    }
}