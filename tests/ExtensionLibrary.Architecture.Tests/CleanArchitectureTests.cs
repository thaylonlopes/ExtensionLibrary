using System.Xml.Linq;
using AssemblyExtensionLibrary;
using CollectionExtensionsLibrary;
using DateTimeExtensionsLibrary;
using EnumExtensionsLibrary;
using NumericExtensionLibrary;
using ObjectExtensionsLibrary;
using QueryableExtensionsLibrary;
using StringExtensionLibrary;

namespace ExtensionLibrary.Architecture.Tests;

/// <summary>
/// Testes automatizados de conformidade arquitetural da Clean Architecture e governança de metapacotes.
/// </summary>
public class CleanArchitectureTests
{
    private static readonly string SolutionDirectory = FindSolutionDirectory();

    [Fact]
    public void DomainMetapackage_ShouldOnlyReferencePureDomainModules()
    {
        var projectPath = Path.Combine(SolutionDirectory, "metapackages", "TL.ExtensionLibrary.Domain", "TL.ExtensionLibrary.Domain.csproj");
        var references = GetProjectReferenceNames(projectPath);

        var expectedDomainProjects = new[]
        {
            "StringExtensionLibrary.csproj",
            "NumericExtensionLibrary.csproj",
            "DateTimeExtensionsLibrary.csproj",
            "EnumExtensionsLibrary.csproj",
            "CollectionExtensionsLibrary.csproj"
        };

        Assert.Equal(expectedDomainProjects.Length, references.Count);
        foreach (var expected in expectedDomainProjects)
        {
            Assert.Contains(references, r => r.EndsWith(expected, StringComparison.OrdinalIgnoreCase));
        }

        var forbiddenProjects = new[]
        {
            "QueryableExtensionsLibrary.csproj",
            "HttpClientExtensionsLibrary.csproj",
            "AssemblyExtensionLibrary.csproj",
            "ClaimsPrincipalExtensionsLibrary.csproj",
            "ObjectExtensionsLibrary.csproj"
        };

        foreach (var forbidden in forbiddenProjects)
        {
            Assert.DoesNotContain(references, r => r.EndsWith(forbidden, StringComparison.OrdinalIgnoreCase));
        }
    }

    [Fact]
    public void ApplicationMetapackage_ShouldReferenceDomainAndApplicationModulesOnly()
    {
        var projectPath = Path.Combine(SolutionDirectory, "metapackages", "TL.ExtensionLibrary.Application", "TL.ExtensionLibrary.Application.csproj");
        var references = GetProjectReferenceNames(projectPath);

        var expectedProjects = new[]
        {
            "TL.ExtensionLibrary.Domain.csproj",
            "ObjectExtensionsLibrary.csproj",
            "ClaimsPrincipalExtensionsLibrary.csproj"
        };

        Assert.Equal(expectedProjects.Length, references.Count);
        foreach (var expected in expectedProjects)
        {
            Assert.Contains(references, r => r.EndsWith(expected, StringComparison.OrdinalIgnoreCase));
        }

        var forbiddenProjects = new[]
        {
            "QueryableExtensionsLibrary.csproj",
            "HttpClientExtensionsLibrary.csproj",
            "AssemblyExtensionLibrary.csproj"
        };

        foreach (var forbidden in forbiddenProjects)
        {
            Assert.DoesNotContain(references, r => r.EndsWith(forbidden, StringComparison.OrdinalIgnoreCase));
        }
    }

    [Fact]
    public void InfrastructureMetapackage_ShouldReferenceApplicationAndInfrastructureModules()
    {
        var projectPath = Path.Combine(SolutionDirectory, "metapackages", "TL.ExtensionLibrary.Infrastructure", "TL.ExtensionLibrary.Infrastructure.csproj");
        var references = GetProjectReferenceNames(projectPath);

        var expectedProjects = new[]
        {
            "TL.ExtensionLibrary.Application.csproj",
            "QueryableExtensionsLibrary.csproj",
            "HttpClientExtensionsLibrary.csproj",
            "AssemblyExtensionLibrary.csproj"
        };

        Assert.Equal(expectedProjects.Length, references.Count);
        foreach (var expected in expectedProjects)
        {
            Assert.Contains(references, r => r.EndsWith(expected, StringComparison.OrdinalIgnoreCase));
        }
    }

    [Fact]
    public void Metapackages_MustHave_IncludeBuildOutput_ConfiguredToFalse()
    {
        var metapackagePaths = new[]
        {
            Path.Combine(SolutionDirectory, "metapackages", "TL.ExtensionLibrary.Domain", "TL.ExtensionLibrary.Domain.csproj"),
            Path.Combine(SolutionDirectory, "metapackages", "TL.ExtensionLibrary.Application", "TL.ExtensionLibrary.Application.csproj"),
            Path.Combine(SolutionDirectory, "metapackages", "TL.ExtensionLibrary.Infrastructure", "TL.ExtensionLibrary.Infrastructure.csproj")
        };

        foreach (var path in metapackagePaths)
        {
            var doc = XDocument.Load(path);
            var includeBuildOutput = doc.Descendants("IncludeBuildOutput").FirstOrDefault()?.Value;
            Assert.Equal("false", includeBuildOutput);
        }
    }

    [Fact]
    public void Metapackages_MustHave_PrivateAssetsNone_ConfiguredForAllReferences()
    {
        var metapackagePaths = new[]
        {
            Path.Combine(SolutionDirectory, "metapackages", "TL.ExtensionLibrary.Domain", "TL.ExtensionLibrary.Domain.csproj"),
            Path.Combine(SolutionDirectory, "metapackages", "TL.ExtensionLibrary.Application", "TL.ExtensionLibrary.Application.csproj"),
            Path.Combine(SolutionDirectory, "metapackages", "TL.ExtensionLibrary.Infrastructure", "TL.ExtensionLibrary.Infrastructure.csproj")
        };

        foreach (var path in metapackagePaths)
        {
            var doc = XDocument.Load(path);
            var projectReferences = doc.Descendants("ProjectReference");

            foreach (var reference in projectReferences)
            {
                var attributeValue = reference.Attribute("PrivateAssets")?.Value;
                var elementValue = reference.Element("PrivateAssets")?.Value;
                var privateAssets = attributeValue ?? elementValue;

                Assert.Equal("none", privateAssets);
            }
        }
    }

    [Fact]
    public void TransitiveDependencies_ShouldExecuteCorrectlyAcrossLayers()
    {
        const string text = "Clean Architecture";
        Assert.Equal("Clean...", text.Truncate(8, "..."));

        const int number = 7;
        Assert.True(number.IsPrime());

        var sampleList = new List<int> { 1, 2, 3, 4 };
        var chunks = sampleList.ChunkBy(2);
        Assert.Equal(2, chunks.Count);

        var sampleObject = new { Name = "CleanArchitecture" };
        var json = sampleObject.Serialize();
        Assert.Contains("CleanArchitecture", json, StringComparison.Ordinal);

        var queryable = new List<string> { "Alpha", "Beta", "Gamma" }.AsQueryable();
        var paged = queryable.Page(1, 2).ToList();
        Assert.Equal(2, paged.Count);

        var assemblyTypes = typeof(CleanArchitectureTests).Assembly.GetLoadableTypes();
        Assert.NotEmpty(assemblyTypes);
    }

    private static List<string> GetProjectReferenceNames(string projectPath)
    {
        var document = XDocument.Load(projectPath);
        return document.Descendants("ProjectReference")
            .Select(element => (string?)element.Attribute("Include") ?? string.Empty)
            .Where(include => !string.IsNullOrWhiteSpace(include))
            .ToList();
    }

    private static string FindSolutionDirectory()
    {
        var currentDirectory = AppContext.BaseDirectory;
        while (!string.IsNullOrEmpty(currentDirectory))
        {
            if (File.Exists(Path.Combine(currentDirectory, "ExtensionLibrary.sln")))
            {
                return currentDirectory;
            }

            var parent = Directory.GetParent(currentDirectory);
            if (parent == null)
            {
                break;
            }

            currentDirectory = parent.FullName;
        }

        return AppContext.BaseDirectory;
    }
}
