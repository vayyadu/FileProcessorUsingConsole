using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//added this namespace to make use of DirectoryInfo class to access and collect information about the files and folders in the directories
using System.IO;

namespace FileProcessorUsingConsole
{
    class FileProcessor
    {
        // method will calculate the directory size and it calls the same method recursively to include all the subdirectories
        public long GetAggregateDirectorySize(string filePath, bool includeSubDir)
        {
            try
            {

                DirectoryInfo directoryInfo = new DirectoryInfo(filePath);

                // Enumerate all the files
                long aggregateFileSize = directoryInfo.EnumerateFiles().Sum(file => file.Length);

                // If Subdirectories are to be included
                if (includeSubDir)
                {
                    // Enumerate all sub-directories and call the same method recursively
                    aggregateFileSize += directoryInfo.EnumerateDirectories().Sum(dir => GetAggregateDirectorySize(dir.FullName, true));
                }
                return aggregateFileSize;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
