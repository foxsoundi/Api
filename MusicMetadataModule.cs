using Nancy.Metadata.Modules;
using Nancy.Swagger;
using Swagger.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class MusicMetadataModule : MetadataModule<PathItem>
    {
        public MusicMetadataModule(ISwaggerModelCatalog modelCatalog)
        {
            modelCatalog.AddModels(typeof(Genre), typeof(IEnumerable<Genre>));

            Describe["GetGenre"] = desc => desc.AsSwagger(
                with => with.Operation(
                    op => op.OperationId("GetGenre")
                       //Tag means this api belongs to which group  
                       .Tag("Music")
                            //Description of this api  
                            .Description("This returns a list of genres")
                       //Summary of this api  
                       .Summary("Get all genres")
                       //Default response of the api  
                       .Response(r => r.Schema<IEnumerable<Genre>>(modelCatalog).Description("OK"))
                       ));
        }
    }
}
