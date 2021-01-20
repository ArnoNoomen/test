namespace Test_DLL
{
    class Program
    {
        private static string rc = "";

        static void Main(string[] args)
        {
            DLL.Methode routines = new DLL.Methode();
            if (args[0] == "httpPostpy")
            {
                rc = routines.HttpPostpy(args[1], args[2], args[3]);
            }
            if (args[0] == "HttpGetFile")
            {
                rc = routines.HttpGetFile(args[1], args[2]);
            }
        }
    }
}