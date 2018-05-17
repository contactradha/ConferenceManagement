using System;
using System.Collections.Generic;
using ConferenceManager.CrossCutting;
using ConferenceManager.Input;


namespace ConferenceManager
{
    /// <summary>
    /// This is the starting pointer for the program
    /// This program can take input from command line arguments or 
    /// From the file "input.txt" available under root folder
    /// </summary>
    class Program
    {
        public static void Main(string[] args)
        {   
            Logger.RecordMessage("Program Started", Log.MessageType.Information, Logger.LogTypes.File);
            try
            {
                Input.Input ip = new Input.Input(new FileInput())
                {
                     FilePath = args.Length == 0 ? "input.txt" : args[0]                   
                };
                ip.ProcessInput(); //Process the input file                 
                Logger.RecordMessage("Program ended sucessfully", Log.MessageType.Information, Logger.LogTypes.File);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Logger.RecordMessage(ex, Log.MessageType.Information, Logger.LogTypes.File);
            }
        }
    }
}
