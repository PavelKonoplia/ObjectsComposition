using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using ObjectsComposition.Common;
using ObjectsComposition.Interfaces;

namespace ObjectsComposition.Logic.DbLogic
{
    public class CommandRunner<T> : IRepository<T> where T : class, new()
    {
        private static Type typeT = typeof(T);
        private string tableName = $"[dbo].[{typeT.Name}] ";
        private string _connectionString;

        public CommandRunner(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<T> GetItems()
        {
            string sqlExpression = $"SELECT * FROM {tableName}";
            Common.Collection<T> items = new Common.Collection<T>();
            PropertyInfo[] propsInfo = typeT.GetProperties();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    object instance = Activator.CreateInstance(typeT);
                    for (int j = 0; j < ds.Tables[0].Rows[i].ItemArray.Length; j++)
                    {
                        propsInfo[j].SetValue(instance, ds.Tables[0].Rows[i].ItemArray[j], null);
                    }

                    items.Add((T)instance);
                }

                adapter.Dispose();
                ds.Dispose();
            }

            return items;
        }

        public T GetItemById(int id)
        {
            string lowerTypeName = typeT.Name.ToLower(), condition = "";
            PropertyInfo[] propsInfo = typeT.GetProperties();

            for (int i = 0; i < propsInfo.Length; i++)
            {
                if (propsInfo[i].Name.ToString().ToLower().Contains(lowerTypeName + "id"))
                {
                    condition += propsInfo[i].Name + "=" + id;
                    break;
                }
            }

            string sqlExpression = $"SELECT * FROM { tableName } WHERE {condition}";
            T item = new T();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, connection);
                DataSet ds = new DataSet();
                try
                {
                    adapter.Fill(ds);
                    object instance = Activator.CreateInstance(typeT);
                    for (int j = 0; j < ds.Tables[0].Rows[0].ItemArray.Length; j++)
                    {
                        propsInfo[j].SetValue(instance, ds.Tables[0].Rows[0].ItemArray[j], null);
                    }

                    item = (T)instance;
                }
                catch (Exception)
                {
                    return null;
                }

                adapter.Dispose();
                ds.Dispose();
            }

            return item;
        }

        public int Create(T item)
        {
            string lowerTypeName = typeT.Name.ToLower();
            string columns = "", props = "", tempProp;
            PropertyInfo[] propsInfo = typeT.GetProperties();

            for (int i = 0; i < propsInfo.Length; i++)
            {
                if (propsInfo[i].Name == "Id")
                {
                    continue;
                }
                columns += i == propsInfo.Length - 1 ? propsInfo[i].Name : propsInfo[i].Name + ",";
                tempProp = propsInfo[i].PropertyType.ToString().Contains("String")
                    ? "\'" + GetPropValue(item, propsInfo[i].Name) + "\'"
                    : GetPropValue(item, propsInfo[i].Name).ToString();
                props += i == propsInfo.Length - 1 ? tempProp : tempProp + ",";
            }

            string sqlExpression = $"INSERT INTO {tableName} ({columns}) VALUES ({props})";
            string sqlExpressionGetId = $"SELECT LAST(Id) FROM {tableName}";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlCommand resultId = new SqlCommand(sqlExpressionGetId, connection);
                try
                {
                    command.ExecuteNonQuery();
                    SqlDataReader reader = resultId.ExecuteReader();
                    while (reader.Read()) // построчно считываем данные
                    {
                        return (int)reader.GetValue(0);
                    }

                    throw new IncorrectObjectIdException();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void Update(T item)
        {
            string lowerTypeName = typeT.Name.ToLower();
            string props = "", condition = "", tempProp;
            PropertyInfo[] propsInfo = typeT.GetProperties();

            for (int i = 0; i < propsInfo.Length; i++)
            {
                if (propsInfo[i].Name.ToString().ToLower().Contains(lowerTypeName + "id"))
                {
                    condition = propsInfo[i].Name + "=" + GetPropValue(item, propsInfo[i].Name).ToString();
                    continue;
                }

                tempProp = propsInfo[i].PropertyType.ToString().Contains("String")
                     ? "\'" + GetPropValue(item, propsInfo[i].Name) + "\'"
                     : GetPropValue(item, propsInfo[i].Name).ToString();
                props += i == propsInfo.Length - 1 ? propsInfo[i].Name + "=" + tempProp : propsInfo[i].Name + "=" + tempProp + ",";
            }

            string sqlExpression = $"UPDATE {tableName} SET {props} WHERE {condition}";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error on updating Item, check your model. \n");
                }
            }
        }

        public void Delete(int id)
        {
            string lowerTypeName = typeT.Name.ToLower();
            string condition = "";
            PropertyInfo[] propsInfo = typeT.GetProperties();

            for (int i = 0; i < propsInfo.Length; i++)
            {
                if (propsInfo[i].Name.ToString().ToLower().Contains(lowerTypeName + "id"))
                {
                    condition = propsInfo[i].Name + "=" + id;
                    break;
                }
            }

            string sqlExpression = $"DELETE FROM {tableName} WHERE {condition}";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
