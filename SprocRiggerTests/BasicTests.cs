using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xunit;
using SprocRigger;
using SprocRiggerTests.Helpers;

namespace SprocRiggerTests
{
    public class BasicTests
    {
        private const string ConnectionString = "Data Source=localhost;Initial Catalog=TestDatabase;user id=sa;password=SomePasswordHere!";

        [Fact]
        public void TestMethod1()
        {
            var classUnderTest = new DataAccess(new SqlImplementation(new SqlConnection(ConnectionString)));

            var text = DateTime.Now.ToString("HH:mm:ss dd MMM yyyy");

            var testInsertSproc = new TestInsertSproc<int>(text, ExecuteType.NonQuery);
            var testSelectSproc = new TestSelectSproc<ICollection<TestTable>>(ExecuteType.Collection);

            classUnderTest.Use(testInsertSproc).Use(testSelectSproc).Execute();
        }

        [Fact]
        public void TestInsertRowThenSelectNewRow()
        {
            var classUnderTest = new DataAccess(new SqlImplementation(new SqlConnection(ConnectionString)));

            var text = DateTime.Now.ToString("HH:mm:ss dd MMM yyyy");

            var testInsertSproc = new TestInsertSproc<int>(text, ExecuteType.NonQuery);
            var testSelectSproc = new TestSelectByIdSproc<ICollection<TestTable>>(-1, ExecuteType.Collection);

            classUnderTest.Use(testInsertSproc).Use(() =>
            {
                testSelectSproc.Id = testInsertSproc.Id;
                return testSelectSproc;
            }).Execute();
        }

        [Fact]
        public void TestCollectionMapper()
        {
            var classUnderTest = new DataAccess(new SqlImplementation(new SqlConnection(ConnectionString)));

            var testSelectSproc = new TestSelectSproc<ICollection<TestTable>>(ExecuteType.Collection);

            classUnderTest.Use(testSelectSproc).Execute();

            var result = testSelectSproc.Results(new TestSelectMappingClass());
        }

        [Fact]
        public void TestScalarMapper()
        {
            var classUnderTest = new DataAccess(new SqlImplementation(new SqlConnection(ConnectionString)));

            var text = DateTime.Now.ToString("HH:mm:ss dd MMM yyyy");

            var testInsertSproc = new TestInsertSproc<int>(text, ExecuteType.Scalar);

            classUnderTest.Use(testInsertSproc).Execute();

            var result = testInsertSproc.Results(new TestScalarMappingClass());
        }
    }
}
