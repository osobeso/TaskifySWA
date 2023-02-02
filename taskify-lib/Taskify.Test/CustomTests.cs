using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Reflection;

namespace Taskify.Test {
    [TestFixture]
    public class CustomTests {
        [Test]
        public void TestRoslynStuff() {
            // Read the content of the file CodeExample.cs as text
            // Fetch it from the directory where the output dll is located
            var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var codePath = Path.Combine(location, "CodeExample.txt");

            var code = File.ReadAllText(codePath);
            var text = SourceText.From(code);

            var projectId = ProjectId.CreateNewId();
            var documentId = DocumentId.CreateNewId(projectId);
            var versionStamp = VersionStamp.Create();

            // TODO: I need to create a unit test for the scenario I'm trying.
            // For now I'll just create a Dummy Prompt.
            // Assuming that I can get the data that I will need.

            var solution = new AdhocWorkspace().CurrentSolution
                .AddProject(projectId, "Taskify.Data", "Example", LanguageNames.CSharp)
                .AddDocument(documentId, "Program.cs", text, filePath: "Program.cs");

            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            var compilation = CSharpCompilation.Create("MyCompilation")
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddSyntaxTrees(syntaxTree);
            var semanticModel = compilation.GetSemanticModel(syntaxTree);

            var typeSyntaxes = syntaxTree.GetRoot().DescendantNodes().OfType<TypeSyntax>().Where(c => c.ChildNodes().Any());

            foreach (var ts in typeSyntaxes) {
                var typeInfo = semanticModel.GetTypeInfo(ts);
                var name = typeInfo.Type?.Name;
                var kind = typeInfo.Type?.TypeKind;
                var assembly = typeInfo.Type?.ContainingAssembly;
            }
            // var methodSymbol = semanticModel.GetDeclaredSymbol(methodDeclaration);

            // var enclosingSymbol = semanticModel.GetEnclosingSymbol(methodDeclaration.SpanStart);
            // Console.WriteLine("Enclosing symbol: " + enclosingSymbol.Name);
        }
    }
}
