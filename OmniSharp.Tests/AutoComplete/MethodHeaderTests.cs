using System.Linq;
using NUnit.Framework;
using Should;

namespace OmniSharp.Tests.AutoComplete
{
    [TestFixture]
    public class MethodHeaderTests : CompletionTestBase
    {
        [Test]
        public void Should_return_method_header()
        {
            {
                MethodHeaderFor(
                    @"public class A {
    public A() 
    {
        int n;
        n.T$;
    }
}").First().ShouldStartWith("ToString(");
            }
        }

        [Test]
        public void Should_return_method_return_type()
        {
            ReturnTypeFor(
                @"public class A {
    public A() 
    {
        int n;
        n.T$;
    }
}").First().ShouldEqual("string");
        }

        [Test]
        public void Should_add_generic_type_argument()
        {
            MethodHeaderFor(
                @"using System.Collections.Generic;
            public class Class1 {
                public Class1()
                {
                    var l = new Lis$
                }
            }")
                .ShouldContain("List<T>()");
        }

        [Test]
        public void Should_add_generic_type_argument_with_generic_parameter()
        {
            MethodHeaderFor(
                @"using System.Collections.Generic;
            public class Class1 {
                public Class1()
                {
                    var l = new Lis$
                }
            }")
                .ShouldContain("List<T>(IEnumerable<T> collection)");
        }

        [Test]
        public void Should_add_angle_bracket_to_generic_completion()
        {
            MethodHeaderFor(
                @"using System.Collections.Generic;
                public class Class1 {
                    public Class1()
                    {
                        var l = new Diction$
                    }
                }")
                .ShouldContain("Dictionary<TKey, TValue>()");
        }

        [Test]
        public void Should_not_add_this_parameter_to_extension_method()
        {
            MethodHeaderFor(
            @"using System.Collections.Generic;
            using System.Linq;

            public class A {
                public A()
                {
                    string s;
                    s.MyEx$
                }
            }

            public static class StringExtensions
            {
                public static string MyExtension(this string s)
                {
                    return s;
                }

                public static string MyExtension(this string s, int i)
                {
                    return s;
                }
            }
            ").ShouldContainOnly("MyExtension()", "MyExtension(int i)");
        }
    }
}