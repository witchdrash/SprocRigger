namespace SprocRiggerTests.Helpers
{
    public class TestTable
    {
        public int Id { get; }
        public string Text { get; }

        public TestTable(int id, string text)
        {
            Id = id;
            Text = text;
        }
    }
}