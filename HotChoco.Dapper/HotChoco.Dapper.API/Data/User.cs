namespace HotChoco.Dapper.API.Data
{
    public class User
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
        public string? Status { get; set; }

    }

}
