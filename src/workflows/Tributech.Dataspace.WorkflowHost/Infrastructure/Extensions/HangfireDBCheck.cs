using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Tributech.Dataspace.WorkflowHost.Infrastructure.Extensions
{
    public static class HangfireDBCheckExtensions {

        public static IServiceCollection EnsureHangfireDB(this IServiceCollection services, string masterConnectionString, string jobsConnectionString) {

            using (var jobsConnection = new NpgsqlConnection(jobsConnectionString)) {
                using (var con = new NpgsqlConnection(masterConnectionString)) {
                    con.Open();

                    object dbExists = null;

                    var sql = $"SELECT * FROM pg_database WHERE datname = '{jobsConnection.Database}'";
                    using (var cmd = new NpgsqlCommand(sql, con)) {
                        dbExists = cmd.ExecuteScalar();
                    }

                    if (dbExists == null) {
                        using (var cmd = new NpgsqlCommand($"CREATE DATABASE \"{jobsConnection.Database}\"", con)) {
                            cmd.ExecuteNonQuery();
                        }
                    }

                }
            }

            return services;

        }

    }
}
