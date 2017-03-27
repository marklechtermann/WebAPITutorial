namespace WebApiTutorial.Infrastruture
{
    using System;

    public class Logger : ILogger
    {
        public void Add(string log)
        {
            Console.WriteLine(log);
        }
    }
}