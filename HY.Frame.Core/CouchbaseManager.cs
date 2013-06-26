﻿using Couchbase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HY.Frame.Core
{
    public static class CouchbaseManager
    {
        private readonly static CouchbaseClient _instance;

        static CouchbaseManager()
        {
            _instance = new CouchbaseClient();
        }

        public static CouchbaseClient Instance { get { return _instance; } }
    }
}
