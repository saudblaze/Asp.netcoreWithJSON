using FACTSERP.Models;
using System.Text.Json;

namespace FACTSERP.Utilities
{
    public  class HelperClass
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public HelperClass(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public ERPFacts GetDatabase()
        {
            

            var rootPath = _hostingEnvironment.ContentRootPath; //get the root path
            var fullPath = Path.Combine(rootPath, "JSONDatabase/ERPFacts.json"); //combine the root path with that of our json file inside mydata directory

            var jsonData = System.IO.File.ReadAllText(fullPath); //read all the content inside the file

            if (string.IsNullOrWhiteSpace(jsonData)) return null; //if no data is present then return null or error if you wish

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            //var jsonString = File.ReadAllText("my-model.json");
            var jsonModel = JsonSerializer.Deserialize<ERPFacts>(jsonData, options);
            var modelJson = JsonSerializer.Serialize(jsonModel, options);
            return jsonModel;
        }

        public bool SaveChanges(ERPFacts objERPFacts)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };            
            
            string modelJson = JsonSerializer.Serialize(objERPFacts, options);

            var rootPath = _hostingEnvironment.ContentRootPath; //get the root path
            var fullPath = Path.Combine(rootPath, "JSONDatabase/ERPFacts.json"); 
            System.IO.File.WriteAllText(fullPath, modelJson); //read all the content inside the file

            return true;
        }

    }

    


}
