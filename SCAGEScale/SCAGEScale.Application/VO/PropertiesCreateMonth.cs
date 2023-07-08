
using SCAGEScale.Application.DTO;

namespace SCAGEScale.Application.VO
{
    public class PropertiesCreateScale
    {
        public bool IsDay { get; set; }
        public string Sql { get; set; }
        public object Values { get; set; }
        public Guid? MonthId { get; set; }

        public PropertiesCreateScale() { }

        public PropertiesCreateScale(string sql, object values, Guid? monthId = null)
        {
            Sql = sql;
            Values = values;
            MonthId = monthId;
        }

        public static PropertiesCreateScale PropertiesToCreateMonth(CreateScaleDto month)
        {
            string sql = "INSERT INTO month(" +
                    "id, " +
                    "name, " +
                    "transmissions, " +
                    "start, " +
                    "end, " +
                    "status, " +
                    "createdOn, " +
                    "createdBy) " +
                "VALUES(" +
                    "@id, " +
                    "@name, " +
                    "@transmissions, " +
                    "@start, " +
                    "@end, " +
                    "@status, " +
                    "@createdOn, " +
                    "@createdBy) ";

            Guid monthId = Guid.NewGuid();

            object properties = new
            {
                id = monthId,
                name = month.Name,
                transmissions = month.Days.Count,
                start = month.Start,
                end = month.End,
                status = StatusScaleVo.IN_PROGRESS.ToString(),
                createdOn = DateTime.Now,
                createdBy = Guid.NewGuid()
            };

            var monthProperties = new PropertiesCreateScale(sql, properties, monthId);

            return monthProperties;
        }

        public static List<PropertiesCreateScale> PropertiesToCreateDay(List<DayDto> days, Guid monthId)
        {
            var listDays = new List<PropertiesCreateScale>();

            string sql = "INSERT INTO day(" +
                    "id, " +
                    "name, " +
                    "date, " +
                    "cameraOne, " +
                    "cameraTwo, " +
                    "cutDesk, " +
                    "month, " +
                    "createdOn, " +
                    "createdBy) " +
                "VALUES(" +
                    "@id, " +
                    "@name, " +
                    "@date, " +
                    "@cameraOne, " +
                    "@cameraTwo, " +
                    "@cutDesk, " +
                    "@month, " +
                    "@createdOn, " +
                    "@createdBy)";

            foreach (var day in days)
            {
                object properties = new
                {
                    id = Guid.NewGuid(),
                    name = day.Name,
                    date = day.DateTime,
                    cameraOne = day.CameraOne,
                    cameraTwo = day.CameraTwo,
                    cutDesk = day.CutDesk,
                    month = monthId,
                    createdOn = DateTime.Now,
                    createdBy = Guid.NewGuid()
                };

                listDays.Add(new PropertiesCreateScale(sql, properties));
            }
            return listDays;
        }
    }
}
