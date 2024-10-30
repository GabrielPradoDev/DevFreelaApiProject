namespace DevFreela.Core.Entities
{
    public class Skill : BaseEntity
    {
        public Skill(string description) : base()
        {
            Description = description;
        }

        public string Description { get; private set; }
        public List<UserSkill> UserSkiils{ get; private set; }
    }
}

