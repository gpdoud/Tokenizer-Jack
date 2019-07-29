using System;
using System.Collections.Generic;
using System.IO;
using DSI.JsonLib;
using DSI.Xlmlib;

namespace DSI.Tokenizer {

    enum PathSpecType { File, Directory };
    class Program {

        void TokenizeFile(string JackFile, string XmlFile) {
            var xdoc = new Xml(XmlFile);
            var tokenizer = new Tokenizer(JackFile);
            string token = null;
            while((token = tokenizer.GetToken()) != null) {
                Console.WriteLine(token);
                var (newtoken, type) = Tokenizer.GetTokenType(token);
                xdoc.Add(newtoken, type);
            }
            xdoc.Dispose();
        }

        string[] GetJackFilesInDirectory(string directory) {
            return Directory.GetFiles(directory, "*.jack");
        }

        void Run(string path, PathSpecType pathSpec) {
            var jackFiles = new List<string>();
            if(pathSpec == PathSpecType.Directory) {
                jackFiles.AddRange(GetJackFilesInDirectory(path));
            } else { // only a single file
                jackFiles.Add(path);
            }
            // tokenize all files
            foreach(var jackFilename in jackFiles) {
                var jackFileInfo = new FileInfo(jackFilename);
                var filenameNoExt = Path.GetFileNameWithoutExtension(jackFilename);
                var filenameWithExt = string.Concat(filenameNoExt, ".xml");
                var xmlFilename = Path.Combine(jackFileInfo.DirectoryName, filenameWithExt);
                TokenizeFile(jackFilename, xmlFilename);
            }
        }
    
        static void Main(string[] args) {
            var pgm = new Program();
            if(args.Length != 1) throw new Exception("1 argument is required");
            var arg = args[0];
            // check for file or directory
            PathSpecType pathSpec;

            if(Directory.Exists(arg)) {
                pathSpec = PathSpecType.Directory;
            } else if(File.Exists(arg)) {
                pathSpec = PathSpecType.File;
            } else {
                throw new Exception("File or Directory does not exist!");
            }
            pgm.Run(arg, pathSpec);
        }
    }
}