using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ConferenceManager.CrossCutting;
using ConferenceManager.Event;

namespace ConferenceManager.Input
{
    /// <summary>
    /// This class is derived from IInputParser
    /// This class will read the input from a file, provided through commandline arguments 
    /// or Input.txt available under project folder
    /// </summary>
    public class FileInput : IInputType
    {       
        public IEnumerable<Slot> Parse(Input input)
        {
            Logger.RecordMessage("Started Reading Input File", Log.MessageType.Information, Logger.LogTypes.File);
            
            string FilePath = input.FilePath;                
            string[] inputLines = File.ReadAllLines(FilePath);
            if (inputLines.Length == 0) throw new Exception("File is empty");
            var v2 = (from val in
                            (from line in inputLines
                            select new
                            {
                                t1 = line.Length.Equals(0) ? "": line.Substring(0, line.LastIndexOf(' ')).Trim(),  //check empty lines
                                d1 = line.Substring(line.LastIndexOf(' ') + 1, line.Length - line.LastIndexOf(' ') - 1).ToLower()
                            })
                        select new Slot
                        {
                            Description = val.t1,
                            Duration = val.d1.Contains("min") ?
                                    Convert.ToInt32(val.d1.Replace("min", "")) : (val.d1.Equals("lightning") ? 5 : 0)
                        }).ToList<Slot>();
            Logger.RecordMessage("Completed Reading Input File", Log.MessageType.Information, Logger.LogTypes.File);
            return v2;           
        }
    }
}
