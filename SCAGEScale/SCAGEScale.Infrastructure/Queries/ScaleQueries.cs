using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using SCAGEScale.Application.AggregateRoot.ScaleSingleAggregate;
using SCAGEScale.Application.DTO;
using SCAGEScale.Application.Extensions;
using SCAGEScale.Application.QuerySide;
using SCAGEScale.Application.VO;

namespace SCAGEScale.Infrastructure.Queries
{
    public class ScaleQueries : IScaleQuery
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString { get { return _configuration.GetConnectionString("DefaultConnection"); } }

        public ScaleQueries(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<ScaleDto>?> GetAllSingleScales()
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    var response = await connection.QueryAsync<SingleScaleAggregate>(
                        "SELECT " +
                            "m.id as Id, " +
                            "m.name as Name, " +
                            "m.status as Status, " +
                            "m.transmissions as Transmissions, " +
                            "m.start as Start, " +
                            "m.end as End " +
                        "FROM month AS m " +
                        "WHERE m.isEnable = true; "
                    );

                    return response.Count() == 0 ? null : response.ToDTOList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<ScaleDto>> GetAllScales()
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    var response = await connection.QueryAsync<ReferencyUser>(
                        "SELECT " +
                            "u.id as Id, " +
                            "u.name as Name, " +
                            "u.email as Email, " +
                            "u.sex as Sex " +
                        "FROM users as u " +
                        "WHERE isEnable = 1 AND u.id = @id;");

                    if (response.Count() == 0) return null;

                    throw new NotImplementedException();
                    //return response.ToList();
                }
                catch (Exception ex)
                {

                }
            }
            throw new NotImplementedException();
        }
        public async Task<List<ScaleMonthDto>> ScaleMonthMakedList(List<ScaleDay> scaleDays)
        {
            var scaleMonthList = new List<ScaleMonthDto>();

            foreach (var scaleDay in scaleDays)
            {

                var userOne = await GetUserByReferency(scaleDay.CameraOne);
                var userTwo = await GetUserByReferency(scaleDay.CameraTwo);
                var userThree = await GetUserByReferency(scaleDay.CutDesk);

                scaleMonthList.Add(new ScaleMonthDto(userOne, userTwo, userThree));
            }
            return scaleMonthList;
        }

        public async Task<ReferencyUser> GetUserByReferency(Guid userId)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    var response = await connection.QueryAsync<ReferencyUser>(
                        "SELECT " +
                            "u.id as Id, " +
                            "u.name as Name, " +
                            "u.email as Email, " +
                            "u.sex as Sex " +
                        "FROM users as u " +
                        "WHERE isEnable = 1 AND u.id = @id;", new { id = userId });

                    if (response.Count() == 0) return null;

                    return response.ToList().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
