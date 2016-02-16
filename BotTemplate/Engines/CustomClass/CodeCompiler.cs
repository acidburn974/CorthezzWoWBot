using System.Collections.Generic;
using Microsoft.CSharp;
using System.IO;
using System.Windows.Forms;

namespace BotTemplate.Engines.CustomClass
{
    internal static class CodeCompiler
    {
        internal static System.CodeDom.Compiler.CompilerParameters GenerateParameters(string saveToPath)
        {
            System.CodeDom.Compiler.CompilerParameters para = new System.CodeDom.Compiler.CompilerParameters();
            para.GenerateExecutable = false;
            para.GenerateInMemory = false;
            para.OutputAssembly = saveToPath;
            para.ReferencedAssemblies.Add(System.Windows.Forms.Application.ExecutablePath);
            para.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            para.ReferencedAssemblies.Add("System.dll");
            return para;
        }

        internal static bool CreateAssemblyFromFolder(string pathTofolder, string saveTo)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            System.CodeDom.Compiler.CompilerResults results = codeProvider.CompileAssemblyFromSource(GenerateParameters(saveTo), GetFileContensFromDir(pathTofolder));
            if (results.Errors.Count > 0)
            {
                foreach (System.CodeDom.Compiler.CompilerError err in results.Errors)
                {

                }
                return false;
            }
            return true;
        }

        internal static bool CreateAssemblyFromFile(string pathToFile, string saveTo)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            System.CodeDom.Compiler.CompilerResults results = codeProvider.CompileAssemblyFromFile(GenerateParameters(saveTo), pathToFile);

            if (results.Errors.Count > 0)
            {
                foreach (System.CodeDom.Compiler.CompilerError err in results.Errors)
                {
                    MessageBox.Show("Error in " + err.FileName + " at Line " + err.Line + ": " + err.ErrorText);
                }
                return false;
            }
            return true;
        }

        private static string[] GetFileContensFromDir(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
            List<string> contents = new List<string>();
            foreach (string file in files)
            {
                if (!File.Exists(file))
                {
                    continue;
                }
                try
                {
                    TextReader tr = File.OpenText(file);
                    contents.Add(tr.ReadToEnd());
                    tr.Close();
                }
                catch { }
            }
            return contents.ToArray();
        }
    }
}
