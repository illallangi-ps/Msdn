using System;
using System.Collections.Generic;
using System.IO;
using Ninject.Extensions.Logging;

namespace Illallangi.Msdn.Client
{
    public sealed class FolderStructureClient : IFolderStructureClient
    {
        #region Fields

        private readonly ILogger currentLogger;
        private readonly IProductFamilyClient currentProductFamilyClient;
        private readonly IProductCategoryClient currentProductCategoryClient;
        private readonly IFileClient currentFileClient;

        #endregion

        #region Constructors

        public FolderStructureClient(IProductCategoryClient productCategoryClient,
            IProductFamilyClient productFamilyClient, IFileClient fileClient, ILogger logger)
        {
            this.currentProductFamilyClient = productFamilyClient;
            this.currentProductCategoryClient = productCategoryClient;
            this.currentFileClient = fileClient;
            this.currentLogger = logger;
        }

        #endregion

        #region Properties

        public ILogger Logger
        {
            get { return currentLogger; }
        }

        public IProductFamilyClient ProductFamilyClient
        {
            get { return currentProductFamilyClient; }
        }

        public IProductCategoryClient ProductCategoryClient
        {
            get { return currentProductCategoryClient; }
        }

        public IFileClient FileClient
        {
            get { return currentFileClient; }
        }

        #endregion

        #region Methods

        public IEnumerable<object> NewFolderStructure()
        {
            foreach (var msdnCategory in this.ProductCategoryClient.GetProductCategories())
            {
                foreach (var msdnFamily in this.ProductFamilyClient.GetProductFamiliesForCategory(msdnCategory.ProductGroupId))
                {
                    foreach (var msdnStubFile in this.FileClient.GetFileSearchResult(msdnFamily.ProductFamilyId))
                    {
                        var json = string.Empty;
                        Model.File msdnFile = null;

                        try
                        {
                            msdnFile = this.FileClient.GetFileDetail(msdnStubFile.FileId, out json);
                        }
                        catch (PathTooLongException e)
                        {
                            this.Logger.Error(e, "Unable to GetFileDetail for msdnStubFile.FileId = {0}", msdnStubFile.FileId);
                        }
                        catch (ArgumentException e)
                        {
                            this.Logger.Error(e, "Unable to GetFileDetail for msdnStubFile.FileId = {0}", msdnStubFile.FileId);
                        }

                        if (null == msdnFile)
                        {
                            continue;
                        }

                        var path = Path.Combine(Path.GetFullPath("."), msdnCategory.ToString(), msdnFamily.ToString(), msdnFile.ToString());
                        this.Logger.Debug(@"Directory.CreateDirectory(""{0}""", path);
                        try
                        {
                            Directory.CreateDirectory(path);
                        }
                        catch (PathTooLongException e)
                        {
                            this.Logger.Error(e, "Unable to create directory for msdnFile {0} (msdnFile.FileId = {1})", msdnFile.FileName, msdnFile.FileId);
                        }
                        catch (ArgumentException e)
                        {
                            this.Logger.Error(e, "Unable to create directory for msdnFile {0} (msdnFile.FileId = {1})", msdnFile.FileName, msdnFile.FileId);
                        }

                        try
                        {
                            if (!string.IsNullOrWhiteSpace(msdnFile.Notes))
                            {
                                File.WriteAllText(
                                    Path.Combine(path, "readme.html"),
                                    msdnFile.Notes);
                            }
                        }
                        catch (PathTooLongException e)
                        {
                            this.Logger.Error(e, "Unable to create readme.html for msdnFile {0} (msdnFile.FileId = {1})", msdnFile.FileName, msdnFile.FileId);
                        }
                        catch (ArgumentException e)
                        {
                            this.Logger.Error(e, "Unable to create readme.html for msdnFile {0} (msdnFile.FileId = {1})", msdnFile.FileName, msdnFile.FileId);
                        }

                        try
                        {
                            if (!string.IsNullOrWhiteSpace(json))
                            {
                                File.WriteAllText(
                                    Path.Combine(path, "fileinfo.json"),
                                    json);
                            }
                        }
                        catch (PathTooLongException e)
                        {
                            this.Logger.Error(e, "Unable to create fileinfo.json for msdnFile {0} (msdnFile.FileId = {1})", msdnFile.FileName, msdnFile.FileId);
                        }
                        catch (ArgumentException e)
                        {
                            this.Logger.Error(e, "Unable to create fileinfo.json for msdnFile {0} (msdnFile.FileId = {1})", msdnFile.FileName, msdnFile.FileId);
                        }

                        try
                        {
                            File.WriteAllText(
                                Path.Combine(path, "checksums.sha1"), 
                                string.Format("{0} *{1}", msdnFile.Sha1Hash, msdnFile.FileName));
                        }
                        catch (PathTooLongException e)
                        {
                            this.Logger.Error(e, "Unable to create checksums.sha1 for msdnFile {0} (msdnFile.FileId = {1})", msdnFile.FileName, msdnFile.FileId);
                        }
                        catch (ArgumentException e)
                        {
                            this.Logger.Error(e, "Unable to create checksums.sha1 for msdnFile {0} (msdnFile.FileId = {1})", msdnFile.FileName, msdnFile.FileId);
                        }
                    }
                }
            }
            yield break;
        }

        #endregion
    }
}