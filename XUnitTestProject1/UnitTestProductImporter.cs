using System;
using Xunit;
using ProductImporter;
using System.IO;

namespace XUnitTestProject1
{
    //ImportProductDetail
    //ImportSoftwareAdviceFeed  
    //ImportCapterraFeed
    //ProcessTwitterText
    public class UnitTestProductImporter
    {
        /// <summary>
        /// This test case tests: 
        /// 1) result should not be null
        /// 2) result must be an array of length greater then 0
        /// </summary>
        /// <param name="userInput"></param>
        [Theory]
        [InlineData("import capterra C:/TempData/feed-products/capterra.yaml")]
        [InlineData("import softwareadvice C:/TempData/feed-products/softwareadvice.json")]
        public void ShouldImportProductDetails(string userInput)
        {
            string outputFormat = "importing: Name: \"{0}\";  Categories: {1}; Twitter: {2}";
            ProductImporter.ProductImporter productImporter = new ProductImporter.ProductImporter(outputFormat);
            var feeds = productImporter.ImportProductDetails(userInput);
            int resultLen = feeds != null ? feeds.Count : 0;

            Assert.NotNull(feeds);
            Assert.True(resultLen > 0);
        }

        [Theory]
        [InlineData("import C:/TempData/feed-products/capterra.yaml")]
        public void ShouldThrowInvalidOpException(string userInput)
        {
            string outputFormat = "importing: Name: \"{0}\";  Categories: {1}; Twitter: {2}";
            ProductImporter.ProductImporter productImporter = new ProductImporter.ProductImporter(outputFormat);
            var ex = Assert.Throws<InvalidOperationException>(() => productImporter.ImportProductDetails(userInput));
            Assert.Equal("Invalid input! Valid Syntax: <import> <capterra/softwareadvice/..> <file full path>", ex.Message);
        }

        [Theory]
        [InlineData("import Provider3 C:/TempData/feed-products/capterra.yaml")]
        public void ShouldThrowException_ProviderNotSupported(string userInput)
        {
            string outputFormat = "importing: Name: \"{0}\";  Categories: {1}; Twitter: {2}";
            ProductImporter.ProductImporter productImporter = new ProductImporter.ProductImporter(outputFormat);
            var ex = Assert.Throws<InvalidOperationException>(() => productImporter.ImportProductDetails(userInput));
            Assert.Equal("Provider not supported!", ex.Message);
        }

        [Theory]
        [InlineData("import capterra C:/TempData/feed-products/NoFile.yaml")]
        public void ShouldThrowFileNotFoundException(string userInput)
        {
            string outputFormat = "importing: Name: \"{0}\";  Categories: {1}; Twitter: {2}";
            ProductImporter.ProductImporter productImporter = new ProductImporter.ProductImporter(outputFormat);
            var ex = Assert.Throws<FileNotFoundException>(() => productImporter.ImportProductDetails(userInput));
        }
    }
}
