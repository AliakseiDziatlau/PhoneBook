using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using WebApplicationPhoneBook.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplicationPhoneBook.services
{
    public interface ICtrlDataBase
    {
        string GetErrorText();
        List<PhoneItem> GetList();
        List<PhoneItem> GetList(PhoneItemFilter phoneItemFilter);

        List<PhoneItem> GetFullList(string name, string phone, string adress);
        bool Add(PhoneItem recordItem);
        bool Edit(int i, PhoneItem recordItem);
        bool Delete(int i);

    }

    public class CtrlDataBase : ICtrlDataBase
    {
        private string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=PhoneBook;Integrated Security=SSPI;Encrypt=False;User ID=user1;Password=1;";

        private List<PhoneItem> listRecord = new List<PhoneItem>();

        private string textErr = "";
        public bool Add(PhoneItem recordItem)
        {
            string sqlExpression = $"INSERT INTO Person (Name, Phone_number, Adress) VALUES (\'{recordItem.Name}', \'{recordItem.Phone}\', \'{recordItem.Address}\')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
            return true;
        }

        public bool Delete(int id)
        {

            string sqlExpression = $"DELETE FROM Person where Id = {id}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
            return true;
        }

        public bool Edit(int i, PhoneItem recordItem)
        {
            string sqlExpression = $"UPDATE Person SET Name = \'{recordItem.Name}\', Phone_number = \'{recordItem.Phone}\', Adress = \'{recordItem.Address}\'  where Id = {recordItem.ID}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
            return true;
        }

        public string GetErrorText()
        {
            return textErr;
        }

        public List<PhoneItem> GetList()
        {
            GetAllRecord();
            return listRecord;
        }

        //public List<PhoneItem> GetList(PhoneItemFilter PhoneItemFilter)
        //{
        //    string strRequest = "SELECT p.Id, p.Name, p.Phone_number, p.Adress,  p.Email  FROM Person p ";
        //    bool isWhere = false;
        //    if (PhoneItemFilter.Name != null)
        //    {
        //        isWhere = true;
        //        strRequest += $" WHERE p.Name like \'%{PhoneItemFilter.Name}%\'";
        //    }
        //    if (PhoneItemFilter.Phone != null)
        //    {
        //        if (isWhere == false)
        //        {
        //            isWhere = true;
        //            strRequest += $" WHERE ";
        //        }
        //        else strRequest += $" AND ";
        //        strRequest += $"p.Phone_number like \'%{PhoneItemFilter.Phone}%\'";
        //    }
        //    if (PhoneItemFilter.Address != null)
        //    {
        //        if (isWhere == false)
        //        {
        //            isWhere = true;
        //            strRequest += $" WHERE ";
        //        }
        //        else strRequest += $" AND ";
        //        strRequest += $"p.Adress like \'%{PhoneItemFilter.Address}%\'";
        //    }
        //    _GetRecord(strRequest);
        //    return listRecord;
        //}

        public List<PhoneItem> GetList(PhoneItemFilter PhoneItemFilter)
        {
            listRecord.Clear();
            string sqlExpression = "GetPersonFilter ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (PhoneItemFilter.Name != null)
                {
                    SqlParameter nameParam = new SqlParameter
                    {
                        ParameterName = "@Name",
                        Value = PhoneItemFilter.Name
                    };
                    // добавляем параметр
                    command.Parameters.Add(nameParam);
                }
                // параметр для ввода возраста
                if (PhoneItemFilter.Phone != null)
                {
                    SqlParameter nameParam = new SqlParameter
                    {
                        ParameterName = "@Phone_number",
                        Value = PhoneItemFilter.Phone
                    };
                    // добавляем параметр
                    command.Parameters.Add(nameParam);
                }

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        PhoneItem item = new PhoneItem();
                        item.ID = reader.GetInt32(0);
                        item.Name = reader.GetString(1);
                        item.Phone = reader.GetString(2);
                        item.Address = reader.GetString(3);
                        item.Email = reader.GetString(4);

                        listRecord.Add(item);
                    }
                }
                reader.Close();
            }
            return listRecord;
        }

        public List<PhoneItem> GetFullList(string name, string phone, string adress) {
            _GetRecord($"SELECT p.Id, p.Name, p.Phone_number, p.Adress, p.Email FROM Person p Where p.Adress like \'%{adress}%\' And p.Phone_number like \'%{phone}%\' And p.Name like \'%{name}%\'");
            return listRecord;
        }

        public bool GetAllRecord()
        {
            return _GetRecord("SELECT p.Id, p.Name, p.Phone_number, p.Adress, p.Email FROM Person p");
        }

        public bool _GetRecord(string sqlExpression)
        {

            try
            {
                listRecord.Clear();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        PhoneItem item = new PhoneItem();
                        item.ID = reader.GetInt32(0);
                        item.Name = reader.GetString(1);
                        item.Phone = reader.GetString(2);
                        item.Address = reader.GetString(3);// + " " + reader.GetString(3);
                        item.Email= reader.GetString(4);
                        listRecord.Add(item);
                    }

                }

            }
            catch (InvalidOperationException ex)
            {
                textErr += ex.Message;
                return false;
            }
            catch (SqlException ex)
            {
                textErr += ex.Message;
                return false;
            }
            return true;
        }
    }
}
