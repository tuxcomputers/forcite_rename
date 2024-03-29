﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
namespace FileTransfer
{
    class Program
    {
        private static string GetMD5HashFromFile(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        public static string GetInput(string prompt)
        {
            Console.WriteLine("{0}:", prompt);
            return Console.ReadLine();
        }
        static void Main(string[] args)
        {
            // Get a list of the available drives
            string[] drives = System.IO.Directory.GetLogicalDrives();
            // Get the current directory
            string curDir = Environment.CurrentDirectory;
            // Some variables
            string user_input;
            string movieFileName;
            string movieFullName;
            string movieNewName;
            string helmet_hash;
            string drive_hash;
            string yR;
            string mN;
            string dD;
            string hR;
            string mI;
            string sC;
            bool copy_file = false;
            bool delete_file = false;

            user_input = GetInput($"Use the directory {curDir} as the destination? (y/n)");
            if (user_input.ToLower() == "y")
            {

                foreach (string drive in drives)
                {

                    // Skip the C drive, that will not be the helmet or SD card
                    if (drive != "C:\\")
                    {
                        string forcite = drive + "forcite";
                        // Check if the directory exists on each drive
                        if (Directory.Exists (forcite) )
                        {
                            // Ask if they want to use that as the source
                            string source_dir = "Use the directory '" + forcite + "' as the source location? (y/n)";
                            user_input = GetInput(source_dir);
                            if (user_input.ToLower() == "y")
                            {
                                // Ask if they want to delete the file from the source location
                                user_input = GetInput($"Delete the files from {forcite} after the copy? (y/n)");
                                if (user_input.ToLower() == "y")
                                {
                                    delete_file = true;
                                }

                                Console.Clear();

                                // Read the contents of the source dir
                                DirectoryInfo forciteDir = new DirectoryInfo(forcite);
                                // Read the directories within the source dir
                                DirectoryInfo[] myMoviesDir = forciteDir.GetDirectories();
                            
                                // Process each directory
                                foreach (DirectoryInfo dir in myMoviesDir)
                                {
                                    //Console.WriteLine(dir);
                                    // In the subdirectory grab all the file names
                                    FileInfo[] movies = dir.GetFiles();
                                    // Process each file
                                    foreach (FileInfo helmet_movie in movies)
                                    {
                                        // Grab the current name and parse out the date and time
                                        movieFileName = helmet_movie.Name;
                                        movieFullName = helmet_movie.FullName;
                                        yR = movieFileName.Substring(0, 4);
                                        mN = movieFileName.Substring(4, 2);
                                        dD = movieFileName.Substring(6, 2);
                                        hR = movieFileName.Substring(8, 2);
                                        mI = movieFileName.Substring(10, 2);
                                        sC = movieFileName.Substring(12, 6);
                                        // Create a new name that is easier to read
                                        movieNewName = curDir + "\\" + yR + "-" + mN + "-" + dD + "_" + hR + "." + mI + "." + sC + ".mp4";
                                        // Grab the info about the destination file
                                        FileInfo drive_movie = new FileInfo(movieNewName);

                                        // Check if it exists
                                        if (drive_movie.Exists)
                                        {
                                            Console.WriteLine();
                                            // If the size of the movies are the same, then skip the copy
                                            if (helmet_movie.Length == drive_movie.Length)
                                            {
                                                Console.WriteLine("The file " + helmet_movie.Name + " and " + drive_movie.Name + " are the same size");
                                                copy_file = false;
                                                user_input = GetInput("Compare the contents of the two files? (y/n)");
                                                if (user_input.ToLower() == "y")
                                                {
                                                    helmet_hash = GetMD5HashFromFile(helmet_movie.FullName);
                                                    drive_hash = GetMD5HashFromFile(drive_movie.FullName);
                                                    if (helmet_hash == drive_hash)
                                                    {
                                                        Console.WriteLine("The contents are the same, skipping");
                                                        copy_file = false;
                                                    } else
                                                    {
                                                        user_input = GetInput("The contents are different, copy the source file? (y/n)");
                                                        if (user_input.ToLower() == "y")
                                                        {
                                                            copy_file = true;
                                                        }
                                                    }
                                                }

                                            } else
                                            // Ask when the files are different sizes
                                            {
                                                Console.WriteLine("The file " + helmet_movie.Name + " and " + drive_movie.Name + " are different sizes.");
                                                user_input = GetInput($"Copy the file from {forcite} replacing the file? (y/n)");
                                                if (user_input.ToLower() == "y")
                                                {
                                                    copy_file = true;
                                                }
                                                    
                                            }
                                        }
                                        // If it does not exist then copy it
                                        else
                                        {
                                            copy_file = true;
                                        }

                                        // The file needs copying
                                        if (copy_file)
                                        {
                                            Console.WriteLine($"Copying the file {helmet_movie.Name} to {forcite} as {drive_movie.Name}");
                                            File.Copy(movieFullName, movieNewName, true);
                                        }

                                        // The user has asked to have the file deleted
                                        if (delete_file)
                                        {
                                            if (helmet_movie.Exists)
                                            {
                                                Console.WriteLine("Deleteing the file " + helmet_movie);
                                                helmet_movie.Delete();
                                            }
                                        }

                                    }
                                    
                                    // Do some clean up of empty directories
                                    movies = dir.GetFiles();
                                    //Console.WriteLine($"The number of files in the directory {movies} is {movies.Length}");
                                    if (movies.Length == 0)
                                    {
                                        Console.WriteLine("Deleting the empty directory " + dir);
                                        dir.Delete();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
