using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Data;
using System.Text;
using System.Transactions;

namespace ToDoListLogic.DAL
{
    public class ApplicationContext : DbContext
    {

        private NpgsqlConnection _connection;
        private NpgsqlCommand _command;
        private DataTable _dataTable;

        private int maxId = 0;

        public readonly string connectionString =
            "Host=localhost;" +
            "Username=postgres;" +
            "Password=123;" +
            "Database=ToDoList";

        public ApplicationContext()
        {
            Connect();
        }

        public async void Connect()
        {
            _connection = new NpgsqlConnection(connectionString);
        }

        public async void Insert(string execution)
        {
           
        }

        public void Select()
        {
            try
            {
                _connection.Open();
                string sql = @$"select id, todo_name, priority_name as priority, text from todo as t "+
                                "join priorities as p "+
                                "on t.priority = p.priority_id";
                _command = new NpgsqlCommand(sql, _connection);
                _dataTable = new DataTable();
                _dataTable.Load(_command.ExecuteReader());
                _connection.Close();
                DataToConsole();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void DataToConsole()
        {
            foreach (DataRow row in _dataTable.Rows)
            {
                if ((row.ItemArray[0] is int id) && id > maxId)
                {
                    maxId = id;
                }
                foreach (object? o in row.ItemArray)
                {
                    Console.Write("| " + o?.ToString() + " |");
                }
                Console.WriteLine("\n================================");
            }
        }

        public void AddElement(todo data)
        {
            _connection.Open();
            string sql = @$"insert into todo values ({data.ID}, '{data.Name}',{data.Priority},'{data.Text}')";
            _command = new NpgsqlCommand(sql, _connection);
            _dataTable = new DataTable();
            _dataTable.Load(_command.ExecuteReader());
            _connection.Close();
        }

        public void DeleteData(int id)
        {
            try
            {
                _connection.Open();
                string sql = @$"delete from todo where id = {id}";
                _command = new NpgsqlCommand(sql, _connection);
                _dataTable = new DataTable();
                _dataTable.Load(_command.ExecuteReader());
                _connection.Close();
                DataToConsole();
            }   
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
