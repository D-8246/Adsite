using Microsoft.Data.SqlClient;

namespace Adsite.Data
{
    public class Ad
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }

    public class AdManager
    {
        private string _connectionString { get; set; }
        public AdManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void NewAd(Ad ad)
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Ads VALUES " +
                "(@Name, @Number, @Date, @Description) " +
                "SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@Name", ad.Name);
            cmd.Parameters.AddWithValue("@Number", ad.Number);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.Parameters.AddWithValue("@Description", ad.Description);
            connection.Open();
            ad.Id = (int)(decimal)cmd.ExecuteScalar();
        }

        public List<Ad> GetAds()
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Ads ORDER BY DATE DESC";
            connection.Open();
            var reader = cmd.ExecuteReader();
            List<Ad> ads = new();
            while (reader.Read())
            {
                ads.Add(new Ad {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    Number = (int)reader["Number"],
                    Date = (DateTime)reader["Date"],
                    Description = (string)reader["Description"],
                });
            }
            return ads;
        }

        public void DeleteAd(int id)
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM Ads WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
