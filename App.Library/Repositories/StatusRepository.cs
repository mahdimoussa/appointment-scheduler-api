﻿using App.Library.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace App.Library.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration Config;
        public StatusRepository(ApplicationDbContext dbContext, IConfiguration Config)
        {
            this.dbContext = dbContext;
            this.Config = Config;
        }
        public async Task<Tuple<bool, List<Lookup>>> GetAllStatuses()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Config.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    {
                        using (SqlCommand cmd = new SqlCommand("GetAllStatuses", con))
                        {
                            con.Open();
                            List<Lookup> StatusList = new List<Lookup>();
                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                Lookup lookup = new Lookup();
                                lookup.Code = reader.IsDBNull(0) ? "" : reader.GetString(0);
                                lookup.Id = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                                lookup.StatusEn = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                lookup.StatusAr = reader.IsDBNull(3) ? "" : reader.GetString(3);
                                lookup.ParentId = reader.IsDBNull(4) ? "" : reader.GetString(4);


                                StatusList.Add(lookup);
                            }
                            con.Close();
                            return new Tuple<bool, List<Lookup>>(true, StatusList);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return new Tuple<bool, List<Lookup>>(false, null);
            }
        }
       
        public async Task<Tuple<bool>> SaveStatus(Lookup lookup)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Config.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("SaveStatus", con))
                    {
                        con.Open();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = lookup.Id;
                        cmd.Parameters.Add("@Code", SqlDbType.VarChar).Value = lookup.Code;

                        cmd.ExecuteNonQuery();
                        con.Close();
                        return new Tuple<bool>(true);


                    }

                }
            }
            catch (Exception e)
            {
                return new Tuple<bool>(false);
            }
        }
       
        public async Task<Tuple<bool, bool>> DeleteStatus(int Id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Config.GetValue<string>("ConnectionStrings:DefaultConnection")))

                {
                    using (SqlCommand cmd = new SqlCommand("DeleteStatus", con))
                    {
                        con.Open();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return new Tuple<bool, bool>(true, true);
                    }
                }
            }
            catch (Exception e)
            {
                return new Tuple<bool, bool>(false, false);
            }
        }

    }
}
