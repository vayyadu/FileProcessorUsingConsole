using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// addded this namespace to make use of Task class to achieve multithreading needs of this task
using System.Threading.Tasks;
// added this namespace to format the results
using System.Globalization;

namespace FileProcessorUsingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                long result;
                // show the message to the user that the program is still processing
                Console.WriteLine("Processing.. Please wait..");

                // spin three different threads - one for each one of the three user input directories
                var task1 = Task.Factory.StartNew(() => result = CalculateDirectorySize(args[0]));
                var task2 = Task.Factory.StartNew(() => result = CalculateDirectorySize(args[1]));
                var task3 = Task.Factory.StartNew(() => result = CalculateDirectorySize(args[2]));

                // wait for all the three threads to complete its task before we show the result to the user
                Task.WaitAll();

                //add the results from the three threads to get the aggregate of all the file sizes
                result = task1.Result + task2.Result + task3.Result;

                // display the result to the console
                Console.WriteLine("The aggregate of all the file sizes is "
                                    + String.Format(CultureInfo.InvariantCulture, "{0:#,#}", result) + " bytes ("
                                    + String.Format(CultureInfo.InvariantCulture, "{0:#,#}", (result / 1024)) + " KB / "
                                    + String.Format(CultureInfo.InvariantCulture, "{0:#,#}", (result / (1024 * 1024))) + " MB" + ")");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error has occured. Please try again. Additonal error information: " + ex.Message);
            }

            Console.ReadKey();
        }

        // this method is used to calculate the directory size
        public static long CalculateDirectorySize(string filePath)
        {
            try
            {
                // created a FileProcessor class to do the actual work of calculating the directory sizes. This improves the code readability and reusability
                FileProcessor fileProcessor = new FileProcessor();

                //calling the method from FileProcessor class to get the directory size
                long directorySize = fileProcessor.GetAggregateDirectorySize(filePath, true);

                return directorySize;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
