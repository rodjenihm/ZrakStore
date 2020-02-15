namespace ZrakStore.Data.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
    }

    public enum RoleType
    {
        User = 1,
        Admin = 2
    }
}
