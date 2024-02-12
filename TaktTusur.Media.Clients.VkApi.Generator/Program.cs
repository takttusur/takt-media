using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;

var directoryPath = @"..\..\TaktTusur.Media.Clients.VkApi\VkSchema\";
var outputPath = @"..\..\TaktTusur.Media.Clients.VkApi\VkSchema\Generated\";

foreach (var filePath in Directory.GetFiles(directoryPath, "*.json"))
{
    var document = JsonSchema.FromFileAsync(filePath).Result;
    var generator = new CSharpGenerator(document);
    var file = generator.GenerateFile();

    var outputFilePath = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(filePath) + ".cs");

    File.WriteAllText(outputFilePath, file);
}