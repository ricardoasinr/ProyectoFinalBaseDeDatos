﻿using Microsoft.Data.SqlClient;

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

int a = MenuPrincipal();
switch (a)
{
    case 1:
        MenuAdministrador(sqlConnection, ref data);
        break;
    case 2:
        Console.WriteLine("Menu tanto");
        break;
    case 3:
        MenuInformacion(sqlConnection, ref data);
        break;
    default:
        break;
    
}


static void Atras()
{
    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
    Console.WriteLine("Toque cualquier tecla para volver atras");
    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
}

static void MenuAdministrador(SqlConnection sqlConnection, ref SqlDataReader data)
{
    int op;
    do
    {
        Console.Clear();
        Console.WriteLine("--------------Taller----------------");
        Console.WriteLine("1. Listado de clientes");
        Console.WriteLine("2. Listado de vehiculos");
        Console.WriteLine("3. Listado de vendedores");
        Console.WriteLine("4. Listado de mecanicos");
        Console.WriteLine("5. Facturas");
        Console.WriteLine("6. Repuestos");
        Console.WriteLine("7. Detalles");
        Console.WriteLine("8. Hojas de parte");
        Console.WriteLine("------------------------------------");
        Console.WriteLine("Registros: ");
        Console.WriteLine("------------------------------------");
        Console.WriteLine("0. Salir");
        Console.WriteLine("------------------------------------");
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
                
                ProcedureListaVendedores(sqlConnection, ref data);
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
                GetAllDataSheets(sqlConnection, ref data);
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

static int MenuPrincipal()
{
    int op;
    do
    {
        Console.Clear();
        Console.WriteLine("------------------------------");
        Console.WriteLine("1. Administrador");
        Console.WriteLine("2. Registro");
        Console.WriteLine("3. Ver informacion");
        Console.WriteLine("------------------------------");
        
        op = Convert.ToInt32(Console.ReadLine());
        Console.ReadLine();
        if (op > 3 || op <= 0)
        {
            Console.WriteLine("Ingrese un numero valido");
        }
    } while (op >3 || op <= 0);
    return op;
}

static void MenuInformacion(SqlConnection sqlConnection, ref SqlDataReader data)
{
    int op;
    do
    {
        Console.Clear();
        Console.WriteLine("--------------Informacion----------------");
        Console.WriteLine("1. Listado de clientes");
        Console.WriteLine("2. Listado de vehiculos");
        Console.WriteLine("3. Listado de vendedores");
        Console.WriteLine("4. Listado de mecanicos");
        Console.WriteLine("5. Vehiculos vendidos");
        Console.WriteLine("0. Salir");
        Console.WriteLine("------------------------------------");
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
                
                ProcedureListaVendedores(sqlConnection, ref data);
                break;
            case 4:
                GetAllMechanics(sqlConnection, ref data);
                break;
            case 5:
                GetAllSales(sqlConnection, ref data);
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

//static void Registros(SqlConnection sqlConnection, ref SqlDataReader data);

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
            Atras();
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
        Atras();
        
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
        Atras();
        
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
        Atras();
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
        Atras();
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
        Atras();
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
        Atras();
    }
    else
    {
        
        Console.WriteLine("No existen detalles");
    }
    data.Close();
}

static void GetAllSales(SqlConnection sqlConnection, ref SqlDataReader data)
{
    var sqlCommand = new SqlCommand("select * from VehiculosVendidos", sqlConnection);
    data = sqlCommand.ExecuteReader();

    if (data.HasRows)
    {
        Console.WriteLine("\n");
        Console.WriteLine("---------------------Vehiculos vendidos--------------------");
        while (data.Read())
        {
            Console.WriteLine($"Matricula: {data["Matricula"]}\n" +
                              $"Codigo Modelo: {data["CodModelo"]}\n" +
                              $"CI Clientes: {data["CI_Clientes"]}\n" +
                              $"Fabricacion: {data["FechaFabri"]}\n" +
                              $"Color: {data["Color"]}");
            Console.WriteLine("---------------------------------------------");
        }
        Atras();
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