using Common;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DesktopClient
{
    public interface ICompilable
    {
        void Compile();
    }

    public class AssemblyCompiler : ISingleton
    {
        private static readonly object synchronizer = new object();
        public object Sinhronizer => synchronizer;


        public Predicate<In> GeneratePredicate<In>(string code)
        {
            string predParams = null;
            var sanitizedFuncBody = sanitizeCode(code, out predParams);
            if (!string.Empty.Equals(sanitizedFuncBody))
            {
                var finalCode = string.Format(this._predBodyIn1, typeof(In).FullName, predParams, sanitizedFuncBody);
                var methodDefinition = CreateMethod(finalCode, "GetPredicateIn1", typeof(In).Assembly.Location.ToList());
                return (Predicate<In>)methodDefinition.Invoke(null, null);
            }
            return null;
        }

        public Action<In> GenerateAction<In>(string code)
        {
            string funcParams = null;
            var sanitizedFuncBody = sanitizeCode(code, out funcParams);
            if (!string.Empty.Equals(sanitizedFuncBody))
            {
                var finalCode = string.Format(this._actionBodyIn1, typeof(In).FullName, funcParams, sanitizedFuncBody);
                var methodDefinition = CreateMethod(
                    finalCode,
                    "GetActionIn1",
                    typeof(In).Assembly.Location.ToList());
                return (Action<In>)methodDefinition.Invoke(null, null);
            }
            return null;
        }

        public Func<Out> GenerateFunc<Out>(string code)
        {
            var sanitizedFuncBody = sanitizeCode(code);
            if (!string.Empty.Equals(sanitizedFuncBody))
            {
                var finalCode = string.Format(this._funcBodyOut, typeof(Out).FullName, sanitizedFuncBody);
                var methodDefinition = CreateMethod(finalCode, "GetFuncOut", typeof(Out).Assembly.Location.ToList());
                return (Func<Out>)methodDefinition.Invoke(null, null);
            }
            return null;
        }

        public Func<In,Out> GenerateFunc<In,Out>(string code)
        {
            string funcParams = null;
            var sanitizedFuncBody = sanitizeCode(code, out funcParams);
            if (!string.Empty.Equals(sanitizedFuncBody))
            {
                var finalCode = string.Format(this._funcBodyInOut, typeof(In).FullName, typeof(Out).FullName, funcParams, sanitizedFuncBody);
                var methodDefinition = CreateMethod(
                    finalCode, 
                    "GetFuncInOut", 
                    new List<string>(new string[] {
                    typeof(In).Assembly.Location,
                    typeof(Out).Assembly.Location }));
                return (Func<In,Out>) methodDefinition.Invoke(null, null);
            }
            return null;
        }

        public Func<In1, In2, Out> GenerateFunc<In1, In2, Out>(string code)
        {
            string funcParams = null;
            var sanitizedFuncBody = sanitizeCode(code, out funcParams);
            if (!string.Empty.Equals(sanitizedFuncBody))
            {
                var finalCode = string.Format(this._funcBodyIn1In2Out, typeof(In1).FullName, typeof(In2).FullName, typeof(Out).FullName, funcParams, sanitizedFuncBody);
                var methodDefinition = CreateMethod(
                  finalCode,
                  "GetFuncIn1In2Out",
                  new List<string>(new string[] {
                    typeof(In1).Assembly.Location,
                    typeof(In2).Assembly.Location,
                    typeof(Out).Assembly.Location }));
                return (Func<In1, In2, Out>)methodDefinition.Invoke(null, null);
            }
            return null;
        }

        private MethodInfo CreateMethod(string code, string funcName, List<string> typedLibraries)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();

            typedLibraries.ForEach(typedLibrary => parameters.ReferencedAssemblies.Add(typedLibrary));
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.ReferencedAssemblies.Add("mscorlib.dll");
            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = false;
            

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

            Type binaryFunction = results.CompiledAssembly.GetType("DesktopClient.Dynamic");
            return binaryFunction.GetMethod(funcName);
        }

        private string sanitizeCode(string code)
        {
            List<string> codeStr = new List<string>();
            using (StringReader reader = new StringReader(code))
            {
                reader.ReadLine();
                reader.ReadLine(); //skip first 2 lines
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    codeStr.Add(line);
                }
            }
            codeStr.RemoveAt(codeStr.Count()-1);
            
            return codeStr.Count > 0 ? codeStr.Aggregate((aggr, s) => aggr + s) : string.Empty;
        }

        private string sanitizeCode(string code, out string param)
        {
            List<string> codeStr = new List<string>();
            using (StringReader reader = new StringReader(code))
            {
                param = getParameters(reader.ReadLine());
                reader.ReadLine(); //skip 2nd line
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    codeStr.Add(line);
                }
            }
            codeStr.RemoveAt(codeStr.Count() - 1);

            return codeStr.Count > 0 ? codeStr.Aggregate((aggr, s) => aggr + s) : string.Empty;
        }

        private string getParameters(string line)
        {
            string extractParamsRegex = @"\b[^()]+\((.*)\)$";
            var match = Regex.Match(line.Trim(), extractParamsRegex);
            var parameters = match.Groups[1].Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            return parameters.Select(p => p.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1]).Aggregate((s1, s2) => $"{s1},{s2}");
        }

        private readonly string _funcBodyOut = @"
using DesktopClient.SigningModel.DomainModel;
using System.Collections.Generic;
using System.Linq;
using System;


namespace DesktopClient
{{
    public class Dynamic
    {{
        public static Func<{0}> GetFuncOut(){{
            return () => 
            {{
                {1}
            }};
        }}
    }}
}}";

        private readonly string _funcBodyInOut = @"
using DesktopClient.SigningModel.DomainModel;
using System.Collections.Generic;
using System.Linq;
using System;
using Core.Nodes;

namespace DesktopClient
{{
    public class Dynamic
    {{
        public static Func<{0},{1}> GetFuncInOut(){{
            return ({2}) => 
            {{
                {3}
            }};
        }}
    }}
}}";

        private readonly string _funcBodyIn1In2Out = @"
using DesktopClient.SigningModel.DomainModel;
using System.Collections.Generic;
using System.Linq;
using System;
using Core.Nodes;

namespace DesktopClient
{{
    public class Dynamic
    {{
        public static Func<{0},{1},{2}> GetFuncIn1In2Out(){{
            return ({3}) => 
            {{
                {4}
            }};
        }}
    }}
}}";


        private readonly string _predBodyIn1 = @"
using DesktopClient.SigningModel.DomainModel;
using System.Collections.Generic;
using System.Linq;
using System;


namespace DesktopClient
{{
    public class Dynamic
    {{
        public static Predicate<{0}> GetPredicateIn1(){{
            return ({1}) => 
            {{
                {2}
            }};
        }}
    }}
}}";

        private readonly string _actionBodyIn1 = @"
using DesktopClient.SigningModel.DomainModel;
using System.Collections.Generic;
using System.Linq;
using System;


namespace DesktopClient
{{
    public class Dynamic
    {{
        public static Action<{0}> GetActionIn1(){{
            return ({1}) => 
            {{
                {2}
            }};
        }}
    }}
}}";

    }
}
