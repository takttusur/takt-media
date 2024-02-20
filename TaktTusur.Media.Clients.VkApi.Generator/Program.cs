using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;

var directoryPath = @"..\..\..\..\TaktTusur.Media.Clients.VkApi\VkSchema\";
var outputPath = @"..\..\..\..\TaktTusur.Media.Clients.VkApi\VkSchema\Generated\";

if (!Directory.Exists(outputPath))
    Directory.CreateDirectory(outputPath);

foreach (var filePath in Directory.GetFiles(directoryPath, "*.json", SearchOption.AllDirectories))
{
    var classNamespace = filePath.Replace(directoryPath, "").Replace(".json", "");

    if (classNamespace.IndexOf(@"\") > 0)
        Directory.CreateDirectory(Path.Combine(outputPath, classNamespace[..classNamespace.IndexOf(@"\")]));

    var json = File.ReadAllText(filePath);
    var jsonSchema = JsonSchema.FromSampleJson(json);

    var settings = new CSharpGeneratorSettings() { Namespace = $"Generated.{classNamespace.Replace(@"\", ".")}" };
    var generator = new CSharpGenerator(jsonSchema, settings);
    var file = generator.GenerateFile();

    var outputFilePath = Path.Combine(outputPath, $"{classNamespace}.cs");
    File.WriteAllText(outputFilePath, file);
}