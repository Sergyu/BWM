using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesktopClient;
using DesktopClient.SigningModel.DomainModel;
using System.Collections.Generic;
using System.Reflection;

namespace UnitTests
{
    [TestClass]
    public class CompilerTests
    {
        private AssemblyCompiler compiler = new AssemblyCompiler();

        [TestMethod]
        public void SanitizeFuncBodyTest()
        {
            // odd number of lines  
            var funcBody = @"public void Action()
{
    var d = new Document;
    d.IsSet = true;
    return d;
}";

           
           // var sanitized = compiler.sanitizeCode(funcBody);

            // even number of lines
            funcBody = @"public void Action()
{
    var d = new Document;
    d.IsSet = true;
    d.Do();
    return d;
}";
           // sanitized = compiler.sanitizeCode(funcBody);

            funcBody = @"public void Action()
{
    var d = new Document;
    if(d.IsSet){
        d.Do();
    }
    return d;
}";
          //  sanitized = compiler.sanitizeCode(funcBody);
            var a = false;
        }


        [TestMethod]
        public void GetMethodInfoTest()
        {
            var funcBody = @"public Document Initialize()
{
    int partNr = 3;
    List<Available> availabilities = new List<Available>(new Available[]{ Available.Always, Available.Rarely, Available.Sometimes});
    var doc = new Document();

    for (int i = 0; i < partNr; i++)
    {
        var part = new Part();
        part.Signer = new Signer(availabilities[i]);
        doc.Parts.Add(part);
    }
    return doc;
}";
            Func<Document> initializer = null;
            initializer = compiler.GenerateFunc<Document>(funcBody);
            var doc = initializer();
    }
    }
}
