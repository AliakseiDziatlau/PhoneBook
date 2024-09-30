using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using WebApplicationPhoneBook.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplicationPhoneBook.services
{
    public class ApplicationContext : DbContext
    {
        public DbSet<PersonItem> Person { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }

    //public interface ICtrlDataBase
    //{
    //    string GetErrorText();
    //    List<Person> GetList();
    //    List<Person> GetList(PhoneItemFilter phoneItemFilter);

    //    List<Person> GetFullList(string name, string phone, string city, string country, string street, string house_number);
    //    bool Add(Person recordItem);
    //    bool Edit(int i, Person recordItem); 
    //    bool Delete(int i);

    //}

    //public class CtrlDataBase : ICtrlDataBase
    //{
    //    private string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=PhoneBook;Integrated Security=SSPI;Encrypt=False;User ID=user1;Password=1;";

    //    private List<Person> listRecord = new List<Person>();

    //    private string textErr = "";
    //    public bool Add(Person recordItem)
    //    {
    //        string sqlExpression = $"INSERT INTO Person (Name, Phone_number, Country, City, Street, House_number, Email) VALUES (\'{recordItem.Name}', \'{recordItem.Phone_number}\', \'{recordItem.Country}\', \'{recordItem.City}\', \'{recordItem.Street}\', \'{recordItem.House_number}\', \'{recordItem.Email}\')";

    //        using (SqlConnection connection = new SqlConnection(connectionString))
    //        {
    //            connection.Open();
    //            SqlCommand command = new SqlCommand(sqlExpression, connection);
    //            command.ExecuteNonQuery();
    //        }
    //        return true;
    //    }

    //    public bool Delete(int id)
    //    {

    //        string sqlExpression = $"DELETE FROM Person where Id = {id}";

    //        using (SqlConnection connection = new SqlConnection(connectionString))
    //        {
    //            connection.Open();
    //            SqlCommand command = new SqlCommand(sqlExpression, connection);
    //            command.ExecuteNonQuery();
    //        }
    //        return true;
    //    }

    //    public bool Edit(int i, Person recordItem)
    //    {
    //        string sqlExpression = $"UPDATE Person SET Name = \'{recordItem.Name}\', Phone_number = \'{recordItem.Phone_number}\', Country = \'%{recordItem.Country}%\', City = \'{recordItem.City}\', Street =\'{recordItem.Street}\', \'{recordItem.House_number}\', Email=\'{recordItem.Email}\'  where Id = {recordItem.Id}";

    //        using (SqlConnection connection = new SqlConnection(connectionString))
    //        {
    //            connection.Open();
    //            SqlCommand command = new SqlCommand(sqlExpression, connection);
    //            command.ExecuteNonQuery();
    //        }
    //        return true;
    //    }

    //    public string GetErrorText()
    //    {
    //        return textErr;
    //    }

    //    public List<Person> GetList()
    //    {
    //        GetAllRecord();
    //        return listRecord;
    //    }

    //    public List<Person> GetList(PhoneItemFilter PhoneItemFilter)
    //    {
    //        // Инициализация запроса и списка
    //        string strRequest = "SELECT p.Id, p.Name, p.Phone_number, p.City, p.Email, p.Country, p.Street, p.House_number FROM Person p";
    //        List<Person> listRecord = new List<Person>();

    //        using (SqlConnection connection = new SqlConnection(connectionString))
    //        {
    //            connection.Open();
    //            SqlCommand command = new SqlCommand();
    //            command.Connection = connection;

    //            List<string> conditions = new List<string>();

    //            if (!string.IsNullOrEmpty(PhoneItemFilter.Name))
    //            {
    //                conditions.Add("p.Name LIKE @Name");
    //                command.Parameters.AddWithValue("@Name", $"%{PhoneItemFilter.Name}%");
    //            }
    //            if (!string.IsNullOrEmpty(PhoneItemFilter.Phone))
    //            {
    //                conditions.Add("p.Phone_number LIKE @Phone_number");
    //                command.Parameters.AddWithValue("@Phone_number", $"%{PhoneItemFilter.Phone}%");
    //            }
    //            if (!string.IsNullOrEmpty(PhoneItemFilter.City))
    //            {
    //                conditions.Add("p.City LIKE @Adress");
    //                command.Parameters.AddWithValue("@City", $"%{PhoneItemFilter.City}%");
    //            }
    //            if (!string.IsNullOrEmpty(PhoneItemFilter.Country))
    //            {
    //                conditions.Add("p.Country LIKE @Country");
    //                command.Parameters.AddWithValue("@Country", $"%{PhoneItemFilter.Country}%");
    //            }
    //            if (!string.IsNullOrEmpty(PhoneItemFilter.Street))
    //            {
    //                conditions.Add("p.Street LIKE @Street");
    //                command.Parameters.AddWithValue("@Street", $"%{PhoneItemFilter.Street}%");
    //            }

    //            if (conditions.Count > 0)
    //            {
    //                strRequest += " WHERE " + string.Join(" AND ", conditions);
    //            }

    //            command.CommandText = strRequest;

    //            using (var reader = command.ExecuteReader())
    //            {
    //                while (reader.Read())
    //                {
    //                    Person item = new Person
    //                    {
    //                        Id = reader.GetInt32(0),
    //                        Name = reader.GetString(1),
    //                        Phone_number = reader.GetString(2),
    //                        City = reader.GetString(3),
    //                        Email = reader.GetString(4),
    //                        Country = reader.GetString(5),
    //                        Street = reader.GetString(6),
    //                        House_number = reader.GetString(7),
    //                    };
    //                    listRecord.Add(item);
    //                }
    //            }
    //        }

    //        return listRecord;
    //    }

    //    //public List<PhoneItem> GetList(PhoneItemFilter PhoneItemFilter)
    //    //{
    //    //    string strRequest = "SELECT p.Id, p.Name, p.Phone_number, p.Adress,  p.Email  FROM Person p ";
    //    //    bool isWhere = false;
    //    //    if (PhoneItemFilter.Name != null)
    //    //    {
    //    //        isWhere = true;
    //    //        strRequest += $" WHERE p.Name like \'%{PhoneItemFilter.Name}%\'";
    //    //    }
    //    //    if (PhoneItemFilter.Phone != null)
    //    //    {
    //    //        if (isWhere == false)
    //    //        {
    //    //            isWhere = true;
    //    //            strRequest += $" WHERE ";
    //    //        }
    //    //        else strRequest += $" AND ";
    //    //        strRequest += $"p.Phone_number like \'%{PhoneItemFilter.Phone}%\'";
    //    //    }
    //    //    if (PhoneItemFilter.Address != null)
    //    //    {
    //    //        if (isWhere == false)
    //    //        {
    //    //            isWhere = true;
    //    //            strRequest += $" WHERE ";
    //    //        }
    //    //        else strRequest += $" AND ";
    //    //        strRequest += $"p.Adress like \'%{PhoneItemFilter.Address}%\'";
    //    //    }
    //    //    _GetRecord(strRequest);
    //    //    return listRecord;
    //    //}

    //    //public List<PhoneItem> GetList(PhoneItemFilter PhoneItemFilter)
    //    //{
    //    //    listRecord.Clear();
    //    //    string sqlExpression = "GetPersonFilter ";

    //    //    using (SqlConnection connection = new SqlConnection(connectionString))
    //    //    {
    //    //        connection.Open();
    //    //        SqlCommand command = new SqlCommand(sqlExpression, connection);
    //    //        command.CommandType = System.Data.CommandType.StoredProcedure;
    //    //        if (PhoneItemFilter.Name != null)
    //    //        {
    //    //            SqlParameter nameParam = new SqlParameter
    //    //            {
    //    //                ParameterName = "@Name",
    //    //                Value = PhoneItemFilter.Name
    //    //            };
    //    //            command.Parameters.Add(nameParam);
    //    //        }
    //    //        if (PhoneItemFilter.Phone != null)
    //    //        {
    //    //            SqlParameter nameParam = new SqlParameter
    //    //            {
    //    //                ParameterName = "@Phone_number",
    //    //                Value = PhoneItemFilter.Phone
    //    //            };
    //    //            command.Parameters.Add(nameParam);
    //    //        }

    //    //        var reader = command.ExecuteReader();

    //    //        if (reader.HasRows)
    //    //        {

    //    //            while (reader.Read())
    //    //            {
    //    //                PhoneItem item = new PhoneItem();
    //    //                item.ID = reader.GetInt32(0);
    //    //                item.Name = reader.GetString(1);
    //    //                item.Phone = reader.GetString(2);
    //    //                item.Country= reader.GetString(3);
    //    //                item.Address = reader.GetString(4);
    //    //                item.Email = reader.GetString(5);

    //    //                listRecord.Add(item);
    //    //            }
    //    //        }
    //    //        reader.Close();
    //    //    }
    //    //    return listRecord;
    //    //}

    //    public List<Person> GetFullList(string name, string phone, string city, string country, string street, string house_number) {
    //        _GetRecord($"SELECT p.Id, p.Name, p.Phone_number, p.Country, p.City, p.Street, p.House_number, p.Email FROM Person p Where p.City like \'%{city}%\' And p.Phone_number like \'%{phone}%\' And p.Name like \'%{name}%\' And p.Country like \'%{country}%\' AND p.Street like \'{street}\' AND p.House_number like \'{house_number}\'");
    //        return listRecord;
    //    }

    //    public bool GetAllRecord()
    //    {
    //        return _GetRecord("SELECT p.Id, p.Name, p.Phone_number, p.Country, p.City, p.Email, p.Street, p.House_number FROM Person p");
    //    }

    //    public bool _GetRecord(string sqlExpression)
    //    {

    //        try
    //        {
    //            listRecord.Clear();
    //            using (SqlConnection connection = new SqlConnection(connectionString))
    //            {
    //                connection.Open();
    //                SqlCommand command = new SqlCommand(sqlExpression, connection);
    //                SqlDataReader reader = command.ExecuteReader();
    //                while (reader.Read())
    //                {
    //                    Person item = new Person();
    //                    item.Id = reader.GetInt32(0);
    //                    item.Name = reader.GetString(1);
    //                    item.Phone_number = reader.GetString(2);
    //                    item.Country = reader.GetString(3);
    //                    item.City = reader.GetString(4);// + " " + reader.GetString(3);
    //                    if (!reader.IsDBNull(5))
    //                        item.Email = reader.GetString(5);
    //                    item.Street = reader.GetString(6);
    //                    item.House_number = reader.GetString(7);

    //                    listRecord.Add(item);
    //                }

    //            }

    //        }
    //        catch (InvalidOperationException ex)
    //        {
    //            textErr += ex.Message;
    //            return false;
    //        }
    //        catch (SqlException ex)
    //        {
    //            textErr += ex.Message;
    //            return false;
    //        }
    //        return true;
    //    }
    //}
}
