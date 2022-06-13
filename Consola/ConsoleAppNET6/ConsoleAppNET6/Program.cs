using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

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


//GetAMechanicForRegister(sqlConnection, ref data, "M003");
//GetAReceipt(sqlConnection, ref data, "FR0005");
//Receipts(sqlConnection, ref data, "FR1115", "H888", "8127786", "R105");

menuPrincipal(sqlConnection, ref data);



static void menuPrincipal(SqlConnection sqlConnection, ref SqlDataReader data)
{
    int Op, mt;
    do
    {
        Console.Clear();
        Console.WriteLine("------------------------------------");
        Console.WriteLine("1. Taller");
        Console.WriteLine("2. Concesionaria");
        Console.WriteLine("0. Salir");
        Console.WriteLine("------------------------------------");
        Op = Convert.ToInt32(Console.ReadLine());
        switch (Op)
        {
            case 1:
                mt = MenuTaller(sqlConnection, ref data);
                if (mt == 1)
                {
                    
                    Console.WriteLine("Iniciando registro");
                    String id = RegisterClients(sqlConnection, ref data);
                    Console.Write("Ingrese su matricula: ");
                    string matricula = Console.ReadLine();
                    Console.Write("\nCargando mecanicos...");
                    Console.ReadKey();
                    GetAllMechanicsFree(sqlConnection, ref data);
                    
                    Console.Write("Seleccione un mecanico \nDigite el id del mecanico: ");
                    String idMecanico = Console.ReadLine();
                    Console.ReadKey();
                    //Asignar mecanico
                    Console.WriteLine("\t");
                    Console.WriteLine("------------------------------------");
                    Console.Write("ID del Mecánico : ");
                    Console.WriteLine(idMecanico);
                    GetAMechanic(sqlConnection, ref data, idMecanico);
                    Console.ReadKey();
                    
                    Console.WriteLine("Rellene de acuerdo a las necesidades del cliente");
                    string codigoHoja = RegisterDataSheets(sqlConnection, ref data, idMecanico, matricula);
                    Console.ReadKey();
                    
                    Console.Write("Codigo detalle: ");
                    string codigoDetalle = Console.ReadLine();
                    Console.WriteLine("Ingrese los repuestos");
                    Console.ReadKey();
                    string codigoRepuesto = RegisterSpareParts(sqlConnection, ref data, codigoHoja);
                    //GetAllSpareParts(sqlConnection, ref data);
                    Console.ReadKey();
                    Console.Write("Cantidad: ");
                    string cantidad = Console.ReadLine();
                    RegisterDetails(sqlConnection, ref data, codigoDetalle, codigoHoja,codigoRepuesto,cantidad);
                    GetADetail(sqlConnection, ref data, codigoDetalle);
                    Console.ReadKey();
                    Console.WriteLine("Generando factura");
                    Console.ReadKey();
                    Console.Write("Codigo de la factura: ");
                    string Factura = Console.ReadLine();
                    Receipts(sqlConnection, ref data, Factura,codigoHoja,id,codigoRepuesto);
                    AvailableMechanic(sqlConnection, ref data, idMecanico);
                    Console.ReadKey();
                    GetAReceipt(sqlConnection, ref data, Factura);
                    mt = MenuTaller(sqlConnection, ref data);
                }
                if (mt == 2)
                {
                    Console.Write("\nDigite el carnet del cliente: ");
                    String CiCliente = Console.ReadLine();
                    GetAClient(sqlConnection, ref data, CiCliente);
                    
                    Console.Write("Ingrese su matricula: ");
                    string matricula = Console.ReadLine();
                    Console.Write("\nCargando mecanicos...");
                    Console.ReadKey();
                    
                    GetAllMechanicsFree(sqlConnection, ref data);
                   
                    Console.Write("Seleccione un mecanico \nDigite el id del mecanico: ");
                    String idMecanico = Console.ReadLine();
                    Console.ReadKey();
                    //Asignar mecanico
                    Console.WriteLine("\t");
                    
                    Console.Write("ID del Mecánico : ");
                    Console.WriteLine(idMecanico);
                    GetAMechanic(sqlConnection, ref data, idMecanico);
                
                    Console.ReadKey();
                    
                    Console.WriteLine("Rellene de acuerdo a las necesidades del cliente");
                    string codigoHoja = RegisterDataSheets(sqlConnection, ref data, idMecanico, matricula);
                    Console.ReadKey();
                    
                    Console.Write("Codigo detalle: ");
                    string codigoDetalle = Console.ReadLine();
                    Console.WriteLine("Ingrese los repuestos");
                    Console.ReadKey();
                    string codigoRepuesto = RegisterSpareParts(sqlConnection, ref data, codigoHoja);
                    //GetAllSpareParts(sqlConnection, ref data);
                    Console.ReadKey();
                    Console.Write("Cantidad: ");
                    string cantidad = Console.ReadLine();
                    RegisterDetails(sqlConnection, ref data, codigoDetalle, codigoHoja,codigoRepuesto,cantidad);
                    GetADetail(sqlConnection, ref data, codigoDetalle);
                    Console.ReadKey();
                    Console.WriteLine("Generando factura");
                    Console.ReadKey();
                    Console.Write("Codigo de la factura: ");
                    string Factura = Console.ReadLine();
                    Receipts(sqlConnection, ref data, Factura,codigoHoja,CiCliente,codigoRepuesto);
                    Console.ReadKey();
                    GetAReceipt(sqlConnection, ref data, Factura);
                    AvailableMechanic(sqlConnection, ref data, idMecanico);
                    
                    mt = MenuTaller(sqlConnection, ref data);
                    

                }
                break;
            case 2:
                MenuConcesionaria(sqlConnection, ref data);
                break;
            
            case 0:
                Console.WriteLine("Terminado...");
                break;
            default:
                Console.WriteLine("Opcion invalida");
                break;
        }
        Console.ReadLine();
    } while (Op != 0);

    
    
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
        Console.WriteLine("6. Listado de hojas de parte");
        Console.WriteLine("------------------------------------");
        Console.WriteLine("7. Iniciar registro de reparacion");
        Console.WriteLine("8. Registrar mecanico");
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
                GetAllDataSheets(sqlConnection, ref data);
                break;
            case 7:
                i = IniciarReparacion(sqlConnection, ref data);
                return i;
                break;
            case 8:
                string codigoM = RegisterMechanics(sqlConnection, ref data);
                GetAMechanicForRegister(sqlConnection, ref data, codigoM);
                break;


            case 0:
                menuPrincipal(sqlConnection, ref data);
                break;
            default:
                Console.WriteLine("Opcion invalida");
                break;
        }
        Console.ReadLine();
    } while (op != 0);

    return 0;
}
static void MenuConcesionaria(SqlConnection sqlConnection, ref SqlDataReader data)
{
    int op;
    int i;
    do
    {
        Console.Clear();
        Console.WriteLine("--------------Concesionaria----------------");
        Console.WriteLine("1. Listado de vehiculos");
        Console.WriteLine("2. Listado de vendedores");
        Console.WriteLine("3. Listado de modelos");
        Console.WriteLine("4. Listado de servicio oficial");
        Console.WriteLine("5. Listado de vehiculos vendidos");
        Console.WriteLine("------------------------------------");
        Console.WriteLine("6. Iniciar registro de vehiculos");
        Console.WriteLine("7. Iniciar registro de vendedores");
        Console.WriteLine("8. Iniciar registro de vehiculos vendidos");
        Console.WriteLine("9. Iniciar registro de modelos");
        Console.WriteLine("10. Iniciar registro de servicio oficial");

        Console.WriteLine("------------------------------------");
        Console.WriteLine("0. Salir");
        Console.WriteLine("------------------------------------");
        op = Convert.ToInt32(Console.ReadLine());
        switch (op)
        {
            case 1:
                GetAllCars(sqlConnection, ref data);
                break;
            case 2:
                GetAllSellers(sqlConnection, ref data);
                break;
            case 3:
                GetAllModels(sqlConnection, ref data);
                break;
            case 4:
                GetAllOficialServices(sqlConnection, ref data);
                break;
            case 5:
                GetAllSales(sqlConnection, ref data);
                break;
            case 6:
                string chasis = RegisterCars(sqlConnection, ref data);
                GetACar(sqlConnection, ref data, chasis);
                break;
            case 7:
                string vendedor = RegisterSellers(sqlConnection, ref data);
                GetASeller(sqlConnection, ref data, vendedor);
                break;
            case 8:
                string vendido = RegisterSoldCars(sqlConnection, ref data);
                GetASoldCar(sqlConnection, ref data, vendido);
                break;
            case 9:
                RegisterModels(sqlConnection, ref data);
                
                break;
            case 10:
                RegisterOficialServices(sqlConnection, ref data);
                break;
            case 0:
                menuPrincipal(sqlConnection, ref data);
                break;
            default:
                Console.WriteLine("Opcion invalida");
                break;
        }
        Console.ReadLine();
    } while (op != 0);

  
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
static void GetAClient(SqlConnection sqlConnection, ref SqlDataReader data, String CI)
{
    var sqlCommand = new SqlCommand($"select * from Clientes WHERE  CI_Clientes = '{CI}'", sqlConnection);
    data = sqlCommand.ExecuteReader();

    if (data.HasRows)
    {
        while (data.Read())
        {
            Console.WriteLine("\n");
            Console.WriteLine("----------------Cliente----------------");
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
                              $"Color: {data["E"]}\n" +
                              $"Estado: {data["Estado"]}");

            Console.WriteLine("----------------------------------------");

        }
    

    }
    else
    {
        Console.WriteLine("No hay vehiculos registrados");
    }
    data.Close();
}
static void GetACar(SqlConnection sqlConnection, ref SqlDataReader data, string chasis)
{
    SqlCommand sqlCommand = new SqlCommand($"select * from Automoviles WHERE  NroChasis = '{chasis}'", sqlConnection);
    data = sqlCommand.ExecuteReader();
    try
    {
        if (data.HasRows)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Registro exitoso ");
            Console.WriteLine("----------------Vehiculo---------------");
            while (data.Read())
            {

                Console.WriteLine($"Numero de chasis: {data["NroChasis"]}\n" +
                                  $"NIT Concesionaria: {data["NIT_CON"]}\n" +
                                  $"Codigo Modelo: {data["CodModelo"]}\n" +
                                  $"Fecha: {data["FechaFabri"]} \n" +
                                  $"Color: {data["E"]}\n" +
                                  $"Estado: {data["Estado"]}");
                Console.WriteLine("----------------------------------------");

            }
            
        }
        else
        {
            Console.WriteLine("No hay vehiculos registrados");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("No se pudo registrar");
        Console.WriteLine(e);
        throw;
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
                              $"Disponibilidad: {data["Disponibilidad"]}");
            Console.WriteLine("-------------------------------------------------");

        }
     
    }
    else
    {

        Console.WriteLine("No existen datos");
    }
    data.Close();
}
static void GetAllMechanicsFree(SqlConnection sqlConnection, ref SqlDataReader data)
{
    var sqlCommand = new SqlCommand("select * from Mecanicos where Disponibilidad = 'Libre'", sqlConnection);
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
                              $"Disponibilidad: {data["Disponibilidad"]}");
            Console.WriteLine("-------------------------------------------------");

        }
     
    }
    else
    {

        Console.WriteLine("No existen datos");
    }
    data.Close();
}
static void GetAMechanicForRegister(SqlConnection sqlConnection, ref SqlDataReader data, String codigo)
{
    var sqlCommand = new SqlCommand($"select * from Mecanicos WHERE CodMecanico = '{codigo}';", sqlConnection);
    data = sqlCommand.ExecuteReader();
    try
    {
        if (data.HasRows)
        {
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Mecanico registrado exitosamente");
            Console.WriteLine("-------------------------------------------------");
            Console.ReadKey();
            while (data.Read())
            {
                Console.Write("Nombre: ");
                Console.WriteLine($"{data["NombreMec"]}" +
                                  $" {data["ApellidoMec"]} \n" +$"Codigo: {data["CodMecanico"]}\n" +
                                  $"Servicio Oficial: {data["NITSO"]} \n" +
                                  $"Celular: {data["TelefonoMec"]}\n" +
                                  $"Direccion: {data["DireccionMec"]}\n" +
                                  $"Especialidad: {data["Especialidad"]}\n" +
                                  $"Disponibilidad: {data["Disponibilidad"]}\n");
                Console.WriteLine("-------------------------------------------------");
                Console.ReadKey();
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
    
    data.Close();
}
static void GetAMechanic(SqlConnection sqlConnection, ref SqlDataReader data, String CM)
{
    var sqlCommand = new SqlCommand($"select * from Mecanicos WHERE CodMecanico = '{CM}';", sqlConnection);
    var sqlCommand2 = new SqlCommand($"update Mecanicos set Disponibilidad = 'Ocupado' where CodMecanico = '{CM}'",
        sqlConnection);
    data = sqlCommand.ExecuteReader();
    

    if (data.HasRows)
    {
        while (data.Read())
        {
            Console.Write("Mecanico seleccionado: ");
        Console.WriteLine($"{data["NombreMec"]}" +
                          $" {data["ApellidoMec"]} \n" +
                          $"Especialidad: {data["Especialidad"]}\n");
        Console.WriteLine("-------------------------------------------------");
        }
    }
    else
    {

        Console.Write("No existe el mecánico con codigo: [");
        Console.Write(CM);
        Console.WriteLine("]");
    }
    data = sqlCommand2.ExecuteReader();
    data.Close();
}
static void GetAllReceipts(SqlConnection sqlConnection, ref SqlDataReader data)
{
    var sqlCommand = new SqlCommand("select * from Factura", sqlConnection);
    data = sqlCommand.ExecuteReader();

    if (data.HasRows)
    {
        Console.WriteLine("\n");
        Console.WriteLine("---------------------Facturas--------------------");
        while (data.Read())
        {
            Console.WriteLine($"ID: {data["CodFactura"]}\n" +
                              $"Codigo de hoja: {data["CodHoja"]}\n" +
                              $"CI Cliente: {data["CI_Clientes"]}\n" +
                              $"Codigo Repuesto: {data["codRepuesto"]}\n" +
                              $"TOTAL A PAGAR: {data["TotalConIVA"]}");
            Console.WriteLine("----------------------------------------------");
        }
    }
    else
    {

        Console.WriteLine("No hay facturas registradas");
    }
    data.Close();
}
static void GetAReceipt(SqlConnection sqlConnection, ref SqlDataReader data, string factura)
{
    var sqlCommand = new SqlCommand($"select * from Factura  WHERE CodFactura = '{factura}'; ", sqlConnection);
    data = sqlCommand.ExecuteReader();

    if (data.HasRows)
    {
        Console.WriteLine("\n");
        Console.WriteLine("---------------------Factura--------------------");
        while (data.Read())
        {
            Console.WriteLine($"ID: {data["CodFactura"]}\n" +
                              $"Codigo de hoja: {data["CodHoja"]}\n" +
                              $"CI Cliente: {data["CI_Clientes"]}\n" +
                              $"Codigo Repuesto: {data["codRepuesto"]}\n" +
                              $"TOTAL A PAGAR: {data["TotalConIVA"]}");
            Console.WriteLine("----------------------------------------------");
            Console.ReadKey();
        }
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
                              $"Breve descripcion: {data["Nombre"]}\n" +
                              $"Precio: {data["Precio"]}\n" +
                              $"Stock: {data["Stock"]}");
            Console.WriteLine("=================================================");
        }
    }
    else
    {

        Console.WriteLine("No hay datos registrados");
    }
    data.Close();
}
static void GetASeller(SqlConnection sqlConnection, ref SqlDataReader data, string vendedor)
{
    SqlCommand sqlCommand = new SqlCommand($"select * from Vendedores WHERE  CodVendedor = '{vendedor}'", sqlConnection);
    data = sqlCommand.ExecuteReader();
    try
    {
        if (data.HasRows)
        {
            Console.WriteLine("\n");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Vendedor registrado exitosamente");
            Console.WriteLine("-------------------------------------------------");
            Console.ReadKey();
            while (data.Read())
            {
                Console.WriteLine($"Codigo del vendedor: {data["CodVendedor"]}\n" +
                                  $"NIT Concesionaria: {data["NIT_CON"]}\n" +
                                  $"NIT Servicio Oficial: {data["NITSO"]}\n" +
                                  $"Nombre: {data["NombreV"]}\n" +
                                  $"Telefono: {data["TelefonoV"]}\n" +
                                  $"Direccion: {data["DireccionV"]}\n" +
                                  $"Email: {data["EmailV"]}");
                Console.WriteLine("---------------------------------------------");
            }
            
        }
        
    }
    catch (Exception e)
    {
        Console.WriteLine("No se pudo registrar");
        Console.WriteLine(e);
        throw;
    }
    
    data.Close();
}
static void GetASoldCar(SqlConnection sqlConnection, ref SqlDataReader data, string matricula)
{
    SqlCommand sqlCommand = new SqlCommand($"select * from VehiculosVendidos WHERE  Matricula = '{matricula}'", sqlConnection);
    data = sqlCommand.ExecuteReader();
    try
    {
        if (data.HasRows)
        {
            Console.WriteLine("\n");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Vehiculo vendido  exitosamente");
            Console.WriteLine("-------------------------------------------------");
            Console.ReadKey();
            Console.WriteLine("---------------------Vehiculo vendido--------------------");
            while (data.Read())
            {
                Console.WriteLine($"Matricula: {data["Matricula"]}\n" +
                                  $"Codigo modelo: {data["CodModelo"]}\n" +
                                  $"Carnet del cliente : {data["CI_Clientes"]}\n" +
                                  $"Fecha de fabricacion: {data["FechaFabri"]}\n" +
                                  $"Color: {data["Color"]}");
                Console.WriteLine("---------------------------------------------");
            }
            
        }
        
    }
    catch (Exception e)
    {
        Console.WriteLine("No se pudo registrar");
        Console.WriteLine(e);
        throw;
    }
    
    data.Close();
}
static void GetADetail(SqlConnection sqlConnection, ref SqlDataReader data, string codigoDetalle )
{
    var sqlCommand = new SqlCommand($"select * from Detalle where CodDetalle = '{codigoDetalle}'", sqlConnection);
    data = sqlCommand.ExecuteReader();
    
    if (data.HasRows)
    {    Console.WriteLine("\n");
        Console.WriteLine("---------------------Detalle--------------------");
        while (data.Read())
        {
            Console.WriteLine($"Codigo detalle: {data["CodDetalle"]}\n" +
                              $"Codigo de hoja: {data["CodHoja"]}\n" +
                              $"Codigo de Repuesto: {data["CodRep_Ins_Mat"]}\n" +
                              $"Cantidad: {data["Cantidad"]}");
            Console.WriteLine("---------------------------------------------");
        }
      
    }
    else
    {

        Console.WriteLine("No existen detalles");
    }
    data.Close();
}
static void GetAllSellers(SqlConnection sqlConnection, ref SqlDataReader data)
{
        var sqlCommand = new SqlCommand("execute ListaVendedores", sqlConnection);
        data = sqlCommand.ExecuteReader();

        if (data.HasRows)
        {
            Console.WriteLine("\n");
            Console.WriteLine("---------------------Vendedores--------------------");
            while (data.Read())
            {
                Console.WriteLine($"Codigo del vendedor: {data["CodVendedor"]}\n" +
                                  $"NIT Concesionaria: {data["NIT_CON"]}\n" +
                                  $"NIT Servicio Oficial: {data["nitso"]}\n" +
                                  $"Nombre: {data["NombreV"]}\n" +
                                  $"Telefono: {data["TelefonoV"]}\n" +
                                  $"Email: {data["EmailV"]}");
                Console.WriteLine("---------------------------------------------");
            }
          
        }
        else
        {

            Console.WriteLine("No existen vendedores");
        }
        data.Close();
        Console.ReadKey();
}
static void GetAllModels(SqlConnection sqlConnection, ref SqlDataReader data)
{
    var sqlCommand = new SqlCommand("select * from Modelos", sqlConnection);
    data = sqlCommand.ExecuteReader();

    if (data.HasRows)
    {
        Console.WriteLine("\n");
        Console.WriteLine("---------------------Modelos--------------------");
        while (data.Read())
        {
            Console.WriteLine($"Codigo modelo: {data["CodModelo"]}\n" +
                              $"Marca: {data["Marca"]}\n" +
                              $"Modelo: {data["Modelo"]}\n" +
                              $"Numero de Puertas: {data["NroPuertas"]}\n" +
                              $"Cilindradas: {data["Cilindradas"]}\n" +
                              $"Numero de Ruedas: {data["NroRuedas"]}\n" +
                              $"Procedencia: {data["Procedencia"]}\n" +
                              $"Cantidad de pasajeros: {data["CantPasajeros"]}");
            Console.WriteLine("---------------------------------------------");
        }
    }
    else
    {

        Console.WriteLine("No existen detalles");
    }
    data.Close();
}
static void GetAllOficialServices(SqlConnection sqlConnection, ref SqlDataReader data)
{
    var sqlCommand = new SqlCommand("select * from ServicioOficial", sqlConnection);
    data = sqlCommand.ExecuteReader();

    if (data.HasRows)
    {
        Console.WriteLine("\n");
        Console.WriteLine("---------------------Servicios Oficiales--------------------");
        while (data.Read())
        {
            Console.WriteLine($"NIT Servicio Oficial: {data["NITSO"]}\n" +
                              $"NIT Concesionaria: {data["NIT_CON"]}\n" +
                              $"Nombre del Servicio Oficial: {data["NombreSO"]}\n" +
                              $"Direccion Servicio Oficial: {data["DireccionSO"]}\n" +
                              $"Telefono Servicio Oficial: {data["TelefonoSO"]}\n" +
                              $"Email Servicio Oficial: {data["EmailSO"]}\n" +
                              $"Taller Servicio Oficial: {data["TalleresSO"]}");
            Console.WriteLine("---------------------------------------------");
        }
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
       
    }
    else
    {

        Console.WriteLine("No existen detalles");
    }
    data.Close();
}
static string RegisterSellers(SqlConnection sqlConnection, ref SqlDataReader data)
{
    Console.WriteLine("---------------------Registro de vendedores--------------------");
    Console.Write("Codigo del vendedor: ");
    string codVendedor = Console.ReadLine();

    Console.Write("Codigo de la concesionaria: ");
    string codConcesionaria = Console.ReadLine();

    Console.Write("NIT servicio oficial: ");
    string codServOf = Console.ReadLine();

    Console.Write("Nombre del vendedor: ");
    string nombre = Console.ReadLine();

    Console.Write("Numero: ");
    string numero = Console.ReadLine();

    Console.Write("Direccion: ");
    string direccion = Console.ReadLine();

    Console.Write("Email: ");
    string email = Console.ReadLine();

    var sqlCommand = new SqlCommand($"insert into Vendedores values ('{codVendedor}'," +
                                    $"'{codConcesionaria}'," +
                                    $"'{codServOf}'," +
                                    $"'{nombre}', " +
                                    $"'{numero}'," +
                                    $"'{direccion}'," +
                                    $"'{email}');", sqlConnection);
    data = sqlCommand.ExecuteReader();
    data.Close();


    Console.ReadKey();

    return codVendedor;
}
static string RegisterClients(SqlConnection sqlConnection, ref SqlDataReader data)
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
static void Receipts(SqlConnection sqlConnection, ref SqlDataReader data, string factura, string codHoja,  string ciCliente, string repuesto)
{
    try
    {
        Console.WriteLine("---------------------FACTURA--------------------");
        var sqlCommand = new SqlCommand($"execute RegistroFactura @p_CodFactura ='{factura}', @p_CodHoja = '{codHoja}', @p_CI_Clientes = '{ciCliente}',@p_codRepuesto = '{repuesto}' ", sqlConnection);
        
        data = sqlCommand.ExecuteReader();
        data.Close();
        Console.ReadKey();
        Console.WriteLine("Factura generada correctamente");
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
    


    Console.ReadKey();

}
static void AvailableMechanic(SqlConnection sqlConnection, ref SqlDataReader data, string codMec)
{ 
    var sqlCommand = new SqlCommand($"Update Mecanicos set Disponibilidad='Libre' where CodMecanico = '{codMec}' ", sqlConnection);
    data = sqlCommand.ExecuteReader();
    data.Close();
}
static string RegisterDataSheets(SqlConnection sqlConnection, ref SqlDataReader data, String codmecanico, String matricula)
{
    Console.Write("\n");
    Console.WriteLine("---------------------Registro de Hoja de Parte--------------------");
    Console.Write("Codigo de hoja de parte: ");
    string codhoja = Console.ReadLine();
    
    Console.Write("Fecha de ingreso(AAAAMMDD): ");
    string fechaingreso = Console.ReadLine();

    Console.Write("Hora de ingreso(HH:MM): ");
    string horaingreso = Console.ReadLine();

    Console.Write("Precio mano de obra: ");
    string manodeobra = Console.ReadLine();
    
  
        var sqlCommand = new SqlCommand($"exec RegistroHojaDeParte @p_CodHoja='{codhoja}', " +
                                        $"@p_Matricula='{matricula}'," +
                                        $"@p_CodMecanico='{codmecanico}'," +
                                        $"@p_FechaIngreso='{fechaingreso}'," +
                                        $"@p_HoraIngreso='{horaingreso}'," +
                                        $"@p_Manodeobra='{manodeobra}';",sqlConnection);
        
        data = sqlCommand.ExecuteReader();
        data.Close();
   
    
  


    Console.ReadKey();
    return codhoja;
}
static void RegisterDetails(SqlConnection sqlConnection, ref SqlDataReader data, string codigoDetalle, string codigoHoja, string codigoRepuesto, string cantidad)
{
    var sqlCommand = new SqlCommand($"exec RegistroDetalle @p_CodDetalle ='{codigoDetalle}'," +
                                    $"@p_CodHoja='{codigoHoja}'," +
                                    $"@p_CodRep_Ins_Mat='{codigoRepuesto}'," +
                                    $"@p_Cantidad='{cantidad}';", sqlConnection);
    data = sqlCommand.ExecuteReader();
    data.Close();


    Console.ReadKey();

}
static string RegisterCars(SqlConnection sqlConnection, ref SqlDataReader data)
{
    Console.WriteLine("---------------------Registro de Automoviles--------------------");
    Console.Write("Numero de chasis: ");
    string nrochasis = Console.ReadLine();

    Console.Write("NIT concesionaria: ");
    string nitcon = Console.ReadLine();

    Console.Write("Codigo del modelo: ");
    string codmodelo = Console.ReadLine();

    Console.Write("Fecha de fabricacion(AAAAMMDD): ");
    string fechafabri = Console.ReadLine();

    Console.Write("Color: ");
    string color = Console.ReadLine();


    var sqlCommand = new SqlCommand($"insert into Automoviles values ('{nrochasis}'," +
                                    $"'{nitcon}'," +
                                    $"'{codmodelo}'," +
                                    $"'{fechafabri}'," +
                                    $"'{color}'," +
                                    $"'En Stock');", sqlConnection);
    data = sqlCommand.ExecuteReader();
    data.Close();


    Console.ReadKey();
    return nrochasis;

}
static string RegisterSoldCars(SqlConnection sqlConnection, ref SqlDataReader data)
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

    return matricula;

}
static void RegisterModels(SqlConnection sqlConnection, ref SqlDataReader data)
{
    try
    {
        Console.WriteLine("---------------------Registro de Modelos--------------------");
        Console.Write("Codigo del modelo: ");
        string codmodelo = Console.ReadLine();

        Console.Write("Marca: ");
        string marca = Console.ReadLine();

        Console.Write("Modelo: ");
        string modelo = Console.ReadLine();

        Console.Write("Numero de puertas: ");
        string nropuertas = Console.ReadLine();

        Console.Write("Cilindradas: ");
        string cilindradas = Console.ReadLine();

        Console.Write("Numero de ruedas: ");
        string nroruedas = Console.ReadLine();

        Console.Write("Procedencia: ");
        string procedencia = Console.ReadLine();

        Console.Write("Cantidad de pasajeros: ");
        string cantpasajeros = Console.ReadLine();
        
        
        
        var sqlCommand = new SqlCommand($"insert into Modelos values " +
                                        $"('{codmodelo}','{marca}','{modelo}','{nropuertas}'," +
                                        $"'{cilindradas}','{nroruedas}','{procedencia}', '{cantpasajeros}');", sqlConnection);
        
        data = sqlCommand.ExecuteReader();
        data.Close();
        Console.ReadKey();
        Console.WriteLine("Registrado correctamente.");

    }
    catch (Exception e)
    {
        Console.WriteLine("No se pudo registrar.");
        Console.ReadKey();
        Console.WriteLine(e);
        throw;
    }
    

    Console.ReadKey();

}
static void RegisterOficialServices(SqlConnection sqlConnection, ref SqlDataReader data)
{
    try
    {
        Console.WriteLine("---------------------Registro de Servicio Oficial--------------------");
        Console.Write("NIT Servicio Oficial: ");
        string nitso = Console.ReadLine();

        Console.Write("NIT Concesionaria: ");
        string nitcon = Console.ReadLine();

        Console.Write("Nombre Servicio Oficial: ");
        string nombreso = Console.ReadLine();

        Console.Write("Direccion Servicio Oficial: ");
        string direccionso = Console.ReadLine();

        Console.Write("Telefono: ");
        string telefonoso = Console.ReadLine();

        Console.Write("Email: ");
        string emailso = Console.ReadLine();

        Console.Write("Talleres: ");
        string tallerso = Console.ReadLine();
        
       // insert into ServicioOficial values ('22223','45457','Ricardo Service','Av. Banzer 4to Anillo', '75003138','servicio@oficialRicardo.com','Taller Ricardo');


        var sqlCommand = new SqlCommand($"insert into ServicioOficial values ('{nitso}','{nitcon}','{nombreso}','{direccionso}','{telefonoso}','{emailso}','{tallerso}');" ,sqlConnection);
        data = sqlCommand.ExecuteReader();
        data.Close();

        Console.ReadKey();
        Console.WriteLine("Registrado correctamente.");

    }
    catch (Exception e)
    {
        Console.WriteLine("No se pudo registrar.");
        Console.ReadKey();
        Console.WriteLine(e);
        throw;
    }
    

    Console.ReadKey();

}
static string RegisterMechanics(SqlConnection sqlConnection, ref SqlDataReader data)
{
    Console.WriteLine("---------------------Registro de Mecanicos--------------------");
    Console.Write("Codigo del mecanico: ");
    string codigoMec = Console.ReadLine();

    Console.Write("Servicio oficial: ");
    string so = Console.ReadLine();
    
    Console.Write("Solo el Nombre: ");
    string nombre = Console.ReadLine();

    Console.Write("Apellido: ");
    string apellido = Console.ReadLine();
    
    Console.Write("Celular: ");
    string celular = Console.ReadLine();

    Console.Write("Direccion: ");
    string Direccion = Console.ReadLine();
    
    Console.Write("Especialidad: ");
    string especialidad = Console.ReadLine();



    var sqlCommand = new SqlCommand($"insert into Mecanicos values " +
                                    $"('{codigoMec}'," +
                                    $"'{so}'," +
                                    $"'{nombre}', " +
                                    $"'{apellido}'," +
                                    $"'{celular}'," +
                                    $"'{Direccion}'," +
                                    $"'{especialidad}','Libre');", sqlConnection);
    data = sqlCommand.ExecuteReader();
    data.Close();


    Console.ReadKey();

    return codigoMec;

}

static string RegisterSpareParts(SqlConnection sqlConnection, ref SqlDataReader data, string HojaDeParte)
{
    try
    {
    Console.WriteLine("---------------------Registre el Repuestos/Insumos/Materiales--------------------");
    Console.Write("Codigo: ");
    string codRIM = Console.ReadLine();

    Console.Write("Repuesto/Insumo: ");
    string nombre = Console.ReadLine();

    Console.Write("Precio: ");
    string precio = Console.ReadLine();

    var sqlCommand = new SqlCommand($"insert into RepuestosInsumosMateriales values ('{codRIM}'," +
                                    $"'{nombre}'," +
                                    $"'{precio}','Disponible','{HojaDeParte}');", sqlConnection);
    data = sqlCommand.ExecuteReader();
    data.Close();
    Console.ReadKey();
    Console.WriteLine("Registrado correctamente.");
    return codRIM;
    }
    catch (Exception e)
    {
        Console.WriteLine("No se pudo registrar.");
        Console.ReadKey();
        Console.WriteLine(e);
        throw;
    }
    

    Console.ReadKey();
    
   

}