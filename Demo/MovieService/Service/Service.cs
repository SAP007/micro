using Microsoft.AspNetCore.Mvc;
using MovieService.Models;
using Npgsql;
using MovieService;
using MovieService.Service;
using System;
using System.Collections.Generic;

// Service + data access layer combined 

namespace MovieService.Service
{
    public class Service : IMovieService
    {
        private static string Host = "localhost";
        private static string User = "postgres";
        private static string DBname = "dvdrental";
        private static string Password = "admin";
        private static string Port = "5432";

        string connString =
               String.Format(
                   "Server={0};Username={1};Database={2};Port={3};Password={4}",
                   Host,
                   User,
                   DBname,
                   Port,
                   Password);


        public List<string> GetAllMovies()
        {
            Console.Out.WriteLine(" - GetAllMovies() enabled");
            var movieList = new List<string>();
            movieList.Add("HELLO Stuff is working! :)");

            /*
            var movieList2 = new List<Object>();

            using (var conn = new NpgsqlConnection(connString))
            {
                Console.Out.WriteLine("   - Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT title FROM movie", conn))
                {
                    var reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        movieList.Add(
                            string.Format(
                                "(title: {0})",
                                reader.GetString(0)
                                )
                            );


                    };
                    reader.Close();
                    Console.Out.WriteLine("   - Connection closed");
            
                }
                //movieList.ForEach(Console.WriteLine);
           
            }
             */
            return movieList;
        }


        public List<string> SearchMovies(string item1, string item2)
        {
            Console.Out.WriteLine(" - SearchMovies() enabled");

            var movieList = new List<string>();

            using (var conn = new NpgsqlConnection(connString))
            {
                Console.Out.WriteLine("   - Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand($"SELECT title,category FROM movies WHERE LOWER({item1}) LIKE LOWER('%{item2}%')", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        movieList.Add(
                            string.Format(
                                "(title: {0}, category: {1})",
                                reader.GetString(0),
                                reader.GetString(1)
                                )
                            );
                    }
                    reader.Close();
                    Console.Out.WriteLine("   - Connection closed");

                }
                //movieList.ForEach(Console.WriteLine);
            }
            return movieList;
        }
    }
}

