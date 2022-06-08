/******* Programa C# de consola para conectar con la BD y ejecutar acciones*/
using System;
using Microsoft.Data.SqlClient;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Interactuando con la BD desde la consola");

            string connString = @"Server=tcp:localhost,1433;
                                Initial Catalog=master;
                                Persist Security Info=True;
                                User ID=sa;
                                Password=Docker@123;
                                MultipleActiveResultSets=True;
                                Encrypt=True;TrustServerCertificate=True;
                                Connection Timeout=30;";

            SqlConnection conn = new SqlConnection(connString);
            try
            {
                Console.WriteLine("Abrimos la conexion ...");
                conn.Open();
                Console.WriteLine("Connexion exitosa");

            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }


            // Primer ejercicio: un comando SQL directo, MUESTRA LISTA DE VENDEDORES
            Console.WriteLine("Ejecutamos un comando SQL directamente");
            SqlCommand comando = new SqlCommand("select * from Vendedores", conn);
            SqlDataReader ejecutor = comando.ExecuteReader();
            while (ejecutor.Read())
            {
                Console.WriteLine(ejecutor["CodVendedor"] + " | " + ejecutor["NombreV"] + " | " + ejecutor["TelefonoV"]);
            }
            Console.WriteLine("**_______________________**");
            Console.ReadKey();
            ejecutor.Close();

            
            // Segundo ejercicio: un comando SQL directo
             /*comando.Dispose();
             Console.WriteLine("Ejecutamos un segundo comando SQL directamente (insert)");
                 try{
                       comando.CommandText = "insert into Vendedores values ('554','45456','22223','Carlos Flores','7215642','Av Las Americas','carlos2002@gmail.com')";
                       comando.ExecuteNonQuery();
                       Console.WriteLine("Se insertó correctamente");
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
                Console.WriteLine(ejecutor["CodVendedor"] + " | " + ejecutor["NIT_CON"] + " | " + ejecutor["nitso"] + " | " + ejecutor["NombreV"] + " | " + ejecutor["TelefonoV"] + " | " + ejecutor["Email"]);
            }
            Console.WriteLine("**_______________________**");
            Console.ReadKey();


           

            //Cuarto Ejercicio con parametros de entrada y salida
            /*comando.Dispose();
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
            */
        }
    }

}
