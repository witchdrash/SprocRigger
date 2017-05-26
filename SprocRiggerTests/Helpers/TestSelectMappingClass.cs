using System.Collections.Generic;
using System.Linq;
using SprocRigger;
using UglyMapper;

namespace SprocRiggerTests.Helpers
{
    public class TestSelectMappingClass : BaseMapperConfiguration<ICollection<DataResults>, ICollection<TestTable>>
    {
        public TestSelectMappingClass()
        {
            ConstructBy(x =>
            {
                return Enumerable.Select(x, y => new TestTable(y["id"].Value<int>(), y["text"].Value<string>())).ToList();
            });
        }
    }
}