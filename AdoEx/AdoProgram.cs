using System;
using Npgsql;

namespace AdoEx
{
    class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Decription  { get; set; }

        public override string ToString()
        {
            return $"Id = {Id}, Name = {Name}";
        }
    }


    public class AdoProgram
    {
        static void Main(string[] args)
        {
            var connStr = "host=localhost;db=northwind;uid=bulskov;pwd=henrik";

            var conn = new NpgsqlConnection(connStr);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "select * from categories";
            cmd.Connection = conn;

            var reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                var category = new Category
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Decription = reader.GetString(2)
                };
                Console.WriteLine(category);
            }

        }
    }
}
