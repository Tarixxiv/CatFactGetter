using System.IO;
using CatFactGetter.Services;
using JetBrains.Annotations;
using Xunit;

namespace CatFactGetter.Tests.Services;

[TestSubject(typeof(FileService))]
public class FileServiceTest
{
    private readonly FileService _fileService = new();
    private readonly string _path = Path.GetTempFileName();

    [Fact]
    public void AppendLineCreatesFile()
    {
        //act
        _fileService.AddFactLine(_path,"abc");
        
        //assert
        Assert.True(File.Exists(_path));
    }
    
    [Fact]
    public void AppendLineAppendsLineToFile()
    {
        //arrange
        const string line = "line";
        const string line1 = "line1";
        
        //act
        _fileService.AddFactLine(_path,line);
        _fileService.AddFactLine(_path,line1);
        string readLine;
        string readLine1;
        string readLine2;
        using (var streamReader = new StreamReader(_path))
        {
            readLine = streamReader.ReadLine();
            readLine1 = streamReader.ReadLine();
            readLine2 = streamReader.ReadLine();
        }
        
        //assert
        Assert.Equal(line, readLine);
        Assert.Equal(line1, readLine1);
        Assert.Null(readLine2);
    }

    [Fact]
    public void AppendLineThrowsIoExceptionIfPathIsInvalid()
    {
        //act
        void Act() => _fileService.AddFactLine("C:\\invalid_path<name>.txt","abc");

        //assert
        Assert.Throws<IOException>(Act);
    }

    ~FileServiceTest()
    {
        if (File.Exists(_path)) File.Delete(_path);
    }
    
}