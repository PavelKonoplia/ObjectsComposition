using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
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
            string condition = null;
            PropertyInfo[] propsInfo = typeT.GetProperties();

            for (int i = 0; i < propsInfo.Length; i++)
            {
                if (propsInfo[i].Name.ToString().ToLower().Contains("id"))
                {
                    condition += propsInfo[i].Name + "=" + id;
                    break;
                }
            }

            string sqlExpression = $"SELECT * FROM {tableName} WHERE {condition}";
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

                    foreach (PropertyInfo prop in propsInfo)
                    {
                        prop.SetValue(instance, ds.Tables[0].Rows[0][prop.Name], null);
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
            string columns = null, props = null, tempProp;
            PropertyInfo[] propsInfo = typeT.GetProperties();

            for (int i = 0; i < propsInfo.Length; i++)
            {
                if (propsInfo[i].Name == "Id")
                {
                    continue;
                }

                if (i != 0 && i != propsInfo.Length - 1)
                {
                    props += ",";
                    columns += ",";
                }

                columns += propsInfo[i].Name;
                tempProp = propsInfo[i].PropertyType.ToString().Contains("String")
                    ? "\'" + GetPropValue(item, propsInfo[i].Name) + "\'"
                    : GetPropValue(item, propsInfo[i].Name).ToString();
                props += tempProp;
            }

            string sqlExpression = $"INSERT INTO {tableName} ({columns}) VALUES ({props})";
            string sqlExpressionGetId = $"SELECT MAX(ID) FROM {tableName}";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlCommand resultId = new SqlCommand(sqlExpressionGetId, connection);
                try
                {
                    command.ExecuteNonQuery();
                    return (int)resultId.ExecuteScalar();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void Update(T item)
        {
            string props = null, condition = null, tempProp;
            PropertyInfo[] propsInfo = typeT.GetProperties();

            for (int i = 0; i < propsInfo.Length; i++)
            {
                if (propsInfo[i].Name.ToString().ToLower().Contains("id"))
                {
                    condition = propsInfo[i].Name + "=" + GetPropValue(item, propsInfo[i].Name).ToString();
                    continue;
                }

                props += (i != 0 && i != propsInfo.Length - 1) ? "," : null;
                tempProp = propsInfo[i].PropertyType.ToString().Contains("String")
                     ? "\'" + GetPropValue(item, propsInfo[i].Name) + "\'"
                     : GetPropValue(item, propsInfo[i].Name).ToString();
                props += $"{propsInfo[i].Name}={tempProp}";
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
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void Delete(int id)
        {
            string condition = null;
            PropertyInfo[] propsInfo = typeT.GetProperties();

            for (int i = 0; i < propsInfo.Length; i++)
            {
                if (propsInfo[i].Name.ToString().ToLower().Contains("id"))
                {
                    condition = $"{propsInfo[i].Name}={id}";
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