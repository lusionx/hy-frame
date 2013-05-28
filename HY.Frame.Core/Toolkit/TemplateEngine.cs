using RazorEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HY.Frame.Core.Toolkit
{
    public interface ITemplateEngine
    {
        string Parse(string template, dynamic model);
    }

    public class RazorTemplateEngine : ITemplateEngine
    {
        public string Parse(string template, dynamic model)
        {
            return Razor.Parse(template, model);
        }
    }

    public interface ITemplatesService
    {
        string Parse(string templateName, dynamic model, CultureInfo cultureInfo = null);
    }

    public interface IFileSystemService
    {
        string ReadAllText(string fileName);
        bool FileExists(string fileName);
        string GetCurrentDirectory();
    }

    public class FileSystemService : IFileSystemService
    {
        public string ReadAllText(string fileName)
        {
            return File.ReadAllText(fileName);
        }

        public bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        public string GetCurrentDirectory()
        {
            var cc = System.Web.HttpContext.Current;
            // for web app
            if (cc == null)
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
            else
            {
                return cc.Request.PhysicalApplicationPath;
            }
        }
    }

    public class TemplatesService : ITemplatesService
    {
        private const string DefaultLanguage = "en";
        private const string TemplatesDirectoryName = "Templates";
        private const string TemplateFileNameWithCultureTemplate = "{0}.{1}.template";
        private const string TemplateFileNameWithoutCultureTemplate = "{0}.template";

        private readonly IFileSystemService _fileSystemService;
        private readonly ITemplateEngine _templateEngine;
        private readonly string _templatesDirectoryFullName;

        public TemplatesService(IFileSystemService fileSystemService, ITemplateEngine templateEngine)
        {
            _fileSystemService = fileSystemService;
            _templateEngine = templateEngine;
            _templatesDirectoryFullName = Path.Combine(_fileSystemService.GetCurrentDirectory(), TemplatesDirectoryName);
        }

        // rest of the code

        public string Parse(string templateName, dynamic model, CultureInfo cultureInfo = null)
        {
            var templateContent = GetContent(templateName, cultureInfo);

            return _templateEngine.Parse(templateContent, model);
        }

        private string GetContent(string templateName, CultureInfo cultureInfo)
        {
            var templateFileName = TryGetFileName(templateName, cultureInfo);
            if (string.IsNullOrEmpty(templateFileName))
            {
                throw new FileNotFoundException(string.Format("Template file not found for template '{0}' in '{1}'", templateName, _templatesDirectoryFullName));
            }

            return _fileSystemService.ReadAllText(templateFileName);
        }

        private static string GetLanguageName(CultureInfo cultureInfo)
        {
            return cultureInfo != null ? cultureInfo.TwoLetterISOLanguageName.ToLower() : DefaultLanguage;
        }

        private string GetFullFileName(string templateName, string language)
        {
            var fileNameTemplate = string.IsNullOrEmpty(language) ? TemplateFileNameWithoutCultureTemplate : TemplateFileNameWithCultureTemplate;

            var templateFileName = string.Format(fileNameTemplate, templateName, language);

            return Path.Combine(_templatesDirectoryFullName, templateFileName);
        }

        private string TryGetFileName(string templateName, CultureInfo cultureInfo)
        {
            var language = GetLanguageName(cultureInfo);

            // check file for current culture
            var fullFileName = GetFullFileName(templateName, language);
            if (_fileSystemService.FileExists(fullFileName))
            {
                return fullFileName;
            }

            // check file for default culture
            if (language != DefaultLanguage)
            {
                fullFileName = GetFullFileName(templateName, DefaultLanguage);
                if (_fileSystemService.FileExists(fullFileName))
                {
                    return fullFileName;
                }
            }

            // check file without culture
            fullFileName = GetFullFileName(templateName, string.Empty);
            if (_fileSystemService.FileExists(fullFileName))
            {
                return fullFileName;
            }

            return string.Empty;
        }

    }
}
