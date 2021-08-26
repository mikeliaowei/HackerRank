using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningSumArray
{
	public class ClassA
	{
        public void foo()
        {
            Console.WriteLine("A::foo()");
        }
        public virtual void bar()
        {
            Console.WriteLine("A::bar()");
        }
    }

    public class ClassB : ClassA
    {
        public new void foo()
        {
            Console.WriteLine("B::foo()");
        }
        public override void bar()
        {
            Console.WriteLine("B::bar()");
        }
    }

    public class ClassC : ClassB
    {
        public new void foo()
        {
            Console.WriteLine("C::foo()");
        }
        public override void bar()
        {
            Console.WriteLine("C::bar()");
        }
    }
}
