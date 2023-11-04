
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using SCAGEScale.Application.RepositorySide;
using SCAGEScale.Application.VO;

namespace SCAGEScale.Infrastructure.Repositories
{
    public class ScaleRepository : IScaleRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString { get { return _configuration.GetConnectionString("DefaultConnection"); } }

        public ScaleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }        

        public async Task<Guid> TransitionsScale(List<PropertiesManipulationMonth> scale, Guid monthId)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                int qntExecutionSuccess = 0;

                connection.Open();

                using (var transation = connection.BeginTransaction())
                {
                    foreach (var properties in scale)
                    {
                        try
                        {
                            var response = await connection.ExecuteAsync(properties.Sql, properties.Values, transaction: transation);
                            qntExecutionSuccess++;
                        }
                        catch (Exception ex)
                        {
                            qntExecutionSuccess--;
                        }
                    }
                    if (qntExecutionSuccess == scale.Count)
                    {
                        transation.Commit();
                    }
                    else
                    {
                        transation.Rollback();
                        throw new Exception("Não foi possível gerar ou atualizar a escala");
                    }
                }
                connection.Close();

                return monthId;
            }
        }
    }
}
