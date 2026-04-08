using Microsoft.Data.SqlClient;
using MyFamilyRoot.Models;
using System.Data;




namespace MyFamilyRoot.Services
{
    public class Family : IFamily
    {

        public readonly IConfiguration _Configuration;
        public Family(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        public string Constring()
        {
            var constrr = _Configuration.GetConnectionString("dbcs");
            return constrr;
        }

        public List<Familymodel> Getalldata()
        {
            List<Familymodel> fetcheddata = new List<Familymodel>();

            using (SqlConnection conn = new SqlConnection(Constring()))
            {
                SqlCommand cmd = new SqlCommand("SPFamily", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "fetcheddata");

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    Familymodel familymodel = new Familymodel
                    {
                        Id  = Convert.ToInt32(row["Id"]),
                        FullName = row["fullname"].ToString(),
                        Age = Convert.ToInt32(row["age"]),
                        Gender = row["gender"].ToString(),
                        Description = row["description"].ToString()
                    };

                    fetcheddata.Add(familymodel);
                }
            }

            return fetcheddata;
        }

        public bool   InsertData(Familymodel model)
        {
            using (SqlConnection conn = new SqlConnection(Constring()))
            {
                SqlCommand cmd = new SqlCommand("SPFamily", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@flag", "insert"); 
                cmd.Parameters.AddWithValue("@FullName", model.FullName);
                cmd.Parameters.AddWithValue("@Age", model.Age);
                cmd.Parameters.AddWithValue("@Gender", model.Gender);
                cmd.Parameters.AddWithValue("@Description", model.Description);

                conn.Open();
                int id = cmd.ExecuteNonQuery();
                return id > 0;
            }
        }


        public bool DeleteData(Familymodel model) {
        
        using (SqlConnection conn = new SqlConnection(Constring())) {

                SqlCommand cmd = new SqlCommand("SPFamily", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@flag", "delete");
                cmd.Parameters.AddWithValue("@Id", model.Id);

                conn.Open();
                int i = cmd.ExecuteNonQuery();
                return i > 0;

            }
        
        }

        public List<Familymodel> fetchingdatabyid(Familymodel model)
        {
            List<Familymodel> familydatabyid = new List<Familymodel>();
            using(SqlConnection conn = new SqlConnection(Constring()))
            {
                SqlCommand cmd = new SqlCommand("SPFamily", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@flag", "fetcheddatabyid");
                cmd.Parameters.AddWithValue("@Id", model.Id);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    Familymodel familymodel = new Familymodel
                    {
                        FullName = row["fullname"].ToString(),
                        Age = Convert.ToInt32(row["age"]),
                        Gender = row["gender"].ToString(),
                        Description = row["description"].ToString(),

                    };
                    familydatabyid.Add(familymodel);
                }
            }
            return familydatabyid;
        }

        public bool UpdateData(Familymodel model)
        {
            using (SqlConnection conn = new SqlConnection(Constring()))
            {
                SqlCommand cmd = new SqlCommand("SPFamily", conn);
                cmd.CommandType= CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@flag", "confirmupdate");
                cmd.Parameters.AddWithValue("@FullName", model.FullName);
                cmd.Parameters.AddWithValue("@Age", model.Age);
                cmd.Parameters.AddWithValue("@Gender", model.Gender);
                cmd.Parameters.AddWithValue("@Description", model.Description);
                cmd.Parameters.AddWithValue("@Id", model.Id);
                conn.Open();

                int id = cmd.ExecuteNonQuery();
                if (id > 0)
                {
                    return true;
                    
                }
                else
                {
                    return false;
                }


            }
        }
    }

}

