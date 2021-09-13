using System;
using System.IO;

namespace FileTransfer
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] drives = System.IO.Directory.GetLogicalDrives();
            string curDir = Environment.CurrentDirectory;
            string movieFileName;
            string movieFullName;
            string movieNewName;
            string yR;
            string mN;
            string dD;
            string hR;
            string mI;
            string sC;

            foreach (string drive in drives)
            {
                if (drive != "C:\\")
                {
                    string forcite = drive + "forcite";
                    // System.Console.WriteLine(drive);
                    if (Directory.Exists (forcite) )
                    {
                        Console.WriteLine($"The directory {forcite} exists");
                        Console.WriteLine();
                        
                        DirectoryInfo forciteDir = new DirectoryInfo(forcite);
                        DirectoryInfo[] myMoviesDir = forciteDir.GetDirectories();
                        // myDirs
                        foreach ( DirectoryInfo dir in myMoviesDir)
                        {
                            //Console.WriteLine(dir);
                            FileInfo[] movies = dir.GetFiles();
                            foreach (FileInfo movie in movies)
                            {
                                //Console.WriteLine(movie);
                                movieFileName = movie.Name;
                                movieFullName = movie.FullName;
                                yR = movieFileName.Substring(0, 4);
                                mN = movieFileName.Substring(4, 2);
                                dD = movieFileName.Substring(6, 2);
                                hR = movieFileName.Substring(8, 2);
                                mI = movieFileName.Substring(10, 2);
                                sC = movieFileName.Substring(12, 6);
                                movieNewName = curDir + "\\" + yR + "-" + mN + "-" + dD + "_" + hR + "." + mI + "." + sC + ".mp4";
                                Console.WriteLine(movieFullName + " -> " + movieNewName);
                                File.Copy(movieFullName, movieNewName, true);
                                //;
                            }


                        }
                    }
                }
            }
            // Console.ReadLine();
        }
    }
}
