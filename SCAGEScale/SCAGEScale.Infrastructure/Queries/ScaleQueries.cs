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

        public async Task<ScaleDto> GetScaleById(Guid id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    var response = await connection.QueryAsync<dynamic>(
                        "SELECT " +
                            "m.id as Id, " +
                            "m.name as Name, " +
                            "m.transmissions as Transmissions, " +
                            "m.start as Start, " +
                            "m.end as End, " +
                            "m.status as Status, " +
                            "m.isEnable AS IsEnable, " +
                            "d.id as IdDay, " +
                            "d.name as NameDay, " +
                            "d.date as DateDay, " +
                            "d.month as Month, " +
                            "d.isEnable AS IsEnableDay, " +
                            "userOne.id as IdUserOne, " +
                            "userOne.name as NameUserOne, " +
                            "userOne.email as EmailUserOne, " +
                            "userOne.sex as SexUserOne, " +
                            "userOne.isEnable AS IsEnableUserOne, " +
                            "userTwo.id as IdUserTwo, " +
                            "userTwo.name as NameUserTwo, " +
                            "userTwo.email as EmailUserTwo, " +
                            "userTwo.sex as SexUserTwo, " +
                            "userTwo.isEnable AS IsEnableUserTwo, " +
                            "userThree.id as IdUserThree, " +
                            "userThree.name as NameUserThree, " +
                            "userThree.email as EmailUserThree, " +
                            "userThree.sex as SexUserThree, " +
                            "userThree.isEnable AS IsEnableUserThree " +
                        "FROM month AS m " +
                        "INNER JOIN day AS d ON m.id = d.month AND d.isEnable = 1 " +
                        "INNER JOIN users AS userOne ON d.cameraOne = userOne.id AND userOne.isEnable = 1 " +
                        "INNER JOIN users AS userTwo ON d.cameraTwo = userTwo.id AND userTwo.isEnable = 1 " +
                        "INNER JOIN users AS userThree ON d.cutDesk = userThree.id AND userThree.isEnable = 1 " +
                        "WHERE m.id = @scaleId", new { scaleId = id }
                        );

                    return response.ToDtoListByDinamic();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
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
                            "m.end as End, " +
                            "m.isEnable AS IsEnable " +
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
                            "u.isEnable AS IsEnable " +
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
                            "u.sex as Sex, " +
                            "u.isEnable AS IsEnable " +
                        "FROM users as u " +
                        "WHERE isEnable = 1 AND u.id = @id;", new { id = userId });

                    return response.Count() != 0 ?
                        response.ToList().FirstOrDefault() :
                        null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<ScaleDto>?> GetAllSingleByFilterScales(string filter)
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
                            "m.end as End, " +
                            "m.isEnable AS IsEnable " +
                        "FROM month AS m " +
                        "WHERE m.isEnable = true AND m.name LIKE @filter;",
                        new { filter }
                    );

                    return response.Count() == 0 ? null : response.ToDTOList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
