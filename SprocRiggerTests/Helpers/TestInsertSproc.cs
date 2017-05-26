using System.Data;
using SprocRigger;

namespace SprocRiggerTests.Helpers
{
    public class TestInsertSproc<T> : SprocInstanceBase<T>
    {
        public TestInsertSproc(string text, ExecuteType executionType) : base((string) "Test_Insert", executionType)
        {
            DictionaryParameters.Add("text", new QueryParameter("text", ParameterDirection.Input, text));
            DictionaryParameters.Add("id", new QueryParameter("id", ParameterDirection.Output, typeof(int)));
        }

        public string Text
        {
            get => (string)DictionaryParameters["text"].Value;
            set => DictionaryParameters["text"].Value = value;
        }

        public int Id
        {
            get => (int)DictionaryParameters["id"].Value;
            set => DictionaryParameters["id"].Value = value;
        }
    }
}