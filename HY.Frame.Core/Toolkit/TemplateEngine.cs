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
    /// <summary>
    /// 
    /// </summary>
    public interface ITemplateEngine
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        string Parse(string template, dynamic model);
    }

    /// <summary>
    /// 
    /// </summary>
    public class RazorTemplateEngine : ITemplateEngine
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Parse(string template, dynamic model)
        {
            return Razor.Parse(template, model);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ITemplatesService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="model"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        string Parse(string templateName, dynamic model, CultureInfo cultureInfo = null);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IFileSystemService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string ReadAllText(string fileName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        bool FileExists(string fileName);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetCurrentDirectory();
    }

    /// <summary>
    /// 
    /// </summary>
    public class FileSystemService : IFileSystemService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string ReadAllText(string fileName)
        {
            return File.ReadAllText(fileName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
    /// <summary>
    /// 
    /// </summary>
    public class TemplatesService : ITemplatesService
    {
        private const string DefaultLanguage = "en";
        private const string TemplatesDirectoryName = "Templates";
        private const string TemplateFileNameWithCultureTemplate = "{0}.{1}.template";
        private const string TemplateFileNameWithoutCultureTemplate = "{0}.template";

        private readonly IFileSystemService _fileSystemService;
        private readonly ITemplateEngine _templateEngine;
        private readonly string _templatesDirectoryFullName;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSystemService"></param>
        /// <param name="templateEngine"></param>
        public TemplatesService(IFileSystemService fileSystemService, ITemplateEngine templateEngine)
        {
            _fileSystemService = fileSystemService;
            _templateEngine = templateEngine;
            _templatesDirectoryFullName = Path.Combine(_fileSystemService.GetCurrentDirectory(), TemplatesDirectoryName);
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="model"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
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
