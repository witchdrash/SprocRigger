using System.Data;
using SprocRigger;

namespace SprocRiggerTests.Helpers
{
    public class TestSelectByIdSproc<T> : SprocInstanceBase<T>
    {
        public TestSelectByIdSproc(int id, ExecuteType executeType) : base((string) "Test_Select_By_Id", executeType)
        {
            DictionaryParameters.Add("id", new QueryParameter("id", ParameterDirection.Input, id));
        }

        public int Id
        {
            get => (int) DictionaryParameters["id"].Value;
            set => DictionaryParameters["id"].Value = value;
        }
    }
}