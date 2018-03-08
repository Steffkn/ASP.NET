namespace ElementsWeb.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BaseAtributes
    {
        [Key]
        public int Id { get; set; }

        public virtual CharacterClass CharacterClass { get; set; }

        [Required]
        public float MaxHealth { get; set; }

        [Required]
        public float HealthRegen { get; set; }

        [Required]
        public float MaxResource { get; set; }

        [Required]
        public float ResourceRegen { get; set; }

        [Required]
        public float AttackDamage { get; set; }

        [Required]
        public float MagicDamage { get; set; }

        [Required]
        public int Armor { get; set; }

        [Required]
        public int Stamina { get; set; }

        [Required]
        public int Strength { get; set; }

        [Required]
        public int Intellect { get; set; }

        [Required]
        public int Agility { get; set; }
    }
}
