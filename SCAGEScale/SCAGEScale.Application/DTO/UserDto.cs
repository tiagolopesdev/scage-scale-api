
namespace SCAGEScale.Application.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }

        public static UserDto New(Guid id, string name, string email, string sex)
        {
            var dtoToReturn = new UserDto
            {
                Id = id,
                Name = name,
                Email = email,
                Sex = sex
            };
            return dtoToReturn;
        }
    }
}
