using SprocRigger;

namespace SprocRiggerTests.Helpers
{
    public class TestSelectSproc<T> : SprocInstanceBase<T> {
        public TestSelectSproc(ExecuteType executionType) : base((string) "Test_Select", executionType)
        {

        }
    }
}