using Newtonsoft.Json;

namespace OData.Business.Models
{
    public class PeopleRowModel
    {       
        [JsonProperty("value")]
        public List<PeopleModel> Data { get; set; }
    }
}
