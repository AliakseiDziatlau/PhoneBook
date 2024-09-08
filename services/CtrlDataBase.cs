using System.Data.SqlClient;
using WebApplicationPhoneBook.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplicationPhoneBook.services
{
    public interface ICtrlDataBase
    {
        string GetErrorText();
        List<PhoneItem> GetList();
        List<PhoneItem> GetListFio(string text);
        List<PhoneItem> GetListPhone(string text);
        List<PhoneItem> GetListAdress(string text);

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

        public List<PhoneItem> GetListFio(string text)
        {
            _GetRecord($"SELECT p.Id, p.Name, p.Phone_number, p.Adress FROM Person p Where p.Name like \'%{text}%\'");
            return listRecord;
        }

        public List<PhoneItem> GetListPhone(string text)
        {
            _GetRecord($"SELECT p.Id, p.Name, p.Phone_number, p.Adress FROM Person p Where p.Phone_number like \'%{text}%\'");
            return listRecord;
        }

        public List<PhoneItem> GetListAdress(string text) {
            _GetRecord($"SELECT p.Id, p.Name, p.Phone_number, p.Adress FROM Person p Where p.Adress like \'%{text}%\'");
            return listRecord;
        }

        public List<PhoneItem> GetFullList(string name, string phone, string adress) {
            _GetRecord($"SELECT p.Id, p.Name, p.Phone_number, p.Adress FROM Person p Where p.Adress like \'%{adress}%\' And p.Phone_number like \'%{phone}%\' And p.Name like \'%{name}%\'");
            return listRecord;
        }

        public bool GetAllRecord()
        {
            return _GetRecord("SELECT p.Id, p.Name, p.Phone_number, p.Adress FROM Person p");
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
