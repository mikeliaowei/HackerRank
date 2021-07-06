using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningSumArray
{
    public class SamplesDelegate
    {

        // Declares a delegate for a method that takes in an int and returns a String.
        public delegate String myMethodDelegate(int myInt);

        // Defines some methods to which the delegate can point.
        public class mySampleClass
        {

            // Defines an instance method.
            public String myStringMethod(int myInt)
            {
                if (myInt > 0)
                    return ("positive");
                if (myInt < 0)
                    return ("negative");
                return ("zero");
            }

            // Defines a static method.
            public static String mySignMethod(int myInt)
            {
                if (myInt > 0)
                    return ("+");
                if (myInt < 0)
                    return ("-");
                return ("");
            }
        }

    }
}
