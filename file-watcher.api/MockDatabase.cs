using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace file_watcher.api
{
    public class MockDatabase
    {
        private DateTime _dateLastUpdated = DateTime.Now;
        private static MockDatabase _instance;

        public static MockDatabase GetInstance()
        {
            if (_instance == null)
                _instance = new MockDatabase();

            return _instance;
        }

        public void SetDateLastUpdated()
        {
            _dateLastUpdated = DateTime.Now;
        }

        public DateTime? GetDateLastUpdated() => _dateLastUpdated;
    }
}
