using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;

var directoryPath = @"..\..\..\..\TaktTusur.Media.Clients.VkApi\VkSchema\";
var outputPath = @"..\..\..\..\TaktTusur.Media.Clients.VkApi\VkSchema\Generated\";

foreach (var filePath in Directory.GetFiles(directoryPath, "*.json", SearchOption.AllDirectories))
{
    var json = File.ReadAllText(filePath);
    var jsonSchema = JsonSchema.FromSampleJson(json);
    var settings = new CSharpGeneratorSettings() { Namespace = "JsonSchemeGeneratedClasses" };
    var generator = new CSharpGenerator(jsonSchema, settings);
    var file = generator.GenerateFile();

    var outputFilePath = Path.Combine(outputPath, filePath.Replace(directoryPath, "").Replace(".json", ".cs"));

    File.WriteAllText(outputFilePath, file);
}