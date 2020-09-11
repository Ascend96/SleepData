using System;
using System.IO;
using NLog.Web;

namespace SleepData
{
    class Program
    {
        static void Main(string[] args)
        {
            // create instance of Logger
            string path = Directory.GetCurrentDirectory() + "\\nlog.config";
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();

            // logs that program has started
            logger.Info("Program started"); 

            var file = "data.txt";
            // ask for input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();

            if (resp == "1")
            {
                // create data file

                // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                string ans = Console.ReadLine();
                int weeks;
                if (!int.TryParse(ans, out weeks))
                {
                    logger.Error("Invalid input (integer): {Answer}", ans);
                }
                else
                {
                    // determine start and end date
                    DateTime today = DateTime.Now;
                    // we want full weeks sunday - saturday
                    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                    // subtract # of weeks from endDate to get startDate
                    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
                    
                    // random number generator
                    Random rnd = new Random();

                    // create file
                    StreamWriter sw = new StreamWriter(file);
                    // loop for the desired # of weeks
                    while (dataDate < dataEndDate)
                    {
                        // 7 days in a week
                        int[] hours = new int[7];
                        for (int i = 0; i < hours.Length; i++)
                        {
                            // generate random number of hours slept between 4-12 (inclusive)
                            hours[i] = rnd.Next(4, 13);
                        }
                        // M/d/yyyy,#|#|#|#|#|#|#
                        //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                        sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                        // add 1 week to date
                        dataDate = dataDate.AddDays(7);
                    }
                    sw.Close();
                }
            }
            else if (resp == "2")
            {

                DateTime today = DateTime.Now;
                DateTime weeks = today.AddDays(-7);
               // int count = 0;

                // checkes if file exists
                if (File.Exists(file)) {
                    // reads file
                        StreamReader sr = new StreamReader(file);
                        // while loop to run if there is still data in file
                        while(!sr.EndOfStream) {
                            // creates and converst the lines of the file into an array
                            string line = sr.ReadLine();
                            // splits the items in file by comma for date, pipe for days
                            string [] week = line.Split(new char[] {',', '|'});
                            // Parses first element in array to DateTime for formatting
                            DateTime date = DateTime.Parse(week[0]);
                            // variables that represent hours for each day of week starting sunday
                            string sunday = week[1];
                            string monday = week[2];
                            string tuesday = week[3];
                            string wednesday = week[4];
                            string thursday = week[5];
                            string friday = week[6];
                            string saturday = week[7];

                            // variables for spaces/day abbreviations
                            string spaces = "--";
                            string day1 = "Su";
                            string day2 = "Mo";
                            string day3 = "Tu";
                            string day4 = "We";
                            string day5 = "Th";
                            string day6 = "Fr";
                            string day7 = "Sa"; 

                            // Displays the Date in correct format MMM/dd/yyyy
                            Console.WriteLine($"Week of {date:MMM}, {date:dd}, {date:yyyy}");
                            // Displays the column headers with day abbreivations and spaces as dividers
                            Console.WriteLine($"{day1,3}{day2,3}{day3,3}{day4,3}{day5,3}{day6,3}{day7,3}");
                            Console.WriteLine($"{spaces,3}{spaces,3}{spaces,3}{spaces,3}{spaces,3}{spaces,3}{spaces,3}");
                            // Displays the hours in proper column based on day of week
                            Console.WriteLine($"{sunday,3}{monday,3}{tuesday,3}{wednesday,3}{thursday,3}{friday,3}{saturday,3}\n");                
                  }
                 
                  sr.Close();

                } 
                   
                

            }
            // logs that program has ended
            logger.Info("Program ended");
        }
    }
}