using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SimpleDBDiff
{
    [TestFixture]
    class Tester
    {
        private dbHelper db;
        [SetUp]
        public void Init()
        {
            db  = new dbHelper();
            Assert.True(db.SetupConnection("10.25.6.33"));

        }

        [Test]
        public void TestGetDatabases()
        {
            string[] dbs = db.GetDatabases();
        }

        [Test]
        public void TestGetTables()
        {
            string[] dbs = db.GetTables("naiTemp");
            string[] tbs = db.GetColumns("naiTemp",dbs[0]);
        }

        [Test]
        public void TestCompareTables()
        {
            DataTable dt =db.CompareTables("naiTemp", "naiTestDB", "OrderLineType");
        }
        
    }
}
