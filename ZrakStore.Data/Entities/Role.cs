namespace ZrakStore.Data.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
    }

    public enum RoleType
    {
        user = 1,
        admin = 2
    }
}
