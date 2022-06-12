using Microsoft.Data.SqlClient;

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

//Menu(sqlConnection, ref data);
//ProcedureRegistroClientes(sqlConnection, ref data);
//ProcedureRegistroAutos(sqlConnection, ref data);
int m = MenuTaller(sqlConnection, ref data);
if (m == 1)
{
    
    Console.WriteLine("Iniciando registro");
    String id = ProcedureRegistroClientes(sqlConnection, ref data);
    Console.Write("\nCargando mecanicos...");
    Console.ReadKey();
    GetAllMechanics(sqlConnection, ref data);
    Console.Write("Seleccione un mecanico \nDigite el id del mecanico: ");
    String idMecanico = Console.ReadLine();
    Console.ReadKey();
    //Asignar mecanico
    Console.WriteLine("\t");
    Console.WriteLine("------------------------------------");
    Console.Write("Mecanico: ");
    Console.WriteLine(idMecanico);
    Console.WriteLine("------------------------------------");
    
    Console.WriteLine("Rellene de acuerdo a las necesidades del cliente");
    //Registro de Hoja de parte
    
    




}

if (m == 2)
{
    Console.Write("\nDigite el carnet del cliente: ");
    String CiCliente = Console.ReadLine();
    GetAllClient(sqlConnection, ref data, CiCliente);
    Console.Write("\nCargando mecanicos...");
    Console.ReadKey();
    GetAllMechanics(sqlConnection, ref data);
   
    Console.Write("Seleccione un mecanico \nDigite el id del mecanico: ");
    String idMecanico = Console.ReadLine();
    Console.ReadKey();
    //Asignar mecanico
    Console.WriteLine("\t");
    Console.WriteLine("------------------------------------");
    Console.Write("Mecanico: ");
    Console.WriteLine(idMecanico);
    Console.WriteLine("------------------------------------");
    
    Console.WriteLine("Rellene de acuerdo a las necesidades del cliente");
    //Registro de Hoja de parte
    
    
    
}


static void Atras()
{
    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
    Console.WriteLine("Toque cualquier tecla para volver atras");
    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
}

static int MenuTaller(SqlConnection sqlConnection, ref SqlDataReader data)
{
    int op;
    int i;
    do
    {
        Console.Clear();
        Console.WriteLine("--------------Taller----------------");
        Console.WriteLine("1. Listado de clientes");
        Console.WriteLine("2. Listado de vehiculos");
        Console.WriteLine("3. Listado de Repuestos");
        Console.WriteLine("4. Listado de mecanicos");
        Console.WriteLine("5. Listado de Facturas");
        Console.WriteLine("------------------------------------");
        Console.WriteLine("6. Iniciar registro de reparacion");
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
                GetAllSpareParts(sqlConnection, ref data);
                break;
            case 4:
                GetAllMechanics(sqlConnection, ref data);
                break;
            case 5:
                GetAllReceipts(sqlConnection, ref data);
                break;
            case 6:
                i = IniciarReparacion(sqlConnection, ref data);
                return i;
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

    return 0;
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
            
        }

    }
    //Atras();
    else
    {
        Console.WriteLine("No existen clientes");
    }
    data.Close();
}

static void GetAllClient(SqlConnection sqlConnection, ref SqlDataReader data, String CI)
{
    var sqlCommand = new SqlCommand($"select * from Clientes WHERE  CI_Clientes = '{CI}';", sqlConnection);
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
        }

    }
    else
    {
        Console.WriteLine("No se encuentra el cliente");
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
                              $"Color: {data["Color"]}\n" +
                              $"Estado: {data["Estado"]}");

            Console.WriteLine("----------------------------------------");

        }
        Atras();

    }
    else
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
                              $"Fecha de ingreso: {data["FechaIngreso"]}" +
                              $" Hora: {data["HoraIngreso"]}\n" +
                              $"Mecanico encargado: {data["CodMecanico"]}\n" +
                              $"Costo de mano de obra: {data["Manodeobra"]}");
            Console.WriteLine("--------------------------------------------");
        }
        Atras();

    }
    else
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
                              $"Especialidad: {data["Especialidad"]}\n" +
                              $"Nombre: {data["NombreMec"]}" +
                              $" {data["ApellidoMec"]} \n" +
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

static int IniciarReparacion(SqlConnection sqlConnection, ref SqlDataReader data)
{
    int op;
    do
    {
    Console.Clear();
    Console.WriteLine("--------------Reparacion----------------");
    Console.WriteLine("Cliente nuevo?");
    Console.WriteLine("1. Si \t 2.No");
    Console.WriteLine("0. Salir");
    Console.WriteLine("------------------------------------");
    op = Convert.ToInt32(Console.ReadLine());
    Console.ReadLine();
    } while (op < 0 || op > 2);

    return op;

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

static void RegisterSellers(SqlConnection sqlConnection, ref SqlDataReader data)
{
    Console.WriteLine("Registro de vendedores: ");
    Console.WriteLine("Nombre: ");
    string nombre = Console.ReadLine();
    Console.WriteLine(nombre);

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

    Console.WriteLine("__Procedimiento Lista Vendedores ejecutada__");
    Console.ReadKey();
}

static string ProcedureRegistroClientes(SqlConnection sqlConnection, ref SqlDataReader data)
{
    Console.WriteLine("---------------------Registro de Clientes--------------------");
    Console.Write("Carnet: ");
    string carnet = Console.ReadLine();

    Console.Write("Factura: ");
    string factura = Console.ReadLine();


    Console.Write("Nombre: ");
    string nombre = Console.ReadLine();

    Console.Write("Telefono: ");
    string telef = Console.ReadLine();

    Console.Write("Direccion: ");
    string Direccion = Console.ReadLine();



    var sqlCommand = new SqlCommand($"exec RegistroCliente @p_CI_Clientes = '{carnet}', " +
                                    $"@p_Factura  = '{factura}'," +
                                    $"@p_NombreClie  = '{nombre}'," +
                                    $"@p_TelefonoCli  = '{telef}'," +
                                    $"@p_DireccionCli = '{Direccion}';", sqlConnection);
    data = sqlCommand.ExecuteReader();
    data.Close();


    Console.ReadKey();

    return carnet;

}

static void ProcedureRegistroAutos(SqlConnection sqlConnection, ref SqlDataReader data)
{
    Console.WriteLine("---------------------Registro del vehiculo del cliente--------------------");
    Console.Write("Matricula: ");
    string matricula = Console.ReadLine();

    Console.Write("Codigo Modelo: ");
    string codmodelo = Console.ReadLine();


    Console.Write("Carnet del cliente: ");
    string cicliente = Console.ReadLine();

    Console.Write("Fecha de fabricacion del vehiculo(ej: 20201103): AAAAMMDD ");
    string fechafab = Console.ReadLine();

    Console.Write("Color: ");
    string color = Console.ReadLine();


    var sqlCommand = new SqlCommand($"exec RegistroVehiculo @p_Matricula = '{matricula}', " +
                                    $"@p_CodModelo  = '{codmodelo}'," +
                                    $"@p_CI_Clientes  = '{cicliente}'," +
                                    $"@p_FechaFabri  = '{fechafab}'," +
                                    $"@p_Color = '{color}';", sqlConnection);
    data = sqlCommand.ExecuteReader();
    data.Close();


    Console.ReadKey();



}

static void ProcedureMecanicos(SqlConnection sqlConnection, ref SqlDataReader data)
{
    Console.WriteLine("---------------------Seleccion de mecanico--------------------");

    var sqlCommand = new SqlCommand("select Disponibilidad from Mecanicos", sqlConnection);
    data = sqlCommand.ExecuteReader();

    

}

static void ProcedureFacturas(SqlConnection sqlConnection, ref SqlDataReader data)
{
    Console.WriteLine("---------------------FACTURA--------------------");
    Console.Write("Codigo de factura: ");
    string codfactura = Console.ReadLine();

    Console.Write("Codigo hoja de parte: ");
    string codhoja = Console.ReadLine();


    Console.Write("Codigo del mecanico: ");
    string codmecanico = Console.ReadLine();

    Console.Write("carnet del cliente ");
    string cicliente = Console.ReadLine();

    Console.Write("Nombre del repuesto/insumo/material: ");
    string nombrerim = Console.ReadLine();

    Console.Write("Precio: ");
    string preciorim = Console.ReadLine();

    Console.Write("Precio mano de obra: ");
    string preciomo = Console.ReadLine();


    Console.Write("Precio total: ");
    string preciotot = Console.ReadLine();
    /*
    Console.WriteLine("Precio total con el IVA:");
    string precioiva = Console.ReadLine();
    */


    /*var sqlCommand = new SqlCommand($"exec RegistroFactura @p_CodFactura = '{codfactura}', " +
                                    $"@p_CodHoja  = '{codhoja}'," +
                                    $"@p_CodMecanico  = '{codmecanico}'," +
                                    $"@p_CI_Clientes  = '{cicliente}'," +
                                    $"@p_NombreRIM = '{nombrerim}'," +
                                    $"@p_PrecioRIM  = '{preciorim}'," +
                                    $"@p_PrecioMO  = '{cicliente}'," +
                                    $"@p_PrecioTOT  = '{preciotot}'," +
                                    $"@p_PrecioIVA = '{precioiva}';" , sqlConnection);
    data = sqlCommand.ExecuteReader();
    data.Close();


    Console.ReadKey();

    */

}