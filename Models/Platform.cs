using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace CommanderGQL.Models
{
    // Remove this to comply with Code First Approach
    // PlatformType class will host this
    //[GraphQLDescription("This is the platform model in my app")]
    public class Platform
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Remove this to comply with Code First Approach
        // PlatformType class will host this
        // [GraphQLDescription("This is the platform model license")]
        public string LicenseKey { get; set; }

        public ICollection<Command> Commands { get; set; } = new List<Command>();

    }
}