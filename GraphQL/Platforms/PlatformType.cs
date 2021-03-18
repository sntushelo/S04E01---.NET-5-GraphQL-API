using System.Linq;
using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Types;

namespace CommanderGQL.GraphQL.Platforms
{
    // Having this class, allows us to remove the GraphQL specific code from our Platform model/entity
    // This is the code first approach
    public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor.Description("Represents any software or service that has a command line interafce.");

            descriptor                              //use Ignore() as we're not exposing this LicenseKey Property to GraphQL, 
                .Field(p => p.LicenseKey).Ignore(); //so no need for a Descriptor

            descriptor
                .Field(p => p.Commands)
                .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!)) //Map the Resolver with Descriptor
                .UseDbContext<AppDbContext>()
                .Description("This is the list of availble commands for this platform"); // Good documentation here :)
        }

        private class Resolvers
        {
            //This method tells our PlatformType how its going to get our commands
            public IQueryable<Command> GetCommands(Platform platform, [ScopedService] AppDbContext context)
            {
                return context.Commands.Where(p => p.PlatformId == platform.Id);
            }
        }
    }
}