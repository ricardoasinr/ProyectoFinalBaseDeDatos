/******* Programa C# de consola para conectar con la BD y ejecutar acciones*/
using System;
using Microsoft.Data.SqlClient;

namespace Practica2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Interactuando con la BD desde la consola");
            string connString = "Data Source=MONICA-LAPTOP\\SQLEXPRESS;Initial Catalog=Practica6mayo;Integrated Security=True;TrustServerCertificate=True";
            //            string connString = "Data Source = V-W7-DES; Initial Catalog = ConcesionariosVeh; User ID = PracticaConn; Password = PracticaConn";
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                Console.WriteLine("Abrimos la conexion ...");
                conn.Open();
                Console.WriteLine("Connexion exitosa");

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }


            // Primer ejercicio: un comando SQL directo, MUESTRA LISTA DE VENDEDORES
            Console.WriteLine("Ejecutamos un comando SQL directamente");
            SqlCommand comando = new SqlCommand("select * from Vendedor", conn);
            SqlDataReader ejecutor = comando.ExecuteReader();
            while (ejecutor.Read())
            {
                Console.WriteLine(ejecutor["CodVendedor"] + " | " + ejecutor["NombreV"] + " | " + ejecutor["TelefonoV"]);
            }
            Console.WriteLine("**_______________________**");
            Console.ReadKey();
            ejecutor.Close();

            /*
            // Segundo ejercicio: un comando SQL directo
                       comando.Dispose();
                        Console.WriteLine("Ejecutamos un segundo comando SQL directamente (insert)");
                        try
                        {
                            comando.CommandText = "insert into Vendedor values ('890', '88888', '7777', 'Monica Torres', '69021661', 'monica@gmail.com')";
                            comando.ExecuteNonQuery();
                            Console.WriteLine("**_______________________**");
                            Console.ReadKey();
                            ejecutor.Close();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e.Message);
                            Environment.Exit(0);
                        }
            
            */
            
            //Tercer ejercicio: procedimiento almacenado sin parametros
            comando.Dispose();
            Console.WriteLine("Ejecutamos un procedimiento almacenado sin parametros");
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = "ListaVendedores";
            ejecutor = comando.ExecuteReader();
            while (ejecutor.Read())
            {
                Console.WriteLine(ejecutor["CodVendedor"] + " | " + ejecutor["nitcs"] + " | " + ejecutor["nitso"] + " | " + ejecutor["NombreV"] + " | "+ ejecutor["TelefonoV"] + " | "+ ejecutor["Email"]);
            }
            Console.WriteLine("**_______________________**");
            Console.ReadKey();




            //Cuarto Ejercicio con parametros de entrada y salida
            comando.Dispose();
            ejecutor.Close();
            Console.WriteLine("Ejecutamos un procedimiento almacenado con parametros de entrada y salida");
            comando.CommandText = "Vervendedor";
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@p_CodVendedor", "890"); //aqui se envia el parametro
            SqlParameter Nombre = new SqlParameter("@p_NombreV", System.Data.SqlDbType.VarChar, 40);
            Nombre.Direction = System.Data.ParameterDirection.Output;
            comando.Parameters.Add(Nombre);
            comando.ExecuteNonQuery();
            Console.WriteLine("El numero de Codigo de Vendedor corresponde al vendedor:" + Nombre.Value); //asi es como se va a mostrar
            Console.WriteLine("**_______________________**");
            Console.ReadKey();
            
        }
    }

}