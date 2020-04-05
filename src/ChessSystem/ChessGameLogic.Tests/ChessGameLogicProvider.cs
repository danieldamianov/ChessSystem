namespace ChessGameLogic.Tests
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Class that provides the test classes with the types and instances of ChessGameLogic library.
    /// </summary>
    public static class ChessGameLogicProvider
    {
        /// <summary>
        /// Gets the ChessGameLogic Assembly.
        /// </summary>
        /// <returns>ChessGameLogic Assembly.</returns>
        public static Assembly GetAssembly()
        {
            return Assembly.GetAssembly(typeof(ChessGame));
        }

        /// <summary>
        /// Gets a type from the the ChessGameLogic Assembly.
        /// </summary>
        /// <param name="name">Name of the Type</param>
        /// <returns>ChessGameLogic Type.</returns>
        public static Type GetType(string name)
        {
            return GetAssembly().GetType(name);
        }

        /// <summary>
        /// Creates type instance using the parameterless constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetTypeInstanceUsingParameterlessConstructor(string name)
        {
            return Activator.CreateInstance(GetAssembly().GetType(name));
        }
    }
}
