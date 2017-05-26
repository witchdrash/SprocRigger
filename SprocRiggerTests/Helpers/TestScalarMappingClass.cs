using System.Collections.Generic;
using System.Linq;
using SprocRigger;
using UglyMapper;

namespace SprocRiggerTests.Helpers
{
    public class TestScalarMappingClass : BaseMapperConfiguration<ICollection<DataResults>, int> {
        public TestScalarMappingClass()
        {
            ConstructBy(x => Enumerable.First<DataResults>(x)[0].Value<int>());
        }
    }
}