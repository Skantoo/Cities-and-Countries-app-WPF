using Gradovi.Models;
using Gradovi.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Gradovi.Dal
{
    class SqlRepository : IRepository
    {
        private const string ImeParam = "@name";
        private const string IdCountryParam = "@idCountry";
        private const string IdCityParam = "@idCity";
        private const string PictureParam = "@picture";
        //private const string CountryIdParam = "@countryId";


        private static readonly string cs = EncryptionUtils.Decrypt(ConfigurationManager.ConnectionStrings["cs"].ConnectionString, "Pa$$w0rd");


        public void AddCity(City city)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(ImeParam, city.ImeGrada);
                    cmd.Parameters.AddWithValue(IdCountryParam, city.CountryID);
                    cmd.Parameters.Add(new SqlParameter(PictureParam, SqlDbType.Binary, city.Picture.Length)
                    {
                        Value = city.Picture
                    });
                    SqlParameter idCity = new SqlParameter(IdCityParam, SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idCity);
                    cmd.ExecuteNonQuery();
                    city.IDCity = (int)idCity.Value;
                }
            }
        }

        public void AddCountry(Country country)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(ImeParam, country.ImeDrzave);
                    cmd.Parameters.Add(new SqlParameter(PictureParam, SqlDbType.Binary, country.Picture.Length)
                    {
                        Value = country.Picture
                    });
                    SqlParameter idCountry = new SqlParameter(IdCountryParam, SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idCountry);
                    cmd.ExecuteNonQuery();
                    country.IDCountry = (int)idCountry.Value;
                }
            }
        }

        public void DeleteCity(City city)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdCityParam, city.IDCity);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCountry(Country country)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdCountryParam, country.IDCountry);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<City> GetCities()
        {
            IList<City> cities = new List<City>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cities.Add(ReadCity(dr));
                        }
                    }
                }
            }
            return cities;
        }

        private City ReadCity(SqlDataReader dr) => new City
        {
            IDCity = (int)dr[nameof(City.IDCity)],
            ImeGrada = dr[nameof(City.ImeGrada)].ToString(),
            CountryID = (int)dr[nameof(City.CountryID)],
            Picture = ImageUtils.ByteArrayFromSqlDataReader(dr, 2)
        };

        public City GetCity(int IDCity)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdCityParam, IDCity);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return ReadCity(dr);
                        }
                    }
                }
            }
            throw new Exception("City does not exist");
        }

        public IList<Country> GetCountries()
        {
            IList<Country> countries = new List<Country>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            countries.Add(ReadCountry(dr));
                        }
                    }
                }
            }
            return countries;
        }

        private Country ReadCountry(SqlDataReader dr) => new Country
        {
            IDCountry = (int)dr[nameof(Country.IDCountry)],
            ImeDrzave = dr[nameof(Country.ImeDrzave)].ToString(),
            Picture = ImageUtils.ByteArrayFromSqlDataReader(dr, 2)
        };

        public Country GetCountry(int IDCountry)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdCountryParam, IDCountry);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return ReadCountry(dr);
                        }
                    }
                }
            }
            throw new Exception("Country does not exist");
        }

        public void UpdateCity(City city)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(ImeParam, city.ImeGrada);
                    cmd.Parameters.AddWithValue(IdCountryParam, city.CountryID);
                    cmd.Parameters.AddWithValue(IdCityParam, city.IDCity);
                    cmd.Parameters.Add(new SqlParameter(PictureParam, SqlDbType.Binary, city.Picture.Length)
                    {
                        Value = city.Picture
                    });

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCountry(Country country)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(ImeParam, country.ImeDrzave);
                    cmd.Parameters.AddWithValue(IdCountryParam, country.IDCountry);
                    cmd.Parameters.Add(new SqlParameter(PictureParam, SqlDbType.Binary, country.Picture.Length)
                    {
                        Value = country.Picture
                    });

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
