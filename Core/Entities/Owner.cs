namespace Core.Entities
{
    public class Owner :EntitiyBase
    {
        public string FullName { get; set; }
        public string Profile { get; set; }
        public string Avatar { get; set; }
        public Address Address { get; set; }
    }

}
