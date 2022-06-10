using Microsoft.Data.SqlClient;

// MAIN
string connString = @"Server=tcp:localhost,1433;
                                Initial Catalog=master;
                                Persist Security Info=True;
                                User ID=sa;
                                Password=Docker@123;
                                MultipleActiveResultSets=True;
                                Encrypt=True;TrustServerCertificate=True;
                                Connection Timeout=30;";

Console.WriteLine("Conexion con base de datos");


var sqlConnection = new SqlConnection(connString);
SqlDataReader data = null;

try
{
    Console.WriteLine("Abriendo conexion...");
    sqlConnection.Open();
    Console.WriteLine("Conectado");
}
catch (Exception e)
{
    Console.WriteLine($"Error: {e.Message}");
    Environment.Exit(0);
}
Menu(sqlConnection, ref data);
// FIN MAIN
static void MainMenu()
{
    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
    Console.WriteLine("Toque cualquier tecla para volver al menu principal");
    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
}
static void Menu(SqlConnection sqlConnection, ref SqlDataReader data)
{
    int op;
    do
    {
        Console.Clear();
        Console.WriteLine("--------------TALLER RIMAFRA----------------");
        Console.WriteLine("1. Obtener todos los clientes");
        Console.WriteLine("2. Obtener todos los vehiculos");
        Console.WriteLine("3. Obtener todos las hojas de parte");
        Console.WriteLine("4. Obtener todos los mecanicos");
        Console.WriteLine("5. Obtener todos las facturas");
        Console.WriteLine("6. Obtener todos los repuestos");
        Console.WriteLine("7. Obtener todos los servicios");
        Console.WriteLine("8. Lista de vendedores");
        Console.WriteLine("0. Salir");
        op = Convert.ToInt32(Console.ReadLine());

        switch (op)
        {
            case 1:
                GetAllClients(sqlConnection, ref data);
                break;
            case 2:
                GetAllCars(sqlConnection, ref data);
                break;
            case 3:
                GetAllDataSheets(sqlConnection, ref data);
                break;
            case 4:
                GetAllMechanics(sqlConnection, ref data);
                break;
            case 5:
                GetAllReceipts(sqlConnection, ref data);
                break;
            case 6:
                GetAllSpareParts(sqlConnection, ref data);
                break;
            case 7:
                GetAllDetails(sqlConnection, ref data);
                break;
            case 8:
                ProcedureListaVendedores(sqlConnection, ref data);
                break;
            case 0:
                Console.WriteLine("Terminado...");
                break;
            default:
                Console.WriteLine("Opcion invalida");
                break;
        }
        Console.ReadLine();
    } while (op != 0);
}

static void GetAllClients(SqlConnection sqlConnection, ref SqlDataReader data)
{
    var sqlCommand = new SqlCommand("select * from Clientes", sqlConnection);
    data = sqlCommand.ExecuteReader();

    if (data.HasRows)
    {
        Console.WriteLine("\n");
        Console.WriteLine("----------Clientes Registrados----------");
        while (data.Read())
        {
            
            Console.WriteLine($"CI: {data["CI_Clientes"]} \n" +
                              $"Nombre: {data["NombreClie"]}\n" +
                              $"Direccion: {data["DireccionCli"]}\n" +
                              $"Telfono: {data["TelefonoCli"]}\n" +
                              $"Factura: {data["Factura"]}");
            Console.WriteLine("---------------------------------------");
            MainMenu();
        }
        
    }
    else
    {
        Console.WriteLine("No existen clientes");
    }
    data.Close();
}

static void GetAllCars(SqlConnection sqlConnection, ref SqlDataReader data)
{
    SqlCommand sqlCommand = new SqlCommand("select * from Automoviles", sqlConnection);
    data = sqlCommand.ExecuteReader();
    if (data.HasRows)
    {
        Console.WriteLine("\n");
        Console.WriteLine("----------------Vehiculos---------------");
        while (data.Read())
        {
            
            Console.WriteLine($"Numero de chasis: {data["NroChasis"]}\n" +
                              $"NIT Concesionaria: {data["NIT_CON"]}\n" +
                              $"Codigo Modelo: {data["CodModelo"]}\n" +
                              $"Fecha: {data["FechaFabri"]} \n" +
                              $"Color: {data["Color"]}\n"+
                              $"Estado: {data["Estado"]}");

            Console.WriteLine("----------------------------------------");
            
        }
        MainMenu();
        
    } else
    {
        Console.WriteLine("No hay vehiculos registrados");
    }
    data.Close();
}

static void GetAllDataSheets(SqlConnection sqlConnection, ref SqlDataReader data)
{
    SqlCommand sqlCommand = new SqlCommand("select * from HojadeParte", sqlConnection);
    data = sqlCommand.ExecuteReader();
    if (data.HasRows)
    {
        Console.WriteLine("\n");
        Console.WriteLine("----------------Hoja de parte---------------");
        while (data.Read())
        {
            
            Console.WriteLine($"Codigo: {data["CodHoja"]}\n" +
                              $"Matricula: {data["Matricula"]}\n" + 
                              $"Fecha de ingreso: {data["FechaIngreso"]}"+
                              $" Hora: {data["HoraIngreso"]}\n"+
                              $"Mecanico encargado: {data["CodMecanico"]}\n" +
                              $"Costo de mano de obra: {data["Manodeobra"]}");
            Console.WriteLine("--------------------------------------------");
        }
        MainMenu();
        
    } else
    {
        Console.WriteLine("No existen datos");
    }
    data.Close();
}

static void GetAllMechanics(SqlConnection sqlConnection, ref SqlDataReader data)
{
    var sqlCommand = new SqlCommand("select * from Mecanicos", sqlConnection);
    data = sqlCommand.ExecuteReader();

    if (data.HasRows)
    {
        Console.WriteLine("\n");
        Console.WriteLine("---------------------Mecanicos--------------------");
        while (data.Read())
        {
            
            Console.WriteLine($"Codigo: {data["CodMecanico"]}\n" +
                              $"NIT Servicio oficial: {data["NITSO"]}\n" +
                              $"Especialidad: {data["Especialidad"]}\n"+
                              $"Nombre: {data["NombreMec"]}"+
                              $" {data["ApellidoMec"]} \n"+
                              $"Telefono: {data["TelefonoMec"]}");
            Console.WriteLine("-------------------------------------------------");
            
        }
        MainMenu();
    }
    else
    {
        
        Console.WriteLine("No existen datos");
    }
    data.Close();
}

static void GetAllReceipts(SqlConnection sqlConnection, ref SqlDataReader data)
{
    var sqlCommand = new SqlCommand("select * from Facturas", sqlConnection);
    data = sqlCommand.ExecuteReader();

    if (data.HasRows)
    {
        Console.WriteLine("\n");
        Console.WriteLine("---------------------Facturas--------------------");
        while (data.Read())
        {
            Console.WriteLine($"ID: {data["CodFactura"]}\n" +
                              $"Codigo de hoja: {data["CodHoja"]}\n" +
                              $"Codigo Mecanico: {data["CodMecanico"]} \n " +
                              $"CI Cliente: {data["CI_Clientes"]}\n" +
                              $"Repuesto: {data["NombreRIM"]}\n" +
                              $"Precio: {data["PrecioMO"]}");
            Console.WriteLine("----------------------------------------------");
        }
        MainMenu();
    }
    else
    {
        
        Console.WriteLine("No hay facturas registradas");
    }
    data.Close();
}

static void GetAllSpareParts(SqlConnection sqlConnection, ref SqlDataReader data)
{
    var sqlCommand = new SqlCommand("select * from RepuestosInsumosMateriales", sqlConnection);
    data = sqlCommand.ExecuteReader();

    if (data.HasRows)
    {
        Console.WriteLine("\n");
        Console.WriteLine("---------------------Repuestos--------------------");
        while (data.Read())
        {
            Console.WriteLine($"Codigo repuesto: {data["CodRep_Ins_Mat"]} \n" +
                              $"Codigo de hoja: {data["CodHoja"]}\n" +
                              $"Breve descripcion: {data["Nombre"]}\n" +
                              $"Precio: {data["Precio"]}");
            Console.WriteLine("=================================================");
        }
        MainMenu();
    }
    else
    {
        
        Console.WriteLine("No hay datos registrados");
    }
    data.Close();
}

static void GetAllDetails(SqlConnection sqlConnection, ref SqlDataReader data)
{
    var sqlCommand = new SqlCommand("select * from Detalle", sqlConnection);
    data = sqlCommand.ExecuteReader();

    if (data.HasRows)
    {
        Console.WriteLine("\n");
        Console.WriteLine("---------------------Detalle--------------------");
        while (data.Read())
        {
            Console.WriteLine($"Codigo detalle: {data["CodDetalle"]}\n " +
                              $"Codigo de hoja: {data["CodHoja"]}\n" +
                              $"Codigo de Repuesto: {data["CodRep_Ins_Mat"]}\n" +
                              $"Cantidad: {data["Cantidad"]}");
            Console.WriteLine("---------------------------------------------");
        }
        MainMenu();
    }
    else
    {
        
        Console.WriteLine("No existen detalles");
    }
    data.Close();
}

static void ProcedureListaVendedores(SqlConnection sqlConnection, ref SqlDataReader data) 
{
    Console.WriteLine("Ejecutamos un procedimiento almacenado sin parametros: ");
    var sqlCommand = new SqlCommand("execute ListaVendedores", sqlConnection);
    data = sqlCommand.ExecuteReader();
    if (!data.HasRows)
    {
        Console.WriteLine("No existen datos");
    }
    else
    {
        while (data.Read())
        {
            Console.WriteLine($"Codigo vendedor: {data["CodVendedor"]} \nNIT Concesionaria: {data["NIT_CON"]} \nNombre: {data["NombreV"]}");
            Console.WriteLine("----------------------------------------------------------");
        }
    }
    data.Close();

    Console.WriteLine("*________Procedimiento Lista Vendedores ejecutada________*");
    Console.ReadKey();
}