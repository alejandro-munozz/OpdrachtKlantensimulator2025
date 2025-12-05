using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Exceptions;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorBL.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using static KlantenSimulatorDL_SQL.KlantenSimulatorRepository_SQL;

namespace KlantenSimulatorDL_SQL
{
    public class KlantenSimulatorRepository_SQL : IKlantenSimulatorRepository_SQL
    {
        public KlantenSimulatorRepository_SQL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private string connectionString {  get; set; }

        public bool BestaatVersie(int landVersie)
        {
            string SQLBestaatLand = $"select count(*) from versie where versie = {landVersie}";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQLBestaatLand;
                int count = (int)cmd.ExecuteScalar();
                if (count != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public int UploadLandMetVersie(string landCode, int landVersie)
        {
            int land_versie_id = 0;
            string SQLland = $"insert into land (naam) output inserted.id values (@naam)";
            string SQLversie = $"insert into versie (versie) output inserted.id values (@versie)";
            string SQLland_versie = $"insert into land_versie (land, versie) output inserted.id values (@land, @versie)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmdLand = conn.CreateCommand())
            using (SqlCommand cmdVersie = conn.CreateCommand())
            using (SqlCommand cmdLand_versie = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                cmdLand.Transaction = tran;
                cmdVersie.Transaction = tran;
                cmdLand_versie.Transaction = tran;
                cmdLand.CommandText = SQLland;
                cmdVersie.CommandText = SQLversie;
                cmdLand_versie.CommandText = SQLland_versie;
                int land_id;
                int versie_id;
                try
                {
                    cmdLand.Parameters.Add("@naam", SqlDbType.NVarChar).Value = landCode;
                    land_id = (int)cmdLand.ExecuteScalar();
                    cmdVersie.Parameters.Add("@versie", SqlDbType.Int).Value = landVersie;
                    versie_id = (int)cmdVersie.ExecuteScalar();
                    cmdLand_versie.Parameters.Add("@land", SqlDbType.Int).Value = land_id;
                    cmdLand_versie.Parameters.Add("@versie", SqlDbType.Int).Value= versie_id;
                    land_versie_id = (int)cmdLand_versie.ExecuteScalar();
                    tran.Commit();
                } catch (Exception ex)
                {
                    tran.Rollback();
                }
                return land_versie_id;
            }
        }

        public void UploadNamen(Dictionary<string, int> namenAantal, Geslacht geslacht, int land_versieId, string naamType)
        {
            string SQLNaam = $"insert into {naamType} (voornaam, land_versie, aantal_voorkomen, geslacht) values (@voornaam, @land_versie, @aantal_voorkomen, @geslacht)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                cmd.CommandText = SQLNaam;
                try
                {
                    foreach (string naam in namenAantal.Keys)
                    {
                        cmd.Parameters.Add("@voornaam", SqlDbType.NVarChar).Value = naam;
                        cmd.Parameters.Add("@land_versie", SqlDbType.Int).Value = land_versieId;
                        cmd.Parameters.Add("@aantal_voorkomen", SqlDbType.Int).Value = namenAantal[naam];
                        cmd.Parameters.Add("@geslacht", SqlDbType.NVarChar).Value = geslacht;
                    }
                    cmd.ExecuteNonQuery();
                    tran.Commit();
                } catch (Exception ex)
                {
                    tran.Rollback();
                }
            }
        }
    }
}
