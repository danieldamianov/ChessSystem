using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ChessGameLogic.Tests
{
    public static class ChessGameLogicProvider
    {
        public static Assembly GetAssembly()
        {
            return Assembly.GetAssembly(typeof(ChessGame));
        }

        public static Type GetType(string name)
        {
            return GetAssembly().GetType(name);
        }

        public static object GetTypeInstanceUsingParameterlessConstructor(string name)
        {
            return Activator.CreateInstance(GetAssembly().GetType(name));
        }
    }
}
