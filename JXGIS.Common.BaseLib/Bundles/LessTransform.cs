using dotless.Core;
using dotless.Core.Abstractions;
using dotless.Core.Importers;
using dotless.Core.Input;
using dotless.Core.Loggers;
using dotless.Core.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Optimization;

namespace JXGIS.Common.BaseLib
{
    public class LessTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse bundle)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (bundle == null)
            {
                throw new ArgumentNullException("bundle");
            }

            context.HttpContext.Response.Cache.SetLastModifiedFromFileDependencies();

            var lessParser = new Parser();
            ILessEngine lessEngine = CreateLessEngine(lessParser);

            var content = new StringBuilder(bundle.Content.Length);

            var bundleFiles = new List<BundleFile>();

            foreach (var bundleFile in bundle.Files)
            {
                bundleFiles.Add(bundleFile);

                var name = context.HttpContext.Server.MapPath(bundleFile.VirtualFile.VirtualPath);
                SetCurrentFilePath(lessParser, name);
                using (var stream = bundleFile.VirtualFile.Open())
                using (var reader = new StreamReader(stream))
                {
                    string source = reader.ReadToEnd();
                    content.Append(lessEngine.TransformToCss(source, name));
                    content.AppendLine();
                }

                bundleFiles.AddRange(GetFileDependencies(lessParser));
            }

            if (BundleTable.EnableOptimizations)
            {
                // include imports in bundle files to register cache dependencies
                bundle.Files = bundleFiles.Distinct();
            }

            bundle.ContentType = "text/css";
            bundle.Content = content.ToString();
        }

        /// <summary>
        /// Creates an instance of LESS engine.
        /// </summary>
        /// <param name="lessParser">The LESS parser.</param>
        private ILessEngine CreateLessEngine(Parser lessParser)
        {
            var logger = new AspNetTraceLogger(LogLevel.Debug, new Http());
            return new LessEngine(lessParser, logger, true, false);
        }

        /// <summary>
        /// Gets the file dependencies (@imports) of the LESS file being parsed.
        /// </summary>
        /// <param name="lessParser">The LESS parser.</param>
        /// <returns>An array of file references to the dependent file references.</returns>
        private IEnumerable<BundleFile> GetFileDependencies(Parser lessParser)
        {
            IPathResolver pathResolver = GetPathResolver(lessParser);

            foreach (var importPath in lessParser.Importer.Imports)
            {
                yield return
                    new BundleFile(pathResolver.GetFullPath(importPath),
                        HostingEnvironment.VirtualPathProvider.GetFile(importPath));
            }

            lessParser.Importer.Imports.Clear();
        }

        /// <summary>
        /// Returns an <see cref="IPathResolver"/> instance used by the specified LESS lessParser.
        /// </summary>
        /// <param name="lessParser">The LESS parser.</param>
        private IPathResolver GetPathResolver(Parser lessParser)
        {
            var importer = lessParser.Importer as Importer;
            var fileReader = importer.FileReader as FileReader;

            return fileReader.PathResolver;
        }

        /// <summary>
        /// Informs the LESS parser about the path to the currently processed file. 
        /// This is done by using a custom <see cref="IPathResolver"/> implementation.
        /// </summary>
        /// <param name="lessParser">The LESS parser.</param>
        /// <param name="currentFilePath">The path to the currently processed file.</param>
        private void SetCurrentFilePath(Parser lessParser, string currentFilePath)
        {
            var importer = lessParser.Importer as Importer;

            if (importer == null)
                throw new InvalidOperationException("Unexpected dotless importer type.");

            var fileReader = importer.FileReader as FileReader;

            if (fileReader == null || !(fileReader.PathResolver is ImportedFilePathResolver))
            {
                fileReader = new FileReader(new ImportedFilePathResolver(currentFilePath));
                importer.FileReader = fileReader;
            }
        }
    }
}
